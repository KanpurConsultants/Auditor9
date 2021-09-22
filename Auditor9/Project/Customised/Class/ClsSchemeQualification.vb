Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsSchemeQualification

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
    Public Const Col1SchemeCode As String = "Scheme Code"
    Public Const Col1Scheme As String = "Scheme"
    Public Const Col1SubCode As String = "Sub Code"
    Public Const Col1PartyName As String = "Party Name"
    Public Const Col1SalesTaxGroupParty As String = "Sales Tax Group Party"
    Public Const Col1PlaceOfSupply As String = "Place Of Supply"
    Public Const Col1PostToAccount As String = "Post To Account"
    Public Const Col1PostToAccountName As String = "Post To Account Name"
    Public Const Col1PostEntryAs As String = "Post Entry As"
    Public Const Col1SchemeProcess As String = "Scheme Process"
    Public Const Col1PostToSubGroupType As String = "Post To Sub Group Type"
    Public Const Col1SchemeAmount As String = "Scheme Amount"
    Public Const Col1SalesTaxGroupItem As String = "Sales Tax Group Item"

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
    Dim mHelpSubGroupQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg Where Sg.SubGroupType = '" & SubgroupType.Customer & "' "
    Dim mHelpSchemeQry$ = "Select Code, Description As [Scheme] From SchemeHead "
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("Scheme", "Scheme", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpSchemeQry, "")
            ReportFrm.CreateHelpGrid("SchemeProcessDate", "Scheme Process Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.BtnProceed.Visible = True
            ReportFrm.BtnProceed.Text = "Save"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcSchemeQualification()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcSchemeQualification(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Scheme Qualification"

            If ReportFrm.FGetText(0) = "" Then
                MsgBox("Please Select Scheme...!", MsgBoxStyle.Information)
                Exit Sub
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If

            Dim bApplyOn As String = "", bSchemeFromDate As String = "", bSchemeToDate As String = "", bSchemeBase As String = "", bSchemePostToSubGroupType As String = ""

            mQry = " Select * From SchemeHead H Where H.Code = '" & ReportFrm.FGetCode(0) & "'"
            Dim DtScheme As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtScheme.Rows.Count > 0 Then
                bApplyOn = AgL.XNull(DtScheme.Rows(0)("ApplyOn"))
                bSchemeBase = AgL.XNull(DtScheme.Rows(0)("Base"))
                bSchemeFromDate = AgL.XNull(DtScheme.Rows(0)("FromDate"))
                bSchemeToDate = AgL.XNull(DtScheme.Rows(0)("ToDate"))
                bSchemePostToSubGroupType = AgL.XNull(DtScheme.Rows(0)("PostToSubGroupType"))
            End If


            mCondStr = " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(bSchemeFromDate).ToString("s")) & " And " & AgL.Chk_Date(CDate(bSchemeToDate).ToString("s")) & " "

            Dim mSchemeQry As String = ""

            'Base Qry For Getting Invoices On Selected Date Criteria and Applicable Schemes On this Date Criteria
            mSchemeQry = "SELECT L.DocId, L.Sr, H.SaleToParty As Party, Sh.Code As SchemeCode, L.Item, L.SalesTaxGroupItem, "

            If bSchemeBase = "Net Amount" Then
                mSchemeQry += " Case When IfNull(L.Net_Amount,0) = 0 Then L.Amount Else L.Net_Amount End - IfNull(VReturn.ItemReturnAmount,0) As ItemInvoiceAmount, "
            ElseIf bSchemeBase = "Rate" Then
                mSchemeQry += " (IfNull(L.Qty,0) * IfNull(L.Rate,0)) - IfNull(VReturn.ItemReturnAmount,0) As ItemInvoiceAmount, "
            ElseIf bSchemeBase = "Taxable Amount" Then
                mSchemeQry += " Case When IfNull(L.Taxable_Amount,0) = 0 Then L.Amount Else L.Taxable_Amount End - IfNull(VReturn.ItemReturnAmount,0) As ItemInvoiceAmount, "
            End If


            mSchemeQry += " L.Qty - IfNull(VReturn.ItemReturnQty,0) As ItemInvoiceQty
                    From SaleInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode 
                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN (
                        SELECT L.SaleInvoice, L.SaleInvoiceSr, "

            If bSchemeBase = "Net Amount" Then
                mSchemeQry += " Sum(Case When IfNull(L.Net_Amount,0) = 0 Then L.Amount Else L.Net_Amount End) As ItemReturnAmount, "
            ElseIf bSchemeBase = "Rate" Then
                mSchemeQry += " Sum(IfNull(L.Qty,0) * IfNull(L.Rate,0)) As ItemReturnAmount, "
            ElseIf bSchemeBase = "Taxable Amount" Then
                mSchemeQry += " Sum(Case When IfNull(L.Taxable_Amount,0) = 0 Then L.Amount Else L.Net_Amount End) As ItemReturnAmount, "
            End If


            mSchemeQry += " Sum(L.Qty) As ItemReturnQty
                        From SaleInvoice H 
                        LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Where Vt.NCat = '" & IIf(bApplyOn = "Multiple Order" Or bApplyOn = "Single Order", Ncat.SaleOrderCancel, Ncat.SaleReturn) & "'
                        GROUP BY L.SaleInvoice, L.SaleInvoiceSr
                    ) As VReturn On L.DocId = VReturn.SaleInvoice And L.Sr = VReturn.SaleInvoiceSr
                    LEFT JOIN Item I ON L.Item = I.Code
                    LEFT JOIN SchemeHead Sh ON Sh.Code = '" & ReportFrm.FGetCode(0) & "'
                    LEFT JOIN (
                        Select L.DocId, Sum(L.Qty) As TotalQty FROM SaleInvoiceDetail L GROUP By L.DocId
                    ) AS VInvoiceDetail On H.DocId = VInvoiceDetail.DocId "


            'Get Count Of Inclusion Of Party, Item, ItemGroup, ItemCategory, Site, Division
            mSchemeQry += "  LEFT JOIN (	
                                SELECT Code, Count(*) AS Cnt FROM SchemePartyDetail WHERE IsNull(IsExcluded,0) = 0 GROUP BY Code
                            ) AS VSchemePartyIncludedCount ON Sh.Code = VSchemePartyIncludedCount.Code
                            LEFT JOIN (	
                                SELECT Code, Count(*) AS Cnt FROM SchemeItemDetail WHERE IsNull(IsExcluded,0) = 0 GROUP BY Code
                            ) AS VSchemeItemIncludedCount ON Sh.Code = VSchemeItemIncludedCount.Code
                            LEFT JOIN (	
                                SELECT Code, Count(*) AS Cnt FROM SchemeItemGroupDetail WHERE IsNull(IsExcluded,0) = 0 GROUP BY Code
                            ) AS VSchemeItemGroupIncludedCount ON Sh.Code = VSchemeItemIncludedCount.Code
                            LEFT JOIN (	
                                SELECT Code, Count(*) AS Cnt FROM SchemeItemCategoryDetail WHERE IsNull(IsExcluded,0) = 0 GROUP BY Code
                            ) AS VSchemeItemCategoryIncludedCount ON Sh.Code = VSchemeItemIncludedCount.Code
                            LEFT JOIN (	
                                SELECT Code, Count(*) AS Cnt FROM SchemeSiteDetail WHERE IsNull(IsExcluded,0) = 0 GROUP BY Code
                            ) AS VSchemeSiteIncludedCount ON Sh.Code = VSchemeSiteIncludedCount.Code
                            LEFT JOIN (	
                                SELECT Code, Count(*) AS Cnt FROM SchemeDivisionDetail WHERE IsNull(IsExcluded,0) = 0 GROUP BY Code
                            ) AS VSchemeDivisionIncludedCount ON Sh.Code = VSchemeDivisionIncludedCount.Code"

            'Get Data If These Schemes Inclusion And Exclusions Of Party, Item, ItemGroup, ItemCategory, Site, Division
            mSchemeQry += " LEFT JOIN (SELECT * FROM SchemePartyDetail WHERE IsNull(IsExcluded,0) = 0) AS SchemePartyIncluded ON H.SaleToParty = SchemePartyIncluded.SubCode
                    LEFT JOIN (SELECT * FROM SchemePartyDetail WHERE IsNull(IsExcluded,0) <> 0) AS SchemePartyExcluded ON H.SaleToParty = SchemePartyExcluded.SubCode
                    LEFT JOIN (SELECT * FROM SchemeItemDetail WHERE IsNull(IsExcluded,0) = 0) AS SchemeItemIncluded ON L.Item = SchemeItemIncluded.Item 
                    LEFT JOIN (SELECT * FROM SchemeItemDetail WHERE IsNull(IsExcluded,0) <> 0) AS SchemeItemExcluded ON L.Item = SchemeItemExcluded.Item 
                    LEFT JOIN (SELECT * FROM SchemeItemGroupDetail WHERE IsNull(IsExcluded,0) = 0) AS SchemeItemGroupIncluded ON I.ItemGroup = SchemeItemGroupIncluded.Item 
                    LEFT JOIN (SELECT * FROM SchemeItemGroupDetail WHERE IsNull(IsExcluded,0) <> 0) AS SchemeItemGroupExcluded ON I.ItemGroup = SchemeItemGroupExcluded.Item 
                    LEFT JOIN (SELECT * FROM SchemeItemCategoryDetail WHERE IsNull(IsExcluded,0) = 0) AS SchemeItemCategoryIncluded ON I.ItemCategory = SchemeItemCategoryIncluded.Item 
                    LEFT JOIN (SELECT * FROM SchemeItemCategoryDetail WHERE IsNull(IsExcluded,0) <> 0) AS SchemeItemCategoryExcluded ON I.ItemCategory = SchemeItemCategoryExcluded.Item 
                    LEFT JOIN (SELECT * FROM SchemeSiteDetail WHERE IsNull(IsExcluded,0) = 0) AS SchemeSiteIncluded ON H.Site_Code = SchemeSiteIncluded.Code
                    LEFT JOIN (SELECT * FROM SchemeSiteDetail WHERE IsNull(IsExcluded,0) <> 0) AS SchemeSiteExcluded ON H.Site_Code = SchemeSiteExcluded.Code
					LEFT JOIN (SELECT * FROM SchemeDivisionDetail WHERE IsNull(IsExcluded,0) = 0) AS SchemeDivisionIncluded ON H.Div_Code = SchemeDivisionIncluded.Div_Code
                    LEFT JOIN (SELECT * FROM SchemeDivisionDetail WHERE IsNull(IsExcluded,0) <> 0) AS SchemeDivisionExcluded ON H.Div_Code = SchemeDivisionExcluded.Div_Code "

            'Date Condition And Other Condision On Basis Of Inclusion Of Exclusion Of Scheme
            mSchemeQry += " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(bSchemeFromDate).ToString("s")) & " And " & AgL.Chk_Date(CDate(bSchemeToDate).ToString("s")) & "
                    AND Vt.NCat = '" & IIf(bApplyOn = "Multiple Order" Or bApplyOn = "Single Order", Ncat.SaleOrder, Ncat.SaleInvoice) & "'
                    AND (IsNull(VSchemePartyIncludedCount.Cnt,0) = 0 OR SchemePartyIncluded.SubCode IS NOT NULL) 
                    AND SchemePartyExcluded.SubCode IS NULL
                    AND (IsNull(VSchemeItemIncludedCount.Cnt,0) = 0 OR SchemeItemIncluded.Item IS NOT NULL)
                    AND SchemeItemExcluded.Code IS NULL
                    AND (IsNull(VSchemeItemGroupIncludedCount.Cnt,0) = 0 OR SchemeItemGroupIncluded.Item IS NOT NULL)
                    AND SchemeItemGroupExcluded.Code IS NULL
                    AND (IsNull(VSchemeItemCategoryIncludedCount.Cnt,0) = 0 OR SchemeItemCategoryIncluded.Item IS NOT NULL)
                    AND SchemeItemCategoryExcluded.Code IS NULL
                    AND (IsNull(VSchemeSiteIncludedCount.Cnt,0) = 0 OR SchemeSiteIncluded.Site_Code IS NOT NULL)
                    AND SchemeSiteExcluded.Code IS NULL
                    AND (IsNull(VSchemeDivisionIncludedCount.Cnt,0) = 0 OR SchemeDivisionIncluded.Div_Code IS NOT NULL)
                    AND SchemeDivisionExcluded.Code IS NULL
                    AND Sh.Description IS NOT NULL "

            mQry = "Select VScheme.DocId, VScheme.Sr, VScheme.Party, VScheme.Item, VScheme.SalesTaxGroupItem, 
                        VScheme.SchemeCode As SchemeCode,
                        Sum(VScheme.ItemInvoiceAmount) As ItemInvoiceAmount, 0 As SchemeAmount
                        From (" & mSchemeQry & ") As VScheme
                        Group By VScheme.DocId, VScheme.Sr, VScheme.Party, VScheme.Item, VScheme.SalesTaxGroupItem, VScheme.SchemeCode "
            Dim DtSchemeAppliedInvoiceLines As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If bApplyOn = "Multiple Invoice" Or bApplyOn = "Multiple Order" Then
                mQry = "Select VScheme.Party, VScheme.Item, VScheme.SalesTaxGroupItem, VScheme.SchemeCode As SchemeCode, 
                        Sum(VScheme.ItemInvoiceAmount) As ItemInvoiceAmount, 0 As SchemeAmount
                        From (" & mSchemeQry & ") As VScheme
                        Group By VScheme.Party, VScheme.Item, VScheme.SalesTaxGroupItem, VScheme.SchemeCode "
                Dim DtSchemeAppliedPartyItems As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                mQry = " Select * From SchemeDetail Where Code = '" & ReportFrm.FGetCode(0) & "' Order By ValueGreaterThen "
                Dim DtSchemeDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To DtSchemeAppliedPartyItems.Rows.Count - 1
                    For J As Integer = 0 To DtSchemeDetail.Rows.Count - 1
                        If AgL.VNull(DtSchemeAppliedPartyItems.Rows(I)("ItemInvoiceAmount")) > AgL.VNull(DtSchemeDetail.Rows(J)("ValueGreaterThen")) Then
                            If AgL.VNull(DtSchemeDetail.Rows(J)("DiscountPer")) <> 0 Then
                                DtSchemeAppliedPartyItems.Rows(I)("SchemeAmount") = Math.Round(AgL.VNull(DtSchemeAppliedPartyItems.Rows(I)("ItemInvoiceAmount")) * AgL.VNull(DtSchemeDetail.Rows(J)("DiscountPer")) / 100, 2)
                            Else
                                DtSchemeAppliedPartyItems.Rows(I)("SchemeAmount") = Math.Round(AgL.VNull(DtSchemeDetail.Rows(J)("DiscountAmount")), 2)
                            End If
                        End If
                    Next
                Next

                For I As Integer = 0 To DtSchemeAppliedInvoiceLines.Rows.Count - 1
                    For J As Integer = 0 To DtSchemeAppliedPartyItems.Rows.Count - 1
                        If AgL.XNull(DtSchemeAppliedInvoiceLines.Rows(I)("Item")) = AgL.XNull(DtSchemeAppliedPartyItems.Rows(J)("Item")) And
                                AgL.XNull(DtSchemeAppliedInvoiceLines.Rows(I)("Party")) = AgL.XNull(DtSchemeAppliedPartyItems.Rows(J)("Party")) Then
                            DtSchemeAppliedInvoiceLines.Rows(I)("SchemeAmount") = AgL.VNull(DtSchemeAppliedPartyItems.Rows(J)("SchemeAmount")) * AgL.VNull(DtSchemeAppliedInvoiceLines.Rows(I)("ItemInvoiceAmount")) / AgL.VNull(DtSchemeAppliedPartyItems.Rows(J)("ItemInvoiceAmount"))
                        End If
                    Next
                Next


                Try
                    mQry = "Drop Table #TempScheme"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Catch ex As Exception
                End Try


                mQry = " CREATE Temporary TABLE #TempScheme (DocId  nvarchar(21), Sr Int, SchemeCode nvarchar(10), 
                        Item nvarchar(10), SalesTaxGroupItem nvarchar(10), LineAmount FLOAT, SchemeAmount FLOAT);	"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                For I As Integer = 0 To DtSchemeAppliedInvoiceLines.Rows.Count - 1
                    mQry = " INSERT INTO #TempScheme(DocId, Sr, SchemeCode, Item, SalesTaxGroupItem, LineAmount, SchemeAmount)	
                            Select '" & AgL.XNull(DtSchemeAppliedInvoiceLines.Rows(I)("DocId")) & "', 
                            '" & AgL.VNull(DtSchemeAppliedInvoiceLines.Rows(I)("Sr")) & "', 
                            '" & AgL.XNull(DtSchemeAppliedInvoiceLines.Rows(I)("SchemeCode")) & "', 
                            '" & AgL.XNull(DtSchemeAppliedInvoiceLines.Rows(I)("Item")) & "', 
                            '" & AgL.XNull(DtSchemeAppliedInvoiceLines.Rows(I)("SalesTaxGroupItem")) & "', 
                            '" & AgL.VNull(DtSchemeAppliedInvoiceLines.Rows(I)("ItemInvoiceAmount")) & "', 
                            '" & AgL.VNull(DtSchemeAppliedInvoiceLines.Rows(I)("SchemeAmount")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Next

                If bApplyOn = "Multiple Invoice" Then
                    mQry = "SELECT 'þ' As Tick, H.DocId As SearchCode, Sg.SubCode As SubCode, Sg.Name As PartyName, H.ManualRefNo As InvoiceNo, 
                            H.V_Date, H.SalesTaxGroupParty, H.PlaceOfSupply,
                            IfNull(VInvoiceDetail.InvoiceAmount,0) - IfNull(VReturn.ReturnAmount,0) As InvoiceAmount, 
                            IfNull(VInvoiceDetail.TotalQty,0) - IfNull(VReturn.ReturnQty,0) As InvoiceQty,
                            Sh.Description As [Scheme], Sh.Code As SchemeCode, Sh.PostToAccount, 
                            SPTA.Name As PostToAccountName, Sh.PostEntryAs, Sh.Process As SchemeProcess,Sh.PostToSubGroupType,
                            T.SalesTaxGroupItem, T.SchemeAmount
                            From (Select DocId, SchemeCode, SalesTaxGroupItem, Sum(SchemeAmount) As SchemeAmount 
                                    From #TempScheme Group By DocId, SchemeCode, SalesTaxGroupItem) As T 
                            LEFT JOIN SaleBill H ON T.DocId = H.DocId
                            LEFT JOIN (
                                SELECT L.SaleBill, "

                    If bSchemeBase = "Net Amount" Then
                        mQry += " Sum(Case When IfNull(L.Net_Amount,0) = 0 Then L.Amount Else L.Net_Amount End) As ReturnAmount, "
                    ElseIf bSchemeBase = "Rate" Then
                        mQry += " Sum(IfNull(L.Qty,0) * IfNull(L.Rate,0)) As ReturnAmount, "
                    ElseIf bSchemeBase = "Taxable Amount" Then
                        mQry += " Sum(Case When IfNull(L.Taxable_Amount,0) = 0 Then L.Amount Else L.Taxable_Amount End) As ReturnAmount, "
                    End If

                    mQry += "Sum(-L.Qty) As ReturnQty
                                From SaleBill H 
                                LEFT JOIN SaleBillDetail L On H.DocId = L.DocId
                                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                                Where Vt.NCat = '" & Ncat.SaleReturn & "'
                                GROUP BY L.SaleBill
                            ) As VReturn On H.DocId = VReturn.SaleBill 
                            LEFT JOIN SchemeHead Sh On T.SchemeCode = Sh.Code
                            LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode 
                            LEFT JOIN SubGroup SPTA On Sh.PostToAccount = SPTA.SubCode
                            LEFT JOIN SchemeQulified Sq On H.DocId = Sq.DocId And Sh.Code = Sq.Scheme
                            LEFT JOIN (
                                Select L.DocId, "

                    If bSchemeBase = "Net Amount" Then
                        mQry += " Sum(Case When IfNull(L.Net_Amount,0) = 0 Then L.Amount Else L.Net_Amount End) As InvoiceAmount, "
                    ElseIf bSchemeBase = "Rate" Then
                        mQry += " Sum(IfNull(L.Qty,0) * IfNull(L.Rate,0)) As InvoiceAmount, "
                    ElseIf bSchemeBase = "Taxable Amount" Then
                        mQry += " Sum(Case When IfNull(L.Taxable_Amount,0) = 0 Then L.Amount Else L.Taxable_Amount End) As InvoiceAmount, "
                    End If

                    mQry += " Sum(L.Qty) As TotalQty FROM SaleBillDetail L GROUP By L.DocId
                            ) AS VInvoiceDetail On H.DocId = VInvoiceDetail.DocId
                            Where IfNull(T.SchemeAmount,0) > 0 
                            AND Sq.Code Is Null 
                            Order By Sg.Name "

                ElseIf bApplyOn = "Multiple Order" Then

                    mQry = "SELECT 'þ' As Tick, H.DocId As SearchCode, T.SubCode, T.PartyName As PartyName, H.ManualRefNo As InvoiceNo, 
                            H.V_Date, H.SalesTaxGroupParty, H.PlaceOfSupply, 
                            IfNull(VInvoiceDetail.InvoiceAmount, 0) - IfNull(VReturn.ReturnAmount,0) As InvoiceAmount, 
                            IfNull(VInvoiceDetail.TotalQty,0) - IfNull(VReturn.ReturnQty,0) As InvoiceQty,
                            Sh.Description As [Scheme], Sh.Code As SchemeCode, Sh.PostToAccount, 
                            SPTA.Name As PostToAccountName, Sh.PostEntryAs, Sh.Process As SchemeProcess,Sh.PostToSubGroupType,
                            T.SalesTaxGroupItem, T.SchemeAmount
                            From (Select H.DocId, "

                    If bSchemePostToSubGroupType = SubgroupType.Supplier Then
                        mQry += " Supplier.SubCode, Supplier.Name As PartyName, "
                    Else
                        mQry += " Customer.SubCode, Customer.Name As PartyName, "
                    End If

                    mQry += " T.SchemeCode, T.SalesTaxGroupItem, Sum(T.SchemeAmount * "

                    If bSchemeBase = "Net Amount" Then
                        mQry += " (Case When IfNull(L.Net_Amount,0) = 0 Then L.Amount Else L.Net_Amount End) "
                    ElseIf bSchemeBase = "Rate" Then
                        mQry += " (IfNull(L.Qty,0) * IfNull(L.Rate,0)) "
                    ElseIf bSchemeBase = "Taxable Amount" Then
                        mQry += " (Case When IfNull(L.Taxable_Amount,0) = 0 Then L.Amount Else L.Taxable_Amount End) "
                    End If

                    mQry += " / T.LineAmount) As SchemeAmount 
                                From #TempScheme T
                                LEFT JOIN SaleBillDetail L On T.DocId = L.SaleOrder And T.Sr = L.SaleOrderSr
                                LEFT JOIN SaleBill H On L.DocId = H.DocId
                                LEFT JOIN Item I On L.Item = I.Code
                                LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.COde
                                LEFT JOIN SubGroup Supplier On Ig.DefaultSupplier = Supplier.SubCode
                                LEFT JOIN SubGroup Customer On H.SaleToParty = Customer.SubCode
                                GROUP By H.DocId, "

                    If bSchemePostToSubGroupType = SubgroupType.Supplier Then
                        mQry += " Supplier.SubCode, "
                    Else
                        mQry += " Customer.SubCode, "
                    End If

                    mQry += " T.SchemeCode, T.SalesTaxGroupItem) As T 
                            LEFT JOIN SaleBill H ON T.DocId = H.DocId
                            LEFT JOIN (
                                SELECT L.SaleBill, "

                    If bSchemeBase = "Net Amount" Then
                        mQry += " Sum(Case When IfNull(L.Net_Amount,0) = 0 Then L.Amount Else L.Net_Amount End) As ReturnAmount, "
                    ElseIf bSchemeBase = "Rate" Then
                        mQry += " Sum(IfNull(L.Qty,0) * IfNull(L.Rate,0)) As ReturnAmount, "
                    ElseIf bSchemeBase = "Taxable Amount" Then
                        mQry += " Sum(Case When IfNull(L.Taxable_Amount,0) = 0 Then L.Amount Else L.Taxable_Amount End) As ReturnAmount, "
                    End If

                    mQry += "   Sum(L.Qty) As ReturnQty
                                From SaleBill H 
                                LEFT JOIN SaleBillDetail L On H.DocId = L.DocId
                                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                                Where Vt.NCat = '" & Ncat.SaleReturn & "'
                                GROUP BY L.SaleBill
                            ) As VReturn On H.DocId = VReturn.SaleBill 
                            LEFT JOIN SchemeHead Sh On T.SchemeCode = Sh.Code
                            LEFT JOIN SubGroup SPTA On Sh.PostToAccount = SPTA.SubCode
                            LEFT JOIN SchemeQulified Sq On H.DocId = Sq.DocId And Sh.Code = Sq.Scheme
                            Left Join(
                                Select L.DocId, "

                    If bSchemeBase = "Net Amount" Then
                        mQry += " Sum(Case When IfNull(L.Net_Amount, 0) = 0 Then L.Amount Else L.Net_Amount End) As InvoiceAmount, "
                    ElseIf bSchemeBase = "Rate" Then
                        mQry += " Sum(IfNull(L.Qty,0) * IfNull(L.Rate,0)) As InvoiceAmount, "
                    ElseIf bSchemeBase = "Taxable Amount" Then
                        mQry += " Sum(Case When IfNull(L.Taxable_Amount,0) = 0 Then L.Amount Else L.Taxable_Amount End) As InvoiceAmount, "
                    End If

                    mQry += "   Sum(L.Qty) As TotalQty FROM SaleBillDetail L 
                                GROUP By L.DocId
                            ) AS VInvoiceDetail On H.DocId = VInvoiceDetail.DocId
                            Where IfNull(T.SchemeAmount,0) > 0 
                            AND Sq.Code Is Null
                            And H.DocId Is Not Null 
                            Order By T.PartyName "
                End If
                DsHeader = AgL.FillData(mQry, AgL.GCn)
            Else
                MsgBox("Single Invoice Scheme is not allowed...!", MsgBoxStyle.Information) : Exit Sub
            End If

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Scheme Qualification"
            ReportFrm.ClsRep = Me
            ReportFrm.IsHideZeroColumns = False
            'ReportFrm.IsAllowFind = False
            ReportFrm.ReportProcName = "ProcSchemeQualification"

            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.DGL1.Columns(Col1SchemeCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SubCode).Visible = False
            ReportFrm.DGL1.Columns(Col1PostToAccount).Visible = False
            ReportFrm.DGL1.Columns(Col1PostToAccountName).Visible = False
            ReportFrm.DGL1.Columns(Col1PostToSubGroupType).Visible = False
            ReportFrm.DGL1.Columns(Col1PostEntryAs).Visible = False
            ReportFrm.DGL1.Columns(Col1SchemeProcess).Visible = False

            ReportFrm.DGL1.ReadOnly = False

            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next

            AgCL.GridSetiingShowXml(ReportFrm.Text & "-Visible", ReportFrm.DGL1)
            AgCL.GridSetiingShowXml(ReportFrm.Text & "-Visible", ReportFrm.DGL2)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Public Sub FSaveSchemeQualificationEntries()
        Dim I As Integer = 0
        Dim mV_Type As String = ""
        Dim mTrans As String = ""

        If ReportFrm.FGetText(1) = "" Then
            MsgBox("Please input Scheme Process Date...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            For I = 0 To ReportFrm.DGL1.Rows.Count - 1
                If ReportFrm.DGL1.Item(Col1Select, I).Value = "þ" Then
                    If Val(ReportFrm.DGL1.Item(Col1SchemeAmount, I).Value) > 0 Then
                        If AgL.XNull(ReportFrm.DGL1.Item(Col1PostEntryAs, I).Value) = "Journal Voucher" Then
                            FPostSchemeJournalEntry(I, "JV")
                        Else
                            mV_Type = ""
                            If AgL.XNull(ReportFrm.DGL1.Item(Col1SchemeProcess, I).Value) = Process.Sales Then
                                If AgL.XNull(ReportFrm.DGL1.Item(Col1PostToSubGroupType, I).Value) = SubgroupType.Customer Then
                                    mV_Type = "CNC"
                                ElseIf AgL.XNull(ReportFrm.DGL1.Item(Col1PostToSubGroupType, I).Value) = SubgroupType.Supplier Then
                                    mV_Type = "DNS"
                                End If
                            End If
                            FPostDebitCreditNoteEntry(I, mV_Type)
                        End If
                    End If
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
            MsgBox("Process Completed...!", MsgBoxStyle.Information)
            ReportFrm.DGL1.DataSource = Nothing
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FPostDebitCreditNoteEntry(bRowIndex As Integer, mV_Type As String)
        Dim bMultiplier As Integer = 1

        Dim Tot_Gross_Amount As Double = 0
        Dim Tot_Taxable_Amount As Double = 0
        Dim Tot_Tax1 As Double = 0
        Dim Tot_Tax2 As Double = 0
        Dim Tot_Tax3 As Double = 0
        Dim Tot_Tax4 As Double = 0
        Dim Tot_Tax5 As Double = 0
        Dim Tot_SubTotal1 As Double = 0

        bMultiplier = -1

        Tot_Gross_Amount = 0
        Tot_Taxable_Amount = 0
        Tot_Tax1 = 0
        Tot_Tax2 = 0
        Tot_Tax3 = 0
        Tot_Tax4 = 0
        Tot_Tax5 = 0
        Tot_SubTotal1 = 0

        Dim VoucherEntryTableList(0) As FrmVoucherEntry.StructLedgerHead
        Dim VoucherEntryTable As New FrmVoucherEntry.StructLedgerHead

        VoucherEntryTable.DocID = ""
        VoucherEntryTable.V_Type = mV_Type
        VoucherEntryTable.V_Prefix = ""
        VoucherEntryTable.Site_Code = AgL.PubSiteCode
        VoucherEntryTable.Div_Code = AgL.PubDivCode
        VoucherEntryTable.V_No = 0
        VoucherEntryTable.V_Date = CDate(ReportFrm.FGetText(1))
        VoucherEntryTable.ManualRefNo = ""
        VoucherEntryTable.Subcode = ReportFrm.DGL1.Item(Col1SubCode, bRowIndex).Value
        VoucherEntryTable.SubcodeName = ReportFrm.DGL1.Item(Col1PartyName, bRowIndex).Value

        If VoucherEntryTable.V_Type = "DNS" Or VoucherEntryTable.V_Type = "DNC" Then
            VoucherEntryTable.DrCr = "Dr"
        ElseIf VoucherEntryTable.V_Type = "CNS" Or VoucherEntryTable.V_Type = "CNC" Then
            VoucherEntryTable.DrCr = "Cr"
        End If

        VoucherEntryTable.SalesTaxGroupParty = ReportFrm.DGL1.Item(Col1SalesTaxGroupParty, bRowIndex).Value
        VoucherEntryTable.PlaceOfSupply = ReportFrm.DGL1.Item(Col1PlaceOfSupply, bRowIndex).Value
        VoucherEntryTable.StructureCode = ""
        VoucherEntryTable.CustomFields = ""
        VoucherEntryTable.Remarks = ReportFrm.FGetText(0)
        VoucherEntryTable.Status = "Active"
        VoucherEntryTable.EntryBy = AgL.PubUserName
        VoucherEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
        VoucherEntryTable.ApproveBy = ""
        VoucherEntryTable.ApproveDate = ""
        VoucherEntryTable.MoveToLog = ""
        VoucherEntryTable.MoveToLogDate = ""
        VoucherEntryTable.UploadDate = ""

        VoucherEntryTable.Deduction_Per = 0
        VoucherEntryTable.Deduction = 0
        VoucherEntryTable.Other_Charge_Per = 0
        VoucherEntryTable.Other_Charge = 0
        VoucherEntryTable.Round_Off = 0
        VoucherEntryTable.Net_Amount = 0

        VoucherEntryTable.Line_Sr = 1
        VoucherEntryTable.Line_SubCode = ""
        VoucherEntryTable.Line_SubCodeName = ""
        VoucherEntryTable.Line_Specification = ""
        VoucherEntryTable.Line_SalesTaxGroupItem = ReportFrm.DGL1.Item(Col1SalesTaxGroupItem, bRowIndex).Value
        VoucherEntryTable.Line_Qty = 0
        VoucherEntryTable.Line_Unit = ""
        VoucherEntryTable.Line_Rate = 0
        VoucherEntryTable.Line_Amount = Math.Round(Val(ReportFrm.DGL1.Item(Col1SchemeAmount, bRowIndex).Value), 2)
        VoucherEntryTable.Line_Amount = VoucherEntryTable.Line_Amount * bMultiplier
        VoucherEntryTable.Line_ChqRefNo = ""
        VoucherEntryTable.Line_ChqRefDate = ""
        VoucherEntryTable.Line_Remarks = ""

        Dim Tax1_Per As Double = 0
        Dim Tax2_Per As Double = 0
        Dim Tax3_Per As Double = 0

        FGetTaxRateForPurchase(Tax1_Per, Tax2_Per, Tax3_Per, VoucherEntryTable.Line_SalesTaxGroupItem,
                                           VoucherEntryTable.SalesTaxGroupParty, VoucherEntryTable.PlaceOfSupply)

        VoucherEntryTable.Line_Gross_Amount = VoucherEntryTable.Line_Amount
        VoucherEntryTable.Line_Taxable_Amount = VoucherEntryTable.Line_Amount
        VoucherEntryTable.Line_Tax1_Per = Tax1_Per
        VoucherEntryTable.Line_Tax1 = Math.Round(VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax1_Per / 100, 2)
        VoucherEntryTable.Line_Tax2_Per = Tax2_Per
        VoucherEntryTable.Line_Tax2 = Math.Round(VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax2_Per / 100, 2)
        VoucherEntryTable.Line_Tax3_Per = Tax3_Per
        VoucherEntryTable.Line_Tax3 = Math.Round(VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax3_Per / 100, 2)
        VoucherEntryTable.Line_Tax4_Per = 0
        VoucherEntryTable.Line_Tax4 = Math.Round(VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax4_Per / 100, 2)
        VoucherEntryTable.Line_Tax5_Per = 0
        VoucherEntryTable.Line_Tax5 = Math.Round(VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax5_Per / 100, 2)
        VoucherEntryTable.Line_SubTotal1 = (VoucherEntryTable.Line_Amount +
                                                            VoucherEntryTable.Line_Tax1 +
                                                            VoucherEntryTable.Line_Tax2 +
                                                            VoucherEntryTable.Line_Tax3 +
                                                            VoucherEntryTable.Line_Tax4 +
                                                            VoucherEntryTable.Line_Tax5)


        'For Header Values
        Tot_Gross_Amount += VoucherEntryTable.Line_Gross_Amount
        Tot_Taxable_Amount += VoucherEntryTable.Line_Taxable_Amount
        Tot_Tax1 += VoucherEntryTable.Line_Tax1
        Tot_Tax2 += VoucherEntryTable.Line_Tax2
        Tot_Tax3 += VoucherEntryTable.Line_Tax3
        Tot_Tax4 += VoucherEntryTable.Line_Tax4
        Tot_Tax5 += VoucherEntryTable.Line_Tax5
        Tot_SubTotal1 += VoucherEntryTable.Line_SubTotal1


        VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
        ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)


        VoucherEntryTableList(0).Gross_Amount = Tot_Gross_Amount
        VoucherEntryTableList(0).Taxable_Amount = Tot_Taxable_Amount
        VoucherEntryTableList(0).Tax1 = Tot_Tax1
        VoucherEntryTableList(0).Tax2 = Tot_Tax2
        VoucherEntryTableList(0).Tax3 = Tot_Tax3
        VoucherEntryTableList(0).Tax4 = Tot_Tax4
        VoucherEntryTableList(0).Tax5 = Tot_Tax5
        VoucherEntryTableList(0).SubTotal1 = Tot_SubTotal1
        VoucherEntryTableList(0).Other_Charge = 0
        VoucherEntryTableList(0).Deduction = 0
        VoucherEntryTableList(0).Round_Off = Math.Round(Math.Round(VoucherEntryTableList(0).SubTotal1) - VoucherEntryTableList(0).SubTotal1, 2)
        VoucherEntryTableList(0).Net_Amount = Math.Round(VoucherEntryTableList(0).SubTotal1)

        Dim Tot_RoundOff As Double = 0
        Dim Tot_NetAmount As Double = 0
        For J As Integer = 0 To VoucherEntryTableList.Length - 1
            VoucherEntryTableList(J).Line_Round_Off = Math.Round(VoucherEntryTableList(0).Round_Off * VoucherEntryTableList(J).Line_Gross_Amount / VoucherEntryTableList(0).Gross_Amount, 2)
            VoucherEntryTableList(J).Line_Net_Amount = Math.Round(VoucherEntryTableList(0).Net_Amount * VoucherEntryTableList(J).Line_Gross_Amount / VoucherEntryTableList(0).Gross_Amount, 2)
            Tot_RoundOff += VoucherEntryTableList(J).Line_Round_Off
            Tot_NetAmount += VoucherEntryTableList(J).Line_Net_Amount
        Next

        If Tot_RoundOff <> VoucherEntryTableList(0).Round_Off Then
            VoucherEntryTableList(0).Line_Round_Off = VoucherEntryTableList(0).Line_Round_Off + (VoucherEntryTableList(0).Round_Off - Tot_RoundOff)
        End If

        If Tot_NetAmount <> VoucherEntryTableList(0).Net_Amount Then
            VoucherEntryTableList(0).Line_Net_Amount = VoucherEntryTableList(0).Line_Net_Amount + (VoucherEntryTableList(0).Net_Amount - Tot_NetAmount)
        End If
        Dim bCreditNoteDocId As String = FrmVoucherEntry.InsertLedgerHead(VoucherEntryTableList)

        Dim bCode As String = AgL.GetMaxId("SchemeQulified", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        mQry = " INSERT INTO SchemeQulified(Code, DocId, Scheme, GeneratedDocId)
                Select '" & bCode & "', '" & ReportFrm.DGL1.Item(Col1SearchCode, bRowIndex).Value & "', 
                '" & ReportFrm.DGL1.Item(Col1SchemeCode, bRowIndex).Value & "', '" & bCreditNoteDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    End Sub
    Private Sub FPostSchemeJournalEntry(bRowIndex As Integer, mV_Type As String)
        Dim VoucherEntryTableList(0) As AgAccounts.FrmVoucherEntry.StructVoucherEntry
        Dim VoucherEntryTable As New AgAccounts.FrmVoucherEntry.StructVoucherEntry

        VoucherEntryTable.V_Type = mV_Type
        VoucherEntryTable.Site_Code = AgL.PubSiteCode
        VoucherEntryTable.Div_Code = AgL.PubDivCode
        VoucherEntryTable.V_No = 0
        VoucherEntryTable.V_Date = CDate(ReportFrm.FGetText(1))
        VoucherEntryTable.Line_V_Date = CDate(ReportFrm.FGetText(1))
        VoucherEntryTable.Line_SubCode = ReportFrm.DGL1.Item(Col1SubCode, bRowIndex).Value
        VoucherEntryTable.Line_SubCodeName = ReportFrm.DGL1.Item(Col1PartyName, bRowIndex).Value
        VoucherEntryTable.Line_ContraSub = ReportFrm.DGL1.Item(Col1PostToAccount, bRowIndex).Value
        VoucherEntryTable.Line_ContraSubName = ReportFrm.DGL1.Item(Col1PostToAccountName, bRowIndex).Value

        If ReportFrm.DGL1.Item(Col1SchemeProcess, bRowIndex).Value = Process.Sales Then
            If ReportFrm.DGL1.Item(Col1PostToSubGroupType, bRowIndex).Value = SubgroupType.Supplier Then
                VoucherEntryTable.Line_AmtDr = Math.Round(Val(ReportFrm.DGL1.Item(Col1SchemeAmount, bRowIndex).Value), 2)
                VoucherEntryTable.Line_AmtCr = 0
            Else
                VoucherEntryTable.Line_AmtDr = 0
                VoucherEntryTable.Line_AmtCr = Math.Round(Val(ReportFrm.DGL1.Item(Col1SchemeAmount, bRowIndex).Value), 2)
            End If
        End If


        VoucherEntryTable.Line_Narration = "Scheme " + ReportFrm.DGL1.Item(Col1Scheme, bRowIndex).Value + " Processed."
        VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
        ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)

        VoucherEntryTable.V_Type = mV_Type
        VoucherEntryTable.Site_Code = AgL.PubSiteCode
        VoucherEntryTable.Div_Code = AgL.PubDivCode
        VoucherEntryTable.V_No = 0
        VoucherEntryTable.V_Date = CDate(ReportFrm.FGetText(1))
        VoucherEntryTable.Line_V_Date = CDate(ReportFrm.FGetText(1))
        VoucherEntryTable.Line_SubCode = ReportFrm.DGL1.Item(Col1PostToAccount, bRowIndex).Value
        VoucherEntryTable.Line_SubCodeName = ReportFrm.DGL1.Item(Col1PostToAccountName, bRowIndex).Value
        VoucherEntryTable.Line_ContraSub = ReportFrm.DGL1.Item(Col1SubCode, bRowIndex).Value
        VoucherEntryTable.Line_ContraSubName = ReportFrm.DGL1.Item(Col1PartyName, bRowIndex).Value


        If ReportFrm.DGL1.Item(Col1SchemeProcess, bRowIndex).Value = Process.Sales Then
            If ReportFrm.DGL1.Item(Col1PostToSubGroupType, bRowIndex).Value = SubgroupType.Supplier Then
                VoucherEntryTable.Line_AmtDr = 0
                VoucherEntryTable.Line_AmtCr = Math.Round(Val(ReportFrm.DGL1.Item(Col1SchemeAmount, bRowIndex).Value), 2)
            Else
                VoucherEntryTable.Line_AmtDr = Math.Round(Val(ReportFrm.DGL1.Item(Col1SchemeAmount, bRowIndex).Value), 2)
                VoucherEntryTable.Line_AmtCr = 0
            End If
        End If



        VoucherEntryTable.Line_Narration = "Scheme " + ReportFrm.DGL1.Item(Col1Scheme, bRowIndex).Value + " Processed."
        VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
        ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)

        Dim bGeneratedDocId As String = AgAccounts.FrmVoucherEntry.InsertVoucherEntry(VoucherEntryTableList)

        Dim bCode As String = AgL.GetMaxId("SchemeQulified", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        mQry = " INSERT INTO SchemeQulified(Code, DocId, Scheme, GeneratedDocId)
                                Select '" & bCode & "', '" & ReportFrm.DGL1.Item(Col1SearchCode, bRowIndex).Value & "', 
                                '" & ReportFrm.DGL1.Item(Col1SchemeCode, bRowIndex).Value & "', '" & bGeneratedDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FGetTaxRateForPurchase(ByRef Tax1_Per As Double, ByRef Tax2_Per As Double,
                                        ByRef Tax3_Per As Double, SalesTaxGroupItem As String,
                                        SalesTaxGroupParty As String, PlaceOfSupply As String)
        Dim mQry As String = ""
        mQry = " SELECT Max(Case When H.ChargeType = 'TAX1' Then H.Percentage Else 0 End) As Tax1_Per,
                Max(Case When H.ChargeType = 'TAX2' Then H.Percentage Else 0 End) As Tax2_Per,
                Max(Case When H.ChargeType = 'TAX3' Then H.Percentage Else 0 End) As Tax3_Per
                FROM PostingGroupSalesTax H With (NoLock)
                WHERE PostingGroupSalesTaxItem = '" & SalesTaxGroupItem & "' 
                AND PostingGroupSalesTaxParty = '" & SalesTaxGroupParty & "'
                AND PlaceOfSupply = '" & PlaceOfSupply & "'
                AND H.Process = 'SALES'"
        Dim DtTaxRates As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTaxRates.Rows.Count > 0 Then
            Tax1_Per = AgL.VNull(DtTaxRates.Rows(0)("Tax1_Per"))
            Tax2_Per = AgL.VNull(DtTaxRates.Rows(0)("Tax2_Per"))
            Tax3_Per = AgL.VNull(DtTaxRates.Rows(0)("Tax3_Per"))
        Else
            Tax1_Per = 0
            Tax2_Per = 0
            Tax3_Per = 0
        End If
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        FSaveSchemeQualificationEntries()
    End Sub
End Class
