Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports System.Math
Imports System.Net

Public Class ClsMain
#Region "Public Declaration"
    Public GCnComp As OleDb.OleDbConnection
    Public GCn As SqlClient.SqlConnection
	Public GcnRead As SqlClient.SqlConnection
    Public PubCompSerialNo As String
    Public PubServerName As String
    Public PubUserName As String
    Public PubUserPassword As String
    Public PubIsUserAdmin As Boolean
    Public PubUserMainStreamCode As String
    Public PubCompanyDBName As String
    Public PubLoginDate As String
    Public PubSiteCode As String
    Public PubDivCode As String
    Public PubSiteName As String
    Public PubSiteManualCode As String
    Public PubSiteAdd1 As String
    Public PubSiteAdd2 As String
    Public PubSiteAdd3 As String
    Public PubSiteCity As String
    Public PubSitePinNo As String
    Public PubSitePhone As String
    Public PubSiteMobile As String
    Public PubLogSiteName As String
    Public PubSiteCodeDisplay As String
    Public PubSitewiseV_No As Boolean   'Holds Whether Voucher Numbering will be Sitewise or Not.
    Public PubStartDate As String
    Public PubEndDate As String
    Public PubCompName As String
    Public PubCompShortName As String
    Public PubCompCode As String
    Public PubCompAdd1 As String
    Public PubCompAdd2 As String
    Public PubCompAdd3 As String
    Public PubCompEMail As String
    Public PubCompTIN As String
    Public PubCompPhone As String
    Public PubCompFax As String
    Public PubCompCST As String
    Public PubCompYear As String
    Public PubRegOfficeName As String
    Public PubRegOfficeAdd1 As String
    Public PubRegOfficeAdd2 As String
    Public PubRegOfficeAdd3 As String
    Public PubRegOfficeCity As String
    Public PubRegOfficePin As String
    Public PubRegOfficePhone As String
    Public PubRegOfficeMobile As String
    Public PubDBPrefix As String
    Public PubDBName As String
    Public PubPrevDBName As String

    Public PubAgReportPath As String
    Public PubReportPath As String
    Public PubReportFaPath As String
    Public PubReportPath_Utility As String
    Public PubReportPath_CommonData As String
    Public PubReportPath_Store As String
    Public PubReportPath_PayLite As String
    Public PubReportPath_SMS As String
    Public PubReportPath_EMail As String

    Public PubReportTitle As String
    Public PubCompCity As String
    Public PubCompCountry As String
    Public PubCompPinCode As String
    Public PubCompVPrefix As String
    Public PubLicenceNo As String
    Public PubRegistrationNo As String
    Public PubSiteListCharIndex As String
    Public PubSiteList As String
    Public PubDivisionList As String
    Public PubDivWiseBrowsing As Boolean
    Public PubDBUserSQL As String
    Public PubDBPasswordSQL As String
    Public PubChkPasswordSQL As String
    Public PubChkPasswordAccess As String
    Public PubFindQry As String             'Holds The Query Of Find For Forms
    Public PubFindQryOrdBy As String        'Holds The Query Order By Of Find For Forms 
    Public FindFormatStr() As Long
    Public PubObjFrmFind As Form
    Public PubObjFrmPaymentDetail As AgLibrary.FrmPaymentDetail
    Public PubSearchRow As String
    Public PubDRFound As DataRow
    Public Const PubMsgTitleInfo As String = "Information Window ...." 'Is For Title For MsgBox
    Public PubDataBackUpPath As String


    Public PubOutGoingMailId As String = ""
    Public PubOutGoingMailIdPassword As String = ""

    Public PubBlnIsBankMasterActive As Boolean = False
#End Region

    Public ClrPubBackColorForm As Color = Color.FromArgb(224, 224, 224)
    Public ADMain As SqlClient.SqlDataAdapter

    Public GcnMain_ConnectionString As String
    Public Gcn_ConnectionString As String
    Public GcnComp_ConnectionString As String
    Public ECompConn_ConnectionString As String
    Public GCnAdo_ConnectionString As String
    Public GcnMainAdo_ConnectionString As String
    Public GCnRep_ConnectionString As String
    Public GcnSiteComp_ConnectionString As String
    Public GcnSite_ConnectionString As String

    Public GcnLibrary_ConnectionString As String

    Public GcnMain As SqlClient.SqlConnection
    Public ECompConn As SqlClient.SqlConnection
    Public GCnAdo As ADODB.Connection
    Public GcnMainAdo As ADODB.Connection
    Public GCnRep As SqlClient.SqlConnection
    Public GcnSiteComp As SqlClient.SqlConnection
    Public GcnSite As SqlClient.SqlConnection
    Public GcnSiteRead As SqlClient.SqlConnection
    Public GcnLibrary As SqlClient.SqlConnection

    Public ECmd As SqlClient.SqlCommand
    Public ECompCmd As SqlClient.SqlCommand
    Public ECmdSite As SqlClient.SqlCommand
    Public ECompCmdSite As SqlClient.SqlCommand
    Public EAdptr As SqlClient.SqlDataAdapter
    Public EAdptrSite As SqlClient.SqlDataAdapter
    Public ETrans As SqlClient.SqlTransaction
    Public ETransSite As SqlClient.SqlTransaction

    Public PubIsLogInProjectActive As Boolean = False
    Public PubDivisionApplicable As Boolean = False
    Public PubIsHo As Boolean = False
    Public PubDivisionDBName As String = ""
    Public PubManageOfflineData As Boolean = False
    Public PubOfflineApplicable As Boolean = False
    Public PubMoveRecApplicable As Boolean = True
    Public PubUseSiteNameAsCompanyName As Boolean = False
    Public PubDbNameSite As String = ""
    Public PubDbMainNameSite As String = ""
    Public PubSqlServerSite As String = ""
    Public PubSqlUserSite As String = ""
    Public PubSqlPasswordSite As String = ""
    Public PubMachineName As String = ""
    Public PubKillerDate As String = ""
    Public BaseTableList As String = ""
    Public PubSiteCodeActual As String = ""
    Public PubSmsAPI As String = ""
    Public PubLongSmsLimit As Long = 0
    Public PubLibraryDbName As String = ""

    Public PubTallyIntegrationDbName As String = ""
    Public PubTallyCompanyName As String = ""

    Public PubKillerFile As String = ""
    
    Public PubDtEnviro_LedgerAccounts As DataTable = Nothing
    Public PubDtEnviro_SMS As DataTable = Nothing
    Public PubDtEnviro_EMail As DataTable = Nothing
    Public PubMdlTable As LITable()

    Dim mIs_Project_Running_Online As Boolean


    Public Shared PubBranchDivisionsMainGRCode As String = "070"
    Public Shared PubBranchDivisionsMainGRLen As Integer = 3

    Public Shared PubDutiesTaxesMainGRCode As String = "030001"
    Public Shared PubDutiesTaxesMainGRLen As Integer = 6

    Public Shared MainGRCodeBranchDivisions As String = "070"
    Public Shared MainGRLenBranchDivisions As Integer = 3

    Public Shared MainGRCodeDutiesTaxes As String = "030001"
    Public Shared MainGRLenDutiesTaxes As Integer = 6

    Public Shared MainGRCodeIndirectExpences As String = "280"
    Public Shared MainGRLenIndirectExpences As Integer = 3

    Public Shared MainGRCodeIndirectIncome As String = "270"
    Public Shared MainGRLenIndirectIncome As Integer = 3

    Public Shared MainGRCodeDirectExpences As String = "260"
    Public Shared MainGRLenDirectExpences As Integer = 3

    Public Shared MainGRCodeDirectIncome As String = "250"
    Public Shared MainGRLenDirectIncome As Integer = 3

    Public Shared MainGRCodeSales As String = "230"
    Public Shared MainGRLenSales As Integer = 3

    Public Shared MainGRCodePurchase As String = "240"
    Public Shared MainGRLenPurchase As Integer = 3

    Public Shared MainGRCodeCashInHand As String = "060005"
    Public Shared MainGRLenCashInHand As Integer = 6

    Public Shared MainGRCodeBank As String = "060006"
    Public Shared MainGRLenBank As Integer = 6
    Dim mAglObj As AgLibrary.ClsMain
    Public Shared PaymentModeCash As String = "CS"
    Public Shared PaymentModeCheque As String = "CH"
    Public Shared PaymentModeDD As String = "DD"
    Public Shared PaymentModeCreditCard As String = "CC"
    Public Shared PaymentModeAcTransfer As String = "TR"
    Public Shared PaymentModeOtherAdjustment As String = "OA"


    Public Shared NCat_Receipt As String = "RCPT"
    Public Shared NCat_Payment As String = "SPMT"
    Public Shared Cat_Receipt As String = "RCPT"
    Public Shared Cat_Payment As String = "SPMT"

    Public Property Is_Project_Running_Online() As Boolean
        Get
            Is_Project_Running_Online = mIs_Project_Running_Online
        End Get
        Set(ByVal value As Boolean)
            mIs_Project_Running_Online = value
        End Set
    End Property

    Public Property AglObj() As AgLibrary.ClsMain
        Get
            AglObj = mAglObj
        End Get
        Set(ByVal value As AgLibrary.ClsMain)
            mAglObj = value
        End Set
    End Property

    Public Enum DocIdPart
        Division
        Site
        ForSite
        VoucherType
        VoucherPrefix
        VoucherNo
    End Enum

    Public Enum EntryMode
        Add
        Edit
        Delete
    End Enum

    Public Enum FieldType
        StringType
        DateType
        NumType
    End Enum

    Public Enum ReportFormGlobalControls
        Date1_Control
        Date2_Control
        Cmbo1_Control
        Cmbo2_Control
        Cmbo3_Control
        Cmbo4_Control
        Cmbo5_Control
    End Enum

    Public Structure LedgRec
        Public SubCode As String
        Public AmtDr As Double
        Public AmtCr As Double
        Public ContraSub As String
        Public Narration As String
        Public ChqNo As String
        Public ChqDt As String
        Public ClrChqDt As String
        Public TDSOnAmt As Double

        Public Sub LedgRec()
            SubCode = ""
            AmtDr = 0
            AmtCr = 0
            ContraSub = ""
            Narration = ""
            ChqNo = ""
            ChqDt = ""
            ClrChqDt = ""
            TDSOnAmt = 0
        End Sub
    End Structure

    Public Structure PaymentDetail
        Public CashAc As String
        Public CashAmount As Double

        Public BankAc As String
        Public BankAmount As Double
        Public Bank_Code As String
        Public Bank_Name As String
        Public Chq_No As String
        Public Chq_Date As String
        Public Clg_Date As String

        Public CardAc As String
        Public CardAmount As Double
        Public CardBank_Code As String
        Public CardBank_Name As String
        Public Card_No As String

        Public AcTransferBankAc As String
        Public AcTransferAmount As Double
        Public AcTransferBank_Code As String
        Public AcTransferBank_Name As String
        Public AcTransferAcNo As String
        Public TotalAmount As Double
        Public PartyDrCr As String

        Public BankAc2 As String
        Public BankAmount2 As Double
        Public Bank_Code2 As String
        Public Bank_Name2 As String
        Public Chq_No2 As String
        Public Chq_Date2 As String
        Public Clg_Date2 As String

        Public BankAc3 As String
        Public BankAmount3 As Double
        Public Bank_Code3 As String
        Public Bank_Name3 As String
        Public Chq_No3 As String
        Public Chq_Date3 As String
        Public Clg_Date3 As String

        Public AdjustmentAc As String
        Public AdjustmentAmount As Double
        Public AdjustmentRemark As String

        Public Sub PaymentDetail()
            CashAc = ""
            CashAmount = 0

            BankAc = ""
            BankAmount = 0
            Bank_Code = ""
            Bank_Name = ""
            Chq_No = ""
            Chq_Date = ""
            Clg_Date = ""

            CardAc = ""
            CardAmount = 0
            CardBank_Code = ""
            CardBank_Name = ""
            Card_No = ""

            AcTransferBankAc = ""
            AcTransferAmount = 0
            AcTransferBank_Code = ""
            AcTransferBank_Name = ""
            AcTransferAcNo = ""
            TotalAmount = 0
            PartyDrCr = ""

            BankAc2 = ""
            BankAmount2 = 0
            Bank_Code2 = ""
            Bank_Name2 = ""
            Chq_No2 = ""
            Chq_Date2 = ""
            Clg_Date2 = ""

            BankAc3 = ""
            BankAmount3 = 0
            Bank_Code3 = ""
            Bank_Name3 = ""
            Chq_No3 = ""
            Chq_Date3 = ""
            Clg_Date3 = ""

            AdjustmentAc = ""
            AdjustmentAmount = 0
            AdjustmentRemark = ""
        End Sub
    End Structure

    Function GetColumnString(ByVal TableName As String, ByVal mConnectionString As String)
        Dim mQry$
        Dim myStr$ = ""
        Dim DtTemp As DataTable
        Dim GcnRead As New SqlClient.SqlConnection
        Dim I As Integer

        GcnRead.ConnectionString = mConnectionString
        GcnRead.Open()
        mQry = "SELECT C.name AS Column_Name " & _
               "FROM sys.all_columns C With (NoLock) " & _
               "LEFT JOIN sys.Objects O With (NoLock) ON C.object_id =O.object_id  " & _
               "WHERE C.is_identity =0 AND O.name ='" & TableName & "'"
        DtTemp = FillData(mQry, GcnRead).Tables(0)

        With DtTemp
            For I = 0 To DtTemp.Rows.Count - 1
                myStr += XNull(.Rows(I)("Column_Name")) + IIf(I <> DtTemp.Rows.Count - 1, ",", "")
            Next
        End With

        GetColumnString = myStr

        DtTemp.Dispose()
    End Function

    Function GetColumnDataTypeString(ByVal TableName As String, ByVal mConnectionString As String)
        Dim mQry$
        Dim myStr$ = ""
        Dim DtTemp As DataTable
        Dim GcnRead As New SqlClient.SqlConnection
        Dim I As Integer

        GcnRead.ConnectionString = mConnectionString
        GcnRead.Open()
        mQry = "SELECT Convert(nVarChar,C.User_Type_ID) AS Column_Name " & _
               "FROM sys.all_columns C With (NoLock) " & _
               "LEFT JOIN sys.Objects O With (NoLock) ON C.object_id =O.object_id  " & _
               "WHERE C.is_identity =0 AND O.name ='" & TableName & "'"
        DtTemp = FillData(mQry, GcnRead).Tables(0)

        With DtTemp
            For I = 0 To DtTemp.Rows.Count - 1
                myStr += XNull(.Rows(I)("Column_Name")) + IIf(I <> DtTemp.Rows.Count - 1, ",", "")
            Next
        End With

        GetColumnDataTypeString = myStr

        DtTemp.Dispose()
    End Function


    Public Sub ConnectLogInSite(ByVal AgL As ClsMain)
        Dim mQry$ = ""

        mQry = "sp_addlinkedsrvlogin '" & AgL.PubSqlServerSite & "',  'false', 'sa', '" & AgL.PubSqlUserSite & "', '" & AgL.PubSqlPasswordSite & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        AgL.GcnSite_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubSqlUserSite & "';pwd=" & AgL.PubSqlPasswordSite & ";Initial Catalog=" & AgL.PubDbNameSite & ";Data Source=" & AgL.PubSqlServerSite
        AgL.GcnSite = New SqlClient.SqlConnection
        AgL.GcnSite.ConnectionString = AgL.GcnSite_ConnectionString

        AgL.GcnSiteComp_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubSqlUserSite & "';pwd=" & AgL.PubSqlPasswordSite & ";Initial Catalog=" & AgL.PubDbMainNameSite & ";Data Source=" & AgL.PubSqlServerSite
        AgL.GcnSiteComp = New SqlClient.SqlConnection
        AgL.GcnSiteComp.ConnectionString = AgL.GcnSiteComp_ConnectionString

        AgL.GcnSite.Open()
        AgL.GcnSiteComp.Open()
    End Sub

#Region "SubRoutine Synchronise Web Database"
    Public Sub SynchroniseSiteOffineData(ByVal AgL As ClsMain, ByVal mConn As SqlClient.SqlConnection, ByVal mConnectionString As String, ByVal mConnectionStringSite As String, Optional ByVal mCmd As SqlClient.SqlCommand = Nothing)

        ''=====================================================================================================
        ''=======================<< Sychronising Masters>>=====================================================
        ''=========================<< of all Sites >>==========================================================
        ''=====================================================================================================
        Call SynchroniseSiteOffLineToOnline(AgL, mConn, mConnectionString, mConnectionStringSite, False, mCmd)
        'Call SynchroniseSiteOffLineTables(AgL, mConn, mConnectionString, mConnectionStringSite, False, mCmd)

        ''=====================================================================================================
        ''=======================<< Sychronising Transactions>>================================================
        ''=========================<< of all Sites >>==========================================================
        ''=====================================================================================================

        'Call SynchroniseSiteOffLineTables(AgL, mConn, mConnectionString, mConnectionStringSite, True, mCmd)

        ''=====================================================================================================
        ''=======================<< Sychronisation >>==========================================================
        ''=========================<< Complete >>==============================================================
        ''=====================================================================================================

    End Sub

    Private Sub SynchroniseSiteOffLineToOnline(ByVal AgL As ClsMain, ByVal mConn As SqlClient.SqlConnection, ByVal mConnectionString As String, ByVal mConnectionStringSite As String, ByVal mTransactionTables As Boolean, Optional ByVal mCmd As SqlClient.SqlCommand = Nothing)
        'SELECT RowId, SearchKey, AED, UpdateDate, TableName, Site, UploadDate, SearchField FROM dbo.Log_TableRecords
        If Not AgL.PubOfflineApplicable Then Exit Sub
        Dim DtRecord As DataTable = Nothing


        If AgL.PubSiteCode <> AgL.PubSiteCodeActual Then Exit Sub

        Dim DtTemp As DataTable = Nothing, DtTemp1 As DataTable = Nothing
        Dim I As Integer = 0, J As Integer = 0, K As Integer = 0
        Dim mQry$ = "", mColumnStr$ = "", mCondStr$ = ""
        Dim mColumns() As String = Nothing
        Dim mColumnsDataType() As String = Nothing
        Dim mFlag As Boolean = False



        mQry = "SELECT L.*, IsNull(T.TransactionYn,0) AS TransactionYn, IsNull(T.LineItemYn,0) AS LineItemYn " & _
                " FROM Log_TableRecords L  With (NoLock) " & _
                " LEFT JOIN Table_SearchField T With (NoLock) ON L.TableName = T.TABLE_NAME " & _
                " WHERE SubString(SearchKey,1,2)='S'+'" & AgL.PubSiteCode & "' And  charindex( '|" & AgL.PubSiteCode & "|',L.Site) = 0 And L.UploadDate IS NULL " & mCondStr & " " & _
                " ORDER BY L.RowId "
        DtTemp = AgL.FillData(mQry, AgL.GcnSiteRead).Tables(0)


        With DtTemp
            If .Rows.Count > 0 Then


                mQry = "Delete From Synchronise_Error"
                Dman_ExecuteNonQry(mQry, GcnSite)


                For I = 0 To .Rows.Count - 1
                    Try
                        If UTrim(XNull(.Rows(I)("AED"))) = "E" Then
                            mColumns = Split(GetColumnString(XNull(.Rows(I)("TableName")), AgL.Gcn_ConnectionString), ",")
                            mColumnsDataType = Split(GetColumnDataTypeString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString), ",")

                            mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            DtRecord = AgL.FillData(mQry, AgL.GcnSiteRead).Tables(0)

                            mQry = " Update " & XNull(.Rows(I)("TableName")) & "  Set  "

                            For J = 0 To mColumns.Length - 1
                                If mColumnsDataType(J) = "34" Then 'For Image Data Type
                                    If IsDBNull(DtRecord.Rows(0)(mColumns(J))) Then
                                        mQry = mQry & " " & mColumns(J) & " = Null" & IIf(J < mColumns.Length - 1, ",", "")
                                    Else
                                        mQry = mQry & " " & mColumns(J) & " = " & CreateImageFile(DtRecord.Rows(0)(mColumns(J))) & IIf(J < mColumns.Length - 1, ",", "")
                                    End If

                                Else
                                    mQry = mQry & " " & mColumns(J) & " = " & Chk_Text(XNull(DtRecord.Rows(0)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
                                End If
                            Next
                            mQry = mQry & " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' , UploadDate = " & AgL.ConvertDate(AgL.PubLoginDate) & " " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " "
                            Dman_ExecuteNonQry(mQry, AgL.GcnSite)

                        ElseIf UTrim(XNull(.Rows(I)("AED"))) = "D" Then
                            mQry = "Delete From " & XNull(.Rows(I)("TableName")) & " " & _
                                   "Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' , UploadDate = " & AgL.ConvertDate(AgL.PubLoginDate) & " " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " "
                            Dman_ExecuteNonQry(mQry, AgL.GcnSite)
                        ElseIf UTrim(XNull(.Rows(I)("AED"))) = "A" Then
                            mQry = "Delete From " & XNull(.Rows(I)("TableName")) & " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)

                            mColumnStr = GetColumnString(XNull(.Rows(I)("TableName")), AgL.Gcn_ConnectionString)
                            mColumnsDataType = Split(GetColumnDataTypeString(XNull(.Rows(I)("TableName")), AgL.Gcn_ConnectionString), ",")

                            mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            DtRecord = AgL.FillData(mQry, AgL.GcnSiteRead).Tables(0)

                            For K = 0 To DtRecord.Rows.Count - 1
                                mQry = "Insert Into " & XNull(.Rows(I)("TableName")) & " (" & mColumnStr & ") "
                                mQry = mQry & " Values ("
                                mColumns = Split(mColumnStr, ",")
                                For J = 0 To mColumns.Length - 1
                                    If mColumnsDataType(J) = "34" Then 'For Image DataType
                                        If IsDBNull(DtRecord.Rows(K)(mColumns(J))) Then
                                            mQry = mQry & " NULL " & IIf(J < mColumns.Length - 1, ",", "")
                                        Else
                                            mQry = mQry & " " & CreateImageFile(DtRecord.Rows(K)(mColumns(J))) & IIf(J < mColumns.Length - 1, ",", "")
                                        End If

                                    Else
                                        mQry = mQry & " " & Chk_Text(XNull(DtRecord.Rows(K)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
                                    End If

                                Next
                                mQry = mQry & ")"

                                Dman_ExecuteNonQry(mQry, mConn, mCmd)
                            Next K

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' , UploadDate = " & AgL.ConvertDate(AgL.PubLoginDate) & " " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " "
                            Dman_ExecuteNonQry(mQry, AgL.GcnSite)

                        End If
                    Catch ex As Exception
                        mQry = "Insert Into Synchronise_Error (RowId, Message) " & _
                               "Values ('" & VNull(.Rows(I)("RowId")) & "', " & AgL.Chk_Text(AgL.PubSiteCode + " - " + ex.Message) & ")"
                        Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)
                    End Try
                Next

            End If
        End With



        DtTemp.Dispose()
    End Sub

    Private Sub SynchroniseSiteOffLineTables(ByVal AgL As ClsMain, ByVal mConn As SqlClient.SqlConnection, ByVal mConnectionString As String, ByVal mConnectionStringSite As String, ByVal mTransactionTables As Boolean, Optional ByVal mCmd As SqlClient.SqlCommand = Nothing)
        'SELECT RowId, SearchKey, AED, UpdateDate, TableName, Site, UploadDate, SearchField FROM dbo.Log_TableRecords
        If Not AgL.PubOfflineApplicable Then Exit Sub
        Dim GcnRead As SqlClient.SqlConnection = Nothing
        Dim GcnSite As SqlClient.SqlConnection = Nothing
        Dim DtRecord As DataTable = Nothing
        Dim GcnSiteRead As SqlClient.SqlConnection
        Dim ECmdSite As SqlClient.SqlCommand


        If AgL.PubSiteCode <> AgL.PubSiteCodeActual Then Exit Sub

        Dim DtTemp As DataTable = Nothing, DtTemp1 As DataTable = Nothing
        Dim I As Integer = 0, J As Integer = 0, K As Integer = 0
        Dim mQry$ = "", mColumnStr$ = "", mCondStr$ = ""
        Dim mColumns() As String = Nothing
        Dim mColumnsDataType() As String = Nothing
        Dim mFlag As Boolean = False

        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = mConn.CreateCommand
            mCmd.CommandTimeout = 1024
        End If

        GcnRead = New SqlClient.SqlConnection
        GcnRead.ConnectionString = mConnectionString
        GcnRead.Open()


        GcnSite = New SqlClient.SqlConnection
        GcnSite.ConnectionString = mConnectionStringSite
        GcnSite.Open()

        GcnSiteRead = New SqlClient.SqlConnection
        GcnSiteRead.ConnectionString = mConnectionStringSite
        GcnSiteRead.Open()


        ECmdSite = New SqlCommand
        ECmdSite = mConn.CreateCommand
        ECmdSite.CommandTimeout = 1024


        ECmdSite = GcnSite.CreateCommand
        'ETransSite = GcnSite.BeginTransaction(IsolationLevel.ReadCommitted)
        'ECmdSite.Transaction = ETransSite



        If mTransactionTables Then
            mCondStr = "  AND IsNull(T.TransactionYn,0) <> 0 And SubString(L.SearchKey,2,1) = '" & AgL.PubSiteCode & "' "
            'mCondStr = "  AND IsNull(T.TransactionYn,0) <> 0 "
        Else
            mCondStr = "  AND IsNull(T.TransactionYn,0) = 0 "
        End If

        mQry = "sp_addlinkedsrvlogin '" & AgL.PubServerName & "', 'false', '" & AgL.PubDBName & "', '" & AgL.PubDBUserSQL & "', '" & AgL.PubDBPasswordSQL & "' "
        AgL.Dman_Execute(mQry, mConn)
        mQry = "SELECT L.*, IsNull(T.TransactionYn,0) AS TransactionYn, IsNull(T.LineItemYn,0) AS LineItemYn " & _
                " FROM Log_TableRecords L  With (NoLock) " & _
                " LEFT JOIN Table_SearchField T With (NoLock) ON L.TableName = T.TABLE_NAME " & _
                " WHERE charindex( '|" & ClsConstant.SiteCode_Reserve & "|',L.Site) = 0 And L.UploadDate IS NULL " & mCondStr & " " & _
                " ORDER BY L.RowId "
        DtTemp = AgL.FillData(mQry, GcnSiteRead).Tables(0)


        With DtTemp
            If .Rows.Count > 0 Then

                If Not mTransactionTables Then
                    AllowTableLog(True, GcnRead)
                Else
                    AllowTableLog(False, GcnRead)
                End If

                mQry = "Delete From Synchronise_Error"
                Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)


                For I = 0 To .Rows.Count - 1
                    Try
                        If UTrim(XNull(.Rows(I)("AED"))) = "E" Then
                            mColumns = Split(GetColumnString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString), ",")
                            mColumnsDataType = Split(GetColumnDataTypeString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString), ",")

                            mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            DtRecord = AgL.FillData(mQry, GcnSiteRead).Tables(0)

                            mQry = " Update " & XNull(.Rows(I)("TableName")) & "  Set  "

                            For J = 0 To mColumns.Length - 1
                                If mColumnsDataType(J) = "34" Then 'For Image Data Type
                                    If IsDBNull(DtRecord.Rows(0)(mColumns(J))) Then
                                        mQry = mQry & " " & mColumns(J) & " = Null" & IIf(J < mColumns.Length - 1, ",", "")
                                    Else
                                        mQry = mQry & " " & mColumns(J) & " = " & CreateImageFile(DtRecord.Rows(0)(mColumns(J))) & IIf(J < mColumns.Length - 1, ",", "")
                                    End If

                                Else
                                    mQry = mQry & " " & mColumns(J) & " = " & Chk_Text(XNull(DtRecord.Rows(0)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
                                End If
                            Next
                            mQry = mQry & " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & ClsConstant.SiteCode_Reserve & "|' , UploadDate = " & AgL.ConvertDate(AgL.PubLoginDate) & " " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " And " & _
                                    " charindex( '|" & ClsConstant.SiteCode_Reserve & "|',Site) = 0 "
                            Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)
                        ElseIf UTrim(XNull(.Rows(I)("AED"))) = "D" Then
                            mQry = "Delete From " & XNull(.Rows(I)("TableName")) & " " & _
                                   "Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & ClsConstant.SiteCode_Reserve & "|' , UploadDate = " & AgL.ConvertDate(AgL.PubLoginDate) & " " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " And " & _
                                    " charindex( '|" & ClsConstant.SiteCode_Reserve & "|',Site) = 0 "
                            Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)
                        ElseIf UTrim(XNull(.Rows(I)("AED"))) = "A" Then
                            mQry = "Delete From " & XNull(.Rows(I)("TableName")) & " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)

                            mColumnStr = GetColumnString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString)
                            mColumnsDataType = Split(GetColumnDataTypeString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString), ",")

                            mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            DtRecord = AgL.FillData(mQry, GcnSiteRead).Tables(0)

                            For K = 0 To DtRecord.Rows.Count - 1
                                mQry = "Insert Into " & XNull(.Rows(I)("TableName")) & " (" & mColumnStr & ") "
                                mQry = mQry & " Values ("
                                mColumns = Split(mColumnStr, ",")
                                For J = 0 To mColumns.Length - 1
                                    If mColumnsDataType(J) = "34" Then 'For Image DataType
                                        If IsDBNull(DtRecord.Rows(K)(mColumns(J))) Then
                                            mQry = mQry & " NULL " & IIf(J < mColumns.Length - 1, ",", "")
                                        Else
                                            mQry = mQry & " " & CreateImageFile(DtRecord.Rows(K)(mColumns(J))) & IIf(J < mColumns.Length - 1, ",", "")
                                        End If

                                    Else
                                        mQry = mQry & " " & Chk_Text(XNull(DtRecord.Rows(K)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
                                    End If

                                Next
                                mQry = mQry & ")"

                                Dman_ExecuteNonQry(mQry, mConn, mCmd)
                            Next K
                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & ClsConstant.SiteCode_Reserve & "|' , UploadDate = " & AgL.ConvertDate(AgL.PubLoginDate) & " " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " And " & _
                                    " charindex( '|" & ClsConstant.SiteCode_Reserve & "|',Site) = 0 "
                            Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)

                            If mTransactionTables Then UpdateVoucherCounter(XNull(.Rows(I)("SearchKey")), mConn, mCmd)
                        End If
                        mQry = "Update Log_TableRecords Set Site=IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' Where UniqueKey = '" & XNull(.Rows(I)("UniqueKey")) & "' And AED = '" & XNull(.Rows(I)("AED")) & "' And CharIndex('|" & AgL.PubSiteCode & "|',Site)=0 "
                        Dman_ExecuteNonQry(mQry, mConn, mCmd)
                    Catch ex As Exception
                        mQry = "Insert Into Synchronise_Error (RowId, Message) " & _
                               "Values ('" & VNull(.Rows(I)("RowId")) & "', " & AgL.Chk_Text(ex.Message) & ")"
                        Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)
                    End Try
                Next

                AllowTableLog(True, GcnRead)
            End If
        End With

        'ETransSite.Commit()

        DtTemp.Dispose()
        GcnRead.Dispose()

    End Sub
