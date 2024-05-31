Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms

Public Class ClsSalesTaxReports_OneDotSeven

#Region "Danger Zone"
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""

    Dim bSaleInvoiceVoucherCnt As Integer = 1

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4

    Dim rowReportType As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowNextFormat As Integer = 3
    Dim rowSite As Integer = 4
    Dim rowDivision As Integer = 5


    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

#End Region

#Region "Common Reports Constant"
    Private Const CityList As String = "CityList"
    Private Const UserWiseEntryReport As String = "UserWiseEntryReport"
    Private Const UserWiseEntryTargetReport As String = "UserWiseEntryTargetReport"
#End Region

#Region "GST Report Format Constants"
    Private Const B2B As String = "B2B Invoices - 4A, 4B, 4C, 6B, 6C"
    Private Const B2CL As String = "B2C (Large) Invoices - 5A, 5B"
    Private Const B2CS As String = "B2C (Small) Invoices - 7"
    Private Const CDNR As String = "Credit/Debit Note (Registered) - 9B"
    Private Const CDNUR As String = "Credit/Debit Note (UnRegistered) - 9B"
    Private Const EXP As String = "Export Invoice - 6A"
    Private Const AT As String = "Tax Liability (Advance Received) - 11A(1), 11A(2)"
    Private Const ATADJ As String = "Adjustment of Advance - 11B(1), 11B(2)"
    Private Const EXEMP As String = "Nil Rated Invoices - 8A, 8B, 8C, 8D"
    Private Const HSN As String = "HSN Wise Summary of Outward Supplies"
    Private Const DOCS As String = "Summary Of Documents issued during the tax period"

    'Detailed Formats
    Private Const B2CSDetail As String = "B2CS Detail"
    Private Const EXEMPDetail As String = "EXEMP Detail"
    Private Const HSNWiseDetail As String = "HSN Wise Detail"
    Private Const DocumentWiseDetail As String = "Document Wise Detail"


    '3B Constants
    Private Const OutwardTaxableSuppliesOtherThanZero As String = "Outward Taxable  supplies  (other than zero rated, nil rated And exempted)"
    Private Const OutwardTaxableSuppliesOtherThanZeroVTypeWiseSummary As String = "Outward Taxable  supplies  (other than zero rated, nil rated And exempted) [Summary]"

    Private Const OutwardTaxableSuppliesZeroRated As String = "Outward Taxable  supplies  (zero rated )"

    Private Const OutwardTaxableSuppliesNillRated As String = "Outward Taxable  supplies  (nil rated)"
    Private Const InwardSuppliesLiableToReverseCharge As String = "Inward Supplies Liable To Reverse Charge"

    Private Const InterStateSuppliesToUnRegesteredPerson As String = "Supplies made to Unregistered Person"
    Private Const InterStateSuppliesToCompositionPerson As String = "Supplies made to Composition Person"
    Private Const InterStateSuppliesToUINholders As String = "Supplies made to UIN holders"

    Private Const IntraStateExcemptAndNillRatedSupply As String = "Intra State Excempt And Nill Rated Supply"
    Private Const IntraStateNonGSTSupplies As String = "Supplies made to UIN holders"
    Private Const InwardSuppliesLiableToReverseChargeOtherThan1And2 As String = "Inward supplies liable To reverse charge(other than 1 & 2 above)"
    Private Const AllOtherITC As String = "All other ITC"
    Private Const AllOtherITCVTypeWiseSummary As String = "All other ITC  [Summary]"
#End Region

#Region "Reports Constant"
    Private Const GSTReports As String = "GSTReports"
#End Region

#Region "Queries Definition"
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpAreaQry$ = "Select 'o' As Tick, Code, Description From Area "
    Dim mHelpAcGroupCustomerQry$ = "Select 'o' As Tick, GroupCode, GroupName From AcGroup Where Nature='Customer' "
    Dim mHelpAcGroupSupplierQry$ = "Select 'o' As Tick, GroupCode, GroupName From AcGroup Where Nature='Supplier' "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where Code In (" & AgL.PubSiteList & ") "
    Dim mHelpDivisionQry$ = "Select Div_Code as Code, Div_Name As [Division] From Division Where Div_Code In (" & AgL.PubDivisionList & ") "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpSchemeQry$ = "Select Code, Description As [Scheme] From SchemeHead "

    Dim mHelpVendorQry$ = " Select 'o' As Tick,  H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpTableQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM HT_Table H "
    Dim mHelpPaymentModeQry$ = "Select 'o' As Tick, 'Cash' As Code, 'Cash' As Description " &
                                " UNION ALL " &
                                " Select 'o' As Tick, 'Credit' As Code, 'Credit' As Description "
    Dim mHelpOutletQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM Outlet H "
    Dim mHelpStewardQry$ = "Select 'o' As Tick,  Sg.SubCode AS Code, Sg.DispName AS Steward FROM SubGroup Sg  "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpLocationQry$ = " Select Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
    Dim mHelpPartySingleQry$ = " Select Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpTransporterQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.Transporter & "' "
    Dim mHelpSalesRepresentativeQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Sales Representative] FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code ='SREP' "
    Dim mHelpResponsiblePersonQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code <>'SREP' "
    Dim mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpYesNo$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpSaleOrderQry$ = " Select 'o' As Tick,  H.DocID AS Code, H.V_Type || '-' || H.ReferenceNo  FROM SaleOrder H "
    Dim mHelpSaleInvoiceQry$ = " SELECT 'o' As Tick,DocId, ReferenceNo AS BillNo, V_Date AS Date FROM SaleChallan "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick,I.Code,I.Description  AS ItemReportingGroup FROM ItemReportingGroup I "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description   FROM Tag H "
    Dim mHelpVoucherTypeQry$ = "SELECT 'o' As Tick, H.V_Type AS Code, H.Description FROM Voucher_Type H  "
