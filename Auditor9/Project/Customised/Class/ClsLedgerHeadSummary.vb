Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsLedgerHeadSummary
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
    Dim rowHeaderAccount As Integer = 4
    Dim rowLineAccount As Integer = 5
    Dim rowSite As Integer = 6
    Dim rowDivision As Integer = 7
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
    Dim mHelpLedgerAccountQry$ = " Select 'o' As Tick,  Sg.Code As Code, Sg.Name AS Account FROM ViewHelpSubGroup Sg  "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
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
                        SELECT 'o' As Tick, 'HeaderAccountCode' As Code, 
                        '" & IIf(EntryNCat = "LF", "Freight Account", "Header Account") & "' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'LineAccountCode' As Code, 
                        '" & IIf(EntryNCat = "LF", "Transporter", "Line Account") & "' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'SalesTaxGroupItem' As Code, 'Sales Tax Group Item' As Name 
                        UNION ALL 
                        SELECT 'o' As Tick, 'HSN' As Code, 'HSN' As Name  "
            ReportFrm.CreateHelpGrid("GroupOn", "Group On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, "")
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("HeaderAccount", "Header Account", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpLedgerAccountQry, , 450, 825, 300)
            ReportFrm.CreateHelpGrid("LineAccount", "Line Account", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpLedgerAccountQry, , 450, 825, 300)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcLedgerHeadSummary()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, ByVal strNCat As String)
        ReportFrm = mReportFrm
        EntryNCat = strNCat
    End Sub
    Public Sub ProcLedgerHeadSummary(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Ledger Head Summary"

            Dim bGroupOn As String = ""
            If ReportFrm.FGetCode(rowGroupOn) <> "" Then
                bGroupOn = ReportFrm.FGetCode(rowGroupOn).ToString.Replace("'", "")
            Else
                bGroupOn = "LineAccountCode"
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If ReportFrm.FGetText(rowReportType) = "Summary" Then
                    If bGroupOn.Contains("DivisionCode") Then
                        mFilterGrid.Item(GFilterCode, rowDivision).Value = "'" + mGridRow.Cells("Division Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowDivision).Value = mGridRow.Cells("Division").Value
                    End If

                    If bGroupOn.Contains("HeaderAccountCode") Then
                        mFilterGrid.Item(GFilterCode, rowHeaderAccount).Value = "'" + mGridRow.Cells("Header Account Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowHeaderAccount).Value = mGridRow.Cells("Header Account").Value
                    End If

                    If bGroupOn.Contains("LineAccountCode") Then
                        mFilterGrid.Item(GFilterCode, rowLineAccount).Value = "'" + mGridRow.Cells("Line Account Code").Value + "'"
                        mFilterGrid.Item(GFilter, rowLineAccount).Value = mGridRow.Cells("Line Account").Value
                    End If

                    mFilterGrid.Item(GFilter, rowReportType).Value = "Doc.Line Detail"
                    mFilterGrid.Item(GFilterCode, rowGroupOn).Value = ""
                    mFilterGrid.Item(GFilter, rowGroupOn).Value = ""
                ElseIf ReportFrm.FGetText(rowReportType) = "Doc.Header Wise Detail" Or
                    ReportFrm.FGetText(rowReportType) = "Doc.Line Detail" Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                End If
            End If

            Dim bNcat As String = Replace(EntryNCat, ",", "','")
            mCondStr = " Where VT.NCat In ('" & bNcat & "') "
            mCondStr = mCondStr & " And Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode", rowHeaderAccount)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SubCode", rowLineAccount)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")

            Dim mMainQry As String = ""
            mMainQry = " SELECT H.DocID, L.Sr, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.V_Type || '-' || H.ManualRefNo As EntryNo, H.V_Type, D.Div_Code As DivisionCode,
                    H.Site_Code, Site.Name as Site,
                    H.SubCode As HeaderAccountCode, H.SalesTaxGroupParty, 
                    L.SalesTaxGroupItem, L.Specification,
                    H.PartyName As HeaderAccountName, H.PartyMobile as Mobile, H.PartySalesTaxNo as PartyGstNo,
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.PartyDocNo as PartyInvoiceNo, H.PartyDocDate As PartyInvoiceDate, H.ManualRefNo as ManualRefNo, 
                    L.SubCode As LineAccountCode, LineAccount.Name As LineAccountName,
                    L.HSN, D.Div_Name As DivisionName,
                    Lc.Taxable_Amount, Lc.Net_Amount, L.Qty, L.Unit, 
                    L.Rate, L.Amount,
                    Lc.Tax1, Lc.Tax2, Lc.Tax3, Lc.Tax4, Lc.Tax5, Lc.Tax1 + Lc.Tax2+ Lc.Tax3 + Lc.Tax4 + Lc.Tax5 as TotalTax, 
                    " & IIf(AgL.PubServerName = "", "strftime('%m-%Y',V_Date)  ", "Substring(Convert(NVARCHAR, H.V_Date,103),4,7)") & " As Month
                    FROM LedgerHead H 
                    Left Join LedgerHeadDetail L On H.DocID = L.DocID 
                    Left Join LedgerHeadDetailCharges Lc On L.DocID = Lc.DocID And L.Sr = Lc.Sr
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join viewHelpSubgroup HeaderAccount On H.SubCode = HeaderAccount.Code                     
                    Left Join viewHelpSubgroup LineAccount On L.SubCode = LineAccount.Code                     
                    Left Join (Select SILTV.Subcode,SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code ) as LTV On HeaderAccount.code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On H.PartyCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


            If ReportFrm.FGetText(rowReportType) = "Doc.Header Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.DivisionName) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As DocDate, Max(VMain.EntryNo) As DocNo,
                    Max(VMain.HeaderAccountName) As HeaderAccount, Max(VMain.Mobile) as Mobile, Max(VMain.SalesTaxGroupParty) As SalesTaxGroupParty, 
                    IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount 
                    From(" & mMainQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
            ElseIf ReportFrm.FGetText(rowReportType) = "Doc.Line Detail" Then
                mQry = " Select VMain.DocId As SearchCode, VMain.DivisionName as Division, Vmain.Site as Site, VMain.V_Date As DocDate, VMain.EntryNo As DocNo,
                    VMain.HeaderAccountName As HeaderAccount, VMain.SalesTaxGroupParty As SalesTaxGroupParty, 
                    VMain.LineAccountName As LineAccount,
                    VMain.Specification As Specification,
                    IfNull(VMain.Qty,0) As Qty,
                    IfNull(VMain.Amount,0) As Amount,IfNull(VMain.Taxable_Amount,0) As TaxableAmount, IfNull(VMain.TotalTax,0) As TaxAmount, IfNull(VMain.Net_Amount,0) As NetAmount 
                    From(" & mMainQry & ") As VMain
                    Order By VMain.V_Date_ActualFormat, Cast(Replace(Vmain.ManualRefNo,'-','') as Integer) "
            ElseIf ReportFrm.FGetText(rowReportType) = "Summary" Then
                mQry = " Select Max(VMain.LineAccountCode) As SearchCode
                    " & IIf(bGroupOn.Contains("Month"), ", " & IIf(AgL.PubServerName = "", "Max(strftime('%m-%Y',VMain.V_Date_ActualFormat))", "Max(Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7))") & " as Month", "") & " 
                    " & IIf(bGroupOn.Contains("Date"), ", Max(VMain.V_Date) as Date", "") & " 
                    " & IIf(bGroupOn.Contains("DivisionCode"), ", DivisionCode, Max(VMain.DivisionName) as Division", "") & " 
                    " & IIf(bGroupOn.Contains("StateCode"), ", StateCode, Max(VMain.StateName) as State", "") & " 
                    " & IIf(bGroupOn.Contains("CityCode"), ", CityCode, Max(VMain.CityName) as City", "") & " 
                    " & IIf(bGroupOn.Contains("HeaderAccountCode"), ", HeaderAccountCode, Max(VMain.HeaderAccountName) as HeaderAccount", "") & " 
                    " & IIf(bGroupOn.Contains("LineAccountCode"), ", LineAccountCode, Max(VMain.LineAccountName) as LineAccount", "") & " 
                    " & IIf(bGroupOn.Contains("SalesTaxGroupItem"), ", SalesTaxGroupItem", "") & " 
                    " & IIf(bGroupOn.Contains("HSN"), ", HSN", "") & " 
                    ,Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Sum(VMain.Amount) as Amount,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount] 
                    From (" & mMainQry & ") As VMain 
                    Where VMain.DocId Is Not Null
                    GROUP By " & bGroupOn & ""

                Dim mOrderBy As String = ""
                mOrderBy += IIf(bGroupOn.Contains("Month"), "Month,", "")
                mOrderBy += IIf(bGroupOn.Contains("V_Date"), "V_Date,", "")
                mOrderBy += IIf(bGroupOn.Contains("DivisionCode"), "Division,", "")
                mOrderBy += IIf(bGroupOn.Contains("StateCode"), "State,", "")
                mOrderBy += IIf(bGroupOn.Contains("CityCode"), "City,", "")
                mOrderBy += IIf(bGroupOn.Contains("HeaderAccountCode"), "HeaderAccount,", "")
                mOrderBy += IIf(bGroupOn.Contains("LineAccountCode"), "LineAccount,", "")
                mOrderBy += IIf(bGroupOn.Contains("SalesTaxGroupItem"), "SalesTaxGroupItem,", "")
                mOrderBy += IIf(bGroupOn.Contains("HSN"), "HSN,", "")
                mQry = mQry + " Order By " + mOrderBy.Substring(0, mOrderBy.Length - 1)
            End If
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Ledger Head " + ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcLedgerHeadSummary"
            ReportFrm.AllowAutoResizeRows = False

            ReportFrm.ProcFillGrid(DsHeader)



            'If ReportFrm.DGL1.Columns.Contains("Doc Id") Then ReportFrm.DGL1.Columns("Doc Id").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Division Code") Then ReportFrm.DGL1.Columns("Division Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("State Code") Then ReportFrm.DGL1.Columns("State Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("City Code") Then ReportFrm.DGL1.Columns("City Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Header Account Code") Then ReportFrm.DGL1.Columns("Header Account Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Line Account Code") Then ReportFrm.DGL1.Columns("Line Account Code").Visible = False

            If EntryNCat = "LF" Then
                If ReportFrm.DGL1.Columns.Contains("Header Account") Then
                    ReportFrm.DGL1.Columns("Header Account").HeaderText = "Freight Account"
                End If
                If ReportFrm.DGL1.Columns.Contains("Line Account") Then
                    ReportFrm.DGL1.Columns("Line Account").HeaderText = "Transporter"
                End If
                If ReportFrm.DGL1.Columns.Contains("Specification") Then
                    ReportFrm.DGL1.Columns("Specification").HeaderText = "Lr No."
                End If
                If ReportFrm.DGL1.Columns.Contains("Qty") Then
                    ReportFrm.DGL1.Columns("Qty").HeaderText = "Bales"
                End If
            End If
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
