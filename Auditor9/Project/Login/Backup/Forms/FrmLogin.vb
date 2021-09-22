Public Class FrmLogin
    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Dim DtTemp As DataTable = Nothing
        Select Case sender.Name
            Case BtnOk.Name
                'FCreateDatabase()
                'FCreateTables(StrPath + "\" + IniName)
                If FOpenIni(StrPath + "\" + IniName, TxtUserName.Text, TxtPassword.Text) Then
                    If AgL.PubDivisionApplicable Then
                        DtTemp = AgL.FillData("SELECT D.* FROM Division D", AgL.GcnMain).Tables(0)
                        If DtTemp.Rows.Count = 1 Then
                            AgL.PubDivCode = AgL.XNull(DtTemp.Rows(0)("Div_Code"))
                            AgL.PubDivName = AgL.XNull(DtTemp.Rows(0)("Div_Name"))
                            AgL.PubDivisionDBName = AgL.XNull(DtTemp.Rows(0)("DataPath"))
                            FrmCompany.Show()
                        Else
                            FrmDivisionSelection.Show()
                        End If
                        DtTemp = Nothing
                    Else
                        FrmCompany.Show()
                    End If

                    Me.Hide()
                Else
                    TxtPassword.Focus()
                End If
            Case BtnCancel.Name
                Me.Dispose()
                End
        End Select

    End Sub

    Private Sub FrmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL = New AgLibrary.ClsMain()
        AgL.AglObj = AgL
        AgL.PubIsLogInProjectActive = True
        AgL.PubDivisionApplicable = True
    End Sub

    Private Sub FrmLogin_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
        LogoPictureBox.BackColor = Color.Transparent
    End Sub
    Private Sub TxtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles TxtPassword.KeyPress, TxtUserName.KeyPress

        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If e.KeyChar = Chr(Keys.Enter) And Not (TypeOf sender Is ComboBox) Then SendKeys.Send("{Tab}") : Exit Sub

        Try
            Agl.CheckQuote(e)
        Catch Ex As Exception
            MsgBox("System Exception : " & vbCrLf & Ex.Message, MsgBoxStyle.Exclamation, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub FCreateTables(ByVal StrIniPath As String)
        Dim mQry$ = ""
        'Dim Agl.Gcn As SqlClient.SqlConnection = New SqlClient.SqlConnection()
        Dim bDbUserSql$ = "", bDBPasswordSQL$ = "", bDataSource$ = "", bCompanyDBName$ = ""
        Try

            'bDbUserSql = "SA"
            'bDBPasswordSQL = ""
            'bDataSource = AgL.INIRead(StrIniPath, "Server", "Name", "")
            'bCompanyDBName = AgL.INIRead(StrIniPath, "CompanyInfo", "Path", "")

            'Agl.Gcn.ConnectionString = "Persist Security Info=False;User ID='" & bDbUserSql & "';pwd=" & bDBPasswordSQL & ";Initial Catalog=" & bCompanyDBName & ";Data Source=" & bDataSource
            'Agl.Gcn.Open()




            mQry = " SELECT Count(*) FROM INFORMATION_SCHEMA.Tables WHERE TABLE_NAME = 'Company'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) = 0 Then


                mQry = " CREATE TABLE Company " & _
                            " 	( " & _
                            " 	Comp_Code                NVARCHAR (5) NOT NULL, " & _
                            " 	Div_Code                 NVARCHAR (1) NULL, " & _
                            " 	Comp_Name                NVARCHAR (100) NULL, " & _
                            " 	CentralData_Path         NVARCHAR (100) NULL, " & _
                            " 	PrevDBName               VARCHAR (50) NULL, " & _
                            " 	DbPrefix                 VARCHAR (50) NULL, " & _
                            " 	Repo_Path                NVARCHAR (100) NULL, " & _
                            " 	Start_Dt                 SMALLDATETIME NULL, " & _
                            " 	End_Dt                   SMALLDATETIME NULL, " & _
                            " 	address1                 NVARCHAR (35) NULL, " & _
                            " 	address2                 NVARCHAR (35) NULL, " & _
                            " 	city                     NVARCHAR (35) NULL, " & _
                            " 	pin                      NVARCHAR (6) NULL, " & _
                            " 	phone                    NVARCHAR (30) NULL, " & _
                            " 	fax                      NVARCHAR (25) NULL, " & _
                            " 	lstno                    NVARCHAR (35) NULL, " & _
                            " 	lstdate                  NVARCHAR (12) NULL, " & _
                            " 	cstno                    NVARCHAR (35) NULL, " & _
                            " 	cstdate                  NVARCHAR (12) NULL, " & _
                            " 	cyear                    NVARCHAR (9) NULL, " & _
                            " 	pyear                    NVARCHAR (9) NULL, " & _
                            " 	SerialKeyNo              NVARCHAR (25) NULL, " & _
                            " 	SName                    NVARCHAR (15) NULL, " & _
                            " 	EMail                    VARCHAR (30) NULL, " & _
                            " 	Gram                     VARCHAR (15) NULL, " & _
                            " 	Desc1                    VARCHAR (100) NULL, " & _
                            " 	Desc2                    VARCHAR (100) NULL, " & _
                            " 	Desc3                    VARCHAR (50) NULL, " & _
                            " 	ECCCode                  VARCHAR (15) NULL, " & _
                            " 	ExDivision               VARCHAR (30) NULL, " & _
                            " 	ExRegNo                  VARCHAR (30) NULL, " & _
                            " 	ExColl                   VARCHAR (30) NULL, " & _
                            " 	ExRange                  VARCHAR (30) NULL, " & _
                            " 	Desc4                    VARCHAR (150) NULL, " & _
                            " 	VatNo                    VARCHAR (20) NULL, " & _
                            " 	VatDate                  SMALLDATETIME NULL, " & _
                            " 	TinNo                    VARCHAR (12) NULL, " & _
                            " 	Site_Code                VARCHAR (2) NULL, " & _
                            " 	LogSiteCode              VARCHAR (2) NULL, " & _
                            " 	PANNo                    VARCHAR (25) NULL, " & _
                            " 	State                    VARCHAR (35) NULL, " & _
                            " 	U_Name                   VARCHAR (35) NULL, " & _
                            " 	U_EntDt                  SMALLDATETIME NULL, " & _
                            " 	U_AE                     NVARCHAR (1) NULL, " & _
                            " 	DeletedYN                NVARCHAR (1) NULL, " & _
                            " 	Country                  NVARCHAR (50) NULL, " & _
                            " 	V_Prefix                 NVARCHAR (5) NULL, " & _
                            " 	NotificationNo           NVARCHAR (10) NULL, " & _
                            " 	WorkAddress1             NVARCHAR (35) NULL, " & _
                            " 	WorkAddress2             NVARCHAR (35) NULL, " & _
                            " 	WorkCity                 NVARCHAR (35) NULL, " & _
                            " 	WorkCountry              NVARCHAR (50) NULL, " & _
                            " 	WorkPin                  NVARCHAR (6) NULL, " & _
                            " 	WorkPhone                NVARCHAR (30) NULL, " & _
                            " 	WorkFax                  NVARCHAR (25) NULL, " & _
                            " 	WebServer                NVARCHAR (50) NULL, " & _
                            " 	WebUser                  NVARCHAR (50) NULL, " & _
                            " 	WebPassword              NVARCHAR (50) NULL, " & _
                            " 	Webdatabase              NVARCHAR (50) NULL, " & _
                            " 	RowId                    BIGINT IDENTITY NOT NULL, " & _
                            " 	UpLoadDate               SMALLDATETIME NULL, " & _
                            " 	UseSiteNameAsCompanyName BIT NULL, " & _
                            " 	CONSTRAINT PK_Company PRIMARY KEY (Comp_Code) " & _
                            " 	) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " CREATE TABLE SiteMast " & _
                            " 	( " & _
                            " 	Code          NVARCHAR (2) NOT NULL, " & _
                            " 	Name          NVARCHAR (50) NULL, " & _
                            " 	HO_YN         NVARCHAR (1) NULL, " & _
                            " 	Add1          NVARCHAR (50) NULL, " & _
                            " 	Add2          NVARCHAR (50) NULL, " & _
                            " 	Add3          NVARCHAR (50) NULL, " & _
                            " 	City_Code     NVARCHAR (7) NULL, " & _
                            " 	Phone         NVARCHAR (50) NULL, " & _
                            " 	Mobile        NVARCHAR (50) NULL, " & _
                            " 	PinNo         NVARCHAR (15) NULL, " & _
                            " 	U_Name        NVARCHAR (10) NULL, " & _
                            " 	U_EntDt       SMALLDATETIME NULL, " & _
                            " 	U_AE          NVARCHAR (1) NULL, " & _
                            " 	Edit_Date     SMALLDATETIME NULL, " & _
                            " 	ModifiedBy    NVARCHAR (10) NULL, " & _
                            " 	ManualCode    NVARCHAR (20) NULL, " & _
                            " 	RowId         BIGINT IDENTITY NOT NULL, " & _
                            " 	UpLoadDate    SMALLDATETIME NULL, " & _
                            " 	Active        BIT NULL, " & _
                            " 	AcCode        NVARCHAR (10) NULL, " & _
                            " 	SqlServer     NVARCHAR (50) NULL, " & _
                            " 	DataPath      NVARCHAR (50) NULL, " & _
                            " 	DataPathMain  NVARCHAR (50) NULL, " & _
                            " 	SqlUser       NVARCHAR (50) NULL, " & _
                            " 	SqlPassword   NVARCHAR (50) NULL, " & _
                            " 	CreditLimit   FLOAT NULL, " & _
                            " 	ApprovedBy    NVARCHAR (10) NULL, " & _
                            " 	ApprovedDate  SMALLDATETIME NULL, " & _
                            " 	GPX1          NVARCHAR (255) NULL, " & _
                            " 	GPX2          NVARCHAR (255) NULL, " & _
                            " 	GPN1          FLOAT NULL, " & _
                            " 	GPN2          FLOAT NULL, " & _
                            " 	Photo         IMAGE NULL, " & _
                            " 	LastNarration NVARCHAR (255) NULL, " & _
                            " 	CONSTRAINT PK_SiteMast PRIMARY KEY (Code) " & _
                            " 	) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " CREATE TABLE Division " & _
                        " 	( " & _
                        " 	Div_Code     NVARCHAR (1) NOT NULL, " & _
                        " 	Div_Name     NVARCHAR (100) NULL, " & _
                        " 	DataPath     NVARCHAR (50) NULL, " & _
                        " 	address1     NVARCHAR (35) NULL, " & _
                        " 	address2     NVARCHAR (35) NULL, " & _
                        " 	address3     NVARCHAR (35) NULL, " & _
                        " 	city         NVARCHAR (35) NULL, " & _
                        " 	pin          NVARCHAR (6) NULL, " & _
                        " 	PreparedBy   NVARCHAR (10) NULL, " & _
                        " 	U_EntDt      SMALLDATETIME NULL, " & _
                        " 	U_AE         NVARCHAR (1) NULL, " & _
                        " 	Edit_Date    SMALLDATETIME NULL, " & _
                        " 	ModifiedBy   NVARCHAR (10) NULL, " & _
                        " 	SitewiseV_No BIT CONSTRAINT DF_Division_SitewiseV_No DEFAULT ('0') NULL, " & _
                        " 	RowId        BIGINT IDENTITY NOT NULL, " & _
                        " 	UpLoadDate   SMALLDATETIME NULL, " & _
                        " 	ApprovedBy   NVARCHAR (10) NULL, " & _
                        " 	ApprovedDate SMALLDATETIME NULL, " & _
                        " 	GPX1         NVARCHAR (255) NULL, " & _
                        " 	GPX2         NVARCHAR (255) NULL, " & _
                        " 	GPN1         FLOAT NULL, " & _
                        " 	GPN2         FLOAT NULL, " & _
                        " 	CONSTRAINT PK_Division PRIMARY KEY (Div_Code) " & _
                        " 	) " & _
                        " CREATE UNIQUE NONCLUSTERED INDEX IX_Division " & _
                        " 	ON Division (Div_Name) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " CREATE TABLE UserMast " & _
                        " 	( " & _
                        " 	USER_NAME   NVARCHAR (10) NOT NULL, " & _
                        " 	Code        NVARCHAR (15) NULL, " & _
                        " 	PASSWD      NVARCHAR (16) NULL, " & _
                        " 	Description NVARCHAR (50) NULL, " & _
                        " 	Admin       NVARCHAR (1) NULL, " & _
                        " 	RowId       BIGINT IDENTITY NOT NULL, " & _
                        " 	UpLoadDate  SMALLDATETIME NULL, " & _
                        " 	CONSTRAINT PK_UserMast PRIMARY KEY (USER_NAME) " & _
                        " 	) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " CREATE TABLE User_Permission " & _
                        " 	( " & _
                        " 	UserName                NVARCHAR (10) NOT NULL, " & _
                        " 	MnuModule               NVARCHAR (50) NOT NULL, " & _
                        " 	MnuName                 NVARCHAR (100) NOT NULL, " & _
                        " 	MnuText                 NVARCHAR (100) NULL, " & _
                        " 	SNo                     INT NULL, " & _
                        " 	MnuLevel                INT NULL, " & _
                        " 	Parent                  NVARCHAR (50) NULL, " & _
                        " 	Permission              NVARCHAR (4) NULL, " & _
                        " 	ReportFor               NVARCHAR (50) NULL, " & _
                        " 	Active                  NVARCHAR (1) NULL, " & _
                        " 	RowId                   BIGINT IDENTITY NOT NULL, " & _
                        " 	UpLoadDate              SMALLDATETIME NULL, " & _
                        " 	MainStreamCode          VARCHAR (max) NULL, " & _
                        " 	GroupLevel              FLOAT NULL, " & _
                        " 	ControlPermissionGroups VARCHAR (max) NULL, " & _
                        " 	CONSTRAINT PK_User_Permission PRIMARY KEY (MnuModule,MnuName,UserName) " & _
                        " 	) " & _
                        " CREATE NONCLUSTERED INDEX IX_User_Permission " & _
                        " 	ON User_Permission (UserName,MnuModule,Parent) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " CREATE TABLE City " & _
                        " 	( " & _
                        " 	CityCode      NVARCHAR (6) NOT NULL, " & _
                        " 	CityName      NVARCHAR (50) NULL, " & _
                        " 	State         NVARCHAR (50) NULL, " & _
                        " 	IsDeleted     BIT NULL, " & _
                        " 	Country       NVARCHAR (50) NULL, " & _
                        " 	EntryBy       NVARCHAR (10) NULL, " & _
                        " 	EntryDate     SMALLDATETIME NULL, " & _
                        " 	EntryType     NVARCHAR (10) NULL, " & _
                        " 	EntryStatus   NVARCHAR (10) NULL, " & _
                        " 	ApproveBy     NVARCHAR (10) NULL, " & _
                        " 	ApproveDate   SMALLDATETIME NULL, " & _
                        " 	MoveToLog     NVARCHAR (10) NULL, " & _
                        " 	MoveToLogDate SMALLDATETIME NULL, " & _
                        " 	Status        NVARCHAR (10) NULL, " & _
                        " 	Div_Code      NVARCHAR (1) NULL, " & _
                        " 	UID           UNIQUEIDENTIFIER NULL, " & _
                        " 	CONSTRAINT PK_City PRIMARY KEY (CityCode) " & _
                        " 	) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " CREATE TABLE AcGroup " & _
                        " 	( " & _
                        " 	GroupCode       NVARCHAR (4) NOT NULL, " & _
                        " 	SNo             TINYINT NULL, " & _
                        " 	GroupName       NVARCHAR (50) NULL, " & _
                        " 	ContraGroupName NVARCHAR (50) NULL, " & _
                        " 	GroupUnder      NVARCHAR (4) NULL, " & _
                        " 	GroupNature     NVARCHAR (1) NULL, " & _
                        " 	Nature          NVARCHAR (15) NULL, " & _
                        " 	SysGroup        NVARCHAR (1) NULL, " & _
                        " 	U_Name          NVARCHAR (10) NULL, " & _
                        " 	U_EntDt         SMALLDATETIME NULL, " & _
                        " 	U_AE            NVARCHAR (1) NULL, " & _
                        " 	TradingYn       NVARCHAR (1) NULL, " & _
                        " 	MainGrCode      NVARCHAR (255) NULL, " & _
                        " 	BlOrd           FLOAT NULL, " & _
                        " 	MainGrLen       INT NULL, " & _
                        " 	ID              FLOAT NULL, " & _
                        " 	Site_Code       NVARCHAR (2) NULL, " & _
                        " 	GroupNameBiLang NVARCHAR (50) NULL, " & _
                        " 	GroupLevel      FLOAT NULL, " & _
                        " 	CurrentCount    FLOAT NULL, " & _
                        " 	CurrentBalance  FLOAT NULL, " & _
                        " 	SubLedYn        NVARCHAR (1) NULL, " & _
                        " 	AliasYn         NVARCHAR (1) NULL, " & _
                        " 	GroupHelp       NVARCHAR (50) NULL, " & _
                        " 	LastYearBalance FLOAT NULL, " & _
                        " 	RowId           BIGINT IDENTITY NOT NULL, " & _
                        " 	UpLoadDate      SMALLDATETIME NULL, " & _
                        " 	CONSTRAINT PK_AcGroup PRIMARY KEY (GroupCode), " & _
                        " 	CONSTRAINT FK_AcGroup_AcGroup_GroupUnder FOREIGN KEY (GroupUnder) REFERENCES AcGroup (GroupCode), " & _
                        " 	CONSTRAINT FK_AcGroup_SiteMast_Site_Code FOREIGN KEY (Site_Code) REFERENCES SiteMast (Code) " & _
                        " 	) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " CREATE TABLE SubGroup " & _
                        " 	( " & _
                        " 	SubCode              NVARCHAR (10) NOT NULL, " & _
                        " 	Site_Code            NVARCHAR (2) NULL, " & _
                        " 	Div_Code             NVARCHAR (1) NULL, " & _
                        " 	SiteList             NVARCHAR (500) NULL, " & _
                        " 	NamePrefix           NVARCHAR (10) NULL, " & _
                        " 	Name                 NVARCHAR (123) NULL, " & _
                        " 	DispName             NVARCHAR (100) NULL, " & _
                        " 	GroupCode            NVARCHAR (4) NULL, " & _
                        " 	GroupNature          NVARCHAR (1) NULL, " & _
                        " 	ManualCode           NVARCHAR (20) NULL, " & _
                        " 	Nature               NVARCHAR (11) NULL, " & _
                        " 	Add1                 NVARCHAR (50) NULL, " & _
                        " 	Add2                 NVARCHAR (50) NULL, " & _
                        " 	Add3                 NVARCHAR (50) NULL, " & _
                        " 	CityCode             NVARCHAR (6) NULL, " & _
                        " 	CountryCode          NVARCHAR (6) NULL, " & _
                        " 	PIN                  NVARCHAR (6) NULL, " & _
                        " 	Phone                NVARCHAR (35) NULL, " & _
                        " 	Mobile               NVARCHAR (35) NULL, " & _
                        " 	FAX                  NVARCHAR (35) NULL, " & _
                        " 	EMail                NVARCHAR (100) NULL, " & _
                        " 	CSTNo                NVARCHAR (40) NULL, " & _
                        " 	LSTNo                NVARCHAR (40) NULL, " & _
                        " 	TINNo                NVARCHAR (20) NULL, " & _
                        " 	PAN                  NVARCHAR (20) NULL, " & _
                        " 	TDS_Catg             NVARCHAR (6) NULL, " & _
                        " 	ActiveYN             NVARCHAR (1) NULL, " & _
                        " 	CreditLimit          FLOAT NULL, " & _
                        " 	CreditDays           SMALLINT NULL, " & _
                        " 	DueDays              INT NULL, " & _
                        " 	ContactPerson        NVARCHAR (100) NULL, " & _
                        " 	Party_Type           INT NULL, " & _
                        " 	PAdd1                NVARCHAR (50) NULL, " & _
                        " 	PAdd2                NVARCHAR (50) NULL, " & _
                        " 	PAdd3                NVARCHAR (50) NULL, " & _
                        " 	PCityCode            NVARCHAR (6) NULL, " & _
                        " 	PCountryCode         NVARCHAR (7) NULL, " & _
                        " 	PPin                 NVARCHAR (6) NULL, " & _
                        " 	PPhone               NVARCHAR (35) NULL, " & _
                        " 	PMobile              NVARCHAR (35) NULL, " & _
                        " 	PFax                 NVARCHAR (35) NULL, " & _
                        " 	Curr_Bal             FLOAT NULL, " & _
                        " 	OpBal_DocId          NVARCHAR (21) NULL, " & _
                        " 	FatherName           NVARCHAR (100) NULL, " & _
                        " 	FatherNamePrefix     NVARCHAR (10) NULL, " & _
                        " 	HusbandName          NVARCHAR (100) NULL, " & _
                        " 	HusbandNamePrefix    NVARCHAR (10) NULL, " & _
                        " 	DOB                  SMALLDATETIME NULL, " & _
                        " 	Remark               NVARCHAR (1) NULL, " & _
                        " 	Location             NVARCHAR (1) NULL, " & _
                        " 	U_Name               NVARCHAR (10) NULL, " & _
                        " 	U_EntDt              SMALLDATETIME NULL, " & _
                        " 	U_AE                 NVARCHAR (1) NULL, " & _
                        " 	Edit_Date            SMALLDATETIME NULL, " & _
                        " 	ModifiedBy           NVARCHAR (10) NULL, " & _
                        " 	ApprovedBy           NVARCHAR (10) NULL, " & _
                        " 	StCategory           NVARCHAR (6) NULL, " & _
                        " 	SiteStr              NVARCHAR (50) NULL, " & _
                        " 	STRegNo              NVARCHAR (25) NULL, " & _
                        " 	ECCNo                NVARCHAR (35) NULL, " & _
                        " 	EXREGNO              NVARCHAR (25) NULL, " & _
                        " 	CEXRANGE             NVARCHAR (25) NULL, " & _
                        " 	CEXDIV               NVARCHAR (25) NULL, " & _
                        " 	COMMRATE             NVARCHAR (25) NULL, " & _
                        " 	VATNo                NVARCHAR (35) NULL, " & _
                        " 	CommonAc             BIT CONSTRAINT DF_SubGroup_CommonAc DEFAULT ('1') NULL, " & _
                        " 	RowId                BIGINT NULL, " & _
                        " 	UpLoadDate           SMALLDATETIME NULL, " & _
                        " 	ChequeReport         NVARCHAR (50) NULL, " & _
                        " 	EntryBy              NVARCHAR (10) NULL, " & _
                        " 	EntryDate            SMALLDATETIME NULL, " & _
                        " 	EntryType            NVARCHAR (10) NULL, " & _
                        " 	EntryStatus          NVARCHAR (10) NULL, " & _
                        " 	ApproveBy            NVARCHAR (10) NULL, " & _
                        " 	ApproveDate          SMALLDATETIME NULL, " & _
                        " 	MoveToLog            NVARCHAR (10) NULL, " & _
                        " 	IsDeleted            BIT NULL, " & _
                        " 	MoveToLogDate        SMALLDATETIME NULL, " & _
                        " 	Status               NVARCHAR (20) NULL, " & _
                        " 	SisterConcernYn      BIT NULL, " & _
                        " 	UID                  UNIQUEIDENTIFIER NULL, " & _
                        " 	SalesTaxPostingGroup NVARCHAR (20) NULL, " & _
                        " 	ExcisePostingGroup   NVARCHAR (20) NULL, " & _
                        " 	EntryTaxPostingGroup NVARCHAR (20) NULL, " & _
                        " 	TDSCat_Description   NVARCHAR (6) NULL, " & _
                        " 	CONSTRAINT PK_SubGroup PRIMARY KEY (SubCode), " & _
                        " 	CONSTRAINT FK_SubGroup_City_CityCode FOREIGN KEY (CityCode) REFERENCES City (CityCode), " & _
                        " 	CONSTRAINT FK_SubGroup_AcGroup_GroupCode FOREIGN KEY (GroupCode) REFERENCES AcGroup (GroupCode), " & _
                        " 	CONSTRAINT FK_SubGroup_City_PCityCode FOREIGN KEY (PCityCode) REFERENCES City (CityCode), " & _
                        " 	CONSTRAINT FK_SubGroup_SiteMast_Site_Code FOREIGN KEY (Site_Code) REFERENCES SiteMast (Code) " & _
                        " 	) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " CREATE TABLE Enviro " & _
                    " 	( " & _
                    " 	ID                         NVARCHAR (4) NOT NULL, " & _
                    " 	Site_Code                  NVARCHAR (2) NULL, " & _
                    " 	Div_Code                   NVARCHAR (1) NULL, " & _
                    " 	CashAc                     NVARCHAR (10) NULL, " & _
                    " 	BankAc                     NVARCHAR (10) NULL, " & _
                    " 	TdsAc                      NVARCHAR (10) NULL, " & _
                    " 	AdditionAc                 NVARCHAR (10) NULL, " & _
                    " 	DeductionAc                NVARCHAR (10) NULL, " & _
                    " 	ServiceTaxAc               NVARCHAR (10) NULL, " & _
                    " 	ECessAc                    NVARCHAR (10) NULL, " & _
                    " 	RoundOffAc                 NVARCHAR (10) NULL, " & _
                    " 	HECessAc                   NVARCHAR (10) NULL, " & _
                    " 	ServiceTaxPer              FLOAT NULL, " & _
                    " 	ECessPer                   FLOAT NULL, " & _
                    " 	HECessPer                  FLOAT NULL, " & _
                    " 	RowId                      BIGINT IDENTITY NOT NULL, " & _
                    " 	UpLoadDate                 SMALLDATETIME NULL, " & _
                    " 	PreparedBy                 NVARCHAR (10) NULL, " & _
                    " 	U_EntDt                    SMALLDATETIME NULL, " & _
                    " 	U_AE                       NVARCHAR (1) NULL, " & _
                    " 	Edit_Date                  SMALLDATETIME NULL, " & _
                    " 	ModifiedBy                 NVARCHAR (10) NULL, " & _
                    " 	ApprovedBy                 NVARCHAR (10) NULL, " & _
                    " 	ApprovedDate               SMALLDATETIME NULL, " & _
                    " 	GPX1                       NVARCHAR (255) NULL, " & _
                    " 	GPX2                       NVARCHAR (255) NULL, " & _
                    " 	GPN1                       FLOAT NULL, " & _
                    " 	GPN2                       FLOAT NULL, " & _
                    " 	DefaultSalesTaxGroupParty  NVARCHAR (20) NULL, " & _
                    " 	DefaultSalesTaxGroupItem   NVARCHAR (20) NULL, " & _
                    " 	PurchOrderShowIndentInLine BIT CONSTRAINT DF_Enviro_PurchOrderShowIndentInLine DEFAULT ('0') NULL, " & _
                    " 	IsLinkWithFA               BIT NULL, " & _
                    " 	IsNegativeStockAllowed     BIT CONSTRAINT DF_Enviro_IsNegativeStockAllowed DEFAULT ('1') NULL, " & _
                    " 	IsLotNoApplicable          BIT CONSTRAINT DF_Enviro_IsLotNoApplicable DEFAULT ('1') NULL, " & _
                    " 	DefaultDueDays             FLOAT NULL, " & _
                    " 	CONSTRAINT PK_Enviro PRIMARY KEY (ID), " & _
                    " 	CONSTRAINT FK_Enviro_SubGroup_AdditionAc FOREIGN KEY (AdditionAc) REFERENCES SubGroup (SubCode), " & _
                    " 	CONSTRAINT FK_Enviro_SubGroup_BankAc FOREIGN KEY (BankAc) REFERENCES SubGroup (SubCode), " & _
                    " 	CONSTRAINT FK_Enviro_SubGroup_CashAc FOREIGN KEY (CashAc) REFERENCES SubGroup (SubCode), " & _
                    " 	CONSTRAINT FK_Enviro_SubGroup_DeductionAc FOREIGN KEY (DeductionAc) REFERENCES SubGroup (SubCode), " & _
                    " 	CONSTRAINT FK_Enviro_SubGroup_ECessAc FOREIGN KEY (ECessAc) REFERENCES SubGroup (SubCode), " & _
                    " 	CONSTRAINT FK_Enviro_SubGroup_HECessAc FOREIGN KEY (HECessAc) REFERENCES SubGroup (SubCode), " & _
                    " 	CONSTRAINT FK_Enviro_SubGroup_RoundOffAc FOREIGN KEY (RoundOffAc) REFERENCES SubGroup (SubCode), " & _
                    " 	CONSTRAINT FK_Enviro_SubGroup_ServiceTaxAc FOREIGN KEY (ServiceTaxAc) REFERENCES SubGroup (SubCode), " & _
                    " 	CONSTRAINT FK_Enviro_SiteMast_Site_Code FOREIGN KEY (Site_Code) REFERENCES SiteMast (Code), " & _
                    " 	CONSTRAINT FK_Enviro_SubGroup_TdsAc FOREIGN KEY (TdsAc) REFERENCES SubGroup (SubCode) " & _
                    " 	) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " CREATE TABLE Nature " & _
                        " 	( " & _
                        " 	Nature          NVARCHAR (50) NOT NULL, " & _
                        " 	Personal_Nature BIT CONSTRAINT DF_Nature_Personal_Nature DEFAULT ('0') NULL, " & _
                        " 	UploadDate      SMALLDATETIME NULL, " & _
                        " 	ApprovedBy      NVARCHAR (10) NULL, " & _
                        " 	ApprovedDate    SMALLDATETIME NULL, " & _
                        " 	CONSTRAINT PK_Nature PRIMARY KEY (Nature) " & _
                        " 	) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Company (Comp_Code, Div_Code, Comp_Name, CentralData_Path, PrevDBName, DbPrefix, Repo_Path, Start_Dt, End_Dt, address1, address2, city, pin, phone, fax, lstno, lstdate, cstno, cstdate, cyear, pyear, SerialKeyNo, SName, EMail, Gram, Desc1, Desc2, Desc3, ECCCode, ExDivision, ExRegNo, ExColl, ExRange, Desc4, VatNo, VatDate, TinNo, Site_Code, LogSiteCode, PANNo, State, U_Name, U_EntDt, U_AE, DeletedYN, Country, V_Prefix, NotificationNo, WorkAddress1, WorkAddress2, WorkCity, WorkCountry, WorkPin, WorkPhone, WorkFax, WebServer, WebUser, WebPassword, Webdatabase, UpLoadDate, UseSiteNameAsCompanyName) " & _
                        " VALUES ('1', 'D', 'Kanpur Consultants', 'HT1', NULL, 'HT', NULL, '2012-04-01', '2013-03-31', '13/383 Civil Lines', NULL, 'Kanpur', '221301', '-', '-', '-', NULL, '-', '12/Nov/2010', '2010-2011', '2009-2010', 'RA96082587', 'PP', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '2010-11-12', '-', NULL, NULL, '-', 'M.P.', 'SA', '2008-04-01', 'E', 'N', 'INDIA', '2010', '-', '-', '-', '-', '-', '-', '-', '-', NULL, NULL, NULL, NULL, NULL, 0) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO SiteMast (Code, Name, HO_YN, Add1, Add2, Add3, City_Code, Phone, Mobile, PinNo, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ManualCode, UpLoadDate, Active, AcCode, SqlServer, DataPath, DataPathMain, SqlUser, SqlPassword, CreditLimit, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, Photo, LastNarration) " & _
                        " VALUES ('1', 'Kanpur Consultants', 'N', '13/383 Civil Lines', 'Sant Ravidas Nagar', NULL, 'M10153', '0512-2317191', NULL, '221301', 'SA', '2008-08-06', 'E', '2010-05-21', 'sa', 'HO', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Division (Div_Code, Div_Name, DataPath, address1, address2, address3, city, pin, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, SitewiseV_No, UpLoadDate, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2) " & _
                        " VALUES ('D', 'Main', 'PP', '-', '-', '-', 'Kanpur', '-', 'SA', '2008-04-01', 'E', '2010-05-21', 'sa', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO UserMast (USER_NAME, Code, PASSWD, Description, Admin, UpLoadDate) " & _
                        " VALUES ('SA', '1', '@', 'CEO', 'Y', NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Bank', 1, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Cash', 0, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Customer', 1, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Employee', 1, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Expenses', 0, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Others', 0, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Purchase', 0, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Sales', 0, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Supplier', 1, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('T.D.S.', 0, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Transporter', 1, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " INSERT INTO Nature (Nature, Personal_Nature, UploadDate, ApprovedBy, ApprovedDate) " & _
                        " VALUES ('Unloader', 1, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If


            AgL.GCn.Close()
            AgL.GCn.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateDatabase()
        Dim DtTemp As DataTable = Nothing
        If FOpenUpdateIni() Then
            If ConnectDb(AgL.PubServerName, AgL.PubCompanyDBName, AgL.PubDBUserSQL, AgL.PubChkPasswordSQL) <> "" Then
                FrmServerNotFound.ShowDialog()
            End If
            'DtTemp = AgL.FillData("Select Count(*) from [User]  ", AgL.GCn).Tables(0)
            'If AgL.VNull(DtTemp.Rows(0)(0)) = 0 Then
            '    AgL.Dman_ExecuteNonQry("Insert Into [User](UserName, Password) Values ('SA','')", AgL.GCn)
            'End If
            'DtTemp = AgL.FillData("Select * from [User] Where UserName='" & TxtUserName.Text & "' ", AgL.GCn).Tables(0)
            'If DtTemp.Rows.Count > 0 Then
            '    'If AgL.StrCmp(AgL.DCODIFY(AgL.XNull(DtTemp.Rows(0)("Password"))), TxtPassword.Text) Then
            '    '    AgL.PubUserName = UCaseX(TxtUserName.Text)
            '    '    MDIMain.Show()
            '    '    Me.Hide()
            '    'End If
            'Else
            '    MsgBox("Invalid user name / password.")
            '    TxtPassword.Focus()
            'End If
        End If
    End Sub

    Public Function ConnectDb(ByVal ServerName As String, ByVal DatabaseName As String, Optional ByVal DatabaseUser As String = "sa", Optional ByVal DatabasePassword As String = "") As String
        Agl.AglObj = Agl
        Agl.PubDBUserSQL = "SA"
        Agl.GCn = New SqlClient.SqlConnection
        Agl.GcnRead = New SqlClient.SqlConnection
        Agl.Gcn_ConnectionString = "Persist Security Info=False;User ID='" & DatabaseUser & "';pwd=" & DatabasePassword & ";Initial Catalog=" & DatabaseName & ";Data Source=" & ServerName
        Agl.GCn.ConnectionString = Agl.Gcn_ConnectionString
        Agl.GcnRead.ConnectionString = Agl.Gcn_ConnectionString
        ConnectDb = ""
        Try
            AgL.GCn.Open()
            Agl.GcnRead.Open()
            Agl.ECmd = New SqlClient.SqlCommand
            Agl.ECmd = Agl.GCn.CreateCommand
        Catch ex As Exception
            ConnectDb = ex.Message
        End Try
    End Function

    Public Function FOpenUpdateIni() As Boolean
        Try
            AgL.PubDBUserSQL = "SA"
            AgL.PubServerName = AgL.INIRead(StrPath + "\" + IniName, "Server", "Name", "")
            AgL.PubCompanyDBName = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Path", "")
            AgL.PubChkPasswordSQL = AgL.INIRead(StrPath + "\" + IniName, "Security", "Password", "")
            FOpenUpdateIni = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
End Class
