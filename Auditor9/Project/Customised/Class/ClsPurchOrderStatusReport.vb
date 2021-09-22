Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsPurchOrderStatusReport

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

    Public Const Col1Rate As String = "Rate"
    Public Const Col1AmountExDiscount As String = "Amount Ex Discount"
    Public Const Col1Amount As String = "Amount"


    Dim rowFromDate As Integer = 0
    Dim rowToDate As Integer = 1
    Dim rowStatusType As Integer = 2
    Dim rowProcess As Integer = 3
    Dim rowParty As Integer = 4
    Dim rowSite As Integer = 5
    Dim rowVoucherType As Integer = 6
    Dim rowCashCredit As Integer = 7
    Dim rowAgent As Integer = 8
    Dim rowItemCategory As Integer = 9
    Dim rowItemGroup As Integer = 10
    Dim rowItem As Integer = 11
    Dim rowDimension1 As Integer = 12
    Dim rowDimension2 As Integer = 13
    Dim rowDimension3 As Integer = 14
    Dim rowDimension4 As Integer = 15
    Dim rowSize As Integer = 16
    Dim rowCity As Integer = 17
    Dim rowState As Integer = 18
    Dim rowSalesRepresentative As Integer = 19
    Dim rowResponsiblePerson As Integer = 20
    Dim rowTag As Integer = 21
    Dim rowDivision As Integer = 22
    Dim rowBalanceType As Integer = 23


    Public Const hcFromDate As String = "From Date"
    Public Const hcToDate As String = "To Date"
    Public Const hcStatusType As String = "Status Type"
    Public Const hcProcess As String = "Process"
    Public Const hcParty As String = "Party"
    Public Const hcSite As String = "Site"
    Public Const hcVoucherType As String = "Voucher Type"
    Public Const hcCashCredit As String = "Cash Credit"
    Public Const hcAgent As String = "Agent"
    Public Const hcItemCategory As String = "Item Category"
    Public Const hcItemGroup As String = "Item Group"
    Public Const hcItem As String = "Item"
    Public Const hcDimension1 As String = "Dimension1"
    Public Const hcDimension2 As String = "Dimension2"
    Public Const hcDimension3 As String = "Dimension3"
    Public Const hcDimension4 As String = "Dimension4"
    Public Const hcSize As String = "Size"
    Public Const hcCity As String = "City"
    Public Const hcState As String = "State"
    Public Const hcSalesRepresentative As String = "Sales Representative"
    Public Const hcResponsiblePerson As String = "Responsible Person"
    Public Const hcTag As String = "Tag"
    Public Const hcDivision As String = "Division"
    Public Const hcBalanceType As String = "Balance Type"

    'Report Column Constants
    Public Const Col1Division As String = "Division"
    Public Const Col1Site As String = "Site"
    Public Const Col1OrderDate As String = "Order Date"
    Public Const Col1OrderNo As String = "Order No"
    Public Const Col1Party As String = "Party"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1OrderQty As String = "Order Qty"
    Public Const Col1OrderAmount As String = "Order Amount"
    Public Const Col1BalanceQty As String = "Balance Qty"
    Public Const Col1BalanceAmount As String = "Balance Amount"

    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing
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
    Dim mHelpProcessQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.Name AS Process FROM SubGroup Sg Where Sg.SubGroupType = '" & SubgroupType.Process & "' And IfNull(Sg.Status,'Active') = 'Active' "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpDimension1Qry$ = "Select 'o' As Tick, Code, Description From Dimension1 "
    Dim mHelpDimension2Qry$ = "Select 'o' As Tick, Code, Description From Dimension2 "
    Dim mHelpDimension3Qry$ = "Select 'o' As Tick, Code, Description From Dimension3 "
    Dim mHelpDimension4Qry$ = "Select 'o' As Tick, Code, Description From Dimension4 "
    Dim mHelpSizeQry$ = "Select 'o' As Tick, Code, Description From Size "
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description   FROM Tag H "
    Dim mHelpSalesRepresentativeQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Sales Representative] FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code ='SREP' "
    Dim mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpResponsiblePersonQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code <>'SREP' "
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid(hcFromDate, hcFromDate, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid(hcToDate, hcToDate, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            mQry = "Select 'Pending' as Code, 'Pending' as Name 
                    Union All Select 'All' as Code, 'All' as Name "
            ReportFrm.CreateHelpGrid(hcStatusType, hcStatusType, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Pending",,, 300)
            ReportFrm.CreateHelpGrid(hcProcess, hcProcess, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpProcessQry)
            ReportFrm.CreateHelpGrid(hcParty, hcParty, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry,, 600, 650, 300)
            ReportFrm.CreateHelpGrid(hcSite, hcSite, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
            ReportFrm.CreateHelpGrid(hcVoucherType, hcVoucherType, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice", EntryNCat))
            ReportFrm.CreateHelpGrid(hcCashCredit, hcCashCredit, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
            ReportFrm.CreateHelpGrid(hcAgent, hcAgent, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
            ReportFrm.CreateHelpGrid(hcItemCategory, hcItemCategory, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
            ReportFrm.CreateHelpGrid(hcItemGroup, hcItemGroup, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
            ReportFrm.CreateHelpGrid(hcItem, hcItem, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
            ReportFrm.CreateHelpGrid(hcDimension1, hcDimension1, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension1Qry)
            ReportFrm.CreateHelpGrid(hcDimension2, hcDimension2, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension2Qry)
            ReportFrm.CreateHelpGrid(hcDimension3, hcDimension3, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension3Qry)
            ReportFrm.CreateHelpGrid(hcDimension4, hcDimension4, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDimension4Qry)
            ReportFrm.CreateHelpGrid(hcSize, hcSize, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSizeQry)
            ReportFrm.CreateHelpGrid(hcCity, hcCity, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid(hcState, hcState, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
            ReportFrm.CreateHelpGrid(hcSalesRepresentative, hcSalesRepresentative, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesRepresentativeQry)
            ReportFrm.CreateHelpGrid(hcResponsiblePerson, hcResponsiblePerson, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpResponsiblePersonQry)
            ReportFrm.CreateHelpGrid(hcTag, hcTag, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTagQry)
            ReportFrm.CreateHelpGrid(hcDivision, hcDivision, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            mQry = "Select 'Amount Balance' as Code, 'Amount Balance' as Name 
                            Union All 
                            Select 'Qty Balance' as Code, 'Qty Balance' as Name "
            ReportFrm.CreateHelpGrid(hcBalanceType, hcBalanceType, FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Qty Balance",,, 300)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcPurchOrderStatus()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, ByVal strNCat As String)
        ReportFrm = mReportFrm
        EntryNCat = strNCat
        mReportDefaultText = mReportFrm.Text
    End Sub
    Public Sub ProcPurchOrderStatus(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer

            ReportFrm.ReportName = mReportDefaultText
            ReportFrm.ReportFormatName = ""

            If mReportDefaultText = "" Then
                mReportDefaultText = ReportFrm.Text
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where VT.NCat In ('" & Replace(EntryNCat, ",", "','") & "') "
            mCondStr = mCondStr & " AND L.SubRecordType Is Null "
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", rowProcess)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", rowParty)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", rowVoucherType)
            If ReportFrm.FGetText(7) = "Cash" Then
                mCondStr = mCondStr & " AND BillToParty.Nature = 'Cash'"
            ElseIf ReportFrm.FGetText(7) = "Credit" Then
                mCondStr = mCondStr & " AND BillToParty.Nature <> 'Cash'"
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemCategory", rowItemCategory)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemGroup", rowItemGroup)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", rowItem)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension1", rowDimension1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension2", rowDimension2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension3", rowDimension3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Dimension4", rowDimension4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Size", rowSize)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", rowCity)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", rowState)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesRepresentative", rowSalesRepresentative)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.ResponsiblePerson", rowResponsiblePerson)

            If ReportFrm.FGetText(rowTag) <> "All" Then
                mTags = ReportFrm.FGetText(rowTag).ToString.Split(",")
                For J = 0 To mTags.Length - 1
                    mCondStr += " And CharIndex('+' || '" & mTags(J) & "',H.Tags) > 0 "
                Next
            End If
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")

            mQry = " SELECT L.DocID, L.Sr, H.V_Type, Vt.Description as VoucherType, Site.Name as Site, Div.Div_Name as Division,                    
                    strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat, Prs.Name As Process,                    
                    (Case When H.Vendor = H.BillToParty Then Party.Name Else BillToParty.Name || ' - ' || Party.Name End) As VendorName ,                                         
                    H.V_Type || '-' || H.ManualRefNo as OrderNo, H.ManualRefNo, 
                    IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, I.Description As ItemDesc, 
                    D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                    D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                    L.Qty, IfNull(SI.ReceiveQty,0) as ReceiveQty, 
                    L.Qty - IfNull(SI.ReceiveQty,0) as BalanceQty
                    FROM PurchOrder H 
                    Left Join PurchOrderDetail L On H.DocID = L.DocID 
                    LEFT JOIN SubGroup Prs On H.Process = Prs.SubCode
                    Left Join (
                                Select S.ReferenceDocID As PurchOrder, S.ReferenceTSr As PurchOrderSr, Sum(S.Qty_Rec) as ReceiveQty
                                From Stock S With (NoLock)
                                Group By S.ReferenceDocID, S.ReferenceTSr
                              ) SI On L.DocID = SI.PurchOrder And L.Sr = SI.PurchOrderSr
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
                    Left Join viewHelpSubgroup Party On H.Vendor = Party.Code 
                    Left Join viewHelpSubgroup BillToParty On H.BillToParty = BillToParty.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode                                                           
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join Division Div On H.Div_Code = Div.Div_Code
                    " & mCondStr


            mQry = " Select VMain.DocId As SearchCode, 
                    Max(VMain.V_Date) As OrderDate, Max(VMain.OrderNo) As OrderNo,
                    Max(VMain.Process) As Process, 
                    Max(VMain.VendorName) As Party, 
                    Max(Vmain.ItemCategoryDesc) as ItemCategory, 
                    Max(Vmain.ItemGroupDesc) as ItemGroup, 
                    Max(Vmain.ItemDesc) as Item, 
                    Max(Vmain.Dimension1Desc) as Dimension1, 
                    Max(Vmain.Dimension2Desc) as Dimension2, 
                    Max(Vmain.Dimension3Desc) as Dimension3, 
                    Max(Vmain.Dimension4Desc) as Dimension4, 
                    Max(Vmain.SizeDesc) as Size, 
                    Max(VMain.Qty) as OrderQty, 
                    Max(VMain.ReceiveQty) as ReceiveQty, 
                    Max(VMain.BalanceQty) as BalanceQty
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr  "

            If ReportFrm.FGetText(rowStatusType) = "Pending" Then
                If ReportFrm.FGetText(rowBalanceType) = "Amount Balance" Then
                    mQry += "Having Max(VMain.BalanceAmount) > 0 "
                Else
                    mQry += "Having Max(VMain.BalanceQty) > 0 "
                End If
            End If

            mQry += "Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "


            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = mReportDefaultText 
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcPurchOrderStatus"

            ReportFrm.ProcFillGrid(DsHeader)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
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
    Public Shared Sub FSeedTable_ReportHeaderUISetting()
        Dim objMdi As New MDIMain

        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcStatusType, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcProcess, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcFromDate, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcToDate, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcParty, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcItem, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcSite, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcVoucherType, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcCashCredit, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcAgent, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcItemGroup, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcItemCategory, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcDimension1, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcDimension2, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcDimension3, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcDimension4, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcSize, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcCity, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcState, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcSalesRepresentative, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcResponsiblePerson, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcTag, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcDivision, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "FilterGrid", hcBalanceType, 1, 0, 0, "")
    End Sub
    Public Shared Sub FSeedTable_ReportLineUISetting()
        Dim objMdi As New MDIMain

        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1Division, 1, 0, 0, "", 70)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1Site, 1, 0, 0, "", 60)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1OrderDate, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1OrderNo, 1, 0, 0, "", 90)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1Party, 1, 0, 0, "", 120)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1ItemCategory, 1, 0, 0, "", 105)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1ItemGroup, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1Item, 1, 0, 0, "", 100)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1Dimension1, 1, 0, 0, "", 100)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1Dimension2, 1, 0, 0, "", 100)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1Dimension3, 1, 0, 0, "", 100)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1Dimension4, 1, 0, 0, "", 100)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Size) Then
            ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1Size, 1, 0, 0, "", 80)
        End If
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1OrderQty, 1, 0, 0, "", 80)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1OrderAmount, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1BalanceQty, 1, 0, 0, "", 80)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(objMdi.MnuPurchaseOrderStatusReport.Text, "", "Dgl1", Col1BalanceAmount, 1, 0, 0, "", 100)
    End Sub
End Class
