Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsSaleSummary
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
    Dim rowGroupOn As Integer = 1
    Dim rowFromDate As Integer = 2
    Dim rowToDate As Integer = 3
    Dim rowParty As Integer = 4
    Dim rowAgent As Integer = 5
    Dim rowItemType As Integer = 6
    Dim rowItemCategory As Integer = 7
    Dim rowItemGroup As Integer = 8
    Dim rowItem As Integer = 9
    Dim rowDimension1 As Integer = 10
    Dim rowDimension2 As Integer = 11
    Dim rowDimension3 As Integer = 12
    Dim rowDimension4 As Integer = 13
    Dim rowSize As Integer = 14
    Dim rowSupplierCity As Integer = 15
    Dim rowVucherType As Integer = 16
    Dim rowSite As Integer = 17
    Dim rowDivision As Integer = 18

    Dim Col1ItemCategoryCode = "Item Category Code"
    Dim Col1ItemCategory = "Item Category"
    Dim Col1ItemGroupCode = "Item Group Code"
    Dim Col1ItemGroup = "Item Group"

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
    Dim mHelpDimension1Qry$ = "Select 'o' As Tick, Code, Description From Dimension1 "
    Dim mHelpDimension2Qry$ = "Select 'o' As Tick, Code, Description From Dimension2 "
    Dim mHelpDimension3Qry$ = "Select 'o' As Tick, Code, Description From Dimension3 "
    Dim mHelpDimension4Qry$ = "Select 'o' As Tick, Code, Description From Dimension4 "
    Dim mHelpSizeQry$ = "Select 'o' As Tick, Code, Description From Size "
    Dim mHelpVoucherTypeQry$ = "SELECT 'o' As Tick, H.V_Type AS Code, H.Description FROM Voucher_Type H  "

    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Doc.Header Wise Detail' as Code, 'Doc.Header Wise Detail' as Name 
                    Union All 
                    Select 'Doc.Line Detail' as Code, 'Doc.Line Detail' as Name 
                    Union All 
                    Select 'Summary' as Code, 'Summary' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Doc.Header Wise Detail",,, 300)
            mQry = "SELECT 'o' As Tick, 'Month' As Code, 'Month' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'V_Date' As Code, 'Date' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'DivisionCode' As Code, 'Division' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'StateCode' As Code, 'State' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'CityCode' As Code, 'City' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'PartyCode' As Code, 'Party' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'AgentCode' As Code, 'Agent' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'SalesTaxGroupItem' As Code, 'Sales Tax Group Item' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'HSN' As Code, 'HSN' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'ItemCategoryCode' As Code, 'Item Category' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'ItemGroupCode' As Code, 'Item Group' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'ItemCode' As Code, 'Item Name' As Name "

            If ClsMain.IsScopeOfWorkContains(IndustryType.SubIndustryType.AadhatModule) Then
                mQry = mQry & " Union All "
                mQry = mQry & " SELECT 'o' As Tick, 'SupplierCity' As Code, 'Supplier City' As Name "
            End If
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
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)
            ReportFrm.CreateHelpGrid("ItemType", AgL.PubCaptionItemType, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemTypeQry)
            ReportFrm.CreateHelpGrid("ItemCategory", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
            ReportFrm.CreateHelpGrid("ItemGroup", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
            ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
            ReportFrm.CreateHelpGrid("Dimension1", "Dimension1", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension1Qry)
            ReportFrm.CreateHelpGrid("Dimension2", "Dimension2", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension2Qry)
            ReportFrm.CreateHelpGrid("Dimension3", "Dimension3", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension3Qry)
            ReportFrm.CreateHelpGrid("Dimension4", "Dimension4", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension4Qry)
            ReportFrm.CreateHelpGrid("Size", "Size", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSizeQry)
            ReportFrm.CreateHelpGrid("SupplierCity", "Supplier City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsMain.FGetVoucher_TypeQry("SaleInvoice", Ncat.SaleInvoice + "," + Ncat.SaleReturn))
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")

            ReportFrm.FilterGrid.Rows(rowDimension1).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1), Boolean)
            ReportFrm.FilterGrid.Rows(rowDimension2).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2), Boolean)
            ReportFrm.FilterGrid.Rows(rowDimension3).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3), Boolean)
            ReportFrm.FilterGrid.Rows(rowDimension4).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4), Boolean)
            ReportFrm.FilterGrid.Rows(rowSize).Visible = CType(ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Size), Boolean)

            If ClsMain.FDivisionNameForCustomization(12) = "MAA KI KRIPA" Or ClsMain.FDivisionNameForCustomization(16) = "KAMAKHYA TRADERS" Then
                ReportFrm.FilterGrid.Rows(rowSupplierCity).Visible = True
            Else
                ReportFrm.FilterGrid.Rows(rowSupplierCity).Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcSaleSummary()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcSaleSummary(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Sales Summary"

            Dim bGroupOn As String = ""
            If ReportFrm.FGetCode(rowGroupOn) <> "" Then
                bGroupOn = ReportFrm.FGetCode(rowGroupOn).ToString.Replace("'", "")
            Else
                bGroupOn = "ItemCategoryCode,ItemGroupCode,ItemCode,Dimension1Code,Dimension2Code,Dimension3Code,Dimension4Code,SizeCode"
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If ReportFrm.FGetText(rowReportType) = "Summary" Then
                    If bGroupOn.Contains("ItemCategoryCode") Then
                        mFilterGrid.Item(GFilterCode, rowItemCategory).Value = "'" + mGridRow.Cells(Col1ItemCategoryCode).Value + "'"
                        mFilterGrid.Item(GFilter, rowItemCategory).Value = mGridRow.Cells(Col1ItemCategory).Value
                    End If
                    If bGroupOn.Contains("ItemGroupCode") Then
                        mFilterGrid.Item(GFilterCode, rowItemGroup).Value = "'" + mGridRow.Cells(Col1ItemGroupCode).Value + "'"
                        mFilterGrid.Item(GFilter, rowItemGroup).Value = mGridRow.Cells(Col1ItemGroup).Value
                    End If
                    If bGroupOn.Contains("ItemCode") Then
                        mFilterGrid.Item(GFilterCode, rowItem).Value = "'" + mGridRow.Cells("Item Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowItem).Value = mGridRow.Cells("Item").Value
                    End If
                    If bGroupOn.Contains("Dimension1Code") Then
                        mFilterGrid.Item(GFilterCode, rowDimension1).Value = "'" + mGridRow.Cells("Dimension1 Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowDimension1).Value = mGridRow.Cells("Dimension1").Value
                    End If
                    If bGroupOn.Contains("Dimension2Code") Then
                        mFilterGrid.Item(GFilterCode, rowDimension2).Value = "'" + mGridRow.Cells("Dimension2 Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowDimension2).Value = mGridRow.Cells("Dimension2").Value
                    End If
                    If bGroupOn.Contains("Dimension3Code") Then
                        mFilterGrid.Item(GFilterCode, rowDimension3).Value = "'" + mGridRow.Cells("Dimension3 Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowDimension3).Value = mGridRow.Cells("Dimension3").Value
                    End If
                    If bGroupOn.Contains("Dimension4Code") Then
                        mFilterGrid.Item(GFilterCode, rowDimension4).Value = "'" + mGridRow.Cells("Dimension4 Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowDimension4).Value = mGridRow.Cells("Dimension4").Value
                    End If
                    If bGroupOn.Contains("SizeCode") Then
                        mFilterGrid.Item(GFilterCode, rowSize).Value = "'" + mGridRow.Cells("Size Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowSize).Value = mGridRow.Cells("Size").Value
                    End If

                    If bGroupOn.Contains("DivisionCode") Then
                        mFilterGrid.Item(GFilterCode, rowDivision).Value = "'" + mGridRow.Cells("Division Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowDivision).Value = mGridRow.Cells("Division").Value
                    End If

                    If bGroupOn.Contains("PartyCode") Then
                        mFilterGrid.Item(GFilterCode, rowParty).Value = "'" + mGridRow.Cells("Party Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowParty).Value = mGridRow.Cells("Party").Value
                    End If

                    If bGroupOn.Contains("AgentCode") Then
                        mFilterGrid.Item(GFilterCode, rowAgent).Value = "'" + mGridRow.Cells("Agent Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowAgent).Value = mGridRow.Cells("Agent").Value
                    End If

                    If bGroupOn.Contains("SupplierCity") Then
                        mFilterGrid.Item(GFilterCode, rowSupplierCity).Value = "'" + mGridRow.Cells("Supplier City Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowSupplierCity).Value = mGridRow.Cells("Supplier City").Value
                    End If

                    mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Line Detail"
                    mFilterGrid.Item(GFilterCode, rowGroupOn).Value = ""
                    mFilterGrid.Item(GFilter, rowGroupOn).Value = ""


                    'If ReportFrm.DGL1.Columns.Contains(Col1ItemCategoryCode) Then
                    '    mFilterGrid.Item(GFilterCode, rowItemCategory).Value = "'" + mGridRow.Cells(Col1ItemCategoryCode).Value + "'"
                    '    mFilterGrid.Item(GFilter, rowItemCategory).Value = mGridRow.Cells(Col1ItemCategory).Value
                    '    mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Line Detail"
                    '    mFilterGrid.Item(GFilterCode, rowGroupOn).Value = ""
                    '    mFilterGrid.Item(GFilter, rowGroupOn).Value = ""
                    'End If

                    'If ReportFrm.DGL1.Columns.Contains(Col1ItemGroupCode) Then
                    '    mFilterGrid.Item(GFilterCode, rowItemGroup).Value = "'" + mGridRow.Cells(Col1ItemGroupCode).Value + "'"
                    '    mFilterGrid.Item(GFilter, rowItemGroup).Value = mGridRow.Cells(Col1ItemGroup).Value
                    '    mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Line Detail"
                    '    mFilterGrid.Item(GFilterCode, rowGroupOn).Value = ""
                    '    mFilterGrid.Item(GFilter, rowGroupOn).Value = ""
                    'End If
                ElseIf ReportFrm.FGetText(rowReportType) = "Doc.Header Wise Detail" Or
                    ReportFrm.FGetText(rowReportType) = "Doc.Line Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                End If
            End If

            mCondStr = " Where 1=1 "
            mCondStr = mCondStr & " And Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "


            'mCondStr = mCondStr & "And Sku.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "','" & ItemTypeCode.InternalProduct & "')"
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemType", rowItemType)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("IfNull(Sku.ItemCategory,Ls.ItemCategory)", rowItemCategory)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("IfNull(Sku.ItemGroup,Ls.ItemGroup)", rowItemGroup)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension1", rowDimension1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension2", rowDimension2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension3", rowDimension3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension4", rowDimension4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Size", rowSize)

            If AgL.XNull(ReportFrm.FGetText(rowItem)) <> "" Then
                mCondStr = mCondStr & ReportFrm.GetWhereCondition("IfNull(Sku.BaseItem,Sku.Code)", rowItem)
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Supp.CityCode", rowSupplierCity)

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", rowVucherType)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")

            Dim mMainQry As String = ""
            mMainQry = " SELECT H.DocID, L.Sr, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.V_Type + '-' + H.ManualRefNo As EntryNo, H.V_Type, D.Div_Code As DivisionCode,
                    H.Site_Code, Site.Name as Site,
                    H.SaleToParty As PartyCode, H.SalesTaxGroupParty, Sku.Code AS SkuCode, Sku.Description AS SkuName, 
                    IfNull(Sku.ItemCategory,Ls.ItemCategory) As ItemCategoryCode, 
                    IfNull(Sku.ItemGroup,Ls.ItemGroup) As ItemGroupCode, 
                    Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Code Else Sku.Code End as ItemCode,
                    Sku.Dimension1 As Dimension1Code, 
                    Sku.Dimension2 As Dimension2Code, Sku.Dimension3 As Dimension3Code, 
                    Sku.Dimension4 As Dimension4Code, Sku.Size As SizeCode,
                    L.SalesTaxGroupItem, 
                    H.SaleToPartyName As PartyName, H.SaleToPartyMobile as Mobile, H.SaleToPartySalesTaxNo as PartyGstNo,
                    Agent.Code As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    SuppCity.CityCode as SupplierCityCode, SuppCity.CityName as SupplierCityName,
                    H.SaleToPartyDocNo as PartyInvoiceNo, H.SaleToPartyDocDate As PartyInvoiceDate, H.ManualRefNo as ManualRefNo, 
                    L.Item, I.Specification As ItemSpecification, IfNull(I.HSN,Ic.HSN) As HSN, D.Div_Name As DivisionName,
                    IC.Description As ItemCategoryName, IG.Description As ItemGroupName, 
                    Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Specification Else Sku.Specification End as ItemName, 
                    D1.Description as Dimension1Name, D2.Description as Dimension2Name,
                    D3.Description as Dimension3Name, D4.Description as Dimension4Name, Size.Description as SizeName,
                    (Case When L.DiscountPer = 0 Then '' else Cast(L.DiscountPer as nVarchar) End)  + (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) + (Case When L.AdditionalDiscountPer=0 Then '' else Cast(L.AdditionalDiscountPer  as nVarchar) End) as DiscountPer, 
                    L.DiscountAmount + L.AdditionalDiscountAmount as Discount, 
                    L.Taxable_Amount, L.Net_Amount, L.Qty, L.Unit, 
                    L.DealQty, L.DealUnit,
                    L.Rate, L.Amount +(L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) as AmountExDiscount, L.Amount,
                    L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax, 0 As Stock,
                    " & IIf(AgL.PubServerName = "", "strftime('%m-%Y',V_Date)  ", "Substring(Convert(NVARCHAR, H.V_Date,103),4,7)") & " As Month
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
                    LEFT JOIN SaleInvoiceDetailSku Ls On L.DocId = Ls.DocId And L.Sr = Ls.Sr
                    Left Join SaleInvoiceTransport HT On H.DocId = HT.DocID
                    Left Join viewHelpSubgroup Transporter On HT.Transporter = Transporter.Code
                    LEFT JOIN Item Sku ON Sku.Code = L.Item
                    LEFT JOIN ItemType It On Sku.ItemType = It.Code
                    Left Join Item IC On IfNull(Sku.ItemCategory,Ls.ItemCategory) = IC.Code
                    Left Join Item IG On IfNull(Sku.ItemGroup,Ls.ItemGroup) = IG.Code
                    LEFT JOIN Item I ON Sku.BaseItem = I.Code
                    LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sku.Size = Size.Code
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code                     
                    Left Join viewHelpSubgroup Sg On H.BillToParty = Sg.Code                     
                    Left Join (Select SILTV.Subcode,SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code ) as LTV On Party.code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On H.SaleToPartyCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN viewHelpSubgroup Supp On Ig.DefaultSupplier = Supp.Code
                    Left Join City SuppCity On SuppCity.CityCode = Supp.CityCode 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


            If ReportFrm.FGetText(rowReportType) = "Summary" Then
                mMainQry += " UNION ALL "

                mMainQry += "  SELECT NULL AS DocID, NULL AS Sr, NULL As V_Date, NULL As V_Date_ActualFormat,
                    NULL AS EntryNo, Null As V_Type, D.Div_Code As DivisionCode,
                    Null As Site_Code, Null As Site,
                    NULL AS PartyCode, Null As SalesTaxGroupParty, Sku.Code AS SkuCode, Sku.Description AS SkuName, 
                    Sku.ItemCategory As ItemCategoryCode, Sku.ItemGroup As ItemGroupCode, 
                    Case When Sku.V_Type = 'SKU' Then I.Code Else Sku.Code End as ItemCode,
                    Sku.Dimension1 As Dimension1Code, 
                    Sku.Dimension2 As Dimension2Code, Sku.Dimension3 As Dimension3Code, 
                    Sku.Dimension4 As Dimension4Code, Sku.Size As SizeCode,
                    L.SalesTaxGroupItem, 
                    NULL As PartyName, Null as Mobile, NULL as PartyGstNo,
                    NULL As AgentCode, NULL As AgentName , 
                    NULL AS CityCode, NULL AS CityName, NULL AS StateCode, NULL AS StateName,
                    Null as SupplierCityCode, Null as SupplierCityName,
                    NULL AS PartyInvoiceNo, NULL AS PartyInvoiceDate, 
                    NULL AS ManualRefNo, 
                    L.Item, I.Specification As ItemSpecification, IfNull(I.HSN,Ic.HSN) As HSN, D.Div_Name As DivisionName,
                    IC.Description As ItemCategoryName, IG.Description As ItemGroupName, 
                    Case When Sku.V_Type = 'SKU' Then I.Specification Else Sku.Specification End as ItemName, 
                    D1.Description as Dimension1Name, D2.Description as Dimension2Name,
                    D3.Description as Dimension3Name, D4.Description as Dimension4Name, Size.Description as SizeName,
                    0 AS DiscountPer, 
                    0 AS Discount, 
                    0 AS Taxable_Amount, 0 AS Net_Amount, 0 AS Qty, NULL AS Unit, 
                    0 AS DealQty, NULL  AS DealUnit,
                    0 AS Rate, 0 as AmountExDiscount, 0 AS Amount,
                    0 AS Tax1, 0 AS Tax2, 0 AS Tax3, 0 AS Tax4, 0 AS Tax5, 0 TotalTax, 
                    IsNull(L.Qty_Rec,0) - IsNull(L.Qty_Iss,0) AS Stock,
                    Null As Month
                    FROM Stock L 
                    LEFT JOIN Item Sku ON Sku.Code = L.Item
                    LEFT JOIN ItemType It On Sku.ItemType = It.Code
                    Left Join Item IC On Sku.ItemCategory = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item I ON Sku.BaseItem = I.Code
                    LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sku.Size = Size.Code
                    LEFT JOIN Division D On L.Div_Code = D.Div_Code "
            End If



            Dim mSaleInvoicePaymentQry As String = ""
            Dim DtSaleInvoicePayment As DataTable
            If (Not bGroupOn.Contains("Item") And Not bGroupOn.Contains("Dimension") And Not bGroupOn.Contains("Size")) Or ReportFrm.FGetText(rowReportType) = "Doc.Header Wise Detail" Then
                mQry = " SELECT Code, Description  FROM PaymentMode "
                Dim DtPaymentMode As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                mSaleInvoicePaymentQry = "SELECT Sip.DocID As PaymentDocId "
                For I As Integer = 0 To DtPaymentMode.Rows.Count - 1
                    mSaleInvoicePaymentQry += ", Sum(CASE WHEN Pm.Code = '" & DtPaymentMode.Rows(I)("Code") & "' And Sip.Amount > 0 THEN Sip.Amount ELSE 0 END) As [" & DtPaymentMode.Rows(I)("Description") & "] "
                    mSaleInvoicePaymentQry += ", Sum(CASE WHEN Pm.Code = '" & DtPaymentMode.Rows(I)("Code") & "' And Sip.Amount < 0 THEN Sip.Amount ELSE 0 END) As [" & DtPaymentMode.Rows(I)("Description") & " Refund" & "] "
                Next
                mSaleInvoicePaymentQry += "FROM SaleInvoicePayment Sip
                    LEFT JOIN PaymentMode Pm ON Sip.PaymentMode = Pm.Code
                    Group By Sip.DocId "
                DtSaleInvoicePayment = AgL.FillData(mSaleInvoicePaymentQry, AgL.GCn).Tables(0)

                Dim ColumnRemoveArr(DtSaleInvoicePayment.Columns.Count) As String
                For M As Integer = 0 To DtSaleInvoicePayment.Columns.Count - 1
                    If AgL.StrCmp(DtSaleInvoicePayment.Columns(M).ColumnName, "PaymentDocId") = False Then
                        If DtSaleInvoicePayment.Compute("SUM([" & DtSaleInvoicePayment.Columns(M).ColumnName & "])", "") = 0 Then
                            ColumnRemoveArr(M) = DtSaleInvoicePayment.Columns(M).ColumnName
                        End If
                    End If
                Next

                If ColumnRemoveArr IsNot Nothing Then
                    For M As Integer = 0 To ColumnRemoveArr.Length - 1
                        If AgL.XNull(ColumnRemoveArr(M)) <> "" Then
                            DtSaleInvoicePayment.Columns.Remove(DtSaleInvoicePayment.Columns(ColumnRemoveArr(M)))
                        End If
                    Next
                End If
            Else
                mSaleInvoicePaymentQry = " Select Null As PaymentDocId "
            End If


            If ReportFrm.FGetText(rowReportType) = "Doc.Header Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.DivisionName) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As InvoiceDate, Max(VMain.V_Type) as DocType, Max(VMain.EntryNo) As EntryNo,
                    Max(VMain.PartyName) As Party, Max(VMain.Mobile) as Mobile, Max(C.Description) as CatalogName, Max(VMain.SalesTaxGroupParty) As SalesTaxGroupParty, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount "

                If DtSaleInvoicePayment IsNot Nothing Then
                    For I As Integer = 0 To DtSaleInvoicePayment.Columns.Count - 1
                        If DtSaleInvoicePayment.Columns(I).ColumnName <> "PaymentDocId" Then
                            mQry += ", Max(VPayment.[" & DtSaleInvoicePayment.Columns(I).ColumnName & "]) As [" & DtSaleInvoicePayment.Columns(I).ColumnName & "]"
                        End If
                    Next
                End If

                mQry += " From (" & mMainQry & ") As VMain
                    LEFT JOIN (" & mSaleInvoicePaymentQry & ") As VPayment On VMain.DocId = VPayment.PaymentDocId
                    Left Join (SELECT L1.docId, Max(L1.Catalog) AS Catalog FROM SaleInvoiceDetail L1 GROUP BY L1.DocID) as L On VMain.DocId = L.DocId
                    Left join Catalog C On L.Catalog = C.Code
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
            ElseIf ReportFrm.FGetText(rowReportType) = "Doc.Line Detail" Then
                mQry = " Select VMain.DocId As SearchCode, VMain.DivisionName as Division, Vmain.Site as Site, VMain.V_Date As InvoiceDate, VMain.V_Type as DocType, VMain.EntryNo As EntryNo,
                    VMain.PartyName As Party, VMain.SalesTaxGroupParty As SalesTaxGroupParty, 
                    VMain.ItemName As Item, VMain.Dimension1Name As Dimension1, VMain.Dimension2Name As Dimension2, 
                    VMain.Dimension3Name As Dimension3, VMain.Dimension4Name As Dimension4, VMain.SizeName As Size,
                    IfNull(VMain.AmountExDiscount,0) As AmountExDiscount, IfNull(VMain.Discount,0) As Discount,
                    IfNull(VMain.Amount,0) As Amount,IfNull(VMain.Taxable_Amount,0) As TaxableAmount, IfNull(VMain.TotalTax,0) As TaxAmount, IfNull(VMain.Net_Amount,0) As NetAmount 
                    From(" & mMainQry & ") As VMain
                    LEFT JOIN (" & mSaleInvoicePaymentQry & ") As VPayment On VMain.DocId = VPayment.PaymentDocId
                    Order By VMain.V_Date_ActualFormat, Cast(Replace(Vmain.ManualRefNo,'-','') as Integer) "
            ElseIf ReportFrm.FGetText(rowReportType) = "Summary" Then
                mQry = " Select Max(VMain.SkuCode) As SearchCode
                    " & IIf(bGroupOn.Contains("Month"), ", " & IIf(AgL.PubServerName = "", "Max(strftime('%m-%Y',VMain.V_Date_ActualFormat))", "Max(Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7))") & " as Month", "") & " 
                    " & IIf(bGroupOn.Contains("Date"), ", Max(VMain.V_Date) as Date", "") & " 
                    " & IIf(bGroupOn.Contains("DivisionCode"), ", DivisionCode, Max(VMain.DivisionName) as Division", "") & " 
                    " & IIf(bGroupOn.Contains("StateCode"), ", StateCode, Max(VMain.StateName) as State", "") & " 
                    " & IIf(bGroupOn.Contains("CityCode"), ", CityCode, Max(VMain.CityName) as City", "") & " 
                    " & IIf(bGroupOn.Contains("PartyCode"), ", PartyCode, Max(VMain.PartyName) as Party", "") & " 
                    " & IIf(bGroupOn.Contains("AgentCode"), ", AgentCode, Max(VMain.AgentName) as Agent", "") & " 
                    " & IIf(bGroupOn.Contains("SalesTaxGroupItem"), ", SalesTaxGroupItem", "") & " 
                    " & IIf(bGroupOn.Contains("HSN"), ", HSN", "") & " 
                    " & IIf(bGroupOn.Contains("ItemCategoryCode"), ", ItemCategoryCode, Max(VMain.ItemCategoryName) as ItemCategory", "") & " 
                    " & IIf(bGroupOn.Contains("ItemGroupCode"), ", ItemGroupCode, Max(VMain.ItemGroupName) as ItemGroup", "") & " 
                    " & IIf(bGroupOn.Contains("ItemCode"), ", ItemCode, Max(VMain.ItemName) as Item", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension1Code"), ", Dimension1Code, Max(VMain.Dimension1Name) as Dimension1", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension2Code"), ", Dimension2Code, Max(VMain.Dimension2Name) as Dimension2", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension3Code"), ", Dimension3Code, Max(VMain.Dimension3Name) as Dimension3", "") & " 
                    " & IIf(bGroupOn.Contains("Dimension4Code"), ", Dimension4Code, Max(VMain.Dimension4Name) as Dimension4", "") & " 
                    " & IIf(bGroupOn.Contains("SizeCode"), ", SizeCode, Max(VMain.SizeName) as Size", "") & " 
                    " & IIf(bGroupOn.Contains("SupplierCity"), ", SupplierCityCode, Max(VMain.SupplierCity) as SupplierCity", "") & " 
                    ,Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Sum(VMain.AmountExDiscount) As AmountExDiscount, 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.Amount) as Amount,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount],
                    Sum(VMain.Stock) As [Stock]"

                If DtSaleInvoicePayment IsNot Nothing Then
                    For I As Integer = 0 To DtSaleInvoicePayment.Columns.Count - 1
                        If DtSaleInvoicePayment.Columns(I).ColumnName <> "PaymentDocId" Then
                            mQry += ", Sum(VPayment.[" & DtSaleInvoicePayment.Columns(I).ColumnName & "]) As [" & DtSaleInvoicePayment.Columns(I).ColumnName & "]"
                        End If
                    Next
                End If

                If (Not bGroupOn.Contains("Item") And Not bGroupOn.Contains("Dimension") And Not bGroupOn.Contains("Size") And Not bGroupOn.Contains("HSN")) Then
                    mQry += "From (Select VHeader.DocId , Max(VHeader.DivisionName) as DivisionName, Max(VHeader.DivisionCode) as DivisionCode, 
                            Max(VHeader.Site) as Site, Max(VHeader.V_Date) As V_Date, 
                            Max(VHeader.Month) As Month,
                            Max(VHeader.V_Date_ActualFormat) As V_Date_ActualFormat,
                            Max(VHeader.V_Type) as DocType, 
                            Max(VHeader.EntryNo) As EntryNo,
                            Max(VHeader.StateName) As StateName, Max(VHeader.StateCode) as StateCode, 
                            Max(VHeader.CityName) As CityName, Max(VHeader.CityCode) as CityCode, 
                            Max(VHeader.SupplierCityName) As SupplierCity, Max(VHeader.SupplierCityCode) as SupplierCityCode, 
                            Max(VHeader.PartyName) As PartyName, Max(VHeader.PartyCode) As PartyCode, 
                            Max(VHeader.AgentName) As AgentName, Max(VHeader.AgentCode) As AgentCode, 
                            Max(VHeader.SalesTaxGroupParty) As SalesTaxGroupParty, 
                            Sum(VHeader.Qty) As Qty, Sum(VHeader.DealQty) As DealQty, 
                            Max(VHeader.Unit) As Unit, Max(VHeader.DealUnit) As DealUnit, 
                            Max(VHeader.SkuCode) As SkuCode, Max(VHeader.DiscountPer) As DiscountPer, 
                            IfNull(Sum(VHeader.AmountExDiscount),0) As AmountExDiscount, IfNull(Sum(VHeader.Discount),0) As Discount,
                            IfNull(Sum(VHeader.Amount),0) As Amount,IfNull(Sum(VHeader.Taxable_Amount),0) As Taxable_Amount, 
                            IfNull(Sum(VHeader.TotalTax),0) As TotalTax, IfNull(Sum(VHeader.Net_Amount),0) As Net_Amount, 
                            IfNull(Sum(VHeader.Stock),0) As Stock
                            From(" & mMainQry & ") As VHeader
                            GROUP By VHeader.DocId) As VMain "
                Else
                    mQry += "From (" & mMainQry & ") As VMain "
                End If
            mQry += " LEFT JOIN (" & mSaleInvoicePaymentQry & ") As VPayment On VMain.DocId = VPayment.PaymentDocId
                    Where VMain.DocId Is Not Null
                    GROUP By " & bGroupOn & ""




                Dim mOrderBy As String = ""
                mOrderBy += IIf(bGroupOn.Contains("Month"), "Month,", "")
                mOrderBy += IIf(bGroupOn.Contains("V_Date"), "V_Date,", "")
                mOrderBy += IIf(bGroupOn.Contains("DivisionCode"), "Division,", "")
                mOrderBy += IIf(bGroupOn.Contains("StateCode"), "State,", "")
                mOrderBy += IIf(bGroupOn.Contains("CityCode"), "City,", "")
                mOrderBy += IIf(bGroupOn.Contains("PartyCode"), "Party,", "")
                mOrderBy += IIf(bGroupOn.Contains("AgentCode"), "Agent,", "")
                mOrderBy += IIf(bGroupOn.Contains("SalesTaxGroupItem"), "SalesTaxGroupItem,", "")
                mOrderBy += IIf(bGroupOn.Contains("HSN"), "HSN,", "")
                mOrderBy += IIf(bGroupOn.Contains("ItemCategoryCode"), "ItemCategory,", "")
                mOrderBy += IIf(bGroupOn.Contains("ItemGroupCode"), "ItemGroup,", "")
                mOrderBy += IIf(bGroupOn.Contains("Dimension1Code"), "Dimension1,", "")
                mOrderBy += IIf(bGroupOn.Contains("Dimension2Code"), "Dimension2,", "")
                mOrderBy += IIf(bGroupOn.Contains("Dimension3Code"), "Dimension3,", "")
                mOrderBy += IIf(bGroupOn.Contains("Dimension4Code"), "Dimension4,", "")
                mOrderBy += IIf(bGroupOn.Contains("SizeCode"), "Size,", "")
                mOrderBy += IIf(bGroupOn.Contains("SupplierCity"), "VMain.SupplierCity,", "")
                mQry = mQry + " Order By " + mOrderBy.Substring(0, mOrderBy.Length - 1)
            End If
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sale Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcSaleSummary"
            ReportFrm.AllowAutoResizeRows = False


            ReportFrm.ProcFillGrid(DsHeader)

            'If ReportFrm.DGL1.Columns.Contains("Doc Id") Then ReportFrm.DGL1.Columns("Doc Id").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Division Code") Then ReportFrm.DGL1.Columns("Division Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("State Code") Then ReportFrm.DGL1.Columns("State Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("City Code") Then ReportFrm.DGL1.Columns("City Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Party Code") Then ReportFrm.DGL1.Columns("Party Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Agent Code") Then ReportFrm.DGL1.Columns("Agent Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains(Col1ItemCategoryCode) Then ReportFrm.DGL1.Columns(Col1ItemCategoryCode).Visible = False
            If ReportFrm.DGL1.Columns.Contains("Item Group Code") Then ReportFrm.DGL1.Columns("Item Group Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Item Code") Then ReportFrm.DGL1.Columns("Item Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Dimension1Code") Then ReportFrm.DGL1.Columns("Dimension1Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Dimension2Code") Then ReportFrm.DGL1.Columns("Dimension2Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Dimension3Code") Then ReportFrm.DGL1.Columns("Dimension3Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Dimension4Code") Then ReportFrm.DGL1.Columns("Dimension4Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Size Code") Then ReportFrm.DGL1.Columns("Size Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Supplier City Code") Then ReportFrm.DGL1.Columns("Supplier City Code").Visible = False
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
