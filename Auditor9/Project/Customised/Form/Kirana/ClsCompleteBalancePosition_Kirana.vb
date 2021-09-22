Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsCompleteBalancePosition_Kirana

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""


    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4


    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Dim rowReportType As Integer = 0
    Dim rowAsOnDate As Integer = 1
    Dim rowOverDueDays As Integer = 2
    Dim rowSite As Integer = 3
    Dim rowDivision As Integer = 4


    Dim rowBroker As Integer = 5
    Dim rowUpToDate_Payment As Integer = 6
    Dim rowUpToDate_Sales As Integer = 7
    Dim rowRateOfInterest As Integer = 8
    Dim rowGraceDays As Integer = 9

    Private Const ReportType_BrokerWise As String = "Broker Wise"
    Private Const ReportType_BrokerPartyWise As String = "Broker Party Wise"
    Private Const ReportType_BrokerLedger As String = "Broker Ledger"
    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name FROM ItemType "
    Dim mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
    Dim mHelpTagQry$ = "Select Distinct 'o' As Tick, H.Tags as Code, H.Tags as Description  FROM PurchInvoiceDetail H "
    Public Sub Ini_Grid()
        Try
            mQry = "Select '" & ReportType_BrokerWise & "' as Code, '" & ReportType_BrokerWise & "' as Name 
                    Union All Select '" & ReportType_BrokerPartyWise & "' as Code, '" & ReportType_BrokerPartyWise & "' as Name 
                    Union All Select '" & ReportType_BrokerLedger & "' as Code, '" & ReportType_BrokerLedger & "' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, ReportType_BrokerWise,,, 300)
            ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("OverDueDays", "Over Due Days", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", "")
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")

            ReportFrm.CreateHelpGrid("Broker", "Broker", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
            ReportFrm.CreateHelpGrid("UpToDatePayment", "Up To Date Payment", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("UpToDateSales", "Up To Date Sales", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("RateOfInterest", "Rate Of Interest", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", "")
            ReportFrm.CreateHelpGrid("GraceDays", "Grace Days", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", "")

            ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
            ReportFrm.FilterGrid.Rows(rowBroker).Visible = False
            ReportFrm.FilterGrid.Rows(rowUpToDate_Payment).Visible = False
            ReportFrm.FilterGrid.Rows(rowUpToDate_Sales).Visible = False
            ReportFrm.FilterGrid.Rows(rowRateOfInterest).Visible = False
            ReportFrm.FilterGrid.Rows(rowGraceDays).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcCompleteBalancePosition()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcCompleteBalancePosition(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = ReportFrm.FGetText(rowReportType)


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = ReportType_BrokerWise Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = ReportType_BrokerPartyWise
                        mFilterGrid.Item(GFilter, rowBroker).Value = mGridRow.Cells("Broker").Value
                        mFilterGrid.Item(GFilterCode, rowBroker).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = ReportType_BrokerPartyWise Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = ReportType_BrokerLedger
                        mFilterGrid.Item(GFilter, rowBroker).Value = mGridRow.Cells("Broker").Value
                        mFilterGrid.Item(GFilterCode, rowBroker).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    Else
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " And Sg.SubgroupType = '" & SubgroupType.Customer & "'"
            mCondStr = " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowAsOnDate)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SubCode", rowBroker)

            Dim mMainQry As String = ""
            mMainQry = " Select L.DocId, L.SubCode, L.LinkedSubCode, 
                    Sg.Name As SubCodeName, LSg.Name As LinkedSubCodeName, L.AmtDr, L.AmtCr
                    From Ledger L 
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode
                    LEFT JOIN SubGroup LSg On L.LinkedSubCode = LSg.SubCode
                    Where 1=1 " & mCondStr

            If ReportFrm.FGetText(rowReportType) = ReportType_BrokerWise Then
                mQry = "SELECT L.SubCode As SearchCode, Max(L.SubCodeName) AS Broker, 
                    CASE WHEN IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) >= 0 THEN IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) ELSE 0 END AS Dr,
                    CASE WHEN IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) < 0 THEN Abs(IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0)) ELSE 0 END AS Cr
                    FROM (" & mMainQry & ") L 
                    GROUP BY L.SubCode "
            ElseIf ReportFrm.FGetText(rowReportType) = ReportType_BrokerPartyWise Then
                mQry = "SELECT L.SubCode As SearchCode, Max(L.SubCodeName) AS Broker, 
                    Max(L.LinkedSubCodeName) AS Party, 
                    CASE WHEN IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) >= 0 THEN IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) ELSE 0 END AS Dr,
                    CASE WHEN IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) < 0 THEN Abs(IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0)) ELSE 0 END AS Cr
                    FROM (" & mMainQry & ") L 
                    GROUP BY L.SubCode, L.LinkedSubCode "
            ElseIf ReportFrm.FGetText(rowReportType) = ReportType_BrokerLedger Then
                ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
                ReportFrm.FilterGrid.Rows(rowAsOnDate).Visible = False
                ReportFrm.FilterGrid.Rows(rowOverDueDays).Visible = False
                ReportFrm.FilterGrid.Rows(rowSite).Visible = False
                ReportFrm.FilterGrid.Rows(rowDivision).Visible = False

                ReportFrm.FilterGrid.Rows(rowBroker).Visible = True
                ReportFrm.FilterGrid.Rows(rowUpToDate_Payment).Visible = True
                ReportFrm.FilterGrid.Rows(rowUpToDate_Sales).Visible = True
                ReportFrm.FilterGrid.Rows(rowRateOfInterest).Visible = True
                ReportFrm.FilterGrid.Rows(rowGraceDays).Visible = True

                Dim mInvoiceCondStr As String = ""
                mInvoiceCondStr = ReportFrm.GetWhereCondition("H.SaleToParty", rowBroker)
                mInvoiceCondStr = mInvoiceCondStr & " And Vt.NCat = '" & Ncat.SaleInvoice & "'"

                Try
                    mQry = "Drop Table #TempTblCompleteBalancePosition "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Catch ex As Exception
                End Try


                mQry = "Create Temporary Table #TempTblCompleteBalancePosition 
                    (
                        InvoiceDocId nVarchar(21),
                        InvoiceSr Integer,
                        InvoiceDocDate DateTime,
                        InvoiceDocNo nVarchar(21) Collate NoCase,
                        InvoicePartyName nVarchar(250),
                        InvoiceItemName nVarchar(250),  
                        InvoiceBags Float Default 0,  
                        InvoiceQty Float Default 0,  
                        InvoiceAmount Float Default 0,  
                        InvoiceBardana Float Default 0,  
                        InvoiceDr Float Default 0,  
                        InvoiceAge Integer,
                        InvoiceInterest Float Default 0,
                        PaymentDocId nVarchar(21),
                        PaymentDocDate DateTime,  
                        PaymentPartyName nVarchar(250),
                        PaymentAmount Float Default 0,  
                        PaymentAge Integer,
                        PaymentInterest Float Default 0
                    ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                mQry = " Insert Into #TempTblCompleteBalancePosition(InvoiceDocId, InvoiceSr,
                        InvoiceDocDate, InvoiceDocNo, InvoicePartyName, InvoiceItemName, 
                        InvoiceBags, InvoiceQty, InvoiceAmount,
                        InvoiceBardana, InvoiceDr, InvoiceAge, InvoiceInterest)"
                mQry += " SELECT H.DocId As SearchCode, L.Sr As InvoiceSr, H.V_Date AS BillDate, 
                        H.ManualRefNo AS BillNo, Lsg.Name AS Party, 
                        I.Description AS Item, L.Qty As Bags, L.DealQty As Qty,
                        L.Amount, L.AdditionAmount AS Bardana, VLine.NetAmount AS Dr,
                        0 As InvoiceAge, 0 As InvoiceInterest
                        FROM SaleInvoice H 
                        LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        LEFT JOIN Subgroup LSg ON H.LinkedParty = LSg.Subcode
                        LEFT JOIN Item I ON L.Item = I.Code
                        LEFT JOIN (
	                        SELECT L.DocID, Sum(L.Net_Amount) AS NetAmount
	                        FROM SaleInvoiceDetail L 
	                        GROUP BY L.DocID
                        ) AS VLine ON H.DocId = VLine.DocId 
                        Where 1=1 " & mInvoiceCondStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                Dim mPaymentCondStr As String = ""
                mPaymentCondStr = mPaymentCondStr & " And Vt.NCat = '" & Ncat.Payment & "'"
                mPaymentCondStr = ReportFrm.GetWhereCondition("H.SubCode", rowBroker)

                mQry = " Insert Into #TempTblCompleteBalancePosition(PaymentDocId, PaymentDocDate, PaymentPartyName,
                        PaymentAmount, PaymentAge, PaymentInterest) "
                mQry = " SELECT H.DocId As PaymentDocId, H.V_Date As PaymentDocDate, Sg.Name AS PaymentPartyName, 
                        L.Amount As PaymentAmount, 0 AS PaymentAge, 0 AS PaymentInterest
                        FROM LedgerHead H 
                        LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID
                        LEFT JOIN Subgroup Sg ON H.LinkedSubcode = Sg.Subcode 
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        WHERE 1 = 1 " & mPaymentCondStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Select VMain.InvoiceDocDate As Date, Max(VMain.InvoiceDocNo) As BillNo, 
                        Max(VMain.InvoicePartyName) As Party, Max(VMain.InvoiceItemName) As Item,  
                        Max(VMain.InvoiceBags) As Bags, Max(VMain.InvoiceQty) As Qty, 
                        Max(VMain.InvoiceAmount) As Amount, Max(VMain.InvoiceBardana) As Bardana, 
                        Max(VMain.InvoiceDr) As Dr,
                        Max(VMain.InvoiceAge) As Age, Max(VMain.InvoiceInterest) As Interest, 
                        VMain.PaymentDocDate As Date, Max(VMain.PaymentPartyName) As Party, 
                        Max(VMain.PaymentAmount) As Amount, Max(VMain.PaymentAge) As Age, 
                        Max(VMain.PaymentInterest) As Interest
                        From #TempTblCompleteBalancePosition As VMain
                        Group By VMain.InvoiceDocId, VMain.InvoiceSr, VMain.PaymentDocId "
            End If

            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcCompleteBalancePosition"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        Finally
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
                ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
                ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
            Next
        End Try
    End Sub
End Class
