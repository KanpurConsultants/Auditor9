Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsMoneyReceiptReport

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


    Dim rowReportType As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowParty As Integer = 3
    Dim rowCity As Integer = 4
    Dim rowState As Integer = 5
    Dim rowSite As Integer = 6
    Dim rowDivision As Integer = 7


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

    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Payment Mode Wise Summary' as Code, 'Payment Mode Wise Summary' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Month Wise Summary")
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry,, 600, 650, 300)
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            If AgL.PubSiteCount = 1 Then ReportFrm.FilterGrid.Rows(rowSite).Visible = False 'Hide Site Row
            If AgL.PubDivisionCount = 1 Then ReportFrm.FilterGrid.Rows(rowDivision).Visible = False 'Hide Division Row

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcStockHeadReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, ByVal strNCat As String)
        ReportFrm = mReportFrm
        EntryNCat = strNCat
        mReportDefaultText = mReportFrm.Text
    End Sub
    Public Sub ProcStockHeadReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"



            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Payment Mode Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, rowFromDate).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, rowToDate).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "State Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, rowState).Value = mGridRow.Cells("State").Value
                        mFilterGrid.Item(GFilterCode, rowState).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Header Wise Detail" Or
                                mFilterGrid.Item(GFilter, rowReportType).Value = "Item Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If


            'If GRepFormName = PurchaseOrderReport Then
            '    mCondStr = " Where VT.NCat In ('" & Ncat.PurchaseOrder & "', '" & Ncat.PurchaseOrderCancel & "') "
            'Else
            '    mCondStr = " Where VT.NCat In ('" & Ncat.PurchaseInvoice & "', '" & Ncat.PurchaseReturn & "') "
            'End If
            mCondStr = " Where VT.NCat In ('" & Replace(EntryNCat, ",", "','") & "') "
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", rowSite)

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", rowCity)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", rowState)
            'If ReportFrm.FGetText(rowHSN) <> "All" Then
            '    mCondStr = mCondStr & " And I.HSN = " & AgL.Chk_Text(ReportFrm.FGetText(rowHSN)) & " "
            'End If

            mQry = " SELECT H.DocID, L.Sr, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    Prs.Name As Process, H.SubCode As Party, Sku.ItemGroup, Sku.ItemCategory,
                    Sku.BaseItem, Sku.Dimension1, Sku.Dimension2, Sku.Dimension3, Sku.Dimension4, Sku.Size,
                    Party.Name As PartyName, 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as ManualRefNo, 
                    L.Item, I.Specification As ItemSpecification, I.HSN, 
                    IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, I.Description As ItemDesc, 
                    D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                    D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                    Rm.Description as RawMaterialDesc, L.RawMaterialConsumptionQty, 
                    L.Qty, L.Unit, 
                    L.DealQty, L.DealUnit,
                    L.Rate, L.Amount
                    FROM StockHead H 
                    Left Join StockHeadDetail L On H.DocID = L.DocID 
                    LEFT JOIN SubGroup Prs On H.Process = Prs.SubCode
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
                    LEFT JOIN Item Rm On L.RawMaterial = Rm.Code
                    Left Join viewHelpSubgroup Party On H.SubCode = Party.Code                     
                    Left Join (Select SILTV.Subcode,SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code ) as LTV On Party.code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


            If ReportFrm.FGetText(rowReportType) = "Doc.Header Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As DocDate, Max(VMain.ManualRefNo) As DocNo, 
                    Max(VMain.Process) As Process, 
                    Max(VMain.PartyName) As Party, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    IfNull(Sum(VMain.Amount),0) As Amount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat) "
            ElseIf ReportFrm.FGetText(0) = "Item Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As [Doc Date], Max(VMain.ManualRefNo) As [Doc No], 
                    Max(VMain.Process) As Process, 
                    Max(VMain.PartyName) As Party, 
                    Max(VMain.ItemCategoryDesc) As ItemCategory, 
                    Max(VMain.ItemGroupDesc) As ItemGroup, 
                    Max(VMain.ItemDesc) As Item, 
                    Max(VMain.Dimension1Desc) As Dimension1, 
                    Max(VMain.Dimension2Desc) As Dimension2, 
                    Max(VMain.Dimension3Desc) As Dimension3, 
                    Max(VMain.Dimension4Desc) As Dimension4, 
                    Max(VMain.SizeDesc) As Size, 
                    Max(VMain.RawMaterialDesc) As FavricWidth, 
                    Max(VMain.RawMaterialConsumptionQty) As FavricConsumedQty, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit, 
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.Amount) as Amount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr 
                    Order By Max(VMain.V_Date_ActualFormat), Max(VMain.ManualRefNo), Vmain.Sr "
            ElseIf ReportFrm.FGetText(rowReportType) = "Party Wise Summary" Then
                mQry = " Select VMain.Party as SearchCode, Max(VMain.PartyName) As Party, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Party 
                    Order By Max(VMain.PartyName)"
            ElseIf ReportFrm.FGetText(rowReportType) = "HSN Wise Summary" Then
                mQry = " Select VMain.HSN As SearchCode, VMain.HSN, Max(VMain.ItemCategoryDesc) As [Description], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit
                    From (" & mQry & ") As VMain
                    GROUP By VMain.HSN 
                    Order By VMain.HSN, Max(VMain.ItemCategoryDesc)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Item Wise Summary" Then
                mQry = " Select VMain.Item As SearchCode, 
                    Max(VMain.ItemCategoryDesc) As ItemCategory, 
                    Max(VMain.ItemGroupDesc) As ItemGroup, 
                    Max(VMain.ItemDesc) As Item, 
                    Max(VMain.Dimension1Desc) As Dimension1, 
                    Max(VMain.Dimension2Desc) As Dimension2, 
                    Max(VMain.Dimension3Desc) As Dimension3, 
                    Max(VMain.Dimension4Desc) As Dimension4, 
                    Max(VMain.SizeDesc) As Size, 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit 
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory, VMain.ItemGroup, VMain.Item, 
                    VMain.Dimension1, VMain.Dimension2, VMain.Dimension3, VMain.Dimension4, VMain.Size
                    Order By Max(VMain.ItemDesc)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Item Group Wise Summary" Then
                mQry = " Select VMain.ItemGroup as SearchCode, Max(VMain.ItemGroupDesc) As [Item Group], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit 
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemGroup 
                    Order By Max(VMain.ItemGroupDesc)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Item Category Wise Summary" Then
                mQry = " Select VMain.ItemCategory as SearchCode, Max(VMain.ItemCategoryDesc) As [Item Category], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit 
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory 
                    Order By Max(VMain.ItemCategoryDesc)"
            ElseIf ReportFrm.FGetText(rowReportType) = "City Wise Summary" Then
                mQry = " Select VMain.CityCode as SearchCode, Max(VMain.CityName) As [City], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit 
                    From (" & mQry & ") As VMain
                    GROUP By VMain.CityCode 
                    Order By Max(VMain.CityName)"
            ElseIf ReportFrm.FGetText(rowReportType) = "State Wise Summary" Then
                mQry = " Select VMain.StateCode as SearchCode, Max(VMain.StateName) As [State], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit 
                    From (" & mQry & ") As VMain
                    GROUP By VMain.StateCode 
                    Order By Max(VMain.StateName)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Month Wise Summary" Then
                If AgL.PubServerName = "" Then
                    mQry = " Select strftime('%m-%Y',VMain.V_Date_ActualFormat) As SearchCode, strftime('%m-%Y',VMain.V_Date_ActualFormat) As [Month], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit
                    From (" & mQry & ") As VMain
                    GROUP By strftime('%m-%Y',VMain.V_Date_ActualFormat)  
                    Order By strftime('%Y',VMain.V_Date_ActualFormat), strftime('%m',VMain.V_Date_ActualFormat)"
                Else
                    mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As SearchCode, Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As [Month], 
                    Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.DealQty) As DealQty, Max(VMain.DealUnit) As DealUnit 
                    From (" & mQry & ") As VMain
                    GROUP By Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7)
                    Order By Max(Year(VMain.V_Date_ActualFormat)), Max(Month(VMain.V_Date_ActualFormat)) "
                End If
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")



            ReportFrm.Text = mReportDefaultText + "-" + ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcStockHeadReport"

            ReportFrm.ProcFillGrid(DsHeader)
            AgL.FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1, AgL)

            If ReportFrm.DGL1.Columns.Contains(Col1Rate) Then ReportFrm.DGL1.Columns(Col1Rate).Visible = False
            If ReportFrm.DGL1.Columns.Contains(Col1AmountExDiscount) Then ReportFrm.DGL1.Columns(Col1AmountExDiscount).Visible = False
            If ReportFrm.DGL1.Columns.Contains(Col1Amount) Then ReportFrm.DGL1.Columns(Col1Amount).Visible = False
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
End Class
