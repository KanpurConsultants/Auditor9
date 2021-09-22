Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsTransactionSummary

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
    Dim rowVoucherType As Integer = 5
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
    Dim mHelpVoucherTypeQry$ = "Select 'o' As Tick, V_Type, Description From Voucher_type Order By Description"
    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name  "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Summary",,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpVoucherTypeQry,, 600, 650, 300)
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
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Transaction Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Detail"
                        mFilterGrid.Item(GFilter, rowVoucherType).Value = mGridRow.Cells("Doc Type Name").Value
                        mFilterGrid.Item(GFilterCode, rowVoucherType).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    Else
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " And Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", rowVoucherType)


            mQry = "Select H.DocId as SearchCode, H.V_Type DocType, H.ManualRefNo as DocNo, H.V_Date as DocDate, VT.NCat, VT.Category, VT.Description as DocTypeDesc, 
                    H.SaleToPartyName || (Case When IfNull(C.CityName,'')='' Then '' Else ', ' || C.CityName End) as PartyName, H.Remarks as Narration, Round(Abs(L.Qty),3) as Qty, L.Unit, 
                    Abs(H.Net_Amount) as Net_Amount,
                    (Case When H.Net_Amount > 0 Then H.Net_Amount Else 0.00 End) as AmountDr, 
                    (Case When H.Net_Amount < 0 Then Abs(H.Net_Amount) Else 0.00 End) as AmountCr 
                    From SaleInvoice H
                    Left Join City C On H.SaleToPartyCity = C.CityCode
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
                    Left Join (Select L1.DocID, 
                               Sum(Case when abs(IfNull(I1.MaintainStockYn,1)) =1 AND IfNull(I1.ItemType,Ic1.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then L1.Qty Else 0 End) as Qty, 
                               Max(Case when abs(IfNull(I1.MaintainStockYn,1)) =1 AND IfNull(I1.ItemType,Ic1.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then I1.Unit Else Null End) as Unit 
                               From SaleInvoiceDetail L1 With (NoLock) 
                               Left Join Item I1 On L1.Item = I1.Code 
                               Left Join Item IC1 On IsNull(I1.ItemCategory,I1.Code) = IC1.Code 
                               Group By L1.DocId) as L On H.DocId = L.DocId
                    Where 1=1 " & mCondStr & "  "

            mQry = mQry & " Union all "

            mQry = mQry & "Select H.DocId as SearchCode, H.V_Type DocType, H.ManualRefNo as DocNo, H.V_Date as DocDate, VT.NCat, VT.Category, VT.Description as DocTypeDesc, 
                    H.VendorName || (Case When IfNull(C.CityName,'')='' Then '' Else ', ' || C.CityName End) as PartyName, H.Remarks as Narration, Round(Abs(L.Qty),3) as Qty, L.Unit, 
                    Abs(H.Net_Amount) as Net_Amount,
                    (Case When H.Net_Amount < 0 Then H.Net_Amount Else 0.00 End) as AmountDr, 
                    (Case When H.Net_Amount > 0 Then Abs(H.Net_Amount) Else 0.00 End) as AmountCr 
                    From PurchInvoice H
                    Left Join City C On H.VendorCity = C.CityCode
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
                    Left Join (Select L1.DocID, 
                               Sum(Case when abs(IfNull(I1.MaintainStockYn,1)) =1 AND IfNull(I1.ItemType,Ic1.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then L1.Qty Else 0 End) as Qty, 
                               Max(Case when abs(IfNull(I1.MaintainStockYn,1)) =1 AND IfNull(I1.ItemType,Ic1.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then I1.Unit Else Null End) as Unit 
                               From PurchInvoiceDetail L1 With (NoLock) 
                               Left Join Item I1 On L1.Item = I1.Code 
                               Left Join Item IC1 On IsNull(I1.ItemCategory,I1.Code) = IC1.Code 
                               Group By L1.DocId) as L On H.DocId = L.DocId
                    Where 1=1 " & mCondStr & "  "


            mQry = mQry & " Union all "

            mQry = mQry & "Select H.DocId as SearchCode, H.V_Type DocType, H.ManualRefNo as DocNo, H.V_Date as DocDate, VT.NCat, VT.Category, VT.Description as DocTypeDesc, 
                    Sg.Name || (Case When IfNull(C.CityName,'')='' Then '' Else ', ' || C.CityName End) as PartyName, 
                    IfNull(H.Remarks,'') || (Case When IfNull(H.Remarks,'')<>'' And IfNull(L.Remarks,'')<>'' Then ', ' Else '' End) || IfNull(L.Remarks,'') || '. ' || IfNull(L.Specification,'') as Narration, Round(L.Qty,3), Null as Unit, 
                    L.Amount as Net_Amount,
                    (Case When VT.HeaderAccountDrCr='CR' Then L.Amount Else 0.00 End) as AmountDr, 
                    (Case When VT.HeaderAccountDrCr='DR' Then L.Amount Else 0.00 End) as AmountCr 
                    From LedgerHead H
                    Left Join LedgerHeadDetail L On H.DocId = L.DocID
                    Left Join LedgerHeadDetailCharges LC On L.DocId = LC.DocID And L.Sr = LC.Sr
                    Left Join Subgroup SgH On H.Subcode = SgH.Subcode
                    Left Join Subgroup Sg On L.Subcode = Sg.Subcode
                    Left Join City C On Sg.CityCode = C.CityCode
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
                    Where (SgH.Nature In ('Cash','Bank') Or VT.Ncat='LF')And L.Amount>0 And VT.HeaderAccountDrCr In ('DR','CR') " & mCondStr & "  "


            mQry = mQry & " Union all "

            mQry = mQry & "Select H.DocId as SearchCode, H.V_Type DocType, H.ManualRefNo as DocNo, H.V_Date as DocDate, VT.NCat, VT.Category, VT.Description as DocTypeDesc, 
                    Sg.Name || (Case When IfNull(C.CityName,'')='' Then '' Else ', ' || C.CityName End) as PartyName, 
                    IfNull(H.Remarks,'') || (Case When IfNull(H.Remarks,'')<>'' And IfNull(L.Remarks,'')<>'' Then ', ' Else '' End) || IfNull(L.Remarks,'') as Narration, Round(Abs(L.Qty),3) as Qty, Null as Unit, 
                    L.Amount as Net_Amount,
                    L.Amount as AmountDr, 
                    L.AmountCr 
                    From LedgerHead H
                    Left Join LedgerHeadDetail L On H.DocId = L.DocID
                    Left Join LedgerHeadDetailCharges LC On L.DocId = LC.DocID And L.Sr = LC.Sr
                    Left Join Subgroup SgH On H.Subcode = SgH.Subcode
                    Left Join Subgroup Sg On L.Subcode = Sg.Subcode
                    Left Join City C On Sg.CityCode = C.CityCode
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
                    Where H.Subcode Is Null " & mCondStr & "  "


            mQry = mQry & " Union all "

            mQry = mQry & "Select H.DocId as SearchCode, H.V_Type DocType, H.ManualRefNo as DocNo, H.V_Date as DocDate, VT.NCat, VT.Category, VT.Description as DocTypeDesc, 
                    Sg.Name || (Case When IfNull(C.CityName,'')='' Then '' Else ', ' || C.CityName End) as PartyName, 
                    IfNull(H.Remarks,'') as Narration, 0.00 as Qty, Null as Unit, 
                    Abs(IsNull(HC.Net_Amount,VLine.TotalLineAmount)) as Net_Amount,
                    (Case When IsNull(HC.Net_Amount,VLine.TotalLineAmount) < 0 Then IsNull(HC.Net_Amount,VLine.TotalLineAmount) Else 0.00 End) as AmountDr, 
                    (Case When IsNull(HC.Net_Amount,VLine.TotalLineAmount) > 0 Then Abs(IsNull(HC.Net_Amount,VLine.TotalLineAmount)) Else 0.00 End) as AmountCr 
                    From LedgerHead H                    
                    Left Join LedgerHeadCharges HC On H.DocId = HC.DocID
                    Left Join PurchInvoice PI On H.DocID = PI.DocId
                    Left Join Subgroup Sg On H.Subcode = Sg.Subcode
                    Left Join City C On Sg.CityCode = C.CityCode
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
                    LEFT JOIN (
	                    SELECT L.DocID, Sum(L.Amount) AS TotalLineAmount
	                    FROM LedgerHead H 
	                    LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID
	                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                    WHERE Vt.NCat = 'PS'
	                    GROUP BY L.DocID) AS VLine ON H.DocId = VLine.DocId
                    Where Sg.Nature Not In ('Cash','Bank') And PI.DocID is Null And VT.NCat Not In ('LF') " & mCondStr & "  "

            'mQry = mQry & "Select H.V_Type as SearchCode, VT.NCat, VT.Category, VT.Description, Count(H.DocId) as DocCount, Sum(HC.Net_Amount) as NetAmount 
            '        From LedgerHead H
            '        Left Join LedgerHeadCharges HC On H.DocId = HC.DocId
            '        Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
            '        Where H.Structure Is Not Null And HC.Net_Amount > 0 " & mCondStr & " Group By H.V_Type, VT.NCat, VT.Category, VT.Description "

            'mQry = mQry & " Union all "

            'mQry = mQry & "Select H.V_Type as SearchCode, VT.NCat, VT.Category, VT.Description, Count(Distinct H.DocId) as DocCount, Sum(L.Amount) as NetAmount 
            '        From LedgerHead H
            '        Left Join LedgerHeadDetail L On H.DocId = L.DocId
            '        Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
            '        Where H.Structure Is Null And L.Amount > 0 " & mCondStr & " Group By H.V_Type, VT.NCat, VT.Category, VT.Description "


            If ReportFrm.FGetText(rowReportType) = "Summary" Then

                'mQry = " Select VMain.SearchCode, VMain.Description as VoucherType, VMain.DocCount, VMain.NetAmount 
                '    From (" & mQry & ") As VMain
                '    Order By VMain.NCat, VMain.Description "
                mQry = " Select VMain.DocType as SearchCode, VMain.DocType, Max(VMain.DocTypeDesc) as DocTypeName, 
                    Count(Distinct VMain.Searchcode) as DocCount, Round(Sum(VMain.Qty),3) as Qty, Max(VMain.Unit) as Unit,
                    Sum(VMain.Net_Amount) as Net_Amount
                    From (" & mQry & ") As VMain
                    Group By VMain.DocType 
                    Order By Max(VMain.DocTypeDesc)"
            Else
                mQry = " Select VMain.SearchCode, VMain.DocType, VMain.DocNo, VMain.DocDate, VMain.DocTypeDesc, 
                    VMain.PartyName, VMain.Narration, Round(VMain.Qty,3) as Qty, VMain.Unit, VMain.Net_Amount
                    From (" & mQry & ") As VMain
                    Order By VMain.DocDate, VMain.DocType, VMain.DocNo "
            End If



            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Transaction Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"
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

    Public Sub ProcMainBackup(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Transaction Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Detail"
                        mFilterGrid.Item(GFilter, rowVoucherType).Value = mGridRow.Cells("Voucher Type").Value
                        mFilterGrid.Item(GFilterCode, rowVoucherType).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    Else
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " And Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", rowVoucherType)


            mQry = "Select H.V_Type as SearchCode, VT.NCat, VT.Category, VT.Description, Count(H.DocId) as DocCount, Sum(H.Net_Amount) as NetAmount 
                    From SaleInvoice H
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
                    Where 1=1 " & mCondStr & " Group By H.V_Type, VT.NCat, VT.Category, VT.Description "

            mQry = mQry & " Union all "

            mQry = mQry & "Select H.V_Type as SearchCode, VT.NCat, VT.Category, VT.Description, Count(H.DocId) as DocCount, Sum(H.Net_Amount) as NetAmount 
                    From PurchInvoice H
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
                    Where 1=1 " & mCondStr & " Group By H.V_Type, VT.NCat, VT.Category, VT.Description "

            mQry = mQry & " Union all "

            mQry = mQry & "Select H.V_Type as SearchCode, VT.NCat, VT.Category, VT.Description, Count(H.DocId) as DocCount, Sum(HC.Net_Amount) as NetAmount 
                    From LedgerHead H
                    Left Join LedgerHeadCharges HC On H.DocId = HC.DocId
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
                    Where H.Structure Is Not Null And HC.Net_Amount > 0 " & mCondStr & " Group By H.V_Type, VT.NCat, VT.Category, VT.Description "

            mQry = mQry & " Union all "

            mQry = mQry & "Select H.V_Type as SearchCode, VT.NCat, VT.Category, VT.Description, Count(Distinct H.DocId) as DocCount, Sum(L.Amount) as NetAmount 
                    From LedgerHead H
                    Left Join LedgerHeadDetail L On H.DocId = L.DocId
                    Left Join Voucher_Type Vt On H.V_Type = Vt.V_type 
                    Where H.Structure Is Null And L.Amount > 0 " & mCondStr & " Group By H.V_Type, VT.NCat, VT.Category, VT.Description "


            If ReportFrm.FGetText(rowReportType) = "Summary" Then
                mQry = " Select VMain.SearchCode, VMain.Description as VoucherType, VMain.DocCount, VMain.NetAmount 
                    From (" & mQry & ") As VMain
                    Order By VMain.NCat, VMain.Description "
            End If



            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Transaction Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"
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
