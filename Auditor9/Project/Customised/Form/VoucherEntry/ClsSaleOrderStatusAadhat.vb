Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsSaleOrderStatusAadhat

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""

    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4

    Public Const Col1Select As String = "Tick"
    Public Const Col1SearchCode As String = "Search Code"
    Public Const Col1SearchSr As String = "Search Sr"
    Public Const Col1Exception As String = "Exception"
    Public Const Col1OrderDate As String = "Order Date"
    Public Const Col1OrderNo As String = "Doc Date"
    Public Const Col1DeliveryDate As String = "Delivery Date"
    Public Const Col1MinDeliveryDate As String = "Min Delivery Date"
    Public Const Col1PartyName As String = "Account Name"
    Public Const Col1ItemName As String = "Item Name"
    Public Const Col1OrderBales As String = "Order Bales"
    Public Const Col1InvoiceBales As String = "Invoice Bales"
    Public Const Col1InvoiceAmount As String = "Invoice Amount"
    Public Const Col1OrderStatus As String = "Order Status"
    Public Const Col1OldOrderStatus As String = "Old Order Status"

    Dim rowOrderStatus As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowParty As Integer = 3
    Dim rowItemGroup As Integer = 4
    Dim rowItemCategory As Integer = 5
    Dim rowSite As Integer = 6
    Dim rowDivision As Integer = 7
    Dim rowAgent As Integer = 8
    Dim rowCity As Integer = 9
    Dim rowState As Integer = 10
    Dim rowSalesRepresentative As Integer = 11
    Dim rowResponsiblePerson As Integer = 12
    Dim rowTag As Integer = 13

    Dim mShowReportType As String = ""

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
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpSubGroupQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('" & SubgroupType.Customer & "','" & SubgroupType.Supplier & "','" & SubgroupType.LedgerAccount & "')  "
    Dim mHelpSchemeQry$ = "Select Code, Description As [Scheme] From SchemeHead "
    Public Sub Ini_Grid()
        Try

            mQry = "Select 'ALL' as Code, 'ALL' as Name 
                    Union All Select 'Active' as Code, 'Active' as Name 
                    Union All Select 'Cancelled' as Code, 'Cancelled' as Name 
                    Union All Select 'Closed' as Code, 'Closed' as Name 
                    Union All Select 'Completed' as Code, 'Completed' as Name 
                   "

            ReportFrm.CreateHelpGrid("Order Status", "Order Status", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Active",,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpPartyQry)
            ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpItemGroupQry)
            ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpItemCategoryQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpSalesAgentQry)
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpCityQry)
            ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpStateQry)
            ReportFrm.CreateHelpGrid("SalesRepresentative", "Sales Representative", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpSalesRepresentativeQry)
            ReportFrm.CreateHelpGrid("ResponsiblePerson", "ResponsiblePerson", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpResponsiblePersonQry)
            ReportFrm.CreateHelpGrid("Tag", "Tag", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpTagQry)


            ReportFrm.BtnProceed.Visible = False
            ReportFrm.BtnProceed.Text = "Proceed"
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
                                Optional mGridRow As DataGridViewRow = Nothing, Optional bDocId As String = "")


        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer



            RepTitle = "Sale Order Status"


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If



            mCondStr = " Where VT.NCat In ('" & Ncat.SaleOrder & "', '" & Ncat.SaleOrderCancel & "') "
            If ReportFrm.FGetText(rowOrderStatus).ToString.ToUpper <> "ALL" Then
                mCondStr = mCondStr & " And IfNull(H.Status,'Active') = '" & ReportFrm.FGetText(rowOrderStatus) & "' "
            End If
            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", rowItemGroup)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", rowItemCategory)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
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


            mQry = " SELECT L.DocID, L.Sr, H.V_Type, Vt.Description as VoucherType, Site.Name as Site, Div.Div_Name as Division,                    
                    strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,                    
                    BillToParty.Name As PartyName, 
                    Party.Name As SubPartyName, 
                    ShipToParty.Name as ShipToPartyName,                                         
                    H.V_Type || '-' || H.ManualRefNo as OrderNo, H.ManualRefNo, Sit.PrivateMark, Agent.Name As AgentName, Supp.Name As SupplierName,
                    I.Specification as ItemSpecification, I.Description As ItemDesc,IG.Description as ItemGroupDescription, 
                    IC.Description as ItemCategoryDescription, L.Qty as NoOfBales, L.Qty, L.Amount, IfNull(SI.BillNoOfBales,0) as BillBales, IfNull(SI.BillQty,0) as BillQty, IfNull(SI.BillAmount,0) as BillAmount, 
                    (Case When L.Qty - IfNull(SI.BillNoOfBales,0) > 0  Then L.Qty - IfNull(SI.BillNoOfBales,0) Else 0 End) as BalanceBales,                                                            
                    (Case When L.Qty - IfNull(SI.BillQty,0) > 0  Then L.Qty - IfNull(SI.BillQty,0) Else 0 End) as BalanceQty,                                                            
                    (Case When L.Amount - IfNull(SI.BillAmount,0) > 0  Then L.Amount - IfNull(SI.BillAmount,0) Else 0 End) as BalanceAmount, H.Status as OrderStatus                                                            
                    FROM SaleOrder H 
                    Left Join SaleOrderDetail L On H.DocID = L.DocID 
                    Left Join (
                                select BL.SaleOrder, BL.SaleOrderSr, Sum(Case When BL.Sr=1 Then (Case When IfNull(BT.NoOfBales,0)=0 Then 1 Else BT.NoOfBales End) Else 0 End) BillNoOfBales, Sum(BL.Qty) as BillQty, Sum(BL.Amount) as BillAmount
                                From SaleBill BH With (NoLock)
                                Left Join SaleBillDetail BL With (NoLock) On BH.DocId = BL.DocID                                
                                Left Join SaleInvoiceTransport BT With (NoLock) On BH.DocId = BT.DocId
                                Group By BL.SaleOrder, BL.SaleOrdersr
                              ) SI On L.DocID = SI.SaleOrder And L.Sr = SI.SaleOrderSr
                    Left Join Item I On L.Item = I.Code 
                    Left Join Item IG On I.ItemGroup = IG.Code
                    Left Join Item IC On I.ItemCategory = IC.Code
                    LEFT JOIN SaleInvoiceTransport Sit On H.DocId = Sit.DocId
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
                    Left Join viewHelpSubgroup BillToParty On H.BillToParty = BillToParty.Code 
                    Left Join viewHelpSubgroup ShipToParty On H.ShipToParty = ShipToParty.Code 
                    Left Join viewHelpSubgroup Agent On H.Agent = Agent.Code 
                    Left Join viewHelpSubgroup Supp On I.DefaultSupplier = Supp.Code
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode                                                           
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join Division Div On H.Div_Code = Div.Div_Code
                    " & mCondStr


            If ReportFrm.FGetText(0) = "Item Wise Balance" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, Max(VMain.OrderNo) As OrderNo,
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.ShipToPartyName) as ShipToParty , Max(Vmain.ItemDesc) as ItemDescription, Max(VMain.NoOfBales) as OrderBales, Max(VMain.Qty) as OrderQty, Max(VMain.Amount) as OrderAmount, Max(Vmain.BalanceBales) as BalanceBales, Max(VMain.BalanceQty) as BalanceQty, Max(VMain.BalanceAmount) as BalanceAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr  "

                mQry += "Having Max(VMain.BalanceBales) > 0 "
                mQry += "Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "

            Else
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, 
                        Max(VMain.OrderNo) As OrderNo, Max(VMain.PartyName) As Party, Max(VMain.SubPartyName) As SubParty, Max(VMain.ShipToPartyName) as ShipToParty, Max(Vmain.ItemDesc) as ItemDescription, 
                        Max(VMain.NoOfBales) as OrderBales, Max(VMain.BillBales) as InvoiceBales, Max(Vmain.BalanceBales) as BalanceBales, 
                        Max(VMain.BillAmount) as InvoiceAmount, Max(VMain.OrderStatus) as OrderStatus, Max(VMain.OrderStatus) as OldOrderStatus,
                        Max(VMain.SupplierName) As SupplierName, Max(VMain.PrivateMark) As Marka, Max(VMain.AgentName) As AgentName
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr  
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
            End If



            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sale Order Status - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"
            ReportFrm.InputColumnsStr = Col1OrderStatus

            ReportFrm.ProcFillGrid(DsHeader)


            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next
            ReportFrm.DGL1.Columns(Col1OldOrderStatus).Visible = False
            ReportFrm.DGL1.AutoResizeRows()

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try


    End Sub
    Public Sub FProceed()
        Try
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        FProceed()
    End Sub


    Private Sub ObjRepFormGlobal_Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Dim dsTemp As DataSet
        Try

            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) = True Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1OrderStatus
                    mQry = " 
                            Select 'Active' as Code, 'Active' as Description 
                            Union All
                            Select 'Cancelled' as Code, 'Cancelled' as Description 
                            Union All
                            Select 'Closed' as Code, 'Closed' as Description 
                            Union All
                            Select 'Completed' as Code, 'Completed' as Description 
                           "
                    dsTemp = AgL.FillData(mQry, AgL.GCn)
                    FSingleSelectForm(Col1OrderStatus, bRowIndex, dsTemp)
                    If ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value <> ReportFrm.DGL1.Item(Col1OldOrderStatus, bRowIndex).Value Then
                        mQry = "Update SaleInvoice Set Status = " & AgL.Chk_Text(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value) & " Where DocID = '" & ReportFrm.DGL1.Item(Col1SearchCode, bRowIndex).Value & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End If


                    'Case Col1AccountPayeeYn
                    '    If Not ClsMain.IsSpecialKeyPressed(e) Then
                    '        If e.KeyCode = Keys.N Then
                    '            ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value = "NO"
                    '        Else
                    '            ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value = "YES"
                    '        End If
                    '    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FSingleSelectForm(bColumnName As String, bRowIndex As Integer, bDataSet As DataSet)
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(CType(bDataSet, DataSet).Tables(0)), "", 500, 500, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Description", 400, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Tag = FRH_Single.DRReturn("Code")
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Value = FRH_Single.DRReturn("Description")
        End If
    End Sub

    Private Sub ObjRepFormGlobal_Dgl1CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles ReportFrm.DGL1CellBeginEdit
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0

        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                'Case Col1AccountPayeeYn
                '    e.Cancel = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub ObjRepFormGlobal_Dgl1CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ReportFrm.DGL1CellEnter
        Dim bRowIndex As Integer
        Dim bColumnIndex As Integer
        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
