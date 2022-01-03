Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsGstOutputTaxReport
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
    Dim rowSummerise As Integer = 1
    Dim rowFromDate As Integer = 2
    Dim rowToDate As Integer = 3
    Dim rowSite As Integer = 4
    Dim rowDivision As Integer = 5
    Dim rowHSN As Integer = 6
    Dim rowV_Type As Integer = 7
    Dim rowPartyTaxGroup As Integer = 8
    Dim rowItemTaxGroup As Integer = 9



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
    Dim mHelpVoucherTypeQry$ = "SELECT 'o' As Tick, H.V_Type AS Code, H.Description FROM Voucher_Type H  "



    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Customise Summary' as Code, 'Customise Summary' as Name
                    Union All Select 'HSN Wise Summary' as Code, 'HSN Wise Summary' as Name
                    Union All Select 'State Wise Summary' as Code, 'State Wise Summary' as Name
                    Union All Select 'Item Tax Group Wise Summary' as Code, 'Item Tax Group Wise Summary' as Name"
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Customise Summary",,, 300)

            mQry = "Select 'o' As Tick,  'State' as Code, 'State' as Name
                    Union All Select 'o' As Tick, 'Party' as Code, 'Party' as Name
                    Union All Select 'o' As Tick, 'HSN' as Code, 'HSN' as Name
                    Union All Select 'o' As Tick, 'Item Category' as Code, 'Item Category' as Name                             
                    Union All Select 'o' As Tick, 'Item Tax Group' as Code, 'Item Tax Group' as Name 
                    Union All Select 'o' As Tick, 'Party Tax Group' as Code, 'Party Tax Group' as Name 
                    Union All Select 'o' As Tick, 'Month' as Code, 'Month' as Name 
                    Union All Select 'o' As Tick, 'Voucher Type' as Code, 'Voucher Type' as Name 
                    "
            ReportFrm.CreateHelpGrid("Summarise", "Summarise", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, "",,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("HSN", "HSN", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
            ReportFrm.CreateHelpGrid("VoucherType", "VoucherType", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
            ReportFrm.CreateHelpGrid("PartyTaxGroup", "PartyTaxGroup", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
            ReportFrm.CreateHelpGrid("ItemTaxGroup", "ItemTaxGroup", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")


            ReportFrm.FilterGrid.Rows(rowHSN).Visible = False 'Hide HSN Row
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
        Try
            Dim mCondStr$ = ""
            Dim mLedgerHeadCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer
            Dim mMainQry As String


            RepTitle = "Sale Invoice Report"

            If ReportFrm.FGetText(rowSummerise) = "" Then
                MsgBox("Select Any column to summarise.")
                Exit Sub
            End If


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Customise Summary" Or
    mFilterGrid.Item(GFilter, rowReportType).Value = "HSN Wise Summary" Or
    mFilterGrid.Item(GFilter, rowReportType).Value = "State Wise Summary" Or
    mFilterGrid.Item(GFilter, rowReportType).Value = "Item Tax Group Wise Summary" Then

                        mFilterGrid.Item(GFilter, rowReportType).Value = "Detail"

                        If ReportFrm.FGetCode(rowSummerise).ToString.Contains("HSN") Then
                            mFilterGrid.Item(GFilterCode, rowHSN).Value = "'" + mGridRow.Cells("HSN").Value + "'"
                            mFilterGrid.Item(GFilter, rowHSN).Value = mGridRow.Cells("HSN").Value
                        End If

                        If ReportFrm.FGetCode(rowSummerise).ToString.Contains("Voucher Type") Then
                            mFilterGrid.Item(GFilterCode, rowV_Type).Value = "'" + mGridRow.Cells("Voucher Type").Value + "'"
                            mFilterGrid.Item(GFilter, rowV_Type).Value = mGridRow.Cells("Voucher Type").Value
                        End If

                        If ReportFrm.FGetCode(rowSummerise).ToString.Contains("Party Tax Group") Then
                            mFilterGrid.Item(GFilterCode, rowPartyTaxGroup).Value = "'" + mGridRow.Cells("Party Tax Group").Value + "'"
                            mFilterGrid.Item(GFilter, rowPartyTaxGroup).Value = mGridRow.Cells("Party Tax Group").Value
                        End If

                        If ReportFrm.FGetCode(rowSummerise).ToString.Contains("Item Tax Group") Then
                            mFilterGrid.Item(GFilterCode, rowItemTaxGroup).Value = "'" + mGridRow.Cells("Item Tax Group").Value + "'"
                            mFilterGrid.Item(GFilter, rowItemTaxGroup).Value = mGridRow.Cells("Item Tax Group").Value
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

            mCondStr = " Where VT.NCat In ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("I.HSN", rowHSN), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("Vt.Description", rowV_Type), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.SalesTaxGroupParty", rowPartyTaxGroup), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.SalesTaxGroupItem", rowItemTaxGroup), "''", "'")


            mLedgerHeadCondStr = " Where VT.NCat In ('" & Ncat.IncomeVoucher & "', '" & Ncat.DebitNoteCustomer & "', '" & Ncat.CreditNoteCustomer & "') "
            mLedgerHeadCondStr = mLedgerHeadCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mLedgerHeadCondStr = mLedgerHeadCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mLedgerHeadCondStr = mLedgerHeadCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mLedgerHeadCondStr = mLedgerHeadCondStr & Replace(ReportFrm.GetWhereCondition("L.HSN", rowHSN), "''", "'")
            mLedgerHeadCondStr = mLedgerHeadCondStr & Replace(ReportFrm.GetWhereCondition("Vt.Description", rowV_Type), "''", "'")
            mLedgerHeadCondStr = mLedgerHeadCondStr & Replace(ReportFrm.GetWhereCondition("H.SalesTaxGroupParty", rowPartyTaxGroup), "''", "'")
            mLedgerHeadCondStr = mLedgerHeadCondStr & Replace(ReportFrm.GetWhereCondition("L.SalesTaxGroupItem", rowItemTaxGroup), "''", "'")




            mMainQry = " SELECT H.DocID, H.V_Type, Vt.Description as VoucherType, 
                    H.Site_Code, H.Div_Code, 
                    Site.Name as Site, Div.Div_Name as Division,                    
                    H.V_Date As V_Date,
                    " & IIf(AgL.PubServerName = "", "strftime('%m-%Y',V_Date)  ", "Substring(Convert(NVARCHAR, H.V_Date,103),4,7)") & " As Month,
                    H.SaleToParty, 
                    (Case When Party.Nature='Cash' Or Party.SubgroupType='" & SubgroupType.RevenuePoint & "' Then Party.Name || ' - ' || IfNull(H.SaleToPartyName,'') Else Party.Name End) As SaleToPartyName ,                     
                    H.SalesTaxGroupParty, L.SalesTaxGroupItem,
                    State.Code As StateCode, State.Description As StateName,
                    Cast(Replace(H.ManualRefNo,'-','') as Integer) as InvoiceNo, 
                    IfNull(I.HSN,IC.HSN) as HSN,
                    IC.Code as ItemCategoryCode,IC.Description as ItemCategory,
                    L.Qty, L.Unit, L.Taxable_Amount, L.Net_Amount,                      
                    L.Tax1 as Igst, L.Tax2 as Cgst, L.Tax3 as Sgst, L.Tax4 as Cess, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
                    Left Join Item I On L.Item = I.Code                     
                    Left Join Item IC On IfNull(I.ItemCategory,I.Code) = IC.Code
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code                     
                    Left Join City On H.SaleToPartyCity = City.CityCode 
                    Left Join State On City.State = State.Code                    
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join Division Div On H.Div_Code = Div.Div_Code
                    " & mCondStr

            mMainQry += " UNION ALL "

            mMainQry += " SELECT H.DocID, H.V_Type, Vt.Description as VoucherType, 
                    H.Site_Code, H.Div_Code, 
                    Site.Name as Site, Div.Div_Name as Division,                    
                    H.V_Date As V_Date,
                    " & IIf(AgL.PubServerName = "", "strftime('%m-%Y',V_Date)  ", "Substring(Convert(NVARCHAR, H.V_Date,103),4,7)") & " As Month,
                    H.SubCode, 
                    (Case When Party.Nature='Cash' Or Party.SubgroupType='" & SubgroupType.RevenuePoint & "' Then Party.Name || ' - ' || IfNull(H.PartyName,'') Else Party.Name End) As PartyName ,                     
                    H.SalesTaxGroupParty, L.SalesTaxGroupItem,
                    State.Code As StateCode, State.Description As StateName,
                    Cast(Replace(H.ManualRefNo,'-','') as Integer) as InvoiceNo, 
                    L.HSN as HSN,
                    Null as ItemCategoryCode, Null as ItemCategory,
                    L.Qty, L.Unit, Lc.Taxable_Amount, Lc.Net_Amount,                      
                    Lc.Tax1 as Igst, Lc.Tax2 as Cgst, Lc.Tax3 as Sgst, Lc.Tax4 as Cess, Lc.Tax1+Lc.Tax2+Lc.Tax3+Lc.Tax4+Lc.Tax5 as TotalTax
                    FROM LedgerHead H 
                    Left Join LedgerHeadDetail L On H.DocID = L.DocID 
                    LEFT JOIN LedgerHeadDetailCharges Lc On L.DocId = Lc.DocId And L.Sr = Lc.Sr
                    Left Join viewHelpSubgroup Party On H.SubCode = Party.Code                     
                    Left Join City On H.PartyCity = City.CityCode 
                    Left Join State On City.State = State.Code                    
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join Division Div On H.Div_Code = Div.Div_Code
                    " & mLedgerHeadCondStr



            If ReportFrm.FGetText(0) = "Detail" Then
                mQry = " Select VMain.DocId As SearchCode "
                mQry += ", Max(Vmain.Site) as Site "
                mQry += ", Max(VMain.Division) as Division "
                mQry += ", Max(VMain.V_Date) As InvoiceDate "
                mQry += ", Max(VMain.V_Type) As DocType "
                mQry += ", Max(VMain.InvoiceNo) As InvoiceNo "
                mQry += ", Max(VMain.SaleToPartyName) As Party "
                mQry += ", VMain.ItemCategory "
                mQry += ", Sum(VMain.Qty) As Qty "
                mQry += ", Max(VMain.Unit) As Unit "
                mQry += ", Sum(VMain.Taxable_Amount) As TaxableAmount "
                mQry += ", Sum(VMain.Igst) As Igst "
                mQry += ", Sum(VMain.Cgst) As Cgst "
                mQry += ", Sum(VMain.Sgst) As Sgst "
                mQry += ", Sum(VMain.Cess) As Cess "
                mQry += ", Sum(VMain.Net_Amount) As NetAmount "
                mQry += " From (" & mMainQry & ") As VMain "
                mQry += " GROUP By VMain.DocId, VMain.ItemCategory "
                mQry += " Order By Max(VMain.V_Date), Cast(Max(Replace(Vmain.InvoiceNo,'-','')) as Integer) "
            Else
                Dim mGroupColumns As String = ""
                Dim mSelectColumns As String = ""
                Dim mOrderColumns As String = ""

                If ReportFrm.FGetCode(rowSummerise).ToString.Contains("State") Then
                    mGroupColumns += IIf(mGroupColumns = "", "", ",") + "VMain.StateCode,VMain.StateName"
                    mSelectColumns += IIf(mSelectColumns = "", "", ",") + "VMain.StateName"
                    mOrderColumns += IIf(mOrderColumns = "", "", ",") + "VMain.StateName"
                End If

                If ReportFrm.FGetCode(rowSummerise).ToString.Contains("HSN") Then
                    mGroupColumns += IIf(mGroupColumns = "", "", ",") + "VMain.HSN"
                    mSelectColumns += IIf(mSelectColumns = "", "", ",") + "VMain.HSN"
                    mOrderColumns += IIf(mOrderColumns = "", "", ",") + "VMain.HSN"
                End If

                If ReportFrm.FGetCode(rowSummerise).ToString.Contains("Item Category") Then
                    mGroupColumns += IIf(mGroupColumns = "", "", ",") + "VMain.ItemCategoryCode, VMain.ItemCategory"
                    mSelectColumns += IIf(mSelectColumns = "", "", ",") + "VMain.ItemCategory"
                    mOrderColumns += IIf(mOrderColumns = "", "", ",") + "VMain.ItemCategory"
                End If

                If ReportFrm.FGetCode(rowSummerise).ToString.Contains("Item Tax Group") Then
                    mGroupColumns += IIf(mGroupColumns = "", "", ",") + "VMain.SalesTaxGroupItem"
                    mSelectColumns += IIf(mSelectColumns = "", "", ",") + "VMain.SalesTaxGroupItem as ItemTaxGroup"
                    mOrderColumns += IIf(mOrderColumns = "", "", ",") + "VMain.SalesTaxGroupItem"
                End If

                If ReportFrm.FGetCode(rowSummerise).ToString.Contains("Month") Then
                    mGroupColumns += IIf(mGroupColumns = "", "", ",") + "VMain.Month"
                    mSelectColumns += IIf(mSelectColumns = "", "", ",") + "VMain.Month as Month"
                    mOrderColumns += IIf(mOrderColumns = "", "", ",") + "Max(VMain.V_Date)"
                End If

                If ReportFrm.FGetCode(rowSummerise).ToString.Contains("Party Tax Group") Then
                    mGroupColumns += IIf(mGroupColumns = "", "", ",") + "VMain.SalesTaxGroupParty"
                    mSelectColumns += IIf(mSelectColumns = "", "", ",") + "VMain.SalesTaxGroupParty as PartyTaxGroup"
                    mOrderColumns += IIf(mOrderColumns = "", "", ",") + "VMain.SalesTaxGroupParty"
                End If

                If ReportFrm.FGetCode(rowSummerise).ToString.Contains("Voucher Type") Then
                    mGroupColumns += IIf(mGroupColumns = "", "", ",") + "VMain.VoucherType"
                    mSelectColumns += IIf(mSelectColumns = "", "", ",") + "VMain.VoucherType as VoucherType"
                    mOrderColumns += IIf(mOrderColumns = "", "", ",") + "VMain.VoucherType"
                End If


                mQry = " Select Max(VMain.DocId) As SearchCode, "
                mQry += mSelectColumns
                mQry += " , Round(Sum(VMain.Qty),3) as Qty "
                mQry += " , Sum(VMain.Taxable_Amount) as TaxableAmount "
                mQry += " , Sum(VMain.Igst) as Igst "
                mQry += " , Sum(VMain.Cgst) as Cgst "
                mQry += " , Sum(VMain.Sgst) as Sgst "
                mQry += " , Sum(VMain.Cess) as Cess "
                mQry += " , Sum(VMain.Net_Amount) as NetAmount "
                mQry += " From (" & mMainQry & ") as VMain "
                mQry += " Group By " + mGroupColumns
                mQry += " Order By " + mOrderColumns
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "GST Output Tax Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"

            ReportFrm.ProcFillGrid(DsHeader)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    Private Sub ReportFrm_FilterSelectionValidated(rowIndex As Integer) Handles ReportFrm.FilterSelectionValidated
        Select Case rowIndex
            Case rowReportType
                If ReportFrm.FGetCode(rowIndex) = "HSN Wise Summary" Then
                    ReportFrm.FilterGrid.Item(GFilter, rowSummerise).Value = "HSN,Item Tax Group,ItemCategory"
                    ReportFrm.FilterGrid.Item(GFilterCode, rowSummerise).Value = "HSN,Item Tax Group,ItemCategory"
                ElseIf ReportFrm.FGetCode(rowIndex) = "State Wise Summary" Then
                    ReportFrm.FilterGrid.Item(GFilter, rowSummerise).Value = "State,Item Tax Group"
                    ReportFrm.FilterGrid.Item(GFilterCode, rowSummerise).Value = "State,Item Tax Group"
                ElseIf ReportFrm.FGetCode(rowIndex) = "Item Tax Group Wise Summary" Then
                    ReportFrm.FilterGrid.Item(GFilter, rowSummerise).Value = "Item Tax Group"
                    ReportFrm.FilterGrid.Item(GFilterCode, rowSummerise).Value = "Item Tax Group"
                ElseIf ReportFrm.FGetCode(rowIndex) = "Customise Summary" Then
                    ReportFrm.FilterGrid.Item(GFilter, rowSummerise).Value = ""
                    ReportFrm.FilterGrid.Item(GFilterCode, rowSummerise).Value = ""
                Else
                    ReportFrm.FilterGrid.Item(GFilter, rowSummerise).Value = "HSN,Item Tax Group,ItemCategory"
                    ReportFrm.FilterGrid.Item(GFilterCode, rowSummerise).Value = "HSN,Item Tax Group,ItemCategory"
                End If
        End Select
    End Sub
End Class
