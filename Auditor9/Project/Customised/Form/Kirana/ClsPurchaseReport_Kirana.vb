Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsPurchaseReport_Kirana
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
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowParty As Integer = 3
    Dim rowItem As Integer = 4
    Dim rowSite As Integer = 5
    Dim rowVoucherType As Integer = 6
    Dim rowCashCredit As Integer = 7
    Dim rowAgent As Integer = 8
    Dim rowItemGroup As Integer = 9
    Dim rowItemCategory As Integer = 10
    Dim rowCity As Integer = 11
    Dim rowState As Integer = 12
    Dim rowSalesRep As Integer = 13
    Dim rowResponsiblePerson As Integer = 14
    Dim rowTag As Integer = 15
    Dim rowDivision As Integer = 16
    Dim rowUser As Integer = 17
    Dim rowHSN As Integer = 18
    Dim rowPartyTaxGroup As Integer = 19
    Dim rowItemTaxGroup As Integer = 20
    Dim rowCatalog As Integer = 21

    Public Const PurchaseOrderReport As String = "PurchaseOrderReport"

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
    Dim mHelpSalesRepresentativeQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Sales Representative] FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code ='SREP' "
    Dim mHelpResponsiblePersonQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code <>'SREP' "
    Dim mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description   FROM Tag H "
    Dim mHelpVoucherTypeQry$ = "SELECT 'o' As Tick, H.V_Type AS Code, H.Description FROM Voucher_Type H  "
    Dim mHelpPartyTaxGroup$ = "SELECT 'o' As Tick, H.Description AS Code, H.Description FROM PostingGroupSalesTaxParty H  "
    Dim mHelpItemTaxGroup$ = "SELECT 'o' As Tick, H.Description AS Code, H.Description FROM PostingGroupSalesTaxItem H  "
    Dim mHelpCatalog$ = "SELECT 'o' As Tick, H.Code, H.Description FROM Catalog H Order By H.Description "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpItemStateQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From Item Where V_Type = '" & ItemV_Type.ItemState & "' And IfNull(Status,'Active') = 'Active' "


    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Doc.Header Wise Detail' as Code, 'Doc.Header Wise Detail' as Name 
                            Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name 
                            Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name 
                            Union All Select 'Voucher Type Wise Summary' as Code, 'Voucher Type Wise Summary' as Name 
                            Union All Select 'HSN Wise Summary' as Code, 'HSN Wise Summary' as Name 
                            Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name 
                            Union All Select 'Item Group Wise Summary' as Code, 'Item Group Wise Summary' as Name 
                            Union All Select 'Item Category Wise Summary' as Code, 'Item Category Wise Summary' as Name 
                            Union All Select 'City Wise Summary' as Code, 'City Wise Summary' as Name 
                            Union All Select 'State Wise Summary' as Code, 'State Wise Summary' as Name
                            Union All Select 'Sales Representative Wise Summary' as Code, 'Sales Representative Wise Summary' as Name
                            Union All Select 'Responsible Person Wise Summary' as Code, 'Responsible Person Wise Summary' as Name
                            Union All Select 'User Wise Summary' as Code, 'User Wise Summary' as Name
                            Union All Select 'Party Tax Group Wise Summary' as Code, 'Party Tax Group Wise Summary' as Name
                            Union All Select 'Item Tax Group Wise Summary' as Code, 'Item Tax Group Wise Summary' as Name
                            Union All Select 'Division Wise Summary' as Code, 'Division Wise Summary' as Name
                            Union All Select 'Site Wise Summary' as Code, 'Site Wise Summary' as Name
                            "
            If ClsMain.FDivisionNameForCustomization(13) = "JAIN BROTHERS" Or ClsMain.FDivisionNameForCustomization(11) = "BOOK SHOPEE" Then
                mQry = mQry & " Union All Select 'Catalog Wise Summary' as Code, 'Catalog Wise Summary' as Name "
            End If

            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Item Wise Detail",,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Broker", "Broker", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
            ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            If GRepFormName = PurchaseOrderReport Then
                ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice", Ncat.PurchaseOrder + "," + Ncat.PurchaseOrderCancel))
            Else
                ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice", Ncat.PurchaseInvoice + "," + Ncat.PurchaseReturn))
            End If
            ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
            ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
            ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
            ReportFrm.CreateHelpGrid("SalesRepresentative", "Sales Representative", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesRepresentativeQry)
            ReportFrm.CreateHelpGrid("ResponsiblePerson", "ResponsiblePerson", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpResponsiblePersonQry)
            ReportFrm.CreateHelpGrid("Tag", "Tag", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTagQry)
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("User", "User", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpUserQry)
            ReportFrm.CreateHelpGrid("HSN", "HSN", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
            ReportFrm.CreateHelpGrid("Party Tax Group", "Party Tax Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyTaxGroup)
            ReportFrm.CreateHelpGrid("Item Tax Group", "Item Tax Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemTaxGroup)
            ReportFrm.CreateHelpGrid("Catalog", "Catalog", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCatalog)
            ReportFrm.CreateHelpGrid("Supplier City", "Supplier City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("Item State", "Item State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemStateQry)

            ReportFrm.FilterGrid.Rows(0).Visible = False
            ReportFrm.FilterGrid.Rows(5).Visible = False
            ReportFrm.FilterGrid.Rows(6).Visible = False
            ReportFrm.FilterGrid.Rows(7).Visible = False
            ReportFrm.FilterGrid.Rows(8).Visible = False
            ReportFrm.FilterGrid.Rows(9).Visible = False
            ReportFrm.FilterGrid.Rows(10).Visible = False
            ReportFrm.FilterGrid.Rows(11).Visible = False
            ReportFrm.FilterGrid.Rows(12).Visible = False
            ReportFrm.FilterGrid.Rows(13).Visible = False
            ReportFrm.FilterGrid.Rows(14).Visible = False
            ReportFrm.FilterGrid.Rows(15).Visible = False
            ReportFrm.FilterGrid.Rows(16).Visible = False
            ReportFrm.FilterGrid.Rows(17).Visible = False
            ReportFrm.FilterGrid.Rows(18).Visible = False
            ReportFrm.FilterGrid.Rows(19).Visible = False
            ReportFrm.FilterGrid.Rows(20).Visible = False
            ReportFrm.FilterGrid.Rows(21).Visible = False
            ReportFrm.FilterGrid.Rows(22).Visible = False
            ReportFrm.FilterGrid.Rows(23).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetVoucher_TypeQry(ByVal TableName As String, Optional NCat As String = "") As String
        Dim mQry As String
        mQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
        If NCat <> "" Then
            NCat = Replace(NCat, ",", "','")
            mQry = mQry & " Where Vt.NCat In ('" & NCat & "') "
        End If
        FGetVoucher_TypeQry = mQry
    End Function
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcPurchaseReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub

    Public Sub ProcPurchaseReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer



            RepTitle = "Purchase Invoice Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Month Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 1).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, 2).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 3).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 3).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Item").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Voucher Type Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 6).Value = mGridRow.Cells("Voucher Type").Value
                        mFilterGrid.Item(GFilterCode, 6).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 8).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, 8).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells("Item Group").Value
                        mFilterGrid.Item(GFilterCode, 9).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Category Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 10).Value = mGridRow.Cells("Item Category").Value
                        mFilterGrid.Item(GFilterCode, 10).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "City Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 11).Value = mGridRow.Cells("City").Value
                        mFilterGrid.Item(GFilterCode, 11).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "State Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 12).Value = mGridRow.Cells("State").Value
                        mFilterGrid.Item(GFilterCode, 12).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Sales Representative Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 13).Value = mGridRow.Cells("Sales Representative").Value
                        mFilterGrid.Item(GFilterCode, 13).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "User Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 17).Value = mGridRow.Cells("User Name").Value
                        mFilterGrid.Item(GFilterCode, 17).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Responsible Person Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 14).Value = mGridRow.Cells("Responsible Person").Value
                        mFilterGrid.Item(GFilterCode, 14).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "HSN Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 18).Value = mGridRow.Cells("HSN").Value
                        mFilterGrid.Item(GFilterCode, 18).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Tax Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 19).Value = mGridRow.Cells("Party Tax Group").Value
                        mFilterGrid.Item(GFilterCode, 19).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Tax Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 20).Value = mGridRow.Cells("Item Tax Group").Value
                        mFilterGrid.Item(GFilterCode, 20).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Catalog Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 21).Value = mGridRow.Cells("Catalog").Value
                        mFilterGrid.Item(GFilterCode, 21).Value = "'" + mGridRow.Cells("Search Code").Value + "'"

                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Site Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 5).Value = mGridRow.Cells("Site").Value
                        mFilterGrid.Item(GFilterCode, 5).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Division Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 16).Value = mGridRow.Cells("Division").Value
                        mFilterGrid.Item(GFilterCode, 16).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail" Or
                            mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail" Then

                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            If GRepFormName = PurchaseOrderReport Then
                mCondStr = " Where VT.NCat In ('" & Ncat.PurchaseOrder & "', '" & Ncat.PurchaseOrderCancel & "') "
            Else
                mCondStr = " Where VT.NCat In ('" & Ncat.PurchaseInvoice & "', '" & Ncat.PurchaseReturn & "') "
            End If
            'mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 5), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            If ReportFrm.FGetText(7) = "Cash" Then
                mCondStr = mCondStr & " AND BillToParty.Nature = 'Cash'"
            ElseIf ReportFrm.FGetText(7) = "Credit" Then
                mCondStr = mCondStr & " AND BillToParty.Nature <> 'Cash'"
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 12)
            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesRepresentative", 13)
            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.ResponsiblePerson", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.EntryBy", 17)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Catalog", 21)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("DS.CityCode", 22)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.ItemState", 23)

            'If ReportFrm.FGetText(8) <> "All" Then
            '    mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "' "
            'End If

            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 8)

            If ReportFrm.FGetText(15) <> "All" Then
                mTags = ReportFrm.FGetText(15).ToString.Split(",")
                For J = 0 To mTags.Length - 1
                    mCondStr += " And CharIndex('+' || '" & mTags(J) & "',H.Tags) > 0 "
                Next
            End If
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 16), "''", "'")
            If AgL.XNull(ReportFrm.FGetText(18)) <> "All" Then
                mCondStr = mCondStr & " And IfNull(IfNull(IfNull(I.HSN,IC.HSN),Bi.HSN),'') = '" & AgL.XNull(ReportFrm.FGetText(18)) & "' "
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SalesTaxGroupParty", 19)
            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesTaxGroupItem", 20)


            mQry = " SELECT H.DocID, H.V_Type, Vt.Description as VoucherType, H.Site_Code, H.Div_Code, Site.Name as Site, Div.Div_Name as Division,
                    (Select Case When Vt1.NCat = 'SO' Then S1.ManualRefNo Else Null End From PurchInvoice S1 Left Join Voucher_Type Vt1 On S1.V_Type = Vt1.V_Type Where S1.DocID = L.PurchInvoice) as OrderNo, 
                    strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.Vendor, I.ItemGroup, I.ItemCategory,
                    LinkedParty.Name As LinkedPartyName,
                    (Case When H.Vendor=H.BillToParty And (Party.Nature='Cash' Or Party.SubgroupType='" & SubgroupType.RevenuePoint & "') Then Party.Name || ' - ' || IfNull(H.VendorName,'') When H.Vendor=H.BillToParty Then Party.Name When BillToParty.Nature='Cash' And H.Vendor<>H.BillToParty Then  BillToParty.Name || ' - ' || Party.Name  Else Party.Name || ' - ' || BillToParty.Name End) As VendorName , 
                    LTV.Agent As AgentCode, Agent.Name As AgentName, 
                    H.SalesTaxGroupParty, L.SalesTaxGroupItem,
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    Cast(Replace(H.ManualRefNo,'-','') as Integer) as InvoiceNo, H.ManualRefNo, L.Item,
                    I.Specification as ItemSpecification, I.Description As ItemDesc, IfNull(IfNull(I.HSN,IC.HSN),Bi.HSN) as HSN,IG.Description as ItemGroupDescription, IC.Description as ItemCategoryDescription,  
                    L.Catalog, Catalog.Description as CatalogDesc,
                    (Case When L.DiscountPer = 0 Then '' else Cast(L.DiscountPer as nVarchar) End)  || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || (Case When L.AdditionalDiscountPer=0 Then '' else Cast(L.AdditionalDiscountPer as nVarchar) End)  as DiscountPer, 
                    L.DiscountAmount as Discount, L.AdditionalDiscountAmount as AdditionalDiscount, L.AdditionAmount as Addition, 
                    L.SpecialDiscount_Per, L.SpecialDiscount, L.SpecialAddition_Per, L.SpecialAddition, 
                    L.Taxable_Amount, (Case When L.Net_Amount=0 Then L.Amount Else L.Net_Amount End) as Net_Amount, L.Qty, L.Unit, L.DealQty, L.DealUnit, L.Rate, L.Amount +(L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) as AmountExDiscount, L.Amount,
                    L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax, H.EntryBy as EntryByUser
                    FROM PurchInvoice H 
                    Left Join PurchInvoiceDetail L On H.DocID = L.DocID 
                    Left Join PurchInvoiceDetailSku LS On L.DocID = LS.DocID And LS.Sr = L.Sr
                    Left Join Item I On L.Item = I.Code 
                    Left Join Item IG On LS.ItemGroup = IG.Code
                    Left Join Item IC On I.ItemCategory = IC.Code
                    LEFT JOIN Item Bi On I.BaseItem = Bi.Code
                    Left Join viewHelpSubgroup Party On H.Vendor = Party.Code 
                    Left Join viewHelpSubgroup BillToParty On H.BillToParty = BillToParty.Code 
                    Left Join viewHelpSubgroup LinkedParty On H.LinkedParty = LinkedParty.Code 
                    Left Join (Select SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code) as LTV On Party.code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On H.VendorCity = City.CityCode 
                    Left Join State On City.State = State.Code                    
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join Division Div On H.Div_Code = Div.Div_Code
                    Left Join Catalog On L.Catalog = Catalog.Code
                    Left Join Subgroup DS On IG.DefaultSupplier = Ds.Subcode                    
                    " & mCondStr


            If ReportFrm.FGetText(0) = "Doc.Header Wise Detail" Then
                If GRepFormName = PurchaseOrderReport Then
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As OrderNo,
                    Max(VMain.VendorName) As Broker, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, IfNull(Sum(VMain.Discount + VMain.AdditionalDiscount),0) As Discount, IfNull(Sum(VMain.Addition),0) as Addition, IfNull(Sum(VMain.SpecialDiscount),0) As SpecialDiscount, IfNull(Sum(VMain.SpecialAddition),0) As SpecialAddition,
                    IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                Else
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As InvoiceDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As InvoiceNo,
                    Max(VMain.VendorName) As Broker, Max(VMain.SalesTaxGroupParty) As SalesTaxGroupParty, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, 
                    IfNull(Sum(VMain.Discount+VMain.AdditionalDiscount),0) As Discount, IfNull(Sum(VMain.Addition),0) as Addition, IfNull(Sum(VMain.SpecialDiscount),0) As SpecialDiscount, IfNull(Sum(VMain.SpecialAddition),0) As SpecialAddition,
                    IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                End If
            ElseIf ReportFrm.FGetText(0) = "Item Wise Detail" Then
                If GRepFormName = PurchaseOrderReport Then
                    mQry = " Select VMain.DocId As SearchCode, Max(Vmain.Site) as Site, Max(VMain.Division) as Division, Max(VMain.V_Date) As [Order Date], Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As [Order No],
                    Max(VMain.LinkedPartyName) As Party, 
                    Max(VMain.VendorName) As Broker, 
                    Max(VMain.ItemDesc) As Item, Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, Max(VMain.HSN) As HSN, 
                    Max(VMain.DealQty)  as DealQty, Max(VMain.DealUnit) as DealUnit,
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As AmountExDiscount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.AdditionalDiscount) As AdditionalDiscount,
                    Sum(VMain.Addition) As Addition,
                    Max(VMain.SpecialDiscount_Per) As [Sp Disc Per], 
                    Sum(VMain.SpecialDiscount) As [Sp Disc],        
                    Max(VMain.SpecialAddition_Per) As [Sp Addition Per], 
                    Sum(VMain.SpecialAddition) As [Sp Addition],        
                    Sum(VMain.Amount) As [Amount],
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Item 
                    Order By  Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                Else
                    mQry = " Select VMain.DocId As SearchCode, Max(Vmain.Site) as Site, Max(VMain.Division) as Division, Max(VMain.V_Date) As [Invoice Date], Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As [Invoice No],
                    Max(VMain.LinkedPartyName) As Party, 
                    Max(VMain.VendorName) As Broker, Max(VMain.ItemDesc) As Item, Max(VMain.ItemGroupDescription) as ItemGroup, 
                    Sum(VMain.Qty) As Bags, Sum(VMain.DealQty) As Qty, 
                    Max(VMain.Rate) As Rate, Sum(VMain.Addition) As Bardana, 
                    Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Item 
                    Order By  Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                End If
            ElseIf ReportFrm.FGetText(0) = "Voucher Type Wise Summary" Then
                mQry = " Select VMain.V_Type as SearchCode, Max(VMain.VoucherType) As VoucherType, 
                    Count(Distinct Vmain.DocID) as [Doc.Count], Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.V_Type
                    Order By Max(VMain.VoucherType)"
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.Vendor as SearchCode, Max(VMain.VendorName) As Broker, 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Vendor 
                    Order By Max(VMain.VendorName)"
            ElseIf ReportFrm.FGetText(0) = "Sales Representative Wise Summary" Then
                mQry = " Select VMain.SalesRepresentative as SearchCode, Max(VMain.SalesRepresentativeName) As SalesRepresentative, 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Count(Distinct VMain.V_Date) as DaysCount,  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesRepresentative 
                    Order By Max(VMain.SalesRepresentativeName)"
            ElseIf ReportFrm.FGetText(0) = "Responsible Person Wise Summary" Then
                mQry = " Select VMain.ResponsiblePerson as SearchCode, Max(VMain.ResponsiblePersonName) As ResponsiblePerson,
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Count(Distinct VMain.V_Date) as DaysCount,  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ResponsiblePerson 
                    Order By Max(VMain.ResponsiblePersonName)"
            ElseIf ReportFrm.FGetText(0) = "User Wise Summary" Then
                mQry = " Select VMain.EntryByUser as SearchCode, Max(VMain.EntryByUser) As UserName,
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Count(Distinct VMain.V_Date) as DaysCount,  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.EntryByUser
                    Order By Max(VMain.EntryByUser)"
            ElseIf ReportFrm.FGetText(0) = "Catalog Wise Summary" Then
                mQry = " Select VMain.Catalog as SearchCode, Max(VMain.CatalogDesc) As Catalog, 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Catalog
                    Order By Max(VMain.CatalogDesc)"

            ElseIf ReportFrm.FGetText(0) = "Item Wise Summary" Then
                mQry = " Select VMain.Item As SearchCode, Max(VMain.ItemDesc) As [Item],  
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Item 
                    Order By Max(VMain.ItemDesc)"
            ElseIf ReportFrm.FGetText(0) = "HSN Wise Summary" Then
                mQry = " Select VMain.HSN As SearchCode, VMain.HSN, Max(VMain.ItemCategoryDescription) As [Description],  
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.Tax1),0) As IGST, IfNull(Sum(VMain.Tax2),0) As CGST, 
                    IfNull(Sum(VMain.Tax3),0) As SGST, IfNull(Sum(VMain.Tax4),0) As Cess, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.HSN, VMain.ItemCategoryDescription 
                    Order By VMain.HSN, VMain.ItemCategoryDescription"
            ElseIf ReportFrm.FGetText(0) = "Item Group Wise Summary" Then
                mQry = " Select VMain.ItemGroup as SearchCode, Max(VMain.ItemGroupDescription) As [Item Group],  
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemGroup 
                    Order By Max(VMain.ItemGroupDescription)"
            ElseIf ReportFrm.FGetText(0) = "Item Category Wise Summary" Then
                mQry = " Select VMain.ItemCategory as SearchCode, Max(VMain.ItemCategoryDescription) As [Item Category],  
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory 
                    Order By Max(VMain.ItemCategoryDescription)"
            ElseIf ReportFrm.FGetText(0) = "City Wise Summary" Then
                mQry = " Select VMain.CityCode as SearchCode, Max(VMain.CityName) As [City], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.CityCode 
                    Order By Max(VMain.CityName)"
            ElseIf ReportFrm.FGetText(0) = "State Wise Summary" Then
                mQry = " Select VMain.StateCode as SearchCode, Max(VMain.StateName) As [State], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.StateCode 
                    Order By Max(VMain.StateName)"
            ElseIf ReportFrm.FGetText(0) = "Party Tax Group Wise Summary" Then
                mQry = " Select VMain.SalesTaxGroupParty as SearchCode, Max(VMain.SalesTaxGroupParty) As [Party Tax Group], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesTaxGroupParty 
                    Order By Max(VMain.SalesTaxGroupParty)"
            ElseIf ReportFrm.FGetText(0) = "Item Tax Group Wise Summary" Then
                mQry = " Select VMain.SalesTaxGroupItem as SearchCode, Max(VMain.SalesTaxGroupItem) As [Item Tax Group], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesTaxGroupItem
                    Order By Max(VMain.SalesTaxGroupItem)"
            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                mQry = " Select VMain.AgentCode As SearchCode, Max(VMain.AgentName) As [Agent], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AgentCode 
                    Order By Max(VMain.AgentName)"
            ElseIf ReportFrm.FGetText(0) = "Site Wise Summary" Then
                mQry = " Select VMain.Site_Code As SearchCode, Max(VMain.Site) As [Site], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Site_Code 
                    Order By Max(VMain.Site)"
            ElseIf ReportFrm.FGetText(0) = "Division Wise Summary" Then
                mQry = " Select VMain.Div_Code As SearchCode, Max(VMain.Division) As [Division], 
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Div_Code 
                    Order By Max(VMain.Division)"
            ElseIf ReportFrm.FGetText(0) = "Month Wise Summary" Then
                If AgL.PubServerName = "" Then
                    mQry = " Select strftime('%m-%Y',VMain.V_Date_ActualFormat) As SearchCode, strftime('%m-%Y',VMain.V_Date_ActualFormat) As [Month], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By strftime('%m-%Y',VMain.V_Date_ActualFormat)  
                    Order By strftime('%Y',VMain.V_Date_ActualFormat), strftime('%m',VMain.V_Date_ActualFormat)"
                Else
                    mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As SearchCode, Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As [Month], 
                    Count(Distinct Vmain.DocID) as [Doc.Count], 
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7), Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat)  
                    Order By Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat) "
                End If
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)



            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sale Invoice Report - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcSaleReport"

            ReportFrm.ProcFillGrid(DsHeader)


            If ReportFrm.DGL2.Columns.Contains("Taxable Amount") Then
                If AgL.VNull(ReportFrm.DGL2.Item("Taxable Amount", 0).Value) = AgL.VNull(ReportFrm.DGL2.Item("Amount", 0).Value) Then
                    ReportFrm.DGL1.Columns("Taxable Amount").Visible = False
                    ReportFrm.DGL2.Columns("Taxable Amount").Visible = False
                End If

                If AgL.VNull(ReportFrm.DGL2.Item("Amount", 0).Value) = AgL.VNull(ReportFrm.DGL2.Item("Net Amount", 0).Value) Then
                    ReportFrm.DGL1.Columns("Amount").Visible = False
                    ReportFrm.DGL2.Columns("Amount").Visible = False
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

End Class