#End Region

#Region "SubRoutine Synchronise Site Database"
    Public Sub SynchroniseSiteOnLineData(ByVal AgL As ClsMain, ByVal mConn As SqlClient.SqlConnection, ByVal mConnectionString As String, ByVal mConnectionStringSite As String, Optional ByVal mCmd As SqlClient.SqlCommand = Nothing)

        ''=====================================================================================================
        ''=======================<< Sychronising Masters>>=====================================================
        ''=========================<< of all Sites >>==========================================================
        ''=====================================================================================================
        Call SynchroniseOnLineToOffline(AgL, mConn, mConnectionString, mConnectionStringSite, False, mCmd)
        'Call SynchroniseSiteOnLineTables(AgL, mConn, mConnectionString, mConnectionStringSite, False, mCmd)

        ''=====================================================================================================
        ''=======================<< Sychronising Transactions>>================================================
        ''=========================<< of all Sites >>==========================================================
        ''=====================================================================================================

        'Call SynchroniseSiteOnLineTables(AgL, mConn, mConnectionString, mConnectionStringSite, True, mCmd)

        ''=====================================================================================================
        ''=======================<< Sychronisation >>==========================================================
        ''=========================<< Complete >>==============================================================
        ''=====================================================================================================

    End Sub

    '' ''Private Sub SynchroniseSiteOnLineTables(ByVal AgL As ClsMain, ByVal mConn As SqlClient.SqlConnection, ByVal mConnectionString As String, ByVal mConnectionStringSite As String, ByVal mTransactionTables As Boolean, Optional ByVal mCmd As SqlClient.SqlCommand = Nothing)
    '' ''    'SELECT RowId, SearchKey, AED, UpdateDate, TableName, Site, UploadDate, SearchField FROM dbo.Log_TableRecords
    '' ''    If Not AgL.PubOfflineApplicable Then Exit Sub
    '' ''    Dim GcnRead As SqlClient.SqlConnection = Nothing
    '' ''    Dim DtTemp As DataTable = Nothing
    '' ''    Dim I As Integer = 0
    '' ''    Dim J As Integer = 0
    '' ''    Dim K As Integer = 0
    '' ''    Dim mQry$ = "", mColumnStr$ = "", mCondStr$ = ""
    '' ''    Dim mColumns() As String = Nothing
    '' ''    Dim mFlag As Boolean = False
    '' ''    Dim DtRecord As DataTable
    '' ''    Dim DtTemp1 As DataTable
    '' ''    Dim GcnSite As SqlClient.SqlConnection
    '' ''    Dim ETransSite As SqlClient.SqlTransaction
    '' ''    Dim ECmdSite As SqlClient.SqlCommand



    '' ''    If mCmd Is Nothing Then
    '' ''        mCmd = New SqlCommand
    '' ''        mCmd = mConn.CreateCommand
    '' ''        mCmd.CommandTimeout = 1024
    '' ''    End If

    '' ''    GcnRead = New SqlClient.SqlConnection
    '' ''    GcnRead.ConnectionString = mConnectionString
    '' ''    GcnRead.Open()

    '' ''    GcnSite = New SqlClient.SqlConnection
    '' ''    GcnSite.ConnectionString = mConnectionStringSite
    '' ''    GcnSite.Open()

    '' ''    ECmdSite = AgL.GcnSite.CreateCommand
    '' ''    ETransSite = AgL.GcnSite.BeginTransaction(IsolationLevel.ReadCommitted)
    '' ''    ECmdSite.Transaction = AgL.ETransSite


    '' ''    If mTransactionTables Then
    '' ''        mCondStr = "  AND IsNull(T.TransactionYn,0) <> 0 And SubString(L.SearchKey,2,1) = '" & AgL.PubSiteCode & "'"
    '' ''    Else
    '' ''        mCondStr = "  AND IsNull(T.TransactionYn,0) = 0 "
    '' ''    End If
    '' ''    mQry = "SELECT L.*, IsNull(T.TransactionYn,0) AS TransactionYn, IsNull(T.LineItemYn,0) AS LineItemYn " & _
    '' ''            " FROM Log_TableRecords L  With (NoLock) " & _
    '' ''            " LEFT JOIN Table_SearchField T With (NoLock) ON L.TableName = T.TABLE_NAME " & _
    '' ''            " WHERE charindex( '|" & AgL.PubSiteCode & "|',L.Site) = 0 " & mCondStr & " " & _
    '' ''            " ORDER BY L.RowId "
    '' ''    DtTemp = AgL.FillData(mQry, GcnRead).Tables(0)


    '' ''    With DtTemp
    '' ''        If .Rows.Count > 0 Then
    '' ''            AllowTableLog(False, GcnSite, )
    '' ''            For I = 0 To .Rows.Count - 1


    '' ''                If UTrim(XNull(.Rows(I)("AED"))) = "E" Then
    '' ''                    mColumns = Split(GetColumnString(XNull(.Rows(I)("TableName")), mConnectionStringSite), ",")

    '' ''                    mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
    '' ''                    DtRecord = AgL.FillData(mQry, GcnRead).Tables(0)

    '' ''                    mQry = " Update " & XNull(.Rows(I)("TableName")) & "  Set  "

    '' ''                    For J = 0 To mColumns.Length - 1
    '' ''                        mQry = mQry & " " & mColumns(J) & " = " & Chk_Text(XNull(DtRecord.Rows(0)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
    '' ''                    Next
    '' ''                    mQry = mQry & " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
    '' ''                    AgL.Dman_ExecuteNonQry(mQry, GcnSite)
    '' ''                ElseIf UTrim(XNull(.Rows(I)("AED"))) = "D" Then
    '' ''                    mQry = "Delete From " & XNull(.Rows(I)("TableName")) & " " & _
    '' ''                           "Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
    '' ''                    Dman_ExecuteNonQry(mQry, GcnSite)
    '' ''                ElseIf UTrim(XNull(.Rows(I)("AED"))) = "A" Then
    '' ''                    mQry = "Select IsNull(Count(*),0) Cnt " & _
    '' ''                            " From " & XNull(.Rows(I)("TableName")) & "  With (NoLock)  " & _
    '' ''                            " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
    '' ''                    DtTemp1 = AgL.FillData(mQry, GcnSite).Tables(0)
    '' ''                    If DtTemp1.Rows.Count > 0 Then
    '' ''                        If VNull(DtTemp1.Rows(0)("Cnt")) > 0 Then
    '' ''                            mFlag = False
    '' ''                        Else
    '' ''                            mFlag = True
    '' ''                        End If
    '' ''                        If mFlag = True Then
    '' ''                            mColumnStr = GetColumnString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString)
    '' ''                            mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
    '' ''                            DtRecord = AgL.FillData(mQry, GcnRead).Tables(0)

    '' ''                            For K = 0 To DtRecord.Rows.Count - 1
    '' ''                                mQry = "Insert Into " & XNull(.Rows(I)("TableName")) & " (" & mColumnStr & ") "
    '' ''                                mQry = mQry & " Values ("
    '' ''                                mColumns = Split(mColumnStr, ",")
    '' ''                                For J = 0 To mColumns.Length - 1
    '' ''                                    mQry = mQry & " " & Chk_Text(XNull(DtRecord.Rows(K)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
    '' ''                                Next
    '' ''                                mQry = mQry & ")"

    '' ''                                Dman_ExecuteNonQry(mQry, GcnSite)
    '' ''                            Next K
    '' ''                            If mTransactionTables Then UpdateVoucherCounter(XNull(.Rows(I)("SearchKey")), GcnSite)
    '' ''                            mFlag = False
    '' ''                        End If
    '' ''                    End If

    '' ''                End If



    '' ''                'If UTrim(XNull(.Rows(I)("AED"))) = "E" Or UTrim(XNull(.Rows(I)("AED"))) = "D" Then
    '' ''                '    mQry = "Delete From [" & AgL.PubSqlServerSite & "].[" & AgL.PubDbNameSite & "].dbo." & XNull(.Rows(I)("TableName")) & " " & _
    '' ''                '           "Where " & XNull(.Rows(I)("SearchField")) & " = '" & XNull(.Rows(I)("SearchKey")) & "' "
    '' ''                '    Dman_ExecuteNonQry(mQry, mConn, mCmd)
    '' ''                'End If

    '' ''                'If UTrim(XNull(.Rows(I)("AED"))) = "A" Or UTrim(XNull(.Rows(I)("AED"))) = "E" Then
    '' ''                '    mColumnStr = GetColumnString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString)
    '' ''                '    mQry = "Insert Into [" & AgL.PubSqlServerSite & "].[" & AgL.PubDbNameSite & "].dbo." & XNull(.Rows(I)("TableName")) & " (" & mColumnStr & ") " & _
    '' ''                '           "Select " & mColumnStr & " From " & XNull(.Rows(I)("TableName")) & "  With (NoLock) " & _
    '' ''                '           "Where " & XNull(.Rows(I)("SearchField")) & " = '" & XNull(.Rows(I)("SearchKey")) & "' " & _
    '' ''                '           "Order By RowId "
    '' ''                '    Dman_ExecuteNonQry(mQry, mConn, mCmd)
    '' ''                'End If

    '' ''                mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' " & _
    '' ''                        " Where RowId = " & VNull(.Rows(I)("RowId")) & " And " & _
    '' ''                        " charindex( '|" & AgL.PubSiteCode & "|',Site) = 0 "
    '' ''                Dman_ExecuteNonQry(mQry, mConn, mCmd)
    '' ''            Next
    '' ''            AllowTableLog(True, GcnSite)
    '' ''        End If
    '' ''    End With


    '' ''    DtTemp.Dispose()
    '' ''    GcnRead.Dispose()

    '' ''End Sub

    Private Sub SynchroniseOnLineToOffline(ByVal AgL As ClsMain, ByVal mConn As SqlClient.SqlConnection, ByVal mConnectionString As String, ByVal mConnectionStringSite As String, ByVal mTransactionTables As Boolean, Optional ByVal mCmd As SqlClient.SqlCommand = Nothing)
        'SELECT RowId, SearchKey, AED, UpdateDate, TableName, Site, UploadDate, SearchField FROM dbo.Log_TableRecords
        If Not AgL.PubOfflineApplicable Then Exit Sub

        Dim GcnRead As SqlClient.SqlConnection = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim mQry$ = "", mColumnStr$ = "", mCondStr$ = ""
        Dim mColumns() As String = Nothing
        Dim mColumnsDataType() As String = Nothing
        Dim mFlag As Boolean = False
        Dim DtRecord As DataTable
        Dim DtTemp1 As DataTable = Nothing




        If AgL.PubSiteCode <> AgL.PubSiteCodeActual Then Exit Sub


        mQry = "SELECT L.*, IsNull(T.TransactionYn,0) AS TransactionYn, IsNull(T.LineItemYn,0) AS LineItemYn " & _
                " FROM Log_TableRecords L  With (NoLock) " & _
                " LEFT JOIN Table_SearchField T With (NoLock) ON L.TableName = T.TABLE_NAME " & _
                " WHERE SubString(SearchKey,1,2)<>'S'+'" & AgL.PubSiteCode & "' And  charindex( '|" & AgL.PubSiteCode & "|',L.Site) = 0 " & mCondStr & " " & _
                " ORDER BY L.RowId "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        With DtTemp
            If .Rows.Count > 0 Then

                mQry = "Delete from Synchronise_Error"
                Dman_ExecuteNonQry(mQry, mConn, mCmd)

                For I = 0 To .Rows.Count - 1

                    Try
                        If UTrim(XNull(.Rows(I)("AED"))) = "E" Then
                            mColumns = Split(GetColumnString(XNull(.Rows(I)("TableName")), mConnectionStringSite), ",")
                            mColumnsDataType = Split(GetColumnDataTypeString(XNull(.Rows(I)("TableName")), mConnectionStringSite), ",")

                            mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            DtRecord = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                            mQry = " Update " & XNull(.Rows(I)("TableName")) & "  Set  "

                            For J = 0 To mColumns.Length - 1
                                If mColumnsDataType(J) = "34" Then  'For Image Data Type
                                    If IsDBNull(DtRecord.Rows(0)(mColumns(J))) Then
                                        mQry = mQry & " " & mColumns(J) & " = NULL " & IIf(J < mColumns.Length - 1, ",", "")
                                    Else
                                        mQry = mQry & " " & mColumns(J) & " = " & CreateImageFile(DtRecord.Rows(0)(mColumns(J))) & IIf(J < mColumns.Length - 1, ",", "")
                                    End If

                                Else
                                    mQry = mQry & " " & mColumns(J) & " = " & Chk_Text(XNull(DtRecord.Rows(0)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
                                End If
                            Next

                            mQry = mQry & " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " "
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)

                        ElseIf UTrim(XNull(.Rows(I)("AED"))) = "D" Then
                            mQry = "Delete From " & XNull(.Rows(I)("TableName")) & " " & _
                                   "Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            Dman_ExecuteNonQry(mQry, AgL.GcnSite)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " "
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)
                        ElseIf UTrim(XNull(.Rows(I)("AED"))) = "A" Then
                            mQry = "Delete From " & XNull(.Rows(I)("TableName")) & " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            Dman_ExecuteNonQry(mQry, AgL.GcnSite)
                            mColumnStr = GetColumnString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString)
                            mColumnsDataType = Split(GetColumnDataTypeString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString), ",")
                            mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            DtRecord = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                            For K = 0 To DtRecord.Rows.Count - 1
                                mQry = "Insert Into " & XNull(.Rows(I)("TableName")) & " (" & mColumnStr & ") "
                                mQry = mQry & " Values ("
                                mColumns = Split(mColumnStr, ",")
                                For J = 0 To mColumns.Length - 1
                                    If mColumnsDataType(J) = "34" Then
                                        If IsDBNull(DtRecord.Rows(0)(mColumns(J))) Then
                                            mQry = mQry & " NULL " & IIf(J < mColumns.Length - 1, ",", "")
                                        Else
                                            mQry = mQry & " " & CreateImageFile(DtRecord.Rows(K)(mColumns(J))) & IIf(J < mColumns.Length - 1, ",", "")
                                        End If

                                    Else
                                        mQry = mQry & " " & Chk_Text(XNull(DtRecord.Rows(K)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
                                    End If
                                Next
                                mQry = mQry & ")"

                                Dman_ExecuteNonQry(mQry, AgL.GcnSite)
                            Next K

                            If mTransactionTables Then UpdateVoucherCounter(XNull(.Rows(I)("SearchKey")), GcnSite, ECmdSite)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " "
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)
                        End If




                    Catch ex As Exception
                        mQry = "Insert Into Synchronise_Error (RowId, Message) " & _
                               "Values ('" & VNull(.Rows(I)("RowId")) & "', " & AgL.Chk_Text(AgL.PubSiteCode + " - " + ex.Message) & ")"
                        Dman_ExecuteNonQry(mQry, mConn, mCmd)
                    End Try
                Next

            End If
        End With


        DtTemp.Dispose()


    End Sub

    Private Sub SynchroniseSiteOnLineTables(ByVal AgL As ClsMain, ByVal mConn As SqlClient.SqlConnection, ByVal mConnectionString As String, ByVal mConnectionStringSite As String, ByVal mTransactionTables As Boolean, Optional ByVal mCmd As SqlClient.SqlCommand = Nothing)
        'SELECT RowId, SearchKey, AED, UpdateDate, TableName, Site, UploadDate, SearchField FROM dbo.Log_TableRecords
        If Not AgL.PubOfflineApplicable Then Exit Sub

        Dim GcnRead As SqlClient.SqlConnection = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim mQry$ = "", mColumnStr$ = "", mCondStr$ = ""
        Dim mColumns() As String = Nothing
        Dim mColumnsDataType() As String = Nothing
        Dim mFlag As Boolean = False
        Dim DtRecord As DataTable
        Dim DtTemp1 As DataTable = Nothing
        Dim GcnSiteRead As SqlClient.SqlConnection
        Dim GcnSite As SqlClient.SqlConnection
        Dim ECmdSite As SqlClient.SqlCommand


        If AgL.PubSiteCode <> AgL.PubSiteCodeActual Then Exit Sub

        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = mConn.CreateCommand
            mCmd.CommandTimeout = 1024
        End If

        GcnRead = New SqlClient.SqlConnection
        GcnRead.ConnectionString = mConnectionString
        GcnRead.Open()

        GcnSite = New SqlClient.SqlConnection
        GcnSite.ConnectionString = mConnectionStringSite
        GcnSite.Open()

        GcnSiteRead = New SqlClient.SqlConnection
        GcnSiteRead.ConnectionString = mConnectionStringSite
        GcnSiteRead.Open()

        ECmdSite = New SqlCommand
        ECmdSite = mConn.CreateCommand
        ECmdSite.CommandTimeout = 1024

        ECmdSite = GcnSite.CreateCommand
        'ETransSite = GcnSite.BeginTransaction(IsolationLevel.ReadCommitted)
        'ECmdSite.Transaction = ETransSite


        If mTransactionTables Then
            mCondStr = "  AND IsNull(T.TransactionYn,0) <> 0 And SubString(L.SearchKey,2,1) = '" & AgL.PubSiteCode & "'"
            'mCondStr = "  AND IsNull(T.TransactionYn,0) <> 0 "
        Else
            mCondStr = "  AND IsNull(T.TransactionYn,0) = 0 "
        End If

        mQry = "SELECT L.*, IsNull(T.TransactionYn,0) AS TransactionYn, IsNull(T.LineItemYn,0) AS LineItemYn " & _
                " FROM Log_TableRecords L  With (NoLock) " & _
                " LEFT JOIN Table_SearchField T With (NoLock) ON L.TableName = T.TABLE_NAME " & _
                " WHERE charindex( '|" & AgL.PubSiteCode & "|',L.Site) = 0 " & mCondStr & " " & _
                " ORDER BY L.RowId "
        DtTemp = AgL.FillData(mQry, GcnRead).Tables(0)

        With DtTemp
            If .Rows.Count > 0 Then
                AllowTableLog(False, GcnSite, ECmdSite)

                mQry = "Delete from Synchronise_Error"
                Dman_ExecuteNonQry(mQry, mConn, mCmd)

                For I = 0 To .Rows.Count - 1

                    Try
                        If UTrim(XNull(.Rows(I)("AED"))) = "E" Then
                            mColumns = Split(GetColumnString(XNull(.Rows(I)("TableName")), mConnectionStringSite), ",")
                            mColumnsDataType = Split(GetColumnDataTypeString(XNull(.Rows(I)("TableName")), mConnectionStringSite), ",")

                            mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            DtRecord = AgL.FillData(mQry, GcnRead).Tables(0)

                            mQry = " Update " & XNull(.Rows(I)("TableName")) & "  Set  "

                            For J = 0 To mColumns.Length - 1
                                If mColumnsDataType(J) = "34" Then  'For Image Data Type
                                    If IsDBNull(DtRecord.Rows(0)(mColumns(J))) Then
                                        mQry = mQry & " " & mColumns(J) & " = NULL " & IIf(J < mColumns.Length - 1, ",", "")
                                    Else
                                        mQry = mQry & " " & mColumns(J) & " = " & CreateImageFile(DtRecord.Rows(0)(mColumns(J))) & IIf(J < mColumns.Length - 1, ",", "")
                                    End If

                                Else
                                    mQry = mQry & " " & mColumns(J) & " = " & Chk_Text(XNull(DtRecord.Rows(0)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
                                End If
                            Next

                            mQry = mQry & " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            AgL.Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " And " & _
                                    " charindex( '|" & AgL.PubSiteCode & "|',Site) = 0 "
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)

                        ElseIf UTrim(XNull(.Rows(I)("AED"))) = "D" Then
                            mQry = "Delete From " & XNull(.Rows(I)("TableName")) & " " & _
                                   "Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " And " & _
                                    " charindex( '|" & AgL.PubSiteCode & "|',Site) = 0 "
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)
                        ElseIf UTrim(XNull(.Rows(I)("AED"))) = "A" Then
                            mQry = "Delete From " & XNull(.Rows(I)("TableName")) & " Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "' "
                            Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)
                            mColumnStr = GetColumnString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString)
                            mColumnsDataType = Split(GetColumnDataTypeString(XNull(.Rows(I)("TableName")), Gcn_ConnectionString), ",")
                            mQry = "Select * From " & XNull(.Rows(I)("TableName")) & " With (NoLock) Where " & XNull(.Rows(I)("UniqueField")) & " = '" & XNull(.Rows(I)("UniqueKey")) & "'"
                            DtRecord = AgL.FillData(mQry, GcnRead).Tables(0)

                            For K = 0 To DtRecord.Rows.Count - 1
                                mQry = "Insert Into " & XNull(.Rows(I)("TableName")) & " (" & mColumnStr & ") "
                                mQry = mQry & " Values ("
                                mColumns = Split(mColumnStr, ",")
                                For J = 0 To mColumns.Length - 1
                                    If mColumnsDataType(J) = "34" Then
                                        If IsDBNull(DtRecord.Rows(0)(mColumns(J))) Then
                                            mQry = mQry & " NULL " & IIf(J < mColumns.Length - 1, ",", "")
                                        Else
                                            mQry = mQry & " " & CreateImageFile(DtRecord.Rows(K)(mColumns(J))) & IIf(J < mColumns.Length - 1, ",", "")
                                        End If

                                    Else
                                        mQry = mQry & " " & Chk_Text(XNull(DtRecord.Rows(K)(mColumns(J)))) & IIf(J < mColumns.Length - 1, ",", "")
                                    End If
                                Next
                                mQry = mQry & ")"

                                Dman_ExecuteNonQry(mQry, GcnSite, ECmdSite)
                            Next K

                            If mTransactionTables Then UpdateVoucherCounter(XNull(.Rows(I)("SearchKey")), GcnSite, ECmdSite)

                            mQry = "Update Log_TableRecords Set Site = IsNull(Site,'') + '|" & AgL.PubSiteCode & "|' " & _
                                    " Where RowId = " & VNull(.Rows(I)("RowId")) & " And " & _
                                    " charindex( '|" & AgL.PubSiteCode & "|',Site) = 0 "
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)
                        End If




                    Catch ex As Exception
                        mQry = "Insert Into Synchronise_Error (RowId, Message) " & _
                               "Values ('" & VNull(.Rows(I)("RowId")) & "', " & AgL.Chk_Text(ex.Message) & ")"
                        Dman_ExecuteNonQry(mQry, mConn, mCmd)
                    End Try
                Next
                AllowTableLog(True, GcnSite, ECmdSite)
            End If
        End With

        'ETransSite.Commit()

        DtTemp.Dispose()
        GcnRead.Dispose()

    End Sub

