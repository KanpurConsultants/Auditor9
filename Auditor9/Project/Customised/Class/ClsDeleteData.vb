Imports AgLibrary.ClsMain.agConstants

Public Class ClsDeleteData

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""
    Dim mLogText As String = ""
    Dim mSearchCode As String = ""

    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

    'For Deleting All Data
    'mQry = "Delete  From  SaleInvoiceDimensionDetailSku ;
    '        Delete  From  SaleInvoiceDimensionDetail ;
    '        Delete  From  SaleInvoiceDetailSku;
    '        Delete  From  SaleInvoiceDetail ;
    '        Delete  From  SaleInvoiceGeneratedEntries ;
    '        Delete  From  SaleInvoiceLastTransactionValues ;
    '        Delete  From  SaleInvoicePayment ;
    '        Delete  From  SaleInvoiceReferences;
    '        Delete  From  SaleInvoiceTransport ;
    '        Delete  From  SaleInvoiceTrnSetting ;
    '        Delete  From  SaleInvoiceBarcodeLastTransactionValues ;
    '        Delete  From  SaleInvoiceDetailBarCodeValues ;
    '        Delete  From  SaleInvoiceDetailHelpValues ;
    '        Delete  From  SaleInvoice ;


    '        Delete  From  PurchInvoiceDimensionDetailSku;
    '        Delete  From  PurchInvoiceDimensionDetail ;
    '        Delete  From  PurchInvoiceDetailSku ;
    '        Delete  From  PurchInvoiceDetail ;
    '        Delete  From  PurchInvoiceTransport ;
    '        Delete  From  PurchInvoice ;


    '        Delete  From  Cloth_SupplierSettlementInvoices ;
    '        Delete  From  Cloth_SupplierSettlementInvoicesAdjustment ;
    '        Delete  From  Cloth_SupplierSettlementInvoicesLine ;
    '        Delete  From  Cloth_SupplierSettlementPayments ;


    '        Delete  From  ItemGroupPerson ;
    '        Delete  From  LogTable ;

    '        Delete  From  LedgerHeadDetailCharges ;
    '        Delete  From  LedgerHeadDetail ;
    '        Delete  From  Ledger ;
    '        Delete  From  LedgerAdj ;
    '        Delete  From  LedgerHeadCharges ;
    '        Delete  From  LedgerM ;
    '        Delete  From  LedgerHead ;


    '        Delete  From  StockHeadDimensionDetailSku ;
    '        Delete  From  StockHeadDimensionDetail ;
    '        Delete  From  StockHeadDetailBomSku;
    '        Delete  From  StockHeadDetailBom ;
    '        Delete  From  StockHeadDetailTransfer ;
    '        Delete  From  StockHeadDetailSku;
    '        Delete  From  StockHeadDetail ;
    '        Delete  From  StockHeadDetailBarCodeValues ;
    '        Delete  From  StockHeadDetailBase ;
    '        Delete  From  Stock;
    '        Delete  From  StockProcess;
    '        Delete  From  StockAdj ;
    '        Delete  From  StockHeadTransfer ;
    '        Delete  From  StockHeadTransport ;
    '        Delete  From  StockHead ;


    '        Delete  From  TransactionReferences ;
    '        Delete  From  WLedgerHeadDetail ;
    '        Delete  From  WPurchInvoiceDetail; 
    '        Delete  From  WSaleInvoiceDetail;"



    Dim mHelpAreaQry$ = "Select 'o' As Tick, Code, Description From Area "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
    'Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division Where Div_Code In (" & AgL.PubDivisionList & ") "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name From ItemType "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpVendorQry$ = " Select 'o' As Tick,  H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpTableQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM HT_Table H "
    Dim mHelpPaymentModeQry$ = "Select 'o' As Tick, 'Cash' As Code, 'Cash' As Description " &
                                " UNION ALL " &
                                " Select 'o' As Tick, 'Credit' As Code, 'Credit' As Description "
    Dim mHelpOutletQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM Outlet H "
    Dim mHelpStewardQry$ = "Select 'o' As Tick,  Sg.SubCode AS Code, Sg.DispName AS Steward FROM SubGroup Sg  "
    Dim mHelpPartyQry$ = " Select Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  
                            Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode 
                            Where Sg.Nature In ('Customer','Supplier','Cash') 
                            And Sg.SubGroupType = 'Master Customer'"
    Dim mHelpAgentQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpYesNo$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpSaleOrderQry$ = " Select 'o' As Tick,  H.DocID AS Code, H.V_Type || '-' || H.ManualRefNo  FROM SaleOrder H "
    Dim mHelpSaleBillQry$ = " SELECT 'o' As Tick,DocId, ReferenceNo AS BillNo, V_Date AS Date FROM SaleChallan "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick,I.Code,I.Description  AS ItemReportingGroup FROM ItemReportingGroup I "
    Dim mHelpSalesRepresentativeQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Sales Representative] FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code ='SREP' "
    Dim mHelpResponsiblePersonQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code <>'SREP' "
    Dim mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpDimension1Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension1 & "' Order By Specification "
    Dim mHelpDimension2Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension2 & "' Order By Specification "
    Dim mHelpDimension3Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension3 & "' Order By Specification "
    Dim mHelpDimension4Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension4 & "' Order By Specification "
    Dim mHelpSingleDimension1Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension1 & "' Order By Specification "
    Dim mHelpSingleDimension2Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension2 & "' Order By Specification "
    Dim mHelpSingleDimension3Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension3 & "' Order By Specification "
    Dim mHelpSingleDimension4Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension4 & "' Order By Specification "
    Dim mHelpSingleProcessQry$ = "Select Subcode as Code, Name From Subgroup Where SubgroupType = '" & SubgroupType.Process & "' Order By Name "
    Dim mHelpSingleJobProcessQry$ = "Select Subcode as Code, Name From Subgroup Where SubgroupType = '" & SubgroupType.Process & "' And Subcode Not In ('" & Process.Sales & "', '" & Process.Purchase & "', '" & Process.Stock & "')  Order By Name "
    Dim mHelpSizeQry$ = "Select 'o' As Tick, Code, Description As Name From Item Where V_Type = '" & ItemV_Type.SIZE & "' Order By Specification "
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description   FROM Tag H "


    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""

    Private Const rowAsOnDate As Integer = 0
    Private Const rowParty As Integer = 1
    Private Const rowDivision As Integer = 2
    Private Const rowSite As Integer = 3
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Account Group", "Account Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpPartyQry, "", 600, 800, 400)
            ReportFrm.BtnPrint.Text = "Delete"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcDeleteData()
    End Sub
    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub
    Private Sub ProcDeleteData()
        Dim mTrans As String
        Dim bConStr$ = ""
        Dim bOMSIdConStr$ = ""
        Dim Connection_Pakka As New SQLite.SQLiteConnection
        Dim mDbPath As String = ""
        Dim mDbEncryption As String = ""

        mLogText = ""
        mSearchCode = AgL.GetGUID(AgL.GCn)

        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        mDbEncryption = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Encryption", "")
        If mDbEncryption = "N" Then
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
        Else
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection_Pakka.Open()


        If ReportFrm.FGetText(0) = "" Then MsgBox("As On Date is required.", MsgBoxStyle.Information) : Exit Sub
        If ReportFrm.FGetText(1) = "" Then MsgBox("Party is required.", MsgBoxStyle.Information) : Exit Sub

        If MsgBox("Are you sure you want to proceed delete data ?" & vbNewLine & "This will wash selected data.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                '''''''''''''For Updating Updaload in Pakka Databsae''''''''''''

                bOMSIdConStr = " Where DocId In (SELECT H.OMSId
                            FROM SaleInvoice H 
                            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                            WHERE Vt.NCat = '" & Ncat.SaleInvoice & "' 
                            And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.BillToParty", 1) & ")"

                mQry = " UPDATE SaleInvoice Set IsAlreadyUploaded = 1 " & bOMSIdConStr
                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                bOMSIdConStr = " Where DocId In (SELECT H.OMSId
                            FROM PurchInvoice H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.BillToParty", 1) & ")"

                mQry = " UPDATE PurchInvoice Set IsAlreadyUploaded = 1 " & bOMSIdConStr
                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                bOMSIdConStr = " Where DocId In (SELECT H.OMSId
                            FROM LedgerHead H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.LinkedSubcode", 1) & ")"

                mQry = " UPDATE LedgerHead Set IsAlreadyUploaded = 1 " & bOMSIdConStr
                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                '''''''''''''End For Updating Updaload in Pakka Databsae''''''''''''


                bConStr = " Where DocId In (SELECT H.DocID
                            FROM SaleInvoice H 
                            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                            WHERE Vt.NCat = '" & Ncat.SaleInvoice & "' 
                            And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) &
                            ReportFrm.GetWhereCondition("H.BillToParty", 1) & ")"

                FCreateLog("SaleInvoice", bConStr, "")

                mQry = "DELETE FROM SaleInvoiceBarcodeLastTransactionValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailBarCodeValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailHelpValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDimensionDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDimensionDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceGeneratedEntries " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoicePayment " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceReferences " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoice " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                bConStr = " Where DocId In (SELECT H.DocID
                            FROM PurchInvoice H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) &
                            ReportFrm.GetWhereCondition("H.BillToParty", 1) & ")"

                FCreateLog("PurchInvoice", bConStr, "")

                mQry = "DELETE FROM PurchInvoiceBarcodeLastTransactionValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBarCodeValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBom " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBomSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailHelpValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDimensionDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDimensionDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceTransport " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoice " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                bConStr = " Where DocId In (SELECT H.DocID
                            FROM LedgerHead H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.LinkedSubcode", 1) & ")"

                FCreateLog("LedgerHead", bConStr, "")

                mQry = "DELETE FROM Ledger " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerAdj " & " Where Vr_DocId In (SELECT H.DocID
                            FROM LedgerHead H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.LinkedSubcode", 1) & ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadCharges " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetailCharges " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetailChequePrinting " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerItemAdj " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerM " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHead " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                mQry = " SELECT H.DocID, H.V_Type || '-' || H.RecId As DocNo, H.V_Date As DocDate
                            FROM Ledger H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.LinkedSubcode", 1) & ""
                FCreateLog("", "", mQry)

                FDeleteLedgerHead()

                Call AgL.LogTableEntry(mSearchCode, ReportFrm.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), AgL.GCn, AgL.ECmd,,,,,, AgL.PubSiteCode, AgL.PubDivCode, mLogText)

                AgL.ETrans.Commit()
                mTrans = "Commit"
                MsgBox("Process Complete.", MsgBoxStyle.Information)
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub FDeleteLedgerHead()
        Dim DtParties As DataTable
        Dim DtLedger As DataTable
        Dim DtLedgerTotal As DataTable
        Dim mDeleteFullEntry As Boolean = False

        mQry = " Select * From SubGroup Sg Where 1=1 "
        mQry += ReportFrm.GetWhereCondition("SubCode", 1)
        DtParties = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For P As Integer = 0 To DtParties.Rows.Count - 1
            mQry = " Select L.DocId, L.LinkedSubcode, Sum(L.AmtDr) As AmtDr, 
                    Sum(L.AmtCr) As AmtCr, Max(Vt.NCat) As NCat 
                    From Ledger L
                    LEFT JOIN Voucher_Type Vt On L.V_Type = Vt.V_Type
                    Where L.LinkedSubcode = '" & DtParties.Rows(P)("SubCode") & "' 
                    And Date(L.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) &
                    " Group By L.DocId, L.LinkedSubcode "
            DtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I As Integer = 0 To DtLedger.Rows.Count - 1
                mQry = " Select IfNull(Sum(AmtDr),0) As AmtDrTotal, 
                        IfNull(Sum(AmtCr),0) As AmtCrTotal
                        From Ledger 
                        Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "' "
                DtLedgerTotal = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If AgL.VNull(DtLedger.Rows(I)("AmtDr")) = DtLedgerTotal.Rows(0)("AmtCrTotal") Then
                    mDeleteFullEntry = True
                End If

                If AgL.VNull(DtLedger.Rows(I)("AmtCr")) = DtLedgerTotal.Rows(0)("AmtDrTotal") Then
                    mDeleteFullEntry = True
                End If

                If AgL.VNull(AgL.Dman_Execute(" Select Count(Distinct LinkedSubcode) From Ledger 
                    Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'", AgL.GCn).ExecuteScalar()) = 1 Then
                    mDeleteFullEntry = True
                End If

                If mDeleteFullEntry = True Then
                    mQry = "DELETE FROM Ledger Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerAdj " & " Where Vr_DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHeadCharges Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHeadDetail Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHeadDetailCharges Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHeadDetailChequePrinting Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerItemAdj Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerM Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHead Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    If AgL.XNull(DtLedger.Rows(I)("NCat")) <> Ncat.JournalVoucher Then
                        If AgL.VNull(AgL.Dman_Execute(" Select Count(*) 
                                From LedgerHeadDetail 
                                Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'", AgL.GCn).ExecuteScalar()) <> 0 Then
                            mQry = " Delete From LedgerHeadDetail 
                                Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'
                                And LinkedSubcode = '" & AgL.XNull(DtLedger.Rows(I)("LinkedSubcode")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            FrmVoucherEntry.FGetCalculationData(AgL.XNull(DtLedger.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
                        End If
                    End If
                End If
            Next
            mQry = " Select L.DocId, L.V_Type || '-' || L.RecId As RecId, Sg.Name As PartyName  
                    From Ledger L
                    LEFT JOIN SubGroup Sg On L.LinkedSubcode = Sg.SubCode
                    Where L.LinkedSubcode = '" & DtParties.Rows(P)("Subcode") & "'
                    And Date(L.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ""
            Dim DtPendingLedgerEntries As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dim UnableToDelete As String = ""
            For I As Integer = 0 To DtPendingLedgerEntries.Rows.Count - 1
                UnableToDelete += "Unable To Delete Entry " & AgL.XNull(DtPendingLedgerEntries.Rows(I)("RecId")) & " For Party " & AgL.XNull(DtPendingLedgerEntries.Rows(I)("PartyName"))
            Next
            If UnableToDelete <> "" Then
                Err.Raise(1, "", UnableToDelete)
            End If
        Next
    End Sub
    Private Sub FCreateLog(bTable As String, bConStr As String, bQry As String)
        If mLogText = "" Then
            mLogText += " As On Date : " & ReportFrm.FGetText(0) & vbCrLf
            mLogText += " Party : " & ReportFrm.FGetText(1) & vbCrLf
        End If

        If bQry <> "" Then
            mQry = bQry
        Else
            mQry = " Select DocId, ManualRefNo As DocNo, V_Date As DocDate From " & bTable & bConStr
        End If
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            mLogText += " Affected Document DocId : " & AgL.XNull(DtTemp.Rows(I)("DocId")) & ", Doc No : " & AgL.XNull(DtTemp.Rows(I)("DocNo")) & ", Doc Date : " & AgL.XNull(DtTemp.Rows(I)("DocDate")) & vbCrLf
        Next
    End Sub
End Class
