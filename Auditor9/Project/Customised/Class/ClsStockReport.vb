Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsStockReport
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""


    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Public WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4


    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Public rowReportType As Integer = 0
    Public rowGroupOn As Integer = 1
    Public rowValuation As Integer = 2
    Public rowFromDate As Integer = 3
    Public rowToDate As Integer = 4
    Public rowLocationType As Integer = 5
    Public rowLocation As Integer = 6
    Public rowProcess As Integer = 7
    Public rowItemType As Integer = 8
    Public rowItemCategory As Integer = 9
    Public rowItemGroup As Integer = 10
    Public rowItem As Integer = 11
    Public rowDimension1 As Integer = 12
    Public rowDimension2 As Integer = 13
    Public rowDimension3 As Integer = 14
    Public rowDimension4 As Integer = 15
    Public rowSize As Integer = 16
    Public rowHSN As Integer = 17
    Public rowLotNo As Integer = 18
    Public rowShowZeroBalance As Integer = 19
    Public rowValuationPercentage As Integer = 20
    Public rowSite As Integer = 21
    Public rowDivision As Integer = 22
    Public rowIncludeOpening As Integer = 23

    Dim IsLastPurchaseRateUpdated As Boolean = False
    Dim IsMultiItemStockLedgerAllowedProgramatically As Boolean = False
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
    Dim mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg "
    Dim mHelpDimension1Qry$ = "Select 'o' As Tick, Code, Description From Dimension1 "
    Dim mHelpDimension2Qry$ = "Select 'o' As Tick, Code, Description From Dimension2 "
    Dim mHelpDimension3Qry$ = "Select 'o' As Tick, Code, Description From Dimension3 "
    Dim mHelpDimension4Qry$ = "Select 'o' As Tick, Code, Description From Dimension4 "
    Dim mHelpSizeQry$ = "Select 'o' As Tick, Code, Description From Size "
    Dim mHelpProcessQry$ = "Select 'o' As Tick, SubCode, Name FROM Subgroup WHERE SubgroupType = 'Process' "
    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Stock Balance' as Code, 'Stock Balance' as Name 
                            Union All Select 'Stock Summary' as Code, 'Stock Summary' as Name
                            Union All Select 'Stock Summary With Valuation' as Code, 'Stock Summary With Valuation' as Name 
                            Union All Select 'Stock Ledger' as Code, 'Stock Ledger' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Stock Balance")
            mQry = "SELECT 'o' As Tick, 'ItemCategoryCode' As Code, 'Item Category' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'ItemGroupCode' As Code, 'Item Group' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'ItemCode' As Code, 'Item' As Name 
                        UNION ALL 
                        Select 'o' As Tick, 'HSN' As Code, 'HSN' As Name 
                        UNION ALL 
                        Select 'o' As Tick, 'LotNo' As Code, 'Lot No' As Name 
                        UNION ALL 
                        Select 'o' As Tick, 'ProcessCode' As Code, 'Process' As Name 
                   "

            If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
                mQry += " UNION ALL SELECT 'o' As Tick, 'Dimension1Code' As Code, '" & AgL.PubCaptionDimension1 & "' As Name "
            End If
            If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
                mQry += " UNION ALL SELECT 'o' As Tick, 'Dimension2Code' As Code, '" & AgL.PubCaptionDimension2 & "' As Name "
            End If
            If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
                mQry += " UNION ALL SELECT 'o' As Tick, 'Dimension3Code' As Code, '" & AgL.PubCaptionDimension3 & "' As Name "
            End If
            If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
                mQry += " UNION ALL SELECT 'o' As Tick, 'Dimension4Code' As Code, '" & AgL.PubCaptionDimension4 & "' As Name "
            End If
            If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Size) Then
                mQry += " UNION ALL SELECT 'o' As Tick, 'SizeCode' As Code, 'Size' As Name "
            End If
            ReportFrm.CreateHelpGrid("GroupOn", "Group On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, "")


            mQry = "SELECT 'None' As Code, 'None' As Name 
                        UNION ALL 
                        SELECT 'Master Purchase Rate' As Code, 'Master Purchase Rate' As Name 
                        UNION ALL 
                        SELECT 'Last Purchase Rate' As Code, 'Last Purchase Rate' As Name 
                   "
            ReportFrm.CreateHelpGrid("Valuation", "Valuation", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "None")


            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            mQry = "Select 'In Hand' as Code, 'In Hand' as Name
                    Union All 
                    Select 'At Person' as Code, 'At Person' as Name 
                    Union All 
                    Select 'Both' as Code, 'Both' as Name "
            ReportFrm.CreateHelpGrid("LocationType", "Location Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "In Hand")
            ReportFrm.CreateHelpGrid("Location", "Location", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpLocationQry)
            ReportFrm.CreateHelpGrid("Process", "Process", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpProcessQry)
            ReportFrm.CreateHelpGrid("ItemType", AgL.PubCaptionItemType, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemTypeQry)
            'ReportFrm.FilterGrid.Rows(rowItemType).Visible = False
            ReportFrm.CreateHelpGrid("ItemCategory", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
            ReportFrm.CreateHelpGrid("ItemGroup", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
            ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
            ReportFrm.CreateHelpGrid("Dimension1", "Dimension1", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension1Qry)
            ReportFrm.FilterGrid.Rows(rowDimension1).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1), Boolean)
            ReportFrm.CreateHelpGrid("Dimension2", "Dimension2", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension2Qry)
            ReportFrm.FilterGrid.Rows(rowDimension2).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2), Boolean)
            ReportFrm.CreateHelpGrid("Dimension3", "Dimension3", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension3Qry)
            ReportFrm.FilterGrid.Rows(rowDimension3).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3), Boolean)
            ReportFrm.CreateHelpGrid("Dimension4", "Dimension4", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension4Qry)
            ReportFrm.FilterGrid.Rows(rowDimension4).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4), Boolean)
            ReportFrm.CreateHelpGrid("Size", "Size", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSizeQry)
            ReportFrm.FilterGrid.Rows(rowSize).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Size), Boolean)
            ReportFrm.CreateHelpGrid("HSN", "HSN", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
            ReportFrm.FilterGrid.Rows(rowHSN).Visible = False 'Hide HSN Row
            ReportFrm.CreateHelpGrid("LotNo", "Lot No", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
            ReportFrm.FilterGrid.Rows(rowLotNo).Visible = False 'Hide LotNo Row
            ReportFrm.CreateHelpGrid("ShowZeroBalance", "Show Zero Balance", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "Yes")
            ReportFrm.CreateHelpGrid("ValuationPercentage", "Valuation Percentage", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", "")
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("IncludeOpening", "Include Opening", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "Yes")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcStockReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcStockReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"



            RepTitle = "Stock Report"


            'If ReportFrm.FGetText(rowValuation) = "Last Purchase Rate" And IsLastPurchaseRateUpdated = False Then
            '    If AgL.PubServerName <> "" Then
            '        mQry = "UPDATE Item SET LastPurchaseRate = 0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        mQry = "UPDATE Item SET LastPurchaseRate = (SELECT TOP 1 L.Taxable_Amount / (CASE WHEN L.Qty > 0 THEN L.Qty ELSE 1 End)  FROM PurchInvoiceDetail L LEFT JOIN PurchInvoice H ON L.DocID = H.DocID 
            '                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            '                WHERE L.Item = Item.Code And L.Qty > 0 
            '                And Vt.NCat = '" & Ncat.PurchaseInvoice & "'
            '                ORDER BY H.V_Date DESC  ) Where LastPurchaseRate = 0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        mQry = "UPDATE Item SET LastPurchaseRate = (
            '                 SELECT TOP 1 L.Taxable_Amount / (CASE WHEN L.Qty > 0 THEN L.Qty ELSE 1 End)  
            '                 FROM PurchInvoiceDimensionDetail Pdl
            '                 LEFT JOIN PurchInvoiceDetail L ON Pdl.DocID = L.DocID AND Pdl.TSr = L.Sr
            '                 LEFT JOIN PurchInvoice H ON L.DocID = H.DocID
            '                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type  
            '                 WHERE Pdl.Item = Item.Code 
            '                    And Vt.NCat = '" & Ncat.PurchaseInvoice & "'
            '                 And L.Qty > 0 
            '                 AND Pdl.Item IS NOT NULL
            '                 ORDER BY H.V_Date DESC  
            '                ) Where IsNull(LastPurchaseRate,0) = 0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        mQry = "UPDATE Item SET LastPurchaseRate = (SELECT Top 1 L.Rate  FROM PurchInvoiceDetail L LEFT JOIN PurchInvoice H ON L.DocID = H.DocID WHERE H.V_Type ='OS' and L.Qty > 0 And L.Rate > 0  And L.Item = Item.Code ORDER BY H.V_Date DESC  ) WHERE IfNull(LastPurchaseRate,0) =0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        mQry = "UPDATE Item SET LastPurchaseRate = (SELECT Top 1 L.Rate  
            '                FROM PurchInvoiceDimensionDetail Pdl
            '             LEFT JOIN PurchInvoiceDetail L ON Pdl.DocID = L.DocID AND Pdl.TSr = L.Sr
            '                LEFT JOIN PurchInvoice H ON L.DocID = H.DocID 
            '                WHERE H.V_Type ='OS' and L.Qty > 0 And L.Rate > 0  
            '                And Pdl.Item = Item.Code 
            '                ORDER BY H.V_Date DESC) 
            '                WHERE IfNull(LastPurchaseRate,0) =0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        If ClsMain.FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
            '            ClsMain.FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
            '            mQry = "UPDATE Item
            '                    SET Item.LastPurchaseRate = V1.LastPurchaseRate_New
            '                    FROM (
            '                     SELECT I.Code, I1.LastPurchaseRate AS LastPurchaseRate_New
            '                     FROM Item I 
            '                     LEFT JOIN Item I1 ON IsNull(I.ItemCategory,'') = IsNull(I1.ItemCategory,'') 
            '                       AND IsNull(I.ItemGroup,'') = IsNull(I1.ItemGroup,'') 
            '                       AND IsNull(I.BaseItem,'') = IsNull(I1.BaseItem,'') 
            '                       AND IsNull(I.Dimension1,'') = IsNull(I1.Dimension1,'') 
            '                       AND IsNull(I.Dimension2,'') = IsNull(I1.Dimension2,'') 
            '                       AND IsNull(I.Dimension4,'') = IsNull(I1.Dimension4,'') 
            '                       AND IsNull(I.Size,'') = IsNull(I1.Size,'') 
            '                       AND IsNull(I.Code,'') <> IsNull(I1.Code,'') 
            '                     WHERE IsNull(I.LastPurchaseRate,0) = 0
            '                     AND I.Dimension3 IS NOT NULL
            '                     AND I1.Code IS NOT NULL
            '                     AND IsNull(I1.LastPurchaseRate,0) <> 0
            '                    ) AS V1 WHERE Item.Code = V1.Code "
            '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            '        End If

            '        IsLastPurchaseRateUpdated = True
            '    Else
            '        mQry = "UPDATE Item SET LastPurchaseRate = 0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        mQry = "UPDATE Item SET LastPurchaseRate = (SELECT L.Taxable_Amount / (CASE WHEN L.Qty > 0 THEN L.Qty ELSE 1 End)  FROM PurchInvoiceDetail L LEFT JOIN PurchInvoice H ON L.DocID = H.DocID  
            '                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            '                And Vt.NCat = '" & Ncat.PurchaseInvoice & "'
            '                WHERE L.Item = Item.Code And L.Qty > 0  ORDER BY H.V_Date DESC Limit 1 ) Where LastPurchaseRate = 0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        mQry = "UPDATE Item SET LastPurchaseRate = (
            '                 SELECT L.Taxable_Amount / (CASE WHEN L.Qty > 0 THEN L.Qty ELSE 1 End)  
            '                 FROM PurchInvoiceDimensionDetail Pdl
            '                 LEFT JOIN PurchInvoiceDetail L ON Pdl.DocID = L.DocID AND Pdl.TSr = L.Sr
            '                 LEFT JOIN PurchInvoice H ON L.DocID = H.DocID 
            '                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type   
            '                 WHERE Pdl.Item = Item.Code 
            '                    And Vt.NCat = '" & Ncat.PurchaseInvoice & "'
            '                 And L.Qty > 0 
            '                 AND Pdl.Item IS NOT NULL
            '                 ORDER BY H.V_Date DESC  Limit 1 
            '                ) Where IsNull(LastPurchaseRate,0) = 0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        mQry = "UPDATE Item SET LastPurchaseRate = (SELECT L.Rate  FROM PurchInvoiceDetail L LEFT JOIN PurchInvoice H ON L.DocID = H.DocID WHERE H.V_Type ='OS' And L.Item = Item.Code and L.Qty > 0 And L.Rate > 0 ORDER BY H.V_Date DESC Limit 1) WHERE IfNull(LastPurchaseRate,0) = 0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        mQry = "UPDATE Item SET LastPurchaseRate = (SELECT L.Rate  
            '                FROM PurchInvoiceDimensionDetail Pdl
            '             LEFT JOIN PurchInvoiceDetail L ON Pdl.DocID = L.DocID AND Pdl.TSr = L.Sr
            '                LEFT JOIN PurchInvoice H ON L.DocID = H.DocID 
            '                WHERE H.V_Type ='OS' and L.Qty > 0 And L.Rate > 0  
            '                And Pdl.Item = Item.Code 
            '                ORDER BY H.V_Date DESC Limit 1 )  
            '                WHERE IfNull(LastPurchaseRate,0) =0 "
            '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            '        IsLastPurchaseRateUpdated = True
            '    End If
            'End If



            If ReportFrm.FGetText(rowValuation) = "Last Purchase Rate" And IsLastPurchaseRateUpdated = False Then
                mQry = "UPDATE Item SET LastPurchaseRate = 0 "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "UPDATE Item SET LastPurchaseRate = (SELECT " & IIf(AgL.PubServerName = "", "", "Top 1") & " L.Taxable_Amount / (CASE WHEN L.Qty > 0 THEN L.Qty ELSE 1 End)  FROM PurchInvoiceDetail L LEFT JOIN PurchInvoice H ON L.DocID = H.DocID 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        WHERE L.Item = Item.Code And L.Qty > 0 And L.Rate > 0
                        And Vt.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.OpeningStock & "')
                        ORDER BY H.V_Date DESC " & IIf(AgL.PubServerName = "", "Limit 1", "") & ") Where LastPurchaseRate = 0 "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "UPDATE Item SET LastPurchaseRate = (
	                            SELECT " & IIf(AgL.PubServerName = "", "", "Top 1") & " Case When IfNull(L.Taxable_Amount,0) = 0 Then L.Amount Else L.Taxable_Amount End / (CASE WHEN L.Qty > 0 THEN L.Qty ELSE 1 End)  
	                            FROM PurchInvoiceDimensionDetail Pdl
	                            LEFT JOIN PurchInvoiceDetail L ON Pdl.DocID = L.DocID AND Pdl.TSr = L.Sr
	                            LEFT JOIN PurchInvoice H ON L.DocID = H.DocID
                                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type  
	                            WHERE Pdl.Item = Item.Code 
                                And Vt.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.OpeningStock & "')
	                            And L.Qty > 0 
	                            AND Pdl.Item IS NOT NULL
	                            ORDER BY H.V_Date DESC  " & IIf(AgL.PubServerName = "", "Limit 1", "") & "
                            ) Where IsNull(LastPurchaseRate,0) = 0 "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "UPDATE Item SET LastPurchaseRate = (SELECT " & IIf(AgL.PubServerName = "", "", "Top 1") & " Round(L.Taxable_Amount / (CASE WHEN L.Qty > 0 THEN L.Qty * Uc.Multiplier ELSE 1 End),2)  
                        FROM Item I 
                        LEFT JOIN PurchInvoiceDetail L ON I.Code = L.Item
                        LEFT JOIN PurchInvoice H ON L.DocID = H.DocID
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        LEFT JOIN UnitConversion Uc ON L.Item = Uc.Item And L.Unit = Uc.FromUnit AND I.StockUnit = Uc.ToUnit
                        WHERE L.Item = Item.Code 
                        And Vt.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.OpeningStock & "')
                        AND I.StockUnit IS NOT NULL 
                        AND I.Unit <> I.StockUnit
                        ORDER BY H.V_Date DESC  " & IIf(AgL.PubServerName = "", "Limit 1", "") & ")
                        WHERE Code IN (SELECT I.Code FROM Item I WHERE I.StockUnit IS NOT NULL AND I.Unit <> I.StockUnit)"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


                If ClsMain.FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
                        ClsMain.FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                    mQry = "UPDATE Item
                                SET Item.LastPurchaseRate = V1.LastPurchaseRate_New
                                FROM (
	                                SELECT I.Code, I1.LastPurchaseRate AS LastPurchaseRate_New
	                                FROM Item I 
	                                LEFT JOIN Item I1 ON IsNull(I.ItemCategory,'') = IsNull(I1.ItemCategory,'') 
			                                AND IsNull(I.ItemGroup,'') = IsNull(I1.ItemGroup,'') 
			                                AND IsNull(I.BaseItem,'') = IsNull(I1.BaseItem,'') 
			                                AND IsNull(I.Dimension1,'') = IsNull(I1.Dimension1,'') 
			                                AND IsNull(I.Dimension2,'') = IsNull(I1.Dimension2,'') 
			                                AND IsNull(I.Dimension4,'') = IsNull(I1.Dimension4,'') 
			                                AND IsNull(I.Size,'') = IsNull(I1.Size,'') 
			                                AND IsNull(I.Code,'') <> IsNull(I1.Code,'') 
	                                WHERE IsNull(I.LastPurchaseRate,0) = 0
	                                AND I.Dimension3 IS NOT NULL
	                                AND I1.Code IS NOT NULL
	                                AND IsNull(I1.LastPurchaseRate,0) <> 0
                                ) AS V1 WHERE Item.Code = V1.Code "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                End If

                'If AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0 Then
                '    mQry = " UPDATE Item Set LastPurchaseRate = LastPurchaseRate * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100 "
                '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                'End If

                IsLastPurchaseRateUpdated = True
            End If



            Dim bStockTable As String = " Select DocId, TSr, Sr, V_Date, V_Type, Site_Code, Div_Code, RecId, SubCode, Godown, Process, LotNo, Item, Qty_Iss, Qty_Rec, Unit, Rate From Stock "
            Dim bStockProcessTable As String = " Select DocId, TSr, Sr, V_Date, V_Type, Site_Code, Div_Code, RecId, SubCode, SubCode As Godown, Process, LotNo, Item, Qty_Iss, Qty_Rec, Unit, Rate From StockProcess "
            Dim bCombinedTable As String = "(" & bStockTable & " UNION ALL " & bStockProcessTable & ")"
            bStockTable = " (" + bStockTable + ") "
            bStockProcessTable = " (" + bStockProcessTable + ") "


            If ReportFrm.FGetText(rowLocationType) = "At Person" Then
                bTableName = bStockProcessTable
            ElseIf ReportFrm.FGetText(rowLocationType) = "Both" Then
                bTableName = bCombinedTable
            Else
                bTableName = bStockTable
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Stock Balance" Or
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Stock Summary With Valuation" Or
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Stock Summary" Then
                        If mGridRow.DataGridView.Columns.Contains("Item Category Code") = True Then
                            If AgL.XNull(mGridRow.Cells("Item Category").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowItemCategory).Value = mGridRow.Cells("Item Category").Value
                                mFilterGrid.Item(GFilterCode, rowItemCategory).Value = "'" + mGridRow.Cells("Item Category Code").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("Item Group Code") = True Then
                            If AgL.XNull(mGridRow.Cells("Item Group").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowItemGroup).Value = mGridRow.Cells("Item Group").Value
                                mFilterGrid.Item(GFilterCode, rowItemGroup).Value = "'" + mGridRow.Cells("Item Group Code").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("Item Code") = True Then
                            If AgL.XNull(mGridRow.Cells("Item").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowItem).Value = mGridRow.Cells("Item").Value
                                mFilterGrid.Item(GFilterCode, rowItem).Value = "'" + mGridRow.Cells("Item Code").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("Dimension1Code") = True Then
                            If AgL.XNull(mGridRow.Cells("Dimension1").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowDimension1).Value = mGridRow.Cells("Dimension1").Value
                                mFilterGrid.Item(GFilterCode, rowDimension1).Value = "'" + mGridRow.Cells("Dimension1Code").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("Dimension2Code") = True Then
                            If AgL.XNull(mGridRow.Cells("Dimension2").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowDimension2).Value = mGridRow.Cells("Dimension2").Value
                                mFilterGrid.Item(GFilterCode, rowDimension2).Value = "'" + mGridRow.Cells("Dimension2Code").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("Dimension3Code") = True Then
                            If AgL.XNull(mGridRow.Cells("Dimension3").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowDimension3).Value = mGridRow.Cells("Dimension3").Value
                                mFilterGrid.Item(GFilterCode, rowDimension3).Value = "'" + mGridRow.Cells("Dimension3Code").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("Dimension4Code") = True Then
                            If AgL.XNull(mGridRow.Cells("Dimension4").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowDimension4).Value = mGridRow.Cells("Dimension4").Value
                                mFilterGrid.Item(GFilterCode, rowDimension4).Value = "'" + mGridRow.Cells("Dimension4Code").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("Size Code") = True Then
                            If AgL.XNull(mGridRow.Cells("Size").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowSize).Value = mGridRow.Cells("Size").Value
                                mFilterGrid.Item(GFilterCode, rowSize).Value = "'" + mGridRow.Cells("Size Code").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("HSN") = True Then
                            If AgL.XNull(mGridRow.Cells("HSN").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowHSN).Value = mGridRow.Cells("HSN").Value
                                mFilterGrid.Item(GFilterCode, rowHSN).Value = "'" + mGridRow.Cells("HSN").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("LotNo") = True Then
                            If AgL.XNull(mGridRow.Cells("LotNo").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowLotNo).Value = mGridRow.Cells("LotNo").Value
                                mFilterGrid.Item(GFilterCode, rowLotNo).Value = "'" + mGridRow.Cells("LotNo").Value + "'"
                            End If
                        End If
                        If mGridRow.DataGridView.Columns.Contains("Process Code") = True Then
                            If AgL.XNull(mGridRow.Cells("Process").Value) <> "" Then
                                mFilterGrid.Item(GFilter, rowProcess).Value = mGridRow.Cells("Process").Value
                                mFilterGrid.Item(GFilterCode, rowProcess).Value = "'" + mGridRow.Cells("Process Code").Value + "'"
                            End If
                        End If


                        mFilterGrid.Item(GFilter, rowReportType).Value = "Stock Ledger"
                        If ReportFrm.GetWhereCondition("Sku.Code", rowItem) = "" Then
                            mQry = " Select Code, Description 
                                    From Item I 
                                    Where 1 = 1 
                                    " & IIf(AgL.XNull(mFilterGrid.Item(GFilterCode, rowItemCategory).Value) <> "", " And I.ItemCategory = " & AgL.XNull(mFilterGrid.Item(GFilterCode, rowItemCategory).Value) & "", "") & " 
                                    " & IIf(AgL.XNull(mFilterGrid.Item(GFilterCode, rowItemGroup).Value) <> "", " And I.ItemGroup = " & AgL.XNull(mFilterGrid.Item(GFilterCode, rowItemGroup).Value) & "", "") & " 
                                    " & IIf(AgL.XNull(mFilterGrid.Item(GFilterCode, rowDimension1).Value) <> "", " And I.Dimension1 = " & AgL.XNull(mFilterGrid.Item(GFilterCode, rowDimension1).Value) & "", IIf(mGridRow.DataGridView.Columns.Contains("Dimension1Code") = True, " And I.Dimension1 Is Null", "")) & " 
                                    " & IIf(AgL.XNull(mFilterGrid.Item(GFilterCode, rowDimension2).Value) <> "", " And I.Dimension2 = " & AgL.XNull(mFilterGrid.Item(GFilterCode, rowDimension2).Value) & "", IIf(mGridRow.DataGridView.Columns.Contains("Dimension2Code") = True, " And I.Dimension2 Is Null", "")) & " 
                                    " & IIf(AgL.XNull(mFilterGrid.Item(GFilterCode, rowDimension3).Value) <> "", " And I.Dimension3 = " & AgL.XNull(mFilterGrid.Item(GFilterCode, rowDimension3).Value) & "", IIf(mGridRow.DataGridView.Columns.Contains("Dimension3Code") = True, " And I.Dimension3 Is Null", "")) & " 
                                    " & IIf(AgL.XNull(mFilterGrid.Item(GFilterCode, rowDimension4).Value) <> "", " And I.Dimension4 = " & AgL.XNull(mFilterGrid.Item(GFilterCode, rowDimension4).Value) & "", IIf(mGridRow.DataGridView.Columns.Contains("Dimension4Code") = True, " And I.Dimension4 Is Null", "")) & " 
                                    " & IIf(AgL.XNull(mFilterGrid.Item(GFilterCode, rowSize).Value) <> "", " And I.Size = " & AgL.XNull(mFilterGrid.Item(GFilterCode, rowSize).Value) & "", IIf(mGridRow.DataGridView.Columns.Contains("SizeCode") = True, " And I.Size Is Null", "")) & " 
                                    "
                            Dim DtItem As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtItem.Rows.Count > 1 Then
                                If ClsMain.FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
                                    ClsMain.FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                                    mFilterGrid.Item(GFilter, rowReportType).Value = "Stock Ledger"
                                    For I As Integer = 0 To DtItem.Rows.Count - 1
                                        If mFilterGrid.Item(GFilter, rowItem).Value <> "" Then mFilterGrid.Item(GFilter, rowItem).Value += ","
                                        mFilterGrid.Item(GFilter, rowItem).Value += DtItem.Rows(I)("Description")
                                        If mFilterGrid.Item(GFilterCode, rowItem).Value <> "" Then mFilterGrid.Item(GFilterCode, rowItem).Value += ","
                                        mFilterGrid.Item(GFilterCode, rowItem).Value += "'" + DtItem.Rows(I)("Code") + "'"
                                    Next
                                    IsMultiItemStockLedgerAllowedProgramatically = True
                                Else
                                    mFilterGrid.Item(GFilter, rowReportType).Value = "Stock Balance"
                                    mFilterGrid.Item(GFilter, rowGroupOn).Value = ""
                                    mFilterGrid.Item(GFilterCode, rowGroupOn).Value = ""
                                End If
                            ElseIf DtItem.Rows.Count = 1 Then
                                mFilterGrid.Item(GFilter, rowItem).Value = DtItem.Rows(0)("Description")
                                mFilterGrid.Item(GFilterCode, rowItem).Value = "'" + DtItem.Rows(0)("Code") + "'"
                                mFilterGrid.Item(GFilter, rowReportType).Value = "Stock Ledger"
                            Else
                                Exit Sub
                            End If
                        End If
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Stock Ledger" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If




            mCondStr = "  "
            mCondStr = mCondStr & "  "
            mCondStr = mCondStr & "And Sku.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "','" & ItemTypeCode.InternalProduct & "')"
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Godown", rowLocation)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Process", rowProcess)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemType", rowItemType)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("IfNull(Sku.ItemCategory,Sku.Code)", rowItemCategory)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemGroup", rowItemGroup)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension1", rowDimension1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension2", rowDimension2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension3", rowDimension3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension4", rowDimension4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Size", rowSize)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("IfNull(Sku.HSN,IC.HSN)", rowHSN)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.LotNo", rowLotNo)

            If AgL.XNull(ReportFrm.FGetText(rowItem)) <> "" Then
                mCondStr = mCondStr & ReportFrm.GetWhereCondition("IfNull(Sku.BaseItem,Sku.Code)", rowItem)
            End If


            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Div_Code", rowDivision), "''", "'")


            If ReportFrm.FGetText(rowReportType) = "Stock Ledger" Then
                If ReportFrm.GetWhereCondition("Sku.Code", rowItem) = "" Then
                    MsgBox("Stock Ledger can be filled for single item only.")
                    Exit Sub
                ElseIf InStr(ReportFrm.GetWhereCondition("Sku.Code", rowItem), "',") > 0 Then
                    If IsMultiItemStockLedgerAllowedProgramatically = False Then
                        MsgBox("Stock Ledger can be filled for single item only.")
                        Exit Sub
                    End If
                End If
            End If



            Dim bTempTableName As String = "[" + Guid.NewGuid().ToString() + "]"

            If AgL.VNull(AgL.Dman_Execute("SELECT Count(Bd.Code) As Cnt
                            FROM BOMDetail Bd
                            LEFT JOIN Item I ON Bd.Code = I.Code
                            LEFT JOIN Item Bi ON I.BaseItem = Bi.Code
                            LEFT JOIN Item Ci ON Bd.Item = Ci.Code
                            WHERE Bi.ItemType = Ci.ItemType", AgL.GCn).ExecuteScalar()) > 0 Then

                If AgL.IsTableExist(bTempTableName.Replace("[", "").Replace("]", ""), AgL.GCn) Then
                    mQry = "Drop Table " + bTempTableName
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                End If

                mQry = " CREATE TABLE " & bTempTableName & "(DocID NVARCHAR (21), TSr INT
                        , Sr INT, V_Type NVARCHAR (5), V_Prefix NVARCHAR (5), V_Date DATETIME
                        , V_No BIGINT, Div_Code NVARCHAR (1), Site_Code NVARCHAR (2), SubCode NVARCHAR (10)
                        , LotNo NVARCHAR (20), Godown NVARCHAR (10), Item WVARCHAR (255), Qty_Iss DOUBLE
                        , Qty_Rec DOUBLE, Unit NVARCHAR (10), UnitMultiplier FLOAT, Rate FLOAT, RecId VARCHAR (20)) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "INSERT INTO " & bTempTableName & "(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, 
                        Site_Code, SubCode, LotNo, Godown, Item, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, Rate, RecId)
                        SELECT L.DocID, L.TSr, L.Sr, L.V_Type, L.V_Prefix, L.V_Date, L.V_No, L.Div_Code, L.Site_Code, L.SubCode, 
                        L.LotNo, L.Godown, 
                        CASE WHEN V1.Item IS NOT NULL THEN V1.Item ELSE L.Item END AS Item, 
                        CASE WHEN V1.Item IS NOT NULL THEN V1.Qty * L.Qty_Iss ELSE L.Qty_Iss END AS Qty_Iss, 
                        CASE WHEN V1.Item IS NOT NULL THEN V1.Qty * L.Qty_Rec ELSE L.Qty_Rec END AS Qty_Rec, 
                        L.Unit, L.UnitMultiplier, L.Rate, L.RecId
                        FROM " & bTableName & " L 
                        LEFT JOIN Item I ON L.Item = I.Code
                        LEFT JOIN (
	                        SELECT I.BaseItem AS Code, Bd.Item, Bd.Qty
	                        FROM BOMDetail Bd
	                        LEFT JOIN Item I ON Bd.Code = I.Code
                        ) AS V1 ON I.Code = V1.Code "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                bTableName = bTempTableName
            End If

            Dim mMainQry As String = ""
            If ReportFrm.FGetText(rowIncludeOpening) = "Yes" Then
                mMainQry = " SELECT ' Opening' as DocID, ' Opening' V_Type, ' 0' as RecId, strftime('%d/%m/%Y', " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & ")  V_Date, " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & "  V_Date_ActualFormat
                    , Null as PartyName, Max(Location.Name) as LocationName
                    , Sku.Code AS SkuCode, Max(Sku.Description) AS SkuName 
                    , Max(Sku.Specification) as SkuSpecification
                    , Max(IG.Code) as ItemGroupCode, Max(IG.Description) as ItemGroupName
                    , Max(IC.Code) as ItemCategoryCode, Max(IC.Description) as ItemCategoryName 
                    , Max(Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Code Else Sku.Code End) as ItemCode
                    , Max(Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Specification Else IfNull(Sku.Specification, Sku.Description) End) as ItemName 
                    , Max(D1.Code) as Dimension1Code, Max(D1.Specification) as Dimension1Name 
                    , Max(D2.Code) as Dimension2Code, Max(D2.Specification) as Dimension2Name 
                    , Max(D3.Code) as Dimension3Code, Max(D3.Specification) as Dimension3Name 
                    , Max(D4.Code) as Dimension4Code, Max(D4.Specification) as Dimension4Name 
                    , Max(Size.Code) as SizeCode, Max(Size.Description) as SizeName
                    , Max(IfNull(Sku.HSN, IC.HSN)) as HSN, Max(L.LotNo) as LotNo
                    , Max(Prc.SubCode) as ProcessCode, Max(Prc.Name) as ProcessName, 
                    Max(IfNull(Sku.StockUnit, L.Unit)) as Unit, Max(U.DecimalPlaces) as DecimalPlaces, 
                    Sum(Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                        Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End) AS Opening, 
                    0 AS Qty_Rec, 
                    0 AS Qty_Iss, 
                    Sum(Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                        Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End) AS Closing, 
                    0 as TransactionRate, "


            If ReportFrm.FGetText(rowValuation) = "Master Purchase Rate" Then
                mMainQry = mMainQry & " Max(Sku.PurchaseRate) " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as ValuationRate, 
                    IfNull(Sum((Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End)*(Sku.PurchaseRate)),0) 
                    " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as Amount "
            ElseIf ReportFrm.FGetText(rowValuation) = "Last Purchase Rate" Then
                mMainQry = mMainQry & " (Case When Max(RList.Code) Is Not Null Then Max(RList.Cost) Else 
                    Max(Sku.LastPurchaseRate) End) " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as ValuationRate, 
                    IfNull(Sum((Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End)*(Case When RList.Code Is Not Null Then RList.Cost Else Sku.LastPurchaseRate End)),0) 
                    " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as Amount "
            Else
                mMainQry = mMainQry & " 0 as ValuationRate, 0 as Amount "
            End If
            mMainQry = mMainQry & " FROM " & bTableName & " L
                    LEFT JOIN Item Sku ON L.Item = Sku.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    Left Join Item IC On IfNull(Sku.ItemCategory,Sku.code) = IC.Code
                    LEFT JOIN Item I ON IfNull(Sku.BaseItem, Sku.Code) = I.Code
                    LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sku.Size = Size.Code
                    LEFT JOIN SubGroup Prc On L.Process = Prc.SubCode
                    Left Join Unit U On L.Unit = U.Code
                    LEFT JOIN Unit Su On Sku.StockUnit = Su.Code 
                    LEFT JOIN UnitConversion Uc On L.Item = Uc.Item And L.Unit = Uc.FromUnit And Sku.StockUnit = Uc.ToUnit
                    Left Join viewHelpSubgroup Sg On L.Subcode = Sg.Code
                    LEFT JOIN Voucher_type vt ON L.V_Type = vt.V_Type 
                    Left Join Subgroup Location On L.Godown = Location.Subcode "
            mMainQry += " Left Join (
                                     Select Max(RLD.Code) as Code, RLD.ItemCategory, RLD.Dimension1, RLD.Size, Max(RLD.Cost) as Cost 
                                     From RateListDetail RLD 
                                     Left Join RateList RL On RLD.Code = RL.Code 
                                     Where RLD.Process='PSales' 
                                     And RL.RateCategory is Null
                                     Group By RLD.ItemCategory, RLD.Dimension1, RLD.Size
                                     ) as RList On IC.Code = RList.ItemCategory and D1.Code = RList.Dimension1 and size.Code = RList.Size "
            mMainQry += " WHERE L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " " & mCondStr & "
                    GROUP BY Sku.Code , L.Godown 
                    Union All "
            End If
            mMainQry += " Select L.DocID, L.V_Type, L.RecId, 
                    strftime('%d/%m/%Y', L.V_Date) As V_Date, L.V_Date As V_Date_ActualFormat
                    , Sg.Name as PartyName, Location.Name as LocationName
                    , Sku.Code AS SkuCode, Sku.Description AS SkuName
                    , Sku.Specification as SkuSpecification
                    , IG.Code as ItemGroupCode, IG.Description as ItemGroupName
                    , IC.Code as ItemCategoryCode, IC.Description as ItemCategoryName 
                    , Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Code Else Sku.Code End as ItemCode
                    , Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Specification Else IfNull(Sku.Specification,Sku.Description) End as ItemName 
                    , D1.Code as Dimension1Code, D1.Specification as Dimension1Name 
                    , D2.Code as Dimension2Code, D2.Specification as Dimension2Name 
                    , D3.Code as Dimension3Code, D3.Specification as Dimension3Name 
                    , D4.Code as Dimension4Code, D4.Specification as Dimension4Name 
                    , Size.Code as SizeCode, Size.Description as SizeName
                    , IfNull(Sku.HSN, IC.HSN) as HSN, L.LotNo as LotNo
                    , Prc.SubCode as ProcessCode, Prc.Name as ProcessName, 
                    IfNull(Sku.StockUnit, L.Unit) As Unit, U.DecimalPlaces, 
                    0 AS Opening,
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End AS Qty_Rec, 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End As Qty_Iss, 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End AS Closing, 
                    L.Rate as TransactionRate, "

            If ReportFrm.FGetText(rowValuation) = "Master Purchase Rate" Then
                mMainQry = mMainQry & " Sku.PurchaseRate " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as ValuationRate, 
                    IfNull((Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End)*(Sku.PurchaseRate),0) " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as Amount "
            ElseIf ReportFrm.FGetText(rowValuation) = "Last Purchase Rate" Then
                mMainQry = mMainQry & " (Case When RList.Code Is Not Null Then RList.Cost Else 
                    Sku.LastPurchaseRate End) " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as ValuationRate, 
                    IfNull((Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End)*(Case When RList.Code Is Not Null Then RList.Cost Else Sku.LastPurchaseRate End),0) 
                    " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as Amount "
            Else
                mMainQry = mMainQry & " 0 as ValuationRate, 0 as Amount "
            End If

            mMainQry = mMainQry & " FROM " & bTableName & " L
                    LEFT JOIN Item Sku ON L.Item = Sku.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    Left Join Item IC On IfNull(Sku.ItemCategory,Sku.code) = IC.Code
                    LEFT JOIN Item I ON IfNull(Sku.BaseItem, Sku.Code) = I.Code
                    LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sku.Size = Size.Code
                    LEFT JOIN SubGroup Prc On L.Process = Prc.SubCode
                    Left Join Unit U On L.Unit = U.Code
                    LEFT JOIN Unit Su On Sku.StockUnit = Su.Code 
                    LEFT JOIN UnitConversion Uc On L.Item = Uc.Item And L.Unit = Uc.FromUnit And Sku.StockUnit = Uc.ToUnit
                    Left Join viewHelpSubgroup Sg on L.Subcode = Sg.Code
                    LEFT JOIN Voucher_type vt ON L.V_Type = vt.V_Type 
                    Left Join Subgroup Location On L.Godown = Location.Subcode
                    "
            mMainQry += " Left Join (
                                     Select Max(RLD.Code) as Code, RLD.ItemCategory, RLD.Dimension1, RLD.Size, Max(RLD.Cost) as Cost 
                                     From RateListDetail RLD 
                                     Left Join RateList RL On RLD.Code = RL.Code 
                                     Where RLD.Process='PSales' 
                                     And RL.RateCategory is Null
                                     Group By RLD.ItemCategory, RLD.Dimension1, RLD.Size
                                     ) as RList On IC.Code = RList.ItemCategory and D1.Code = RList.Dimension1 and size.Code = RList.Size "
            mMainQry = mMainQry & "WHERE Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " " & mCondStr & "  "

            Dim bGroupOn As String = ""
            If ReportFrm.FGetCode(rowGroupOn) <> "" Then
                bGroupOn = ReportFrm.FGetCode(rowGroupOn).ToString.Replace("'", "")
            Else
                bGroupOn = "ItemCategoryCode,ItemGroupCode,ItemCode,Dimension1Code,Dimension2Code,Dimension3Code,Dimension4Code,SizeCode"
            End If

            If ReportFrm.FGetText(rowReportType) = "Stock Summary" Or ReportFrm.FGetText(rowReportType) = "Stock Summary With Valuation" Or ReportFrm.FGetText(rowReportType) = "Stock Balance" Then
                mQry = " Select Max(VMain.SkuCode) As SearchCode 
                    " & IIf(bGroupOn.Contains("ItemCategoryCode"), ", ItemCategoryCode, Max(VMain.ItemCategoryName) as ItemCategory", "") & " 
                    " & IIf(bGroupOn.Contains("ItemGroupCode"), ", ItemGroupCode, Max(VMain.ItemGroupName) as ItemGroup", "") & " 
                    " & IIf(bGroupOn.Contains("ItemCode"), ", ItemCode, Max(VMain.ItemName) as Item", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension1Code"), ", Dimension1Code, Max(VMain.Dimension1Name) as Dimension1", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension2Code"), ", Dimension2Code, Max(VMain.Dimension2Name) as Dimension2", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension3Code"), ", Dimension3Code, Max(VMain.Dimension3Name) as Dimension3", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension4Code"), ", Dimension4Code, Max(VMain.Dimension4Name) as Dimension4", "") & " 
                    " & IIf(bGroupOn.Contains("SizeCode"), ", SizeCode, Max(VMain.SizeName) as Size", "") & " 
                    " & IIf(bGroupOn.Contains("HSN"), ", HSN ", "") & " 
                    " & IIf(bGroupOn.Contains("LotNo"), ", LotNo ", "") & " 
                    " & IIf(bGroupOn.Contains("ProcessCode"), ", ProcessCode, Max(VMain.ProcessName) as Process", "") & " 
                    , Max(VMain.Unit) as Unit,"

                If ReportFrm.FGetText(rowReportType) = "Stock Summary With Valuation" Then
                    mQry += " Round(Max(VMain.ValuationRate),2) as [Rate], 
                              Round(Sum(VMain.Opening),Max(VMain.DecimalPlaces)) As [Opening], Round(Round(Sum(VMain.Opening),Max(VMain.DecimalPlaces))*Max(VMain.ValuationRate),2) As [OpeningValue] , 
                              Round(Sum(VMain.Qty_Rec),Max(VMain.DecimalPlaces)) as [ReceiveQty], Round(Round(Sum(VMain.Qty_Rec),Max(VMain.DecimalPlaces))*Max(VMain.ValuationRate),2) as [ReceiveValue],  
                              Round(Sum(VMain.Qty_Iss),Max(VMain.DecimalPlaces)) as [IssueQty], Round(Round(Sum(VMain.Qty_Iss),Max(VMain.DecimalPlaces))*Max(VMain.ValuationRate),2) as [IssueValue],
                              Round(Sum(VMain.Closing), IfNull(Max(VMain.DecimalPlaces), 0)) As [Closing], Round(Round(Sum(VMain.Closing), IfNull(Max(VMain.DecimalPlaces), 0))*Max(VMain.ValuationRate),2) as [ClosingValue]
                              From (" & mMainQry & ") As VMain
                              GROUP By " & bGroupOn & ""

                Else
                    If ReportFrm.FGetText(rowReportType) = "Stock Summary" Then
                        mQry += " Round(Sum(VMain.Opening),Max(VMain.DecimalPlaces)) As [Opening], Round(Sum(VMain.Qty_Rec),Max(VMain.DecimalPlaces)) as [ReceiveQty], Round(Sum(VMain.Qty_Iss),Max(VMain.DecimalPlaces)) as [IssueQty],"
                    End If

                    mQry += " Round(Sum(VMain.Closing), IfNull(Max(VMain.DecimalPlaces),0)) as [Closing], Sum(VMain.Amount) as Amount
                    From (" & mMainQry & ") As VMain
                    GROUP By " & bGroupOn & ""
                End If


                'If ReportFrm.FGetText(rowReportType) = "Stock Balance" Then
                '    mQry += " Having Sum(VMain.Closing) <> 0 "
                'End If

                If UCase(ReportFrm.FGetText(rowShowZeroBalance)) = "NO" Then
                        mQry += " Having Sum(VMain.Closing) <> 0 "
                    End If

                    mQry += " Order By 1
                    " & IIf(bGroupOn.Contains("ItemCategoryCode"), ", ItemCategory", "") & " 
                    " & IIf(bGroupOn.Contains("ItemGroupCode"), ", ItemGroup", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension1Code"), ", Dimension1", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension2Code"), ", Dimension2", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension3Code"), ", Dimension3", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension4Code"), ", Dimension4", "") & " 
                    " & IIf(bGroupOn.Contains("SizeCode"), ", Size", "") & " 
                    " & IIf(bGroupOn.Contains("HSn"), ", HSN", "") & "
                    " & IIf(bGroupOn.Contains("LotNo"), ", LotNo", "") & "
                    " & IIf(bGroupOn.Contains("ProcessCode"), ", Process", "") & " 
                    "
                Else
                    mQry = " Select VMain.DocID As SearchCode 
                    , Max(VMain.V_Date) As [Doc Date], Max(VMain.V_Type) as DocType, Max(VMain.RecId) As [Doc No]
                    , Max(Vmain.PartyName) as PartyName, Max(VMain.LocationName) As [Location Name]
                    , Round(Sum(VMain.Qty_Rec),4) as [Receive Qty]
                    , Round(Sum(VMain.Qty_Iss),4) as [Issue Qty]
                    , Round(Sum(VMain.Closing),4) as [Balance] 
                    , Max(VMain.Unit) as Unit
                    , Round(Max(VMain.TransactionRate),3) as TransactionRate                    
                    , Round(Max(VMain.ValuationRate),3) as ValuationRate                    
                    , Round(Sum(VMain.Amount),2) as Amount                    
                    From (" & mMainQry & ") As VMain
                    GROUP By VMain.DocID--, VMain.ItemCode,VMain.TransactionRate, VMain.ValuationRate                     
                    Order By Max(VMain.ItemName), Max(VMain.V_Date_ActualFormat), Max(Cast(Replace(VMain.RecID,'-','') as Integer)), Max(VMain.Qty_Rec), Max(VMain.Qty_Iss)"
            End If


            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If AgL.IsTableExist(bTempTableName.Replace("[", "").Replace("]", ""), AgL.GCn) Then
                mQry = "Drop Table " + bTempTableName
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If



            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            If ReportFrm.FilterGrid.Rows(rowItemType).Visible = False Then
                ReportFrm.Text = "Stock Report - " + ReportFrm.FilterGrid.Item(GFilter, rowItemType).Value + ReportFrm.FGetText(rowReportType)
            Else
                ReportFrm.Text = "Stock Report - " + ReportFrm.FGetText(rowReportType)
            End If

            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcStockReport"
            ReportFrm.AllowAutoResizeRows = False

            ReportFrm.ProcFillGrid(DsHeader)

            If ReportFrm.DGL1.Columns.Contains("Item Category Code") Then ReportFrm.DGL1.Columns("Item Category Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Item Group Code") Then ReportFrm.DGL1.Columns("Item Group Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Item Code") Then ReportFrm.DGL1.Columns("Item Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Dimension1Code") Then ReportFrm.DGL1.Columns("Dimension1Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Dimension2Code") Then ReportFrm.DGL1.Columns("Dimension2Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Dimension3Code") Then ReportFrm.DGL1.Columns("Dimension3Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Dimension4Code") Then ReportFrm.DGL1.Columns("Dimension4Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Size Code") Then ReportFrm.DGL1.Columns("Size Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Process Code") Then ReportFrm.DGL1.Columns("Process Code").Visible = False



            If ReportFrm.FGetText(rowReportType) = "Stock Ledger" Then
                Dim I As Integer
                Dim mRunningBal As Double
                mRunningBal = 0
                For I = 0 To ReportFrm.DGL1.RowCount - 1
                    mRunningBal += AgL.VNull(ReportFrm.DGL1.Item("Balance", I).Value)
                    ReportFrm.DGL1.Item("Balance", I).Value = mRunningBal
                    ReportFrm.DGL1.Item("Amount", I).Value = AgL.VNull(ReportFrm.DGL1.Item("Balance", I).Value) * AgL.VNull(ReportFrm.DGL1.Item("Valuation Rate", I).Value)
                Next
            End If

            If ReportFrm.FGetText(rowReportType) = "Stock Balance" Then
                If ReportFrm.DGL1.Columns.Contains("Closing") Then ReportFrm.DGL1.Columns("Closing").Visible = True
            ElseIf ReportFrm.FGetText(rowReportType) = "Stock Summary" Then
                If ReportFrm.DGL1.Columns.Contains("Opening") Then ReportFrm.DGL1.Columns("Opening").Visible = True
                If ReportFrm.DGL1.Columns.Contains("ReceiveQty") Then ReportFrm.DGL1.Columns("ReceiveQty").Visible = True
                If ReportFrm.DGL1.Columns.Contains("IssueQty") Then ReportFrm.DGL1.Columns("IssueQty").Visible = True
                If ReportFrm.DGL1.Columns.Contains("Closing") Then ReportFrm.DGL1.Columns("Closing").Visible = True
            ElseIf ReportFrm.FGetText(rowReportType) = "Stock Ledger" Then
                If ReportFrm.DGL1.Columns.Contains("Receive Qty") Then ReportFrm.DGL1.Columns("Receive Qty").Visible = True
                If ReportFrm.DGL1.Columns.Contains("Issue Qty") Then ReportFrm.DGL1.Columns("Issue Qty").Visible = True
                If ReportFrm.DGL1.Columns.Contains("Balance") Then ReportFrm.DGL1.Columns("Balance").Visible = True
            End If

            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
            Next
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
    Private Sub FGetItemWithRecipe(bTempTableName As String, bFromTableName As String)
    End Sub
End Class