#End Region

    Sub UpdateVoucherCounter(ByVal DocId As String, ByVal mConn As SqlClient.SqlConnection, Optional ByVal mCmd As SqlClient.SqlCommand = Nothing)
        Dim mDivCode As String
        Dim mSitecode As String
        Dim mVType As String
        Dim mVPrefix As String
        Dim mVNo As Integer
        Dim mQry As String

        If DocId.Length <> 21 Then Exit Sub
        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = mConn.CreateCommand
            mCmd.CommandTimeout = 1024
        End If


        mDivCode = DeCodeDocID(DocId, DocIdPart.Division)
        mSitecode = DeCodeDocID(DocId, DocIdPart.Site)
        mVType = DeCodeDocID(DocId, DocIdPart.VoucherType)
        mVPrefix = DeCodeDocID(DocId, DocIdPart.VoucherPrefix)
        mVNo = DeCodeDocID(DocId, DocIdPart.VoucherNo)

        mQry = "Update Voucher_Prefix Set Start_Srl_No = " & mVNo & "  Where V_Type = '" & mVType & "' And Prefix = '" & mVPrefix & "' And Site_Code = '" & mSitecode & "' And Div_Code = '" & mDivCode & "' And Start_Srl_No<" & mVNo & " "
        Dman_ExecuteNonQry(mQry, mConn, mCmd)
    End Sub

    Public Sub UpdateVoucherCounter(ByVal mDocId As String, ByVal mDate As Date, ByVal mConn As SqlConnection, _
                                    ByVal mCmd As SqlCommand, ByVal mDiv_Code As String, ByVal mSite_Code As String, _
                                    Optional ByVal mDelete_Record As Boolean = False, Optional ByVal mComp_Code As String = "")
        Dim RdTemp As SqlDataReader = Nothing
        Dim mVno As Long
        Dim mVType As String
        Dim mVPrefix As String
        Dim CondStr As String
        Dim mQry As String

        Try
            mVno = Val(DeCodeDocID(mDocId, DocIdPart.VoucherNo))
            mVType = DeCodeDocID(mDocId, DocIdPart.VoucherType)
            mVPrefix = DeCodeDocID(mDocId, DocIdPart.VoucherPrefix)

            ECmd.CommandText = "Select SiteWise,DivisionWise From Voucher_Type Where V_Type='" & mVType & "'"
            RdTemp = ECmd.ExecuteReader

            CondStr = " Where V_Type='" & Trim(mVType) & "' and Prefix='" & Trim(mVPrefix) & "' " & _
                      "And Date_From<=" & ConvertDate(mDate.ToString) & " And Date_To>=" & ConvertDate(mDate.ToString) & "  And Start_Srl_No " & IIf(mDelete_Record, "=", "<") & " " & mVno & " "

            If RdTemp.Read Then
                If Abs(VNull(RdTemp.Item("SiteWise"))) = 1 Then CondStr = CondStr & " And Site_Code='" & mSite_Code & "' "
                If Abs(VNull(RdTemp.Item("DivisionWise"))) = 1 Then CondStr = CondStr & " And Div_Code='" & mDiv_Code & "' "
            End If
            If RdTemp IsNot Nothing Then RdTemp.Close()

            If mComp_Code.Trim <> "" Then CondStr += " And IsNull(Comp_Code,'" & AglObj.PubCompCode & "') = '" & mComp_Code & "' "

            mQry = "Update Voucher_Prefix Set Start_Srl_No=" & IIf(mDelete_Record, mVno - 1, mVno) & CondStr
            Dman_ExecuteNonQry(mQry, mConn, mCmd)
        Catch Ex As Exception
            If RdTemp IsNot Nothing Then RdTemp.Close()
            MsgBox(Ex.Message)
        Finally
            If RdTemp IsNot Nothing Then RdTemp.Close()
        End Try
    End Sub

    Public Function LedgerPost(ByVal EMode As String, ByRef RecAry() As LedgRec, ByVal mConn As SqlClient.SqlConnection, _
                                ByVal mCmd As SqlClient.SqlCommand, ByVal DocID As String, ByVal Vdate As Date, _
                                ByVal mU_Name As String, ByVal mU_EntDt As String, _
                                Optional ByVal CommNarr As String = "", Optional ByVal mOpening_Balance As Boolean = False, _
                                Optional ByVal mConnectionString As String = "") As Boolean

        Dim GSQL As String, mQry As String
        Dim Rd As SqlDataReader = Nothing
        Dim I As Integer, mVSNo As Integer, mDR As Double = 0, mCR As Double = 0
        Dim mDivCode$, mSiteCode$, mVtype$, mVPrefix$, mVNo$
        Dim GcnRead As New SqlConnection
        Dim TblTemp As DataTable = Nothing

        Try
            If mConnectionString = "" Then
                GcnRead.ConnectionString = mConn.ConnectionString
            Else
                GcnRead.ConnectionString = mConnectionString
            End If

            GcnRead.Open()

            For I = 0 To UBound(RecAry)
                mDR = mDR + RecAry(I).AmtDr
                mCR = mCR + RecAry(I).AmtCr
            Next
            If EMode <> "D" And Round(mDR, 2) <> Round(mCR, 2) And mOpening_Balance = False Then Exit Function
            'If EMode <> "A" And mOpening_Balance = False Then
            If EMode <> "A" Then
                LedgerUnPost(mConn, mCmd, DocID)
            End If


            If EMode <> "D" Then
                mDivCode = DeCodeDocID(DocID, DocIdPart.Division)
                mSiteCode = DeCodeDocID(DocID, DocIdPart.Site) + DeCodeDocID(DocID, DocIdPart.ForSite)
                mVtype = DeCodeDocID(DocID, DocIdPart.VoucherType)
                mVPrefix = DeCodeDocID(DocID, DocIdPart.VoucherPrefix)
                mVNo = DeCodeDocID(DocID, DocIdPart.VoucherNo)
                mVSNo = 0

                mQry = "INSERT INTO LedgerM (DocId,V_Type,v_Prefix,V_No,Site_Code," & _
                                    " V_Date,Narration,U_Name,U_EntDt,U_AE) VALUES (" & _
                                    " " & Chk_Text(DocID) & "," & Chk_Text(mVtype) & "," & Chk_Text(mVPrefix) & "," & _
                                    " " & mVNo & "," & Chk_Text(mSiteCode) & "," & ConvertDate(Vdate.ToString) & "," & Chk_Text(CommNarr) & "," & _
                                    " " & Chk_Text(mU_Name) & "," & ConvertDate(mU_EntDt) & ",'" & EMode & "')"

                Dman_ExecuteNonQry(mQry, mConn, mCmd)


                For I = 0 To UBound(RecAry)
                    If RecAry(I).AmtCr + RecAry(I).AmtDr <> 0 Then
                        TblTemp = FillData("Select GroupCode, GroupNature From SubGroup With (NoLock) Where SubCode = '" & RecAry(I).SubCode & "'", GcnRead).Tables(0)


                        mVSNo = mVSNo + 1
                        GSQL = "insert into ledger(" _
                            & "DocId,DivCode, Site_Code,V_SNo,V_Type,V_Prefix,V_No," _
                            & "V_Date,SubCode,ContraSub, " _
                            & "AmtDr,AmtCr,Narration," _
                            & "Chq_No, Chq_Date, Clg_Date, GroupCode, GroupNature, U_Name, U_EntDt, U_AE)" _
                            & " values(" _
                            & "'" & DocID & "', '" & mDivCode & "','" & mSiteCode & "'," & mVSNo & ",'" & mVtype & "','" & mVPrefix & "'," & Val(mVNo) & _
                            "," & ConvertDate(Vdate.ToString) & ",'" & RecAry(I).SubCode & "', " & Chk_Text(RecAry(I).ContraSub) & _
                            ", " & RecAry(I).AmtDr & "," & RecAry(I).AmtCr & ",'" & RecAry(I).Narration & _
                            "', " & Chk_Text(RecAry(I).ChqNo) & ", " & ConvertDate(RecAry(I).ChqDt) & ", " & ConvertDate(RecAry(I).ClrChqDt) & _
                            ", " & Chk_Text(XNull(TblTemp.Rows(0)("GroupCode"))) & ", " & Chk_Text(XNull(TblTemp.Rows(0)("GroupNature"))) & ", " & Chk_Text(mU_Name) & "," & ConvertDate(mU_EntDt) & ",'" & EMode & "')"
                        Dman_ExecuteNonQry(GSQL, mConn, mCmd)


                        'mCmd = Dman_Execute("Select Acgroup.maingrcode from subgroup left join acgroup on subgroup.groupcode=acgroup.groupcode where acgroup.aliasyn='N'", EConnection)
                        'mMainGrCode = mCmd.ExecuteScalar()

                        'If RecAry(I).AmtCr + RecAry(I).AmtDr <> 0 Then
                        '    ECmd.CommandText = "SELECT Sum(Ledger.AmtCr)-Sum(Ledger.AmtDr) AS Balance FROM SubGroup LEFT JOIN Ledger ON SubGroup.SubCode = Ledger.SubCode where SubGroup.SubCode ='" & RecAry(I).SubCode & "'"
                        '    Rd = ECmd.ExecuteReader
                        '    If Rd.Read Then
                        '        'DsTemp = FillData("SELECT Sum(Ledger.AmtCr)-Sum(Ledger.AmtDr) AS Balance FROM SubGroup LEFT JOIN Ledger ON SubGroup.SubCode = Ledger.SubCode where SubGroup.SubCode ='" & RecAry(I).SubCode & "'", EConnection)
                        '        If VNull(Rd.Item("Balance")) <> 0 Then
                        '            Dman_ExecuteNonQry("Update SubGroup Set Curr_Bal=" & VNull(Rd.Item("Balance")) & " Where SubCode='" & RecAry(I).SubCode & "'", mConn, mCmd)
                        '        Else
                        '            Dman_ExecuteNonQry("Update SubGroup Set Curr_Bal=0 Where SubCode='" & RecAry(I).SubCode & "'", mConn, mCmd)
                        '        End If
                        '    End If
                        '    Rd.Close()
                        'End If
                    End If
                Next
                mQry = "SELECT NewID() AS GUID, L.DocId, L.V_SNo as Sr, L.DivCode AS Div_Code, " & _
                       "L.Site_Code, L.V_Type, L.V_Prefix, L.V_No, L.V_Date, '' AS ReferenceNo,  " & _
                       "L.ContraSub AS BankAc, L.SubCode AS PartyAc, L.Chq_No AS ChequeNo,  " & _
                       "L.Chq_Date AS ChequeDate, L.Chq_Date AS ClearingDate,  " & _
                       "NULL AS DishonourDate, L.AmtDr, L.AmtCr, L.Narration AS Remark     " & _
                       "FROM Ledger L  WITH (NoLock) " & _
                       "LEFT JOIN subgroup S  WITH (NoLock) ON L.ContraSub  = S.SubCode  " & _
                       "WHERE S.Nature ='Bank' AND L.DocID ='" & DocID & "' "

                TblTemp = FillData(mQry, GcnRead).Tables(0)
                With TblTemp
                    If TblTemp.Rows.Count > 0 Then
                        For I = 0 To TblTemp.Rows.Count - 1
                            mQry = "INSERT INTO dbo.FaChequeDatail (GUID, DocID, Sr, Div_Code, Site_Code, V_Type, V_Prefix, V_No, V_Date, ReferenceNo, BankAc, PartyAc, BankName, ChequeNo, ChequeDate, ClearingDate, DishonourDate, AmtDr, AmtCr, Remark) " & _
                                   "VALUES ('" & .Rows(I)("GUID").ToString & "', " & Chk_Text(.Rows(I)("docid")) & ", " & Chk_Text(.Rows(I)("Sr")) & ", " & Chk_Text(.Rows(I)("Div_Code")) & ", " & Chk_Text(.Rows(I)("Site_Code")) & ", " & Chk_Text(.Rows(I)("V_Type")) & ", " & Chk_Text(.Rows(I)("V_Prefix")) & ", " & Chk_Text(.Rows(I)("V_No")) & ", " & Chk_Text(.Rows(I)("V_date")) & ", " & Chk_Text(.Rows(I)("ReferenceNo")) & ", " & Chk_Text(.Rows(I)("BankAc")) & ", " & Chk_Text(.Rows(I)("PartyAc")) & ", " & Chk_Text("") & ", " & Chk_Text(.Rows(I)("ChequeNo")) & ", " & Chk_Text(.Rows(I)("ChequeDate")) & ", " & Chk_Text(.Rows(I)("ClearingDate")) & ", " & Chk_Text(.Rows(I)("DishonourDate")) & ", " & Chk_Text(.Rows(I)("AmtDr")) & ", " & Chk_Text(.Rows(I)("AmtCr")) & ", " & Chk_Text(.Rows(I)("Remark")) & ") "
                            Dman_ExecuteNonQry(mQry, mConn, mCmd)

                        Next
                    End If
                End With

                LedgerPost = True
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            If Rd IsNot Nothing Then Rd.Close()
            GcnRead.Dispose()
            If TblTemp IsNot Nothing Then TblTemp.Dispose()
        End Try
    End Function

    Public Sub LedgerUnPost(ByRef mConn As SqlConnection, ByRef mCmd As SqlCommand, ByRef DocID As String)
        Dim DsLedger As DataSet = Nothing
        'Dim DsTemp As DataSet
        'Dim I As Integer

        'DsLedger = FillData("Select ledger.*,SubGroup.GroupCode,AcGroup.MainGrCode from (Ledger left join SubGroup on ledger.SubCode=SubGroup.SubCode) left join AcGroup on SubGroup.GroupCode=AcGroup.GroupCode where docid='" & DocID & "'  and AcGroup.AliasYN='N'", EConnection)
        Dman_ExecuteNonQry("Delete from Ledger where DocId='" & DocID & "'", mConn, mCmd)
        Dman_ExecuteNonQry("Delete from FaChequeDatail where DocId='" & DocID & "'", mConn, mCmd)
        Dman_ExecuteNonQry("Delete from LedgerM where DocId='" & DocID & "'", mConn, mCmd)


        'If DsLedger.Tables(0).Rows.Count > 0 Then
        '    For I = 0 To DsLedger.Tables(0).Rows.Count - 1
        '        DsTemp = FillData("SELECT Sum(Ledger.AmtCr)-Sum(Ledger.AmtDr) AS Balance FROM SubGroup LEFT JOIN Ledger ON SubGroup.SubCode = Ledger.SubCode where SubGroup.SubCode ='" & DsLedger.Tables(0).Rows(I)("SubCode") & "'", EConnection)
        '        If VNull(DsTemp.Tables(0).Rows(0)("Balance")) <> 0 Then
        '            Dman_ExecuteNonQry("Update SubGroup Set Curr_Bal=" & DsTemp.Tables(0).Rows(0)("Balance") & " Where SubCode='" & DsLedger.Tables(0).Rows(I)("SubCode") & "'", mConn, mCmd)
        '        Else
        '            Dman_ExecuteNonQry("Update SubGroup Set Curr_Bal=0 Where SubCode='" & DsLedger.Tables(0).Rows(I)("SubCode") & "'", mConn, mCmd)
        '        End If
        '    Next I
        'End If

        ''Dman_ExecuteNonQry("Delete from LedgerAdj where DocId='" & DocID & "'", mConn, mCmd)
    End Sub

    Public Sub LogTableEntry(ByVal DocId As String, ByVal EntryPoint As String, ByVal EntryMode As String, _
                                ByVal MachineName As String, ByVal U_Name As String, ByVal U_EntDt As String, _
                                ByVal mConn As SqlConnection, Optional ByVal mCmd As SqlCommand = Nothing, _
                                Optional ByVal mRemark As String = "", Optional ByVal V_Date As String = "", _
                                Optional ByVal SubCode As String = "", Optional ByVal PartyDetail As String = "", _
                                Optional ByVal Amount As Double = 0, Optional ByVal Site_code As String = "", Optional ByVal Div_Code As String = "")
        Dim mU_EntDt As String = "", mQry As String = ""

        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = mConn.CreateCommand
        End If

        mCmd = Dman_Execute("Select GetDate() As SrvDate ", mConn, mCmd)
        mU_EntDt = XNull(mCmd.ExecuteScalar)
        mU_EntDt = U_EntDt + MidStr(mU_EntDt, 11, mU_EntDt.Length - 11)

        mQry = "Insert Into LogTable(DocId, EntryPoint, MachineName, U_Name, U_EntDt, U_AE, Remark, V_Date, SubCode, PartyDetail, Amount, Site_Code, Div_Code) " & _
            " Values('" & DocId & "','" & EntryPoint & "', '" & MachineName & "' ,'" & U_Name & "'," & _
            " '" & mU_EntDt & "','" & EntryMode & "', " & Chk_Text(mRemark) & ", " & ConvertDate(V_Date) & ", " & Chk_Text(SubCode) & ", " & Chk_Text(PartyDetail) & ", " & Val(Amount) & ", " & Chk_Text(Site_code) & ", " & Chk_Text(Div_Code) & ")"
        Dman_ExecuteNonQry(mQry, mConn, mCmd)
    End Sub

    Public Sub LogTableRecordsEntry(ByVal SearchKey As String, ByVal AED As String, ByVal UpdateDate As String, _
                                ByVal EntryPoint As String, ByVal TableName As String, ByVal MasterTransaction As Boolean, _
                                ByVal LineRecords As Boolean, ByVal Site As String, ByVal UploadDate As String, _
                                ByVal mConn As SqlConnection, Optional ByVal mCmd As SqlCommand = Nothing)
        Dim mQry As String = ""

        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = mConn.CreateCommand
        End If

        mCmd = Dman_Execute("Select GetDate() As SrvDate ", mConn, mCmd)

        mQry = "INSERT INTO Log_TableRecords (SearchKey,AED,UpdateDate,EntryPoint,TableName,TransactionRecord, LineRecords,Site,UploadDate) " & _
               "VALUES 	(" & Chk_Text(SearchKey) & "," & Chk_Text(AED) & "," & ConvertDate(UpdateDate) & "," & Chk_Text(EntryPoint) & "," & Chk_Text(TableName) & "," & IIf(MasterTransaction = True, 1, 0) & "," & IIf(LineRecords = True, 1, 0) & "," & Chk_Text(Site) & "," & ConvertDate(UploadDate) & ")"
        Dman_ExecuteNonQry(mQry, mConn, mCmd)
    End Sub

    Public Function GetDateTime(ByVal mConn As SqlConnection, Optional ByVal mCmd As SqlCommand = Nothing) As String
        Dim mU_EntDt As String = ""
        Try
            If mCmd Is Nothing Then
                mCmd = New SqlCommand
                mCmd = mConn.CreateCommand
            End If

            mCmd = Dman_Execute("Select GetDate() As SrvDate ", mConn, mCmd)
            mU_EntDt = XNull(mCmd.ExecuteScalar)
        Catch ex As Exception
            mU_EntDt = ""
            MsgBox(ex.Message)
        Finally
            GetDateTime = mU_EntDt
        End Try
    End Function

    Public Function DeCodeDocID(ByVal DocID As String, ByVal Part As DocIdPart) As String
        If DocID.Length <> 21 Then
            DeCodeDocID = ""
        Else
            Select Case Part
                Case DocIdPart.Division
                    DeCodeDocID = MidStr(DocID, 0, 1).Trim
                Case DocIdPart.Site
                    DeCodeDocID = MidStr(DocID, 1, 2).Trim
                Case DocIdPart.ForSite
                    DeCodeDocID = ""
                Case DocIdPart.VoucherType
                    DeCodeDocID = MidStr(DocID, 3, 5).Trim
                Case DocIdPart.VoucherPrefix
                    DeCodeDocID = MidStr(DocID, 8, 5).Trim
                Case DocIdPart.VoucherNo
                    DeCodeDocID = Val(MidStr(DocID, 13, 8).Trim)
                Case Else
                    DeCodeDocID = ""
            End Select
        End If
    End Function

    Public Function V_No_Field(ByVal DocID As String) As String
        If DocID.Trim = "" Then
            V_No_Field = "' '"
        Else
            V_No_Field = "Convert(nVarChar, Convert(Numeric, Right(" & DocID & ", 8))) + '/' + RTrim(LTrim(SubString(" & DocID & ", 9, 5))) + '/' + RTrim(LTrim(SubString(" & DocID & ", 4, 5))) + '/' + RTrim(LTrim(SubString(" & DocID & ", 2, 2))) + '/' + Left(" & DocID & ", 1) "
            'V_No_Field = **************** < VOUCHER NUMBER >  *********************** + '/' + *************** < VOUCHER PREFIX > ********* + '/' + **************** < VOUCHER TYPE > ********** + '/' + **************** < SITE CODE > ************* + '/' + *** < DIVISION CODE > ***
        End If
    End Function

    Public Function ConvertDocId(ByVal DocID As String) As String
        If DocID.Trim = "" Or DocID.Trim.Length < 21 Then
            ConvertDocId = ""
        Else
            ConvertDocId = DeCodeDocID(DocID, DocIdPart.VoucherNo) + "/" + DeCodeDocID(DocID, DocIdPart.VoucherPrefix) + "/" + DeCodeDocID(DocID, DocIdPart.VoucherType) + "/" + DeCodeDocID(DocID, DocIdPart.Site) + DeCodeDocID(DocID, DocIdPart.ForSite) + "/" + DeCodeDocID(DocID, DocIdPart.Division)
            'ConvertDocId = ******* < VOUCHER NUMBER >  ********** + '/' + ************** < VOUCHER PREFIX > ********* + '/' + ************* < VOUCHER TYPE > ********** + '/' + ********** < SITE CODE > ********* + ******** < FOR_SITE CODE > ********** + '/' + ********* < DIVISION CODE > **********
        End If
    End Function


#Region "API Calls"
    ' standard API declarations for INI access
    ' changing only "As Long" to "As Int32" (As Integer would work also)
    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
    Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String, _
    ByVal lpKeyName As String, ByVal lpString As String, _
    ByVal lpFileName As String) As Int32

    Private Declare Unicode Function GetPrivateProfileString Lib "kernel32" _
    Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String, _
    ByVal lpKeyName As String, ByVal lpDefault As String, _
    ByVal lpReturnedString As String, ByVal nSize As Int32, _
    ByVal lpFileName As String) As Int32
#End Region

    Public Overloads Function INIRead(ByVal INIPath As String, _
    ByVal SectionName As String, ByVal KeyName As String, _
    ByVal DefaultValue As String) As String
        ' primary version of call gets single value given all parameters
        Dim n As Int32
        Dim sData As String
        sData = Space$(1024) ' allocate some room
        n = GetPrivateProfileString(SectionName, KeyName, DefaultValue, _
        sData, sData.Length, INIPath)
        If n > 0 Then ' return whatever it gave us
            INIRead = sData.Substring(0, n)
        Else
            INIRead = ""
        End If
    End Function

    Public Function ConvertDate(ByVal temp As Object) As String
        On Error GoTo errorbox
        ConvertDate = ""
        If IsDBNull(temp) Or temp Is Nothing Then
            ConvertDate = "Null"
        ElseIf IsDate(temp.text) = False Then
            If temp.text = "" Then ConvertDate = "Null"
        Else
            ConvertDate = "'" & Format(CDate(temp.text), "Short Date") & "'"
        End If

errorbox: If Err.Number > 0 Then MsgBox(Err.Description, vbCritical)
    End Function

    Public Function ConvertDate(ByVal temp As String) As String
        On Error GoTo errorbox
        ConvertDate = "Null"
        If temp = "" Or temp Is Nothing Then
            ConvertDate = "Null"
        ElseIf IsDate(temp) = False Then
            If temp = "" Then ConvertDate = "Null"
        Else
            ConvertDate = "'" & Format(CDate(temp), "Short Date") & "'"
        End If

errorbox: If Err.Number > 0 Then MsgBox(Err.Description, vbCritical)
    End Function

    Public Function ConvertDate(ByVal mDate As Date) As String
        On Error GoTo errorbox
        Dim Temp As String
        Temp = mDate.ToString
        ConvertDate = "Null"
        If Temp = "" Or Temp Is Nothing Then
            ConvertDate = "Null"
        ElseIf IsDate(Temp) = False Then
            If Temp = "" Then ConvertDate = "Null"
        Else
            ConvertDate = "'" & Format(CDate(Temp), "Short Date") & "'"
        End If

errorbox: If Err.Number > 0 Then MsgBox(Err.Description, vbCritical)
    End Function

    Public Function ConvertDateField(ByVal FieldName As String) As String
        On Error GoTo errorbox
        ConvertDateField = "Null"
        If FieldName = "" Or FieldName Is Nothing Then
            ConvertDateField = "Null"
        Else
            ConvertDateField = "Replace(Convert(VARCHAR," & FieldName & ",106),' ','/')"
        End If

errorbox: If Err.Number > 0 Then MsgBox(Err.Description, vbCritical)
    End Function

    Public Function ConvertTimeField(ByVal FieldName As String, _
                                Optional ByVal Seconds_Requird As Boolean = False) As String
        On Error GoTo errorbox
        ConvertTimeField = "Null"
        If FieldName = "" Or FieldName Is Nothing Then
            ConvertTimeField = "Null"
        Else
            If Seconds_Requird Then
                ConvertTimeField = "Convert(VARCHAR," & FieldName & ",8)"
            Else
                ConvertTimeField = "SubString(Convert(VARCHAR," & FieldName & ",8),0,6)"
            End If

        End If

errorbox: If Err.Number > 0 Then MsgBox(Err.Description, vbCritical)
    End Function

    Public Function ConvertDateTimeField(ByVal FieldName As String, _
                                    Optional ByVal Seconds_Requird As Boolean = False) As String
        On Error GoTo errorbox
        ConvertDateTimeField = "Null"
        If FieldName = "" Or FieldName Is Nothing Then
            ConvertDateTimeField = "Null"
        Else
            If Seconds_Requird Then
                ConvertDateTimeField = "Replace(Convert(VARCHAR," & FieldName & ",106),' ','/')" & "+ Space(1) +" & "Convert(VARCHAR," & FieldName & ",8)"
            Else
                ConvertDateTimeField = "Replace(Convert(VARCHAR," & FieldName & ",106),' ','/')" & "+ Space(1) +" & "SubString(Convert(VARCHAR," & FieldName & ",8),0,6)"
            End If

        End If

