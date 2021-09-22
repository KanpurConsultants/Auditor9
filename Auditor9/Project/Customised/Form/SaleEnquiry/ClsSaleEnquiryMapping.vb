Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
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
    Public Const Col1V_Type As String = "Type"
    Public Const Col1V_Date As String = "Enquiry Date"
    Public Const Col1Sr As String = "Sr"


    Public Const Col1ItemCategoryCode As String = "Item Category Code"
    Public Const Col1ItemGroupCode As String = "Item Group Code"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Dimension1Code As String = "Dimension1Code"
    Public Const Col1Dimension2Code As String = "Dimension2Code"
    Public Const Col1Dimension3Code As String = "Dimension3Code"
    Public Const Col1Dimension4Code As String = "Dimension4Code"
    Public Const Col1SizeCode As String = "Size Code"
    Public Const Col1SKUCode As String = "Sku Code"

    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1SKU As String = "Sku"


    Public Const Col1SaleEnquiryMappingDocId As String = "Sale Enquiry Mapping Doc Id"
    Public Const Col1SaleOrderDocId As String = "Sale Order Doc Id"


    Public Const Col1MItemCategory As String = "Main Item Category"
    Public Const Col1MItemGroup As String = "Main Item Group"
    Public Const Col1MItemSpecification As String = "Main Item Specification"
    Public Const Col1MDimension1 As String = "MDimension1"
    Public Const Col1MDimension2 As String = "MDimension2"
    Public Const Col1MDimension3 As String = "MDimension3"
    Public Const Col1MDimension4 As String = "MDimension4"
    Public Const Col1MSize As String = "Main Size"

    Public Const Col1Unit As String = "Unit"



    Dim mItemCategoryDataSet As DataSet
    Dim mItemGroupDataSet As DataSet
    Dim mItemDataSet As DataSet
    Dim mDimension1DataSet As DataSet
    Dim mDimension2DataSet As DataSet
    Dim mDimension3DataSet As DataSet
    Dim mDimension4DataSet As DataSet
    Dim mSizeDataSet As DataSet

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
            ReportFrm.BtnProceed.Visible = True
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
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "

            If ReportFrm.FGetText(2) = "Un-Mapped" Then
                mCondStr = mCondStr & " And Sem.DocId Is Null "
            ElseIf ReportFrm.FGetText(2) = "Mapped" Then
                mCondStr = mCondStr & " And Sem.DocId Is Not Null "
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 3)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 4), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 5), "''", "'")

            mQry = "SELECT H.DocID As SearchCode, H.V_Type As Type, L.Sr, H.ManualRefNo As EnquiryNo, H.V_Date As EnquiryDate, 
                    H.SaleToPartyName As PartyName, H.SaleToPartyDocNo As PartyDocNo, H.SaleToPartyDocDate As PartyDocDate, 
                    L.PartyItem, 
                    L.PartyItemSpecification1 As [Party Item Specification1], 
                    L.PartyItemSpecification2 As [Party Item Specification2], 
                    L.PartyItemSpecification3 As [Party Item Specification3], 
                    L.PartyItemSpecification4 As [Party Item Specification4], 
                    L.PartyItemSpecification5 As [Party Item Specification5], 
                    Sem.DocId As SaleEnquiryMappingDocId, Sid.DocId As SaleOrderDocId,
                    Sku.Code As SkuCode, Sku.BaseItem As ItemCode, Sku.ItemCategory As ItemCategoryCode, Sku.ItemGroup As ItemGroupCode, 
                    SKU.Dimension1 As Dimension1Code, SKU.Dimension2 As Dimension2Code, 
                    Sku.Dimension3 As Dimension3Code, Sku.Dimension4 As Dimension4Code, Sku.Size As SizeCode, Sku.Unit,
                    Sku.Description As Sku, IC.Description as ItemCategory, 
                    IG.Description as ItemGroup, I.Description as Item,                
                    D1.Description as Dimension1,D2.Description as Dimension2,
                    D3.Description as Dimension3,D4.Description as Dimension4,
                    Size.Description as Size, 
                    I.ItemCategory as MainItemCategory, I.ItemGroup as MainItemGroup, I.Specification as MainItemSpecification, 
                    I.Dimension1 as MDimension1,  I.Dimension2 as MDimension2,  
                    I.Dimension3 as MDimension3,  I.Dimension4 as MDimension4,  I.Size as MainSize
                    From SaleEnquiry H WITH (Nolock)
                    LEFT JOIN SaleEnquiryDetail L WITH (Nolock) ON L.DocID = H.DocID 
                    LEFT JOIN SaleEnquiryMapping Sem WITH (Nolock) on L.DocID = Sem.DocId And L.Sr = Sem.Sr
                    LEFT JOIN Item Sku ON Sku.Code = Sem.Item 
                    LEFT JOIN Item I ON I.Code = IfNull(Sku.BaseItem,Sku.Code) 
                    Left Join Item IC On Sku.ItemCategory = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                    LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                    LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                    LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                    LEFT JOIN Item Size ON Size.Code = Sku.Size
                    LEFT JOIN SaleInvoiceDetail Sid WITH (Nolock) On L.DocId = Sid.GenDocId And L.Sr = Sid.GenDocIdSr " & mCondStr &
                    " Order By H.V_Date, H.V_No, L.Sr "
            DsReport = AgL.FillData(mQry, AgL.GCn)

            If DsReport.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.DGL1.Name = "Dgl1"
            ReportFrm.Text = "Sale Enquiry Mapping"
            ReportFrm.ClsRep = Me
            ReportFrm.InputColumnsStr = "|" + Col1ItemCategory + "|" + "|" + Col1ItemGroup + "|" + "|" + Col1Item + "|" + "|" + Col1Dimension1 + "|" + "|" + Col1Dimension2 + "|" + "|" + Col1Dimension3 + "|" + "|" + Col1Dimension4 + "|" + "|" + Col1Size + "|"
            ReportFrm.ProcFillGrid(DsReport)


            For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                If AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategoryCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag = ReportFrm.DGL1.Item(Col1ItemCategoryCode, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroupCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1ItemGroup, I).Tag = ReportFrm.DGL1.Item(Col1ItemGroupCode, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Item, I).Tag = ReportFrm.DGL1.Item(Col1ItemCode, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1Code, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension1, I).Tag = ReportFrm.DGL1.Item(Col1Dimension1Code, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2Code, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension2, I).Tag = ReportFrm.DGL1.Item(Col1Dimension2Code, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3Code, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension3, I).Tag = ReportFrm.DGL1.Item(Col1Dimension3Code, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4Code, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension4, I).Tag = ReportFrm.DGL1.Item(Col1Dimension4Code, I).Value
                End If
                If AgL.XNull(ReportFrm.DGL1.Item(Col1SizeCode, I).Value) <> "" Then
                    ReportFrm.DGL1.Item(Col1Size, I).Tag = ReportFrm.DGL1.Item(Col1SizeCode, I).Value
                End If
            Next


            ReportFrm.DGL1.Columns(Col1SKUCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SKU).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCategoryCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemGroupCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCode).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension1Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension2Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension3Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension4Code).Visible = False
            ReportFrm.DGL1.Columns(Col1SizeCode).Visible = False

            ReportFrm.DGL1.Columns(Col1MItemCategory).Visible = False
            ReportFrm.DGL1.Columns(Col1MItemGroup).Visible = False
            ReportFrm.DGL1.Columns(Col1MItemSpecification).Visible = False
            ReportFrm.DGL1.Columns(Col1MDimension1).Visible = False
            ReportFrm.DGL1.Columns(Col1MDimension2).Visible = False
            ReportFrm.DGL1.Columns(Col1MDimension3).Visible = False
            ReportFrm.DGL1.Columns(Col1MDimension4).Visible = False
            ReportFrm.DGL1.Columns(Col1MSize).Visible = False


            If ClsMain.IsScopeOfWorkContains(IndustryType.CarpetIndustry) Then
                ReportFrm.DGL1.Columns(Col1ItemCategory).Visible = True
                ReportFrm.DGL1.Columns(Col1Item).Visible = False
                ReportFrm.DGL1.Columns(Col1Dimension1).Visible = True
                ReportFrm.DGL1.Columns(Col1Dimension2).Visible = True
                ReportFrm.DGL1.Columns(Col1Dimension3).Visible = True
                ReportFrm.DGL1.Columns(Col1Dimension4).Visible = False
                ReportFrm.DGL1.Columns(Col1Size).Visible = True
            Else
                ReportFrm.DGL1.Columns(Col1ItemCategory).Visible = False
                ReportFrm.DGL1.Columns(Col1Item).Visible = True
                ReportFrm.DGL1.Columns(Col1Dimension1).Visible = False
                ReportFrm.DGL1.Columns(Col1Dimension2).Visible = False
                ReportFrm.DGL1.Columns(Col1Dimension3).Visible = False
                ReportFrm.DGL1.Columns(Col1Dimension4).Visible = False
                ReportFrm.DGL1.Columns(Col1Size).Visible = False
            End If

            ReportFrm.DGL1.Columns(Col1V_Type).Visible = False
            ReportFrm.DGL1.Columns(Col1Sr).Visible = False
            ReportFrm.DGL1.Columns(Col1SaleEnquiryMappingDocId).Visible = False
            ReportFrm.DGL1.Columns(Col1SaleOrderDocId).Visible = False


            AgL.FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1, AgL)
            Dim DTUP As DataTable
            Dim ObjMdi As New MDIMain
            Dim StrUserPermission As String = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, ObjMdi.MnuSalesEnquiry.Name, ObjMdi.MnuSalesEnquiry.Text, DTUP)
            Dim FrmObj As New FrmSaleEnquiry(StrUserPermission, DTUP, Ncat.SaleEnquiry)
            ClsMain.GetUICaptions(ReportFrm.DGL1, FrmObj.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", ClsMain.GridTypeConstants.HorizontalGrid)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsReport = Nothing
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
        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) = True Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1ItemCategory
                    If mItemCategoryDataSet Is Nothing Then
                        mQry = " Select H.Code, H.Description From ItemCategory H Order By H.Description "
                        mItemCategoryDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                    FSingleSelectForm(Col1ItemCategory, bRowIndex, mItemCategoryDataSet)
                Case Col1ItemGroup
                    If mItemGroupDataSet Is Nothing Then
                        mQry = " Select H.Code, H.Description From ItemGroup H Order By H.Description "
                        mItemGroupDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                    FSingleSelectForm(Col1ItemGroup, bRowIndex, mItemGroupDataSet)
                Case Col1Item
                    If mItemDataSet Is Nothing Then
                        mQry = " Select H.Code, H.Description From Item H Where H.V_Type = '" & ItemV_Type.Item & "' Order By H.Description "
                        mItemDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                    FSingleSelectForm(Col1Item, bRowIndex, mItemDataSet)
                    Validating_ItemCode(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Tag, bColumnIndex, bRowIndex)
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
                Case Col1Size
                    If mSizeDataSet Is Nothing Then
                        mQry = " Select H.Code, H.Description From Size H Order By H.Description "
                        mSizeDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                    FSingleSelectForm(Col1Size, bRowIndex, mSizeDataSet)
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
    Public Sub FSave()
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim mDescription As String = ""
        Dim mSaleOrderDocId As String = ""
        Dim mV_Type As String = ""
        Dim mV_No As String
        Dim mV_Prefix As String
        Dim mSr As Integer = 0

        If FDataValidation() = False Then Exit Sub



        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            For I = 0 To ReportFrm.DGL1.RowCount - 1
                mV_Type = FGetSettings(SettingFields.GeneratedEntryV_TypeForAadhat, SettingType.General, ReportFrm.DGL1.Item(Col1V_Type, I).Value)
                If mV_Type = "" Then
                    mV_Type = Ncat.SaleOrder
                End If


                If AgL.XNull(ReportFrm.DGL1.Item(Col1SKUCode, I).Value) <> "" Then
                    If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                                From SaleEnquiryMapping With (NoLock)
                                Where DocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & "
                                And Sr = " & Val(ReportFrm.DGL1.Item(Col1Sr, I).Value) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                        mQry = "INSERT INTO SaleEnquiryMapping (DocID, Sr, Item)
                            SELECT " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & ", " & ReportFrm.DGL1.Item(Col1Sr, I).Value & " Sr, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SKUCode, I).Value) & " Item
                            FROM SaleEnquiryDetail L 
                            WHERE L.DocID =" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            AND L.Sr =" & ReportFrm.DGL1.Item(Col1Sr, I).Value & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "INSERT INTO SaleEnquiryMappingSku (DocID, Sr, ItemCategory, ItemGroup, Item, 
                            Dimension1, Dimension2, Dimension3, Dimension4, Size)
                            SELECT " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & ", " & ReportFrm.DGL1.Item(Col1Sr, I).Value & " Sr, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag) & " ItemCategory, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemGroup, I).Tag) & " ItemGroup, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Item, I).Tag) & " Item, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag) & " Dimension1, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag) & " Dimension2, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag) & " Dimension3, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag) & " Dimension4, 
                            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Size, I).Tag) & " Size "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Else
                        mQry = "UPDATE SaleEnquiryMapping 
                            Set 
                            Item = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SKUCode, I).Value) & " 
                            Where DocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            And Sr = " & ReportFrm.DGL1.Item(Col1Sr, I).Value & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "UPDATE SaleEnquiryMappingSku 
                            Set 
                            ItemCategory = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag) & ", 
                            ItemGroup = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemGroup, I).Tag) & ", 
                            Item = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Item, I).Tag) & ", 
                            Dimension1 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag) & ", 
                            Dimension2 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag) & ", 
                            Dimension3 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag) & ", 
                            Dimension4 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag) & ",
                            Size = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Size, I).Tag) & "  
                            Where DocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            And Sr = " & mSr & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If

                    mQry = "SELECT DocID FROM SaleInvoice WITH (Nolock) 
                            WHERE GenDocId =" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & ""
                    mSaleOrderDocId = AgL.XNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)

                    If mSaleOrderDocId = "" Then
                        'mSaleOrderDocId = AgL.GetDocId(mV_Type, CStr(0), CDate(ReportFrm.DGL1.Item(Col1V_Date, I).Value), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                        mSaleOrderDocId = AgL.CreateDocId(AgL, "SaleInvoice", mV_Type, CStr(0), CDate(ReportFrm.DGL1.Item(Col1V_Date, I).Value), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                        mV_No = Val(AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                        mV_Prefix = AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
                        mQry = "INSERT INTO SaleInvoice (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, 
                                ManualRefNo, SaleToParty, BillToParty,  Agent, SaleToPartyName, SaleToPartyAddress, SaleToPartyPinCode, 
                                SaleToPartyCity, SaleToPartyMobile, SaleToPartySalesTaxNo, SaleToPartyDocNo, 
                                SaleToPartyDocDate, Remarks, TermsAndConditions, Status, EntryBy, EntryDate, 
                                SpecialDiscount_Per, SpecialDiscount, DeliveryDate, GenDocId, LockText)
                                SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & AgL.Chk_Text(mV_Type) & ", 
                                " & AgL.Chk_Text(mV_No) & ", H.V_Date, " & AgL.Chk_Text(mV_Prefix) & ", H.Div_Code, 
                                H.Site_Code, H.ManualRefNo, H.SaleToParty, H.SaleToParty As BillToParty, H.Agent, H.SaleToPartyName, 
                                H.SaleToPartyAddress, H.SaleToPartyPinCode, H.SaleToPartyCity, H.SaleToPartyMobile, 
                                H.SaleToPartySalesTaxNo, H.SaleToPartyDocNo, H.SaleToPartyDocDate, H.Remarks, 
                                H.TermsAndConditions, 'Active' Status, EntryBy, EntryDate, 0 SpecialDiscount_Per, 
                                0 SpecialDiscount, H.DeliveryDate, H.DocID As GenDocId, 'Createed From Sale Enquiry.'
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

                        mQry = "INSERT INTO SaleInvoiceDetail (DocID, Sr, Item, Pcs, DocQty, Qty, Unit, UnitMultiplier, 
                            DocDealQty, DealQty, DealUnit, Rate, Amount, Remark, GenDocId, GenDocIdSr, SaleInvoice, SaleInvoiceSr)
                            SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & mSr & " Sr, 
                            Sem.Item As Item, 
                            L.Qty As Pcs, L.Qty As DocQty, L.Qty As Qty, 'Pcs' As Unit, 1 As UnitMultiplier, 1 As DocDealQty, 
                            1 As DealQty, 'Pcs' As DealUnit, L.Rate, L.Amount, L.Remark, 
                            L.Docid GenDocId, L.Sr GenDocIdSr,
                            " & AgL.Chk_Text(mSaleOrderDocId) & " As SaleInvoice, " & mSr & " SaleInvoiceSr
                            FROM SaleEnquiryDetail L 
                            LEFT JOIN SaleEnquiryMapping Sem oN L.DocId = Sem.Docid And L.Sr = Sem.Sr
                            WHERE L.DocID =" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            AND L.Sr =" & ReportFrm.DGL1.Item(Col1Sr, I).Value & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Insert Into SaleInvoiceDetailSku
                                (DocId, Sr, ItemCategory, ItemGroup, Item, Dimension1, 
                                Dimension2, Dimension3, Dimension4, Size) "
                        mQry += " Values(" & AgL.Chk_Text(mSaleOrderDocId) & ", " & mSr & ", " &
                                " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag) & ", " &
                                " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemGroup, I).Tag) & ", " &
                                " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Item, I).Tag) & ", " &
                                " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag) & ", " &
                                " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag) & ", " &
                                " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag) & ", " &
                                " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag) & ", " &
                                " " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Size, I).Tag) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Else
                        mQry = "UPDATE SaleInvoiceDetail 
                            Set 
                            Item = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SKUCode, I).Value) & " 
                            Where GenDocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            And GenDocIdSr = " & ReportFrm.DGL1.Item(Col1Sr, I).Value & " "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " Select L.DocId, L.Sr From SaleInvoiceDetail L With (NoLock)
                            Where L.GenDocId = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1DocId, I).Value) & " 
                            And GenDocIdSr = " & ReportFrm.DGL1.Item(Col1Sr, I).Value & " "
                        Dim DtSaleInvoiceDetail As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

                        mQry = "Update SaleInvoiceDetailSku " &
                                " SET ItemCategory = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag) & ", " &
                                " ItemGroup = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ItemGroup, I).Tag) & ", " &
                                " Item = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Item, I).Tag) & ", " &
                                " Dimension1 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag) & ", " &
                                " Dimension2 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag) & ", " &
                                " Dimension3 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag) & ", " &
                                " Dimension4 = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag) & ", " &
                                " Size = " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Size, I).Tag) & " " &
                                " Where DocId = '" & AgL.XNull(DtSaleInvoiceDetail.Rows(0)("DocId")) & "' " &
                                " And Sr = " & AgL.VNull(DtSaleInvoiceDetail.Rows(0)("Sr")) & " "
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

            MsgBox("Process Completed...!", MsgBoxStyle.Information)
            ReportFrm.DGL1.DataSource = Nothing
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        FSave()
    End Sub
    Private Sub Validating_ItemCode(ItemCode As String, ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtItem As DataTable = Nothing
        Try
            mQry = "Select I.Code, I.Description, I.ManualCode, I.Unit, I.Specification, I.ItemType
                    , I.ItemCategory, IC.Description as ItemCategoryName
                    , I.ItemGroup, IG.Description as ItemGroupName
                    , I.Dimension1, D1.Description as Dimension1Name
                    , I.Dimension2, D2.Description as Dimension2Name
                    , I.Dimension3, D3.Description as Dimension3Name
                    , I.Dimension4, D4.Description as Dimension4Name
                    , I.Size, Size.Description as SizeName 
                    From Item I  With (NoLock)
                    Left Join Item IC With (NoLock) On I.ItemCategory = IC.Code
                    Left Join Item IG With (NoLock) On I.ItemGroup = IG.Code
                    Left Join Item D1 With (NoLock) On I.Dimension1 = D1.Code
                    Left Join Item D2 With (NoLock) On I.Dimension2 = D2.Code
                    Left Join Item D3 With (NoLock) On I.Dimension3 = D3.Code
                    Left Join Item D4 With (NoLock) On I.Dimension4 = D1.Code
                    Left Join Item Size With (NoLock) On I.Size = Size.Code
                    Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
                ReportFrm.DGL1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                ReportFrm.DGL1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))

                If AgL.XNull(DtItem.Rows(0)("ItemGroup")) <> "" Then
                    ReportFrm.DGL1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                    ReportFrm.DGL1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                End If

                ReportFrm.DGL1.Item(Col1MItemSpecification, mRow).Value = AgL.XNull(DtItem.Rows(0)("Specification"))
                If AgL.XNull(DtItem.Rows(0)("Dimension1")) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                    ReportFrm.DGL1.Item(Col1Dimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension2")) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                    ReportFrm.DGL1.Item(Col1Dimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension3")) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension3, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                    ReportFrm.DGL1.Item(Col1Dimension3, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension4")) <> "" Then
                    ReportFrm.DGL1.Item(Col1Dimension4, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                    ReportFrm.DGL1.Item(Col1Dimension4, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Size")) <> "" Then
                    ReportFrm.DGL1.Item(Col1Size, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
                    ReportFrm.DGL1.Item(Col1Size, mRow).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
                End If




                ReportFrm.DGL1.Item(Col1MItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                ReportFrm.DGL1.Item(Col1MItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))
                ReportFrm.DGL1.Item(Col1MItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                ReportFrm.DGL1.Item(Col1MItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                ReportFrm.DGL1.Item(Col1MItemSpecification, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Specification"))
                ReportFrm.DGL1.Item(Col1MDimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                ReportFrm.DGL1.Item(Col1MDimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                ReportFrm.DGL1.Item(Col1MDimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                ReportFrm.DGL1.Item(Col1MDimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                ReportFrm.DGL1.Item(Col1MDimension3, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                ReportFrm.DGL1.Item(Col1MDimension3, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                ReportFrm.DGL1.Item(Col1MDimension4, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                ReportFrm.DGL1.Item(Col1MDimension4, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                ReportFrm.DGL1.Item(Col1MSize, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
                ReportFrm.DGL1.Item(Col1MSize, mRow).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub
    Private Function FDataValidation() As Boolean
        FDataValidation = False

        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            If AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategory, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroup, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4, I).Value) <> "" _
                    Or AgL.XNull(ReportFrm.DGL1.Item(Col1Size, I).Value) <> "" _
                   Then
                ReportFrm.DGL1.Item(Col1SKUCode, I).Value = ClsMain.FGetSKUCode(I + 1, ItemTypeCode.InternalProduct _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategory, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategory, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroup, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroup, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Item, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1Size, I).Tag), AgL.XNull(ReportFrm.DGL1.Item(Col1Size, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MItemCategory, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MItemGroup, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MItemSpecification, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MDimension1, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MDimension2, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MDimension3, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MDimension4, I).Value) _
                               , AgL.XNull(ReportFrm.DGL1.Item(Col1MSize, I).Value)
                               )
                If AgL.XNull(ReportFrm.DGL1.Item(Col1SKUCode, I).Value) = "" Then
                    MsgBox("Item Combination is not allowed...!", MsgBoxStyle.Information)
                    FDataValidation = False
                    Exit Function
                End If
            End If
        Next

        FDataValidation = True
    End Function
    Private Function FGetSettings(FieldName As String, SettingType As String, V_Type As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode,
                    VoucherCategory.Sales, Ncat.SaleEnquiry, V_Type, "", "")
        FGetSettings = mValue
    End Function
End Class
