Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsSaleEnquiryMapping

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

    Public Const Col1DocId As String = "Search Code"
    Public Const Col1V_Type As String = "V_Type"
    Public Const Col1V_Date As String = "Enquiry Date"
    Public Const Col1Sr As String = "Sr"
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
    Public Const Col1SaleEnquiryMappingDocId As String = "Sale Enquiry Mapping Doc Id"
    Public Const Col1SaleOrderDocId As String = "Sale Order Doc Id"

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
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            mQry = "Select 'All' as Code, 'All' as Name 
                    Union All 
                    Select 'Mapped' as Code, 'Mapped' as Name 
                    Union All 
                    Select 'Un-Mapped' as Code, 'Un-Mapped' as Name "
            ReportFrm.CreateHelpGrid("Type", "Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Un-Mapped")
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupQry$, "All", 500, 500, 360)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.BtnCustomMenu.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcSaleEnquiryMapping()
    End Sub


    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcSaleEnquiryMapping(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Sale Enquiry Mapping"

            mCondStr = " Where 1=1"
            mCondStr = mCondStr & " AND H.V_Date Between '" & CDate(ReportFrm.FGetText(0)).ToString("s") & "' And '" & CDate(ReportFrm.FGetText(1)).ToString("s") & "' "

            If ReportFrm.FGetText(2) = "Un-Mapped" Then
                mCondStr = mCondStr & " And Sem.DocId Is Null "
            ElseIf ReportFrm.FGetText(2) = "Mapped" Then
                mCondStr = mCondStr & " And Sem.DocId Is Not Null "
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 3)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 4), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 5), "''", "'")

            mQry = "SELECT H.DocID As SearchCode, L.Sr, H.ManualRefNo As EnquiryNo, H.V_Date As EnquiryDate, 
                    H.SaleToPartyName As PartyName, H.SaleToPartyDocNo As PartyDocNo, H.SaleToPartyDocDate As PartyDocDate, 
                    L.PartyItem, L.PartyItemSpecification1, L.PartyItemSpecification2, L.PartyItemSpecification3, 
                    L.PartyItemSpecification4, L.PartyItemSpecification5, 
                    Sem.Item, Sem.Specification, Sem.Dimension1, Sem.Dimension2, Sem.Dimension3, Sem.Dimension4,
                    Sem.DocId As SaleEnquiryMappingDocId, Sid.DocId As SaleOrderDocId, I.Description As ItemDesc, 
                    D1.Description As Dimension1Desc, D2.Description As Dimension2Desc, 
                    D3.Description As Dimension3Desc, D4.Description As Dimension4Desc  
                    FROM SaleEnquiry H WITH (Nolock)
                    LEFT JOIN SaleEnquiryDetail L WITH (Nolock) ON L.DocID = H.DocID 
                    LEFT JOIN SaleEnquiryMapping Sem WITH (Nolock) on L.DocID = Sem.DocId And L.Sr = Sem.Sr
                    LEFT JOIN Item I On Sem.Item = I.Code
                    LEFT JOIN Dimension1 D1 On Sem.Dimension1 = D1.Code
                    LEFT JOIN Dimension2 D2 On Sem.Dimension2 = D2.Code
                    LEFT JOIN Dimension3 D3 On Sem.Dimension3 = D3.Code
                    LEFT JOIN Dimension4 D4 On Sem.Dimension4 = D4.Code
                    LEFT JOIN SaleInvoiceDetail Sid WITH (Nolock) On L.DocId = Sid.GenDocId And L.Sr = Sid.GenDocIdSr " & mCondStr &
                    " Order By H.V_Date, H.V_No, L.Sr "
            DsReport = AgL.FillData(mQry, AgL.GCn)

            If DsReport.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            mQry = "Select 'Save' As MenuText, 'FSave' As FunctionName"
            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            ReportFrm.Text = "Sale Enquiry Mapping"
            ReportFrm.ClsRep = Me
            ReportFrm.InputColumnsStr = Col1Item + Col1Dimension1 + Col1Dimension2 + Col1Dimension3 + Col1Dimension4
            ReportFrm.DTCustomMenus = DtMenuList

            ReportFrm.ProcFillGrid(DsReport)

            For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Item, I).Tag = ReportFrm.DGL1.Item(Col1Item, I).Value
                    ReportFrm.DGL1.Item(Col1Item, I).Value = ReportFrm.DGL1.Item(Col1ItemDesc, I).Value
                End If

                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension1, I).Tag = ReportFrm.DGL1.Item(Col1Dimension1, I).Value
                    ReportFrm.DGL1.Item(Col1Dimension1, I).Value = ReportFrm.DGL1.Item(Col1Dimension1Desc, I).Value
                End If

                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension2, I).Tag = ReportFrm.DGL1.Item(Col1Dimension2, I).Value
                    ReportFrm.DGL1.Item(Col1Dimension2, I).Value = ReportFrm.DGL1.Item(Col1Dimension2Desc, I).Value
                End If

                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension3, I).Tag = ReportFrm.DGL1.Item(Col1Dimension3, I).Value
                    ReportFrm.DGL1.Item(Col1Dimension3, I).Value = ReportFrm.DGL1.Item(Col1Dimension3Desc, I).Value
                End If

                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension4, I).Tag = ReportFrm.DGL1.Item(Col1Dimension4, I).Value
                    ReportFrm.DGL1.Item(Col1Dimension4, I).Value = ReportFrm.DGL1.Item(Col1Dimension4Desc, I).Value
                End If
            Next

            ReportFrm.DGL1.Columns(Col1Item).Visible = True

            ReportFrm.DGL1.Columns(Col1Dimension1).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension2).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension3).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension4).Visible = False
            ReportFrm.DGL1.Columns(Col1Sr).Visible = False
            ReportFrm.DGL1.Columns(Col1SaleEnquiryMappingDocId).Visible = False
            ReportFrm.DGL1.Columns(Col1SaleOrderDocId).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemDesc).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
            DsReport = Nothing
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) = True Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1Item
                    If mItemDataSet Is Nothing Then
                        mQry = " Select H.Code, H.Description From Item H Where H.V_Type = '" & ItemV_Type.Item & "' Order By H.Description "
                        mItemDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                    FSingleSelectForm(Col1Item, bRowIndex, mItemDataSet)
                Case Col1Dimension1
                    If mDimension1DataSet Is Nothing Then
                        mQry = " Select H.Code, H.Description From Dimension1 H Order By H.Description "
                        mDimension1DataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                    FSingleSelectForm(Col1Dimension1, bRowIndex, mDimension1DataSet)
                Case Col1Dimension2
                    If mDimension2DataSet Is Nothing Then
                        mQry = " Select H.Code, H.Description From Dimension2 H Order By H.Description "
                        mDimension2DataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                    FSingleSelectForm(Col1Dimension2, bRowIndex, mDimension2DataSet)
                Case Col1Dimension3
                    If mDimension3DataSet Is Nothing Then
                        mQry = " Select H.Code, H.Description From Dimension3 H Order By H.Description "
                        mDimension3DataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                    FSingleSelectForm(Col1Dimension3, bRowIndex, mDimension3DataSet)
                Case Col1Dimension4
                    If mDimension4DataSet Is Nothing Then
                        mQry = " Select H.Code, H.Description From Dimension4 H Order By H.Description "
                        mDimension4DataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                    FSingleSelectForm(Col1Dimension4, bRowIndex, mDimension4DataSet)
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
    Public Sub FSave(DGL As AgControls.AgDataGrid)
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim mDescription As String = ""
        Dim mSaleOrderDocId As String = ""
        Dim mV_Type As String = Ncat.SaleOrder
        Dim mV_No As String
        Dim mV_Prefix As String
        Dim mSr As Integer = 0

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            For I = 0 To ReportFrm.DGL1.RowCount - 1
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Tag) <> "" Then
                    If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                                From SaleEnquiryMapping With (NoLock)
                                Where DocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & "
                                And Sr = " & Val(ReportFrm.DGL1.Item(Col1Sr, I).Value) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                        mQry = "INSERT INTO SaleEnquiryMapping (DocID, Sr, Item, Specification, 
                            Dimension1, Dimension2, Dimension3, Dimension4)
                            SELECT " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & ", " & ReportFrm.DGL1.Item(Col1Sr, I).Value & " Sr, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Item, I).Tag) & " Item, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Specification, I).Value) & " Specification, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag) & " Dimension1, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag) & " Dimension2, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag) & " Dimension3, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag) & " Dimension4 
                            FROM SaleEnquiryDetail L 
                            WHERE L.DocID =" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            AND L.Sr =" & ReportFrm.DGL1.Item(Col1Sr, I).Value & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Else
                        mQry = "UPDATE SaleEnquiryMapping 
                            Set 
                            Item = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Item, I).Tag) & ", 
                            Specification = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Specification, I).Value) & ", 
                            Dimension1 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag) & ", 
                            Dimension2 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag) & ", 
                            Dimension3 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag) & ", 
                            Dimension4 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag) & " 
                            Where DocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            And Sr = " & ReportFrm.DGL1.Item(Col1Sr, I).Value & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If

                    mQry = "SELECT DocID FROM SaleInvoice WITH (Nolock) 
                            WHERE GenDocId =" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & ""
                    mSaleOrderDocId = AgL.XNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)

                    If mSaleOrderDocId = "" Then
                        mSaleOrderDocId = AgL.GetDocId(mV_Type, CStr(0), CDate(ReportFrm.DGL1.Item(Col1V_Date, I).Value), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                        mV_No = Val(AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                        mV_Prefix = AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
                        mQry = "INSERT INTO SaleInvoice (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, 
                                ManualRefNo, SaleToParty, BillToParty,  Agent, SaleToPartyName, SaleToPartyAddress, SaleToPartyPinCode, 
                                SaleToPartyCity, SaleToPartyMobile, SaleToPartySalesTaxNo, SaleToPartyDocNo, 
                                SaleToPartyDocDate, Remarks, TermsAndConditions, Status, EntryBy, EntryDate, 
                                SpecialDiscount_Per, SpecialDiscount, DeliveryDate, GenDocId)
                                SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & AgL.Chk_Text(mV_Type) & ", 
                                " & AgL.Chk_Text(mV_No) & ", H.V_Date, " & AgL.Chk_Text(mV_Prefix) & ", H.Div_Code, 
                                H.Site_Code, H.ManualRefNo, H.SaleToParty, H.SaleToParty As BillToParty, H.Agent, H.SaleToPartyName, 
                                H.SaleToPartyAddress, H.SaleToPartyPinCode, H.SaleToPartyCity, H.SaleToPartyMobile, 
                                H.SaleToPartySalesTaxNo, H.SaleToPartyDocNo, H.SaleToPartyDocDate, H.Remarks, 
                                H.TermsAndConditions, 'Active' Status, EntryBy, EntryDate, 0 SpecialDiscount_Per, 
                                0 SpecialDiscount, H.DeliveryDate, H.DocID As GenDocId
                                FROM SaleEnquiry H WHERE H.DocID =" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        AgL.UpdateVoucherCounter(mSaleOrderDocId, CDate(ReportFrm.DGL1.Item(Col1V_Date, I).Value), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
                    End If

                    If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                                From SaleInvoiceDetail With (NoLock)
                                Where GenDocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & "
                                And GenDocIdSr = " & Val(ReportFrm.DGL1.Item(Col1Sr, I).Value) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then

                        mSr = AgL.VNull(AgL.Dman_Execute("Select IsNull(Max(Sr),0) + 1 From SaleInvoiceDetail With (NoLock)
                                    Where DOcID = " & AgL.Chk_Text(mSaleOrderDocId) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

                        mQry = "INSERT INTO SaleInvoiceDetail (DocID, Sr, Item, Specification, 
                            Dimension1, Dimension2, Dimension3, Dimension4, Pcs, DocQty, Qty, Unit, UnitMultiplier, 
                            DocDealQty, DealQty, DealUnit, Rate, Amount, Remark, GenDocId, GenDocIdSr)
                            SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & mSr & " Sr, 
                            Sem.Item As Item, Sem.Specification As Specification, 
                            Sem.Dimension1 As Dimension1, Sem.Dimension2 As Dimension2, 
                            Sem.Dimension3 As Dimension3, Sem.Dimension4 As Dimension4, 
                            L.Qty As Pcs, L.Qty As DocQty, L.Qty As Qty, 'Pcs' As Unit, 1 As UnitMultiplier, 1 As DocDealQty, 
                            1 As DealQty, 'Pcs' As DealUnit, L.Rate, L.Amount, L.Remark, 
                            L.Docid GenDocId, L.Sr GenDocIdSr
                            FROM SaleEnquiryDetail L 
                            LEFT JOIN SaleEnquiryMapping Sem oN L.DocId = Sem.Docid And L.Sr = Sem.Sr
                            WHERE L.DocID =" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            AND L.Sr =" & ReportFrm.DGL1.Item(Col1Sr, I).Value & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Else
                        mQry = "UPDATE SaleInvoiceDetail 
                            Set 
                            Item = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Item, I).Tag) & ", 
                            Specification = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Specification, I).Value) & ", 
                            Dimension1 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag) & ", 
                            Dimension2 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag) & ", 
                            Dimension3 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag) & ", 
                            Dimension4 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag) & " 
                            Where GenDocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            And GenDocIdSr = " & ReportFrm.DGL1.Item(Col1Sr, I).Value & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Else
                    If AgL.XNull(ReportFrm.DGL1.Item(Col1SaleEnquiryMappingDocId, I).Value) <> "" Then
                        mQry = " Delete From SaleEnquiryMapping 
                            Where DocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & "
                            And Sr = " & ReportFrm.DGL1.Item(Col1Sr, I).Value & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If

                    If AgL.XNull(ReportFrm.DGL1.Item(Col1SaleOrderDocId, I).Value) <> "" Then
                        mQry = " Delete From SaleInvoiceDetail 
                            Where GenDocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & "
                            And GenDocIdSr = " & ReportFrm.DGL1.Item(Col1Sr, I).Value & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
