Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsPaymentReceiptReport_Kirana

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
    Dim rowSite As Integer = 3
    Dim rowDivision As Integer = 4
    Dim rowBroker As Integer = 5
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
    Public Class ReportType
        Public Const PaymentRegister As String = "Payment Register"
        Public Const ReceiptRegister As String = "Receipt Register"
    End Class

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
            mQry = "Select '" & ReportType.PaymentRegister & "' as Code, '" & ReportType.PaymentRegister & "' as Name 
                    Union All Select '" & ReportType.ReceiptRegister & "' as Code, '" & ReportType.ReceiptRegister & "' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, ShowReportType,,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Broker", "Broker", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
            ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcPaymentReceiptReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcPaymentReceiptReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = ReportFrm.FGetText(rowReportType)


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If



            mCondStr = " And Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode", rowBroker)

            If ReportFrm.FGetText(rowReportType) = ReportType.PaymentRegister Then
                mCondStr = mCondStr & " And Vt.NCat = '" & Ncat.Payment & "'"
                mCondStr = mCondStr & " And IVt.NCat = '" & Ncat.PurchaseInvoice & "'"
            ElseIf ReportFrm.FGetText(rowReportType) = ReportType.ReceiptRegister Then
                mCondStr = mCondStr & " And Vt.NCat = '" & Ncat.Receipt & "'"
                mCondStr = mCondStr & " And IVt.NCat = '" & Ncat.SaleInvoice & "'"
            End If

            mQry = "SELECT H.DocID As SearchCode, H.V_Type || '-' || H.ManualRefNo As EntryNo, H.V_Date As EntryDate, 
                    Lsg.Name As Party, SG.Name As Broker, "
            If ReportFrm.FGetText(rowReportType) = ReportType.PaymentRegister Then
                mQry += " Ls.AmountCr As BillAmount, "
            ElseIf ReportFrm.FGetText(rowReportType) = ReportType.ReceiptRegister Then
                mQry += " Ls.AmountDr As BillAmount, "
            End If
            mQry += " Ls.Addition1Per As InterestPer, Ls.Addition1Amount As InterestAmount,
                    Ls.Deduction1Per As DiscountPer, Ls.Deduction1Amount As DiscountAmount,
                    Ls.Addition2Per As BrokeragePer, Ls.Addition2Amount As BrokerageAmount,
                    L.Amount As NetAmount
                    FROM LedgerHead H 
                    LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID
                    LEFT JOIN Subgroup SG ON H.Subcode = Sg.Subcode 
                    LEFT JOIN SubGroup LSg On H.LinkedSubCode = LSg.SubCode
                    LEFT JOIN LedgerSettlement Ls On L.DocId = Ls.DocId
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SaleInvoice Si On Ls.TransactionDocId = Si.DocId
                    LEFT JOIN PurchInvoice Pi On Ls.TransactionDocId = Pi.DocId
                    LEFT JOIN LedgerHead Lh On Ls.TransactionDocId = Lh.DocId
                    LEFT JOIN Voucher_Type IVt On IfNull(IfNull(Si.V_Type, Pi.V_Type), Lh.V_Type) = IVt.V_Type
                    Where 1=1 " & mCondStr
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcPaymentReceiptReport"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.ProcFillGrid(DsHeader)

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
