Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsSadhviBranchStatus

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""
    Dim isSaleAdjusted As Boolean = False


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
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowSite As Integer = 3
    Dim rowDivision As Integer = 4
    Dim rowAccount As Integer = 5
    Dim rowAccountGroup As Integer = 6
    Dim rowFormat As Integer = 7

    Private Const Format_Actual = "Format Actual"
    Private Const Format_WithEstimatedTax = "Format With Estimated Tax"

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property
    Public Property ShowReportType() As String
        Get
            ShowReportType = mShowReportType
        End Get
        Set(ByVal value As String)
            mShowReportType = value
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
            mQry = "Select 'Summary' as Code, 'Summary' as Name 
                    Union All Select 'Detail' as Code, 'Detail' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Group Wise Summary",,, 300)
            ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthStartDate(AgL.PubLoginDate))
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthEndDate(AgL.PubLoginDate))
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Account", "Account", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "",,, 300)
            ReportFrm.FilterGrid.Rows(rowAccount).Visible = False
            ReportFrm.CreateHelpGrid("Account Group", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "",,, 300)
            ReportFrm.FilterGrid.Rows(rowAccountGroup).Visible = False
            mQry = "Select '" & Format_Actual & "' as Code, '" & Format_Actual & "' as Name 
                    Union All 
                    Select '" & Format_WithEstimatedTax & "' as Code, '" & Format_WithEstimatedTax & "' as Name "
            ReportFrm.CreateHelpGrid("Format", "Format", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, Format_Actual,,, 300)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcMain()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcMain(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)

        If AgL.PubSiteCode = "1" Then
            MsgBox("This report can not be processed at HO ")
            Exit Sub
        End If

        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            If isSaleAdjusted = False Then
                ClsMain.FifoAdjustSale(True)
                isSaleAdjusted = True
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Account Wise Summary"
                        mFilterGrid.Item(GFilter, rowAccountGroup).Value = mGridRow.Cells("Group Name").Value
                        mFilterGrid.Item(GFilterCode, rowAccountGroup).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Account Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Detail"
                        Select Case ReportFrm.FGetText(rowAccountGroup).ToString.ToUpper
                            Case "Rate Difference".ToUpper, "Stock".ToUpper
                                mFilterGrid.Item(GFilter, rowAccount).Value = mGridRow.Cells("Item Name").Value
                                mFilterGrid.Item(GFilterCode, rowAccount).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                            Case Else
                                mFilterGrid.Item(GFilter, rowAccount).Value = mGridRow.Cells("Account Name").Value
                                mFilterGrid.Item(GFilterCode, rowAccount).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        End Select
                        If ReportFrm.FGetText(rowAccountGroup).ToString.ToUpper = "" Then
                        Else
                        End If
                    Else
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " And Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'")




            Dim mQrySalesCashBank As String
            Dim mQryTaxCollected As String
            Dim mQryPurchase As String

            mQryPurchase = "SELECT L.DocId, L.V_Type, strftime('%d/%m/%Y', IfNull(L.EffectiveDate,L.V_Date)) As V_Date1, 
                    IfNull(L.EffectiveDate,L.V_Date) As V_Date, 
                    L.RecID as ManualRefNo, L.Subcode, Sg.Name as AccountName, 
                    Ag.GroupName as GroupName, 
                    L.AmtDr, L.AmtCr, L.Narration,
                    0 AS Qty, 0 AS PurchRate, 0 AS SaleRate, 0 AS RateDiff                                        
                    FROM Ledger L
                    LEFT JOIN viewHelpSubgroup Sg On L.Subcode = Sg.Code
                    Left Join AcGroup AG On Sg.GroupCode = Ag.GroupCode                    
                    Left Join 
                    WHERE Sg.Nature In ('Supplier') "

            mQryPurchase = mQryPurchase & " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mQryPurchase = mQryPurchase & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mQryPurchase = mQryPurchase & Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'")

            mQrySalesCashBank = "SELECT L.DocId, L.V_Type, strftime('%d/%m/%Y', IfNull(L.EffectiveDate,L.V_Date)) As V_Date1, 
                    IfNull(L.EffectiveDate,L.V_Date) As V_Date, 
                    L.RecID as ManualRefNo, L.Subcode, Sg.Name as AccountName, 
                    Ag.GroupName as GroupName, 
                    L.AmtDr, L.AmtCr, L.Narration,
                    0 AS Qty, 0 AS PurchRate, 0 AS SaleRate, 0 AS RateDiff                                        
                    FROM Ledger L
                    LEFT JOIN viewHelpSubgroup Sg On L.Subcode = Sg.Code
                    Left Join AcGroup AG On Sg.GroupCode = Ag.GroupCode                    
                    WHERE Sg.Nature In ('Cash','Bank','Customer','Supplier') "

            mQrySalesCashBank = mQrySalesCashBank & " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mQrySalesCashBank = mQrySalesCashBank & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mQrySalesCashBank = mQrySalesCashBank & Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'")

            If ReportFrm.FGetText(rowFormat) = Format_WithEstimatedTax Then
                mQrySalesCashBank = mQrySalesCashBank & " UNION ALL "
                mQrySalesCashBank = mQrySalesCashBank & " SELECT L.DocId, H.V_Type, H.V_Date As V_Date1, 
                        H.V_Date As V_Date, H.ManualRefNo, H.Vendor, Sg.Name as AccountName, 
                        Ag.GroupName as GroupName, 
                        0 AS AmtDr,
                        CASE WHEN L.Rate <= 1000 THEN L.Amount * 5/100 
	                         WHEN L.Rate > 1000 THEN L.Amount * 5/100 
	                         ELSE 0 END AS AmtCr, L.Remark AS Narration,
                        0 AS Qty, 0 AS PurchRate, 0 AS SaleRate, 0 AS RateDiff
                        FROM PurchInvoice H
                        LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                        LEFT JOIN viewHelpSubgroup Sg On H.Vendor = Sg.Code
                        LEFT JOIN AcGroup AG On Sg.GroupCode = Ag.GroupCode                    
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        WHERE Vt.NCat IN ('PI','PR') "
                mQrySalesCashBank = mQrySalesCashBank & " And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
                mQrySalesCashBank = mQrySalesCashBank & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
                mQrySalesCashBank = mQrySalesCashBank & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            End If


            mQryTaxCollected = mQryTaxCollected & " SELECT L.DocId, L.V_Type, strftime('%d/%m/%Y', L.V_Date) As V_Date1, 
                    L.V_Date, 
                    L.ManualRefNo, L.SaleToParty Subcode, Sg.Name as AccountName, 
                    'Tax Collected' as GroupName, 
                    0 as AmtDr, (L.Tax1 + L.Tax2 + L.Tax3 + L.Tax4 + L.Tax5) as AmtCr, L.Remarks Narration                    
                    FROM SaleInvoice L
                    Left Join viewHelpSubgroup Sg On L.SaleToParty = Sg.Code
                    Where 1=1 "
                mQryTaxCollected = mQryTaxCollected & " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
                mQryTaxCollected = mQryTaxCollected & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
                mQryTaxCollected = mQryTaxCollected & Replace(ReportFrm.GetWhereCondition("L.Div_Code", rowDivision), "''", "'")

            Dim mQryRateDifference As String

            mQryRateDifference = mQryRateDifference & "Select SI.DocId, SI.V_Type, strftime('%d/%m/%Y', SI.V_Date) As V_Date1, 
                    SI.V_Date As V_Date, 
                    SI.ManualRefNo, sid.Item as Itemcode, I.Description as ItemName, 
                    'Rate Difference' as GroupName,                     
                    sa.AdjQty AS Qty, sid.Rate AS PurchRate, pid.Rate AS SaleRate, (sa.AdjQty*Pid.Rate) as PurchAmt, (sa.AdjQty*sid.Rate) as SaleAmt, (sa.AdjQty*Pid.Rate)-(sa.AdjQty*sid.Rate)  AS RateDiff                    
                    FROM StockAdj sa 
                    LEFT JOIN SaleInvoiceDetail sid ON sa.StockOutDocID = sid.DocID AND sa.StockOutTSr = sid.Sr 
                    Left Join Item I On sid.Item = I.Code
                    LEFT JOIN SaleInvoice SI ON sid.DocID = SI.DocID 
                    LEFT JOIN PurchInvoiceDetail pid ON sa.StockInDocID = pid.DocID AND sa.StockInTSr = pid.Sr 
                    WHERE sid.Rate <> pid.Rate "
            mQryRateDifference = mQryRateDifference & Replace(ReportFrm.GetWhereCondition("SI.Site_Code", rowSite), "''", "'")
            mQryRateDifference = mQryRateDifference & Replace(ReportFrm.GetWhereCondition("SI.Div_Code", rowDivision), "''", "'")

            Dim mQryStock As String

            mQryStock = "SELECT L.DocID, L.V_Type, L.V_Date,                     
                    L.RecID as ManualRefNo, L.Item as Itemcode, I.Description as ItemName, 
                    'Stock' as GroupName, 
                    L.Qty_Rec*Ibr.PurchaseRate AS AmtDr, L.Qty_Iss*Ibr.PurchaseRate AS AmtCr, '' Narration,
                    L.Qty_Rec, L.Qty_Iss, L.Qty_Rec-L.Qty_Iss AS Qty, Ibr.PurchaseRate AS PurchRate, I.Rate AS SaleRate, 0 AS RateDiff                                        
                    FROM Stock L
                    Left join Item I On L.Item = I.Code
                    Left Join Item IG On I.ItemGroup = IG.Code
                    Left Join Item IC On I.ItemCategory = IC.Code
                    LEFT JOIN ItemBranchRate Ibr On I.Code = Ibr.Code
                    WHERE 1=1 "
            mQryStock = mQryStock & " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mQryStock = mQryStock & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mQryStock = mQryStock & Replace(ReportFrm.GetWhereCondition("L.Div_Code", rowDivision), "''", "'")
            mQryStock = mQryStock & " And I.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "','" & ItemTypeCode.InternalProduct & "')"

            If ReportFrm.FGetText(rowFormat) = Format_WithEstimatedTax Then
                mQryStock = mQryStock & " UNION ALL "
                mQryStock = mQryStock & " SELECT L.DocID, L.V_Type, L.V_Date,                     
                    L.RecID as ManualRefNo, L.Item as Itemcode, I.Description as ItemName, 
                    'Stock' as GroupName, 
                    CASE WHEN Ibr.PurchaseRate <= 1000 THEN L.Qty_Rec * Ibr.PurchaseRate * 5/100 
	                     WHEN Ibr.PurchaseRate > 1000 THEN L.Qty_Rec * Ibr.PurchaseRate * 5/100
	                     ELSE 0 END AS AmtDr, 
                    CASE WHEN Ibr.PurchaseRate <= 1000 THEN L.Qty_Iss * Ibr.PurchaseRate * 5/100 
	                     WHEN Ibr.PurchaseRate > 1000 THEN L.Qty_Iss * Ibr.PurchaseRate * 5/100
	                     ELSE 0 END AS AmtCr, '' Narration,
                    0 AS Qty_Rec, 0 AS Qty_Iss, 0 AS Qty, Ibr.PurchaseRate AS PurchRate, I.Rate AS SaleRate, 0 AS RateDiff                                        
                    FROM Stock L
                    Left join Item I On L.Item = I.Code
                    Left Join Item IG On I.ItemGroup = IG.Code
                    Left Join Item IC On I.ItemCategory = IC.Code
                    LEFT JOIN ItemBranchRate Ibr On I.Code = Ibr.Code
                    WHERE 1=1"
                mQryStock = mQryStock & " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
                mQryStock = mQryStock & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
                mQryStock = mQryStock & Replace(ReportFrm.GetWhereCondition("L.Div_Code", rowDivision), "''", "'")
                mQryStock = mQryStock & " And I.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "','" & ItemTypeCode.InternalProduct & "')"
            End If

            Dim mVMainCond As String = " Where 1=1 "
            If ReportFrm.FGetText(rowAccountGroup) <> "" Then
                mVMainCond += " And VMain.GroupName = " & ReportFrm.FGetCode(rowAccountGroup) & ""
            End If


            If ReportFrm.FGetText(rowReportType) = "Detail" Then
                Dim mConditionDetail As String
                Select Case ReportFrm.FGetText(rowAccountGroup).ToString.ToUpper
                    Case "Tax Collected".ToUpper
                        mConditionDetail = " "
                        If ReportFrm.FGetText(rowAccountGroup) <> "" Then
                            mConditionDetail = mConditionDetail & " And V.GroupName = " & ReportFrm.FGetCode(rowAccountGroup) & ""
                        End If
                        If ReportFrm.FGetText(rowAccount) <> "" Then
                            mConditionDetail = mConditionDetail & " And V.Subcode = " & ReportFrm.FGetCode(rowAccount) & ""
                        End If



                        mQry = " Select 'Opening' As SearchCode, 'Opening' As DocNo, 'OB' as DocType,
                                " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " As DocDate, IfNull(Sum(V.AmtCr),0) as Amount                     
                                From (" & mQryTaxCollected & ") As V 
                                Where V.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " "
                        mQry = mQry & mConditionDetail
                        mQry = mQry & " Union All "
                        mQry = mQry & " Select V.DocId As SearchCode, V.ManualRefNo As DocNo, V.V_type as DocType,
                                V.V_Date As DocDate, V.AmtCr as Amount                     
                                From (" & mQryTaxCollected & ") As V 
                                Where V.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                                And V.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & ""
                        mQry = mQry & mConditionDetail

                        mQry = "Select SearchCode, DocNo, DocType, DocDate, Amount
                                From (" & mQry & ") as X 
                                Where X.Amount <>0    "
                        mQry = mQry & " Order By DocDate, DocNo "

                    Case "Stock".ToUpper
                        mConditionDetail = " "
                        If ReportFrm.FGetText(rowAccountGroup) <> "" Then
                            mConditionDetail = mConditionDetail & " And V.GroupName = " & ReportFrm.FGetCode(rowAccountGroup) & ""
                        End If
                        If ReportFrm.FGetText(rowAccount) <> "" Then
                            mConditionDetail = mConditionDetail & " And V.Itemcode = " & ReportFrm.FGetCode(rowAccount) & ""
                        End If



                        mQry = " Select 'Opening' As SearchCode, 'Opening' As DocNo, 'OB' as DocType,
                                " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " As DocDate, 
                                0 as Qty_Rec, 0 as Qty_Iss, IfNull(Sum(V.Qty_Rec- V.Qty_Iss),0) as Balance, Sum(V.AmtDr-V.AmtCr) as Value
                                From (" & mQryStock & ") As V 
                                Where V.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " "
                        mQry = mQry & mConditionDetail
                        mQry = mQry & " Union All "
                        mQry = mQry & " Select V.DocId As SearchCode, V.ManualRefNo As DocNo, V.V_type as DocType,
                                V.V_Date As DocDate, 
                                V.Qty_Rec, V.Qty_Iss, 0 as Balance, V.AmtDr-V.AmtCr as Value
                                From (" & mQryStock & ") As V 
                                Where V.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                                And V.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & ""
                        mQry = mQry & mConditionDetail

                        mQry = "Select SearchCode, DocNo, DocType, DocDate, Qty_Rec, Qty_Iss, Balance, Value
                                From (" & mQry & ") as X 
                                Where X.Qty_Rec <> 0 Or X.Qty_Iss <> 0 Or X.Balance <> 0 "
                        mQry = mQry & " Order By DocDate, DocNo "

                    Case "Rate Difference".ToUpper
                        mConditionDetail = " "
                        If ReportFrm.FGetText(rowAccountGroup) <> "" Then
                            mConditionDetail = mConditionDetail & " And V.GroupName = " & ReportFrm.FGetCode(rowAccountGroup) & ""
                        End If
                        If ReportFrm.FGetText(rowAccount) <> "" Then
                            mConditionDetail = mConditionDetail & " And V.Itemcode = " & ReportFrm.FGetCode(rowAccount) & ""
                        End If



                        mQry = " Select 'Opening' As SearchCode, 'Opening' As DocNo, 'OB' as DocType,
                                " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " As DocDate, IfNull(Sum(V.RateDiff),0) as RateDifference
                                From (" & mQryRateDifference & ") As V 
                                Where V.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " "
                        mQry = mQry & mConditionDetail
                        mQry = mQry & " Union All "
                        mQry = mQry & " Select V.DocId As SearchCode, V.ManualRefNo As DocNo, V.V_type as DocType,
                                V.V_Date As DocDate, V.RateDiff as RateDifference
                                From (" & mQryRateDifference & ") As V 
                                Where V.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                                And V.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & ""
                        mQry = mQry & mConditionDetail

                        mQry = "Select SearchCode, DocNo, DocType, DocDate, RateDifference
                                From (" & mQry & ") as X 
                                Where X.RateDifference <>0    "
                        mQry = mQry & " Order By DocDate, DocNo "

                    Case Else
                        mConditionDetail = " "
                        If ReportFrm.FGetText(rowAccountGroup) <> "" Then
                            mConditionDetail = mConditionDetail & " And V.GroupName = " & ReportFrm.FGetCode(rowAccountGroup) & ""
                        End If
                        If ReportFrm.FGetText(rowAccount) <> "" Then
                            mConditionDetail = mConditionDetail & " And V.Subcode = " & ReportFrm.FGetCode(rowAccount) & ""
                        End If


                        mQry = " Select 'Opening' As SearchCode, 'Opening' As DocNo, 'OB' as DocType,
                                " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " As DocDate, 
                                0 as AmtDr, 0 as AmtCr, IfNull(Sum(V.AmtDr-V.AmtCr),0) as Balance                     
                                From (" & mQrySalesCashBank & ") As V 
                                Where V.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " "
                        mQry = mQry & mConditionDetail
                        mQry = mQry & " Union All "
                        mQry = mQry & " Select V.DocId As SearchCode, V.ManualRefNo As DocNo, V.V_type as DocType,
                                V.V_Date As DocDate, V.AmtDr, V.AmtCr, 0 as Balance
                                From (" & mQrySalesCashBank & ") As V 
                                Where V.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                                And V.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & ""
                        mQry = mQry & mConditionDetail

                        mQry = "Select SearchCode, DocNo, DocType, DocDate, X.AmtDr, X.AmtCr, X.Balance
                                From (" & mQry & ") as X 
                                Where X.AmtDr <>0  Or X.AmtCr <> 0 Or X.Balance <> 0 "
                        mQry = mQry & " Order By DocDate, DocNo "
                End Select
            ElseIf ReportFrm.FGetText(rowReportType) = "Group Wise Summary" Then
                mQry = ""
                mQry = "Select V.GroupName as SearchCode, V.GroupName, 
                       Sum(Case When Date(V.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) as Opening,
                       (Case When Sum(Case When Date(V.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) < 0  Then 'Cr' Else 'Dr' End)  as OpeningType,
                       Sum(Case When Date(V.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtDr Else 0 End) as AmtDr,
                       Sum(Case When Date(V.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtCr Else 0 End) as AmtCr,
                       Sum(Case When Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) as Closing,
                       (Case When Sum(Case When Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) < 0 Then 'Cr' Else 'Dr' End) as ClosingType
                       From (" & mQrySalesCashBank & ") as V Group By V.GroupName "
                mQry = mQry & " Union All "
                mQry = mQry & "Select V.GroupName As SearchCode, V.GroupName, 
                       Sum(Case When Date(V.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " Then V.PurchAmt-V.SaleAmt Else 0 End) As Opening,
                       (Case When Sum(Case When Date(V.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " Then V.PurchAmt-V.SaleAmt Else 0 End) < 0  Then 'Cr' Else 'Dr' End)  as OpeningType,
                       Sum(Case When Date(V.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.PurchAmt Else 0 End) As AmtDr,
                       Sum(Case When Date(V.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.SaleAmt Else 0 End) As AmtCr,
                       Sum(Case When Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.PurchAmt-V.SaleAmt Else 0 End) As Closing,
                       (Case When Sum(Case When Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.PurchAmt-V.SaleAmt Else 0 End) < 0 Then 'Cr' Else 'Dr' End) as ClosingType
                       From (" & mQryRateDifference & ") as V Group By V.GroupName "
                If ReportFrm.FGetText(rowFormat) <> Format_WithEstimatedTax Then
                    mQry = mQry & " Union All "
                    mQry = mQry & "Select V.GroupName as SearchCode, V.GroupName, 
                       Sum(Case When Date(V.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) as Opening,
                       (Case When Sum(Case When Date(V.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) < 0  Then 'Cr' Else 'Dr' End)  as OpeningType,
                       Sum(Case When Date(V.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtDr Else 0 End) as AmtDr,
                       Sum(Case When Date(V.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtCr Else 0 End) as AmtCr,
                       Sum(Case When Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) as Closing,
                       (Case When Sum(Case When Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) < 0 Then 'Cr' Else 'Dr' End) as ClosingType
                       From (" & mQryTaxCollected & ") as V Group By V.GroupName "
                End If
                mQry = mQry & " Union All "
                mQry = mQry & " Select V.GroupName as SearchCode, V.GroupName, 
                       Sum(Case When Date(V.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) as Opening,
                       (Case When Sum(Case When Date(V.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) < 0  Then 'Cr' Else 'Dr' End)  as OpeningType,
                       Sum(Case When Date(V.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtDr Else 0 End) as AmtDr,
                       Sum(Case When Date(V.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtCr Else 0 End) as AmtCr,
                       Sum(Case When Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) as Closing,
                       (Case When Sum(Case When Date(V.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " Then V.AmtDr-V.AmtCr Else 0 End) < 0 Then 'Cr' Else 'Dr' End) as ClosingType
                       From (" & mQryStock & ") as V Group By V.GroupName "
            ElseIf ReportFrm.FGetText(rowReportType) = "Account Wise Summary" Then
                Select Case ReportFrm.FGetText(rowAccountGroup).ToString.ToUpper
                    Case "Tax Collected".ToUpper
                        mQry = "Select V.Subcode as SearchCode, Max(V.AccountName) as AccountName, Sum(V.AmtCr) as Amount
                                From (" & mQryTaxCollected & ") as V 
                                Where 1=1 "

                        If ReportFrm.FGetText(rowAccountGroup) <> "" Then
                            mQry = mQry & " And V.GroupName = " & ReportFrm.FGetCode(rowAccountGroup) & ""
                        End If

                        If ReportFrm.FGetText(rowAccount) <> "" Then
                            mQry += " And V.Subcode = " & ReportFrm.FGetCode(rowAccount) & ""
                        End If

                        mQry = mQry & " Group By V.Subcode, V.AccountName "
                        mQry = mQry & " Order By V.AccountName "
                    Case "Stock".ToUpper
                        mQry = "Select V.Itemcode as SearchCode, Max(V.ItemName) as ItemName, Sum(V.Qty) as Qty, Sum(V.AmtDr-V.AmtCr) as Value
                                From (" & mQryStock & ") as V 
                                Where 1=1 "
                        If ReportFrm.FGetText(rowAccountGroup) <> "" Then
                            mQry = mQry & " And V.GroupName = " & ReportFrm.FGetCode(rowAccountGroup) & ""
                        End If

                        If ReportFrm.FGetText(rowAccount) <> "" Then
                            mVMainCond += " And V.ItemCode = " & ReportFrm.FGetCode(rowAccount) & ""
                        End If

                        mQry = mQry & " Group By V.Itemcode, V.ItemName  "
                        mQry = mQry & " Order By V.ItemName "
                    Case "Rate Difference".ToUpper
                        mQry = "Select V.Itemcode as SearchCode, Max(V.ItemName) as ItemName, Sum(V.Qty) as Qty, Sum(V.RateDiff) as RateDifference
                                From (" & mQryRateDifference & ") as V 
                                Where 1=1 "

                        If ReportFrm.FGetText(rowAccountGroup) <> "" Then
                            mQry = mQry & " And V.GroupName = " & ReportFrm.FGetCode(rowAccountGroup) & ""
                        End If

                        If ReportFrm.FGetText(rowAccount) <> "" Then
                            mVMainCond += " And V.ItemCode = " & ReportFrm.FGetCode(rowAccount) & ""
                        End If

                        mQry = mQry & " Group By V.Itemcode, V.ItemName "
                        mQry = mQry & " Order By V.ItemName "

                    Case Else
                        mQry = "Select V.Subcode as SearchCode, Max(V.AccountName) as AccountName, Sum(V.AmtDr-V.AmtCr) as Amount
                                From (" & mQrySalesCashBank & ") as V 
                                Where 1=1 "
                        If ReportFrm.FGetText(rowAccountGroup) <> "" Then
                            mQry = mQry & " And V.GroupName = " & ReportFrm.FGetCode(rowAccountGroup) & ""
                        End If

                        If ReportFrm.FGetText(rowAccount) <> "" Then
                            mVMainCond += " And V.Subcode = " & ReportFrm.FGetCode(rowAccount) & ""
                        End If

                        mQry = mQry & " Group By V.Subcode, V.AccountName  "
                        mQry = mQry & " Order By V.AccountName "
                End Select
            End If

            mQry = AgL.GetBackendBasedQuery(mQry)
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            RepTitle = "Branch Status Report"
            ReportFrm.Text = "Branch Status Report " & " - " & ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"
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