#End Region

    Dim DsHeader As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""
    Dim mDocumentNoPattern As String = ""
    Dim mCompanyPrefix As String = ""

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim mQry As String
            Dim I As Integer = 0
            Select Case GRepFormName
                Case GSTReports
                    ReportFrm.BtnCustomMenu.Visible = True
                    mQry = "Select 'GST 3B' as Code, 'GST 3B' as Name 
                            Union All Select 'GSTR1' as Code, 'GSTR1' as Name "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "GST 3B")
                    Dim mLastMonthDate As String = DateAdd(DateInterval.Month, -1, CDate(AgL.Dman_Execute("SELECT date('now')", AgL.GCn).ExecuteScalar()))
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthStartDate(mLastMonthDate))
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthEndDate(mLastMonthDate))
                    ReportFrm.CreateHelpGrid("Next Format", "Next Format", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.StringType, "", "",,,,, False)
                    ReportFrm.FilterGrid.Rows(rowNextFormat).Visible = False
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    'ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            End Select

            bSaleInvoiceVoucherCnt = AgL.VNull(AgL.Dman_Execute("Select Count(*) As Cnt 
                    From Voucher_Type Where NCat = '" & Ncat.SaleInvoice & "'", AgL.GCn).ExecuteScalar())
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

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
        Select Case mGRepFormName
            Case GSTReports
                ProcGSTReports()
        End Select
    End Sub

    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub

#Region "GST Reports"
    Public Sub ProcGSTReports(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)

        mDocumentNoPattern = ClsMain.FGetSettings(ClsMain.SettingFields.DocumentNoPattern, SettingType.General, "", "", "", "", "", "", "")
        mCompanyPrefix = AgL.XNull(AgL.Dman_Execute("Select CompanyPrefix From Company Where Comp_code = '" & AgL.PubCompCode & "'", AgL.GCn).executeScalar())

        If mCompanyPrefix <> "" Then
            mQry = " Select Comp_Code From Company 
                    Where (" & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " Between Start_Dt And End_Dt)
                    And (" & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " Between Start_Dt And End_Dt) "
            Dim mSelectedCompCode As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
            If mSelectedCompCode <> AgL.PubCompCode Then
                MsgBox("Please select current year dates.", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If

        If FCheckDivisionSiteValidation() = False Then
            ReportFrm.DGL1.DataSource = Nothing
            Exit Sub
        End If

        If ReportFrm.FGetText(rowReportType) = "GSTR1" Then
            FGetGSTR1Report(mFilterGrid, mGridRow)
        ElseIf ReportFrm.FGetText(rowReportType) = "GST 3B" Then
            FGetGST3BReport(mFilterGrid, mGridRow)
        End If
    End Sub
    Private Function FCheckDivisionSiteValidation() As Boolean
        If AgL.XNull(ReportFrm.FGetCode(rowSite)) = "" Then
            MsgBox("Please select Site.", MsgBoxStyle.Information)
            FCheckDivisionSiteValidation = False
            Exit Function
        End If

        If AgL.XNull(Replace(ReportFrm.FGetCode(rowDivision), "'", "")) = "" Then
            MsgBox("Please select Division.", MsgBoxStyle.Information)
            FCheckDivisionSiteValidation = False
            Exit Function
        End If

        If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Then
            If AgL.XNull(ReportFrm.FGetCode(rowSite)).ToString.Replace("'", "") <> AgL.PubSiteCode Then
                MsgBox("Please select correct Site.", MsgBoxStyle.Information)
                FCheckDivisionSiteValidation = False
                Exit Function
            End If
        End If

        If AgL.XNull(ReportFrm.FGetCode(rowSite)).ToString.Contains(",") = True Or
            AgL.XNull(Replace(ReportFrm.FGetCode(rowDivision), "'", "")).ToString.Contains(",") = True Then

            Dim DtSalesTaxNo As New DataTable
            DtSalesTaxNo.Columns.Add("SalesTaxNo")


            Dim mSiteArr() As String = AgL.XNull(ReportFrm.FGetCode(rowSite)).ToString.Split(",")
            Dim mDivisionArr() As String = AgL.XNull(Replace(ReportFrm.FGetCode(rowDivision), "'", "")).ToString.Split(",")

            For I As Integer = 0 To mSiteArr.Length - 1
                For J As Integer = 0 To mDivisionArr.Length - 1
                    mQry = " Select VReg.SalesTaxNo As DivisionSalesTaxNo
                            From Division D
                            LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                            LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                                        From SubgroupRegistration 
                                        Where RegistrationType = 'Sales Tax No') As VReg On D.SubCode = VReg.SubCode
                            Where D.Div_Code = " & AgL.Chk_Text(AgL.XNull(mDivisionArr(J))) & ""
                    Dim mSalesTaxNo As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())

                    If mSalesTaxNo = "" Then
                        mSalesTaxNo = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteSalesTaxNo, SettingType.General, Replace(AgL.XNull(mDivisionArr(J)), "'", ""), Replace(AgL.XNull(mSiteArr(I)), "'", ""), "", "", "", "", "")
                        If mSalesTaxNo = "" Then
                            MsgBox("Company GST No. is blank.", MsgBoxStyle.Information)
                            FCheckDivisionSiteValidation = False
                            Exit Function
                        Else
                            DtSalesTaxNo.Rows.Add()
                            DtSalesTaxNo.Rows(DtSalesTaxNo.Rows.Count - 1)("SalesTaxNo") = mSalesTaxNo
                        End If
                    Else
                        DtSalesTaxNo.Rows.Add()
                        DtSalesTaxNo.Rows(DtSalesTaxNo.Rows.Count - 1)("SalesTaxNo") = mSalesTaxNo
                    End If
                Next
            Next

            Dim DtSalesTaxNo_Distinct As DataTable = DtSalesTaxNo.DefaultView.ToTable(True, "SalesTaxNo")
            If DtSalesTaxNo_Distinct.Rows.Count > 1 Then
                MsgBox("Selected Site & Divisions have multiple GST Nos.", MsgBoxStyle.Information)
                FCheckDivisionSiteValidation = False
                Exit Function
            End If
        End If

        FCheckDivisionSiteValidation = True
    End Function
    Private Function GetSalesTaxNo() As String
        Dim mSalesTaxNo As String = ""

        Dim mSiteArr() As String = AgL.XNull(ReportFrm.FGetCode(rowSite)).ToString.Split(",")
        Dim mDivisionArr() As String = AgL.XNull(Replace(ReportFrm.FGetCode(rowDivision), "'", "")).ToString.Split(",")

        For I As Integer = 0 To mSiteArr.Length - 1
            For J As Integer = 0 To mDivisionArr.Length - 1
                mQry = " Select VReg.SalesTaxNo As DivisionSalesTaxNo
                            From Division D
                            LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                            LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                                        From SubgroupRegistration 
                                        Where RegistrationType = 'Sales Tax No') As VReg On D.SubCode = VReg.SubCode
                            Where D.Div_Code = " & AgL.Chk_Text(AgL.XNull(mDivisionArr(J))) & ""
                mSalesTaxNo = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())

                If mSalesTaxNo = "" Then
                    mSalesTaxNo = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteSalesTaxNo, SettingType.General, Replace(AgL.XNull(mDivisionArr(J)), "'", ""), Replace(AgL.XNull(mSiteArr(I)), "'", ""), "", "", "", "", "")
                End If
            Next
        Next
        GetSalesTaxNo = mSalesTaxNo
    End Function
    Public Sub FGetGSTR1Report(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Dim mCondStr As String = ""
        Try

            RepTitle = "GST Reports"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mGridRow.Cells("Search Code").Value = B2B Or
                            mGridRow.Cells("Search Code").Value = B2CL Or
                            mGridRow.Cells("Search Code").Value = B2CS Or
                            mGridRow.Cells("Search Code").Value = CDNR Or
                            mGridRow.Cells("Search Code").Value = CDNUR Or
                            mGridRow.Cells("Search Code").Value = EXP Or
                            mGridRow.Cells("Search Code").Value = AT Or
                            mGridRow.Cells("Search Code").Value = ATADJ Or
                            mGridRow.Cells("Search Code").Value = EXEMP Or
                            mGridRow.Cells("Search Code").Value = HSN Or
                            mGridRow.Cells("Search Code").Value = DOCS Or
                            mGridRow.Cells("Search Code").Value = B2CSDetail Or
                            mGridRow.Cells("Search Code").Value = EXEMPDetail Or
                            mGridRow.Cells("Search Code").Value = HSNWiseDetail Or
                            mGridRow.Cells("Search Code").Value = DocumentWiseDetail Then
                        mFilterGrid.Item(GFilter, rowNextFormat).Value = mGridRow.Cells("Search Code").Value
                    Else
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If


            mCondStr = " Where 1=1"
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            'mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "
            mCondStr = mCondStr & " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "',
                                        '" & Ncat.DebitNoteSupplier & "','" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "','" & Ncat.CreditNoteSupplier & "')"
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "


            If ReportFrm.FGetText(rowNextFormat) <> "" And ReportFrm.FGetText(rowNextFormat) IsNot Nothing Then
                Select Case ReportFrm.FGetText(rowNextFormat)
                    Case B2B
                        mQry = "Select H.DocId As SearchCode, Max(H.GSTINofRecipient) As GstNoOfRecipient, Max(H.ReceiverName) As ReceiverName, 
                                Max(H.InvoiceNumber) As InvoiceNumber,
                                Max(H.InvoiceDate) As InvoiceDate, Sum(H.LineNet_Amount) As InvoiceValue, 
                                Max(H.PlaceOfSupply) As PlaceOfSupply, Max(H.ReverseCharge) As ReverseCharge,
                                Max(H.ApplicableTaxRate) As ApplicableTaxRate, Max(H.InvoiceType) As InvoiceType,	
                                Max(H.ECommerceGSTIN) As EcommerceGstin, Max(H.Rate) As Rate,	
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.CessAmount) As CessAmount 
                                From (" + FGetB2BQry(mCondStr) + ") As H 
                                Group By H.DocID, H.SalesTaxGroupItem 
                                Order By InvoiceDate "
                    Case B2CL
                        mQry = " SELECT H.DocId As SearchCode, Max(H.GSTINofRecipient) As GSTINofRecipient, Max(H.ReceiverName) As ReceiverName, Max(H.InvoiceNumber) As InvoiceNumber,
                                Max(H.InvoiceDate) As InvoiceDate, Sum(H.InvoiceValue) As InvoiceValue, Max(H.PlaceOfSupply) As PlaceOfSupply, Max(H.ReverseCharge) As ReverseCharge,
                                Max(H.ApplicableTaxRate) As ApplicableTaxRate, Max(H.InvoiceType) As InvoiceType, Max(H.ECommerceGSTIN) As EcommerceGstin,	 
                                Max(H.Rate) As Rate, Sum(H.TaxableValue) As TaxableValue,  Sum(H.CessAmount) As CessAmount
                                From (" + FGetB2CLargeQry(mCondStr) + ") As H 
                                Group By H.DocID, H.SalesTaxGroupItem
                                Order By InvoiceDate "
                    Case B2CS
                        mQry = " SELECT '" & B2CSDetail & "' As SearchCode, H.Type As Type, H.PlaceOfSupply As PlaceOfSupply, Max(H.ApplicablePercentOfTaxRate) As ApplicablePercentOfTaxRate,
                                Max(H.Rate) As Rate, Sum(H.TaxableValue) As TaxableValue, Sum(H.CessAmount) As CessAmount, 
                                H.ECommerceGSTIN As EcommerceGstin
                                From (" + FGetB2CSmallQry(mCondStr) + ") As H 
                                Group By H.Type, H.PlaceOfSupply, H.SalesTaxGroupItem, H.ECommerceGSTIN "
                    Case CDNR
                        mQry = " SELECT H.DocId As SearchCode, 
                                Max(H.Exception) As Exception,
                                Max(H.GSTINofRecipient) As GstNoOfRecipient, Max(H.ReceiverName) As ReceiverName, 
                                Max(H.InvoiceNumber) As InvoiceNumber, 
                                IfNull((Select strftime('%d/%m/%Y', SaleInvoiceDetail.ReferenceDate) From SaleInvoiceDetail 
                                    Where SaleInvoiceDetail.DocId = H.DocId And SaleInvoiceDetail.ReferenceNo = Max(H.InvoiceNumber)
                                    GROUP BY SaleInvoiceDetail.ReferenceDate), Max(H.InvoiceDate)) As InvoiceDate,
                                Max(H.DebitCreditNoteNo) As DebitCreditNoteNo, Max(H.DebitCreditNoteDate) As DebitCreditNoteDate, 
                                Max(H.DocumentType) As DocumentType, 'Regular B2B' As NoteSupplyType,
                                Max(H.PlaceOfSupply) As PlaceOfSupply, Sum(H.LineNet_Amount) As DebitCreditNoteValue, 
                                Max(H.ApplicableTaxRate) As ApplicableTaxRate, Max(H.Rate) As Rate,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.CessAmount) As CessAmount, Max(H.PreGST) As PreGst
                                From (" + FGetCreditDebitNoteRegisteredQry(mCondStr) + ") As H 
                                Group By H.DocID, H.SalesTaxGroupItem
                                Order By DebitCreditNoteDate, DebitCreditNoteNo "
                    Case CDNUR
                        mQry = " SELECT H.DocId As SearchCode, Max(H.URType) As URType,
                                Max(H.DebitCreditNoteNo) As DebitCreditNoteNo, Max(H.DebitCreditNoteDate) As DebitCreditNoteDate, 
                                Max(H.DocumentType) As DocumentType,
                                Max(H.InvoiceNumber) As InvoiceNumber, Max(H.InvoiceDate) As InvoiceDate,
                                Max(H.PlaceOfSupply) As PlaceOfSupply, 
                                Max(H.DebitCreditNoteValue) As DebitCreditNoteValue, 
                                Max(H.ApplicableTaxRate) As ApplicableTaxRate,
                                Max(H.Rate) As Rate,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.CessAmount) As CessAmount, Max(H.PreGST) As PreGst
                                From (" + FGetCreditDebitNoteUnRegisteredQry(mCondStr) + ") As H 
                                Group By H.DocID, H.SalesTaxGroupItem 
                                Order By DebitCreditNoteDate "
                    Case EXP
                        mQry = "Select H.DocId As SearchCode, Max(H.InvoiceNumber) As InvoiceNo,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount 
                                From (" + FGetExportInvoiceQry(mCondStr) + ") As H 
                                Group By H.DocId "
                    Case AT
                        mQry = "Select H.DocId As SearchCode, Max(H.InvoiceNumber) As InvoiceNo,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount 
                                From (" + FGetTaxLiabilityAdvanceRecQry(mCondStr) + ") As H 
                                Group By H.DocId "
                    Case ATADJ
                        mQry = "Select H.DocId As SearchCode, Max(H.InvoiceNumber) As InvoiceNo,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount 
                                From (" + FGetAdjOfAdvanceQry(mCondStr) + ") As H 
                                Group By H.DocId "
                    Case EXEMP
                        mQry = " SELECT '" & EXEMPDetail & "' As SearchCode, H.ItemCategory As ItemCategory, Sum(H.NilRatedSupplies) As NilRatedSupplies,
                                Sum(H.ExemptedSupplies) As ExemptedSupplies, Sum(H.NonGSTSupplies) As NonGSTSupplies
                                From (" + FGetNilRatedInvoiceQry(mCondStr) + ") As H 
                                Group By H.ItemCategory "
                    Case HSN
                        mQry = " SELECT '" & HSNWiseDetail & "' As SearchCode, H.HSN As HSN, Max(H.ItemCategory) As ItemCategory, Max(H.UQC) As UQC,
                                Sum(H.Qty) As TotalQty, Sum(H.InvoiceValue) As InvoiceValue, H.GrossTaxRate As Rate, Sum(H.TaxableValue) As TaxableValue, 
                                Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, Sum(H.CentralTaxAmount) As CentralTaxAmount, 
                                Sum(H.StateTaxAmount) As StateTaxAmount, Sum(H.CessAmount) As CessAmount
                                From (" + FGetHSNQry(mCondStr) + ") As H 
                                Group By H.HSN, H.GrossTaxRate "
                    Case DOCS
                        mQry = " Select '" & DocumentWiseDetail & "' As SearchCode, H.Type, H.VoucherType, 
                                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Max(H.DivisionShortName),'')),'<SITE>',IfNull(Max(H.SiteShortName),'')),'<DOCTYPE>',IfNull(Max(H.VoucherTypeShortName),'')),'<DOCNO>',IfNull(Min(H.InvoiceNumber_Format),'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As SrNoFrom,
                                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Max(H.DivisionShortName),'')),'<SITE>',IfNull(Max(H.SiteShortName),'')),'<DOCTYPE>',IfNull(Max(H.VoucherTypeShortName),'')),'<DOCNO>',IfNull(Max(H.InvoiceNumber_Format),'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As SrNoTo,
                                Count(Distinct DocId) As TotalNumber, Sum(H.Cancelled) As Cancelled
                                From (" + FGetDOCSQry(mCondStr) + ") As H 
                                Group By H.Type, H.VoucherType, H.DivisionName, H.SiteName "
                    Case B2CSDetail
                        mQry = "Select H.DocId As SearchCode, Max(H.InvoiceNumber) As InvoiceNo, Max(H.ReceiverName) As Party,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount 
                                From (" + FGetB2CSmallQry(mCondStr) + ") As H 
                                Where H.Rate = " & AgL.VNull(mGridRow.Cells("Rate").Value) & "
                                Group By H.DocId "
                    Case EXEMPDetail
                        mQry = "Select H.DocId As SearchCode, Max(H.InvoiceNumber) As InvoiceNo,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount 
                                From (" + FGetNilRatedInvoiceQry(mCondStr) + ") As H 
                                Group By H.DocId "
                    Case HSNWiseDetail
                        mQry = "Select H.DocId As SearchCode, Max(H.InvoiceNumber) As InvoiceNo,Max(H.ReceiverName) As Party,
                                H.GrossTaxRate As Rate, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount 
                                From (" + FGetHSNQry(mCondStr) + ") As H 
                                Where IfNull(H.HSN,'') = '" & mGridRow.Cells("HSN").Value & "'
                                And IfNull(H.GrossTaxRate,0) = '" & mGridRow.Cells("Rate").Value & "'
                                Group By H.DocId, H.HSN, H.GrossTaxRate "
                    Case DocumentWiseDetail
                        mQry = "Select H.DocId As SearchCode, Max(H.InvoiceNumber) As InvoiceNo,Max(H.ReceiverName) As Party,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount 
                                From (" + FGetDOCSQry(mCondStr) + ") As H 
                                Where H.Type = '" & mGridRow.Cells("Type").Value & "'
                                And H.VoucherType = '" & mGridRow.Cells("Voucher Type").Value & "'
                                Group By H.DocId "
                End Select
                ReportFrm.Text = "GST Report" + " (" + ReportFrm.FGetText(rowReportType) + "-" + ReportFrm.FGetText(rowNextFormat).ToString.Replace("/", "-") + ")"

                ReportFrm.DGL2.Visible = True
                ReportFrm.IsAllowFind = True
                ReportFrm.MnuVisible.Visible = True
                ReportFrm.MnuSort.Visible = True
                ReportFrm.MnuFilter.Visible = True
            Else
                mQry = "Select '" & B2B & "' As SearchCode, 
                '" & B2B & "'  As Particulars, Count(Distinct DocId) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.LineNet_Amount) As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetB2BQry(mCondStr) + ") As H "

                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & B2CL & "'  As SearchCode, 
                '" & B2CL & "'   As Particulars, Count(Distinct DocId) VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetB2CLargeQry(mCondStr) + ") As H "

                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & B2CS & "'  As SearchCode, 
                '" & B2CS & "'   As Particulars, Count(Distinct DocId) VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetB2CSmallQry(mCondStr) + ") As H "

                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & CDNR & "' As SearchCode, 
                '" & CDNR & "' As Particulars, Count(Distinct DocId) VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.LineNet_Amount) As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetCreditDebitNoteRegisteredQry(mCondStr) + ") As H "

                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & CDNUR & "' As SearchCode, 
                '" & CDNUR & "' As Particulars, Count(Distinct DocId) VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Max(H.DebitCreditNoteValue) As InvoiceAmount, Max(Exception) As Exception
                From (" + FGetCreditDebitNoteUnRegisteredQry(mCondStr) + ") As H "

                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & EXP & "' As SearchCode, 
                '" & EXP & "' As Particulars, Count(Distinct DocId) VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetExportInvoiceQry(mCondStr) + ") As H "

                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & AT & "' As SearchCode, 
                '" & AT & "' As Particulars, Count(Distinct DocId) VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Max(H.InvoiceValue) As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetTaxLiabilityAdvanceRecQry(mCondStr) + ") As H "

                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & ATADJ & "' As SearchCode, 
                '" & ATADJ & "' As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetAdjOfAdvanceQry(mCondStr) + ") As H "

                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & EXEMP & "' As SearchCode, 
                '" & EXEMP & "' As Particulars, Count(Distinct DocId) VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetNilRatedInvoiceQry(mCondStr) + ") As H "

                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & HSN & "' As SearchCode, 
                '" & HSN & "' As Particulars, Count(Distinct HSN) VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceValue) As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetHSNQry(mCondStr) + ") As H "



                mQry = mQry + "UNION ALL "

                mQry = mQry + "Select '" & DOCS & "' As SearchCode, 
                '" & DOCS & "' As Particulars, Count(Distinct Type) VoucherCount, Null As TaxableValue, Null As IntegratedTaxAmount, 
                Null As CentralTaxAmount, Null As StateTaxAmount, 
                Null As CessAmount, Null As TaxAmount, Null As InvoiceAmount , Max(Exception) As Exception
                From (" + FGetDOCSQry(mCondStr) + ") As H "

                ReportFrm.Text = "GST Report" + " (" + ReportFrm.FGetText(rowReportType) + ")"

                ReportFrm.DGL2.Visible = False
                ReportFrm.IsAllowFind = False
                ReportFrm.MnuVisible.Visible = False
                ReportFrm.MnuSort.Visible = False
                ReportFrm.MnuFilter.Visible = False
            End If


            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            mQry = "Select 'Create Excel File' As MenuText, 'FCreateGSTR1ExcelFile' As FunctionName
                    UNION ALL 
                    Select 'Create JSON File' As MenuText, 'FCreateGSTR1JSONFile' As FunctionName "
            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcGSTReports"
            ReportFrm.MnuCustomOption.Items.Clear()
            ReportFrm.DTCustomMenus = DtMenuList
            ReportFrm.IsHideZeroColumns = False



            ReportFrm.ProcFillGrid(DsHeader)

            If ReportFrm.DGL1.Columns.Contains("Exception") Then
                If ReportFrm.Text = "GST Report" + " (" + ReportFrm.FGetText(rowReportType) + ")" Then
                    ReportFrm.DGL1.Columns("Exception").Visible = False
                    ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns("Exception").Index).Visible = False
                    ReportFrm.DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
                    For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                        If AgL.XNull(ReportFrm.DGL1.Item("Exception", I).Value) <> "" Then
                            ReportFrm.DGL1.Rows(I).DefaultCellStyle.ForeColor = Color.Maroon
                            ReportFrm.DGL1.Rows(I).DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                        End If
                    Next
                Else
                    Dim BlankValueColumn As DataRow() = DsHeader.Tables(0).Select("Exception <> '' ")
                    If BlankValueColumn.Length = 0 Then
                        ReportFrm.DGL1.Columns("Exception").Visible = False
                        ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns("Exception").Index).Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Private Function FGetB2BQry(mCondStr) As String
        Dim mStrQry As String = " SELECT L.DocId, H.SaleToPartySalesTaxNo As GSTINofRecipient, 
                    Replace(Replace(Sg.Name,'{',''),'}','') As ReceiverName,
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                    strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, L.Net_Amount As LineNet_Amount, 
                    H.Net_Amount As HeaderNet_Amount, 
                    S.ManualCode || '-' || S.Description As PlaceOfSupply, 'N' As ReverseCharge,
                    '' As ApplicableTaxRate, 'Regular B2B' As InvoiceType,	'' As ECommerceGSTIN,	 
                    L.SalesTaxGroupItem,
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,	
                    L.Taxable_Amount As TaxableValue, 
                    IfNull(L.Tax1,0) As IntegratedTaxAmount,  IfNull(L.Tax2,0) As CentralTaxAmount, 
                    IfNull(L.Tax3,0) As StateTaxAmount, IfNull(L.Tax4,0) As CessAmount,
                    IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount,
                    '' As Exception
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                    LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And Vt.NCat = '" & Ncat.SaleInvoice & "' And IfNull(S.ManualCode,'') <> '00'
                    And H.SalesTaxGroupParty In ('" & PostingGroupSalesTaxParty.Registered & "')"
        Return mStrQry
    End Function
    Private Function FGetB2CLargeQry(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, H.SaleToPartySalesTaxNo As GSTINofRecipient, Sg.Name As ReceiverName, 
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                    H.V_Date As InvoiceDate, L.Net_Amount As InvoiceValue, S.Code + '-' + S.Description As PlaceOfSupply, 'N' As ReverseCharge,
                    0 As ApplicableTaxRate, 'Regular' As InvoiceType, '' As ECommerceGSTIN,	 
                    L.SalesTaxGroupItem,
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,	
                    L.Taxable_Amount As TaxableValue, 
                    IfNull(L.Tax1,0) As IntegratedTaxAmount,  IfNull(L.Tax2,0) As CentralTaxAmount, 
                    IfNull(L.Tax3,0) As StateTaxAmount, IfNull(L.Tax4,0) As CessAmount,
                    IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount,
                    '' As Exception
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                    LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And Vt.NCat = '" & Ncat.SaleInvoice & "'
                     And IfNull(S.ManualCode,'') <> '00'
                    And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "'  
                    And H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "'
                    And H.Net_Amount > 250000 "

        Return mStrQry
    End Function
    Private Function FGetB2CSmallQry(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, 'OE' As Type, S.ManualCode || '-' || S.Description As PlaceOfSupply,  '' As ApplicablePercentOfTaxRate, 
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                    Sg.Name As ReceiverName, 
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,
                    L.Taxable_Amount As TaxableValue, '' As ECommerceGSTIN,
                    L.Net_Amount As InvoiceValue, L.SalesTaxGroupItem,
                    IfNull(L.Tax1,0) As IntegratedTaxAmount,  IfNull(L.Tax2,0) As CentralTaxAmount, 
                    IfNull(L.Tax3,0) As StateTaxAmount, IfNull(L.Tax4,0) As CessAmount,
                    IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount,
                    '' As Exception
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                    LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "')
                     And IfNull(S.ManualCode,'') <> '00'
                    And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "'
                    And ((H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "' And H.Net_Amount <= 250000)
                    Or H.PlaceOfSupply = '" & PlaceOfSupplay.WithinState & "') "

        mStrQry = mStrQry + " UNION ALL "

        mStrQry = mStrQry + " SELECT L.DocId, 'OE' As Type, S.ManualCode || '-' || S.Description As PlaceOfSupply,  '' As ApplicablePercentOfTaxRate, 
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                Sg.Name As ReceiverName, 
                IfNull(Lc.Tax1_Per,0) + IfNull(Lc.Tax2_Per,0) + IfNull(Lc.Tax3_Per,0) As Rate,
                Lc.Taxable_Amount As TaxableValue, '' As ECommerceGSTIN,
                Lc.Net_Amount As InvoiceValue, L.SalesTaxGroupItem,
                IfNull(Lc.Tax1,0) As IntegratedTaxAmount,  IfNull(Lc.Tax2,0) As CentralTaxAmount, 
                IfNull(Lc.Tax3,0) As StateTaxAmount, IfNull(Lc.Tax4,0) As CessAmount,
                IfNull(Lc.Tax1,0) + IfNull(Lc.Tax2,0) + IfNull(Lc.Tax3,0) + IfNull(Lc.Tax4,0) As TaxAmount,
                '' As Exception
                From LedgerHead H 
                LEFT JOIN LedgerHeadCharges Hc On H.DocId = Hc.DocId
                Left join LedgerHeadDetail L on H.DocID = L.DocID
                LEft join LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID and L.Sr = Lc.Sr
                left join Voucher_Type Vt On H.V_Type = Vt.V_Type
                left join SubGroup Sg On H.Subcode = Sg.SubCode
                Left join SaleInvoice Si On L.SpecificationDocID = Si.DocID
                LEFT JOIN LedgerHead Lh ON L.SpecificationDocID = Lh.DocID
                left join Voucher_Type SVt On Si.V_Type = SVt.V_Type
                LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                LEFT JOIN City C On IfNull(Si.SaleToPartyCity,H.PartyCity) = C.CityCode
                LEFT JOIN State S on C.State = S.Code " & mCondStr &
                " And Vt.V_Type In ('CNC','DNC')
                And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "'
                And ((H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "' And Hc.Net_Amount <= 250000)
                Or H.PlaceOfSupply = '" & PlaceOfSupplay.WithinState & "') "

        Return mStrQry
    End Function
    Private Function FGetCreditDebitNoteRegisteredQry(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, H.SaleToPartySalesTaxNo As GSTINofRecipient, Sg.Name As ReceiverName, 
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(SDm.ShortName,'')),'<SITE>',IfNull(SSm.ShortName,'')),'<DOCTYPE>',IfNull(SVt.Short_Name,'')),'<DOCNO>',IfNull(Si.ManualRefNo,L.ReferenceNo)),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber, 
                strftime('%d/%m/%Y', IfNull(Si.V_Date,L.ReferenceDate)) As InvoiceDate,
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As DebitCreditNoteNo, 
                strftime('%d/%m/%Y', H.V_Date) As DebitCreditNoteDate, 'C' As DocumentType,
                S.ManualCode || '-' || S.Description As PlaceOfSupply, (L.Net_Amount)*-1.00 As LineNet_Amount, 
                (H.Net_Amount)*-1.00 As HeaderNet_Amount, 
                '' As ApplicableTaxRate, L.SalesTaxGroupItem,
                IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,
                (L.Taxable_Amount)*-1.00 As TaxableValue, 'N' As PreGST,
                IfNull((L.Tax1),0)*-1.00 As IntegratedTaxAmount,  IfNull((L.Tax2),0)*-1.00 As CentralTaxAmount, 
                IfNull((L.Tax3),0)*-1.00 As StateTaxAmount, IfNull((L.Tax4),0)*-1.00 As CessAmount,
                IfNull((L.Tax1),0)*-1.00 + IfNull((L.Tax2),0)*-1.00 + IfNull((L.Tax3),0)*-1.00 + IfNull((L.Tax4),0)*-1.00 As TaxAmount,
                Case When H.V_Date < Si.V_Date Then 'Debit/Credit Note date should be greater then invoice date.' Else '' End As Exception
                From SaleInvoice H 
                Left join SaleInvoiceDetail L on H.DocID = L.DocID
                left join Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                Left join SaleInvoice Si On L.ReferenceDocId = Si.DocId
                left join Voucher_Type SVt On Si.V_Type = SVt.V_Type
                LEFT JOIN SiteMast SSm On H.Site_Code = SSm.Code
                LEFT JOIN Division SDm On H.Div_Code = SDm.Div_Code
                LEFT JOIN City C On IfNull(Si.SaleToPartyCity,H.SaleToPartyCity) = C.CityCode
                LEFT JOIN State S on C.State = S.Code " & mCondStr &
                " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Registered & "'
                And Vt.NCat = '" & Ncat.SaleReturn & "'"

        mStrQry = mStrQry + " UNION ALL "

        mStrQry = mStrQry + " SELECT L.DocId, H.PartySalesTaxNo As GSTINofRecipient, Sg.Name As ReceiverName, 
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(SDm.ShortName,'')),'<SITE>',IfNull(SSm.ShortName,'')),'<DOCTYPE>',IfNull(SVt.Short_Name,'')),'<DOCNO>',IfNull(Si.ManualRefNo,Lh.ManualRefNo)),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber, 
                strftime('%d/%m/%Y', IfNull(Si.V_Date,Lh.V_Date)) As InvoiceDate,
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As DebitCreditNoteNo, 
                strftime('%d/%m/%Y', H.V_Date) As DebitCreditNoteDate, substr(Vt.Description,1,1) As DocumentType,
                S.ManualCode || '-' || S.Description As PlaceOfSupply, (Lc.Net_Amount)*-1.00 As LineNet_Amount, 
                (Hc.Net_Amount)*-1.00 As HeaderNet_Amount, 
                '' As ApplicableTaxRate, L.SalesTaxGroupItem,
                IfNull(Lc.Tax1_Per,0) + IfNull(Lc.Tax2_Per,0) + IfNull(Lc.Tax3_Per,0) As Rate,
                (Lc.Taxable_Amount)*-1 As TaxableValue, 'N' As PreGST,
                IfNull((Lc.Tax1),0)*-1.00 As IntegratedTaxAmount,  IfNull((Lc.Tax2),0)*-1.00 As CentralTaxAmount, 
                IfNull((Lc.Tax3),0)*-1.00 As StateTaxAmount, IfNull((Lc.Tax4),0)*-1.00 As CessAmount,
                IfNull((Lc.Tax1),0)*-1.00 + IfNull((Lc.Tax2),0)*-1.00 + IfNull((Lc.Tax3),0)*-1.00 + IfNull((Lc.Tax4),0)*-1.00 As TaxAmount,
                Case When H.V_Date < Si.V_Date Then 'Debit/Credit Note date should be greater then invoice date.' Else '' End As Exception
                From LedgerHead H 
                LEFT JOIN LedgerHeadCharges Hc On H.DocId = Hc.DocId
                Left join LedgerHeadDetail L on H.DocID = L.DocID
                LEft join LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID and L.Sr = Lc.Sr
                left join Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                left join SubGroup Sg On H.Subcode = Sg.SubCode
                Left join SaleInvoice Si On L.SpecificationDocID = Si.DocID
                LEFT JOIN LedgerHead Lh ON L.SpecificationDocID = Lh.DocID
                left join Voucher_Type SVt On Si.V_Type = SVt.V_Type
                LEFT JOIN SiteMast SSm On H.Site_Code = SSm.Code
                LEFT JOIN Division SDm On H.Div_Code = SDm.Div_Code
                LEFT JOIN City C On IfNull(Si.SaleToPartyCity,H.PartyCity) = C.CityCode
                LEFT JOIN State S on C.State = S.Code " & mCondStr &
                " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Registered & "'" &
                " And H.V_Type In ('CNC','DNC') "
        Return mStrQry
    End Function
    Private Function FGetCreditDebitNoteUnRegisteredQry(mCondStr As String) As String
        Dim mStrQry As String = " Select L.DocId, 'B2CL' As URType, Sg.Name As ReceiverName, 
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(SDm.ShortName,'')),'<SITE>',IfNull(SSm.ShortName,'')),'<DOCTYPE>',IfNull(SVt.Short_Name,'')),'<DOCNO>',IfNull(Si.ManualRefNo,L.ReferenceNo)),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber, 
                Si.V_Date As InvoiceDate,
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As DebitCreditNoteNo, 
                H.V_Date As DebitCreditNoteDate, substr(Vt.Description,1,1) As DocumentType,
                S.ManualCode || '-' || S.Description As PlaceOfSupply, L.Net_Amount As DebitCreditNoteValue, 
                '' As ApplicableTaxRate, L.SalesTaxGroupItem,
                IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,
                Abs(L.Taxable_Amount) As TaxableValue, 'N' As PreGST,
                IfNull(Abs(L.Tax1),0) As IntegratedTaxAmount,  IfNull(Abs(L.Tax2),0) As CentralTaxAmount, 
                IfNull(Abs(L.Tax3),0) As StateTaxAmount, IfNull(Abs(L.Tax4),0) As CessAmount,
                IfNull(Abs(L.Tax1),0) + IfNull(Abs(L.Tax2),0) + IfNull(Abs(L.Tax3),0) + IfNull(Abs(L.Tax4),0) As TaxAmount,
                '' As Exception
                From SaleInvoice H 
                Left join SaleInvoiceDetail L on H.DocID = L.DocID
                left join Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                Left join SaleInvoice Si On L.ReferenceDocId = Si.DocId
                left join Voucher_Type SVt On Si.V_Type = SVt.V_Type
                LEFT JOIN SiteMast SSm On H.Site_Code = SSm.Code
                LEFT JOIN Division SDm On H.Div_Code = SDm.Div_Code
                LEFT JOIN City C On Si.SaleToPartyCity = C.CityCode
                LEFT JOIN State S on C.State = S.Code " & mCondStr &
                " And Vt.NCat = '" & Ncat.SaleReturn & "'
                And Si.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "'
                And Si.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "'
                And Si.Net_Amount > 250000
                And Si.DocId Is Not Null "

        mStrQry = mStrQry + " UNION ALL "

        mStrQry = mStrQry + " SELECT L.DocId, 'B2CL' As URType, Sg.Name As ReceiverName, 
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(SDm.ShortName,'')),'<SITE>',IfNull(SSm.ShortName,'')),'<DOCTYPE>',IfNull(SVt.Short_Name,'')),'<DOCNO>',IfNull(Si.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber, 
                Si.V_Date As InvoiceDate,
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As DebitCreditNoteNo, 
                H.V_Date As DebitCreditNoteDate, substr(Vt.Description,1,1) As DocumentType,
                S.ManualCode || '-' || S.Description As PlaceOfSupply, Lc.Net_Amount As DebitCreditNoteValue, 
                '' As ApplicableTaxRate, L.SalesTaxGroupItem,
                IfNull(Lc.Tax1_Per,0) + IfNull(Lc.Tax2_Per,0) + IfNull(Lc.Tax3_Per,0) As Rate,
                Abs(Lc.Taxable_Amount) As TaxableValue, 'N' As PreGST,
                IfNull(Abs(Lc.Tax1),0) As IntegratedTaxAmount,  IfNull(Abs(Lc.Tax2),0) As CentralTaxAmount, 
                IfNull(Abs(Lc.Tax3),0) As StateTaxAmount, IfNull(Abs(Lc.Tax4),0) As CessAmount,
                IfNull(Abs(Lc.Tax1),0) + IfNull(Abs(Lc.Tax2),0) + IfNull(Abs(Lc.Tax3),0) + IfNull(Abs(Lc.Tax4),0) As TaxAmount,
                '' As Exception
                From LedgerHead H 
                LEFT JOIN LedgerHeadCharges Hc On H.DocId = Hc.DocId
                Left join LedgerHeadDetail L on H.DocID = L.DocID
                LEft join LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID and L.Sr = Lc.Sr
                left join Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                left join SubGroup Sg On H.Subcode = Sg.SubCode
                Left join SaleInvoice Si On L.SpecificationDocID = Si.DocID
                left join Voucher_Type SVt On Si.V_Type = SVt.V_Type
                LEFT JOIN SiteMast SSm On H.Site_Code = SSm.Code
                LEFT JOIN Division SDm On H.Div_Code = SDm.Div_Code
                LEFT JOIN City C On Si.SaleToPartyCity = C.CityCode
                LEFT JOIN State S on C.State = S.Code " & mCondStr &
                " And Si.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "'
                And Si.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "'
                And Si.Net_Amount > 250000
                And Si.DocId Is Not Null "

        Return mStrQry
    End Function
    Private Function FGetExportInvoiceQry(mCondStr As String) As String
        Dim mStrQry = ""
        mStrQry = " Select '1' As DocId, '' As V_Date, '' As InvoiceNumber, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceValue, '' As Exception WHERE 1=2 "

        'mStrQry = " SELECT L.DocId, H.SaleToPartySalesTaxNo As GSTINofRecipient, 
        '            Replace(Replace(Sg.Name,'{',''),'}','') As ReceiverName,
        '            Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
        '            strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, L.Net_Amount As LineNet_Amount, 
        '            H.Net_Amount As HeaderNet_Amount, 
        '            S.ManualCode || '-' || S.Description As PlaceOfSupply, 'N' As ReverseCharge,
        '            '' As ApplicableTaxRate, 'Regular' As InvoiceType,	'' As ECommerceGSTIN,	 
        '            L.SalesTaxGroupItem,
        '            IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,	
        '            L.Taxable_Amount As TaxableValue, 
        '            IfNull(L.Tax1,0) As IntegratedTaxAmount,  IfNull(L.Tax2,0) As CentralTaxAmount, 
        '            IfNull(L.Tax3,0) As StateTaxAmount, IfNull(L.Tax4,0) As CessAmount,
        '            IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount,
        '            '' As Exception
        '            From SaleInvoice H 
        '            left join SaleInvoiceDetail L On H.DocID = L.DocID
        '            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
        '            LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
        '            LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
        '            left join SubGroup Sg On H.SaleToParty = Sg.SubCode
        '            LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
        '            LEFT JOIN State S on C.State = S.Code " & mCondStr &
        '            " And Vt.NCat = '" & Ncat.SaleInvoice & "'
        '            And IfNull(S.ManualCode,'') = '00'
        '            "

        Return mStrQry
    End Function
    Private Function FGetTaxLiabilityAdvanceRecQry(mCondStr As String) As String
        Dim mStrQry As String = " Select '1' As DocId, '' As V_Date, '' As InvoiceNumber, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceValue, '' As Exception WHERE 1=2 "
        Return mStrQry
    End Function
    Private Function FGetAdjOfAdvanceQry(mCondStr As String) As String
        Dim mStrQry As String = " Select '1' As DocId, '' As V_Date, '' As InvoiceNumber, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceValue, '' As Exception WHERE 1=2 "
        Return mStrQry
    End Function
    Private Function FGetNilRatedInvoiceQry(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, H.SaleToPartySalesTaxNo As GSTINofRecipient, Sg.Name As ReceiverName, 
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                    strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, L.Net_Amount As InvoiceValue, S.ManualCode || '-' || S.Description As PlaceOfSupply, 'N' As ReverseCharge,
                    '' As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,	
                    L.Taxable_Amount As TaxableValue, 
                    IfNull(L.Tax1,0) As IntegratedTaxAmount,  IfNull(L.Tax2,0) As CentralTaxAmount, 
                    IfNull(L.Tax3,0) As StateTaxAmount, IfNull(L.Tax4,0) As CessAmount,
                    IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount,
                    Case When L.SalesTaxGroupItem = 'GST 0%' Then L.Amount Else 0 End As NilRatedSupplies,
                    Case When L.SalesTaxGroupItem = 'GST Excempt' Then L.Amount Else 0 End As ExemptedSupplies,
                    0 As NonGSTSupplies, Ic.Description As ItemCategory, '' As Exception
                    From SaleInvoice H 
                    Left join SaleInvoiceDetail L on H.DocId = L.DocID
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                    LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    Left join Item I on L.Item = I.Code
                    Left Join ItemCategory Ic On I.ItemCategory = Ic.Code 
                    Left JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And Vt.NCat = '" & Ncat.SaleInvoice & "'
                    And L.SalesTaxGroupItem In ('GST 0%','GST Excempt') "
        Return mStrQry
    End Function
    Private Function FGetHSNQry(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, H.SaleToPartySalesTaxNo As GSTINofRecipient, Sg.Name As ReceiverName, 
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                    strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, L.Net_Amount As InvoiceValue, S.ManualCode || '-' || S.Description As PlaceOfSupply, 'N' As ReverseCharge,
                    '' As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,	
                    CASE WHEN IsNull(Ic.ItemType,I.ItemType) = '" & ItemTypeCode.ServiceProduct & "' THEN NULL ELSE L.Qty END AS Qty,
                    Pst.GrossTaxRate,L.Taxable_Amount As TaxableValue, 
                    IfNull(L.Tax1,0) As IntegratedTaxAmount,  IfNull(L.Tax2,0) As CentralTaxAmount, 
                    IfNull(L.Tax3,0) As StateTaxAmount, IfNull(L.Tax4,0) As CessAmount,
                    IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount,
                    Substr(Replace(Replace(Replace(IfNull (Ic.Description,I.Description),'|',''),'[',''),']',''),1,50) As ItemCategory, 
                    IfNull(IfNull(I.HSN,Ic.HSN),Bi.HSN) As HSN, U.UQC, '' As Exception
                    From SaleInvoice H 
                    Left join SaleInvoiceDetail L on H.DocId = L.DocID 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                    LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    Left join Item I on L.Item = I.Code
                    Left Join ItemCategory Ic On I.ItemCategory = Ic.Code 
                    LEFT JOIN Item Bi On I.BaseItem = Bi.Code
                    LEFT JOIN Unit U On I.Unit = U.Code
                    LEFT JOIN PostingGroupSalesTaxItem Pst ON L.SalesTaxGroupItem = Pst.Description
                    Left JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "') "

        mStrQry += " UNION ALL "

        mStrQry += " SELECT L.DocId, H.PartySalesTaxNo As GSTINofRecipient, Sg.Name As ReceiverName, 
                Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, Lc.Net_Amount As InvoiceValue, S.ManualCode + '-' + S.Description As PlaceOfSupply, 'N' As ReverseCharge,
                '' As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                Isnull(Lc.Tax1_Per,0) + Isnull(Lc.Tax2_Per,0) + Isnull(Lc.Tax3_Per,0) As Rate,	L.Qty,
                Pst.GrossTaxRate, Lc.Taxable_Amount As TaxableValue, 
                Isnull(Lc.Tax1,0) As IntegratedTaxAmount,  Isnull(Lc.Tax2,0) As CentralTaxAmount, 
                Isnull(Lc.Tax3,0) As StateTaxAmount, Isnull(Lc.Tax4,0) As CessAmount,
                Isnull(Lc.Tax1,0) + Isnull(Lc.Tax2,0) + Isnull(Lc.Tax3,0) + Isnull(Lc.Tax4,0) As TaxAmount,
                '' As ItemCategory, Isnull(VSale.HSN,L.HSN) AS HSN, '' AS UQC, '' As Exception
                From LedgerHead H 
                LEFT JOIN LedgerHeadCharges Hc ON H.DocId = Hc.DocId
                Left join LedgerHeadDetail L on H.DocId = L.DocID 
                LEFT JOIN LedgerHeadDetailCharges Lc ON L.DocId = Lc.DocID AND L.Sr = Lc.Sr
                LEFT JOIN (
                    Select H.DocId, Max(IsNull(IfNull(I.HSN,Ic.HSN),Bi.HSN)) As HSN
                    From SaleInvoice H
                    LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocId
                    Left join Item I on L.Item = I.Code
                    Left Join ItemCategory Ic On I.ItemCategory = Ic.Code 
                    LEFT JOIN Item Bi On I.BaseItem = Bi.Code
                    Group BY H.DocId) As VSale On L.SpecificationDocID = VSale.DocId
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                left join SubGroup Sg On H.SubCode = Sg.SubCode
                LEFT JOIN PostingGroupSalesTaxItem Pst ON L.SalesTaxGroupItem = Pst.Description
                Left JOIN City C On H.PartyCity = C.CityCode
                LEFT JOIN State S on C.State = S.Code  " & mCondStr &
                " And H.V_Type In ('DNC','CNC') "
        Return mStrQry
    End Function
    Private Function FGetDOCSQry(mCondStr As String) As String
        Dim mStrQry As String = " SELECT 
                    Case When Vt.NCat = '" & Ncat.SaleInvoice & "' Then 'Invoices for outward supply' 
                         When Vt.NCat = '" & Ncat.SaleReturn & "' Then 'Credit Note' End As Type, 
                    Vt.Description As VoucherType, VT.Short_Name As VoucherTypeShortName, 
                    Sm.Name As SiteName, Sm.ShortName As SiteShortName, 
                    Dm.Div_Name As DivisionName, Dm.ShortName As DivisionShortName, L.DocId, Sg.Name As ReceiverName, 
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                    strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, L.Net_Amount As InvoiceValue, S.ManualCode || '-' || S.Description As PlaceOfSupply, 
                    '' As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,	L.Qty,
                    L.Taxable_Amount As TaxableValue, 
                    IfNull(L.Tax1,0) As IntegratedTaxAmount,  IfNull(L.Tax2,0) As CentralTaxAmount, 
                    IfNull(L.Tax3,0) As StateTaxAmount, IfNull(L.Tax4,0) As CessAmount,
                    IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount,
                    Ic.Description As ItemCategory, IfNull(I.HSN,Ic.HSN) As HSN, U.UQC, 0 As Cancelled, " &
                    IIf(AgL.PubServerName = "", "Cast(H.ManualRefNo As BIGINT)", "Convert(INT,SUBSTRING(H.ManualRefNo, PATINDEX('%[0-9]%', H.ManualRefNo), LEN(H.ManualRefNo)))") & " As InvoiceNumber_Format,
                    '' As Exception
                    From SaleInvoice H 
                    Left join SaleInvoiceDetail L on H.DocId = L.DocID 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                    LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    Left join Item I on L.Item = I.Code
                    Left Join ItemCategory Ic On I.ItemCategory = Ic.Code 
                    LEFT JOIN Unit U On I.Unit = U.Code
                    Left JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "') "

        mStrQry = mStrQry + " UNION ALL "

        mStrQry = mStrQry + " SELECT 'Invoices for inward supply from unregistered person' As Type, Vt.Description As VoucherType, VT.Short_Name As VoucherTypeShortName, Sm.Name As SiteName, Sm.ShortName As SiteShortName, Dm.Div_Name As DivisionName, Dm.ShortName As DivisionShortName, L.DocId, Sg.Name As ReceiverName, 
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                    strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, L.Net_Amount As InvoiceValue, S.ManualCode || '-' || S.Description As PlaceOfSupply, 
                    '' As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,	L.Qty,
                    L.Taxable_Amount As TaxableValue, 
                    IfNull(L.Tax1,0) As IntegratedTaxAmount,  IfNull(L.Tax2,0) As CentralTaxAmount, 
                    IfNull(L.Tax3,0) As StateTaxAmount, IfNull(L.Tax4,0) As CessAmount,
                    IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount,
                    Ic.Description As ItemCategory, IfNull(I.HSN,Ic.HSN) As HSN, U.UQC, 0 As Cancelled, " &
                    IIf(AgL.PubServerName = "", "H.ManualRefNo", "Convert(INT,SUBSTRING(H.ManualRefNo, PATINDEX('%[0-9]%', H.ManualRefNo), LEN(H.ManualRefNo)))") & " As InvoiceNumber_Format,
                    '' As Exception
                    From PurchInvoice H 
                    Left join PurchInvoiceDetail L on H.DocId = L.DocID 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                    LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                    left join SubGroup Sg On H.Vendor = Sg.SubCode
                    Left join Item I on L.Item = I.Code
                    Left Join ItemCategory Ic On I.ItemCategory = Ic.Code 
                    LEFT JOIN Unit U On I.Unit = U.Code
                    Left JOIN City C On H.VendorCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And Vt.NCat = '" & Ncat.PurchaseInvoice & "' 
                    And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "' "

        mStrQry = mStrQry + " UNION ALL "

        mStrQry = mStrQry + " SELECT 
                    'Credit Note' As Type, 
                    Vt.Description As VoucherType, VT.Short_Name As VoucherTypeShortName, Sm.Name As SiteName, Sm.ShortName As SiteShortName, Dm.Div_Name As DivisionName, Dm.ShortName As DivisionShortName, L.DocId, Sg.Name As ReceiverName, 
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Dm.ShortName,'')),'<SITE>',IfNull(Sm.ShortName,'')),'<DOCTYPE>',IfNull(Vt.Short_Name,'')),'<DOCNO>',IfNull(H.ManualRefNo,'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As InvoiceNumber,
                    strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, Lc.Net_Amount As InvoiceValue, S.ManualCode || '-' || S.Description As PlaceOfSupply, 
                    '' As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    IfNull(Lc.Tax1_Per,0) + IfNull(Lc.Tax2_Per,0) + IfNull(Lc.Tax3_Per,0) As Rate,	L.Qty,
                    Lc.Taxable_Amount As TaxableValue, 
                    IfNull(Lc.Tax1,0) As IntegratedTaxAmount,  IfNull(Lc.Tax2,0) As CentralTaxAmount, 
                    IfNull(Lc.Tax3,0) As StateTaxAmount, IfNull(Lc.Tax4,0) As CessAmount,
                    IfNull(Lc.Tax1,0) + IfNull(Lc.Tax2,0) + IfNull(Lc.Tax3,0) + IfNull(Lc.Tax4,0) As TaxAmount,
                    '' As ItemCategory, '' As HSN, '' As UQC, 0 As Cancelled, " &
                    IIf(AgL.PubServerName = "", "Cast(H.ManualRefNo As BIGINT)", "Convert(INT,SUBSTRING(H.ManualRefNo, PATINDEX('%[0-9]%', H.ManualRefNo), LEN(H.ManualRefNo)))") & " As InvoiceNumber_Format,
                    '' As Exception
                    From LedgerHead H 
                    Left join LedgerHeadDetail L on H.DocId = L.DocID 
                    LEFT JOIN LedgerHeadDetailCharges Lc On L.DocId = Lc.DocId And L.Sr = Lc.Sr
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                    LEFT JOIN Division Dm On H.Div_Code = Dm.Div_Code
                    left join SubGroup Sg On H.SubCode = Sg.SubCode
                    Left JOIN City C On H.PartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And Vt.NCat In ('" & Ncat.CreditNoteCustomer & "') "
        Return mStrQry
    End Function
    Public Sub FCreateGSTR1ExcelFile(DGL As AgControls.AgDataGrid)
        Dim SubTitle$ = ""
        Dim GroupHeaderTitle1$ = "", GroupHeaderTitle2$ = ""
        Dim IsReturn As Integer = 0
        Dim AssessmentYear$ = ""
        Dim OutputFile As String = ""
        Dim mCondStr As String = ""

        Dim ToDate As DateTime = ReportFrm.FGetText(rowToDate)
        Dim newdate = String.Format("{0:yyyy-MM-dd}", ToDate)
        Dim MonthName As String = AgL.XNull(AgL.Dman_Execute(" select case strftime('%m', '" & newdate & "') when '01' then 'January' when '02' then 'Febuary' when '03' then 'March' 
                    when '04' then 'April' when '05' then 'May' when '06' then 'June' when '07' then 'July' 
                    when '08' then 'August' when '09' then 'September' when '10' then 'October' when '11' then 'November' 
                    when '12' then 'December' else '' end as month ", AgL.GCn).ExecuteScalar)

        Dim FilePath As String = ""
        Dim SaveFileDialogBox As SaveFileDialog
        Dim sFilePath As String = ""
        SaveFileDialogBox = New SaveFileDialog

        SaveFileDialogBox.Title = "File Name"
        FilePath = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialogBox.InitialDirectory = FilePath
        SaveFileDialogBox.FilterIndex = 1
        SaveFileDialogBox.FileName = "GSTR1_" + MonthName + ".xlsx"
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        OutputFile = SaveFileDialogBox.FileName

        Dim xlApp As Excel.Application
        Dim TemplateWorkBook As Excel.Workbook
        Dim OutputWorkBook As Excel.Workbook

        xlApp = New Excel.Application
        xlApp.AlertBeforeOverwriting = False
        xlApp.DisplayAlerts = False

        TemplateWorkBook = xlApp.Workbooks.Open(My.Application.Info.DirectoryPath + "\Templates\" + "GSTR1_Excel_Workbook_Template_V2.0.xlsx")
        TemplateWorkBook.SaveAs(OutputFile)
        xlApp.Workbooks.Close()
        OutputWorkBook = xlApp.Workbooks.Open(OutputFile)

        Try
            Dim DtTableB2b As DataTable = Nothing
            Dim DtTableB2CL As DataTable = Nothing
            Dim DtTableB2CS As DataTable = Nothing
            Dim DtTableCDNR As DataTable = Nothing
            Dim DtTableCDNUR As DataTable = Nothing
            Dim DtTableEXEMP As DataTable = Nothing
            Dim DtTableHSN As DataTable = Nothing
            Dim DtTableDOCS As DataTable = Nothing

            Dim xlWorkSheet_B2b As Excel.Worksheet
            Dim xlWorkSheet_B2CL As Excel.Worksheet
            Dim xlWorkSheet_B2CS As Excel.Worksheet
            Dim xlWorkSheet_CDNR As Excel.Worksheet
            Dim xlWorkSheet_CDNUR As Excel.Worksheet
            Dim xlWorkSheet_EXEMP As Excel.Worksheet
            Dim xlWorkSheet_HSN As Excel.Worksheet
            Dim xlWorkSheet_DOCS As Excel.Worksheet


            Dim I As Integer = 0

            FGetGSTR1FileCreationData(DtTableB2b, DtTableB2CL, DtTableB2CS, DtTableCDNR,
                                      DtTableCDNUR, DtTableEXEMP, DtTableHSN, DtTableDOCS)

            xlWorkSheet_B2b = OutputWorkBook.Worksheets("b2b,sez,de")
            FillGSTR1ExcelFiles(DtTableB2b, xlWorkSheet_B2b)

            xlWorkSheet_B2CL = OutputWorkBook.Worksheets("b2cl")
            FillGSTR1ExcelFiles(DtTableB2CL, xlWorkSheet_B2CL)

            xlWorkSheet_B2CS = OutputWorkBook.Worksheets("b2cs")
            FillGSTR1ExcelFiles(DtTableB2CS, xlWorkSheet_B2CS)

            xlWorkSheet_CDNR = OutputWorkBook.Worksheets("cdnr")
            FillGSTR1ExcelFiles(DtTableCDNR, xlWorkSheet_CDNR)

            xlWorkSheet_CDNUR = OutputWorkBook.Worksheets("cdnur")
            FillGSTR1ExcelFiles(DtTableCDNUR, xlWorkSheet_CDNUR)

            xlWorkSheet_EXEMP = OutputWorkBook.Worksheets("EXEMP")
            FillGSTR1ExcelFiles(DtTableEXEMP, xlWorkSheet_EXEMP)

            xlWorkSheet_HSN = OutputWorkBook.Worksheets("HSN")
            FillGSTR1ExcelFiles(DtTableHSN, xlWorkSheet_HSN)

            xlWorkSheet_DOCS = OutputWorkBook.Worksheets("docs")
            FillGSTR1ExcelFiles(DtTableDOCS, xlWorkSheet_DOCS)

            OutputWorkBook.Save()
            OutputWorkBook.Close()
            xlApp.Quit()

            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)
            ClsMain.FReleaseObjects(OutputWorkBook)
            'System.Diagnostics.Process.Start(OutputFile)

            MsgBox("File Generated Successfully.", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
            OutputWorkBook.Close()
            xlApp.Quit()
            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)
        End Try
    End Sub
    Public Sub FCreateGSTR1JSONFile(DGL As AgControls.AgDataGrid)
        Try
            Dim I As Integer = 0
            Dim J As Integer = 0
            Dim K As Integer = 0
            Dim M As Integer = 0

            Dim CGSTAmount As Double = 0
            Dim SGSTAmount As Double = 0
            Dim IGSTAmount As Double = 0
            Dim TotalTaxAmount As Double = 0


            'Dim TabStr_1 As String = ControlChars.Tab
            'Dim TabStr_2 As String = ControlChars.Tab + ControlChars.Tab
            'Dim TabStr_3 As String = ControlChars.Tab + ControlChars.Tab + ControlChars.Tab
            'Dim TabStr_4 As String = ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab
            'Dim TabStr_5 As String = ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab
            'Dim TabStr_6 As String = ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab
            'Dim TabStr_7 As String = ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab
            'Dim TabStr_8 As String = ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab
            'Dim TabStr_9 As String = ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab
            'Dim TabStr_10 As String = ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab


            Dim TabStr_1 As String = "  "
            Dim TabStr_2 As String = "    "
            Dim TabStr_3 As String = "      "
            Dim TabStr_4 As String = "        "
            Dim TabStr_5 As String = "          "
            Dim TabStr_6 As String = "            "
            Dim TabStr_7 As String = "              "
            Dim TabStr_8 As String = "                "
            Dim TabStr_9 As String = "                  "
            Dim TabStr_10 As String = "                    "


            Dim DtTableB2b As DataTable = Nothing
            Dim DtTableB2CL As DataTable = Nothing
            Dim DtTableB2CS As DataTable = Nothing
            Dim DtTableCDNR As DataTable = Nothing
            Dim DtTableCDNUR As DataTable = Nothing
            Dim DtTableEXEMP As DataTable = Nothing
            Dim DtTableHSN As DataTable = Nothing
            Dim DtTableDOCS As DataTable = Nothing

            Dim ToDate As DateTime = ReportFrm.FGetText(rowToDate)
            Dim newdate = String.Format("{0:yyyy-MM-dd}", ToDate)
            Dim MonthName As String = AgL.XNull(AgL.Dman_Execute(" select case strftime('%m', '" & newdate & "') when '01' then 'January' when '02' then 'Febuary' when '03' then 'March' 
                    when '04' then 'April' when '05' then 'May' when '06' then 'June' when '07' then 'July' 
                    when '08' then 'August' when '09' then 'September' when '10' then 'October' when '11' then 'November' 
                    when '12' then 'December' else '' end as month ", AgL.GCn).ExecuteScalar)


            Dim FilePath As String = ""
            Dim SaveFileDialogBox As SaveFileDialog
            Dim sFilePath As String = ""
            SaveFileDialogBox = New SaveFileDialog
            SaveFileDialogBox.Title = "File Name"
            FilePath = My.Computer.FileSystem.SpecialDirectories.Desktop
            SaveFileDialogBox.InitialDirectory = FilePath
            SaveFileDialogBox.FilterIndex = 1
            SaveFileDialogBox.FileName = "GSTR1_JSON_" + MonthName + ".json"
            If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
            sFilePath = SaveFileDialogBox.FileName


            mQry = " SELECT ManualCode || '-' || Description AS StateNameWithCode, Description AS StateName, 
                    ManualCode As StateCode FROM State "
            Dim DtStates As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            mQry = " Select '' As DivisionSalesTaxNo, Sg.DispName As DivisionName, 
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                     Then IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'')
                     Else Sg.Address END As DivisionAddress,
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                     Then Sm.PinNo Else Sg.PIN END As DivisionPinCode, 
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                     Then SS.ManualCode Else S.ManualCode END As DivisionStateCode,
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                     Then SS.ManualCode || '-' || SS.Description 
                     Else S.ManualCode || '-' || S.Description END AS DivisionStateNameWithCode
                From Division D
                LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                LEFT JOIN City C On Sg.CityCode = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN SiteMast Sm ON 1=1
                LEFT JOIN City SC On Sm.City_Code = SC.CityCode
                LEFT JOIN State SS On SC.State = SS.Code
                Where D.Div_Code = " & AgL.Chk_Text(AgL.XNull(ReportFrm.FGetCode(rowDivision)).ToString.Replace("'", "")) & "
                And Sm.Code In (" & AgL.XNull(ReportFrm.FGetCode(rowSite)).ToString.Replace("''", "'") & ")"
            Dim DtDivisionDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            DtDivisionDetail.Rows(0)("DivisionSalesTaxNo") = GetSalesTaxNo()

            Dim bMonthYear As String = CDate(ReportFrm.FGetText(rowToDate)).ToString("MM") +
                CDate(ReportFrm.FGetText(rowToDate)).ToString("yyyy")


            FGetGSTR1FileCreationData(DtTableB2b, DtTableB2CL, DtTableB2CS, DtTableCDNR,
                                      DtTableCDNUR, DtTableEXEMP, DtTableHSN, DtTableDOCS)



            Dim mGSTR1JsonVersion As String = ""
            mGSTR1JsonVersion = ClsMain.FGetSettings(ClsMain.SettingFields.GSTR1JsonVersion, SettingType.General, "", "", "", "", "", "", "")
            If mGSTR1JsonVersion = "" Then mGSTR1JsonVersion = "GST3.0.4"

            Dim fileExists As Boolean = File.Exists(sFilePath)
            If fileExists Then File.Delete(sFilePath)
            Dim StringTabPresses As String = ""
            Using sw As New StreamWriter(File.Open(sFilePath, FileMode.OpenOrCreate))
                sw.WriteLine("{")
                sw.WriteLine(TabStr_1 + """gstin"": """ & DtDivisionDetail.Rows(0)("DivisionSalesTaxNo") & """,")
                sw.WriteLine(TabStr_1 + """fp"": """ & bMonthYear & """,")
                'sw.WriteLine(TabStr_1 + """gt"": 2000000,")
                'sw.WriteLine(TabStr_1 + """cur_gt"": 200000,")
                'sw.WriteLine(TabStr_1 + """version"": ""GST2.4"",")
                sw.WriteLine(TabStr_1 + """version"": """ & mGSTR1JsonVersion & """,")
                sw.WriteLine(TabStr_1 + """hash"": ""hash"",")

                'B2B
                Dim DtDistinctGSTNo_B2B As DataTable = DtTableB2b.DefaultView.ToTable(True, "GSTINofRecipient")
                For I = 0 To DtDistinctGSTNo_B2B.Rows.Count - 1
                    If I = 0 Then sw.WriteLine(TabStr_1 + """b2b,sez,de"": [")
                    sw.WriteLine(TabStr_2 + "{")
                    sw.WriteLine(TabStr_3 + """ctin"": """ & DtDistinctGSTNo_B2B.Rows(I)("GSTINofRecipient") & """,")
                    sw.WriteLine(TabStr_3 + """inv"": [")

                    Dim DtTableB2b_FilteredForGstNo As New DataTable
                    DtTableB2b_FilteredForGstNo = DtTableB2b.Clone
                    Dim DtB2BRows_FilteredForGstNo As DataRow() = DtTableB2b.Select("GSTINofRecipient = '" & DtDistinctGSTNo_B2B.Rows(I)("GSTINofRecipient") & "'")
                    For M = 0 To DtB2BRows_FilteredForGstNo.Length - 1
                        DtTableB2b_FilteredForGstNo.ImportRow(DtB2BRows_FilteredForGstNo(M))
                    Next


                    Dim DtDistinctInvoiceNo_B2B As DataTable = DtTableB2b_FilteredForGstNo.DefaultView.ToTable(True, "InvoiceNumber")
                    For J = 0 To DtDistinctInvoiceNo_B2B.Rows.Count - 1

                        Dim DtTableB2b_FilteredForGstAndInvoiceNo As New DataTable
                        DtTableB2b_FilteredForGstAndInvoiceNo = DtTableB2b.Clone
                        Dim DtB2BRows_FilteredForGstAndInvoiceNo As DataRow() = DtTableB2b.Select("GSTINofRecipient = '" & DtDistinctGSTNo_B2B.Rows(I)("GSTINofRecipient") & "'
                                        And InvoiceNumber = '" & DtDistinctInvoiceNo_B2B.Rows(J)("InvoiceNumber") & "'")
                        For M = 0 To DtB2BRows_FilteredForGstAndInvoiceNo.Length - 1
                            DtTableB2b_FilteredForGstAndInvoiceNo.ImportRow(DtB2BRows_FilteredForGstAndInvoiceNo(M))
                        Next

                        sw.WriteLine(TabStr_4 + "{")
                        sw.WriteLine(TabStr_5 + """inum"": """ & AgL.XNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(0)("InvoiceNumber")) & """,")
                        sw.WriteLine(TabStr_5 + """idt"": """ & CDate(AgL.XNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(0)("InvoiceDate"))).ToString("dd'-'MM'-'yyyy") & """,")
                        sw.WriteLine(TabStr_5 + """val"": " & AgL.XNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(0)("InvoiceValue")) & ",")

                        Dim DtStateRow As DataRow() = DtStates.Select(" StateNameWithCode = '" & DtTableB2b_FilteredForGstAndInvoiceNo.Rows(0)("PlaceOfSupply") & "'")
                        sw.WriteLine(TabStr_5 + """pos"": """ & AgL.XNull(DtStateRow(0)("StateCode")) & """,")

                        sw.WriteLine(TabStr_5 + """rchrg"": """ & AgL.XNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(0)("ReverseCharge")) & """,")
                        sw.WriteLine(TabStr_5 + """inv_typ"": """ & AgL.XNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(0)("InvoiceType")).ToString.Substring(0, 1) & """,")
                        sw.WriteLine(TabStr_5 + """itms"": [")

                        For K = 0 To DtTableB2b_FilteredForGstAndInvoiceNo.Rows.Count - 1
                            sw.WriteLine(TabStr_6 + "{")
                            If AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("Rate")) = 5 Then
                                sw.WriteLine(TabStr_7 + """num"": 501,")
                            ElseIf AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("Rate")) = 12 Then
                                sw.WriteLine(TabStr_7 + """num"": 1201,")
                            ElseIf AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("Rate")) = 18 Then
                                sw.WriteLine(TabStr_7 + """num"": 1801,")
                            ElseIf AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("Rate")) = 28 Then
                                sw.WriteLine(TabStr_7 + """num"": 2801,")
                            End If
                            sw.WriteLine(TabStr_7 + """itm_det"": {")
                            sw.WriteLine(TabStr_8 + """txval"": " & AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("TaxableValue")) & ",")
                            sw.WriteLine(TabStr_8 + """rt"": " & AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("Rate")) & ",")

                            If AgL.XNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("PlaceOfSupply")) <> DtDivisionDetail.Rows(0)("DivisionStateNameWithCode") Then
                                IGSTAmount = Math.Round(AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("TaxableValue")) * (AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("Rate"))) / 100, 2)
                                sw.WriteLine(TabStr_8 + """iamt"": " & IGSTAmount & ",")
                            Else
                                TotalTaxAmount = AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("TaxableValue")) * (AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("Rate"))) / 100
                                CGSTAmount = Math.Round(TotalTaxAmount / 2, 2)
                                SGSTAmount = Math.Round(TotalTaxAmount / 2, 2)
                                sw.WriteLine(TabStr_8 + """camt"": " & CGSTAmount & ",")
                                sw.WriteLine(TabStr_8 + """samt"": " & SGSTAmount & ",")
                            End If

                            sw.WriteLine(TabStr_8 + """csamt"": " & AgL.VNull(DtTableB2b_FilteredForGstAndInvoiceNo.Rows(K)("CessAmount")) & "")
                            sw.WriteLine(TabStr_7 + "}")
                            sw.WriteLine(TabStr_6 + "}" + IIf(K < DtTableB2b_FilteredForGstAndInvoiceNo.Rows.Count - 1, ",", ""))
                        Next

                        sw.WriteLine(TabStr_5 + "]")
                        sw.WriteLine(TabStr_4 + "}" + IIf(J < DtDistinctInvoiceNo_B2B.Rows.Count - 1, ",", ""))
                    Next
                    sw.WriteLine(TabStr_3 + "]")
                    sw.WriteLine(TabStr_2 + "}" + IIf(I < DtDistinctGSTNo_B2B.Rows.Count - 1, ",", ""))
                    If I = DtDistinctGSTNo_B2B.Rows.Count - 1 Then sw.WriteLine(TabStr_1 + "],")
                Next



                'B2CS
                For I = 0 To DtTableB2CS.Rows.Count - 1
                    If I = 0 Then sw.WriteLine(TabStr_1 + """b2cs"": [")
                    sw.WriteLine(TabStr_2 + "{")

                    If AgL.XNull(DtTableB2CS.Rows(I)("PlaceOfSupply")) <> DtDivisionDetail.Rows(0)("DivisionStateNameWithCode") Then
                        sw.WriteLine(TabStr_3 + """sply_ty"": ""INTER"",")
                    Else
                        sw.WriteLine(TabStr_3 + """sply_ty"": ""INTRA"",")
                    End If

                    sw.WriteLine(TabStr_3 + """rt"": " & DtTableB2CS.Rows(I)("Rate") & ",")
                    sw.WriteLine(TabStr_3 + """typ"": """ & DtTableB2CS.Rows(I)("Type") & """,")

                    Dim DtStateRow As DataRow() = DtStates.Select(" StateNameWithCode = '" & DtTableB2CS.Rows(I)("PlaceOfSupply") & "'")
                    sw.WriteLine(TabStr_3 + """pos"": """ & AgL.XNull(DtStateRow(0)("StateCode")) & """,")

                    sw.WriteLine(TabStr_3 + """txval"": " & DtTableB2CS.Rows(I)("TaxableValue") & ",")

                    If AgL.XNull(DtTableB2CS.Rows(I)("PlaceOfSupply")) <> DtDivisionDetail.Rows(0)("DivisionStateNameWithCode") Then
                        IGSTAmount = Math.Round(AgL.VNull(DtTableB2CS.Rows(I)("TaxableValue")) * (AgL.VNull(DtTableB2CS.Rows(I)("Rate"))) / 100, 2)
                        sw.WriteLine(TabStr_3 + """iamt"": " & IGSTAmount & ",")
                    Else
                        TotalTaxAmount = AgL.VNull(DtTableB2CS.Rows(I)("TaxableValue")) * (AgL.VNull(DtTableB2CS.Rows(I)("Rate"))) / 100
                        CGSTAmount = Math.Round(TotalTaxAmount / 2, 2)
                        SGSTAmount = Math.Round(TotalTaxAmount / 2, 2)
                        sw.WriteLine(TabStr_3 + """camt"": " & CGSTAmount & ",")
                        sw.WriteLine(TabStr_3 + """samt"": " & SGSTAmount & ",")
                    End If
                    sw.WriteLine(TabStr_3 + """csamt"": " & AgL.VNull(DtTableB2CS.Rows(I)("CessAmount")) & "")
                    sw.WriteLine(TabStr_2 + "}" + IIf(I < DtTableB2CS.Rows.Count - 1, ",", ""))
                    If I = DtTableB2CS.Rows.Count - 1 Then sw.WriteLine(TabStr_1 + "],")
                Next





                'CDNR
                Dim DtDistinctGSTNo_CDNR As DataTable = DtTableCDNR.DefaultView.ToTable(True, "GSTINofRecipient")
                For I = 0 To DtDistinctGSTNo_CDNR.Rows.Count - 1
                    If I = 0 Then sw.WriteLine(TabStr_1 + """cdnr"": [")
                    sw.WriteLine(TabStr_2 + "{")
                    sw.WriteLine(TabStr_3 + """ctin"": """ & DtDistinctGSTNo_CDNR.Rows(I)("GSTINofRecipient") & """,")
                    sw.WriteLine(TabStr_3 + """nt"": [")

                    Dim DtTableCDNR_FilteredForGstNo As New DataTable
                    DtTableCDNR_FilteredForGstNo = DtTableCDNR.Clone
                    Dim DtCDNRRows_FilteredForGstNo As DataRow() = DtTableCDNR.Select("GSTINofRecipient = '" & DtDistinctGSTNo_CDNR.Rows(I)("GSTINofRecipient") & "'")
                    For M = 0 To DtCDNRRows_FilteredForGstNo.Length - 1
                        DtTableCDNR_FilteredForGstNo.ImportRow(DtCDNRRows_FilteredForGstNo(M))
                    Next

                    For J = 0 To DtTableCDNR_FilteredForGstNo.Rows.Count - 1
                        sw.WriteLine(TabStr_4 + "{")
                        sw.WriteLine(TabStr_5 + """nt_num"": """ & AgL.XNull(DtTableCDNR_FilteredForGstNo.Rows(J)("NoteNumber")) & """,")
                        sw.WriteLine(TabStr_5 + """nt_dt"": """ & CDate(AgL.XNull(DtTableCDNR_FilteredForGstNo.Rows(J)("NoteDate"))).ToString("dd'-'MM'-'yyyy") & """,")
                        sw.WriteLine(TabStr_5 + """ntty"": """ & AgL.XNull(DtTableCDNR_FilteredForGstNo.Rows(J)("NoteType")) & """,")
                        sw.WriteLine(TabStr_5 + """val"": " & AgL.XNull(DtTableCDNR_FilteredForGstNo.Rows(J)("NoteValue")) & ",")

                        Dim DtStateRow As DataRow() = DtStates.Select(" StateNameWithCode = '" & DtTableCDNR_FilteredForGstNo.Rows(0)("PlaceOfSupply") & "'")
                        sw.WriteLine(TabStr_5 + """pos"": """ & AgL.XNull(DtStateRow(0)("StateCode")) & """,")
                        sw.WriteLine(TabStr_5 + """rchrg"": """ & AgL.XNull(DtTableCDNR_FilteredForGstNo.Rows(J)("ReverseCharge")) & """,")

                        Select Case AgL.XNull(DtTableCDNR_FilteredForGstNo.Rows(J)("NoteSupplyType"))
                            Case "Regular"
                                sw.WriteLine(TabStr_5 + """inv_typ"": ""R"",")
                        End Select

                        sw.WriteLine(TabStr_5 + """itms"": [")
                        sw.WriteLine(TabStr_6 + "{")

                        If AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("Rate")) = 5 Then
                            sw.WriteLine(TabStr_7 + """num"": 501,")
                        ElseIf AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("Rate")) = 12 Then
                            sw.WriteLine(TabStr_7 + """num"": 1201,")
                        ElseIf AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("Rate")) = 18 Then
                            sw.WriteLine(TabStr_7 + """num"": 1801,")
                        ElseIf AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("Rate")) = 28 Then
                            sw.WriteLine(TabStr_7 + """num"": 2801,")
                        End If

                        sw.WriteLine(TabStr_7 + """itm_det"": {")
                        sw.WriteLine(TabStr_8 + """txval"": " & AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("TaxableValue")) & ",")
                        sw.WriteLine(TabStr_8 + """rt"": " & AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("Rate")) & ",")

                        If AgL.XNull(DtTableCDNR_FilteredForGstNo.Rows(J)("PlaceOfSupply")) <> DtDivisionDetail.Rows(0)("DivisionStateNameWithCode") Then
                            IGSTAmount = Math.Round(AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("TaxableValue")) * (AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("Rate"))) / 100, 2)
                            sw.WriteLine(TabStr_8 + """iamt"": " & IGSTAmount & ",")
                        Else
                            TotalTaxAmount = AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("TaxableValue")) * (AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("Rate"))) / 100
                            CGSTAmount = Math.Round(TotalTaxAmount / 2, 2)
                            SGSTAmount = Math.Round(TotalTaxAmount / 2, 2)
                            sw.WriteLine(TabStr_8 + """camt"": " & CGSTAmount & ",")
                            sw.WriteLine(TabStr_8 + """samt"": " & SGSTAmount & ",")
                        End If

                        sw.WriteLine(TabStr_8 + """csamt"": " & AgL.VNull(DtTableCDNR_FilteredForGstNo.Rows(J)("CessAmount")) & "")
                        sw.WriteLine(TabStr_7 + "}")
                        sw.WriteLine(TabStr_6 + "}")
                        sw.WriteLine(TabStr_5 + "]")
                        sw.WriteLine(TabStr_4 + "}" + IIf(J < DtTableCDNR_FilteredForGstNo.Rows.Count - 1, ",", ""))
                    Next
                    sw.WriteLine(TabStr_3 + "]")
                    sw.WriteLine(TabStr_2 + "}" + IIf(I < DtDistinctGSTNo_CDNR.Rows.Count - 1, ",", ""))
                    If I = DtDistinctGSTNo_CDNR.Rows.Count - 1 Then sw.WriteLine(TabStr_1 + "],")
                Next





                'DOCS


                Dim DtDistinctDocType_DOCS As DataTable = DtTableDOCS.DefaultView.ToTable(True, "Type")
                For I = 0 To DtDistinctDocType_DOCS.Rows.Count - 1
                    If I = 0 Then
                        sw.WriteLine(TabStr_1 + """doc_issue"": {")
                        sw.WriteLine(TabStr_2 + """doc_det"": [")
                    End If
                    sw.WriteLine(TabStr_3 + "{")

                    Dim Doc_Num As Integer = 0
                    Select Case DtDistinctDocType_DOCS.Rows(I)("Type")
                        Case "Invoices for outward supply"
                            Doc_Num = 1
                        Case "Invoices for inward supply from unregistered person"
                            Doc_Num = 2
                        Case "Revised Invoice"
                            Doc_Num = 3
                        Case "Debit Note"
                            Doc_Num = 4
                        Case "Credit Note"
                            Doc_Num = 5
                        Case "Receipt Voucher"
                            Doc_Num = 6
                        Case "Payment Voucher"
                            Doc_Num = 7
                        Case "Refund Voucher"
                            Doc_Num = 8
                    End Select

                    sw.WriteLine(TabStr_4 + """doc_num"": " & Doc_Num & ",")
                    sw.WriteLine(TabStr_4 + """doc_typ"": """ & DtDistinctDocType_DOCS.Rows(I)("Type") & """,")
                    sw.WriteLine(TabStr_4 + """docs"": [")

                    Dim DtTableDOCS_FilteredForDocType As New DataTable
                    DtTableDOCS_FilteredForDocType = DtTableDOCS.Clone
                    Dim DtDOCSRows_FilteredForDocType As DataRow() = DtTableDOCS.Select("Type = '" & DtDistinctDocType_DOCS.Rows(I)("Type") & "'")
                    For M = 0 To DtDOCSRows_FilteredForDocType.Length - 1
                        DtTableDOCS_FilteredForDocType.ImportRow(DtDOCSRows_FilteredForDocType(M))
                    Next

                    For J = 0 To DtTableDOCS_FilteredForDocType.Rows.Count - 1
                        sw.WriteLine(TabStr_5 + "{")
                        sw.WriteLine(TabStr_6 + """num"": " & J + 1 & ",")
                        sw.WriteLine(TabStr_6 + """from"": """ & AgL.XNull(DtTableDOCS_FilteredForDocType.Rows(J)("SrNoFrom")) & """,")
                        sw.WriteLine(TabStr_6 + """to"": """ & AgL.XNull(DtTableDOCS_FilteredForDocType.Rows(J)("SrNoTo")) & """,")
                        sw.WriteLine(TabStr_6 + """totnum"": " & AgL.VNull(DtTableDOCS_FilteredForDocType.Rows(J)("TotalNumber")) & ",")
                        sw.WriteLine(TabStr_6 + """cancel"": " & AgL.VNull(DtTableDOCS_FilteredForDocType.Rows(J)("Cancelled")) & ",")
                        sw.WriteLine(TabStr_6 + """net_issue"": " & AgL.VNull(DtTableDOCS_FilteredForDocType.Rows(J)("TotalNumber")) & "")
                        sw.WriteLine(TabStr_5 + "}" + IIf(J < DtTableDOCS_FilteredForDocType.Rows.Count - 1, ",", ""))
                    Next
                    sw.WriteLine(TabStr_4 + "]")
                    sw.WriteLine(TabStr_3 + "}" + IIf(I < DtDistinctDocType_DOCS.Rows.Count - 1, ",", ""))
                    If I = DtDistinctDocType_DOCS.Rows.Count - 1 Then
                        sw.WriteLine(TabStr_2 + "]")
                        sw.WriteLine(TabStr_1 + "},")
                    End If
                Next


                'HSN
                For I = 0 To DtTableHSN.Rows.Count - 1
                    If I = 0 Then
                        sw.WriteLine(TabStr_1 + """hsn"": {")
                        sw.WriteLine(TabStr_2 + """data"": [")
                    End If
                    sw.WriteLine(TabStr_3 + "{")
                    sw.WriteLine(TabStr_4 + """num"": " & I + 1 & ",")
                    sw.WriteLine(TabStr_4 + """hsn_sc"": """ & DtTableHSN.Rows(I)("HSN") & """,")
                    sw.WriteLine(TabStr_4 + """desc"": """ & FRemoveSpecialCharactersForGSTReturns(RTrim(AgL.XNull(DtTableHSN.Rows(I)("ItemCategory")))) & """,")

                    Dim UQC As String = AgL.XNull(DtTableHSN.Rows(I)("UQC")).ToString()
                    Dim UnitName As String
                    If UQC <> "" Then
                        UnitName = UQC.Substring(0, UQC.IndexOf("-"))
                    Else
                        UnitName = ""
                    End If

                    If Math.Round(AgL.VNull(DtTableHSN.Rows(I)("TotalQty")), 2) = 0 Then
                        UnitName = "NA"
                    End If
                    sw.WriteLine(TabStr_4 + """uqc"": """ & UnitName & """,")
                    sw.WriteLine(TabStr_4 + """qty"": " & Math.Round(AgL.VNull(DtTableHSN.Rows(I)("TotalQty")), 2) & ",")
                    'sw.WriteLine(TabStr_4 + """val"": " & Math.Round(AgL.VNull(DtTableHSN.Rows(I)("InvoiceValue")), 2) & ",")
                    sw.WriteLine(TabStr_4 + """rt"": " & Math.Round(AgL.VNull(DtTableHSN.Rows(I)("Rate")), 2) & ",")
                    sw.WriteLine(TabStr_4 + """txval"": " & Math.Round(AgL.VNull(DtTableHSN.Rows(I)("TaxableValue")), 2) & ",")
                    sw.WriteLine(TabStr_4 + """iamt"": " & Math.Round(AgL.VNull(DtTableHSN.Rows(I)("IntegratedTaxAmount")), 2) & ",")
                    sw.WriteLine(TabStr_4 + """samt"": " & Math.Round(AgL.VNull(DtTableHSN.Rows(I)("StateTaxAmount")), 2) & ",")
                    sw.WriteLine(TabStr_4 + """camt"": " & Math.Round(AgL.VNull(DtTableHSN.Rows(I)("CentralTaxAmount")), 2) & ",")
                    sw.WriteLine(TabStr_4 + """csamt"": " & Math.Round(AgL.VNull(DtTableHSN.Rows(I)("CessAmount")), 2) & "")
                    sw.WriteLine(TabStr_3 + "}" + IIf(I < DtTableHSN.Rows.Count - 1, ",", ""))
                    If I = DtTableHSN.Rows.Count - 1 Then
                        sw.WriteLine(TabStr_2 + "]")
                        sw.WriteLine(TabStr_1 + "}")
                    End If
                Next
                sw.WriteLine("}")
            End Using

            MsgBox("File Generated Successfully.", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    Private Sub FGetGSTR1FileCreationData(ByRef DtTableB2b As DataTable, ByRef DtTableB2CL As DataTable,
                                          ByRef DtTableB2CS As DataTable, ByRef DtTableCDNR As DataTable,
                                          ByRef DtTableCDNUR As DataTable, ByRef DtTableEXEMP As DataTable,
                                          ByRef DtTableHSN As DataTable, ByRef DtTableDOCS As DataTable)
        Dim mCondStr As String = ""

        mCondStr = " Where 1=1"
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
        'mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
        mCondStr = mCondStr & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "

        mCondStr = mCondStr & " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "',
                                        '" & Ncat.DebitNoteSupplier & "', '" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "','" & Ncat.CreditNoteSupplier & "')"
        mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
        mCondStr = mCondStr & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "

        mQry = "Select Max(H.GSTINofRecipient) As GSTINofRecipient, Max(H.ReceiverName) As ReceiverName, 
                    Max(H.InvoiceNumber) As InvoiceNumber,
                    Max(H.InvoiceDate) As InvoiceDate, Max(H.HeaderNet_Amount) As InvoiceValue, 
                    Max(H.PlaceOfSupply) As PlaceOfSupply, Max(H.ReverseCharge) As ReverseCharge,
                    Max(H.ApplicableTaxRate) As ApplicableTaxRate, Max(H.InvoiceType) As InvoiceType,	
                    Max(H.ECommerceGSTIN) As ECommerceGSTIN, Max(H.Rate) As Rate,	
                    Sum(H.TaxableValue) As TaxableValue, Sum(H.CessAmount) As CessAmount 
                    From (" + FGetB2BQry(mCondStr) + ") As H 
                    Group By H.DocID, H.SalesTaxGroupItem
                    Order By InvoiceDate "
        DtTableB2b = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT Max(H.GSTINofRecipient) As GSTINofRecipient, Max(H.ReceiverName) As ReceiverName, Max(H.InvoiceNumber) As InvoiceNumber,
                    Max(H.InvoiceDate) As InvoiceDate, Sum(H.InvoiceValue) As InvoiceValue, Max(H.PlaceOfSupply) As PlaceOfSupply, Max(H.ReverseCharge) As ReverseCharge,
                    Max(H.ApplicableTaxRate) As ApplicableTaxRate, Max(H.InvoiceType) As InvoiceType, Max(H.ECommerceGSTIN) As ECommerceGSTIN,	 
                    Max(H.Rate) As Rate, Sum(H.TaxableValue) As TaxableValue,  Sum(H.CessAmount) As CessAmount
                    From (" + FGetB2CLargeQry(mCondStr) + ") As H 
                    Group By H.DocID, H.SalesTaxGroupItem 
                    Order By InvoiceDate "
        DtTableB2CL = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " SELECT Max(H.Type) As Type, Max(H.PlaceOfSupply) As PlaceOfSupply, Max(H.ApplicablePercentOfTaxRate) As ApplicablePercentOfTaxRate,
                    Max(H.Rate) As Rate, Sum(H.TaxableValue) As TaxableValue, Sum(H.CessAmount) As CessAmount, 
                    Max(H.ECommerceGSTIN) As ECommerceGSTIN
                    From (" + FGetB2CSmallQry(mCondStr) + ") As H 
                    Group By H.PlaceOfSupply, H.SalesTaxGroupItem "
        DtTableB2CS = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " SELECT Max(H.GSTINofRecipient) As GSTINofRecipient, Max(H.ReceiverName) As ReceiverName, 
                Max(H.DebitCreditNoteNo) As NoteNumber, Max(H.DebitCreditNoteDate) As NoteDate, 
                Max(H.DocumentType) As NoteType,
                Max(H.PlaceOfSupply) As PlaceOfSupply, 
                'N' As ReverseCharge,
                'Regular' As NoteSupplyType,
                Max(H.HeaderNet_Amount) As NoteValue, 
                Max(H.ApplicableTaxRate) As ApplicableTaxRate, 
                Max(H.Rate) As Rate,
                Sum(H.TaxableValue) As TaxableValue, 
                Sum(H.CessAmount) As CessAmount 
                From (" + FGetCreditDebitNoteRegisteredQry(mCondStr) + ") As H 
                Group By H.DocID, H.SalesTaxGroupItem
                Order By NoteDate, NoteNumber "
        DtTableCDNR = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " SELECT Max(H.URType) As URType,
                Max(H.DebitCreditNoteNo) As DebitCreditNoteNo, Max(H.DebitCreditNoteDate) As DebitCreditNoteDate, 
                Max(H.DocumentType) As DocumentType,
                Max(H.InvoiceNumber) As InvoiceNumber, Max(H.InvoiceDate) As InvoiceDate,
                Max(H.PlaceOfSupply) As PlaceOfSupply, 
                Max(H.DebitCreditNoteValue) As DebitCreditNoteValue, 
                Max(H.ApplicableTaxRate) As ApplicableTaxRate,
                Max(H.Rate) As Rate,
                Sum(H.TaxableValue) As TaxableValue, Sum(H.CessAmount) As CessAmount, Max(H.PreGST) As PreGST
                From (" + FGetCreditDebitNoteUnRegisteredQry(mCondStr) + ") As H 
                Group By H.DocID, H.SalesTaxGroupItem "
        DtTableCDNUR = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " SELECT H.ItemCategory As ItemCategory, Sum(H.NilRatedSupplies) As NilRatedSupplies,
                Sum(H.ExemptedSupplies) As ExemptedSupplies, Sum(H.NonGSTSupplies) As NonGSTSupplies
                From (" + FGetNilRatedInvoiceQry(mCondStr) + ") As H 
                Group By H.ItemCategory "
        DtTableEXEMP = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.HSN As HSN, Max(H.ItemCategory) As ItemCategory, Max(H.UQC) As UQC,
                Sum(H.Qty) As TotalQty, Sum(H.InvoiceValue) As InvoiceValue, H.GrossTaxRate As Rate, Sum(H.TaxableValue) As TaxableValue, 
                Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, Sum(H.CentralTaxAmount) As CentralTaxAmount, 
                Sum(H.StateTaxAmount) As StateTaxAmount, Sum(H.CessAmount) As CessAmount
                From (" + FGetHSNQry(mCondStr) + ") As H 
                Group By H.HSN, H.GrossTaxRate "
        DtTableHSN = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " Select H.Type, 
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Max(H.DivisionShortName),'')),'<SITE>',IfNull(Max(H.SiteShortName),'')),'<DOCTYPE>',IfNull(Max(H.VoucherTypeShortName),'')),'<DOCNO>',IfNull(Min(H.InvoiceNumber_Format),'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As SrNoFrom,
                    Replace(Replace(Replace(Replace(Replace('" & mDocumentNoPattern & "','<DIVISION>',IfNull(Max(H.DivisionShortName),'')),'<SITE>',IfNull(Max(H.SiteShortName),'')),'<DOCTYPE>',IfNull(Max(H.VoucherTypeShortName),'')),'<DOCNO>',IfNull(Max(H.InvoiceNumber_Format),'')),'<COMPANYPREFIX>', '" & mCompanyPrefix & "') As SrNoTo,
                    Count(Distinct DocId) As TotalNumber, Sum(H.Cancelled) As Cancelled
                    From (" + FGetDOCSQry(mCondStr) + ") As H 
                    Group By H.Type, H.VoucherType, H.DivisionName, H.SiteName "
        DtTableDOCS = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FillGSTR1ExcelFiles(DtTable As DataTable, xlWorkSheet As Excel.Worksheet)
        If DtTable.Rows.Count > 0 Then
            For ColIndex As Integer = 0 To DtTable.Columns.Count - 1
                Dim ColumnValues(0, 0) As Object
                ReDim ColumnValues(0 To DtTable.Rows.Count - 1, 0)
                For I As Integer = 0 To DtTable.Rows.Count - 1
                    If DtTable.Columns(ColIndex).ColumnName.Contains("Date") Then
                        If AgL.PubServerName = "" Then
                            ColumnValues(I, 0) = AgL.XNull(AgL.RetDate(DtTable.Rows(I)(ColIndex)))
                        Else
                            ColumnValues(I, 0) = CDate(AgL.XNull(DtTable.Rows(I)(ColIndex))).ToString("dd/MMM/yyyy")
                        End If
                    Else
                        'ColumnValues(I, 0) = AgL.XNull(DtTable.Rows(I)(ColIndex)).ToString().Replace("(", "").Replace(")", "")
                        ColumnValues(I, 0) = FRemoveSpecialCharactersForGSTReturns(AgL.XNull(DtTable.Rows(I)(ColIndex)))
                    End If
                Next
                xlWorkSheet.Range(GetExcelColumnName(ColIndex + 1) + (5).ToString + ":" + GetExcelColumnName(ColIndex + 1) + (5 + DtTable.Rows.Count - 1).ToString).Value = ColumnValues
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub
    Private Function GetExcelColumnName(columnNumber As Integer) As String
        Dim dividend As Integer = columnNumber
        Dim columnName As String = String.Empty
        Dim modulo As Integer

        While dividend > 0
            modulo = (dividend - 1) Mod 26
            columnName = Convert.ToChar(65 + modulo).ToString() & columnName
            dividend = CInt((dividend - modulo) / 26)
        End While

        Return columnName
    End Function
    Public Sub FGetGST3BReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Dim mCondStr As String = ""
        Dim mCondStrITC As String = ""
        Dim bFormatReport As Boolean = False
        Dim mQry3_1 As String = ""
        Dim mQry3_1_Total As String = ""

        Dim mQry3_2 As String = ""
        Dim mQry3_2_Total As String = ""

        Dim mQry4_A As String = ""
        Dim mQry4_B As String = ""
        Dim mQry4_C As String = ""
        Dim mQry4_D As String = ""

        Dim mQry5 As String = ""
        Dim mQry5_1 As String = ""


        Try

            RepTitle = "GST Reports"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mGridRow.Cells("Search Code").Value Is Nothing Or mGridRow.Cells("Search Code").Value = "" Then
                        Exit Sub
                    Else
                        If mGridRow.Cells("Search Code").Value = OutwardTaxableSuppliesOtherThanZero Or
                            mGridRow.Cells("Search Code").Value = OutwardTaxableSuppliesZeroRated Or
                            mGridRow.Cells("Search Code").Value = OutwardTaxableSuppliesNillRated Or
                            mGridRow.Cells("Search Code").Value = InwardSuppliesLiableToReverseCharge Or
                            mGridRow.Cells("Search Code").Value = InterStateSuppliesToUnRegesteredPerson Or
                            mGridRow.Cells("Search Code").Value = InterStateSuppliesToCompositionPerson Or
                            mGridRow.Cells("Search Code").Value = InterStateSuppliesToUINholders Or
                            mGridRow.Cells("Search Code").Value = InwardSuppliesLiableToReverseChargeOtherThan1And2 Or
                            mGridRow.Cells("Search Code").Value = IntraStateExcemptAndNillRatedSupply Or
                            mGridRow.Cells("Search Code").Value = IntraStateNonGSTSupplies Or
                            mGridRow.Cells("Search Code").Value = AllOtherITC Or
                            mGridRow.Cells("Search Code").Value = OutwardTaxableSuppliesOtherThanZeroVTypeWiseSummary Or
                            mGridRow.Cells("Search Code").Value = AllOtherITCVTypeWiseSummary Then
                            mFilterGrid.Item(GFilter, rowNextFormat).Value = mGridRow.Cells("Search Code").Value
                        Else
                            ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                            ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                            Exit Sub
                        End If
                    End If
                Else
                    Exit Sub
                End If
            End If


            mCondStr = " Where 1=1"
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Div_Code", rowDivision)
            mCondStr = mCondStr & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "
            mCondStr = mCondStr & " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "',
                                        '" & Ncat.DebitNoteSupplier & "','" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "','" & Ncat.CreditNoteSupplier & "',
                                        '" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "', '" & Ncat.ExpenseVoucher & "', '" & Ncat.IncomeVoucher & "', '" & Ncat.ReverseCharge & "', '" & Ncat.JobInvoice & "')"
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "




            mCondStrITC = " Where 1=1"
            mCondStrITC = mCondStrITC & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            'mCondStrITC = mCondStrITC & ReportFrm.GetWhereCondition("H.Div_Code", rowDivision)
            mCondStrITC = mCondStrITC & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "
            mCondStrITC = mCondStrITC & " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "',
                                        '" & Ncat.DebitNoteSupplier & "','" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "','" & Ncat.CreditNoteSupplier & "',
                                        '" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "', '" & Ncat.ExpenseVoucher & "', '" & Ncat.IncomeVoucher & "', '" & Ncat.ReverseCharge & "', '" & Ncat.JobInvoice & "')"
            mCondStrITC = mCondStrITC & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "


            If ReportFrm.FGetText(rowNextFormat) <> "" And ReportFrm.FGetText(rowNextFormat) IsNot Nothing Then
                Select Case ReportFrm.FGetText(rowNextFormat)
                    Case OutwardTaxableSuppliesOtherThanZero
                        mQry = "Select H.DocId As SearchCode, strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, 
                                Max(H.PartyName) As PartyName, Max(H.PartySalesTaxNo) As PartyGstNo, Max(H.VoucherNo) As VoucherNo, 
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetOutwardSuppliesOtherThanZeroRatedQry(mCondStr) + ") As H 
                                Where H.VoucherType = '" & mGridRow.Cells("Voucher Type").Value & "'
                                Group By H.DocID "
                    Case OutwardTaxableSuppliesZeroRated
                        mQry = "Select H.DocId As SearchCode, strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, 
                                Max(H.PartyName) As PartyName, Max(H.PartySalesTaxNo) As PartyGstNo, Max(H.VoucherNo) As VoucherNo, 
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetOutwardSuppliesZeroRatedQry(mCondStr) + ") As H 
                                Group By H.DocID "
                    Case OutwardTaxableSuppliesNillRated
                        mQry = "Select H.DocId As SearchCode, strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, 
                                Max(H.PartyName) As PartyName, Max(H.PartySalesTaxNo) As PartyGstNo, Max(H.VoucherNo) As VoucherNo, 
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetOtherOutwardSuppliesNilRated(mCondStr) + ") As H 
                                Group By H.DocID "
                    Case InwardSuppliesLiableToReverseCharge
                        mQry = "Select H.DocId As SearchCode, strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, 
                                Max(H.VoucherNo) As VoucherNo, 
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetInwardSuppliesLiableToReverseCharge(mCondStr) + ") As H 
                                Group By H.DocID "
                    Case InterStateSuppliesToUnRegesteredPerson
                        mQry = " Select H.DocId As SearchCode, Max(H.PlaceOfSupply) As PlaceOfSupply,
                            strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, Max(H.VoucherNo) As VoucherNo, 
                            Max(H.PartyName) As PartyName, 
                            Sum(H.Taxablevalue_Unregistered) As TaxableValue, 
                            Sum(H.IntegratedTax_Unregistered) As TaxAmount 
                            From (" & FGetInterStateSuppliesToUnRegAndComp(mCondStr) & ") As H 
                            Group By H.DocID 
                            Having Sum(H.Taxablevalue_Unregistered) > 0 "
                    Case InterStateSuppliesToCompositionPerson
                        mQry = " Select H.DocId As SearchCode, Max(H.PlaceOfSupply) As PlaceOfSupply,
                            strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, Max(H.VoucherNo) As VoucherNo, 
                            Max(H.PartyName) As PartyName, Max(H.PartySalesTaxNo) As PartyGstNo, 
                            Sum(H.Taxablevalue_Composition) As TaxableValue, 
                            Sum(H.IntegratedTax_Composition) As TaxAmount 
                            From (" & FGetInterStateSuppliesToUnRegAndComp(mCondStr) & ") As H 
                            Group By H.DocID 
                            Having Sum(H.Taxablevalue_Composition) > 0 "
                    Case InwardSuppliesLiableToReverseChargeOtherThan1And2
                        mQry = "Select H.DocId As SearchCode, strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, 
                                Max(H.VoucherNo) As VoucherNo, 
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetInwardsuppliesliableToReverseChargeOtherThen1And2(mCondStrITC) + ") As H 
                                Group By H.DocID "
                    Case InterStateSuppliesToUINholders
                        mQry = " Select H.DocId As SearchCode, Max(H.PlaceOfSupply) As PlaceOfSupply,
                            strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, Max(H.VoucherNo) As VoucherNo, 
                            Max(H.PartyName) As PartyName, 
                            Sum(H.Taxablevalue_UINholders) As TaxableValue, 
                            Sum(H.IntegratedTax_UINholders) As TaxAmount 
                            From (" & FGetInterStateSuppliesToUnRegAndComp(mCondStr) & ") As H 
                            Group By H.DocID 
                            Having Sum(H.Taxablevalue_UINholders) > 0 "
                    Case AllOtherITC
                        mQry = "Select H.DocId As SearchCode, strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, 
                                Max(H.VoucherNo) As VoucherNo, 
                                Max(H.PartyDocNo) As PartyDocNo, strftime('%d/%m/%Y', Max(H.PartyDocDate)) As PartyDocDate, 
                                Max(H.PartyName) As PartyName, Max(H.PartySalesTaxNo) As PartyGstNo, 
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetAllOtherITC(mCondStrITC) + ") As H 
                                Where H.VoucherType = '" & mGridRow.Cells("Voucher Type").Value & "'
                                Group By H.DocID "
                    Case OutwardTaxableSuppliesOtherThanZeroVTypeWiseSummary
                        mQry = "Select '" & OutwardTaxableSuppliesOtherThanZero & "' As SearchCode, H.VoucherType, 
                                Count(Distinct H.DocId) As VoucherCount,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetOutwardSuppliesOtherThanZeroRatedQry(mCondStr) + ") As H 
                                Group By H.VoucherType "
                    Case AllOtherITCVTypeWiseSummary
                        mQry = "Select '" & AllOtherITC & "' As SearchCode, H.VoucherType, 
                                Count(Distinct H.DocId) As VoucherCount,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetAllOtherITC(mCondStrITC) + ") As H 
                                Group By H.VoucherType "
                    Case IntraStateExcemptAndNillRatedSupply
                        mQry = "Select H.DocId As SearchCode, strftime('%d/%m/%Y', Max(H.VoucherDate)) As VoucherDate, Max(H.VoucherType) As VoucherType, 
                                Max(H.VoucherNo) As VoucherNo, 
                                Max(H.PartyDocNo) As PartyDocNo, strftime('%d/%m/%Y', Max(H.PartyDocDate)) As PartyDocDate, 
                                Max(H.PartyName) As PartyName, Max(H.PartySalesTaxNo) As PartyGstNo, 
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetCompositionExcemptedAndNillRated(mCondStr) + ") As H 
                                Group By H.DocID "
                    Case IntraStateNonGSTSupplies
                        mQry = "Select '" & IntraStateNonGSTSupplies & "' As SearchCode, H.VoucherType, 
                                Count(Distinct H.DocId) As VoucherCount,
                                Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTax) As IntegratedTax, 
                                Sum(H.CentralTax) As CentralTax, Sum(H.StateTax) As StateTax, Sum(H.Cess) As Cess,
                                Sum(H.TaxAmount) As TotalTaxAmount
                                From (" + FGetNonGSTSupplies(mCondStr) + ") As H 
                                Group By H.VoucherType "
                End Select
                ReportFrm.Text = "GST Report" + " (" + ReportFrm.FGetText(rowReportType) + "-" + ReportFrm.FGetText(rowNextFormat).ToString.Replace("/", "-") + ")"
                bFormatReport = False
                ReportFrm.DGL2.Visible = True
                ReportFrm.IsAllowFind = True
                ReportFrm.MnuVisible.Visible = True
                ReportFrm.MnuSort.Visible = True
                ReportFrm.MnuFilter.Visible = True
            Else
                mQry3_1 = mQry3_1 + " Select '" & OutwardTaxableSuppliesOtherThanZeroVTypeWiseSummary & "' As SearchCode, '(a)' As TableNo, 
                        '" & OutwardTaxableSuppliesOtherThanZero & "' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetOutwardSuppliesOtherThanZeroRatedQry(mCondStr) & ") As H "

                mQry3_1 = mQry3_1 + "UNION ALL "

                mQry3_1 = mQry3_1 + " Select '" & OutwardTaxableSuppliesZeroRated & "' As SearchCode, '(b)' As TableNo, 
                        '" & OutwardTaxableSuppliesZeroRated & "' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetOutwardSuppliesZeroRatedQry(mCondStr) & ") As H "

                mQry3_1 = mQry3_1 + "UNION ALL "

                mQry3_1 = mQry3_1 + " Select '" & OutwardTaxableSuppliesNillRated & "' As SearchCode, '(c)' As TableNo, 
                        'Other Outward Taxable  supplies (Nil rated, exempted)' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetOtherOutwardSuppliesNilRated(mCondStr) & ") As H "

                mQry3_1 = mQry3_1 + "UNION ALL "

                mQry3_1 = mQry3_1 + " Select '" & InwardSuppliesLiableToReverseCharge & "' As SearchCode, '(d)' As TableNo, 
                        '" & InwardSuppliesLiableToReverseCharge & "' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetInwardSuppliesLiableToReverseCharge(mCondStr) & ") As H "

                mQry3_1 = mQry3_1 + "UNION ALL "

                mQry3_1 = mQry3_1 + " Select '' As SearchCode, '(e)' As TableNo, 
                        'Non-GST Outward supplies' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetNonGSTOutwardSupplies(mCondStr) & ") As H "

                mQry3_1_Total = " Select '' As SearchCode, '3.1' As TableNo, 
                    'Details of Outward Supplies and inward supplies liable to reverse charge' As Particulars,
                    Sum(H.TaxableValue) As TaxableValue, Sum(IfNull(H.TaxAmount,0)) As TaxAmount 
                    From (" & mQry3_1 & ") As H "


                mQry3_2 = " Select '" & InterStateSuppliesToUnRegesteredPerson & "' As SearchCode, '' As TableNo, 
                        '" & InterStateSuppliesToUnRegesteredPerson & "' As Particulars,
                        Sum(H.Taxablevalue_Unregistered) As TaxableValue, 
                        Sum(H.IntegratedTax_Unregistered) As TaxAmount 
                        From (" & FGetInterStateSuppliesToUnRegAndComp(mCondStr) & ") As H "

                mQry3_2 = mQry3_2 + "UNION ALL "

                mQry3_2 = mQry3_2 + " Select '" & InterStateSuppliesToCompositionPerson & "' As SearchCode, '' As TableNo, 
                        '" & InterStateSuppliesToCompositionPerson & "' As Particulars,
                        Sum(H.Taxablevalue_Composition) As TaxableValue, 
                        Sum(H.IntegratedTax_Composition) As TaxAmount 
                        From (" & FGetInterStateSuppliesToUnRegAndComp(mCondStr) & ") As H "

                mQry3_2 = mQry3_2 + "UNION ALL "

                mQry3_2 = mQry3_2 + " Select '" & InterStateSuppliesToUINholders & "' As SearchCode, '' As TableNo, 
                        '" & InterStateSuppliesToUINholders & "' As Particulars,
                        Sum(H.Taxablevalue_UINholders) As TaxableValue, 
                        Sum(H.IntegratedTax_UINholders) As TaxAmount 
                        From (" & FGetInterStateSuppliesToUnRegAndComp(mCondStr) & ") As H "


                mQry3_2_Total = " Select '' As SearchCode, '3.2' As TableNo, 
                    'Of the supplies shown in 3.1 (a), details of inter-state supplies made to unregistered persons, composition taxable person and UIN holders' As Particulars,
                    Sum(H.TaxableValue) As TaxableValue, Sum(IfNull(H.TaxAmount,0)) As TaxAmount 
                    From (" & mQry3_2 & ") As H "


                mQry4_A = mQry4_A + " Select '' As SearchCode, '4' As TableNo, 
                        'Eligible ITC' As Particulars,
                        Null As TaxableValue, Null As TaxAmount  "

                mQry4_A = mQry4_A + "UNION ALL "

                mQry4_A = mQry4_A + " Select '' As SearchCode, '(A)' As TableNo, 
                        'ITC Available (Whether in full or part)' As Particulars,
                        Null As TaxableValue, Null As TaxAmount  "

                mQry4_A = mQry4_A + "UNION ALL "

                mQry4_A = mQry4_A + " Select '' As SearchCode, '(1)' As TableNo, 
                        'Import of goods ' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetImportOfGoods(mCondStr) & ") As H "

                mQry4_A = mQry4_A + "UNION ALL "

                mQry4_A = mQry4_A + " Select '' As SearchCode, '(2)' As TableNo, 
                        'Import of services' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetImportOfServices(mCondStr) & ") As H "

                mQry4_A = mQry4_A + "UNION ALL "

                mQry4_A = mQry4_A + " Select '" & InwardSuppliesLiableToReverseChargeOtherThan1And2 & "'  As SearchCode, '(3)' As TableNo, 
                        'Inward supplies liable To reverse charge(other than 1 & 2 above)' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetInwardsuppliesliableToReverseChargeOtherThen1And2(mCondStrITC) & ") As H "

                mQry4_A = mQry4_A + "UNION ALL "

                mQry4_A = mQry4_A + " Select '' As SearchCode, '(4)' As TableNo, 
                        'Inward supplies from ISD' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetInwardSuppliesFromISD(mCondStr) & ") As H "

                mQry4_A = mQry4_A + "UNION ALL "

                mQry4_A = mQry4_A + " Select '" & AllOtherITCVTypeWiseSummary & "' As SearchCode, '(5)' As TableNo, 
                        '" & AllOtherITC & "' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetAllOtherITC(mCondStrITC) & ") As H "



                mQry4_B = mQry4_B + " Select '' As SearchCode, '(B)' As TableNo, 
                        'ITC Reserved' As Particulars,
                        Null As TaxableValue, Null As TaxAmount  "

                mQry4_B = mQry4_B + "UNION ALL "

                mQry4_B = mQry4_B + " Select '' As SearchCode, '(1)' As TableNo, 
                        'As per Rule 42 & 43 of SGST/CGST rules' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetITCReservedAsPerRule42And43(mCondStrITC) & ") As H "

                mQry4_B = mQry4_B + "UNION ALL "

                mQry4_B = mQry4_B + " Select '' As SearchCode, '(2)' As TableNo, 
                        'Others' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetITCReservedOthers(mCondStrITC) & ") As H "

                mQry4_C = " Select '' As SearchCode, '(C)' As TableNo,  
                        'Net ITC Available (A)-(B)' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & mQry4_A + " UNION ALL " + mQry4_B & ") As H "


                mQry4_D = " Select '' As SearchCode, '(D)' As TableNo, 
                        'Ineligible ITC' As Particulars,
                        Null As TaxableValue, Null As TaxAmount  "

                mQry4_D = mQry4_D + "UNION ALL "

                mQry4_D = mQry4_D + " Select '' As SearchCode, '(1)' As TableNo, 
                        'As per Rule 42 & 43 of SGST/CGST rules' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetIneligibleITCAsPerSection17(mCondStrITC) & ") As H "

                mQry4_D = mQry4_D + "UNION ALL "

                mQry4_D = mQry4_D + " Select '' As SearchCode, '(2)' As TableNo, 
                        'Others' As Particulars,
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetIneligibleITCOthers(mCondStr) & ") As H "


                mQry5 = " Select '' As SearchCode, '5' As TableNo, 
                        'Values of exempt, Nil-rated and non-GST inward supplies' As Particulars,
                        Null As TaxableValue, Null As TaxAmount  "

                mQry5 = mQry5 + "UNION ALL "

                mQry5 = mQry5 + " Select '' As SearchCode, '' As TableNo, 
                        'Inter-State supplies' As Particulars,
                        Null As TaxableValue, Null As TaxAmount  "

                mQry5 = mQry5 + "UNION ALL "

                mQry5 = mQry5 + " Select '' As SearchCode, '' As TableNo, 
                        '   From a supplier under composition scheme, Exempt  and Nil rated supply',
                        Sum(H.Taxablevalue_InterState) As TaxableValue, Sum(IfNull(TaxAmount_InterState,0)) As TaxAmount 
                        From (" & FGetCompositionExcemptedAndNillRated(mCondStr) & ") As H "

                mQry5 = mQry5 + "UNION ALL "

                mQry5 = mQry5 + " Select '' As SearchCode, '' As TableNo, 
                        '   Non GST supply',
                        Sum(H.Taxablevalue_InterState) As TaxableValue, Sum(IfNull(TaxAmount_InterState,0)) As TaxAmount 
                        From (" & FGetNonGSTSupplies(mCondStr) & ") As H "

                mQry5 = mQry5 + "UNION ALL "

                mQry5 = mQry5 + " Select '' As SearchCode, '' As TableNo, 
                        'Intra-state supplies' As Particulars,
                        Null As TaxableValue, Null As TaxAmount  "

                mQry5 = mQry5 + "UNION ALL "

                mQry5 = mQry5 + " Select '" & IntraStateExcemptAndNillRatedSupply & "' As SearchCode, '' As TableNo, 
                        '   From a supplier under composition scheme, Exempt  and Nil rated supply',
                        Sum(H.Taxablevalue_IntraState) As TaxableValue, Sum(IfNull(H.TaxAmount_IntraState,0)) As TaxAmount 
                        From (" & FGetCompositionExcemptedAndNillRated(mCondStr) & ") As H "

                mQry5 = mQry5 + "UNION ALL "

                mQry5 = mQry5 + " Select '" & IntraStateNonGSTSupplies & "' As SearchCode, '' As TableNo, 
                        '   Non GST supply',
                        Sum(H.Taxablevalue_IntraState) As TaxableValue, Sum(IfNull(H.TaxAmount_IntraState,0)) As TaxAmount 
                        From (" & FGetNonGSTSupplies(mCondStr) & ") As H "


                mQry5_1 = " Select '' As SearchCode, '5.1' As TableNo, 
                        'Interest & late fee payable' As Particulars,
                        Null As TaxableValue, Null As TaxAmount  "

                mQry5_1 = mQry5_1 + "UNION ALL "

                mQry5_1 = mQry5_1 + " Select '' As SearchCode, '' As TableNo, 
                        'Intrest',
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetIntrestQry(mCondStr) & ") As H "

                mQry5_1 = mQry5_1 + "UNION ALL "

                mQry5_1 = mQry5_1 + " Select '' As SearchCode, '' As TableNo, 
                        'Late Fees',
                        Sum(H.TaxableValue) As TaxableValue, Sum(H.TaxAmount) As TaxAmount 
                        From (" & FGetLateFeesQry(mCondStr) & ") As H "


                mQry = mQry3_1_Total + " UNION ALL " +
                        mQry3_1 + " UNION ALL " +
                        mQry3_2_Total + " UNION ALL " +
                        mQry3_2 + " UNION ALL " +
                        mQry4_A + " UNION ALL " +
                        mQry4_B + " UNION ALL " +
                        mQry4_C + " UNION ALL " +
                        mQry4_D + " UNION ALL " +
                        mQry5 + " UNION ALL " +
                        mQry5_1

                ReportFrm.Text = "GST Report" + " (" + ReportFrm.FGetText(rowReportType) + ")"

                bFormatReport = True
                ReportFrm.DGL2.Visible = False
                ReportFrm.IsAllowFind = False
                ReportFrm.MnuVisible.Visible = False
                ReportFrm.MnuSort.Visible = False
                ReportFrm.MnuFilter.Visible = False
            End If
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            mQry = "Select 'Create Excel File' As MenuText, 'FCreateGST3BExcelFile' As FunctionName"
            mQry += " UNION ALL "
            mQry += " Select 'Print' As MenuText, 'FCreateGST3BPrint' As FunctionName"

            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcGSTReports"
            ReportFrm.DTCustomMenus = DtMenuList
            ReportFrm.IsHideZeroColumns = False





            ReportFrm.ProcFillGrid(DsHeader)

            If bFormatReport = True Then
                For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                    ReportFrm.DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                Next I

                ReportFrm.DGL1.Rows(0).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(6).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(10).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(11).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(17).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(20).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(21).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(24).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(25).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(28).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
                ReportFrm.DGL1.Rows(31).DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
            End If




        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Private Function FDataValidationFor3B() As Boolean
        Dim mCondStr As String = ""

        mCondStr = " Where 1=1"
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
        'mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
        mCondStr = mCondStr & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & Ncat.ReverseCharge & "')"
        mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
        mCondStr = mCondStr & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "

        mQry = FGetInwardSuppliesLiableToReverseCharge(mCondStr)
        Dim DtReverseChargeEntryDone As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtReverseChargeEntryDone.Rows.Count = 0 Then
            mCondStr = " Where 1=1"
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            'mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "
            mCondStr = mCondStr & " And Vt.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.ExpenseVoucher & "')"
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "

            mQry = " SELECT L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, 
                        H.VendorName As PartyName, H.ManualRefNo As VoucherNo, 
                        L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        left join PurchInvoiceDetail L On H.DocID = L.DocID 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr &
                        " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "' "
            Dim DtReverseChargeLiable As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtReverseChargeLiable.Rows.Count > 0 Then
                If MsgBox("Reverse Charge Liable Entries found in selected period, but you have not done reverse charge entry.Do you want to continue ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    FDataValidationFor3B = False
                    Exit Function
                End If
            End If
        End If
        FDataValidationFor3B = True
    End Function
    Public Sub FCreateGST3BPrint(DGL As AgControls.AgDataGrid)
        Dim DtTable As DataTable = Nothing
        Dim SubTitle$ = ""
        Dim GroupHeaderTitle1$ = "", GroupHeaderTitle2$ = ""
        Dim IsReturn As Integer = 0
        Dim AssessmentYear$ = ""
        Dim mCondStr$ = ""
        Dim mCondStrITC As String = ""
        Dim mMainQry As String = ""

        If FDataValidationFor3B() = False Then Exit Sub

        Dim ToDate As DateTime = ReportFrm.FGetText(rowToDate)
        Dim newdate = String.Format("{0:yyyy-MM-dd}", ToDate)
        Dim MonthName As String = AgL.XNull(AgL.Dman_Execute(" select case strftime('%m', '" & newdate & "') when '01' then 'January' when '02' then 'Febuary' when '03' then 'March' 
                    when '04' then 'April' when '05' then 'May' when '06' then 'June' when '07' then 'July' 
                    when '08' then 'August' when '09' then 'September' when '10' then 'October' when '11' then 'November' 
                    when '12' then 'December' else '' end as month ", AgL.GCn).ExecuteScalar)
        mMainQry = " Select "

        Try
            mCondStr = " Where 1=1"
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            'mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "
            mCondStr = mCondStr & " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "',
                                        '" & Ncat.DebitNoteSupplier & "','" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "','" & Ncat.CreditNoteSupplier & "',
                                        '" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "', '" & Ncat.ExpenseVoucher & "', '" & Ncat.IncomeVoucher & "', '" & Ncat.ReverseCharge & "', '" & Ncat.JobInvoice & "')"
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "
            mCondStr = mCondStr & " And Vt.Nature In ('Invoice','Return') "


            mCondStrITC = " Where 1=1"
            mCondStrITC = mCondStrITC & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            'mCondStrITC = mCondStrITC & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStrITC = mCondStrITC & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "
            mCondStrITC = mCondStrITC & " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "',
                                        '" & Ncat.DebitNoteSupplier & "','" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "','" & Ncat.CreditNoteSupplier & "',
                                        '" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "', '" & Ncat.ExpenseVoucher & "', '" & Ncat.IncomeVoucher & "', '" & Ncat.ReverseCharge & "', '" & Ncat.JobInvoice & "')"
            mCondStrITC = mCondStrITC & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "


            'For GSTIN, Legal Name of the registered person
            mQry = " Select Sg.DispName As Name
                From Division D
                LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                LEFT JOIN City C On Sg.CityCode = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                Where D.Div_Code =  '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "'"
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.Chk_Text(GetSalesTaxNo) + " As CompanySalesTaxNo, "
                mMainQry += AgL.Chk_Text(DtTable.Rows(0)("Name")) + " As CompanyName, "
            End If

            'For Year
            Dim bYear As String = AgL.XNull(AgL.Dman_Execute(" Select cyear From Company Where Comp_Code = '" & AgL.PubCompCode & "' ", AgL.GCn).ExecuteScalar)
            mMainQry += AgL.Chk_Text(bYear) + " As Year, "

            'Month	
            mMainQry += AgL.Chk_Text(MonthName) + " As MonthName, "

            '3.1 (a) Outward Taxable  supplies  (other than zero rated, nil rated and exempted)
            'Sales Amount And Tax On It (Both Local And Central Combined)
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue, Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.CentralTax) as CentralTax, Sum(H.StateTax) as StateTax, Sum(H.Cess)  As Cess
                    From (" & FGetOutwardSuppliesOtherThanZeroRatedQry(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("TotalTaxablevalue")).ToString + " As OutTaxableSuppOtherThanZeroTotalTaxablevalue, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As OutTaxableSuppOtherThanZeroIntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("CentralTax")).ToString + " As OutTaxableSuppOtherThanZeroCentralTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As OutTaxableSuppOtherThanZeroCess, "
            End If

            '3.1 (b) Outward Taxable  supplies  (zero rated )
            'Export Sales (Both on Bond Without Bond)
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue, Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.Cess)  As Cess
                    From (" & FGetOutwardSuppliesZeroRatedQry(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("TotalTaxablevalue")).ToString + " As OutTaxableSuppZeroTotalTaxablevalue, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As OutTaxableSuppZeroIntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As OutTaxableSuppZeroCess, "
            End If

            '3.1 (c) Other Outward Taxable  supplies (Nil rated, exempted)
            'Goods Covered in Excemtion Notification & Goods Having rate 0%
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue
                    From (" & FGetOtherOutwardSuppliesNilRated(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("TotalTaxablevalue")).ToString + " As OutTaxableSuppNillTotalTaxablevalue, "
            End If

            '3.1 (d) Inward supplies (liable to reverse charge) 
            'Tax to be Paid on reverse charge.
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue, Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.CentralTax) as CentralTax, Sum(H.StateTax) as StateTax, Sum(H.Cess)  As Cess
                    From (" & FGetInwardSuppliesLiableToReverseCharge(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("TotalTaxablevalue")).ToString + " As InwardSuppliesReverseChargeTotalTaxablevalue, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As InwardSuppliesReverseChargeIntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("CentralTax")).ToString + " As InwardSuppliesReverseChargeCentralTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("StateTax")).ToString + " As InwardSuppliesReverseChargeStateTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As InwardSuppliesReverseChargeCess, "
            End If

            '3.1 (e) Non-GST Outward supplies
            'Goods not covered in GST, Like Diesel
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue
                    From (" & FGetNonGSTOutwardSupplies(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("TotalTaxablevalue")).ToString + " As NonGstOutwardTotalTaxablevalue, "
            End If


            '3.2  Of the supplies shown in 3.1 (a), details of inter-state supplies made to unregistered persons, composition taxable person and UIN holders						
            'Suppliers Made to UnRegistered Person : Only InterState Sales to Unregistered
            'Suppliers Made to Composition Taxable Person : Only InterState Sales to Composition Dealer
            'Suppliers Made to UiN Holders : Only InterState Sales to UIN Holders like Embassy
            mQry = " SELECT H.PlaceOfSupply,
                    Sum(H.Taxablevalue_Unregistered) As TotalTaxablevalue_Unregistered,
                    Sum(H.IntegratedTax_Unregistered) As AmountOfIntegratedTax_Unregistered,
                    Sum(H.Taxablevalue_Composition) As TotalTaxablevalue_Composition,
                    Sum(H.IntegratedTax_Composition) As AmountOfIntegratedTax_Composition,
                    Sum(H.Taxablevalue_UINholders) As TotalTaxablevalue_UINholders,
                    Sum(H.IntegratedTax_UINholders) As AmountOfIntegratedTax_UINholders
                    From (" & FGetInterStateSuppliesToUnRegAndComp(mCondStr) & ") As H
                    Group By H.PlaceOfSupply 
                    Having Sum(H.Taxablevalue_Unregistered) > 0 Or 
                    Sum(H.IntegratedTax_Unregistered) > 0 Or 
                    Sum(H.Taxablevalue_Composition) > 0 Or 
                    Sum(H.IntegratedTax_Composition) > 0 Or 
                    Sum(H.Taxablevalue_UINholders) > 0 Or 
                    Sum(H.IntegratedTax_UINholders) > 0 "
            Dim DtInterStateSuppToUnReg As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)




            '4. Eligible ITC	(1)   Import of goods 
            'Tax Charged on Import of Goods liKe IGST
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, Sum(H.Cess)  As Cess
                    From (" & FGetImportOfGoods(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As ImportOfGoodsIntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As ImportOfGoodsCess, "
            End If

            '4. Eligible ITC	(2)   Import of services
            'Tax paid on Import of service (Covered under Reverse Charge) 
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, Sum(H.Cess)  As Cess
                    From (" & FGetImportOfServices(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As ImportOfServiceIntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As ImportOfServiceCess, "
            End If

            '4. Eligible ITC	(3)   Inward supplies liable to reverse charge        (other than 1 &2 above)
            'All Other purchase from unregistered Dealer (Local Purchase)
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, Sum(H.CentralTax) as CentralTax, Sum(H.StateTax) as StateTax, Sum(H.Cess)  As Cess
                    From (" & FGetInwardsuppliesliableToReverseChargeOtherThen1And2(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As InwardSuppliesReverseChargeOtherThan1And2IntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("CentralTax")).ToString + " As InwardSuppliesReverseChargeOtherThan1And2CentralTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("StateTax")).ToString + " As InwardSuppliesReverseChargeOtherThan1And2StateTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As InwardSuppliesReverseChargeOtherThan1And2Cess, "
            End If

            '4. Eligible ITC	(4)   Inward supplies from ISD
            'Input from other Branches (Input Service Distributors)
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.CentralTax) as CentralTax, Sum(H.StateTax) as StateTax, Sum(H.Cess)  As Cess
                    From (" & FGetInwardSuppliesFromISD(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As ISDIntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("CentralTax")).ToString + " As ISDCentralTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As ISDCess, "
            End If

            '4. Eligible ITC	(5)   All other ITC
            'Normal Purchase from Registered Dealer
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.CentralTax) as CentralTax, Sum(H.StateTax) as StateTax, Sum(H.Cess)  As Cess
                    From (" & FGetAllOtherITC(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As AllOtherItcIntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("CentralTax")).ToString + " As AllOtherItcCentralTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As AllOtherItcCess, "
            End If


            '4. (D)  Ineligible ITC	(1)   As per section 17(5) of CGST//SGST Act
            mQry = " SELECT Round(Sum(H.IntegratedTax),2) As IntegratedTax,
                    Round(Sum(H.CentralTax),2) As CentralTax,
                    Round(Sum(H.StateTax),2) As StateTax,
                    Round(Sum(H.Cess),2) As Cess
                    From (" & FGetIneligibleITCAsPerSection17(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As IneligibleITCIntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("CentralTax")).ToString + " As IneligibleITCCentralTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("StateTax")).ToString + " As IneligibleITCStateTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As IneligibleITCCess, "
            End If


            '5. Values of exempt, From a supplier under composition scheme, Exempt  and Nil rated supply	
            'Purchase of Goods 0%, Exempted etc
            mQry = " Select Sum(H.Taxablevalue_InterState) As InterStatesupplies,
                    Sum(H.Taxablevalue_IntraState) As Intrastatesupplies
                    from (" & FGetCompositionExcemptedAndNillRated(mCondStr) & ")  As H"
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("InterStatesupplies")).ToString + " As ZeroRatedInterStatesupplies, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Intrastatesupplies")).ToString + " As ZeroRatedIntrastatesupplies, "
            End If

            '5. Values of exempt, Non GST supply	
            'Purchase of Goods not Covered on GST
            mQry = " Select Sum(H.Taxablevalue_InterState) As InterStatesupplies,
                    Sum(H.Taxablevalue_IntraState) As Intrastatesupplies
                    from (" & FGetNonGSTSupplies(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("InterStatesupplies")).ToString + " As NonGstInterStatesupplies, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Intrastatesupplies")).ToString + " As NonGstIntrastatesupplies, "
            End If


            '5.1 Interest & late fee payable	
            'Intrest @18% on late payment of tax
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax,
                    Sum(H.CentralTax) As CentralTax,
                    Sum(H.StateTax) As StateTax,
                    Sum(H.Cess) As Cess
                    From (" & FGetLateFeesQry(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                mMainQry += AgL.VNull(DtTable.Rows(0)("IntegratedTax")).ToString + " As LateFeeIntegratedTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("CentralTax")).ToString + " As LateFeeCentralTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("StateTax")).ToString + " As LateFeeStateTax, "
                mMainQry += AgL.VNull(DtTable.Rows(0)("Cess")).ToString + " As LateFeeCess, "
            End If

            mMainQry = mMainQry.Substring(1, mMainQry.Length - 3)

            Dim dsMain As DataTable
            Dim dsCompany As DataTable
            Dim mPrintTitle As String


            mPrintTitle = "GST 3B"


            dsMain = AgL.FillData(mMainQry, AgL.GCn).Tables(0)


            'FReplaceInvoiceVariables(dsMain, TxtDivision.Tag, TxtSite_Code.Tag)

            dsCompany = ClsMain.GetDocumentHeaderDataTable(Replace(ReportFrm.FGetCode(rowDivision), "'", ""), AgL.PubSiteCode, " ")

            Dim objRepPrint As New FrmRepPrint(AgL)

            objRepPrint.reportViewer1.Visible = True
            Dim id As Integer = 0
            objRepPrint.reportViewer1.ProcessingMode = ProcessingMode.Local

            If AgL.PubUserName.ToUpper = "SUPER" Then
                dsMain = ClsMain.RemoveNullFromDataTable(dsMain)
                dsCompany = ClsMain.RemoveNullFromDataTable(dsCompany)
                DtInterStateSuppToUnReg = ClsMain.RemoveNullFromDataTable(DtInterStateSuppToUnReg)
                dsMain.WriteXml(AgL.PubReportPath + "\Gst3B_DsMain.xml")
                dsCompany.WriteXml(AgL.PubReportPath + "\Gst3B_DsCompany.xml")
                DtInterStateSuppToUnReg.WriteXml(AgL.PubReportPath + "\Gst3B_DsInterState.xml")
            End If

            objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\GST3B.rdl"


            If (dsMain.Rows.Count = 0) Then
                MsgBox("No records found to print.")
            End If
            Dim rds As New ReportDataSource("DsMain", dsMain)
            Dim rdsCompany As New ReportDataSource("DsCompany", dsCompany)
            Dim rdsInterState As New ReportDataSource("DsInterState", DtInterStateSuppToUnReg)

            objRepPrint.reportViewer1.LocalReport.DataSources.Clear()
            objRepPrint.reportViewer1.LocalReport.DataSources.Add(rds)
            objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsCompany)
            objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsInterState)


            objRepPrint.reportViewer1.LocalReport.Refresh()
            objRepPrint.reportViewer1.RefreshReport()
            objRepPrint.MdiParent = ReportFrm.MdiParent
            objRepPrint.Show()

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Private Function FGetOutwardSuppliesOtherThanZeroRatedQry(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, H.SaleToPartyName As PartyName, H.SaleToPartySalesTaxNo As PartySalesTaxNo, H.ManualRefNo As VoucherNo, L.Taxable_Amount as TaxableValue, 
                        L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, L.Tax4 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from SaleInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join SaleInvoiceDetail L On H.DocID = L.DocID 
                        Left Join City On H.SaleToPartyCity = City.CityCode
                        Left Join State on City.State = State.Code " & mCondStr &
                        " And ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) <> 0
                        And IfNull(H.SalesTaxGroupRegType,'') <> 'SEZ' And IfNull(State.ManualCode,'') <> '00'
                        UNION ALL 
                        Select L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, H.PartyName As PartyName, H.PartySalesTaxNo, H.ManualRefNo As VoucherNo, L.Taxable_Amount As TaxableValue, 
                        L.Tax1 As IntegratedTax, L.Tax2 As CentralTax, L.Tax3 As StateTax, L.Tax4 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        From LedgerHead H 
                        LEFT JOIN LedgerHeadDetailCharges L On H.DocId = L.DocId 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                        Left Join City On H.PartyCity = City.CityCode
                        Left Join State on City.State = State.Code " & mCondStr &
                        " And IfNull(H.SalesTaxGroupRegType,'') <> 'SEZ'  And IfNull(State.ManualCode,'') <> '00' " &
                        " And H.V_Type In ('" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "') 
                        UNION ALL 
                        Select L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, H.PartyName As PartyName, H.PartySalesTaxNo, H.ManualRefNo As VoucherNo, -L.Taxable_Amount As TaxableValue, 
                        -L.Tax1 As IntegratedTax, -L.Tax2 As CentralTax, -L.Tax3 As StateTax, -L.Tax4 As Cess,
                        -(IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0)) As TaxAmount
                        From LedgerHead H 
                        Left Join LedgerHeadDetailCharges L On H.DocId = L.DocId 
                        Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type 
                        Left Join City On H.PartyCity = City.CityCode
                        Left Join State On City.State = State.Code " & mCondStr &
                        " And IfNull(H.SalesTaxGroupRegType,'') <> 'SEZ'  And IfNull(State.ManualCode,'') <> '00' " &
                        " And H.V_Type In ('" & Ncat.IncomeVoucher & "')"

        '" And Vt.NCat = '" & agConstants.Ncat.CreditNote & "' "
        Return mStrQry
    End Function

    Private Function FGetOutwardSuppliesZeroRatedQry(mCondStr As String) As String
        ' And ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) <> 0
        Dim mStrQry As String = " SELECT L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, H.SaleToPartyName As PartyName, H.SaleToPartySalesTaxNo As PartySalesTaxNo, H.ManualRefNo As VoucherNo, L.Taxable_Amount as TaxableValue, 
                        L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, L.Tax4 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from SaleInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join SaleInvoiceDetail L On H.DocID = L.DocID  
                        Left Join City On H.SaleToPartyCity = City.CityCode
                        Left Join State on City.State = State.Code " & mCondStr &
                        "
                        And ( IfNull(H.SalesTaxGroupRegType,'') = 'SEZ' Or IfNull(State.ManualCode,'') = '00')
                        UNION ALL 
                        Select L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, H.PartyName As PartyName, H.PartySalesTaxNo, H.ManualRefNo As VoucherNo, L.Taxable_Amount As TaxableValue, 
                        L.Tax1 As IntegratedTax, L.Tax2 As CentralTax, L.Tax3 As StateTax, L.Tax4 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        From LedgerHead H 
                        LEFT JOIN LedgerHeadDetailCharges L On H.DocId = L.DocId 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type  
                        Left Join City On H.PartyCity = City.CityCode
                        Left Join State on City.State = State.Code " & mCondStr &
                        " And (IfNull(H.SalesTaxGroupRegType,'') = 'SEZ' Or IfNull(State.ManualCode,'') = '00') " &
                        " And H.V_Type In ('" & Ncat.DebitNoteCustomer & "',
                        '" & Ncat.CreditNoteCustomer & "')"
        Return mStrQry
    End Function
    Private Function FGetOtherOutwardSuppliesNilRated(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, 
                    H.SaleToPartyName As PartyName, H.SaleToPartySalesTaxNo As PartySalesTaxNo, H.ManualRefNo As VoucherNo, 
                    ifnull(L.Taxable_Amount,0) as TaxableValue,
                    L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                    IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                    from SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type   
                    Left Join City On H.SaleToPartyCity = City.CityCode
                    Left Join State on City.State = State.Code " & mCondStr &
                    " And ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) = 0  And IfNull(State.ManualCode,'' ) <> '00' "
        Return mStrQry
    End Function
    Private Function FGetInwardSuppliesLiableToReverseCharge(mCondStr As String) As String
        'Dim mStrQry As String = " SELECT L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, 
        '                H.VendorName As PartyName, H.ManualRefNo As VoucherNo, 
        '                L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
        '                IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
        '                from PurchInvoice H 
        '                left join PurchInvoiceDetail L On H.DocID = L.DocID 
        '                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr &
        '                " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "' "

        Dim mStrQry As String = " SELECT L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, 
                    Sg.Name As PartyName, H.ManualRefNo As VoucherNo, 
                    Lc.Taxable_Amount as TaxableValue, Lc.Tax1 As IntegratedTax, Lc.Tax2 as CentralTax, Lc.Tax3 as StateTax, 0 As Cess,
                    IsNull(Lc.Tax1,0) + IsNull(Lc.Tax2,0) + IsNull(Lc.Tax3,0) + IsNull(Lc.Tax4,0) As TaxAmount
                    FROM LedgerHead H 
                    LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID
                    LEFT JOIN LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID AND L.Sr = Lc.Sr
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                    LEFT JOIN Subgroup Sg ON L.Subcode = Sg.Subcode " & mCondStr &
                    " And Vt.NCat = '" & Ncat.ReverseCharge & "'"
        Return mStrQry
    End Function
    Private Function FGetNonGSTOutwardSupplies(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from SaleInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join SaleInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetInterStateSuppliesToUnRegAndComp(mCondStr As String) As String
        Dim mStrQry As String = "  SELECT L.DocId, 
                    H.V_Date As VoucherDate, Vt.Description As VoucherType, H.SaleToPartyName As PartyName, H.SaleToPartySalesTaxNo As PartySalesTaxNo, H.ManualRefNo As VoucherNo, 
                    S.Description As PlaceOfSupply,
                    CASE when H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "' THEN L.Taxable_Amount Else 0 END As Taxablevalue_Unregistered,
                    CASE when H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "' THEN IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) Else 0 END As IntegratedTax_Unregistered,
                    CASE when H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Registered & "' And IfNull(H.SalesTaxGroupRegType,'') = 'COMPOSITION' THEN L.Taxable_Amount Else 0 END As Taxablevalue_Composition,
                    CASE when H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Registered & "' And IfNull(H.SalesTaxGroupRegType,'') = 'COMPOSITION' THEN IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) Else 0 END As IntegratedTax_Composition,
                    0 As Taxablevalue_UINholders,
                    0 As IntegratedTax_UINholders
                    From SaleInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    left join SaleInvoiceDetail L on H.DocID = L.DocID
                    Left join City C On H.SaleToPartyCity = C.CityCode
                    left join State S on C.State = S.Code " & mCondStr &
                    " And H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "' And IfNull(S.ManualCode,'') <> '00' "
        Return mStrQry
    End Function
    Private Function FGetImportOfGoods(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetImportOfServices(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetInwardsuppliesliableToReverseChargeOtherThen1And2(mCondStr As String) As String
        'Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
        '                IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
        '                from PurchInvoice H 
        '                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
        '                left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
        '                " And 1=2 "

        Dim mStrQry As String = " SELECT L.DocId, H.V_Date As VoucherDate, Vt.Description As VoucherType, 
                    Sg.Name As PartyName, H.ManualRefNo As VoucherNo, 
                    Lc.Taxable_Amount as TaxableValue, Lc.Tax1 As IntegratedTax, Lc.Tax2 as CentralTax, Lc.Tax3 as StateTax, 0 As Cess,
                    IsNull(Lc.Tax1,0) + IsNull(Lc.Tax2,0) + IsNull(Lc.Tax3,0) + IsNull(Lc.Tax4,0) As TaxAmount
                    FROM LedgerHead H 
                    LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID
                    LEFT JOIN LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID AND L.Sr = Lc.Sr
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                    LEFT JOIN Subgroup Sg ON L.Subcode = Sg.Subcode " & mCondStr &
                    " AND ifNull(Date(H.GstInputCreditDate),IfNull(Date(H.GstFilingDate), Date(H.V_date))) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & "                                    
                    And Vt.NCat = '" & Ncat.ReverseCharge & "'"
        Return mStrQry
    End Function
    Private Function FGetInwardSuppliesFromISD(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetAllOtherITC(mCondStr As String) As String
        Dim mStrQry As String = " Select L.DocId, 
                        H.V_Date As VoucherDate, Vt.Description As VoucherType, H.VendorName As PartyName, H.VendorSalesTaxNo As PartySalesTaxNo, H.ManualRefNo As VoucherNo, 
                        H.VendorDocNo As PartyDocNo, H.VendorDocDate As PartyDocDate,
                        L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 As CentralTax, L.Tax3 As StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        From PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        Left Join PurchInvoiceDetail L on H.DocID = L.DocID " & mCondStr &
                        " AND ifNull(Date(H.GstInputCreditDate),IfNull(Date(H.GstFilingDate),Date(H.V_date))) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " 
                        And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Registered & "' 
                        And ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) <> 0 
                        UNION ALL 
                        Select L.DocId, 
                        H.V_Date As VoucherDate, Vt.Description As VoucherType, H.PartyName As PartyName, H.PartySalesTaxNo, H.ManualRefNo As VoucherNo,
                        H.PartyDocNo As PartyDocNo, H.PartyDocDate As PartyDocDate,
                        L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 As CentralTax, L.Tax3 As StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        From LedgerHead H 
                        Left Join LedgerHeadDetailCharges L On H.DocId = L.DocId 
                        Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr &
                        " AND ifNull(Date(H.GstInputCreditDate),IfNull(Date(H.GstFilingDate),Date(H.V_date))) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & "                                    
                        And H.V_Type In ('" & Ncat.DebitNoteSupplier & "', 
                                '" & Ncat.CreditNoteSupplier & "', 
                                '" & Ncat.ExpenseVoucher & "')"

        '" And Vt.NCat = '" & agConstants.Ncat.DebitNote & "' "

        Return mStrQry
    End Function
    Private Function FGetITCReservedAsPerRule42And43(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetITCReservedOthers(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetIneligibleITCAsPerSection17(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetIneligibleITCOthers(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetCompositionExcemptedAndNillRated(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, 
                        CASE when H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "' THEN L.Taxable_Amount Else 0 END As Taxablevalue_InterState,
                        CASE when H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "' THEN IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) Else 0 END As TaxAmount_InterState,
                        CASE when H.PlaceOfSupply = '" & PlaceOfSupplay.WithinState & "' THEN L.Taxable_Amount Else 0 END As Taxablevalue_IntraState,
                        CASE when H.PlaceOfSupply = '" & PlaceOfSupplay.WithinState & "' THEN IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) Else 0 END As TaxAmount_IntraState,
                        H.V_Date As VoucherDate, Vt.Description As VoucherType, H.VendorName As PartyName, H.VendorSalesTaxNo As PartySalesTaxNo, H.ManualRefNo As VoucherNo, 
                        H.VendorDocNo As PartyDocNo, H.VendorDocDate As PartyDocDate,
                        L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 As CentralTax, L.Tax3 As StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        From PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And (L.SalesTaxGroupItem = 'GST 0%' Or L.SalesTaxGroupItem Is Null)  "
        Return mStrQry
    End Function
    Private Function FGetNonGSTSupplies(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, 
                        CASE when H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "' THEN L.Taxable_Amount Else 0 END As Taxablevalue_InterState,
                        CASE when H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "' THEN IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) Else 0 END As TaxAmount_InterState,
                        CASE when H.PlaceOfSupply = '" & PlaceOfSupplay.WithinState & "' THEN L.Taxable_Amount Else 0 END As Taxablevalue_IntraState,
                        CASE when H.PlaceOfSupply = '" & PlaceOfSupplay.WithinState & "' THEN IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) Else 0 END As TaxAmount_IntraState,
                        H.V_Date As VoucherDate, Vt.Description As VoucherType, H.VendorName As PartyName, H.VendorSalesTaxNo As PartySalesTaxNo, H.ManualRefNo As VoucherNo, 
                        H.VendorDocNo As PartyDocNo, H.VendorDocDate As PartyDocDate,
                        L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 As CentralTax, L.Tax3 As StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        From PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetIntrestQry(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Private Function FGetLateFeesQry(mCondStr As String) As String
        Dim mStrQry As String = " SELECT L.DocId, L.Taxable_Amount as TaxableValue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess,
                        IfNull(L.Tax1,0) + IfNull(L.Tax2,0) + IfNull(L.Tax3,0) + IfNull(L.Tax4,0) As TaxAmount
                        from PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        left join PurchInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And 1=2 "
        Return mStrQry
    End Function
    Public Sub FCreateGST3BExcelFile(DGL As AgControls.AgDataGrid)
        Dim DtTable As DataTable = Nothing
        Dim SubTitle$ = ""
        Dim GroupHeaderTitle1$ = "", GroupHeaderTitle2$ = ""
        Dim IsReturn As Integer = 0
        Dim AssessmentYear$ = ""
        Dim OutputFile As String = ""
        Dim mCondStr$ = ""
        Dim mCondStrITC$ = ""

        If FDataValidationFor3B() = False Then Exit Sub

        Dim ToDate As DateTime = ReportFrm.FGetText(rowToDate)
        Dim newdate = String.Format("{0:yyyy-MM-dd}", ToDate)
        Dim MonthName As String = AgL.XNull(AgL.Dman_Execute(" select case strftime('%m', '" & newdate & "') when '01' then 'January' when '02' then 'Febuary' when '03' then 'March' 
                    when '04' then 'April' when '05' then 'May' when '06' then 'June' when '07' then 'July' 
                    when '08' then 'August' when '09' then 'September' when '10' then 'October' when '11' then 'November' 
                    when '12' then 'December' else '' end as month ", AgL.GCn).ExecuteScalar)


        Dim FilePath As String = ""
        Dim SaveFileDialogBox As SaveFileDialog
        Dim sFilePath As String = ""
        SaveFileDialogBox = New SaveFileDialog

        SaveFileDialogBox.Title = "File Name"
        FilePath = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialogBox.InitialDirectory = FilePath
        SaveFileDialogBox.FilterIndex = 1
        SaveFileDialogBox.FileName = "GSTR3B_" + MonthName + ".xlsm"
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        OutputFile = SaveFileDialogBox.FileName


        Dim xlApp As Excel.Application
        Dim TemplateWorkBook As Excel.Workbook
        Dim OutputWorkBook As Excel.Workbook

        xlApp = New Excel.Application
        xlApp.AlertBeforeOverwriting = False
        xlApp.DisplayAlerts = False

        TemplateWorkBook = xlApp.Workbooks.Open(My.Application.Info.DirectoryPath + "\Templates\" + "GSTR3B_Excel_Utility_V3.0.xlsm")
        TemplateWorkBook.SaveAs(OutputFile)
        xlApp.Workbooks.Close()
        OutputWorkBook = xlApp.Workbooks.Open(OutputFile)

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = OutputWorkBook.Worksheets("GSTR-3B")

            mCondStr = " Where 1=1"
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            'mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "
            mCondStr = mCondStr & " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "',
                                        '" & Ncat.DebitNoteSupplier & "','" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "','" & Ncat.CreditNoteSupplier & "',
                                        '" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "', '" & Ncat.ExpenseVoucher & "', '" & Ncat.IncomeVoucher & "', '" & Ncat.ReverseCharge & "')"
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "
            mCondStr = mCondStr & " And Vt.Nature In ('Invoice','Return') "


            mCondStrITC = " Where 1=1"
            mCondStrITC = mCondStrITC & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            'mCondStrITC = mCondStrITC & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStrITC = mCondStrITC & " And H.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "' "
            mCondStrITC = mCondStrITC & " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "',
                                        '" & Ncat.DebitNoteSupplier & "','" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "','" & Ncat.CreditNoteSupplier & "',
                                        '" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "', '" & Ncat.ExpenseVoucher & "', '" & Ncat.IncomeVoucher & "', '" & Ncat.ReverseCharge & "')"
            mCondStrITC = mCondStrITC & " And CharIndex('" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "','+' || IfNull(Vt.VoucherTypeTags,'')) = 0 "

            'For GSTIN, Legal Name of the registered person
            mQry = " Select Sg.DispName As Name
                From Division D
                LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                LEFT JOIN City C On Sg.CityCode = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                Where D.Div_Code = '" & Replace(ReportFrm.FGetCode(rowDivision), "'", "") & "'"
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(5, 3).Value = GetSalesTaxNo()
                xlWorkSheet.Cells.Item(6, 3).Value = DtTable.Rows(0)("Name")
            End If

            'For Year
            xlWorkSheet.Cells.Item(5, 7).Value = AgL.XNull(AgL.Dman_Execute(" Select cyear From Company Where Comp_Code = '" & AgL.PubCompCode & "' ", AgL.GCn).ExecuteScalar)

            'Month	
            xlWorkSheet.Cells.Item(6, 7).Value = MonthName


            '3.1 (a) Outward Taxable  supplies  (other than zero rated, nil rated and exempted)
            'Sales Amount And Tax On It (Both Local And Central Combined)
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue, Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.CentralTax) as CentralTax, Sum(H.StateTax) as StateTax, Sum(H.Cess)  As Cess
                    From (" & FGetOutwardSuppliesOtherThanZeroRatedQry(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(11, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
                xlWorkSheet.Cells.Item(11, 4).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(11, 5).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(11, 7).Value = DtTable.Rows(0)("Cess")
            End If

            '3.1 (b) Outward Taxable  supplies  (zero rated )
            'Export Sales (Both on Bond Without Bond)
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue, Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.Cess)  As Cess
                    From (" & FGetOutwardSuppliesZeroRatedQry(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(12, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
                xlWorkSheet.Cells.Item(12, 4).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(12, 7).Value = DtTable.Rows(0)("Cess")
            End If

            '3.1 (c) Other Outward Taxable  supplies (Nil rated, exempted)
            'Goods Covered in Excemtion Notification & Goods Having rate 0%
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue
                    From (" & FGetOtherOutwardSuppliesNilRated(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(13, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
            End If

            '3.1 (d) Inward supplies (liable to reverse charge) 
            'Tax to be Paid on reverse charge.
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue, Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.CentralTax) as CentralTax, Sum(H.StateTax) as StateTax, Sum(H.Cess)  As Cess
                    From (" & FGetInwardSuppliesLiableToReverseCharge(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(14, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
                xlWorkSheet.Cells.Item(14, 4).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(14, 5).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(14, 7).Value = DtTable.Rows(0)("Cess")
            End If

            '3.1 (e) Non-GST Outward supplies
            'Goods not covered in GST, Like Diesel
            mQry = " Select Sum(H.Taxablevalue) as TotalTaxablevalue
                    From (" & FGetNonGSTOutwardSupplies(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(15, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
            End If


            '3.2  Of the supplies shown in 3.1 (a), details of inter-state supplies made to unregistered persons, composition taxable person and UIN holders						
            'Suppliers Made to UnRegistered Person : Only InterState Sales to Unregistered
            'Suppliers Made to Composition Taxable Person : Only InterState Sales to Composition Dealer
            'Suppliers Made to UiN Holders : Only InterState Sales to UIN Holders like Embassy
            mQry = " SELECT H.PlaceOfSupply,
                    Sum(H.Taxablevalue_Unregistered) As TotalTaxablevalue_Unregistered,
                    Sum(H.IntegratedTax_Unregistered) As AmountOfIntegratedTax_Unregistered,
                    Sum(H.Taxablevalue_Composition) As TotalTaxablevalue_Composition,
                    Sum(H.IntegratedTax_Composition) As AmountOfIntegratedTax_Composition,
                    Sum(H.Taxablevalue_UINholders) As TotalTaxablevalue_UINholders,
                    Sum(H.IntegratedTax_UINholders) As AmountOfIntegratedTax_UINholders
                    From (" & FGetInterStateSuppliesToUnRegAndComp(mCondStr) & ") As H
                    Group By H.PlaceOfSupply 
                    Having Sum(H.Taxablevalue_Unregistered) > 0 Or 
                    Sum(H.IntegratedTax_Unregistered) > 0 Or 
                    Sum(H.Taxablevalue_Composition) > 0 Or 
                    Sum(H.IntegratedTax_Composition) > 0 Or 
                    Sum(H.Taxablevalue_UINholders) > 0 Or 
                    Sum(H.IntegratedTax_UINholders) > 0 "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For I As Integer = 0 To DtTable.Rows.Count - 1
                xlWorkSheet.Cells.Item(79 + I, 2).Value = DtTable.Rows(I)("PlaceOfSupply")
                xlWorkSheet.Cells.Item(79 + I, 3).Value = DtTable.Rows(I)("TotalTaxablevalue_Unregistered")
                xlWorkSheet.Cells.Item(79 + I, 4).Value = DtTable.Rows(I)("AmountOfIntegratedTax_Unregistered")
                xlWorkSheet.Cells.Item(79 + I, 5).Value = DtTable.Rows(I)("TotalTaxablevalue_Composition")
                xlWorkSheet.Cells.Item(79 + I, 6).Value = DtTable.Rows(I)("AmountOfIntegratedTax_Composition")
                xlWorkSheet.Cells.Item(79 + I, 7).Value = DtTable.Rows(I)("TotalTaxablevalue_UINholders")
                xlWorkSheet.Cells.Item(79 + I, 8).Value = DtTable.Rows(I)("AmountOfIntegratedTax_UINholders")
            Next



            '4. Eligible ITC	(1)   Import of goods 
            'Tax Charged on Import of Goods liKe IGST
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, Sum(H.Cess)  As Cess
                    From (" & FGetImportOfGoods(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(22, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(22, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(2)   Import of services
            'Tax paid on Import of service (Covered under Reverse Charge) 
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, Sum(H.Cess)  As Cess
                    From (" & FGetImportOfServices(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(23, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(23, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(3)   Inward supplies liable to reverse charge        (other than 1 &2 above)
            'All Other purchase from unregistered Dealer (Local Purchase)
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, Sum(H.CentralTax) as CentralTax, Sum(H.Cess)  As Cess
                    From (" & FGetInwardsuppliesliableToReverseChargeOtherThen1And2(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(24, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(24, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(24, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(4)   Inward supplies from ISD
            'Input from other Branches (Input Service Distributors)
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.CentralTax) as CentralTax, Sum(H.StateTax) as StateTax, Sum(H.Cess)  As Cess
                    From (" & FGetInwardSuppliesFromISD(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(25, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(25, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(25, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(5)   All other ITC
            'Normal Purchase from Registered Dealer
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax, 
                    Sum(H.CentralTax) as CentralTax, Sum(H.StateTax) as StateTax, Sum(H.Cess)  As Cess
                    From (" & FGetAllOtherITC(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(26, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(26, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(26, 6).Value = DtTable.Rows(0)("Cess")
            End If


            '4. (D)  Ineligible ITC	(1)   As per section 17(5) of CGST//SGST Act
            mQry = " SELECT Round(Sum(H.IntegratedTax),2) As IntegratedTax,
                    Round(Sum(H.CentralTax),2) As CentralTax,
                    Round(Sum(H.StateTax),2) As StateTax,
                    Round(Sum(H.Cess),2) As Cess
                    From (" & FGetIneligibleITCAsPerSection17(mCondStrITC) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(32, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(32, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(32, 5).Value = DtTable.Rows(0)("StateTax")
                xlWorkSheet.Cells.Item(32, 6).Value = DtTable.Rows(0)("Cess")
            End If


            '5. Values of exempt, From a supplier under composition scheme, Exempt  and Nil rated supply	
            'Purchase of Goods 0%, Exempted etc
            mQry = " Select Sum(H.Taxablevalue_InterState) As InterStatesupplies,
                    Sum(H.Taxablevalue_IntraState) As Intrastatesupplies
                    from (" & FGetCompositionExcemptedAndNillRated(mCondStr) & ")  As H"
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(39, 4).Value = DtTable.Rows(0)("InterStatesupplies")
                xlWorkSheet.Cells.Item(39, 5).Value = DtTable.Rows(0)("Intrastatesupplies")
            End If

            '5. Values of exempt, Non GST supply	
            'Purchase of Goods not Covered on GST
            mQry = " Select Sum(H.Taxablevalue_InterState) As InterStatesupplies,
                    Sum(H.Taxablevalue_IntraState) As Intrastatesupplies
                    from (" & FGetNonGSTSupplies(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(40, 4).Value = DtTable.Rows(0)("InterStatesupplies")
                xlWorkSheet.Cells.Item(40, 5).Value = DtTable.Rows(0)("Intrastatesupplies")
            End If


            '5.1 Interest & late fee payable	
            'Intrest @18% on late payment of tax
            mQry = " Select Sum(H.IntegratedTax) As IntegratedTax,
                    Sum(H.CentralTax) As CentralTax,
                    Sum(H.StateTax) As StateTax,
                    Sum(H.Cess) As Cess
                    From (" & FGetLateFeesQry(mCondStr) & ") As H "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(56, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(56, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(56, 5).Value = DtTable.Rows(0)("StateTax")
                xlWorkSheet.Cells.Item(56, 6).Value = DtTable.Rows(0)("Cess")
            End If


            ClsMain.FReleaseObjects(xlWorkSheet)

            OutputWorkBook.Save()
            OutputWorkBook.Close()
            xlApp.Quit()

            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)

            'System.Diagnostics.Process.Start(OutputFile)
            MsgBox("File Generated Successfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
            OutputWorkBook.Close()
            xlApp.Quit()
            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)
        End Try
    End Sub
#End Region

    Public Shared Function FRemoveSpecialCharactersForGSTReturns(StrValue As String)
        FRemoveSpecialCharactersForGSTReturns = StrValue.Replace("~", "").Replace("`", "").Replace("!", "").
        Replace("@", "").Replace("#", "").Replace("$", "").Replace("%", "").Replace("^", "").
        Replace("&", "").Replace("*", "").Replace("(", "").Replace(")", "").Replace("{", "").
        Replace("}", "").Replace("[", "").Replace("]", "").Replace("\", "").Replace(":", "").
        Replace(";", "").Replace("'", "").Replace(",", "").Replace("?", "").Replace("<", "").
        Replace(">", "").Replace("""", "").Replace("_", "").Replace("+", "").Replace("=", "")
    End Function
End Class