errorbox: If Err.Number > 0 Then MsgBox(Err.Description, vbCritical)
    End Function

    Public Function ConvertMonthYearField(ByVal FieldName As String) As String
        On Error GoTo errorbox
        ConvertMonthYearField = "Null"
        If FieldName = "" Or FieldName Is Nothing Then
            ConvertMonthYearField = "Null"
        Else
            ConvertMonthYearField = "Left(Convert(VARCHAR," & FieldName & ",107),3)" & "+ '/' +" & "Right(Convert(VARCHAR," & FieldName & ",107),4)"
        End If

errorbox: If Err.Number > 0 Then MsgBox(Err.Description, vbCritical)
    End Function

    Public Function ConvertMonthStartDateField(ByVal FieldName As String) As String
        On Error GoTo errorbox
        ConvertMonthStartDateField = "Null"
        If FieldName = "" Or FieldName Is Nothing Then
            ConvertMonthStartDateField = "Null"
        Else
            ConvertMonthStartDateField = "Convert(SmallDateTime,'01/'+Left(Convert(VARCHAR," & FieldName & ",107),3)" & "+ '/' +" & "Right(Convert(VARCHAR," & FieldName & ",107),4))"
        End If

errorbox: If Err.Number > 0 Then MsgBox(Err.Description, vbCritical)
    End Function

    Public Function ConvertMonthEndDateField(ByVal FieldName As String) As String
        On Error GoTo errorbox
        ConvertMonthEndDateField = "Null"
        If FieldName = "" Or FieldName Is Nothing Then
            ConvertMonthEndDateField = "Null"
        Else
            ConvertMonthEndDateField = "DateAdd(d, -1  , DateAdd(m, 1, " & ConvertMonthStartDateField(FieldName) & "))"
        End If

