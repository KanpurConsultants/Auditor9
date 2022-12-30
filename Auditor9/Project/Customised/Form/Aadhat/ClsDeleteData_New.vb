Imports AgLibrary.ClsMain.agConstants

Public Class ClsDeleteData_New

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""
    Dim mLogText As String = ""
    Dim mSearchCode As String = ""

    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout
    Dim Connection_Pakka As SQLite.SQLiteConnection

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
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description FROM Tag H "
    Dim mHelpAccountGroupQry$ = "SELECT GroupCode As Code, GroupName FROM AcGroup WHERE GroupName IN ('Sundry Creditors','Sundry Debtors') "
    Dim mHelpAccountQry$ = "SELECT SG.Subcode As Code, SG.Name  
                            FROM Subgroup SG
                            LEFT JOIN AcGroup AG ON AG.GroupCode = SG.GroupCode 
                            WHERE AG.GroupName IN ('Sundry Creditors','Sundry Debtors') "

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
            ReportFrm.CreateHelpGrid("Account Group", "Account Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpAccountGroupQry, "")
            ReportFrm.CreateHelpGrid("Account", "Account", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpAccountQry, "")
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
        Connection_Pakka = New SQLite.SQLiteConnection

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
        If ReportFrm.FGetText(1) = "" Then MsgBox("Account Group is required.", MsgBoxStyle.Information) : Exit Sub

        If MsgBox("Are you sure you want to proceed delete data ?" & vbNewLine & "This will wash selected data.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                mQry = "SELECT H.DocId, H.LinkedSubcode, Vt.NCat, H.V_Date, H.OMSId
                        FROM LedgerHead H 
                        LEFT JOIN Subgroup Sg ON H.LinkedSubcode = Sg.Subcode
                        LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode
                        LEFT JOIN Voucher_Type Vt On h.V_Type = Vt.V_Type
                        WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & "
                        AND H.V_Type IN ('WPS','PS','RS','WRS') 
                        AND Ag.GroupCode = " & ReportFrm.FGetCode(1) & ""
                If (ReportFrm.FGetCode(2) <> "") Then
                    mQry = mQry & " And Sg.SubCode = " & ReportFrm.FGetCode(2) & ""
                End If

                mQry += " UNION ALL "

                mQry += "SELECT L.PurchaseInvoiceDocId As DocId, H.LinkedSubcode, Vt.NCat, H.V_Date, IfNull(IfNull(Si.OMSId,Pi.OMSId),Lh.OMSId) As OMSId
                        FROM LedgerHead H 
                        LEFT JOIN Subgroup Sg ON H.LinkedSubcode = Sg.Subcode
                        LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode
                        LEFT JOIN Cloth_SupplierSettlementInvoices L On H.DocId = L.DocId
                        LEFT JOIN SaleInvoice Si On L.PurchaseInvoiceDocId = Si.DocId
                        LEFT JOIN PurchInvoice Pi On L.PurchaseInvoiceDocId = Pi.DocId
                        LEFT JOIN LedgerHead Lh On L.PurchaseInvoiceDocId = Lh.DocId
                        LEFT JOIN Voucher_Type Vt On IfNull(IfNull(Si.V_Type,Pi.V_Type),Lh.V_Type) = Vt.V_Type
                        WHERE L.PurchaseInvoiceDocId IS NOT NULL
                        AND Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & "
                        AND H.V_Type IN ('WPS','PS','RS','WRS') 
                        AND Ag.GroupCode = " & ReportFrm.FGetCode(1) & ""
                If (ReportFrm.FGetCode(2) <> "") Then
                    mQry = mQry & " And Sg.SubCode = " & ReportFrm.FGetCode(2) & ""
                End If

                mQry += " UNION ALL "

                mQry += "SELECT L.PaymentDocId As DocId, H.LinkedSubcode, Vt.NCat, H.V_Date, IfNull(IfNull(Si.OMSId,Pi.OMSId),Lh.OMSId) As OMSId
                        FROM LedgerHead H 
                        LEFT JOIN Subgroup Sg ON H.LinkedSubcode = Sg.Subcode
                        LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode
                        LEFT JOIN Cloth_SupplierSettlementPayments L On H.DocId = L.DocId
                        LEFT JOIN SaleInvoice Si On L.PaymentDocId = Si.DocId
                        LEFT JOIN PurchInvoice Pi On L.PaymentDocId = Pi.DocId
                        LEFT JOIN LedgerHead Lh On L.PaymentDocId = Lh.DocId
                        LEFT JOIN Voucher_Type Vt On IfNull(IfNull(Si.V_Type,Pi.V_Type),Lh.V_Type) = Vt.V_Type
                        WHERE L.PaymentDocId Is Not Null
                        And Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & "
                        AND H.V_Type IN ('WPS','PS','RS','WRS') 
                        AND Ag.GroupCode = " & ReportFrm.FGetCode(1) & ""
                If (ReportFrm.FGetCode(2) <> "") Then
                    mQry = mQry & " And Sg.SubCode = " & ReportFrm.FGetCode(2) & ""
                End If

                mQry = mQry & " Order By H.V_Date Desc "
                Dim DtSelectedData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To DtSelectedData.Rows.Count - 1
                    If AgL.VNull(AgL.Dman_Execute("Select Count(*) From SaleInvoice Where DocId = '" & DtSelectedData.Rows(I)("DocId") & "'", AgL.GCn).ExecuteScalar()) > 0 Then
                        mQry = " UPDATE SaleInvoice Set IsAlreadyUploaded = 1 Where DocId = '" & DtSelectedData.Rows(I)("OMSId") & "'"
                        AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                        bConStr = " Where DocId = '" & DtSelectedData.Rows(I)("DocId") & "' "
                        FCreateLog("SaleInvoice", bConStr, "")

                        mQry = "DELETE FROM Ledger " & bConStr
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
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
                    End If

                    If AgL.VNull(AgL.Dman_Execute("Select Count(*) From PurchInvoice Where DocId = '" & DtSelectedData.Rows(I)("DocId") & "'", AgL.GCn).ExecuteScalar()) > 0 Then
                        mQry = " UPDATE PurchInvoice Set IsAlreadyUploaded = 1 Where DocId = '" & DtSelectedData.Rows(I)("OMSId") & "'"
                        AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                        bConStr = " Where DocId = '" & DtSelectedData.Rows(I)("DocId") & "' "
                        FCreateLog("PurchInvoice", bConStr, "")

                        mQry = "DELETE FROM Ledger " & bConStr
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
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
                        mQry = "DELETE FROM LedgerHead " & bConStr
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If

                    If AgL.VNull(AgL.Dman_Execute("Select Count(*) From LedgerHead Where DocId = '" & DtSelectedData.Rows(I)("DocId") & "' And LinkedSubcode = '" & DtSelectedData.Rows(I)("LinkedSubcode") & "'", AgL.GCn).ExecuteScalar()) > 0 Then
                        mQry = " UPDATE LedgerHead Set IsAlreadyUploaded = 1 Where DocId = '" & DtSelectedData.Rows(I)("OMSId") & "'"
                        AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                        bConStr = " Where DocId = '" & DtSelectedData.Rows(I)("DocId") & "' "
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
                    End If

                    If AgL.VNull(AgL.Dman_Execute("Select Count(*) From Ledger Where DocId = '" & DtSelectedData.Rows(I)("DocId") & "' And LinkedSubcode = '" & DtSelectedData.Rows(I)("LinkedSubcode") & "'", AgL.GCn).ExecuteScalar()) > 0 Then
                        FDeleteLedgerHead(AgL.XNull(DtSelectedData.Rows(I)("DocId")),
                                AgL.XNull(DtSelectedData.Rows(I)("LinkedSubcode")),
                                AgL.XNull(DtSelectedData.Rows(I)("NCat")),
                                AgL.XNull(DtSelectedData.Rows(I)("OMSId")))
                    End If
                Next

                Dim bErrorRaiseDueToDelete As String = ""
                For I As Integer = 0 To DtSelectedData.Rows.Count - 1
                    If AgL.VNull(AgL.Dman_Execute("Select Count(*) From Ledger Where DocId = '" & DtSelectedData.Rows(I)("DocId") & "' And LinkedSubcode = '" & DtSelectedData.Rows(I)("LinkedSubcode") & "'", AgL.GCn).ExecuteScalar()) > 0 Then
                        bErrorRaiseDueToDelete += " Unable To Delete " + AgL.XNull(DtSelectedData.Rows(I)("DocId")) + vbCrLf
                    End If
                Next

                mQry = "SELECT L.DocId, IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) AS Diff
                        FROM Ledger L 
                        WHERE L.V_Type NOT IN ('OB','WOB')
                        And L.DocId Not In ('D1    RC 2019       1', 'D1   WRS 2019      52', 'D1   WRS 2019     124', 'D1   WRS 2019     126', 'D1   WRS 2019     127', 'D1   WRS 2020      65', 'D1   WRS 2020     167')
                        AND L.DocId In ( SELECT H.DocId
                        FROM Ledger H
                        LEFT JOIN Subgroup Sg ON H.LinkedSubcode = Sg.Subcode
                        LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode
                        Where 1=1 AND Ag.GroupCode = " & ReportFrm.FGetCode(1) & ""
                If (ReportFrm.FGetCode(2) <> "") Then
                    mQry = mQry & " And Sg.SubCode = " & ReportFrm.FGetCode(2) & ""
                End If

                mQry = mQry & " GROUP BY H.DocId ) GROUP BY L.DocId
                        HAVING IfNull(Sum(L.AmtDr),0) - IfNull(Sum(L.AmtCr),0) > 0.1"
                Dim DtDiffDocIds As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To DtDiffDocIds.Rows.Count - 1
                    bErrorRaiseDueToDelete += " Difference Found In " + AgL.XNull(DtDiffDocIds.Rows(I)("DocId")) + vbCrLf
                Next

                If bErrorRaiseDueToDelete <> "" Then
                    Err.Raise(1, "", bErrorRaiseDueToDelete)
                End If

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
    Private Sub FDeleteLedgerHead(bDocId As String, bSubCode As String, bNCat As String, bOMSDocId As String)
        Dim DtLedgerForSubCode As DataTable
        Dim DtLedgerTotal As DataTable
        Dim mDeleteFullEntry As Boolean = False

        mQry = " Select IfNull(Sum(AmtDr),0) As AmtDrTotal, 
                IfNull(Sum(AmtCr),0) As AmtCrTotal
                From Ledger 
                Where DocId = '" & bDocId & "' "
        DtLedgerTotal = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select IfNull(Sum(AmtDr),0) As AmtDr, 
                IfNull(Sum(AmtCr),0) As AmtCr
                From Ledger 
                Where DocId = '" & bDocId & "'
                And LinkedSubcode = '" & bSubCode & "'"
        DtLedgerForSubCode = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If AgL.VNull(DtLedgerForSubCode.Rows(0)("AmtDr")) = DtLedgerTotal.Rows(0)("AmtCrTotal") Then
            mDeleteFullEntry = True
        End If

        If AgL.VNull(DtLedgerForSubCode.Rows(0)("AmtCr")) = DtLedgerTotal.Rows(0)("AmtDrTotal") Then
            mDeleteFullEntry = True
        End If

        If AgL.VNull(AgL.Dman_Execute(" Select Count(Distinct LinkedSubcode) 
                    From Ledger 
                    Where DocId = '" & AgL.XNull(bDocId) & "'", AgL.GCn).ExecuteScalar()) = 1 Then
            mDeleteFullEntry = True
        End If

        If mDeleteFullEntry = True Then
            mQry = " UPDATE LedgerHead Set IsAlreadyUploaded = 1 Where DocId = '" & bOMSDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

            mQry = "DELETE FROM Ledger Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerAdj " & " Where Vr_DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerHeadCharges Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerHeadDetail Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerHeadDetailCharges Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerHeadDetailChequePrinting Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerItemAdj Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerM Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerHead Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            If bNCat <> Ncat.JournalVoucher Then
                If AgL.VNull(AgL.Dman_Execute(" Select Count(*) 
                            From LedgerHeadDetail 
                            Where DocId = '" & bDocId & "'", AgL.GCn).ExecuteScalar()) <> 0 Then
                    mQry = " Delete From LedgerHeadDetail 
                            Where DocId = '" & bDocId & "'
                            And LinkedSubcode = '" & bSubCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = " Delete From Ledger Where DocId = '" & bDocId & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    FrmVoucherEntry.FGetCalculationData(bDocId, AgL.GCn, AgL.ECmd)
                End If
            Else
                mQry = "Select * From LedgerHeadDetail L
                        Where L.DocId = '" & bDocId & "' 
                        And L.LinkedSubcode = '" & bSubCode & "'"
                Dim DtLinkedSubCodeData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                For I As Integer = 0 To DtLinkedSubCodeData.Rows.Count - 1
                    Dim bConStr As String = ""
                    If AgL.VNull(DtLinkedSubCodeData.Rows(I)("Amount")) > 0 Then
                        bConStr = " And IfNull(L.AmountCr,0) = " & AgL.VNull(DtLinkedSubCodeData.Rows(I)("Amount")) & " "
                    ElseIf AgL.VNull(DtLinkedSubCodeData.Rows(I)("AmountCr")) > 0 Then
                        bConStr = " And IfNull(L.Amount,0) = " & AgL.VNull(DtLinkedSubCodeData.Rows(I)("AmountCr")) & " "
                    End If
                    mQry = "Select * From LedgerHeadDetail L 
                        Where L.DocId = '" & bDocId & "' 
                        And L.LinkedSubcode <> '" & bSubCode & "' " & bConStr
                    Dim DtBankData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtBankData.Rows.Count = 1 Then
                        mQry = " Delete From LedgerHeadDetail Where DocId = '" & bDocId & "'
                            And Sr In (" & DtLinkedSubCodeData.Rows(I)("Sr") & ", " & DtBankData.Rows(0)("Sr") & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " Delete From Ledger Where DocId = '" & bDocId & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        FrmVoucherEntry.FGetCalculationData(bDocId, AgL.GCn, AgL.ECmd)
                    End If
                Next
            End If
        End If
        'mQry = " Select L.DocId, L.V_Type || '-' || L.RecId As RecId, Sg.Name As PartyName  
        '            From Ledger L
        '            LEFT JOIN SubGroup Sg On L.LinkedSubcode = Sg.SubCode
        '            Where L.LinkedSubcode = '" & SubCode & "'
        '            And Date(L.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ""
        'Dim DtPendingLedgerEntries As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        'Dim UnableToDelete As String = ""
        'For I As Integer = 0 To DtPendingLedgerEntries.Rows.Count - 1
        '    UnableToDelete += "Unable To Delete Entry " & AgL.XNull(DtPendingLedgerEntries.Rows(I)("RecId")) & " For Party " & AgL.XNull(DtPendingLedgerEntries.Rows(I)("PartyName"))
        'Next
        'If UnableToDelete <> "" Then
        '    Err.Raise(1, "", UnableToDelete)
        'End If
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
