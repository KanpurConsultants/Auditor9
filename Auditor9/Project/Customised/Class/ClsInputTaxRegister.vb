Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsInputTaxRegister

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

    Public Const Col1SearchCode As String = "Search Code"
    Public Const Col1Tags As String = "Tags"
    Public Const Col1SalesTaxGroupItem As String = "Sales Tax Group Item"
    Public Const Col1NCat As String = "Ncat"
    Public Const Col1InvoiceValue As String = "Invoice Value"

    Dim rowFromDate As Integer = 0
    Dim rowToDate As Integer = 1
    Dim rowSite As Integer = 2
    Dim rowDivision As Integer = 3
    Dim rowParty As Integer = 4
    Dim rowGSTR2 As Integer = 5
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
            Dim mLastMonthDate As String = DateAdd(DateInterval.Month, -1, CDate(AgL.Dman_Execute("SELECT date('now')", AgL.GCn).ExecuteScalar()))
            ReportFrm.CreateHelpGrid("FromDate", "From Date", Aglibrary.FrmReportLayout.FieldFilterDataType.StringType, Aglibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthStartDate(mLastMonthDate))
            ReportFrm.CreateHelpGrid("ToDate", "To Date", Aglibrary.FrmReportLayout.FieldFilterDataType.StringType, Aglibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthEndDate(mLastMonthDate))
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry,, 600, 650, 300)
            mQry = "Select 'Reconciled' as Code, 'Reconciled' as Name 
                    Union All 
                    Select 'Not Reconciled' as Code, 'Not Reconciled' as Name 
                    Union All 
                    Select 'Both' as Code, 'Both' as Name"
            ReportFrm.CreateHelpGrid("GSTR2", "GSTR2", Aglibrary.FrmReportLayout.FieldFilterDataType.StringType, Aglibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Not Reconciled")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcInputTaxRegister()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcInputTaxRegister(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mPurchCondStr$ = ""
            Dim mLedgerHeadCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"



            RepTitle = "Stock Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            mPurchCondStr = " WHERE Date(IfNull(H.VendorDocDate, H.V_Date)) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(IfNull(H.VendorDocDate, H.V_Date)) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mPurchCondStr = mPurchCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mPurchCondStr = mPurchCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mPurchCondStr = mPurchCondStr & ReportFrm.GetWhereCondition("H.Vendor", rowParty)
            mPurchCondStr = mPurchCondStr & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "

            If ReportFrm.FGetText(rowGSTR2) = "Reconciled" Then
                mPurchCondStr += " And CharIndex('+GSTR2', IsNull(L.Tags,'')) > 0 "
            ElseIf ReportFrm.FGetText(rowGSTR2) = "Not Reconciled" Then
                mPurchCondStr += " And CharIndex('+GSTR2',IsNull(L.Tags,'')) = 0 "
            End If

            mLedgerHeadCondStr = " WHERE Date(IfNull(H.PartyDocDate, H.V_Date)) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(IfNull(H.PartyDocDate, H.V_Date)) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mLedgerHeadCondStr = mLedgerHeadCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mLedgerHeadCondStr = mLedgerHeadCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mLedgerHeadCondStr = mLedgerHeadCondStr & ReportFrm.GetWhereCondition("H.SubCode", rowParty)
            mLedgerHeadCondStr = mLedgerHeadCondStr & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "

            If ReportFrm.FGetText(rowGSTR2) = "Reconciled" Then
                mLedgerHeadCondStr += " And CharIndex('+GSTR2', IsNull(L.Tags,'')) > 0 "
            ElseIf ReportFrm.FGetText(rowGSTR2) = "Not Reconciled" Then
                mLedgerHeadCondStr += " And CharIndex('+GSTR2',IsNull(L.Tags,'')) = 0 "
            End If

            Dim mStrQry As String = " SELECT L.DocId, Vt.Ncat, H.VendorSalesTaxNo As GSTINofRecipient, 
                    Replace(Replace(Sg.Name,'{',''),'}','') As ReceiverName,
                    '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNumber, 
                    strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, 
                    H.VendorDocDate As OrderByDate,
                    H.VendorDocNo As PartyInvoiceNo,
                    strftime('%d/%m/%Y', H.VendorDocDate) As PartyInvoiceDate, 
                    L.Net_Amount As LineNet_Amount, 
                    H.Net_Amount As HeaderNet_Amount, 
                    S.ManualCode || '-' || S.Description As PlaceOfSupply, 'N' As ReverseCharge,
                    '' As ApplicableTaxRate, '' As InvoiceType,	'' As ECommerceGSTIN,	 
                    L.SalesTaxGroupItem,
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,	
                    L.Taxable_Amount As TaxableValue, 
                    IfNull(L.Tax1,0) As IntegratedTaxAmount,  IfNull(L.Tax2,0) As CentralTaxAmount, 
                    IfNull(L.Tax3,0) As StateTaxAmount, IfNull(L.Tax4,0) As CessAmount,
                    IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount,
                    L.Tags, '' As Exception
                    From PurchInvoice H 
                    left join PurchInvoiceDetail L On H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    left join SubGroup Sg On H.Vendor = Sg.SubCode
                    LEFT JOIN City C On H.VendorCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mPurchCondStr &
                    " And Vt.NCat In ('" & Ncat.PurchaseInvoice & "', '" & Ncat.JobInvoice & "')
                    And H.SalesTaxGroupParty In ('" & PostingGroupSalesTaxParty.Registered & "','" & PostingGroupSalesTaxParty.Composition & "') "

            mStrQry += " UNION ALL "

            mStrQry += "SELECT L.DocId, Vt.Ncat, H.PartySalesTaxNo As GSTINofRecipient, 
                    Replace(Replace(Sg.Name,'{',''),'}','') As ReceiverName,
                    'SSF-KNP-' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNumber, 
                    strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, 
                    H.PartyDocDate As OrderByDate,
                    H.PartyDocNo As PartyInvoiceNo,
                    strftime('%d/%m/%Y', H.PartyDocDate) As PartyInvoiceDate, 
                    Lc.Net_Amount As LineNet_Amount, 
                    Hc.Net_Amount As HeaderNet_Amount, 
                    S.ManualCode || '-' || S.Description As PlaceOfSupply, 'N' As ReverseCharge,
                    '' As ApplicableTaxRate, '' As InvoiceType,	'' As ECommerceGSTIN,	 
                    L.SalesTaxGroupItem,
                    IfNull(Lc.Tax1_Per,0) + IfNull(Lc.Tax2_Per,0) + IfNull(Lc.Tax3_Per,0) As Rate,	
                    Lc.Taxable_Amount As TaxableValue, 
                    IfNull(Lc.Tax1,0) As IntegratedTaxAmount,  IfNull(Lc.Tax2,0) As CentralTaxAmount, 
                    IfNull(Lc.Tax3,0) As StateTaxAmount, IfNull(Lc.Tax4,0) As CessAmount,
                    IfNull(Lc.Tax1,0) + IfNull(Lc.Tax2,0) + IfNull(Lc.Tax3,0) + IfNull(Lc.Tax4,0) As TaxAmount,
                    L.Tags, '' As Exception
                    From LedgerHead H 
                    LEFT JOIN LedgerHeadCharges Hc ON H.DocId = Hc.DocId
                    left join LedgerHeadDetail L On H.DocID = L.DocID
                    LEFT JOIN LedgerHeadDetailCharges Lc ON L.DocId = Lc.DocId AND L.Sr = Lc.Sr
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                    LEFT JOIN City C On H.PartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mLedgerHeadCondStr &
                    " And Vt.NCat = '" & Ncat.ExpenseVoucher & "'
                    And H.SalesTaxGroupParty In ('" & PostingGroupSalesTaxParty.Registered & "','" & PostingGroupSalesTaxParty.Composition & "') "

            mQry = "Select H.DocId As SearchCode, Max(H.Ncat) As Ncat, Max(H.GSTINofRecipient) As GstNoOfRecipient, Max(H.ReceiverName) As ReceiverName, 
                                Max(H.InvoiceNumber) As InvoiceNumber,
                                Max(H.InvoiceDate) As InvoiceDate, 
                                Max(H.PartyInvoiceNo) As PartyInvoiceNo,
                                Max(H.PartyInvoiceDate) As PartyInvoiceDate, 
                                Max(H.HeaderNet_Amount) As InvoiceValue, 
                                Max(H.PlaceOfSupply) As PlaceOfSupply, Max(H.ReverseCharge) As ReverseCharge,
                                Max(H.SalesTaxGroupItem) As SalesTaxGroupItem,
                                Max(H.ApplicableTaxRate) As ApplicableTaxRate, Max(H.InvoiceType) As InvoiceType,	
                                Max(H.ECommerceGSTIN) As EcommerceGstin, Max(H.Rate) As Rate,	
                                Sum(H.TaxableValue) As TaxableValue, 
                                Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount,
                                Sum(H.CentralTaxAmount) As CentralTaxAmount,
                                Sum(H.StateTaxAmount) As StateTaxAmount,
                                Sum(H.CessAmount) As CessAmount,
                                Max(H.Tags) As Tags
                                From (" + mStrQry + ") As H 
                                Group By H.DocID, H.SalesTaxGroupItem 
                                Order By Max(OrderByDate), PartyInvoiceNo, DocId "

            DsHeader = AgL.FillData(mQry, AgL.GCn)


            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Input Tax Register"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcInputTaxRegister"
            ReportFrm.InputColumnsStr = Col1Tags

            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.DGL1.Columns(Col1NCat).Visible = False
            ReportFrm.DGL1.Columns(Col1SalesTaxGroupItem).Visible = False
            ReportFrm.DGL1.Columns(Col1Tags).Visible = True
            ReportFrm.DGL1.Columns(Col1Tags).HeaderCell.Style.BackColor = Color.LightCyan
            ReportFrm.DGL1.Columns(Col1Tags).HeaderCell.Style.ForeColor = Color.Black

            ReportFrm.DGL2.Item(Col1InvoiceValue, 0).Value = ""
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
                Case Col1Tags
                    mQry = " 
                            Select 'GSTR2' as Code, '+GSTR2' as Description 
                            Union All
                            Select '' as Code, '' as Description 
                           "
                    dsTemp = AgL.FillData(mQry, AgL.GCn)
                    FSingleSelectForm(Col1Tags, bRowIndex, dsTemp)


                    If ReportFrm.DGL1.Item(Col1NCat, bRowIndex).Value = Ncat.PurchaseInvoice Or ReportFrm.DGL1.Item(Col1NCat, bRowIndex).Value = Ncat.JobInvoice Then
                        mQry = "Update PurchInvoiceDetail 
                            Set Tags = " & AgL.Chk_Text(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value) & " 
                            Where DocID = '" & ReportFrm.DGL1.Item(Col1SearchCode, bRowIndex).Value & "'
                            And SalesTaxGroupItem = '" & ReportFrm.DGL1.Item(Col1SalesTaxGroupItem, bRowIndex).Value & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    ElseIf ReportFrm.DGL1.Item(Col1NCat, bRowIndex).Value = Ncat.ExpenseVoucher Then
                        mQry = "Update LedgerHeadDetail 
                            Set Tags = " & AgL.Chk_Text(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value) & " 
                            Where DocID = '" & ReportFrm.DGL1.Item(Col1SearchCode, bRowIndex).Value & "'
                            And SalesTaxGroupItem = '" & ReportFrm.DGL1.Item(Col1SalesTaxGroupItem, bRowIndex).Value & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End If
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
End Class