errorbox: If Err.Number > 0 Then MsgBox(Err.Description, vbCritical)
    End Function

    Public Function Chk_Quot(ByVal temp As Object) As Object
        Chk_Quot = IIf(InStr(temp, "'") > 0, Replace(temp, "'", "`"), temp)
    End Function

    Public Sub CheckQuote(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        Select Case e.KeyChar
            Case "'"
                e.KeyChar = "`"

            Case "%"
                e.KeyChar = ""
        End Select
        'If Asc(e.KeyChar) = 39 Or e.KeyChar = "%" Then e.KeyChar = ""
    End Sub

    Public Function RetDate(ByRef Txt As String) As String
        On Error GoTo err1
        If Txt = "" Then RetDate = "" : Exit Function
        If Txt.Length >= 11 Then
            If Txt.IndexOf("/") <> -1 Then
                If Txt.LastIndexOf("/") <> -1 Then
                    If Txt.IndexOf("/") <> Txt.LastIndexOf("/") Then
                        RetDate = Format(CDate(Txt), "dd/MMM/yyyy") : Exit Function
                    End If
                End If
            End If
        End If

        Dim mDay As Long, mMonth As String, mYear As String, Txt1 As String, Test As Long
        mDay = 0 : mMonth = "" : mYear = 0
        Txt1 = Trim(Txt)
        '''' FOR DAY
        Test = InStr(1, Txt1, "/")
        If Test = 0 Then Test = InStr(1, Txt1, "-")
        If Test = 0 Then Test = InStr(1, Txt1, ".")
        If Test <> 0 Then
            If IsNumeric(Mid(Txt1, 1, Test - 1)) Then
                mDay = Val(Mid(Txt1, 1, Test - 1))
            Else
                mMonth = Mid(Txt1, 1, Test - 1)
            End If
        End If
        If Test = 0 Then
            If IsNumeric(Txt1) Then
                mDay = Val(Txt1)
            Else
                mMonth = Txt1
            End If
            GoTo EXITFLAG
        End If
        ''''' FOR MONTH
        If mMonth = "" Then
            Txt1 = Mid(Txt1, Test + 1)
            Test = InStr(1, Txt1, "/")
            If Test = 0 Then Test = InStr(1, Txt1, "-")
            If Test = 0 Then Test = InStr(1, Txt1, ".")
            If Test <> 0 Then mMonth = Mid(Txt1, 1, Test - 1)
            If Test = 0 Then
                mMonth = Txt1
                GoTo EXITFLAG
            End If
        End If
        ''''FOR YEAR
        mYear = Mid(Txt1, Test + 1)
EXITFLAG:
        If Val(mYear) = 0 Then mYear = Date.Today.Year
        If mYear > 1999 Then mYear = Microsoft.VisualBasic.Right(Str(mYear), 2)
        mYear = Val(Mid(CStr(Date.Today.Year), 1, 4 - Len(Trim(CStr(mYear)))) + Trim(CStr(mYear)))

        If mDay < 0 Then mDay = 0
        mMonth = Mid(mMonth, 1, 3)
        Select Case Trim(UCase(mMonth))
            Case "1", "01", "J", "JA", "JAN"
                mMonth = "Jan"
            Case "2", "02", "F", "FE", "FEB"
                mMonth = "Feb"
            Case "3", "03", "M", "MA", "MAR"
                mMonth = "Mar"
            Case "4", "04", "A", "AP", "APR"
                mMonth = "Apr"
            Case "5", "05", "MAY"
                mMonth = "May"
            Case "6", "06", "JU", "JUN"
                mMonth = "Jun"
            Case "7", "07", "JUL"
                mMonth = "Jul"
            Case "8", "08", "AU", "AUG"
                mMonth = "Aug"
            Case "9", "09", "S", "SE", "SEP"
                mMonth = "Sep"
            Case "10", "O", "OC", "OCT"
                mMonth = "Oct"
            Case "11", "N", "NO", "NOV"
                mMonth = "Nov"
            Case "12", "D", "DE", "DEC"
                mMonth = "Dec"
            Case Else
                mMonth = Format(Date.Today, "MMM")
        End Select
        Select Case Trim(UCase(mMonth))
            Case "1", "01", "J", "JA", "JAN"
                If mDay > 31 Then mDay = 0
            Case "2", "02", "F", "FE", "FEB"
                If mDay > IIf(mYear Mod 4 = 0, 29, 28) Then mDay = 0
            Case "3", "03", "M", "MA", "MAR"
                If mDay > 31 Then mDay = 0
            Case "4", "04", "A", "AP", "APR"
                If mDay > 30 Then mDay = 0
            Case "5", "05", "MAY"
                If mDay > 31 Then mDay = 0
            Case "6", "06", "JU", "JUN"
                If mDay > 30 Then mDay = 0
            Case "7", "07", "JUL"
                If mDay > 31 Then mDay = 0
            Case "8", "08", "AU", "AUG"
                If mDay > 31 Then mDay = 0
            Case "9", "09", "S", "SE", "SEP"
                If mDay > 30 Then mDay = 0
            Case "10", "O", "OC", "OCT"
                If mDay > 31 Then mDay = 0
            Case "11", "N", "NO", "NOV"
                If mDay > 30 Then mDay = 0
            Case "12", "D", "DE", "DEC"
                If mDay > 31 Then mDay = 0
            Case Else
                mDay = 0
        End Select
        If mDay = 0 Then mDay = Today.Day
        RetDate = Format(mDay, "00") + "/" + Trim(mMonth) + "/" + Trim(Str(mYear))
        Exit Function
err1:
        ' For Overflow Check
        If Err.Number = 6 Then Resume Next
    End Function

    Public Function DCODIFY(ByVal Txt As String) As String
        Dim XXX As String, xx As Integer, MyVal As Integer
        DCODIFY = ""
        If Txt <> "" Then
            MyVal = Asc(Left(Txt, 1)) - 27
            XXX = ""
            For xx = 1 To Len(Txt) - 1
                XXX = XXX + Chr(Asc(Mid(Txt, xx + 1, 1)) - 27 - MyVal)
            Next
            DCODIFY = XXX
        End If
    End Function

    Public Function CODIFY(ByVal TEXT As String) As String
        Dim XXX As String, xx As Integer, MyVal As Integer
        Randomize()
        MyVal = Int((99 * Rnd()) + 1)
        XXX = Chr(MyVal + 27)
        For xx = 1 To Len(TEXT)
            XXX = XXX + Chr(Asc(Mid(TEXT, xx, 1)) + 27 + MyVal)
        Next
        CODIFY = XXX
    End Function

    Public Function XNull(ByVal temp As Object) As Object
        If temp Is Nothing Then temp = ""
        XNull = CStr(IIf(IsDBNull(temp), "", temp))
    End Function

    Public Function VNull(ByRef temp As Object) As Object
        If temp Is Nothing Then temp = 0
        VNull = Val(IIf(IsDBNull(temp), 0, temp))
    End Function


    Public Sub AddButtonColumn(ByVal Dg1 As DataGridView, ByVal columnName As String, ByVal ColWidth As Integer, Optional ByVal columnHeaderTxt As String = "", Optional ByVal DefaultDropDown As Boolean = True)
        Dim BtnCol As DataGridViewButtonColumn

        BtnCol = New DataGridViewButtonColumn
        BtnCol.Name = columnName
        BtnCol.HeaderText = IIf(columnHeaderTxt = "", columnName, columnHeaderTxt)
        BtnCol.Width = ColWidth
        BtnCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        BtnCol.DefaultCellStyle.BackColor = Color.WhiteSmoke
        BtnCol.DefaultCellStyle.ForeColor = Color.BlueViolet

        If DefaultDropDown Then
            BtnCol.DefaultCellStyle.Font = New Font("Webdings", 9, FontStyle.Regular)
            BtnCol.Text = "6"
        End If

        BtnCol.UseColumnTextForButtonValue = True
        BtnCol.FlatStyle = FlatStyle.Popup
        Dg1.Columns.Add(BtnCol)
        Dg1.Columns(Dg1.Columns.Count - 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Public Sub AddDataComboColumn(ByVal mConn As SqlConnection, ByVal Dg1 As DataGridView, ByVal QryStr As String, ByVal columnName As String, ByVal ColWidth As Integer, Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False)
        Dim column As DataGridViewColumn
        column = New DataGridViewComboBoxColumn()
        '' Populate the drop-down list with the enumeration values.
        CType(column, DataGridViewComboBoxColumn).Name = columnName
        CType(column, DataGridViewComboBoxColumn).Width = ColWidth
        CType(column, DataGridViewComboBoxColumn).HeaderText = IIf(columnHeaderTxt = "", columnName, columnHeaderTxt)
        CType(column, DataGridViewComboBoxColumn).DisplayStyleForCurrentCellOnly = True
        IniGridHelp(mConn, column, QryStr)
        column.ReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
    End Sub

    Public Sub IniGridHelp(ByVal mConn As SqlConnection, ByVal column As DataGridViewColumn, ByVal QryStr As String)
        Dim DS As New DataTable
        DS.Clear()
        EAdptr = New SqlClient.SqlDataAdapter(QryStr, mConn)
        EAdptr.Fill(DS)
        CType(column, DataGridViewComboBoxColumn).DataSource = DS
        CType(column, DataGridViewComboBoxColumn).DisplayMember = "name"
        CType(column, DataGridViewComboBoxColumn).ValueMember = "code"
    End Sub

    Public Sub AddTextColumn(ByVal Dg1 As DataGridView, ByVal columnName As String, ByVal ColWidth As Integer, ByVal mMaxInputLength As Integer, Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal isRightAlign As Boolean = False)
        Dim column As DataGridViewColumn
        column = New DataGridViewTextBoxColumn()
        '' Populate the drop-down list with the enumeration values.
        CType(column, DataGridViewTextBoxColumn).Name = columnName
        CType(column, DataGridViewTextBoxColumn).HeaderText = IIf(columnHeaderTxt = "", columnName, columnHeaderTxt)
        CType(column, DataGridViewTextBoxColumn).MaxInputLength = mMaxInputLength
        CType(column, DataGridViewTextBoxColumn).Width = ColWidth
        If isRightAlign = True Then
            CType(column, DataGridViewTextBoxColumn).CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        column.SortMode = DataGridViewColumnSortMode.NotSortable
        column.ReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
    End Sub

    Public Sub AddCheckBox(ByVal Dg1 As DataGridView, ByVal columnName As String, ByVal ColWidth As Integer, Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal isRightAlign As Boolean = False)
        Dim column As DataGridViewColumn
        column = New DataGridViewCheckBoxColumn()

        '' Populate the drop-down list with the enumeration values.
        CType(column, DataGridViewCheckBoxColumn).Name = columnName
        CType(column, DataGridViewCheckBoxColumn).HeaderText = IIf(columnHeaderTxt = "", columnName, columnHeaderTxt)
        CType(column, DataGridViewCheckBoxColumn).Width = ColWidth
        'If isRightAlign = True Then
        '    CType(column, DataGridViewCheckBoxColumn).CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        'End If

        column.ReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
    End Sub

    Public Function Dman_ExecuteReader(ByVal mQRY As String, ByVal mConn As SqlConnection) As SqlDataReader
        Dim myCmd As SqlCommand

        myCmd = mConn.CreateCommand
        myCmd.CommandText = mQRY

        Dman_ExecuteReader = myCmd.ExecuteReader()
        myCmd.Dispose()
    End Function

    Public Function Dman_Execute(ByVal mQRY As String, ByVal mConn As SqlConnection, Optional ByVal mCmd As SqlCommand = Nothing) As SqlCommand
        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = mConn.CreateCommand
            mCmd.CommandTimeout = 1024
        End If
        mCmd.CommandText = mQRY

        Dman_Execute = mCmd
        mCmd.Dispose()
    End Function

    Public Sub Dman_ExecuteNonQry(ByVal mQRY As String, ByRef mConn As SqlConnection, Optional ByRef mCmd As SqlCommand = Nothing)
        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = mConn.CreateCommand
            mCmd.CommandTimeout = 1024
        End If

        mCmd.CommandText = mQRY
        mCmd.ExecuteNonQuery()
        mCmd.Dispose()
    End Sub

    Public Sub Dman_ExecuteNonQry(ByVal mQRY As String, ByRef mConn As OleDb.OleDbConnection, Optional ByRef mCmd As OleDb.OleDbCommand = Nothing)
        If mCmd Is Nothing Then
            mCmd = New OleDb.OleDbCommand
            mCmd = mConn.CreateCommand
            mCmd.CommandTimeout = 1024
        End If

        mCmd.CommandText = mQRY
        mCmd.ExecuteNonQuery()
        mCmd.Dispose()
    End Sub

    Public Function FillData(ByVal mQRY As String, ByRef mConn As SqlConnection)
        Dim DaTemp As SqlDataAdapter
        Dim DsTemp As Object

        DaTemp = New SqlDataAdapter(mQRY, mConn)
        DsTemp = New DataSet
        DaTemp.Fill(DsTemp)
        FillData = DsTemp

        DaTemp.Dispose()
        DsTemp = Nothing
    End Function

    Public Function FillData(ByVal mQRY As String, ByRef mConn As OleDb.OleDbConnection)
        Dim DaTemp As OleDb.OleDbDataAdapter
        Dim DsTemp As Object

        DaTemp = New OleDb.OleDbDataAdapter(mQRY, mConn)
        DsTemp = New DataSet
        DaTemp.Fill(DsTemp)
        FillData = DsTemp

        DaTemp.Dispose()
        DsTemp = Nothing
    End Function

    Public Sub CreateRecordInfo(ByVal LblRecordInfo As Object, ByVal U_EntDt As String, ByVal PreparedBy As String, ByVal U_AE As Char, _
                                   Optional ByVal LblEditInfo As Object = Nothing, Optional ByVal ModifiedBy As String = "", _
                                   Optional ByVal Edit_Date As String = "")
        If StrCmp(U_AE, "A") Then
            LblRecordInfo.text = "Added By User " & PreparedBy & " on " & U_EntDt
        Else
            LblRecordInfo.text = "Modified By User " & PreparedBy & " on " & U_EntDt
        End If

        If Edit_Date.Trim <> "" And Edit_Date.Trim <> "" And LblEditInfo IsNot Nothing Then
            LblEditInfo.text = "Modified By User " & ModifiedBy & " on " & Edit_Date
        Else
            If LblEditInfo IsNot Nothing Then LblEditInfo.text = ""
        End If

    End Sub

    Public Function UTrim(ByVal mStr As String) As String
        UTrim = UCase(Trim(mStr))
    End Function

    Public Function StrCmp(ByVal Str1 As String, ByVal Str2 As String) As Boolean
        If UCase(Trim(Str1)) = UCase(Trim(Str2)) Then
            StrCmp = True
        End If
    End Function

    Public Function RequiredField(ByVal sender As Object, Optional ByVal DispText As String = "It", Optional ByVal NumField As Boolean = False) As Boolean
        If NumField Then
            If Val(sender.text) = 0 Then
                MsgBox(DispText & " is a required field")
                sender.focus()
                RequiredField = True
            End If
        Else
            If sender.text = "" Then
                MsgBox(DispText & " is a required field")
                sender.focus()
                RequiredField = True
            End If
        End If
    End Function

    Public Function IsValid_EMailId(ByVal sender As Object, Optional ByVal DispText As String = "It") As Boolean
        If sender.text <> "" Then
            If InStr(sender.text, "@") > 1 And InStr(sender.text, "@") < sender.text.ToString.Length And InStr(sender.text, ".") > 1 And InStr(sender.text, ".") < sender.text.ToString.Length Then
                IsValid_EMailId = True
            Else
                IsValid_EMailId = False
                MsgBox(DispText & " is not valid Email Id.")
                sender.focus()
            End If
        Else
            IsValid_EMailId = True
        End If
    End Function

    Public Function CondStrFinancialYear(ByVal FieldName As String, ByVal PubStartDate As String, _
                                        ByVal PubEndDate As String) As String
        CondStrFinancialYear = ""

        If FieldName.Trim <> "" Then
            CondStrFinancialYear = " And " & FieldName & " Between " & ConvertDate(PubStartDate) & " And " & ConvertDate(PubEndDate) & " "
        End If
    End Function

    Public Function IsValidDate(ByVal sender As Object, ByVal PubStartDate As String, _
                                    ByVal PubEndDate As String, Optional ByVal DispText As String = "Voucher Date") As Boolean
        If sender.text.ToString.Trim <> "" Then
            IsValidDate = True
            If CDate(PubStartDate) > CDate(sender.text) Then
                MsgBox(DispText + " is Before Financial Year ", vbCritical)
                IsValidDate = False
            ElseIf CDate(PubEndDate) < CDate(sender.text) Then
                MsgBox(DispText + " is After Financial Year ", vbCritical)
                IsValidDate = False
            End If
        Else
            MsgBox(DispText + " is Blank", vbCritical)
            IsValidDate = False
        End If

        If IsValidDate = False Then
            sender.focus()
        End If
    End Function

    Public Function PubSiteConditionCommonAc(ByVal PubIsHo As Boolean, ByVal mSite_Code_Field As String, ByVal mSite_Code As String, ByVal mCommonAcField As String) As String
        PubSiteConditionCommonAc = "(" & IIf(PubIsHo, " 1=1 ", "" & mSite_Code_Field & "='" & mSite_Code & "'") & " Or IsNull(" & mCommonAcField & ",0) <> 0 ) "
    End Function

    Public Function GetMaxId(ByVal mTableName As String, ByVal mPrimaryField As String, ByVal mConn As System.Data.SqlClient.SqlConnection, _
                                ByVal PubDivCode As String, ByVal PubSiteCode As String, Optional ByVal mPad_Len As Integer = 0, _
                                Optional ByVal IsSiteWise As Boolean = False, Optional ByVal IsDivisionWise As Boolean = False, _
                                Optional ByVal mCmd As SqlCommand = Nothing, Optional ByVal mConnectionString As String = "")
        Dim CondStr As String = ""
        Dim GcnRead As New SqlClient.SqlConnection

        If mConnectionString <> "" Then
            GcnRead.ConnectionString = mConnectionString
        Else
            GcnRead.ConnectionString = mConn.ConnectionString
        End If
        GcnRead.Open()

        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = GcnRead.CreateCommand
        End If

        If PubSiteCode.Trim.Length = 1 Then
            If IsDivisionWise Then CondStr = " And Left(" & mPrimaryField & ",1) = '" & PubDivCode & "' "
            If IsSiteWise Then CondStr = " And SubString(" & mPrimaryField & ",2,1) = '" & PubSiteCode & "' "
        ElseIf PubSiteCode.Trim.Length = 2 Then
            PubDivCode = ""
            If IsSiteWise Then CondStr = " And SubString(" & mPrimaryField & ",1,2) = '" & PubSiteCode & "' "
        End If

        mCmd = Dman_Execute("Select IsNull(Max(Convert(BigInt, SubString(" & mPrimaryField & ",3,Len(" & mPrimaryField & ")))) ,0)+1 As MaxId From " & mTableName & " With (NoLock)  " & _
                            " Where 1=1 " & CondStr & " ", GcnRead)

        GetMaxId = PubDivCode & PubSiteCode & mCmd.ExecuteScalar().ToString.PadLeft(mPad_Len, "0")

        If mConnectionString <> "" Then
            GcnRead.Dispose()
        End If
    End Function

    Public Function GetGUID(ByVal mConn As System.Data.SqlClient.SqlConnection, Optional ByVal mCmd As SqlCommand = Nothing, Optional ByVal mConnectionString As String = "")
        Dim GcnRead As New SqlClient.SqlConnection

        If mConnectionString <> "" Then
            GcnRead.ConnectionString = mConnectionString
        Else
            GcnRead.ConnectionString = mConn.ConnectionString
        End If
        GcnRead.Open()

        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = GcnRead.CreateCommand
        End If

        mCmd = Dman_Execute("Select NewId()", GcnRead)

        GetGUID = mCmd.ExecuteScalar()

        GcnRead.Dispose()
    End Function

    Public Function GetGUID(ByVal mConnectionString As String)
        Dim mCmd As SqlClient.SqlCommand = Nothing
        Dim GcnRead As New SqlClient.SqlConnection


        GcnRead.ConnectionString = mConnectionString
        GcnRead.Open()

        If mCmd Is Nothing Then
            mCmd = New SqlCommand
            mCmd = GcnRead.CreateCommand
        End If

        mCmd = Dman_Execute("Select NewId()", GcnRead)

        GetGUID = mCmd.ExecuteScalar()

        GcnRead.Dispose()
        mCmd.Dispose()
    End Function
    Public Function MidStr(ByVal Str As String, Optional ByVal StartPosition As Integer = 0, Optional ByVal Len As Integer = 0, Optional ByVal TrimApply As Boolean = True) As String
        If Len = 0 Then Len = Str.Length
        If TrimApply = True Then Str = Str.Trim
        If Str <> "" Then
            MidStr = Str.Substring(StartPosition, Len)
        Else
            MidStr = Str
        End If
        Return MidStr
    End Function

    Public Sub IniHelpList(ByVal mConn As SqlClient.SqlConnection, ByVal ListBox As System.Windows.Forms.ComboBox, ByVal QryStr As String, ByVal DispField As String, ByVal HiddenField As String)
        Dim mSelectedValue As String
        mSelectedValue = ListBox.SelectedValue
        ListBox.DropDownStyle = ComboBoxStyle.DropDownList
        ListBox.AutoCompleteSource = AutoCompleteSource.ListItems
        ListBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        Dim DS As New DataTable
        EAdptr = New SqlClient.SqlDataAdapter(QryStr, mConn)
        EAdptr.Fill(DS)
        ListBox.DataSource = DS
        ListBox.DisplayMember = DispField
        ListBox.ValueMember = HiddenField
        If ListBox.Items.Count = 0 Then ListBox.Text = ""
        If mSelectedValue Is Nothing Then
            ListBox.SelectedValue = ""
        Else
            ListBox.SelectedValue = mSelectedValue
        End If
    End Sub

    Public Sub IniMasterHelpList(ByVal mConn As SqlClient.SqlConnection, ByVal ListBox As System.Windows.Forms.ComboBox, ByVal QryStr As String, ByVal DispField As String, ByVal HiddenField As String)
        Dim mSelectedValue As String
        Dim mText As String

        mSelectedValue = ListBox.SelectedValue
        mText = ListBox.Text

        ListBox.DropDownStyle = ComboBoxStyle.DropDown
        ListBox.AutoCompleteSource = AutoCompleteSource.ListItems
        ListBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend

        Dim DS As New DataTable
        EAdptr = New SqlClient.SqlDataAdapter(QryStr, mConn)
        EAdptr.Fill(DS)
        ListBox.DataSource = DS
        ListBox.DisplayMember = DispField
        ListBox.ValueMember = HiddenField
        If ListBox.Items.Count = 0 Then ListBox.Text = ""
        ListBox.SelectedValue = IIf(mSelectedValue Is Nothing, "", mSelectedValue)
        ListBox.Text = mText
    End Sub

    Public Function Chk_Text(ByVal temp As String) As String
        Chk_Text = temp
        If IsDBNull(Chk_Text) Or Chk_Text Is Nothing Then
            Chk_Text = "Null"
        Else
            If Chk_Text = "" Then
                Chk_Text = "Null"
            Else
                Chk_Text = "'" & Replace(Chk_Text, "'", "''") & "'"
            End If
        End If
    End Function

    Public Function Chk_Text(ByVal temp) As String
        If IsDBNull(temp) Or temp Is Nothing Then
            Chk_Text = "Null"
        Else
            If temp.ToString = "" Then
                Chk_Text = "Null"
            Else
                Chk_Text = "'" & Replace(temp, "'", "''") & "'"
            End If
        End If
    End Function

    Public Function Check_Entry(ByVal TableName As String, ByVal FieldName As String, ByVal FieldValue As String, ByVal FieldDataType As FieldType, ByVal Message As String, ByVal mConn As System.Data.SqlClient.SqlConnection, Optional ByVal ShowDetail As Boolean = False) As Boolean
        Dim ECmdTm As System.Data.SqlClient.SqlCommand
        Select Case FieldDataType
            Case FieldType.StringType
                ECmdTm = Dman_Execute("Select Count(" & FieldName & ") from " & TableName & " where " & FieldName & "='" & FieldValue & "'", mConn)
            Case FieldType.NumType
                ECmdTm = Dman_Execute("Select Count(" & FieldName & ") from " & TableName & " where " & FieldName & "=" & FieldValue & "", mConn)
            Case FieldType.DateType
                ECmdTm = Dman_Execute("Select Count(" & FieldName & ") from " & TableName & " where " & FieldName & "=" & ConvertDate(FieldValue) & "", mConn)
            Case Else
                ECmdTm = Nothing
        End Select

        If ECmdTm.ExecuteScalar() > 0 Then
            ECmdTm.Dispose()
            Check_Entry = False
            MsgBox("Related Records Exist in " & Message & ", Entry Can't Be Deleted", vbInformation, "Validation Check") : Exit Function
        Else
            Check_Entry = True
        End If
    End Function

    Public Function GetFileName(Optional ByVal FilePath As String = "") As String
        Dim SaveFileDialogBox As SaveFileDialog
        Dim sFilePath As String = ""
        Try
            SaveFileDialogBox = New SaveFileDialog

            SaveFileDialogBox.Title = "File Name"
            SaveFileDialogBox.Filter = "Microsoft Excel Worksheet(*.xls)|*.xls|XLSX Files(*.xlsx)|*.xlsx"

            If FilePath.Trim = "" Then FilePath = My.Application.Info.DirectoryPath
            SaveFileDialogBox.InitialDirectory = FilePath
            SaveFileDialogBox.DefaultExt = "*.xlsx"
            SaveFileDialogBox.FilterIndex = 2


            SaveFileDialogBox.FileName = ""

            If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Function

            sFilePath = SaveFileDialogBox.FileName
        Catch ex As Exception
        Finally
            GetFileName = sFilePath
        End Try
    End Function

    Public Sub GetPicture(ByVal mPictureBox As PictureBox, ByRef ByteArr As Byte(), Optional ByVal ImagePath As String = "")
        Dim OpenPicDialogBox As OpenFileDialog
        Dim Mem As MemoryStream
        Dim Img As Image


        OpenPicDialogBox = New OpenFileDialog

        OpenPicDialogBox.Title = "Set Image File"
        OpenPicDialogBox.Filter = "JPG Files(*.jpg)|*.jpg|JPEG Files(*.jpeg)|*.jpeg" & _
                                "|Gif Files(*.gif)|*.gif|Bitmap Files(*.bmp)|*.bmp"

        If ImagePath.Trim = "" Then ImagePath = My.Application.Info.DirectoryPath
        OpenPicDialogBox.InitialDirectory = ImagePath
        OpenPicDialogBox.DefaultExt = "*.jpeg"
        OpenPicDialogBox.FilterIndex = 1


        OpenPicDialogBox.FileName = ""


        If OpenPicDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim sFilePath As String
        sFilePath = OpenPicDialogBox.FileName
        If sFilePath = "" Then Exit Sub


        If System.IO.File.Exists(sFilePath) = False Then
            Exit Sub
        Else
            ByteArr = Get_ImageFile_Binary(sFilePath)
            Mem = New MemoryStream(ByteArr)
            Img = Image.FromStream(Mem)
            mPictureBox.Image = Img
            mPictureBox.Tag = sFilePath
        End If
    End Sub

    Public Function Get_ImageFile_Binary(ByVal mImageFilePath As String) As Byte()
        Dim FS As FileStream = New FileStream(mImageFilePath.ToString(), FileMode.Open)
        Dim ImgByte As Byte() = New Byte(FS.Length) {}
        FS.Read(ImgByte, 0, FS.Length)

        FS.Close()
        Get_ImageFile_Binary = ImgByte
    End Function

    Public Function GetDocId(ByVal V_Type As String, ByRef mVno As String, _
                         ByVal VDate As Date, ByVal mConn As SqlConnection, _
                         ByVal mDiv_Code As String, ByVal mSite_Code As String, Optional ByRef mCmd As SqlCommand = Nothing, _
                         Optional ByVal VPrefix As String = "", Optional ByVal mComp_Code As String = "") As String
        Dim DsTemp As DataSet
        Dim mQry As String, mCondStr As String = ""
        Dim MyFlag As Boolean = False
        Dim mDivisionWise As Byte = 0, mSiteWise As Byte = 0
        Dim mNumberMethod As String


        Try
            If mCmd Is Nothing Then
                mCmd = New SqlCommand
                mCmd = mConn.CreateCommand
            End If

            GetDocId = ""

            mCondStr = ""
            If VPrefix.Trim <> "" Then mCondStr += " And Vp.Prefix='" & VPrefix & "' "
            If mComp_Code.Trim <> "" Then mCondStr += " And IsNull(Vp.Comp_Code,'" & AglObj.PubCompCode & "') = '" & mComp_Code & "' "

            mQry = "Select top 1 VT.V_Type,VT.DivisionWise,VT.SiteWise, VT.Number_Method, VP.Prefix,VP.Start_Srl_No " & _
                " From Voucher_type VT With (NoLock) " & _
                " left join Voucher_prefix VP With (NoLock) on VT.V_Type=VP.V_Type " & _
                " Where VP.V_Type='" & V_Type & "' And VP.Date_From <= '" & VDate & "' And Vp.Date_To >= '" & VDate & "' " & mCondStr & _
                " Order by vp.date_from desc "
            mCmd.CommandText = mQry
            DsTemp = FillData(mQry, mConn)


            If DsTemp.Tables(0).Rows.Count > 0 Then
                mDivisionWise = Abs(VNull(DsTemp.Tables(0).Rows(0)("DivisionWise")))
                mSiteWise = Abs(VNull(DsTemp.Tables(0).Rows(0)("SiteWise")))
                VPrefix = XNull(DsTemp.Tables(0).Rows(0)("Prefix"))
                mNumberMethod = XNull(DsTemp.Tables(0).Rows(0)("Number_Method"))
                DsTemp = Nothing
            Else
                GetDocId = ""
                MyFlag = True : Exit Function
            End If

            mQry = "Select V_Type from Voucher_Prefix VP With (NoLock) " & _
                    " Where VP.V_Type='" & V_Type & "' And VP.Date_From <= " & ConvertDate(Format(VDate, "dd/MMM/yyyy")) & " AND VP.Date_To >= " & ConvertDate(Format(VDate, "dd/MMM/yyyy")) & " " & _
                    " " & mCondStr & "Order By VP.Date_From Desc"
            mCmd.CommandText = mQry
            DsTemp = FillData(mQry, mConn)

            If DsTemp.Tables(0).Rows.Count > 0 Then
                mQry = "Select Top 1 VP.V_Type,VP.Date_From,VP.Prefix,(Case When '" & mNumberMethod & "'='Manual' And " & Val(mVno) & "<>0 Then " & Val(mVno) & " Else  IsNull(VP.Start_Srl_No,0)+1 End) as Start_Srl_No " & _
                    " From Voucher_Type VT With (NoLock) " & _
                    " Left Join Voucher_Prefix VP With (NoLock) on VT.V_Type=VP.V_Type " & _
                    " Where VP.V_Type='" & V_Type & "' And VP.Date_From<=" & ConvertDate(Format(VDate, "dd/MMM/yyyy")) & "  And VP.Date_To>=" & ConvertDate(Format(VDate, "dd/MMM/yyyy")) & " " & mCondStr
                If mDivisionWise = 1 Then mQry = mQry + " and VP.Div_Code='" & mDiv_Code & "'"
                If mSiteWise = 1 Then mQry = mQry + " and VP.Site_Code='" & mSite_Code & "'"

                mQry = mQry + " Order By VP.Div_Code,VP.Site_Code,VP.Date_From DESC"

                DsTemp = Nothing
                DsTemp = FillData(mQry, mConn)

                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        VPrefix = XNull(.Rows(0)("Prefix"))
                        mVno = VNull(.Rows(0).Item("start_srl_no"))
                        'GetDocId = mDiv_Code + mSite_Code.PadRight(2, Space(1)) + V_Type.PadLeft(5, Space(1)) + CStr(XNull(.Rows(0)("Prefix"))).PadLeft(5, Space(1)) + CStr(mVno).PadLeft(8, "0")
                        GetDocId = mDiv_Code + mSite_Code.PadRight(2, Space(1)) + V_Type.PadLeft(5, Space(1)) + CStr(XNull(.Rows(0)("Prefix"))).PadLeft(5, Space(1)) + CStr(mVno).PadLeft(8, Space(1))
                    Else
                        GetDocId = ""
                        MyFlag = True : Exit Function
                    End If
                End With
            End If
        Catch ex As Exception
            GetDocId = ""
            MsgBox(ex.Message)
        Finally
            If MyFlag = True Then
                MsgBox("Please Define Voucher Numbering  System", vbInformation + vbOKOnly)
                GetDocId = ""
            End If
            DsTemp = Nothing
        End Try
    End Function

    Public Function IsProcedureExist(ByVal ProcedureName As String, ByVal mConn As SqlClient.SqlConnection, Optional ByVal DropProcedure As Boolean = False) As Boolean
        Dim mQry As String
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer

        mQry = "SELECT I.ROUTINE_CATALOG , I.ROUTINE_SCHEMA , I.ROUTINE_NAME " & _
                " FROM INFORMATION_SCHEMA.Routines I " & _
                " WHERE I.ROUTINE_NAME = '" & ProcedureName & "' And I.ROUTINE_TYPE = 'PROCEDURE'"
        DtTemp = FillData(mQry, mConn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            IsProcedureExist = True

            If DropProcedure Then
                For I = 0 To DtTemp.Rows.Count - 1
                    mQry = "Drop PROCEDURE [" & XNull(DtTemp.Rows(I)("ROUTINE_SCHEMA")) & "].[" & XNull(DtTemp.Rows(I)("ROUTINE_NAME")) & "]"
                    Dman_ExecuteNonQry(mQry, mConn)
                Next
            End If
        End If
        DtTemp = Nothing
    End Function

    Public Function IsViewExist(ByVal ViewName As String, ByVal mConn As SqlClient.SqlConnection, Optional ByVal DropView As Boolean = False) As Boolean
        Dim mQry As String
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer

        mQry = "Select I.TABLE_CATALOG , I.TABLE_SCHEMA , I.TABLE_NAME " & _
                " From INFORMATION_SCHEMA.Tables I " & _
                " Where Table_Name='" & ViewName & "' And  Table_Type='VIEW'"
        DtTemp = FillData(mQry, mConn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            IsViewExist = True

            If DropView Then
                For I = 0 To DtTemp.Rows.Count - 1
                    mQry = "Drop View [" & XNull(DtTemp.Rows(I)("TABLE_SCHEMA")) & "].[" & XNull(DtTemp.Rows(I)("TABLE_NAME")) & "]"
                    Dman_ExecuteNonQry(mQry, mConn)
                Next
            End If
        End If
        DtTemp = Nothing
    End Function

    Public Function IsTableExist(ByVal TableName As String, ByVal mConn As SqlClient.SqlConnection) As Boolean
        Dim mQry As String
        mQry = "Select Count(*) From INFORMATION_SCHEMA.tables Where Table_Name='" & TableName & "'"
        ECmd = Dman_Execute(mQry, mConn)
        If ECmd.ExecuteScalar() > 0 Then IsTableExist = True
    End Function

    Public Function IsFieldExist(ByVal FieldName As String, ByVal TableName As String, ByVal mConn As SqlClient.SqlConnection) As Boolean
        Dim mQry As String
        mQry = "Select Count(*) From INFORMATION_SCHEMA.Columns Where Table_Name='" & TableName & "' And Column_Name = '" & FieldName & "' "
        ECmd = Dman_Execute(mQry, mConn)
        If ECmd.ExecuteScalar() > 0 Then IsFieldExist = True
    End Function

    Public Function IsIdentityColumn(ByVal FieldName As String, ByVal TableName As String, ByVal mConn As SqlClient.SqlConnection) As Boolean
        Dim mQry As String
        mQry = "SELECT C.is_identity " & _
                " FROM sys.all_columns C With (NoLock) " & _
                " LEFT JOIN sys.Objects O With (NoLock) ON C.object_id =O.object_id " & _
                " WHERE O.name = '" & TableName & "' And C.Name = '" & FieldName & "' "
        ECmd = Dman_Execute(mQry, mConn)
        IsIdentityColumn = ECmd.ExecuteScalar()
    End Function

    Public Function IsConstraintExist(ByVal Constraint_Name As String, ByVal TableName As String, ByVal FieldsList As String, ByVal mConn As SqlClient.SqlConnection) As Boolean
        Dim mQry As String, mColumn_Name As String = ""
        Dim FieldStr() As String = Nothing
        Dim I As Integer

        FieldStr = Split(FieldsList, ",")

        For I = 0 To FieldStr.Length - 1
            If I = 0 Then
                mColumn_Name = "'" & FieldStr(I) & "'"
            Else
                mColumn_Name = mColumn_Name & ", " & "'" & FieldStr(I) & "'"
            End If
        Next

        mQry = "Select Count(*) From INFORMATION_SCHEMA.KEY_COLUMN_USAGE Where Constraint_Name='" & Constraint_Name & "' And Table_Name='" & TableName & "' And Column_Name in (" & mColumn_Name & ") "
        ECmd = Dman_Execute(mQry, mConn)
        If ECmd.ExecuteScalar() > 0 Then
            mQry = "Select Count(*) From INFORMATION_SCHEMA.KEY_COLUMN_USAGE Where Constraint_Name='" & Constraint_Name & "' And Table_Name='" & TableName & "' "
            ECmd = Dman_Execute(mQry, mConn)
            If ECmd.ExecuteScalar() = FieldStr.Length Then
                IsConstraintExist = True
            End If
        End If
    End Function

    Public Function AddPrimaryKeyConstraint(ByVal Constraint_Name As String, ByVal TableName As String, ByVal FieldsList As String, ByVal mConn As SqlClient.SqlConnection) As Boolean
        Dim mQry As String, mColumn_Name As String = ""
        Dim FieldStr() As String = Nothing
        Dim I As Integer
        Try
            If Not IsConstraintExist(Constraint_Name, TableName, FieldsList, mConn) Then
                FieldStr = Split(FieldsList, ",")

                For I = 0 To FieldStr.Length - 1
                    If I = 0 Then
                        mColumn_Name = "[" & FieldStr(I) & "] ASC "
                    Else
                        mColumn_Name = mColumn_Name & ", " & "[" & FieldStr(I) & "] ASC "
                    End If
                Next

                'ALTER TABLE [dbo].[Voucher_Prefix] ADD  CONSTRAINT [PK_Voucher_Prefix] PRIMARY KEY CLUSTERED 
                '(
                '	[V_Type] ASC,
                '	[Date_From] ASC,
                '	[Prefix] ASC,
                '	[Site_Code] ASC,
                '   [Div_Code](Asc)
                ')WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

                mQry = "ALTER TABLE [" & TableName & "] ADD  CONSTRAINT [" & Constraint_Name & "] PRIMARY KEY CLUSTERED " & _
                        " ( " & mColumn_Name & " " & _
                        " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"

                Dman_ExecuteNonQry(mQry, mConn)
            End If

            AddPrimaryKeyConstraint = True
        Catch ex As Exception
            AddPrimaryKeyConstraint = False
        End Try
    End Function

    Public Function AddUniqueKeyConstraint(ByVal Constraint_Name As String, ByVal TableName As String, ByVal FieldsList As String, ByVal mConn As SqlClient.SqlConnection) As Boolean
        Dim mQry As String, mColumn_Name As String = ""
        Dim FieldStr() As String = Nothing
        Dim I As Integer
        Try
            If Not IsConstraintExist(Constraint_Name, TableName, FieldsList, mConn) Then
                FieldStr = Split(FieldsList, ",")

                For I = 0 To FieldStr.Length - 1
                    If I = 0 Then
                        mColumn_Name = "[" & FieldStr(I) & "] ASC "
                    Else
                        mColumn_Name = mColumn_Name & ", " & "[" & FieldStr(I) & "] ASC "
                    End If
                Next

                mQry = "ALTER TABLE [" & TableName & "] ADD  CONSTRAINT [" & Constraint_Name & "] UNIQUE NONCLUSTERED " & _
                        " ( " & mColumn_Name & " " & _
                        " )WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]"

                Dman_ExecuteNonQry(mQry, mConn)
            End If

            AddUniqueKeyConstraint = True
        Catch ex As Exception
            AddUniqueKeyConstraint = False
        End Try
    End Function

    Public Function AddForeignKey(ByVal mConn As SqlClient.SqlConnection, ByVal Constraint_Name As String, _
                                    ByVal PrimaryKeyTable As String, ByVal ForeignKeyTable As String, _
                                    ByVal PrimaryKeyField As String, ByVal ForeignKeyField As String) As Boolean
        Dim mQry As String
        Try
            mQry = "SELECT Isnull(count(*),0) Cnt FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE='FOREIGN KEY' AND TABLE_NAME = '" & ForeignKeyTable & "' AND CONSTRAINT_NAME = '" & Constraint_Name & "' "
            ECmd = Dman_Execute(mQry, mConn)

            If ECmd.ExecuteScalar = 0 Then
                mQry = "ALTER TABLE [" & ForeignKeyTable & "]  WITH CHECK ADD  CONSTRAINT [" & Constraint_Name & "] FOREIGN KEY([" & ForeignKeyField & "]) REFERENCES [" & PrimaryKeyTable & "] ([" & PrimaryKeyField & "])"
                Dman_ExecuteNonQry(mQry, mConn)
                AddForeignKey = True
            End If
        Catch ex As Exception
            AddForeignKey = False
        End Try
    End Function

    Public Function DeleteForeignKey(ByVal mConn As SqlClient.SqlConnection, ByVal Constraint_Name As String, ByVal ForeignKeyTable As String) As Boolean
        Dim mQry As String
        Try
            mQry = "SELECT Isnull(count(*),0) Cnt FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE='FOREIGN KEY' AND TABLE_NAME = '" & ForeignKeyTable & "' AND CONSTRAINT_NAME = '" & Constraint_Name & "' "
            ECmd = Dman_Execute(mQry, mConn)

            If ECmd.ExecuteScalar > 0 Then
                mQry = "ALTER TABLE [" & ForeignKeyTable & "] DROP CONSTRAINT [" & Constraint_Name & "]"
                Dman_ExecuteNonQry(mQry, mConn)
            End If

            DeleteForeignKey = True
        Catch ex As Exception
            DeleteForeignKey = False
        End Try
    End Function

    Public Sub Modify_PrimaryKey(ByVal mConn As SqlClient.SqlConnection, ByVal TableName As String, ByVal FieldList As String)
        Dim mQry$, StrTemp$
        mQry = "SELECT CONSTRAINT_Name FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE='PRIMARY KEY' AND TABLE_NAME = '" & TableName & "' "
        ECmd = Dman_Execute(mQry, mConn)
        StrTemp = XNull(ECmd.ExecuteScalar)
        If StrTemp <> "" Then
            mQry = "ALTER TABLE " & TableName & " DROP CONSTRAINT " & StrTemp
            ECmd = Dman_Execute(mQry, mConn)
            ECmd.ExecuteNonQuery()
        End If
        mQry = "ALTER TABLE " & TableName & " Add CONSTRAINT [" & StrTemp & "] PRIMARY KEY (" & FieldList & ")"
        ECmd = Dman_Execute(mQry, mConn)
        ECmd.ExecuteNonQuery()
    End Sub



    Public Sub CreateNCat(ByVal mConn As SqlClient.SqlConnection, ByVal NCat As String, ByVal Category As String, ByVal NCatDescription As String, ByVal SiteCode As String)
        Dim mQry$ = ""
        mQry = "Select IsNull(Count(*),0) Cnt From VoucherCat Where NCat='" & NCat & "' And Category='" & Category & "' And NCatDescription='" & NCatDescription & "' "
        If VNull(Dman_Execute(mQry, mConn).ExecuteScalar) = 0 Then
            mQry = "INSERT INTO VoucherCat	(NCat,Category,SITE_CODE,NCatDescription,UserTypeYN)" & _
                    "VALUES ('" & NCat & "','" & Category & "','" & SiteCode & "','" & NCatDescription & "','Y')"
            Dman_ExecuteNonQry(mQry, mConn)
        End If
    End Sub

    Public Sub CreateVType(ByVal mConn As SqlClient.SqlConnection, ByVal NCat As String, ByVal Category As String, ByVal V_Type As String, ByVal Description As String, ByVal Short_Name As String, _
                             ByVal U_Name As String, ByVal U_EntDt As String, ByVal PubStartDate As String, ByVal PubEndDate As String, ByVal SiteCode As String, ByVal DivCode As String, _
                             Optional ByVal DivisionWise As Boolean = False, Optional ByVal SiteWise As Boolean = False, Optional ByVal Affect_FA As Boolean = True, _
                             Optional ByVal mComp_Code As String = "")

        Dim mQry$ = "", mCondStr$ = "", mSite_Code$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        mQry = "If Not Exists (Select * from Voucher_Type Where V_Type='" & V_Type & "') " & _
            "INSERT INTO Voucher_Type (NCat,Category,V_Type,Description,Short_Name,SystemDefine,DivisionWise,SiteWise,PreparedBy,U_EntDt,U_AE,Number_Method,Saperate_Narr,Common_Narr,ChqNo,ChqDt,ClgDt,Separate_Narr,Affect_Fa) " & _
            "VALUES ('" & NCat & "','" & Category & "','" & V_Type & "','" & Description & "','" & Short_Name & "','Y'," & IIf(DivisionWise, -1, 0) & "," & IIf(SiteWise, -1, 0) & ",'" & U_Name & "','" & U_EntDt & "','A','Automatic','N','Y',Null,Null,Null,'Y', " & IIf(Affect_FA, 1, 0) & ")"
        Dman_ExecuteNonQry(mQry, mConn)


        If DivisionWise = True Then mCondStr += " And Div_Code = '" & DivCode & "'"

        If mComp_Code.Trim = "" Then mComp_Code = AglObj.PubCompCode
        If mComp_Code.Trim <> "" Then mCondStr += " And IsNull(Comp_Code,'" & AglObj.PubCompCode & "') = '" & mComp_Code & "' "

        mQry = "SELECT S.Code AS Site_Code  FROM SiteMast S " & _
                " Where 1=1 " & IIf(SiteWise = True, "", " And S.Code = '" & SiteCode & "'") & " "

        DtTemp = FillData(mQry, mConn).Tables(0)
        With DtTemp
            If .Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    mSite_Code = .Rows(I)("Site_Code")

                    mQry = "If Not Exists (Select * from Voucher_Prefix Where V_Type='" & V_Type & "' And Date_From = " & ConvertDate(PubStartDate) & " And Date_To = " & ConvertDate(PubEndDate) & " And Site_Code = '" & mSite_Code & "' " & mCondStr & ") " & _
                            " INSERT INTO Voucher_Prefix	(V_Type,Date_From,Prefix,Start_Srl_No,Date_To,Site_Code,Div_Code,Comp_Code)" & _
                            " VALUES ('" & V_Type & " ', '" & PubStartDate & "', '" & Right(PubStartDate, 4) & "',0,'" & PubEndDate & "','" & mSite_Code & "','" & DivCode & "'," & AglObj.Chk_Text(mComp_Code) & ") "
                    Dman_ExecuteNonQry(mQry, mConn)
                Next
            End If
        End With
    End Sub

    Public Sub AddNewSubGroupType(ByVal mConn As SqlClient.SqlConnection, ByVal PartyTypeDescription As String, Optional ByVal PartyTypeCode As Integer = 0, _
                                 Optional ByVal SiteCode As String = "", Optional ByVal DivCode As String = "", Optional ByVal U_Name As String = "", Optional ByVal U_EntDt As String = "")
        Dim mQry$ = ""
        Dim mFlag As Boolean = True

        If PartyTypeCode = 0 Then
            mQry = "Select IsNull(Max(Party_Type),0) + 1 As MaxId From SubGroupType "
            PartyTypeCode = Dman_Execute(mQry, mConn).ExecuteScalar()
        Else
            mQry = "Select IsNull(Count(*),0) As Cnt From SubGroupType Where Party_Type=" & PartyTypeCode & " "
            If Dman_Execute(mQry, mConn).ExecuteScalar > 0 Then mFlag = False
        End If

        If mFlag = True Then

            mQry = "If Not Exists (Select * from SubGroupType Where Description= '" & PartyTypeDescription & "') " & _
                    " INSERT INTO SubGroupType ( Party_Type, Description, Div_Code, Site_Code, U_Name, U_EntDt, U_AE ) " & _
                    " VALUES ( " & PartyTypeCode & ", '" & PartyTypeDescription & "', " & Chk_Text(DivCode) & ", " & SiteCode & ", " & _
                    " " & Chk_Text(U_Name) & ", " & ConvertDate(" & Chk_Text(U_EntDt) & ") & ", 'A' ) "

            Dman_ExecuteNonQry(mQry, mConn)
        End If
    End Sub

    Public Sub AddNewVoucherReference(ByVal mConn As SqlClient.SqlConnection, ByVal Code As String, ByVal Description As String, Optional ByVal BoundField As String = "", Optional ByVal DisplayField As String = "", Optional ByVal IsDocId_DisplayField As Boolean = False, _
                                        Optional ByVal HelpQuery As String = "", Optional ByVal FilterField As String = "", Optional ByVal SiteField As String = "", Optional ByVal LastHiddenColumns As Integer = 0)
        Dim mQry$ = ""

        mQry = "If Not Exists (Select * from ReferenceTable Where Code = '" & Code & "') " & _
                " INSERT INTO ReferenceTable ( Code, Description, BoundField, DisplayField, IsDocId_DisplayField, " & _
                " HelpQuery, FilterField, SiteField, LastHiddenColumns) " & _
                " VALUES ( " & _
                " '" & Code & "', '" & Description & "', " & Chk_Text(BoundField) & ", " & Chk_Text(DisplayField) & ", " & IIf(IsDocId_DisplayField = True, 1, 0) & ", " & _
                " " & Chk_Text(HelpQuery) & ", " & Chk_Text(FilterField) & ", " & Chk_Text(SiteField) & "," & LastHiddenColumns & "    ) "

        Dman_ExecuteNonQry(mQry, mConn)

    End Sub

    Public Function AddNewField(ByVal mConn As SqlClient.SqlConnection, ByVal mTable As String, ByVal mColumn As String, ByVal mDataType As String, Optional ByVal mDefault_Value As String = "", Optional ByVal AllowNull As Boolean = True) As Boolean
        Dim mQry As String
        Dim mNullClause$
        Try
            Dim mDefault_Caluse As String = ""
            If mDefault_Value.Trim <> "" Then
                mDefault_Caluse = " Default " & mDefault_Value
            End If

            If AllowNull Then
                mNullClause = " Null "
            Else
                mNullClause = " Not Null "
            End If

            mQry = "select Isnull(count(*),0) from sysColumns where id = object_id('" & mTable & "') and name in ('" & mColumn & "')"
            ECmd = Dman_Execute(mQry, mConn)

            If ECmd.ExecuteScalar = 0 Then
                mQry = ("ALTER TABLE " & mTable & " Add " & mColumn & " " & mDataType & mNullClause & "  " & mDefault_Caluse)
                Dman_ExecuteNonQry(mQry, mConn)
                If mDefault_Value.Trim <> "" Then
                    mQry = ("Update " & mTable & " Set " & mColumn & "=" & mDefault_Value & " Where " & mColumn & " Is Null")
                    Dman_ExecuteNonQry(mQry, mConn)
                End If
                AddNewField = True
            End If
        Catch ex As Exception
            AddNewField = False
        End Try
    End Function

    Public Sub EditField(ByVal mTable As String, ByVal mColumn As String, ByVal mDataType As String, ByVal mConn As SqlClient.SqlConnection, Optional ByVal AllowNull As Boolean = True)
        Dim mQry As String
        Dim mNullClause$
        Try
            mQry = "select Isnull(count(*),0) from sysColumns where id = object_id('" & mTable & "') and name in ('" & mColumn & "')"
            ECmd = Dman_Execute(mQry, mConn)

            If ECmd.ExecuteScalar > 0 Then

                If AllowNull Then
                    mNullClause = " Null "
                Else
                    mNullClause = " Not Null "
                End If

                Dman_ExecuteNonQry("ALTER TABLE " & mTable & " ALter Column " & mColumn & " " & mDataType & mNullClause, mConn)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub DeleteField(ByVal mTable As String, ByVal mColumn As String, ByVal mConn As SqlClient.SqlConnection)
        Dim mQry As String, bStrDefaultConstraintName$ = ""
        Try
            mQry = "select Isnull(count(*),0) from sysColumns where id = object_id('" & mTable & "') and name in ('" & mColumn & "')"
            ECmd = Dman_Execute(mQry, mConn)

            If ECmd.ExecuteScalar > 0 Then
                bStrDefaultConstraintName = FunGetColumnDefaultConstraintName(mTable, mColumn, mConn)
                If bStrDefaultConstraintName.Trim <> "" Then
                    Dman_ExecuteNonQry("ALTER TABLE " & mTable & " DROP CONSTRAINT [" & bStrDefaultConstraintName & "]", mConn)
                End If

                Dman_ExecuteNonQry("ALTER TABLE " & mTable & " DROP COLUMN " & mColumn & " ", mConn)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function FunGetDataFilePath(ByVal mConn As SqlClient.SqlConnection) As String
        Dim mQry As String, bReturnStr$ = ""
        Try
            mQry = "SELECT Reverse(Substring(Reverse(F.physical_name), charindex('\',Reverse(F.physical_name)), Len(F.physical_name))) ReturnStr " & _
                    " FROM sys.Database_Files F " & _
                    " WHERE F.physical_name LIKE '%.Mdf'"

            bReturnStr = XNull(Dman_Execute(mQry, mConn))
        Catch ex As Exception
            bReturnStr = ""
        Finally
            FunGetDataFilePath = bReturnStr
        End Try
    End Function


    Public Function FunGetLogFilePath(ByVal mConn As SqlClient.SqlConnection) As String
        Dim mQry As String, bReturnStr$ = ""
        Try
            mQry = "SELECT Reverse(Substring(Reverse(F.physical_name), charindex('\',Reverse(F.physical_name)), Len(F.physical_name))) ReturnStr " & _
                    " FROM sys.Database_Files F " & _
                    " WHERE F.physical_name LIKE '%.Ldf'"

            bReturnStr = XNull(Dman_Execute(mQry, mConn))
        Catch ex As Exception
            bReturnStr = ""
        Finally
            FunGetLogFilePath = bReturnStr
        End Try
    End Function

    Public Function FunGetDataFileName(ByVal mConn As SqlClient.SqlConnection) As String
        Dim mQry As String, bReturnStr$ = ""
        Try
            mQry = "SELECT Reverse(Substring(Reverse(F.physical_name),0, charindex('\',Reverse(F.physical_name)))) ReturnStr " & _
                    " FROM sys.Database_Files F " & _
                    " WHERE F.physical_name LIKE '%.Mdf'"

            bReturnStr = XNull(Dman_Execute(mQry, mConn))
        Catch ex As Exception
            bReturnStr = ""
        Finally
            FunGetDataFileName = bReturnStr
        End Try
    End Function

    Public Function FunGetLogFileName(ByVal mConn As SqlClient.SqlConnection) As String
        Dim mQry As String, bReturnStr$ = ""
        Try
            mQry = "SELECT Reverse(Substring(Reverse(F.physical_name),0, charindex('\',Reverse(F.physical_name)))) ReturnStr " & _
                    " FROM sys.Database_Files F " & _
                    " WHERE F.physical_name LIKE '%.Ldf'"

            bReturnStr = XNull(Dman_Execute(mQry, mConn))
        Catch ex As Exception
            bReturnStr = ""
        Finally
            FunGetLogFileName = bReturnStr
        End Try
    End Function


    Public Function FunGetLogicalDataFileName(ByVal mConn As SqlClient.SqlConnection) As String
        Dim bReturnStr$ = ""
        Try
            bReturnStr = XNull(Dman_Execute("Select File_Name(1)", mConn).ExecuteScalar)
        Catch ex As Exception
            bReturnStr = ""
        Finally
            FunGetLogicalDataFileName = bReturnStr
        End Try
    End Function

    Public Function FunGetLogicalLogFileName(ByVal mConn As SqlClient.SqlConnection) As String
        Dim bReturnStr$ = ""
        Try
            bReturnStr = XNull(Dman_Execute("Select File_Name(2)", mConn).ExecuteScalar)
        Catch ex As Exception
            bReturnStr = ""
        Finally
            FunGetLogicalLogFileName = bReturnStr
        End Try
    End Function

    Public Function FunGetObjectId(ByVal StrObjectName As String, ByVal mConn As SqlClient.SqlConnection) As Integer
        Dim bIntReturnValue As Integer = 0
        Try
            bIntReturnValue = VNull(Dman_Execute("SELECT object_id FROM sys.all_objects O WHERE O.name  ='" & StrObjectName & "'", mConn).ExecuteScalar)
        Catch ex As Exception
            bIntReturnValue = 0
        Finally
            FunGetObjectId = bIntReturnValue
        End Try
    End Function

    Public Function FunGetObjectName(ByVal IntObjectId As Integer, ByVal mConn As SqlClient.SqlConnection) As String
        Dim bReturnStr$ = ""
        Try
            bReturnStr = XNull(Dman_Execute("SELECT Name FROM sys.all_objects O WHERE O.object_id =" & IntObjectId & "", mConn).ExecuteScalar)
        Catch ex As Exception
            bReturnStr = ""
        Finally
            FunGetObjectName = bReturnStr
        End Try
    End Function

    Public Function FunGetColumnDefaultConstraintName(ByVal StrTableName As String, ByVal StrColumnName As String, ByVal mConn As SqlClient.SqlConnection) As String
        Dim bReturnStr$ = "", bQryStr$ = ""
        Try
            bQryStr = "SELECT vO.name AS ConstraintName " & _
                        " FROM sys.All_Columns C " & _
                        " INNER JOIN ( " & _
                        " SELECT o.object_id AS TableObjectId, P.name AS TableObjectName, O.* " & _
                        " FROM sys.all_objects P " & _
                        " LEFT JOIN sys.All_Objects O On P.object_id = O.parent_object_id   " & _
                        " ) AS vO ON C.default_object_id  = vO.object_id " & _
                        " WHERE vO.TableObjectName ='" & StrTableName & "' " & _
                        " AND C.Name = '" & StrColumnName & "' "
            bReturnStr = XNull(Dman_Execute(bQryStr, mConn).ExecuteScalar)
        Catch ex As Exception
            bReturnStr = ""
        Finally
            FunGetColumnDefaultConstraintName = bReturnStr
        End Try
    End Function


    Public Function PubSiteCondition(ByVal mSite_Code_Field As String, ByVal mSite_Code As String) As String
        If mAglObj Is Nothing Then
            PubSiteCondition = "" & "" & mSite_Code_Field & "='" & mSite_Code & "'" & " "
        Else
            PubSiteCondition = "" & IIf(mAglObj.PubIsHo, " 1=1 ", "" & mSite_Code_Field & "='" & mSite_Code & "'") & " "
        End If
    End Function

    Public Sub BlankCtrl(ByRef mForm As Form)
        Dim mObj As Object

        For Each mObj In mForm.Controls
            With mObj
                If TypeOf mObj Is TextBox Then
                    .Text = ""
                    .Tag = ""
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is ComboBox Then
                    .SelectedValue = 0
                    .Text = ""
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is GroupBox Then
                    '.Text = ""
                    .BackColor = Color.Transparent
                    BlankTabPageCtrl(mObj)
                ElseIf TypeOf mObj Is DataGridView Then
                    .RowCount = 1
                    .Rows.Clear()
                ElseIf TypeOf mObj Is Label Then
                    .Tag = ""
                ElseIf TypeOf mObj Is DateTimePicker Then
                    .Tag = ""
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is TabPage Then
                    BlankTabPageCtrl(mObj)
                End If
            End With
        Next
    End Sub

    Public Sub BlankTabPageCtrl(ByRef mTabPage As Object)
        Dim mObj As Object

        For Each mObj In mTabPage.Controls
            With mObj
                If TypeOf mObj Is TextBox Then
                    .Text = ""
                    .Tag = ""
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is ComboBox Then
                    .SelectedValue = 0
                    .Text = ""
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is GroupBox Then
                    '.Text = ""
                    .BackColor = Color.Transparent
                    BlankTabPageCtrl(mObj)
                ElseIf TypeOf mObj Is DataGridView Then
                    .RowCount = 1
                    .Rows.Clear()
                ElseIf TypeOf mObj Is Label Then
                    .Tag = ""
                ElseIf TypeOf mObj Is DateTimePicker Then
                    .Tag = ""
                    .BackColor = Color.White
                End If
            End With
        Next
    End Sub

    Public Sub ChangeCtrlState(ByRef mForm As Form, ByVal Enb As Boolean)
        Dim mObj As Object

        For Each mObj In mForm.Controls
            With mObj
                If TypeOf mObj Is DataGridView Then
                    .ReadOnly = Not Enb
                    .AllowUserToAddRows = Enb
                    .AllowUserToDeleteRows = Enb
                ElseIf TypeOf mObj Is TextBox Or _
                       TypeOf mObj Is CheckBox Or _
                       TypeOf mObj Is GroupBox Or _
                       TypeOf mObj Is ComboBox Then
                    .enabled = Enb
                    .BackColor = Color.White

                ElseIf TypeOf mObj Is TabPage Then
                    ChangeTabPageCtrlState(mObj, Enb)
                End If

            End With
        Next
    End Sub

    Public Sub ChangeTabPageCtrlState(ByRef mTabPage As TabPage, ByVal Enb As Boolean)
        Dim mObj As Object

        For Each mObj In mTabPage.Controls
            With mObj
                If TypeOf mObj Is DataGridView Then
                    .ReadOnly = Not Enb
                    .AllowUserToAddRows = Enb
                    .AllowUserToDeleteRows = Enb
                ElseIf TypeOf mObj Is TextBox Or _
                       TypeOf mObj Is CheckBox Or _
                       TypeOf mObj Is GroupBox Or _
                       TypeOf mObj Is ComboBox Then
                    .enabled = Enb
                    .BackColor = Color.White
                End If

            End With
        Next
    End Sub

    Public Function RetMonthStartDate(ByVal mDate As Date) As String
        RetMonthStartDate = Format(CDate("01/" & MonthName(Month(mDate)) & "/" & Year(mDate)), "dd/MMM/yyyy")
    End Function

    Public Function RetMonthEndDate(ByVal mDate As Date) As String
        Dim TempDate As String
        TempDate = DateAdd("m", 1, CDate("01/" & MonthName(Month(mDate)) & "/" & Year(mDate)))
        RetMonthEndDate = Format(DateAdd("d", -1, CDate(TempDate)), "dd/MMM/yyyy")
    End Function

    Public Function RetMonthDays(ByVal mDate As Date) As Integer
        Dim TempDate As String
        TempDate = DateAdd("m", 1, CDate("01/" & MonthName(Month(mDate)) & "/" & Year(mDate)))
        RetMonthDays = CDate(Format(DateAdd("d", -1, CDate(TempDate)), "dd/MMM/yyyy")).Day
    End Function

    Public Function CreateSubGroup(ByVal Agl As ClsMain, ByVal mConn As SqlClient.SqlConnection, ByVal mCmd As SqlClient.SqlCommand, ByVal mConnectionString As String, ByVal DispName As String, ByVal ManualCode As String, ByVal GroupCode As String, ByVal GroupNature As String, ByVal Nature As String, ByVal PartyType As Integer, ByVal Site_Code As String) As String
        Dim mSearchCode As String
        Dim mName As String, mQry$
        Try
            If Site_Code.Trim = "" Then Err.Raise(1, , "Site Code Can't be Blank")
            If Len(DispName) > 100 Then Err.Raise(1, , "DispName Length Can't Exceed 100 Characters")
            If Len(ManualCode) > 20 Then Err.Raise(1, , "Manual Code Length Can't Exceed 20 Characters")

            mName = DispName + " {" + ManualCode + "}"

            mSearchCode = GetMaxId("SubGroup", "SubCode", mConn, Agl.PubDivCode, Site_Code, 8, True, True, mCmd, mConnectionString)
            mQry = "Insert Into SubGroup (SubCode, Name, DispName, GroupCode, GroupNature, Nature, ManualCode, Party_Type, " & _
                    " Div_Code, Site_Code, U_AE, U_EntDt, U_Name) Values(" & _
                    " '" & mSearchCode & "', " & Agl.Chk_Text(mName) & ", " & Agl.Chk_Text(DispName) & "," & _
                    " " & Agl.Chk_Text(GroupCode) & "," & Agl.Chk_Text(GroupNature) & "," & Agl.Chk_Text(Nature) & ", " & _
                    " " & Agl.Chk_Text(ManualCode) & "," & Val(PartyType) & ", " & _
                    " '" & Agl.PubDivCode & "', '" & Site_Code & "', 'A', '" & Format(Agl.PubLoginDate, "Short Date") & "', '" & Agl.PubUserName & "') "
            Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)
            CreateSubGroup = mSearchCode
        Catch ex As Exception
            CreateSubGroup = ""
        End Try

    End Function

    Public Sub AllowTableLog(ByVal Permission As Boolean, ByVal mConn As SqlClient.SqlConnection, Optional ByVal mCmd As SqlClient.SqlCommand = Nothing)
        If IsTableExist("Log_TablePermission", mConn) Then
            Dman_ExecuteNonQry("Update Log_TablePermission Set CreateLogYn=" & IIf(Permission, 1, 0) & " ", mConn, mCmd)
        End If
    End Sub


#Region "LogInInfo"


    Public Sub FPaintForm(ByVal FrmObjVar As Form, ByVal e As System.Windows.Forms.PaintEventArgs, ByVal IntHieght As Integer)
        Dim LGBBaseBackGround As System.Drawing.Drawing2D.LinearGradientBrush
        Dim RctVar As Rectangle
        Dim CtlVar As Control
        Dim StrVar As String

        'For Form
        RctVar = New Rectangle(0, IntHieght, FrmObjVar.Width, FrmObjVar.Height)
        LGBBaseBackGround = New System.Drawing.Drawing2D.LinearGradientBrush(RctVar, Color.WhiteSmoke, _
                            Color.FromArgb(175, 175, 175), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
        'LGBBaseBackGround = New System.Drawing.Drawing2D.LinearGradientBrush(RctVar, Color.WhiteSmoke, _
        '                    Color.FromArgb(172, 193, 233), System.Drawing.Drawing2D.LinearGradientMode.Vertical)

        e.Graphics.FillRectangle(LGBBaseBackGround, RctVar)


        If IntHieght > 0 Then
            'For TopCtrl
            RctVar = New Rectangle(0, 0, FrmObjVar.Width, IntHieght)
            LGBBaseBackGround = New System.Drawing.Drawing2D.LinearGradientBrush(RctVar, Color.Gray, _
                                Color.WhiteSmoke, System.Drawing.Drawing2D.LinearGradientMode.Vertical)
            e.Graphics.FillRectangle(LGBBaseBackGround, RctVar)
        End If


        For Each CtlVar In FrmObjVar.Controls
            StrVar = CtlVar.GetType.ToString
            If StrVar = "System.Windows.Forms.Label" Then
                CtlVar.BackColor = System.Drawing.Color.Transparent
            End If

            If StrVar = "System.Windows.Forms.GroupBox" Then
                If UCase(Trim(CtlVar.Tag)) = "UP" Or UCase(Trim(CtlVar.Tag)) = "TR" Then
                    CtlVar.BackColor = System.Drawing.Color.Transparent
                End If
            End If

            If UCase(Trim(CtlVar.GetType.Name)) = UCase("Topctrl") Then
                CtlVar.BackColor = System.Drawing.Color.Transparent
            End If

        Next
    End Sub

    Public Sub FSetSNo(ByVal FGObject As DataGridView, ByVal SrtCol As Short)
        Dim I As Int16
        For I = 0 To FGObject.RowCount - 1
            FGObject(SrtCol, I).Value = Trim(I + 1)
        Next
    End Sub

    Public Sub WinSetting(ByRef FrmName As Form, Optional ByVal frmHeight As Integer = 0, Optional ByVal frmWidth As Integer = 0, Optional ByVal frmTop As Integer = 0, Optional ByVal frmLeft As Integer = 0)
        If frmHeight = 0 Then frmHeight = 7635
        If frmWidth = 0 Then frmWidth = 11940

        With FrmName
            .Height = frmHeight
            .Width = frmWidth
            .Top = frmTop
            .Left = frmLeft
            .WindowState = 0
            .BackColor = ClrPubBackColorForm
        End With
    End Sub


    Public Sub GridDesign(ByVal FGObj As System.Windows.Forms.DataGridView)
        FGObj.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 250) 'Color.FromArgb(0, 64, 64)
        FGObj.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 250) 'Color.FromArgb(0, 64, 64)
        FGObj.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(169, 178, 202) 'Color.FromArgb(224, 224, 224)
        FGObj.RowsDefaultCellStyle.SelectionForeColor = Color.Black
        FGObj.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black 'Color.White
        FGObj.RowHeadersDefaultCellStyle.ForeColor = Color.Black 'Color.White
        FGObj.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)

        FGObj.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGObj.BorderStyle = BorderStyle.FixedSingle
        FGObj.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        FGObj.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        FGObj.RowHeadersVisible = False
        FGObj.MultiSelect = False
        FGObj.AllowUserToResizeRows = False
        FGObj.AllowUserToDeleteRows = True
    End Sub

#End Region

    Public Sub AddAgDataGrid(ByVal Dg As AgControls.AgDataGrid, ByVal Pnl As Panel)
        Dg.Height = Pnl.Height
        Dg.Width = Pnl.Width
        Dg.Top = Pnl.Top
        Dg.Left = Pnl.Left
        Pnl.Visible = False
        Pnl.Parent.Controls.Add(Dg)
        Dg.Visible = True
        Dg.BringToFront()

        'Dg.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        If Dg.Columns.Count > 1 Then FSetSNo(Dg, 0)
        Dg.TabIndex = Pnl.TabIndex
        Dg.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        Dg.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
    End Sub


    Function CreateImageFile(ByVal ByteData As Byte())
        If ByteData Is Nothing Then CreateImageFile = "Null" : Exit Function
        ' Create a file and write the byte data to a file.
        Dim oFileStream As System.IO.FileStream
        oFileStream = New System.IO.FileStream(AgLibrary.My.Application.Info.DirectoryPath + "\bytes.dat", System.IO.FileMode.Create)
        oFileStream.Write(ByteData, 0, ByteData.Length)
        oFileStream.Close()

        CreateImageFile = "(SELECT * FROM OPENROWSET(BULK N'" & AgLibrary.My.Application.Info.DirectoryPath & "\bytes.dat', SINGLE_BLOB) rs )"
    End Function

    Public Sub New()

    End Sub

    Public Function Chk_Qry(ByVal mQry As String) As String
        mQry = Replace(mQry, "`", "'")
        Chk_Qry = mQry
    End Function


    Public Function Portfolio_Save(ByVal mConn As SqlClient.SqlConnection, _
                               ByVal mCmd As SqlClient.SqlCommand, _
                               ByVal mConnRead As SqlClient.SqlConnection, _
                               ByVal Agl As AgLibrary.ClsMain, _
                               ByVal Portfolio As String, _
                               ByVal Div_Code As String, _
                               ByVal Site_Code As String, _
                               ByVal TxtName As AgControls.AgTextBox, _
                               ByVal TxtAddress1 As AgControls.AgTextBox, _
                               ByVal TxtAddress2 As AgControls.AgTextBox, _
                               ByVal TxtAddress3 As AgControls.AgTextBox, _
                               ByVal TxtCity As AgControls.AgTextBox, _
                               ByVal TxtPhone As AgControls.AgTextBox, _
                               ByVal TxtMobile As AgControls.AgTextBox, _
                               ByVal TxtFax As AgControls.AgTextBox, _
                               ByVal TxtEmail As AgControls.AgTextBox) As StructPortfolio
        Dim mQry$
        Dim ObjPort As StructPortfolio


        If Portfolio.Trim = "" Then
            Portfolio = CStr(Agl.FillData("Select CONVERT(NVARCHAR,REPLACE(NEWiD(),'-',''))", mConnRead).tABLES(0).rows(0)(0))
            mQry = "INSERT INTO dbo.Portfolio(Code,Name,Add1,Add2,Add3,City,Phone,Mobile,Fax,Email,Div_Code,Site_Code,PreparedBy,U_EntDt,U_AE) " & _
                   "VALUES 	(" & Agl.Chk_Text(Portfolio) & "," & Agl.Chk_Text(TxtName.Text) & "," & Agl.Chk_Text(TxtAddress1.Text) & "," & Agl.Chk_Text(TxtAddress2.Text) & "," & Agl.Chk_Text(TxtAddress3.Text) & "," & Agl.Chk_Text(TxtCity.AgSelectedValue) & "," & Agl.Chk_Text(TxtPhone.Text) & "," & Agl.Chk_Text(TxtMobile.Text) & "," & Agl.Chk_Text(TxtFax.Text) & "," & Agl.Chk_Text(TxtEmail.Text) & "," & Agl.Chk_Text(Div_Code) & "," & Agl.Chk_Text(Site_Code) & "," & Agl.Chk_Text(Agl.PubUserName) & "," & Agl.Chk_Text(Agl.PubLoginDate) & ",'A') "
        Else
            mQry = "Update Portfolio Set Name = " & Agl.Chk_Text(TxtName.Text) & ",Add1 = " & Agl.Chk_Text(TxtAddress1.Text) & ",Add2 = " & Agl.Chk_Text(TxtAddress2.Text) & ",Add3 = " & Agl.Chk_Text(TxtAddress3.Text) & ",City = " & Agl.Chk_Text(TxtCity.AgSelectedValue) & ",Phone = " & Agl.Chk_Text(TxtPhone.Text) & ",Mobile = " & Agl.Chk_Text(TxtMobile.Text) & ",Fax = " & Agl.Chk_Text(TxtFax.Text) & ",Email = " & Agl.Chk_Text(TxtEmail.Text) & ",Div_Code = " & Agl.Chk_Text(Div_Code) & ",Site_Code = " & Agl.Chk_Text(Site_Code) & ",U_AE = 'E',Edit_Date = " & Agl.Chk_Text(Agl.PubLoginDate) & ",ModifiedBy = " & Agl.Chk_Text(Agl.PubUserName) & "  Where   Code = " & Agl.Chk_Text(Portfolio) & " "
        End If

        Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

        With ObjPort
            .Code = Portfolio
            .Name = TxtName.Text
            .Add1 = TxtAddress1.Text
            .Add2 = TxtAddress2.Text
            .Add3 = TxtAddress3.Text
            .City = TxtCity.AgSelectedValue
            .Phone = TxtPhone.Text
            .Mobile = TxtMobile.Text
            .Fax = TxtFax.Text
            .Email = TxtEmail.Text
        End With
        Portfolio_Save = ObjPort

    End Function


    Public Function Portfolio_MoveRec(ByVal mConn As SqlClient.SqlConnection, _
                            ByVal Portfolio As String, _
                            ByVal TxtName As AgControls.AgTextBox, _
                            ByVal TxtAddress1 As AgControls.AgTextBox, _
                            ByVal TxtAddress2 As AgControls.AgTextBox, _
                            ByVal TxtAddress3 As AgControls.AgTextBox, _
                            ByVal TxtCity As AgControls.AgTextBox, _
                            ByVal TxtPhone As AgControls.AgTextBox, _
                            ByVal TxtMobile As AgControls.AgTextBox, _
                            ByVal TxtFax As AgControls.AgTextBox, _
                            ByVal TxtEmail As AgControls.AgTextBox) As StructPortfolio

        Dim DtTemp As DataTable = Nothing
        Dim ObjPortfolio As StructPortfolio = Nothing
        Try
            DtTemp = FillData("Select * From Portfolio Where Code = '" & Portfolio & "'", mConn).Tables(0)
            With DtTemp
                If DtTemp.Rows.Count > 0 Then
                    With ObjPortfolio

                        TxtName.Text = XNull(DtTemp.Rows(0)("Name"))
                        TxtAddress1.Text = XNull(DtTemp.Rows(0)("Add1"))
                        TxtAddress2.Text = XNull(DtTemp.Rows(0)("Add2"))
                        TxtAddress3.Text = XNull(DtTemp.Rows(0)("Add3"))
                        TxtCity.AgSelectedValue = XNull(DtTemp.Rows(0)("City"))
                        TxtPhone.Text = XNull(DtTemp.Rows(0)("Phone"))
                        TxtMobile.Text = XNull(DtTemp.Rows(0)("Mobile"))
                        TxtFax.Text = XNull(DtTemp.Rows(0)("Fax"))
                        TxtEmail.Text = XNull(DtTemp.Rows(0)("EMail"))

                        .Code = Portfolio
                        .Name = XNull(DtTemp.Rows(0)("Name"))
                        .Add1 = XNull(DtTemp.Rows(0)("Add1"))
                        .Add2 = XNull(DtTemp.Rows(0)("Add2"))
                        .Add3 = XNull(DtTemp.Rows(0)("Add3"))
                        .City = XNull(DtTemp.Rows(0)("City"))
                        .Phone = XNull(DtTemp.Rows(0)("Phone"))
                        .Mobile = XNull(DtTemp.Rows(0)("Mobile"))
                        .Fax = XNull(DtTemp.Rows(0)("Fax"))
                        .Email = XNull(DtTemp.Rows(0)("EMail"))
                    End With

                Else
                    With ObjPortfolio

                        TxtName.Text = ""
                        TxtAddress1.Text = ""
                        TxtAddress2.Text = ""
                        TxtAddress3.Text = ""
                        TxtCity.AgSelectedValue = ""
                        TxtPhone.Text = ""
                        TxtMobile.Text = ""
                        TxtFax.Text = ""
                        TxtEmail.Text = ""


                        .Code = ""
                        .Name = ""
                        .Add1 = ""
                        .Add2 = ""
                        .Add3 = ""
                        .City = ""
                        .Phone = ""
                        .Mobile = ""
                        .Fax = ""
                        .Email = ""
                    End With
                End If
            End With

        Catch ex As Exception
            AgLibrary.ClsErrHandler.HandleException(ex, " # Portfolio_Save Function of AgLibrary.ClsMain # ")
        Finally
            Portfolio_MoveRec = ObjPortfolio
            DtTemp.Dispose()
        End Try
    End Function

    Public Function PortfolioItem_Save(ByVal mConn As SqlClient.SqlConnection, _
                            ByVal mCmd As SqlClient.SqlCommand, _
                            ByVal mConnRead As SqlClient.SqlConnection, _
                            ByVal Portfolio As String, _
                            ByVal ObjPortfolio_ItemDetail As StructPortfolio_ItemDetail) As Integer
        Dim mQry As String


        With ObjPortfolio_ItemDetail
            If .Sr > 0 Then
                mQry = "Delete From Portfolio_ItemDetail Where Code = '" & Portfolio & "' and Sr=" & ObjPortfolio_ItemDetail.Sr & ""
                Dman_ExecuteNonQry(mQry, mConn, mCmd)
            Else
                .Sr = FillData("Select IsNull(Max(Sr),0)+1 From Portfolio_ItemDetail Where Code = '" & Portfolio & "'", mConnRead).tables(0).rows(0)(0)
            End If


            mQry = "INSERT INTO dbo.Portfolio_ItemDetail " & _
                    "(Code,ReferenceDocID,Sr,V_Date,Item, " & _
                    "UniqueID1,UniqueID2,UniqueID3,UniqueID4,Item_Nature1, " & _
                    "Item_Nature2,BatchNo,Mfg_ActivationDate) " & _
                   " VALUES " & _
                   "( " & Chk_Text(Portfolio) & " ,  " & Chk_Text(.ReferenceDocID) & " ,  " & Chk_Text(.Sr) & " ,  " & Chk_Text(.V_Date) & " ,  " & Chk_Text(.Item) & " , " & _
                   " " & Chk_Text(.UniqueID1) & " ,  " & Chk_Text(.UniqueID2) & " ,  " & Chk_Text(.UniqueID3) & " ,  " & Chk_Text(.UniqueID4) & " ,  " & Chk_Text(.ItemNature1) & " , " & _
                   " " & Chk_Text(.ItemNature2) & " ,  " & Chk_Text(.BatchNo) & " ,  " & Chk_Text(.Mfg_ActivationDate) & ") "

            Dman_ExecuteNonQry(mQry, mConn, mCmd)

            PortfolioItem_Save = .Sr
        End With
    End Function

    Public Structure StructPortfolio
        Dim Code As String
        Dim Name As String
        Dim Add1 As String
        Dim Add2 As String
        Dim Add3 As String
        Dim City As String
        Dim Phone As String
        Dim Mobile As String
        Dim Fax As String
        Dim Email As String
    End Structure

    Public Structure StructPortfolio_ItemDetail
        Dim Code As String
        Dim ReferenceDocID As String
        Dim Sr As Integer
        Dim V_Date As String
        Dim Item As String
        Dim UniqueID1 As String
        Dim UniqueID2 As String
        Dim UniqueID3 As String
        Dim UniqueID4 As String
        Dim ItemNature1 As String
        Dim ItemNature2 As String
        Dim BatchNo As String
        Dim Mfg_ActivationDate As String
    End Structure

    Public Sub ProcSmsSave(ByVal bConn As SqlConnection, ByVal bCmd As SqlCommand, _
                            ByVal SMS_Date As String, ByVal mCategory As String, _
                            ByVal mSubcode As String, _
                            ByVal mMobile As String, _
                            ByVal MesssageDate As String, _
                            ByVal SMS_Message As String)
        Dim bQry$ = "", bFeeDue1Code$ = ""

        Dim mSearchCodeSms As String

        If MesssageDate = "" Then MesssageDate = Date.Today
        If mMobile = "" Then Exit Sub

        mSearchCodeSms = mAglObj.GetMaxId("Sms_Trans", "Code", mAglObj.GCn, mAglObj.PubDivCode, mAglObj.PubSiteCode, 6, True, True, , mAglObj.Gcn_ConnectionString)


        bQry = "Insert Into Sms_Trans(Code,Sr,V_Date,Div_Code,Site_Code,Category,Mobile,Subcode,MsgDate,Msg,Status,PreparedBy,U_EntDt,U_AE) Values(" & _
                    " '" & mSearchCodeSms & "', 1, " & mAglObj.ConvertDate(mAglObj.PubLoginDate) & ", " & mAglObj.Chk_Text(mAglObj.PubDivCode) & ", " & mAglObj.Chk_Text(mAglObj.PubSiteCode) & ", " & mAglObj.Chk_Text(mCategory) & "," & mAglObj.Chk_Text(mMobile) & "," & mAglObj.Chk_Text(mSubcode) & "," & mAglObj.ConvertDate(SMS_Date) & "," & mAglObj.Chk_Text(SMS_Message) & ", 'Pending', " & mAglObj.Chk_Text(mAglObj.PubUserName) & ", " & mAglObj.ConvertDate(mAglObj.PubLoginDate) & ", 'A')"

        mAglObj.Dman_ExecuteNonQry(bQry, bConn, bCmd)

    End Sub

    Public Sub SendSms(ByVal Agl As AgLibrary.ClsMain)
        Dim DtSms As DataTable
        Dim DtSmsEnviro As DataTable
        Dim mhttp As HttpWebRequest
        Dim mHttpResponse As HttpWebResponse
        Dim mStreamReader As StreamReader
        Dim results As String
        Dim mQry$
        Dim I As Integer
        Dim bLongTotalSendSms As Long = 0, bLongSmsLimitBalance As Long = 0
        Try

            mQry = "Select * From Sms_Enviro With (NoLock) "
            DtSmsEnviro = Agl.FillData(mQry, Agl.GcnRead).Tables(0)
            '            If Not My.Computer.Network.Ping("www.google.com", 1000) Then

            If Agl.PubLongSmsLimit > 0 Then
                mQry = "Select IsNull(Count(*),0) As Cnt " & _
                        " From Sms_Trans With (NoLock) " & _
                        " Where Status = 'Send' "
                bLongTotalSendSms = Agl.Dman_Execute(mQry, Agl.GcnRead).ExecuteScalar

                If Agl.PubLongSmsLimit > bLongTotalSendSms Then
                    bLongSmsLimitBalance = Agl.PubLongSmsLimit - bLongSmsLimitBalance
                Else
                    MsgBox("SMS Limit Is Exceeding!...", MsgBoxStyle.Information, "SMS Limit")
                    Exit Sub
                End If
            End If

            mQry = "Select " & IIf(bLongSmsLimitBalance > 0, " TOP " & bLongSmsLimitBalance & "", "") & " RowID, Mobile, Msg " & _
                    " From Sms_Trans With (NoLock) " & _
                    " Where (MsgDate <='" & Agl.PubLoginDate & "' or MsgDate Is Null) And Status='Pending' "
            DtSms = Agl.FillData(mQry, Agl.GcnRead).Tables(0)


            For I = 0 To DtSms.Rows.Count - 1
                mhttp = WebRequest.Create(GetSmsAPI(XNull(DtSmsEnviro.Rows(0)("APICode")), XNull(DtSmsEnviro.Rows(0)("UserName")), XNull(DtSmsEnviro.Rows(0)("Password")), DtSms.Rows(I)("Mobile"), DtSms.Rows(I)("Msg")))

                mHttpResponse = mhttp.GetResponse
                mStreamReader = New StreamReader(mHttpResponse.GetResponseStream)
                results = mStreamReader.ReadToEnd

                If Not results.StartsWith("Wrong Username or Password") Then
                    mQry = "Update Sms_Trans set Status ='Send' Where RowID = " & DtSms.Rows(I)("RowID") & " "
                    Agl.Dman_ExecuteNonQry(mQry, Agl.GCn)
                End If
            Next I

            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetSmsAPI(ByVal SmsAPI As String, ByVal UserName As String, ByVal Password As String, ByVal Mobile As String, ByVal Message As String) As String
        Dim mApi As String
        mApi = Replace(SmsAPI, "{UserName}", UserName)
        mApi = Replace(mApi, "{Password}", Password)
        mApi = Replace(mApi, "{MobileNo}", Mobile)
        mApi = Replace(mApi, "{Message}", Message)
        GetSmsAPI = mApi
    End Function

    

    Public Function FunSendEMail(ByVal bFromMailIdStr As String, ByVal bFromMailIdPasswordStr As String, ByVal bToMailIdStr As String, _
                              ByVal bSubjectStr As String, ByVal bMessageStr As String, _
                              Optional ByVal bCcMailIdStr As String = "", Optional ByVal bBCcMailIdStr As String = "", _
                              Optional ByVal bAttachments As String = "", Optional ByVal bAttachmentPathStr As String = "") As Boolean

        Dim MLDFrom As System.Net.Mail.MailAddress
        Dim MLDTo As System.Net.Mail.MailAddress
        Dim MLMMain As System.Net.Mail.MailMessage
        Dim SMTPMain As System.Net.Mail.SmtpClient
        Dim I As Integer, J As Integer
        Dim bTempStr() As String, StrMailTo() As String, bAttachmentsStr() As String = Nothing
        Dim txtClientCode As New TextBox

        Dim SmtpHost$ = "mail.datamannet.com", SmtpPort$ = "25"

        StrMailTo = Split(bToMailIdStr, ",")
        If bAttachments.Trim <> "" Then bAttachmentsStr = Split(bAttachments, ",")

        For J = 0 To StrMailTo.Length - 1
            Try
                MLDTo = New System.Net.Mail.MailAddress(StrMailTo(J))
                MLDFrom = New System.Net.Mail.MailAddress(bFromMailIdStr)
                MLMMain = New System.Net.Mail.MailMessage(MLDFrom, MLDTo)
                SMTPMain = New System.Net.Mail.SmtpClient(SmtpHost, SmtpPort)


                '===================< Body Of Message >===================
                MLMMain.Body = bMessageStr
                '===================< *************** >===================

                '===================< Mail Subject >======================
                MLMMain.Subject = bSubjectStr
                '===================< *************** >===================

                If bCcMailIdStr Is Nothing Then bCcMailIdStr = ""
                If bCcMailIdStr.Trim <> "" Then
                    bTempStr = Split(bCcMailIdStr, ",")
                    For I = 0 To bTempStr.Length - 1
                        MLMMain.CC.Add(bTempStr(I))
                    Next
                End If

                If bBCcMailIdStr Is Nothing Then bBCcMailIdStr = ""
                If bBCcMailIdStr.Trim <> "" Then
                    bTempStr = Split(bBCcMailIdStr, ",")
                    For I = 0 To bTempStr.Length - 1
                        MLMMain.Bcc.Add(bTempStr(I))
                    Next
                End If

                If Not bAttachmentsStr Is Nothing Then
                    For I = 0 To bAttachmentsStr.Length - 1
                        MLMMain.Attachments.Add(New System.Net.Mail.Attachment(bAttachmentPathStr & "\" & bAttachmentsStr(I)))
                    Next
                End If

                SMTPMain.Credentials = New Net.NetworkCredential(bFromMailIdStr, bFromMailIdPasswordStr)
                'SMTPMain.Credentials = New Net.NetworkCredential("support@datamannet.com", "dataman")
                SMTPMain.Send(MLMMain)


                MLMMain.Dispose()

                FunSendEMail = True
            Catch ex As Exception
                MsgBox(ex.Message)
                FunSendEMail = False
            End Try
        Next
    End Function

    Public Function funGetBankCode(ByVal bBank_Name As String, ByVal bSite_Code As String, ByVal bDiv_Code As String, _
                                    Optional ByVal bConn As SqlClient.SqlConnection = Nothing, _
                                    Optional ByVal bCmd As SqlClient.SqlCommand = Nothing) As String
        Dim bBank_Code As String = ""
        Dim mQry$
        Try
            If bBank_Name.Trim = "" Then Err.Raise(1, , "Bank Name Can't be Blank")
            If Len(bBank_Name) > 100 Then Err.Raise(1, , "Bank Name Length Can't Exceed 100 Characters")
            bBank_Code = AglObj.XNull(AglObj.Dman_Execute("SELECT B.Bank_Code FROM Bank B WHERE B.Bank_Name = '" & bBank_Name & "'", AglObj.GcnRead).ExecuteScalar).ToString

            If bBank_Code.Trim = "" Then
                If bConn Is Nothing Then
                    bConn = AglObj.GcnRead
                End If

                If bSite_Code.Trim = "" Then Err.Raise(1, , "Site Code Can't be Blank")
                If bDiv_Code.Trim = "" Then Err.Raise(1, , "Division Code Can't be Blank")

                bBank_Code = AglObj.GetMaxId("Bank", "Bank_Code", AglObj.GcnRead, bDiv_Code, bSite_Code, 6, True, True)

                mQry = "Insert Into Bank (Bank_Code, Bank_Name, Div_Code, Site_Code, U_EntDt, PreparedBy, U_AE) Values(" & _
                        " '" & bBank_Code & "', " & AglObj.Chk_Text(bBank_Name.Trim) & ", " & _
                        " '" & bDiv_Code & "', '" & bSite_Code & "', '" & Format(AglObj.PubLoginDate, "Short Date") & "', '" & AglObj.PubUserName & "', 'A') "
                AglObj.Dman_ExecuteNonQry(mQry, bConn, bCmd)
            End If

        Catch ex As Exception
            bBank_Code = ""
        Finally
            funGetBankCode = bBank_Code
        End Try

    End Function


    Public Function RetDivisionCondition(ByVal Agl As AgLibrary.ClsMain, ByVal Div_Fld As String)
        Dim mCond As String = ""
        If Agl.PubDivWiseBrowsing Then
            mCond = " And IsNull(" & Div_Fld & ",'" & Agl.PubDivCode & "') = '" & Agl.PubDivCode & "' "
        ElseIf Not StrCmp(Agl.PubUserName, "Sa") Then
            mCond = " And IsNull(" & Div_Fld & ",'" & Agl.PubDivCode & "') In " & Agl.PubDivisionList & " "
        End If
        RetDivisionCondition = mCond
    End Function

    Public Function RetValidDate(ByVal StrDate As String) As String
        Dim bStrRetrunValue As String

        If StrDate.Trim = "" Then
            StrDate = AglObj.PubLoginDate
        Else
            If Not IsDate(StrDate) Then
                StrDate = AglObj.PubLoginDate
            End If
        End If

        If CDate(StrDate) > CDate(AglObj.PubEndDate) Then
            bStrRetrunValue = AglObj.PubEndDate
        ElseIf CDate(StrDate) < CDate(AglObj.PubStartDate) Then
            bStrRetrunValue = AglObj.PubStartDate
        Else
            bStrRetrunValue = StrDate
        End If

        Return bStrRetrunValue
    End Function

    Public Function RetFinancialYearDate(ByVal StrDate As String) As String
        Dim bStrRetrunValue As String

        If StrDate.Trim = "" Then
            StrDate = AglObj.PubLoginDate
        Else
            If Not IsDate(StrDate) Then
                StrDate = AglObj.PubLoginDate
            End If
        End If

        If CDate(StrDate) > CDate(AglObj.PubEndDate) Then
            bStrRetrunValue = MidStr(Format(StrDate, AgLibrary.ClsConstant.DateFormat_ShortDate), 0, 7) + CDate(AglObj.PubEndDate).Year.ToString

            If CDate(bStrRetrunValue) > CDate(AglObj.PubEndDate) Then
                bStrRetrunValue = MidStr(Format(bStrRetrunValue, AgLibrary.ClsConstant.DateFormat_ShortDate), 0, 7) + CDate(AglObj.PubStartDate).Year.ToString
            End If
        ElseIf CDate(StrDate) < CDate(AglObj.PubStartDate) Then
            bStrRetrunValue = MidStr(Format(StrDate, AgLibrary.ClsConstant.DateFormat_ShortDate), 0, 7) + CDate(AglObj.PubStartDate).Year.ToString

            If CDate(bStrRetrunValue) < CDate(AglObj.PubStartDate) Then
                bStrRetrunValue = MidStr(Format(bStrRetrunValue, AgLibrary.ClsConstant.DateFormat_ShortDate), 0, 7) + CDate(AglObj.PubEndDate).Year.ToString
            End If
        Else
            bStrRetrunValue = StrDate
        End If



        Return Format(bStrRetrunValue, AgLibrary.ClsConstant.DateFormat_ShortDate)
    End Function


    Public Sub AddCheckColumn(ByVal DGL As System.Windows.Forms.DataGridView, _
                                    ByVal ColumnName As String, _
                                    ByVal ColWidth As Integer, ByVal mMaxInputLength As Integer, _
                                    Optional ByVal ColumnHeaderTxt As String = "", _
                                    Optional ByVal ColumnVisible As Boolean = True, _
                                    Optional ByVal IsReadOnly As Boolean = False, _
                                    Optional ByVal IsRightAlign As Boolean = False, _
                                    Optional ByVal IsMandatory As Boolean = False, _
                                    Optional ByVal mSortMode As System.Windows.Forms.DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)

        AgCL.AddAgTextColumn(DGL, ColumnName, ColWidth, mMaxInputLength, ColumnHeaderTxt, ColumnVisible, IsReadOnly, IsRightAlign, IsMandatory, mSortMode)
        DGL.Columns(ColumnName).Tag = AgLibrary.ClsConstant.StrCheckedValue
        DGL.Columns(ColumnName).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DGL.Columns(ColumnName).DefaultCellStyle.ForeColor = Color.Black
        DGL.Columns(ColumnName).DefaultCellStyle.BackColor = Color.White
        DGL.Columns(ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Public Sub ProcSetCheckColumnCellValue(ByVal DGL As System.Windows.Forms.DataGridView, ByVal ColumnIndex As Integer)
        Dim I As Integer = 0
        For I = 0 To DGL.SelectedCells.Count
            If DGL.SelectedCells.Item(I).ColumnIndex = ColumnIndex Then
                If DGL.SelectedCells.Item(I).Value Is Nothing Then DGL.SelectedCells.Item(I).Value = AgLibrary.ClsConstant.StrUnCheckedValue

                If DGL.SelectedCells.Item(I).Value.ToString.Trim = "" _
                    Or DGL.SelectedCells.Item(I).Value.ToString.Trim = AgLibrary.ClsConstant.StrUnCheckedValue Then

                    DGL.SelectedCells.Item(I).Value = AgLibrary.ClsConstant.StrCheckedValue
                Else
                    DGL.SelectedCells.Item(I).Value = AgLibrary.ClsConstant.StrUnCheckedValue
                End If
            End If
        Next
    End Sub

    Public Function FunGetUserModuleList(ByVal StrUserName As String) As String
        Dim bStrModuleList$ = ""

        If Not (AglObj.StrCmp(StrUserName, "SA") Or AglObj.StrCmp(StrUserName, AgLibrary.ClsConstant.PubSuperUserName)) Then
            If AglObj.IsFieldExist("ModuleList", "UserMast", AglObj.GcnMain) Then
                bStrModuleList = AglObj.XNull(AglObj.Dman_Execute("SELECT IsNull(U.ModuleList,'') As ModuleList FROM UserMast U WHERE U.USER_NAME = '" & StrUserName & "' And IsNull(U.ModuleList,'') <> '' ", AglObj.GcnMain).ExecuteScalar)
            End If
        End If

        Return (bStrModuleList)
    End Function


    Public Function FunHaveControlPermission(ByVal StrModule As String, ByVal StrMenuText As String, ByVal StrUserName As String, ByVal StrGroupText As String) As Boolean
        Dim bQry$ = ""
        Dim bBlnHaveControlPermission As Boolean = False

        bQry = "SELECT IsNull(Count(*),0) AS Cnt " & _
                " FROM User_Control_Permission " & _
                " WHERE MnuModule = '" & StrModule & "' AND " & _
                " MnuText='" & StrMenuText & "' AND " & _
                " UserName = '" & IIf(AglObj.StrCmp(AglObj.PubUserName, AgLibrary.ClsConstant.PubSuperUserName), "SA", AglObj.PubUserName) & "' AND " & _
                " GroupText = '" & StrGroupText & "' "
        bBlnHaveControlPermission = IIf(AglObj.Dman_Execute(bQry, AglObj.GcnMain).ExecuteScalar > 0, True, False)

        Return bBlnHaveControlPermission
    End Function

    Public Function FunGetUserMainStreamCode(ByVal StrUserName As String, ByVal StrSeniorName As String) As String
        Dim bQry$ = "", bStrReturn$ = "", bStrSeniorNameMainStreamCode$ = "", bStrMainStreamCode$ = ""

        Dim bIntI As Integer
        Dim bDtTemp As DataTable = Nothing
        Try
            If StrUserName.Trim = "" Then
                bStrReturn = ""
            Else
                If StrSeniorName.Trim = "" Then
                    If AglObj.StrCmp(StrUserName, "SA") Then
                        bStrReturn = AgLibrary.ClsConstant.StrIniMainStreamCode
                    Else
                        bStrReturn = ""
                    End If
                Else
                    bQry = "SELECT U.MainStreamCode " & _
                            " FROM UserMast U WITH (NoLock) " & _
                            " WHERE U.User_Name = " & AglObj.Chk_Text(StrSeniorName) & " "
                    bStrSeniorNameMainStreamCode = AglObj.XNull(AglObj.Dman_Execute(bQry, AglObj.GcnRead).ExecuteScalar).ToString

                    For bIntI = 1 To 999
                        bStrMainStreamCode = bStrSeniorNameMainStreamCode + bIntI.ToString.PadLeft(3, "0")

                        bQry = "SELECT IsNull(Count(*),0) AS Cnt " & _
                                " FROM UserMast U WITH (NoLock) " & _
                                " WHERE U.MainStreamCode = '" & bStrMainStreamCode & "' "
                        If AglObj.VNull(AglObj.Dman_Execute(bQry, AglObj.GcnRead).ExecuteScalar) = 0 Then
                            Exit For
                        Else
                            If bIntI = 999 Then bStrMainStreamCode = ""
                        End If
                    Next

                    bStrReturn = bStrMainStreamCode
                End If
            End If
        Catch ex As Exception
            bStrReturn = ""
            MsgBox(ex.Message)
        Finally
            If bDtTemp IsNot Nothing Then bDtTemp.Dispose()
        End Try

        Return bStrReturn
    End Function

#Region "Update Table Structure Functions"
    Public Enum SQLDataType
        VarCharMax = 0
        VarChar = 1
        SmallInt = 2
        Int = 3
        TinyInt = 4
        SmallDateTime = 5
        Float = 6
        nVarChar = 7
        BigInt = 8
        IDENTITY = 9
        Bit = 10
        uniqueidentifier = 11
        image = 12
    End Enum

    Public Structure LIException
        Dim StrValue1 As String
        Dim StrValue2 As String
        Dim StrValue3 As String
        Dim StrValue4 As String
        Dim StrValue5 As String
        Dim StrMessage As String
    End Structure

    Public Structure LITable
        Dim StrModuleName As String
        Dim StrName As String
        Dim FKey() As LIForeignKey
        Dim ColItem() As LIColumn
    End Structure

    Public Structure LIColumn
        Dim StrName As String
        Dim StrDataType As String
        Dim IntLength As String
        Dim BlnPrimaryKey As Boolean
        Dim BlnAllowNull As Boolean
        Dim StrDefaultValue As String
    End Structure

    Public Structure LIForeignKey
        Dim StrOnColumn As String
        Dim StrWithColumn As String
        Dim StrWithTable As String
    End Structure

    Public Function FGetType(ByVal SQLDT As SQLDataType) As String
        Dim StrRtn As String

        Select Case SQLDT
            Case SQLDataType.Float
                StrRtn = "Float"
            Case SQLDataType.Int
                StrRtn = "Int"
            Case SQLDataType.VarChar
                StrRtn = "VarChar"
            Case SQLDataType.VarCharMax
                StrRtn = "VarChar(MAX)"
            Case SQLDataType.SmallDateTime
                StrRtn = "SmallDateTime"
            Case SQLDataType.TinyInt
                StrRtn = "TinyInt"
            Case SQLDataType.SmallInt
                StrRtn = "SmallInt"
            Case SQLDataType.nVarChar
                StrRtn = "nVarChar"
            Case SQLDataType.BigInt
                StrRtn = "BigInt"
            Case SQLDataType.IDENTITY
                StrRtn = "BIGINT IDENTITY"
            Case SQLDataType.Bit
                StrRtn = "Bit"
            Case SQLDataType.uniqueidentifier
                StrRtn = "uniqueidentifier"
            Case SQLDataType.image
                StrRtn = "image"
            Case Else
                StrRtn = ""
        End Select
        Return StrRtn
    End Function

    'Note:- This Is A Function For Creating Tables In Related Database
    'It Does Not Remove Fields But Add/Alter Fields And Keys
    Public Function FExecuteDBScript(ByVal ModuleTable() As LITable) As LIException()
        Dim StrSQL As String
        Dim LIExcpAry(0) As LIException

        'Note:- Making Temporary Tables And Collecting Keys 
        StrSQL = FCollectKeys(ModuleTable)

        'Note:- Droping Constraints PK/FK/Default Values
        FDropKeys(StrSQL, LIExcpAry)

        'Note:- Adding/Altering Tables In Database
        FSynchronizeTables(ModuleTable, LIExcpAry)

        'Note:- Setting Primary Keys/ Foreign Keys For All Tables
        FSetPK_FK(StrSQL, LIExcpAry)

        Return LIExcpAry
    End Function

    'Note:- Adding/Altering Tables In Database
    Public Sub FSynchronizeTables(ByRef ModuleTable() As LITable, ByRef LIExcpAry() As LIException)
        Dim I As Integer
        Dim J As Integer
        Dim FrmPGB As New FrmProgressBar

        FrmPGB.Show()
        FrmPGB.FMoveBar()

        For I = 0 To UBound(ModuleTable) - 1
            FrmPGB.FMoveBar()
            'Note:- Adding A Blank Table With Temporary Column __Temp__
            FAddException(LIExcpAry, FCreateTable(ModuleTable(I)))

            For J = 0 To UBound(ModuleTable(I).ColItem) - 1
                FrmPGB.FMoveBar()
                'Adding/Altering Columns
                FAddException(LIExcpAry, FCreateColumn(ModuleTable(I).StrName, ModuleTable(I).ColItem(J)))
            Next

            FrmPGB.FMoveBar()
            'Note:- Droping Temporary Column __Temp__
            FAddException(LIExcpAry, FDropColumn(ModuleTable(I).StrName, "__Temp__"))
        Next

        FrmPGB.Close()
        FrmPGB = Nothing
    End Sub

    Public Sub FSetPK_FK(ByRef StrSQL As String, ByRef LIExcpAry() As LIException)
        Dim I As Integer
        Dim DTTemp As DataTable
        Dim FrmPGB As New FrmProgressBar

        FrmPGB.Show()
        FrmPGB.FMoveBar()

        '======================================================================
        '============================== START ================================= 
        '======================================================================
        'Note:- Setting Primary Keys For All Tables
        DTTemp = FillData(StrSQL & "Select OnTable From @TmpTable Where ColType='PK' Group By OnTable ", GCn).Tables(0)
        For I = 0 To DTTemp.Rows.Count - 1
            FrmPGB.FMoveBar()
            FAddException(LIExcpAry, FSetPrimaryKey(StrSQL, XNull(DTTemp.Rows(I).Item("OnTable"))))
        Next
        DTTemp.Dispose()
        DTTemp = Nothing
        '======================================================================
        '============================= END ====================================
        '======================================================================


        '======================================================================
        '============================== START ================================= 
        '======================================================================
        'Note:- Setting Foreign Keys For All Tables
        DTTemp = FillData(StrSQL & "Select OnTable,OnColumn,WithTable,WithColumn " & _
                    "From @TmpTable Where ColType='FK' " & _
                    "Group By OnTable,OnColumn,WithTable,WithColumn ", GCn).Tables(0)
        For I = 0 To DTTemp.Rows.Count - 1
            FrmPGB.FMoveBar()
            FAddException(LIExcpAry, FSetForeignKey(XNull(DTTemp.Rows(I).Item("OnTable")), XNull(DTTemp.Rows(I).Item("WithTable")), _
                         XNull(DTTemp.Rows(I).Item("OnColumn")), XNull(DTTemp.Rows(I).Item("WithColumn"))))
        Next
        DTTemp.Dispose()
        DTTemp = Nothing
        '======================================================================
        '============================= END ====================================
        '======================================================================

        FrmPGB.Close()
        FrmPGB = Nothing
    End Sub

    'Note:- Making Temporary Tables And Collecting Keys 
    Public Function FCollectKeys(ByRef ModuleTable() As LITable) As String
        Dim I As Integer
        Dim StrSQL As String
        Dim FrmPGB As New FrmProgressBar

        FrmPGB.Show()
        FrmPGB.FMoveBar()

        StrSQL = "Declare @TmpTable As Table(ColType NVarChar(20),OnColumn NVarChar(100), "
        StrSQL = StrSQL & "OnTable NVarChar(100),WithColumn NVarChar(100),WithTable NVarChar(100)) "
        For I = 0 To UBound(ModuleTable) - 1
            'FrmPGB.FMoveBar()
            StrSQL = StrSQL & FCollectPrimaryKey(ModuleTable(I).StrName, ModuleTable(I).ColItem)
            StrSQL = StrSQL & FCollectForeignKey(ModuleTable(I).StrName, ModuleTable(I).FKey)
        Next
        FrmPGB.Close()
        FrmPGB = Nothing

        FCollectKeys = StrSQL
    End Function

    'Note:- Droping Constraints PK/FK/Default Values
    Public Sub FDropKeys(ByRef StrSQL As String, ByRef LIExcpAry() As LIException)
        Dim I As Integer
        Dim DTTemp As DataTable
        Dim FrmPGB As New FrmProgressBar

        FrmPGB.Show()
        FrmPGB.FMoveBar()

        DTTemp = FillData(StrSQL & _
                    "Select SO.Name As DFName,SO1.Name As OnTable " & _
                    "From SysObjects SO Left Join SysObjects SO1 On SO.Parent_Obj=SO1.ID " & _
                    "Where SO.XType<>'TR' And SO.Parent_Obj In " & _
                    "(Select ID From SysObjects Where Name In " & _
                    "(Select OnTable From @TmpTable Group By OnTable)) " & _
                    "Order By SO.XType", GCn).Tables(0)
        For I = 0 To DTTemp.Rows.Count - 1
            FrmPGB.FMoveBar()
            FAddException(LIExcpAry, FDropConstraint(XNull(DTTemp.Rows(I).Item("OnTable")), _
                      XNull(DTTemp.Rows(I).Item("DFName"))))
        Next
        DTTemp.Dispose()
        DTTemp = Nothing

        FrmPGB.Close()
        FrmPGB = Nothing
    End Sub

    'Note:- This Procedure Drops All Constraint Of Related Tables
    Private Function FDropConstraint(ByVal StrOnTable As String, ByVal StrCnstName As String) As LIException
        Dim SQLCmd As New SqlClient.SqlCommand
        Dim LIExpRtn As New LIException

        Try
            LIExpRtn.StrValue1 = "FDropConstraint"
            LIExpRtn.StrValue2 = StrOnTable
            LIExpRtn.StrValue3 = StrCnstName

            SQLCmd.CommandTimeout = 0
            SQLCmd.Connection = GCn
            SQLCmd.CommandText = "Alter Table " & StrOnTable & "  "
            SQLCmd.CommandText = SQLCmd.CommandText & "Drop Constraint " & StrCnstName
            SQLCmd.ExecuteNonQuery()
            LIExpRtn.StrValue4 = "Constraint Dropped Successfully."
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try
        Return LIExpRtn
    End Function

    'Note:- Ths Procedure Set Foreign Key For Related Tables
    Private Function FSetForeignKey(ByVal StrOnTable As String, ByVal StrWithTable As String, _
    ByVal StrOnColumn As String, ByVal StrWithColumn As String) As LIException
        Dim StrSQL As String
        Dim SQLCmd As New SqlClient.SqlCommand
        Dim LIExpRtn As New LIException

        LIExpRtn.StrValue1 = "FSetForeignKey"
        LIExpRtn.StrValue2 = StrOnTable
        LIExpRtn.StrValue3 = StrOnColumn
        Try
            SQLCmd.CommandTimeout = 0
            SQLCmd.Connection = GCn
            StrSQL = "Alter Table " & StrOnTable & " Add Constraint "
            StrSQL = StrSQL & "[FK_" & StrOnTable & "_"
            StrSQL = StrSQL & StrWithTable & "_" & Replace(StrOnColumn, ",", "_") & "] "
            StrSQL = StrSQL & "FOREIGN KEY(  "
            StrSQL = StrSQL & " [" & Replace(StrOnColumn, ",", "],[") & "]) "
            StrSQL = StrSQL & " REFERENCES [" & StrWithTable & "] (["
            StrSQL = StrSQL & Replace(StrWithColumn, ",", "],[") & "]) "

            SQLCmd.CommandText = StrSQL
            SQLCmd.ExecuteNonQuery()
            LIExpRtn.StrValue5 = "Key Addedd Successfully."
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try

        Return LIExpRtn
    End Function

    'Note:- Ths Procedure Set Primary Key For Related Tables
    Private Function FSetPrimaryKey(ByVal StrQuery As String, ByVal StrOnTable As String) As LIException
        Dim I As Integer
        Dim StrSQL As String
        Dim StrTemp As String
        Dim DTTemp As DataTable
        Dim SQLCmd As New SqlClient.SqlCommand
        Dim LIExpRtn As New LIException

        Try
            LIExpRtn.StrValue1 = "FSetPrimaryKey"
            LIExpRtn.StrValue2 = StrOnTable
            SQLCmd.CommandTimeout = 0
            SQLCmd.Connection = GCn
            StrTemp = ""
            DTTemp = FillData(StrQuery & "Select OnTable,OnColumn  " & _
                        "From @TmpTable Where ColType='PK' And OnTable='" & StrOnTable & "' " & _
                        "Group By OnTable,OnColumn ", GCn).tables(0)
            For I = 0 To DTTemp.Rows.Count - 1
                If StrTemp = "" Then
                    StrTemp = XNull(DTTemp.Rows(I).Item("OnColumn")) & " ASC "
                Else
                    StrTemp = StrTemp & " , " & XNull(DTTemp.Rows(I).Item("OnColumn")) & " ASC "
                End If
            Next
            LIExpRtn.StrValue3 = StrTemp

            If StrTemp <> "" Then
                StrSQL = "Alter Table " & StrOnTable & " Add Constraint [PK_" & StrOnTable & "] "
                StrSQL = StrSQL & "PRIMARY KEY CLUSTERED "
                StrSQL = StrSQL & "( " & StrTemp & " ) "
                SQLCmd.CommandText = StrSQL
                SQLCmd.ExecuteNonQuery()
            End If
            LIExpRtn.StrValue4 = "Key Added Successfully."
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try

        Return LIExpRtn
    End Function

    'Note:- This Function Searchs For Primary Keys In Array And Make A Temporary Query 
    'And Return That String
    Private Function FCollectPrimaryKey(ByVal StrTableName As String, ByVal ModuleTableCol() As LIColumn) As String
        Dim I As Integer
        Dim StrSQL = ""

        If Not ModuleTableCol Is Nothing Then
            For I = 0 To UBound(ModuleTableCol) - 1
                If ModuleTableCol(I).BlnPrimaryKey Then
                    StrSQL = StrSQL & "Insert Into @TmpTable(ColType,OnColumn,OnTable) Values( "
                    StrSQL = StrSQL & "'PK','" & ModuleTableCol(I).StrName & "','" & StrTableName & "') "
                End If
            Next
        End If
        Return StrSQL
    End Function

    'Note:- This Function Searchs For Foreign Keys In Array And Make A Temporary Query 
    'And Return That String
    Private Function FCollectForeignKey(ByVal StrTableName As String, ByVal ModuleTableFKey() As LIForeignKey) As String
        Dim I As Integer
        Dim StrSQL = ""

        If Not ModuleTableFKey Is Nothing Then
            For I = 0 To UBound(ModuleTableFKey) - 1
                StrSQL = StrSQL & "Insert Into @TmpTable(ColType,OnColumn,OnTable,WithColumn,WithTable) Values( "
                StrSQL = StrSQL & "'FK','" & ModuleTableFKey(I).StrOnColumn & "','" & StrTableName & "', "
                StrSQL = StrSQL & "'" & ModuleTableFKey(I).StrWithColumn & "','" & ModuleTableFKey(I).StrWithTable & "') "
            Next
        End If
        Return StrSQL
    End Function

    'Note:- This Function Create A Table With Temporary Field __Temp__
    Private Function FCreateTable(ByVal ModuleTable As LITable) As LIException
        Dim DTTemp As DataTable
        Dim SQLCmd As New SqlClient.SqlCommand
        Dim LIExpRtn As New LIException

        LIExpRtn.StrValue1 = "FCreateTable"
        LIExpRtn.StrValue2 = ModuleTable.StrName

        Try
            'Note:- Checking That Table Exist Or Not
            DTTemp = FillData("Select Count(*) from SysObjects Where Name='" & ModuleTable.StrName & "' ", GCn).Tables(0)
            If Not DTTemp.Rows(0).Item(0) > 0 Then
                SQLCmd.Connection = GCn
                SQLCmd.CommandText = "Create Table " & ModuleTable.StrName & " (__Temp__ NVarChar(1)) "
                SQLCmd.ExecuteNonQuery()
                LIExpRtn.StrValue3 = "Table Created Successfully."
            Else
                LIExpRtn.StrValue3 = "Table Already Exist."
            End If
            DTTemp.Dispose()
            DTTemp = Nothing
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try

        Return LIExpRtn
    End Function

    'Note:- This Function Add/Alter Column And Set Default Value
    Private Function FCreateColumn(ByVal StrTableName As String, ByVal ModuleTableCol As LIColumn) As LIException
        Dim DTTemp As DataTable
        Dim SQLCmd As New SqlClient.SqlCommand
        Dim StrToDo As String = " Add "
        Dim StrLength As String = ""
        Dim StrCommand As String = ""
        Dim LIExpRtn As New LIException

        LIExpRtn.StrValue1 = "FCreateColumn"
        LIExpRtn.StrValue2 = StrTableName
        LIExpRtn.StrValue3 = ModuleTableCol.StrName

        Try
            'Note:- Checking Wheather To Add Or Alter Column
            DTTemp = FillData("Select Count(*),Max(SC.Name) As ColName,Max(ST.Name) As DType,Max(SC.Length) As Length " & _
               "From SysColumns SC Left Join SysTypes ST On SC.XType=ST.XUserType " & _
               "Where SC.Name='" & ModuleTableCol.StrName & "' And " & _
               "SC.ID In (Select ID from SysObjects Where Name='" & StrTableName & "')", GCn).Tables(0)
            If Not DTTemp.Rows(0).Item(0) > 0 Then
                StrToDo = " Add "
                LIExpRtn.StrValue5 = LIExpRtn.StrValue5 & "Added Column "
            Else
                StrToDo = " Alter Column "
                FValidateColumnDif(LIExpRtn, XNull(DTTemp.Rows(0).Item("DType")), ModuleTableCol.StrDataType, _
                                    VNull(DTTemp.Rows(0).Item("Length")), ModuleTableCol.IntLength)
                LIExpRtn.StrValue5 = LIExpRtn.StrValue5 & "Altered Column "
            End If
            DTTemp.Dispose() : DTTemp = Nothing

            'Note:- Checking Situations
            If ModuleTableCol.IntLength > 0 Then StrLength = " ( " & Trim(ModuleTableCol.IntLength) & " ) "
            If ModuleTableCol.BlnAllowNull Then StrCommand = " Null " Else StrCommand = " Not Null "
            If ModuleTableCol.BlnPrimaryKey Then StrCommand = " Not Null "


            'Note:- Adding/Altering Columns
            SQLCmd.CommandTimeout = 0
            SQLCmd.Connection = GCn
            SQLCmd.CommandText = "Alter Table " & StrTableName & " " & StrToDo & " " & _
                                 ModuleTableCol.StrName & " " & _
                                 ModuleTableCol.StrDataType & " " & _
                                 StrLength & " " & StrCommand
            SQLCmd.ExecuteNonQuery()


            '===================================================================
            '======================= Setting Default Value =====================
            '==============================Start================================
            '===================================================================

            '========= Droping Default Value If Exist ========
            DTTemp = FillData("Select Count(*) From SysColumns " & _
               "Where XType='D' And Name='DF_" & StrTableName & "_" & ModuleTableCol.StrName & "'", GCn).Tables(0)
            If DTTemp.Rows(0).Item(0) > 0 Then
                SQLCmd.CommandText = "Alter Table " & StrTableName & " Drop CONSTRAINT DF_" & StrTableName & "_" & ModuleTableCol.StrName
                SQLCmd.ExecuteNonQuery()
            End If
            DTTemp.Dispose() : DTTemp = Nothing

            If Trim(ModuleTableCol.StrDefaultValue) <> "" Then
                '========= Adding Default Value To Column ========
                SQLCmd.CommandText = "Alter Table " & StrTableName & " ADD CONSTRAINT " & _
                                     "DF_" & StrTableName & "_" & ModuleTableCol.StrName & " " & _
                                     "Default '" & Trim(ModuleTableCol.StrDefaultValue) & "' " & _
                                     "For " & ModuleTableCol.StrName
                SQLCmd.ExecuteNonQuery()
            End If
            '===================================================================
            '======================= Setting Default Value =====================
            '============================== End ================================
            '===================================================================

            LIExpRtn.StrValue5 = LIExpRtn.StrValue5 & " Successfully."
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try

        Return LIExpRtn
    End Function

    Private Sub FValidateColumnDif(ByRef LIExpRtn As LIException, ByVal StrDB_DType As String, _
    ByVal StrMD_DType As String, ByVal IntDB_Length As Integer, _
    ByVal IntMD_Length As Integer)

        If UCase(StrMD_DType) = UCase(FGetType(SQLDataType.VarCharMax)) Then StrMD_DType = FGetType(SQLDataType.VarChar) : IntMD_Length = -1
        If IntDB_Length > 0 Then If UCase(StrDB_DType) = UCase(FGetType(SQLDataType.VarChar)) Then IntDB_Length = IntDB_Length / 2
        If UCase(StrDB_DType) <> UCase(StrMD_DType) Then LIExpRtn.StrValue4 = StrDB_DType & " To " & StrMD_DType

        If IntMD_Length <> 0 Then
            If IntDB_Length <> IntMD_Length Then
                If Trim(LIExpRtn.StrValue4) = "" Then LIExpRtn.StrValue4 = StrMD_DType
                LIExpRtn.StrValue5 = Trim(IntDB_Length) & " To " & Trim(IntMD_Length)
            End If
        End If
    End Sub

    'Note:- Drops Related Column
    Private Function FDropColumn(ByVal StrTableName As String, ByVal StrColName As String) As LIException
        Dim SQLCmd As New SqlClient.SqlCommand
        Dim LIExcpRtn As New LIException
        Dim DTTemp As DataTable

        LIExcpRtn.StrValue1 = "FDropColumn"
        LIExcpRtn.StrValue2 = StrTableName
        LIExcpRtn.StrValue3 = StrColName

        Try
            DTTemp = FillData("Select Count(*) " & _
               "From SysColumns SC  " & _
               "Where SC.Name='" & StrColName & "' And " & _
               "SC.ID In (Select ID from SysObjects Where Name='" & StrTableName & "')", GCn).Tables(0)
            If DTTemp.Rows(0).Item(0) > 0 Then
                SQLCmd.Connection = GCn
                SQLCmd.CommandText = "Alter Table " & StrTableName & " Drop Column " & StrColName
                SQLCmd.ExecuteNonQuery()
                LIExcpRtn.StrValue4 = "Column Dropped Successfully."
            Else
                LIExcpRtn.StrValue4 = "Column Does Not Exist."
            End If
        Catch ex As Exception
            LIExcpRtn.StrMessage = ex.Message
        End Try

        Return LIExcpRtn
    End Function

    'Note:- Sets Column Value To A Specified Array
    Public Sub FSetColumnValue(ByRef ModuleTable() As LITable, ByVal StrColumnName As String, ByVal SQLDataType As SQLDataType, _
    Optional ByVal IntLength As Int16 = 0, Optional ByVal BlnPrimaryKey As Boolean = False, Optional ByVal BlnAllowNull As Boolean = True, _
    Optional ByVal StrDefaultValue As String = "")

        Dim IntTblIndex As Integer
        Dim IntColIndex As Integer
        FAddColumn(ModuleTable)

        IntTblIndex = UBound(ModuleTable) - 1
        IntColIndex = UBound(ModuleTable(UBound(ModuleTable) - 1).ColItem) - 1

        ModuleTable(IntTblIndex).ColItem(IntColIndex).StrName = Trim(StrColumnName)
        ModuleTable(IntTblIndex).ColItem(IntColIndex).StrDataType = FGetType(SQLDataType)
        ModuleTable(IntTblIndex).ColItem(IntColIndex).IntLength = IntLength
        ModuleTable(IntTblIndex).ColItem(IntColIndex).BlnPrimaryKey = BlnPrimaryKey
        ModuleTable(IntTblIndex).ColItem(IntColIndex).BlnAllowNull = BlnAllowNull
        ModuleTable(IntTblIndex).ColItem(IntColIndex).StrDefaultValue = Trim(StrDefaultValue)
    End Sub

    'Note:- Sets Foreign Key For Column To A Specified Array
    Public Sub FSetFKeyValue(ByRef ModuleTable() As LITable, ByVal StrOnColumn As String, ByVal StrWithColumn As String, _
    ByVal StrWithTable As String)
        Dim IntTblIndex As Integer
        Dim IntColIndex As Integer
        FAddFKey(ModuleTable)

        IntTblIndex = UBound(ModuleTable) - 1
        IntColIndex = UBound(ModuleTable(UBound(ModuleTable) - 1).FKey) - 1

        ModuleTable(IntTblIndex).FKey(IntColIndex).StrOnColumn = Trim(Replace(StrOnColumn, " ", ""))
        ModuleTable(IntTblIndex).FKey(IntColIndex).StrWithColumn = Trim(Replace(StrWithColumn, " ", ""))
        ModuleTable(IntTblIndex).FKey(IntColIndex).StrWithTable = Trim(Replace(StrWithTable, " ", ""))
    End Sub

    'Note:- Inserting Row In An Array For Foreign Key
    Private Sub FAddFKey(ByRef ModuleTable() As LITable)
        Dim IntTblIndex As Integer

        IntTblIndex = UBound(ModuleTable) - 1
        If ModuleTable(UBound(ModuleTable) - 1).FKey Is Nothing Then
            ReDim ModuleTable(IntTblIndex).FKey(1)
        Else
            ReDim Preserve ModuleTable(IntTblIndex).FKey(UBound(ModuleTable(IntTblIndex).FKey) + 1)
        End If
    End Sub

    'Note:- Inserting Row In An Array For Columns
    Private Sub FAddColumn(ByRef ModuleTable() As LITable)
        Dim IntTblIndex As Integer

        IntTblIndex = UBound(ModuleTable) - 1
        If ModuleTable(UBound(ModuleTable) - 1).ColItem Is Nothing Then
            ReDim ModuleTable(IntTblIndex).ColItem(1)
        Else
            ReDim Preserve ModuleTable(IntTblIndex).ColItem(UBound(ModuleTable(IntTblIndex).ColItem) + 1)
        End If
    End Sub

    'Note:- Inserting Row In An Array For Table
    Public Sub FAddTable(ByRef ModuleTable() As LITable, ByVal StrTableName As String, ByVal StrModuleName As String)
        If ModuleTable Is Nothing Then
            ReDim ModuleTable(1)
        Else
            ReDim Preserve ModuleTable(UBound(ModuleTable) + 1)
        End If
        ModuleTable(UBound(ModuleTable) - 1).StrName = Trim(StrTableName)
        ModuleTable(UBound(ModuleTable) - 1).StrModuleName = Trim(StrModuleName)
    End Sub

    'Note:- Inserting Row In An Array For Exception
    Public Sub FAddException(ByRef LIExcpAry() As LIException, ByVal LIExcp As LIException)
        If LIExcpAry Is Nothing Then
            ReDim LIExcpAry(1)
        Else
            ReDim Preserve LIExcpAry(UBound(LIExcpAry) + 1)
        End If
        LIExcpAry(UBound(LIExcpAry) - 1) = LIExcp
    End Sub

    Public Function FGetExcpTable(ByVal LIExcpAry() As LIException) As String
        Dim StrRtn As String
        Dim I As Integer

        StrRtn = "Declare @TmpTable As Table(Value1 NVarChar(500),Value2 NVarChar(500), "
        StrRtn = StrRtn & "Value3 NVarChar(500),Value4 NVarChar(500),Value5 NVarChar(500),Msg NVarChar(500)) "
        For I = 0 To UBound(LIExcpAry) - 1
            StrRtn = StrRtn & "Insert Into @TmpTable(Value1,Value2,Value3,Value4,Value5,Msg) Values( "
            StrRtn = StrRtn & "'" & Chk_Quot(LIExcpAry(I).StrValue1) & "','" & Chk_Quot(LIExcpAry(I).StrValue2) & "', "
            StrRtn = StrRtn & "'" & Chk_Quot(LIExcpAry(I).StrValue3) & "','" & Chk_Quot(LIExcpAry(I).StrValue4) & "', "
            StrRtn = StrRtn & "'" & Chk_Quot(LIExcpAry(I).StrValue5) & "','" & Chk_Quot(LIExcpAry(I).StrMessage) & "') "
        Next

        Return StrRtn
    End Function

    Public Function FGetTableStructure(ByVal LIDB() As LITable) As String
        Dim StrRtn As String
        Dim I As Integer
        Dim J As Integer

        StrRtn = "Declare @TmpTable As Table(ModuleName NVarChar(100),TableName NVarChar(100), "
        StrRtn = StrRtn & "ColumnName NVarChar(100),ColDataType NVarChar(50),ColLength NVarChar(5), "
        StrRtn = StrRtn & "PrimaryKey NVarChar(10),AllowNull NVarChar(10),DefaultValue NVarChar(100), "
        StrRtn = StrRtn & "ForeignKey NVarChar(100),WithColumn NVarChar(100),WithTable NVarChar(100)) "

        For I = 0 To UBound(LIDB) - 1
            If Not LIDB(I).ColItem Is Nothing Then
                For J = 0 To UBound(LIDB(I).ColItem) - 1
                    StrRtn = StrRtn & "Insert Into @TmpTable(ModuleName,TableName, "
                    StrRtn = StrRtn & "ColumnName,ColDataType,ColLength,PrimaryKey, "
                    StrRtn = StrRtn & "AllowNull,DefaultValue) Values( "
                    StrRtn = StrRtn & "'" & Chk_Quot(LIDB(I).StrModuleName) & "','" & Chk_Quot(LIDB(I).StrName) & "', "
                    StrRtn = StrRtn & "'" & Chk_Quot(LIDB(I).ColItem(J).StrName) & "','" & Chk_Quot(LIDB(I).ColItem(J).StrDataType) & "', "
                    StrRtn = StrRtn & "'" & Chk_Quot(LIDB(I).ColItem(J).IntLength) & "','" & Chk_Quot(LIDB(I).ColItem(J).BlnPrimaryKey) & "', "
                    StrRtn = StrRtn & "'" & Chk_Quot(LIDB(I).ColItem(J).BlnAllowNull) & "','" & Chk_Quot(LIDB(I).ColItem(J).StrDefaultValue) & "') "
                Next
            End If

            If Not LIDB(I).FKey Is Nothing Then
                For J = 0 To UBound(LIDB(I).FKey) - 1
                    StrRtn = StrRtn & "Insert Into @TmpTable(ModuleName,TableName, "
                    StrRtn = StrRtn & "ForeignKey,WithColumn,WithTable) Values( "
                    StrRtn = StrRtn & "'" & Chk_Quot(LIDB(I).StrModuleName) & "','" & Chk_Quot(LIDB(I).StrName) & "', "
                    StrRtn = StrRtn & "'" & Chk_Quot(LIDB(I).FKey(J).StrOnColumn) & "','" & Chk_Quot(LIDB(I).FKey(J).StrWithColumn) & "', "
                    StrRtn = StrRtn & "'" & Chk_Quot(LIDB(I).FKey(J).StrWithTable) & "') "
                Next
            End If
        Next
        StrRtn = StrRtn & "Select * From @TmpTable "

        Return StrRtn
    End Function

#End Region
End Class
