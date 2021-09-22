Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Microsoft.Reporting.WinForms
Public Class ClsProcessPlan

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
    Dim StrSQLQuery As String = ""
    Private Const CnsProfitAndLoss As String = "PRLS"

    Dim mShowReportType As String = ""

    Public Const Col1Select As String = "Tick"
    Public Const Col1PurchPlan As String = "Purch Plan"
    Public Const Col1PurchPlanSr As String = "Purch Plan Sr"
    Public Const Col1Item As String = "Item"
    Public Const Col1ItemDesc As String = "Item Desc"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Dimension1Desc As String = "Dimension1Desc"
    Public Const Col1Dimension2Desc As String = "Dimension2Desc"
    Public Const Col1Dimension3Desc As String = "Dimension3Desc"
    Public Const Col1Dimension4Desc As String = "Dimension4Desc"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1UnitMultiplier As String = "Unit Multiplier"
    Public Const Col1DealQty As String = "Deal Qty"
    Public Const Col1DealUnit As String = "Deal Unit"
    Public Const Col1ProdPlanQty As String = "Prod Plan Qty"
    Public Const Col1PurchPlanQty As String = "Purch Plan Qty"
    Public Const Col1StockPlanQty As String = "Stock Plan Qty"
    Public Const Col1Remark As String = "Remark"

    Private Const mFormat_PurchPlanPendingForPlan As String = "PurchPlanPendingForPlan"
    Private Const mFormat_SummaryToPlan As String = "SummaryToPlan"

    Private Const mPlanType_Production As String = "Production"
    Private Const mPlanType_Purchase As String = "Purchase"
    Private Const mPlanType_Stock As String = "Stock"

    Dim mFormat As String = ""

    Dim bTempTable As String = Guid.NewGuid.ToString

    Dim mItemDataSet As DataSet
    Dim mDimension1DataSet As DataSet
    Dim mDimension2DataSet As DataSet
    Dim mDimension3DataSet As DataSet
    Dim mDimension4DataSet As DataSet
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
    Dim mHelpPurchPlanQry$ = "SELECT 'o' As Tick, H.DocID, H.ManualRefNo AS PlanNo FROM PurchPlan H "
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupQry$, "All", 500, 500, 360)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("PurchPlan", "Purch Plan", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPurchPlanQry)
            ReportFrm.CreateHelpGrid("Remarks", "Remarks", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")
            ReportFrm.BtnProceed.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcProcessPlan()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcProcessPlan(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Sale Order Plan"

            mCondStr = mCondStr & " AND H.V_Date Between '" & CDate(ReportFrm.FGetText(0)).ToString("s") & "' And '" & CDate(ReportFrm.FGetText(1)).ToString("s") & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 2)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 3), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 4), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.DocId", 5), "''", "'")

            mQry = " SELECT 'o' As Tick, L.Item, I.Dimension1, I.Dimension2, I.Dimension3, I.Dimension4,
                    I.Description AS ItemDesc, L.Specification AS Specification, 
                    D1.Description AS Dimension1Desc, D2.Description AS Dimension2Desc,
                    D3.Description AS Dimension3Desc, D4.Description AS Dimension4Desc, L.PurchPlan, L.PurchPlanSr,
                    L.Qty, L.Unit, L.UnitMultiplier, L.DealQty, L.DealUnit
                    FROM PurchPlanDetail L 
                    LEFT JOIN Item I ON L.Item = I.Code
                    LEFT JOIN Dimension1 D1 ON I.Dimension1 = D1.Code
                    LEFT JOIN Dimension2 D2 ON I.Dimension2 = D2.Code
                    LEFT JOIN Dimension3 D3 ON I.Dimension3 = D3.Code
                    LEFT JOIN Dimension4 D4 ON I.Dimension4 = D4.Code  "
            DsReport = AgL.FillData(mQry, AgL.GCn)

            If DsReport.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Purch Plan"
            ReportFrm.ClsRep = Me
            ReportFrm.InputColumnsStr = Col1Item + Col1Dimension1 + Col1Dimension2 + Col1Dimension3 + Col1Dimension4

            mFormat = mFormat_PurchPlanPendingForPlan

            ReportFrm.Text = "Purch Plan - " + mFormat

            ReportFrm.ProcFillGrid(DsReport)

            ReportFrm.DGL1.Columns(Col1PurchPlan).Visible = False
            ReportFrm.DGL1.Columns(Col1PurchPlanSr).Visible = False
            ReportFrm.DGL1.Columns(Col1Item).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension1).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension2).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension3).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension4).Visible = False

            FAdjustFootedGrid()

            FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsReport = Nothing
        End Try
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        If mFormat = mFormat_PurchPlanPendingForPlan Then
            mQry = "CREATE TEMPORARY TABLE [#" & bTempTable & "] " &
                        " (PurchPlan nVarChar(21), PurchPlanSr Int, 
                        Item NVARCHAR (10), Specification NVARCHAR (255), 
                        Dimension1 NVARCHAR (10), Dimension2 NVARCHAR (10), 
                        Dimension3 NVARCHAR (10), Dimension4 NVARCHAR (10),
                        Qty Decimal(18,4), Unit NVARCHAR (10), UnitMultiplier Decimal(18,4), 
                        DealQty Decimal(18,4), DealUnit NVARCHAR (10))"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnRead)

            For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                If ReportFrm.DGL1.Item(Col1Select, I).Value = "þ" Then
                    mQry = " INSERT INTO [#" & bTempTable & "](PurchPlan, PurchPlanSr, 
                        Item, Specification, Dimension1, Dimension2, Dimension3, Dimension4, 
                        Qty, Unit, UnitMultiplier, DealQty, DealUnit)
                        Select " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1PurchPlan, I).Value)) & " As PurchPlan, 
                        " & Val(AgL.XNull(ReportFrm.DGL1.Item(Col1PurchPlanSr, I).Value)) & " As PurchPlanSr,
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Value)) & " As Item,
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Specification, I).Value)) & " As Specification,
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1, I).Value)) & " As Dimension1,
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2, I).Value)) & " As Dimension2,
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3, I).Value)) & " As Dimension3,
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4, I).Value)) & " As Dimension4,
                        " & Val(AgL.VNull(ReportFrm.DGL1.Item(Col1Qty, I).Value)) & " As Qty, 
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Unit, I).Value)) & " As Unit,
                        " & Val(AgL.VNull(ReportFrm.DGL1.Item(Col1UnitMultiplier, I).Value)) & " As UnitMultiplier,
                        " & Val(AgL.VNull(ReportFrm.DGL1.Item(Col1DealQty, I).Value)) & " As DealQty,
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1DealUnit, I).Value)) & " As DealUnit "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnRead)
                End If
            Next

            mQry = "Select T.Item, T.Dimension1, T.Dimension2, T.Dimension3, T.Dimension4, 
                    Max(I.Description) AS ItemDesc, T.Specification, 
                    Max(D1.Description) AS Dimension1Desc, Max(D2.Description) AS Dimension2Desc,
                    Max(D3.Description) AS Dimension3Desc, Max(D4.Description) AS Dimension4Desc, 
                    Sum(T.Qty) As Qty, T.Unit As Unit, 
                    Max(T.UnitMultiplier) As UnitMultiplier, Sum(T.DealQty) As DealQty, T.DealUnit, 
                    Sum(T.Qty) As ProdPlanQty, 0 As PurchPlanQty, 0 As StockPlanQty, '' As Remark
                    From [#" & bTempTable & "] T
                    LEFT JOIN PurchPlanDetail L On T.PurchPlan = L.DocId And T.PurchPlanSr = L.Sr 
                    LEFT JOIN Item I ON L.Item = I.Code
                    LEFT JOIN Dimension1 D1 ON L.Dimension1 = D1.Code
                    LEFT JOIN Dimension2 D2 ON L.Dimension2 = D2.Code
                    LEFT JOIN Dimension3 D3 ON L.Dimension3 = D3.Code
                    LEFT JOIN Dimension4 D4 ON L.Dimension4 = D4.Code 
                    Group By T.Item, T.Specification, T.Dimension1, T.Dimension2, T.Dimension3, T.Dimension4, T.Unit, T.DealUnit "
            DsReport = AgL.FillData(mQry, AgL.GcnRead)

            If DsReport.Tables(0).Rows.Count = 0 Then MsgBox("No Records Selected...!", MsgBoxStyle.Information) : Exit Sub

            ReportFrm.Text = "Sale Order Plan"
            ReportFrm.ClsRep = Me
            ReportFrm.InputColumnsStr = Col1Item + Col1Dimension1 + Col1Dimension2 + Col1Dimension3 + Col1Dimension4

            mFormat = mFormat_SummaryToPlan

            ReportFrm.Text = "Sale Order Plan - " + mFormat

            ReportFrm.ProcFillGrid(DsReport)

            ReportFrm.DGL1.Columns(Col1Item).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension1).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension2).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension3).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension4).Visible = False

            ReportFrm.InputColumnsStr = Col1Remark

            ReportFrm.DGL1.Columns(Col1ProdPlanQty).Visible = True
            ReportFrm.DGL1.Columns(Col1PurchPlanQty).Visible = True
            ReportFrm.DGL1.Columns(Col1StockPlanQty).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark).Visible = True
            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next
            ReportFrm.DGL1.Columns(Col1Remark).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1ProdPlanQty).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1PurchPlanQty).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1StockPlanQty).ReadOnly = False

            FAdjustFootedGrid()

            FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1)
        ElseIf mFormat = mFormat_SummaryToPlan Then
            Try
                Dim mTrans As String = ""
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                Dim bProdPlanQty As Double = 0
                Dim bPurchPlanQty As Double = 0
                Dim bStockPlanQty As Double = 0
                For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                    bProdPlanQty += Val(ReportFrm.DGL1.Item(Col1ProdPlanQty, I).Value)
                    bPurchPlanQty += Val(ReportFrm.DGL1.Item(Col1PurchPlanQty, I).Value)
                    bStockPlanQty += Val(ReportFrm.DGL1.Item(Col1StockPlanQty, I).Value)
                Next

                If bProdPlanQty > 0 Then FSave(AgL.GCn, AgL.ECmd, mPlanType_Production)
                If bPurchPlanQty > 0 Then FSave(AgL.GCn, AgL.ECmd, mPlanType_Purchase)
                If bStockPlanQty > 0 Then FSave(AgL.GCn, AgL.ECmd, mPlanType_Stock)

                AgL.ETrans.Commit()
                mTrans = "Commit"
                MsgBox("Process Completed...!", MsgBoxStyle.Information)
                ReportFrm.DGL1.DataSource = Nothing
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Public Sub FSave(Conn As Object, Cmd As Object, PlanType As String)
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0

        Dim I As Integer = 0, J As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim mDescription As String = ""
        Dim mPurchPlanDocId As String = ""
        Dim mV_Type As String = Ncat.ProcessPlan
        Dim mV_No As String
        Dim mV_Prefix As String
        Dim mV_Date As String
        Dim mSr As Integer = 0
        Dim mManualRefNo As String = ""
        Dim mRemarks As String = ""
        Dim bQtyColumnName As String = ""
        Dim bProcess As String = ""

        mV_Date = AgL.PubLoginDate.ToString
        mPurchPlanDocId = AgL.GetDocId(mV_Type, CStr(0), CDate(mV_Date), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
        mV_No = Val(AgL.DeCodeDocID(mPurchPlanDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
        mV_Prefix = AgL.DeCodeDocID(mPurchPlanDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)

        If PlanType = mPlanType_Production Then
            bQtyColumnName = Col1ProdPlanQty
            bProcess = "PProduct"
            mManualRefNo = ReportFrm.FGetText(5).ToString() + "-M"
        ElseIf PlanType = mPlanType_Purchase Then
            bQtyColumnName = Col1PurchPlanQty
            bProcess = "PPurchase"
            mManualRefNo = ReportFrm.FGetText(5).ToString() + "-P"
        ElseIf PlanType = mPlanType_Stock Then
            bQtyColumnName = Col1StockPlanQty
            bProcess = "PStock"
            mManualRefNo = ReportFrm.FGetText(5).ToString() + "-S"
        End If

        mRemarks = ReportFrm.FGetText(6).ToString()


        mQry = "INSERT INTO PurchPlan (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, 
                    ManualRefNo, Remarks, EntryBy, EntryDate)
                    Select " & AgL.Chk_Text(mPurchPlanDocId) & " As Docid, " & AgL.Chk_Text(mV_Type) & " As V_Type, 
                    " & AgL.Chk_Text(mV_Prefix) & " As v_prefix, " & AgL.Chk_Text(mV_Date) & " As v_date, 
                    " & Val(mV_No) & " As V_No, " & AgL.Chk_Text(AgL.PubDivCode) & " As div_code, 
                    " & AgL.Chk_Text(AgL.PubSiteCode) & " As Site_Code, " & AgL.Chk_Text(mManualRefNo) & "  As ManualRefNo, 
                    " & AgL.Chk_Text(mRemarks) & "  As Remarks,  " & AgL.Chk_Text(AgL.PubUserName) & "  As entryby, 
                    " & AgL.Chk_Text(AgL.PubLoginDate) & " As EntryDate "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To ReportFrm.DGL1.RowCount - 1
            If AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Value) <> "" Then
                If Val(ReportFrm.DGL1.Item(bQtyColumnName, I).Value) > 0 Then
                    mSr += 1
                    mQry = "Insert Into PurchPlanDetail(DocId, Sr, Process, Item, Dimension1, Dimension2, Dimension3, Dimension4, 
                           Specification, Qty, Unit, UnitMultiplier, DealUnit, DealQty, Remark) "
                    mQry += " Select " & AgL.Chk_Text(mPurchPlanDocId) & ", " & mSr & ", " &
                        " " & AgL.Chk_Text(bProcess) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Item, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension1, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension2, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension3, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension4, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Specification, I).Value) & ", " &
                        " " & Val(ReportFrm.DGL1.Item(bQtyColumnName, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Unit, I).Value) & ", " &
                        " " & Val(ReportFrm.DGL1.Item(Col1UnitMultiplier, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DealUnit, I).Value) & ", " &
                        " " & Val(ReportFrm.DGL1.Item(Col1DealQty, I).Value) & ", " &
                        " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Remark, I).Value) & " "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = " Select L.Docid, L.Sr As TSr, T.PurchPlan, T.PurchPlanSr, T.Qty As PurchPlanQty,
                            L.Qty * T.Qty / (
                                Select Sum(T.Qty) As Qty
                                From [#" & bTempTable & "] T
                                Where IsNull(T.Item,'') = IsNull(L.Item,'') And
                                IsNull(T.Specification,'') = IsNull(L.Specification,'') And
                                IsNull(T.Dimension1,'') = IsNull(L.Dimension1,'') And
                                IsNull(T.Dimension2,'')  = IsNull(L.Dimension2,'') And 
                                IsNull(T.Dimension3,'')  = IsNull(L.Dimension3,'') And 
                                IsNull(T.Dimension4,'')  = IsNull(L.Dimension4,'') 
                            ) As Qty
                            From PurchPlanDetail L With (NoLock)
                            LEFT JOIN [#" & bTempTable & "] T On IsNull(L.Item,'') = IsNull(T.Item,'') And
                                IsNull(L.Specification,'') = IsNull(T.Specification,'') And
                                IsNull(L.Dimension1,'') = IsNull(T.Dimension1,'') And
                                IsNull(L.Dimension2,'') = IsNull(T.Dimension2,'') And 
                                IsNull(L.Dimension3,'') = IsNull(T.Dimension3,'') And 
                                IsNull(L.Dimension4,'') = IsNull(T.Dimension4,'') 
                            Where L.DocId = " & AgL.Chk_Text(mPurchPlanDocId) & " And Sr = " & mSr & ""
                    Dim bPurchPlanDetailBase As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

                    Dim bPurchPlanDetailBaseSr As Integer = 0
                    For J = 0 To bPurchPlanDetailBase.Rows.Count - 1
                        bPurchPlanDetailBaseSr += 1
                        mQry = " INSERT INTO PurchPlanDetailBase (DocID, TSr, Sr, Qty)
                            Select " & AgL.Chk_Text(bPurchPlanDetailBase.Rows(J)("DocId")) & " As DocID, 
                            " & Val(bPurchPlanDetailBase.Rows(J)("TSr")) & " As TSr, 
                            " & Val(bPurchPlanDetailBaseSr) & " As Sr, 
                            " & Val(bPurchPlanDetailBase.Rows(J)("Qty")) & " As Qty "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Next
                End If
            End If
        Next
        AgL.UpdateVoucherCounter(mPurchPlanDocId, CDate(mV_Date), Conn, Cmd, AgL.PubDivCode, AgL.PubSiteCode)
    End Sub
    Private Sub FAdjustFootedGrid()
        For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
            ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
        Next
    End Sub
End Class
