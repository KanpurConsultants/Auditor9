Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmSaleEnquiryMapping
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const ColSNo As String = "S.No."
    Protected Const Col1SaleEnquiryDocId As String = "Enquiry No"
    Protected Const Col1SaleEnquiryDocType As String = "SaleEnquiryDocType"
    Protected Const Col1SaleEnquiryDocIdSr As String = "SaleEnquiryDocIdSr"
    Protected Const Col1SaleEnquiryDocDate As String = "Enquiry Date"
    Protected Const Col1SaleEnquiryNo As String = "Party Enquiry No"
    Protected Const Col1SaleEnquiryDate As String = "Party Enquiry Date"
    Protected Const Col1Party As String = "Party"
    Protected Const Col1PartyItem As String = "Party Item"
    Protected Const Col1PartyItemSpecification1 As String = "Specification1"
    Protected Const Col1PartyItemSpecification2 As String = "Specification2"
    Protected Const Col1PartyItemSpecification3 As String = "Specification3"
    Protected Const Col1PartyItemSpecification4 As String = "Specification4"
    Protected Const Col1PartyItemSpecification5 As String = "Specification5"
    Protected Const Col1ItemType As String = "Item Type"
    Protected Const Col1ItemCategory As String = "Item Category"
    Protected Const Col1ItemGroup As String = "Item Group"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Dimension1 As String = "Dimension1"
    Protected Const Col1Dimension2 As String = "Dimension2"
    Protected Const Col1Dimension3 As String = "Dimension3"
    Protected Const Col1Dimension4 As String = "Dimension4"
    'Protected Const Col1StandardSize As String = "Standard Size"
    Protected Const Col1ManufacturingSize As String = "Manufacturing Size"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1Remark As String = "Remark"


    Dim mQry As String = ""
    Dim EntryNCat As String = ""
    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String

    Public Sub New(ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        'Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        'Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        'mQry = "Select H.* from SaleInvoiceSetting H  With (NoLock) Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') Or H.V_Type Is Null "
        'DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        'If DtV_TypeSettings.Rows.Count = 0 Then
        '    MsgBox("Voucher Type Settings Not Found")
        'End If
    End Sub

    Public Sub Ini_Grid()
        Dim I As Integer = 0
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 50, 5, ColSNo, True, True, False, , DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SaleEnquiryDocType, 140, 0, Col1SaleEnquiryDocType, False, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SaleEnquiryDocId, 80, 0, Col1SaleEnquiryDocId, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SaleEnquiryDocDate, 100, 0, Col1SaleEnquiryDocDate, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SaleEnquiryDocIdSr, 80, 0, Col1SaleEnquiryDocIdSr, False, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SaleEnquiryNo, 100, 0, Col1SaleEnquiryNo, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SaleEnquiryDate, 100, 0, Col1SaleEnquiryDate, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Party, 200, 0, Col1Party, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1PartyItem, 100, 0, Col1PartyItem, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification1, 80, 0, Col1PartyItemSpecification1, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification2, 80, 0, Col1PartyItemSpecification2, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification3, 80, 0, Col1PartyItemSpecification3, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification4, 80, 0, Col1PartyItemSpecification4, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1PartyItemSpecification5, 80, 0, Col1PartyItemSpecification5, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1ItemType, 70, 0, Col1ItemType, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 70, 0, Col1ItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 70, 0, Col1ItemGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 70, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 70, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 70, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 70, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 70, 0, Col1Dimension4, True, False)
            '.AddAgTextColumn(Dgl1, Col1StandardSize, 70, 0, Col1StandardSize, True, False)
            .AddAgTextColumn(Dgl1, Col1ManufacturingSize, 70, 0, Col1ManufacturingSize, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 70, 0, Col1Specification, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 70, 0, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 70
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgAllowFind = True

        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)

        Dgl1.Name = "Dgl1"
        Dgl1.BackgroundColor = Color.White
        Dgl1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        For I = 0 To Dgl1.Columns.Count - 1
            Dgl1.Columns(I).ContextMenuStrip = MnuOptions
        Next
    End Sub
    Private Sub ApplyUISettings(NCAT As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Try

            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='" & Me.Name & "' And NCat = '" & NCAT & "' 
                    And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Columns(J).Name Then
                            Dgl1.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Columns(J).HeaderText = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                        End If
                    Next
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub

    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        Ini_Grid()
        MovRec()
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub ProcSave()
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim mDescription As String = ""
        'Dim mItemCode As String = ""
        Dim mSaleOrderDocId As String = ""
        Dim mV_Type As String = Ncat.SaleOrder
        Dim mV_No As String
        Dim mV_Prefix As String

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            'mQry = "UPDATE " + TableName + " Set " + FieldName + " = " + "'" + Value + "'" + " Where " & PrimaryKey & " = " + "'" + Code + "'"
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Item(Col1Item, I).Tag <> "" Then
                    'mDescription = Dgl1.Item(Col1Dimension1, I).Value & " [" & Dgl1.Item(Col1ItemGroup, I).Value & " | " & Dgl1.Item(Col1ItemCategory, I).Value & "]-" & Dgl1.Item(Col1ManufacturingSize, I).Value & "-" & Dgl1.Item(Col1Dimension2, I).Value & ""

                    'mQry = "SELECT Code FROM Item  WITH (Nolock)  WHERE Description =" & AgL.Chk_Text(mDescription) & ""
                    'mItemCode = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar

                    'If mItemCode = "" Then
                    '    mItemCode = AgL.GetMaxId("Item", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                    '    mQry = "INSERT INTO Item (Code, Description, DisplayName, Unit, DealQty, DealUnit, ItemGroup, ItemCategory, ItemType, EntryBy, EntryDate, EntryType, EntryStatus, Status, Div_Code, Gross_Weight, IsSystemDefine, IsRestricted_InTransaction, IsMandatory_UnitConversion, ShowItemInOtherDivisions, MRP, DiscountPerPurchase, DiscountPerSale, AdditionPerSale, MaintainStockYn, Default_AdditionalDiscountPerSale, Default_AdditionPerSale, Default_DiscountPerPurchase, Default_AdditionalDiscountPerPurchase, Default_MarginPer, Dimension1, Dimension2, Dimension3, Dimension4, Size) " &
                    '            "VALUES (" & AgL.Chk_Text(mItemCode) & ",  " & AgL.Chk_Text(mDescription) & ", " & AgL.Chk_Text(mDescription) & ", 'Pcs', NULL, NULL, " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1ItemType, I).Tag) & ", '" & AgL.PubUserName & "', GetDate(), 'Add', 'Open', 'Active', '" & AgL.PubDivCode & "', 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1ManufacturingSize, I).Tag) & ") "
                    '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    '    mQry = "SELECT Code FROM Item  WITH (Nolock)  WHERE Description =" & AgL.Chk_Text(mDescription) & ""
                    '    mItemCode = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                    'End If

                    'Dgl1.Item(Col1Item, I).Tag = mItemCode
                    mQry = "SELECT DocID  FROM SaleInvoice WITH (Nolock) WHERE GenDocId =" & AgL.Chk_Text(Dgl1.Item(Col1SaleEnquiryDocId, I).Tag) & " AND GenDocIdType  =" & AgL.Chk_Text(Dgl1.Item(Col1SaleEnquiryDocType, I).Tag) & ""
                    mSaleOrderDocId = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar

                    If mSaleOrderDocId = "" Then
                        'mSaleOrderDocId = AgL.GetDocId(mV_Type, CStr(0), CDate(Dgl1.Item(Col1SaleEnquiryDocDate, I).Value), AgL.GcnRead, AgL.PubDivCode, AgL.PubSiteCode)
                        mSaleOrderDocId = AgL.CreateDocId(AgL, "SaleInvoice", mV_Type, CStr(0), CDate(Dgl1.Item(Col1SaleEnquiryDocDate, I).Value), AgL.GcnRead, AgL.PubDivCode, AgL.PubSiteCode)
                        mV_No = Val(AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                        mV_Prefix = AgL.DeCodeDocID(mSaleOrderDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
                        mQry = "INSERT INTO SaleInvoice (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ManualRefNo, SaleToParty,  Agent, SaleToPartyName, SaleToPartyAddress, SaleToPartyPinCode, SaleToPartyCity, SaleToPartyMobile, SaleToPartySalesTaxNo, SaleToPartyDocNo, SaleToPartyDocDate, Remarks, TermsAndConditions, Status, EntryBy, EntryDate, SpecialDiscount_Per, SpecialDiscount, DeliveryDate, GenDocId, GenDocIdType)
                                SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & AgL.Chk_Text(mV_Type) & ", " & AgL.Chk_Text(mV_No) & ", H.V_Date, " & AgL.Chk_Text(mV_Prefix) & ", H.Div_Code, H.Site_Code, H.ManualRefNo, H.SaleToParty, H.Agent, H.SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyPinCode, H.SaleToPartyCity, H.SaleToPartyMobile, H.SaleToPartySalesTaxNo, H.SaleToPartyDocNo, H.SaleToPartyDocDate, H.Remarks, H.TermsAndConditions, 'Active' Status, EntryBy, EntryDate, 0 SpecialDiscount_Per, 0 SpecialDiscount, H.DeliveryDate, H.DocID  GenDocId, H.V_Type GenDocIdType
                                FROM SaleEnquiry H WHERE H.DocID =" & AgL.Chk_Text(Dgl1.Item(Col1SaleEnquiryDocId, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        AgL.UpdateVoucherCounter(mSaleOrderDocId, CDate(Dgl1.Item(Col1SaleEnquiryDocDate, I).Value), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)

                        mQry = "SELECT DocID  FROM SaleInvoice WITH (Nolock) WHERE GenDocId =" & AgL.Chk_Text(Dgl1.Item(Col1SaleEnquiryDocId, I).Tag) & " AND GenDocIdType  =" & AgL.Chk_Text(Dgl1.Item(Col1SaleEnquiryDocType, I).Tag) & ""
                        mSaleOrderDocId = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                    End If

                    mQry = "INSERT INTO SaleInvoiceDetail (DocID, Sr, Item, Specification, Dimension1, Dimension2, Dimension3, Dimension4, Pcs, DocQty, Qty, Unit, UnitMultiplier, DocDealQty, DealQty, DealUnit, Rate, Amount, Remark, GenDocId, GenDocIdType, GenDocIdSr)
                            SELECT " & AgL.Chk_Text(mSaleOrderDocId) & ", " & Dgl1.Item(Col1SaleEnquiryDocIdSr, I).Value & " Sr, " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & " Item, " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & " Specification, " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & " Dimension1, " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & " Dimension2, " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & " Dimension3, " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, I).Tag) & " Dimension4, 
                            L.Qty Pcs, L.Qty DocQty, L.Qty Qty, 'Pcs' Unit, 1 UnitMultiplier, 1 DocDealQty, 1 DealQty, 'Pcs' DealUnit, L.Rate, L.Amount, L.Remark, " & AgL.Chk_Text(Dgl1.Item(Col1SaleEnquiryDocId, I).Tag) & " GenDocId, " & AgL.Chk_Text(Dgl1.Item(Col1SaleEnquiryDocType, I).Tag) & " GenDocIdType, " & Dgl1.Item(Col1SaleEnquiryDocIdSr, I).Value & " GenDocIdSr
                            FROM SaleEnquiryDetail L WHERE L.DocID =" & AgL.Chk_Text(Dgl1.Item(Col1SaleEnquiryDocId, I).Tag) & " AND L.Sr =" & Dgl1.Item(Col1SaleEnquiryDocIdSr, I).Value & " "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
            MovRec()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemCategory
                    Dgl1.AgHelpDataSet(Col1ItemGroup) = Nothing

                Case Col1ItemGroup
                    Dgl1.AgHelpDataSet(Col1Item) = Nothing

                Case Col1Item
                    Validating_ItemCode(Dgl1.Item(mColumnIndex, mRowIndex).Tag, mColumnIndex, mRowIndex)
            End Select

            Dgl1.CurrentCell.Style.BackColor = Color.BurlyWood
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Validating_ItemCode(ItemCode As String, ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtItem As DataTable = Nothing
        Try

            mQry = "SELECT I.Code, I.Dimension1, I.Dimension2, I.Dimension3, I.Dimension4, I.Size,
                    D1.Description AS  Dimension1Name, D2.Description AS  Dimension2Name,D3.Description AS  Dimension3Name,D4.Description AS  Dimension4Name,S.Description AS  SizeName 
                    FROM Item  I WITH (Nolock)  
                    LEFT JOIN Dimension1 D1 WITH (Nolock) ON D1.Code = I.Dimension1 
                    LEFT JOIN Dimension2 D2 WITH (Nolock) ON D2.Code = I.Dimension2 
                    LEFT JOIN Dimension3 D3 WITH (Nolock) ON D3.Code = I.Dimension3 
                    LEFT JOIN Dimension4 D4 WITH (Nolock)ON D4.Code = I.Dimension4
                    LEFT JOIN size S WITH (Nolock) ON S.Code = I.Size
                    Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
                Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                Dgl1.Item(Col1Dimension3, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                Dgl1.Item(Col1Dimension3, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                Dgl1.Item(Col1Dimension4, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                Dgl1.Item(Col1Dimension4, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                Dgl1.Item(Col1ManufacturingSize, mRow).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
                Dgl1.Item(Col1ManufacturingSize, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Public Sub MovRec()
        ApplyUISettings(EntryNCat)
        GetPendingEnquiryToMap()
    End Sub
    Public Sub GetPendingEnquiryToMap()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        'mQry = "SELECT H.DocID, H.ManualRefNo, H.V_Date, H.V_Type, H.SaleToPartyDocNo, H.SaleToPartyDocDate, H.SaleToPartyName, L.Sr,L.PartyItem, L.PartyItemSpecification1, L.PartyItemSpecification2, L.PartyItemSpecification3, L.PartyItemSpecification4, L.PartyItemSpecification5    
        '            FROM SaleEnquiry H WITH (Nolock)
        '            LEFT JOIN saleenquirydetail L WITH (Nolock) ON L.DocID = H.DocID 
        '            LEFT JOIN Saleinvoicedetail SID WITH (Nolock) ON SID.s = H.DocID AND SID.GenDocIdType = H.V_Type AND SID.GenDocIdSr = L.Sr 
        '            WHERE SID.DocID IS NULL
        '            Order By H.V_Date, H.V_No, L.Sr "

        mQry = "SELECT H.DocID, H.ManualRefNo, H.V_Date, H.V_Type, H.SaleToPartyDocNo, H.SaleToPartyDocDate, H.SaleToPartyName, L.Sr,L.PartyItem, L.PartyItemSpecification1, L.PartyItemSpecification2, L.PartyItemSpecification3, L.PartyItemSpecification4, L.PartyItemSpecification5    
                    FROM SaleEnquiry H WITH (Nolock)
                    LEFT JOIN saleenquirydetail L WITH (Nolock) ON L.DocID = H.DocID 
                    Order By H.V_Date, H.V_No, L.Sr "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                Dgl1.Item(Col1SaleEnquiryDocType, I).Value = AgL.XNull(DtTemp.Rows(I)("DocID"))
                Dgl1.Item(Col1SaleEnquiryDocId, I).Tag = AgL.XNull(DtTemp.Rows(I)("DocID"))
                Dgl1.Item(Col1SaleEnquiryDocId, I).Value = AgL.XNull(DtTemp.Rows(I)("ManualRefNo"))
                Dgl1.Item(Col1SaleEnquiryDocType, I).Tag = AgL.XNull(DtTemp.Rows(I)("V_Type"))
                Dgl1.Item(Col1SaleEnquiryDocIdSr, I).Value = AgL.XNull(DtTemp.Rows(I)("Sr"))
                Dgl1.Item(Col1SaleEnquiryDocDate, I).Value = AgL.XNull(DtTemp.Rows(I)("V_Date"))
                Dgl1.Item(Col1SaleEnquiryNo, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleToPartyDocNo"))
                Dgl1.Item(Col1SaleEnquiryDate, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleToPartyDocDate")).ToString()
                Dgl1.Item(Col1Party, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleToPartyName")).ToString()
                Dgl1.Item(Col1PartyItem, I).Value = AgL.XNull(DtTemp.Rows(I)("PartyItem")).ToString()
                Dgl1.Item(Col1PartyItemSpecification1, I).Value = AgL.XNull(DtTemp.Rows(I)("PartyItemSpecification1")).ToString()
                Dgl1.Item(Col1PartyItemSpecification2, I).Value = AgL.XNull(DtTemp.Rows(I)("PartyItemSpecification2")).ToString()
                Dgl1.Item(Col1PartyItemSpecification3, I).Value = AgL.XNull(DtTemp.Rows(I)("PartyItemSpecification3")).ToString()
                Dgl1.Item(Col1PartyItemSpecification4, I).Value = AgL.XNull(DtTemp.Rows(I)("PartyItemSpecification4")).ToString()
                Dgl1.Item(Col1PartyItemSpecification5, I).Value = AgL.XNull(DtTemp.Rows(I)("PartyItemSpecification5")).ToString()


            Next
            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
        End If
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name

                Case Col1ItemCategory
                    If Dgl1.AgHelpDataSet(Col1ItemCategory) Is Nothing Then
                        mQry = "SELECT Code, Description  FROM ItemCategory Order By Description"
                        Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1ItemGroup
                    If Dgl1.AgHelpDataSet(Col1ItemGroup) Is Nothing Then
                        If Dgl1.Item(Col1ItemCategory, bRowIndex).Tag <> "" Then
                            mQry = "SELECT Code, Description  FROM ItemGroup WHERE ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, bRowIndex).Tag) & " Order By Description"
                        Else
                            mQry = "SELECT Code, Description  FROM ItemGroup Order By Description"
                        End If
                        Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
                    End If



                Case Col1Item
                    'If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                    '    If Dgl1.Item(Col1ItemGroup, bRowIndex).Tag <> "" Then
                    '        mQry = "SELECT Code, Description  FROM Item WHERE ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, bRowIndex).Tag) & " Order By Description"
                    '    Else
                    '        mQry = "SELECT Code, Description  FROM Item Order By Description"
                    '    End If
                    '    Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
                    'End If
                Case Col1Item
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem(Dgl1.CurrentCell.RowIndex)
                        End If
                    ElseIf e.KeyCode = Keys.Insert Then
                        FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    End If

                Case Col1Dimension1
                    If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                        mQry = "SELECT Code, Description  FROM Dimension1 Order By Description"
                        Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Dimension2
                    If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                        mQry = "SELECT Code, Description  FROM Dimension2 Order By Description"
                        Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Dimension3
                    If Dgl1.AgHelpDataSet(Col1Dimension3) Is Nothing Then
                        mQry = "SELECT Code, Description  FROM Dimension3 Order By Description"
                        Dgl1.AgHelpDataSet(Col1Dimension3) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Dimension4
                    If Dgl1.AgHelpDataSet(Col1Dimension4) Is Nothing Then
                        mQry = "SELECT Code, Description  FROM Dimension4 Order By Description"
                        Dgl1.AgHelpDataSet(Col1Dimension4) = AgL.FillData(mQry, AgL.GCn)
                    End If

                'Case Col1StandardSize
                '    If Dgl1.AgHelpDataSet(Col1StandardSize) Is Nothing Then
                '        mQry = "SELECT L.Code, L.Description  FROM Size L Order By Description"
                '        Dgl1.AgHelpDataSet(Col1StandardSize) = AgL.FillData(mQry, AgL.GCn)
                '    End If

                Case Col1ManufacturingSize
                    If Dgl1.AgHelpDataSet(Col1ManufacturingSize) Is Nothing Then
                        mQry = "SELECT L.Code, L.Description  FROM Size L Order By Description"
                        Dgl1.AgHelpDataSet(Col1ManufacturingSize) = AgL.FillData(mQry, AgL.GCn)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub

    Private Sub FrmReportWindow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Dgl1_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnDisplayIndexChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl1_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnWidthChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub MnuExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuExportToExcel.Click, MnuFreezeColumns.Click
        Dim FileName As String = ""
        Dim bColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Select Case sender.Name
            Case MnuExportToExcel.Name
                If MsgBox("Want to Export Grid Data", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Export Grid?...") = vbNo Then Exit Sub
                FileName = AgControls.Export.GetFileName(My.Computer.FileSystem.SpecialDirectories.Desktop)
                If FileName.Trim <> "" Then
                    Call AgControls.Export.exportExcel(Dgl1, FileName, Dgl1.Handle)
                End If

            Case MnuFreezeColumns.Name
                If MnuFreezeColumns.Checked Then
                    Dgl1.Columns(bColumnIndex).Frozen = True
                Else
                    For I As Integer = 0 To bColumnIndex
                        Dgl1.Columns(I).Frozen = False
                    Next
                End If
        End Select
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        ProcSave()
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell IsNot Nothing Then
            If e.Control And e.KeyCode = Keys.D And Dgl1.Rows(Dgl1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                sender.CurrentRow.Visible = False
            End If
        End If




        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If Dgl1.CurrentCell IsNot Nothing Then
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode = Keys.Insert Then
                        FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    End If


            End Select
        End If

    End Sub

    Private Sub FOpenItemMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""


        Dim DtTemp As DataTable = Nothing

        Dim objMdi As New MDIMain
        Dim StrUserPermission As String
        Dim DTUP As DataTable

        StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, objMdi.MnuItemMaster.Name, objMdi.MnuItemMaster.Text, DTUP)

        Dim frmObj As FrmItemMaster

        frmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.Item)
        frmObj.EntryPointIniMode = AgTemplate.ClsMain.EntryPointIniMode.Insertion
        frmObj.StartPosition = FormStartPosition.CenterParent
        frmObj.IniGrid()
        frmObj.Dgl1(FrmItemMaster.Col1LastValue, FrmItemMaster.rowItemCategory).Value = Dgl1.Item(Col1ItemCategory, RowIndex).Value
        frmObj.Dgl1(FrmItemMaster.Col1LastValue, FrmItemMaster.rowItemCategory).Tag = Dgl1.Item(Col1ItemCategory, RowIndex).Tag
        frmObj.Dgl1(FrmItemMaster.Col1LastValue, FrmItemMaster.rowItemGroup).Value = Dgl1.Item(Col1ItemGroup, RowIndex).Value
        frmObj.Dgl1(FrmItemMaster.Col1LastValue, FrmItemMaster.rowItemGroup).Tag = Dgl1.Item(Col1ItemGroup, RowIndex).Tag
        frmObj.ShowDialog()
        bItemCode = frmObj.mSearchCode
        frmObj = Nothing






        'bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Master", TxtV_Type.Tag)
        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1Remark, RowIndex)
        'FCreateHelpItem(Dgl1.Columns(ColumnIndex).Name)
        FCreateHelpItem(0)
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Item  With (NoLock) Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemCode(Dgl1.Item(ColumnIndex, RowIndex).Tag, ColumnIndex, RowIndex)
        Dgl1.CurrentCell = Dgl1.Item(Col1Item, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub FCreateHelpItem(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        'If DtV_TypeSettings.Rows.Count > 0 Then
        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
        '        strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
        '        strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
        '    End If

        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
        '        strCond += " And CharIndex('+' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
        '        strCond += " And CharIndex('-' || I.ItemGroup,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') <= 0 "
        '    End If

        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")) <> "" Then
        '        strCond += " And CharIndex('+' || I.ItemCategory,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")) & "') > 0 "
        '        strCond += " And CharIndex('-' || I.ItemCategory,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemCategory")) & "') <= 0 "
        '    End If

        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")) <> "" Then
        '        strCond += " And CharIndex('+' || I.V_Type,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")) & "') > 0 "
        '        strCond += " And CharIndex('-' || I.V_Type,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemV_Type")) & "') <= 0 "
        '    Else
        '        strCond += " And I.V_Type = 'ITEM' "
        '    End If


        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
        '        strCond += " And CharIndex('+' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
        '        strCond += " And CharIndex('-' || I.Code,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') <= 0 "
        '    End If
        'End If

        'If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
        '    strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1)  "
        'End If

        'If Dgl1.Item(Col1ItemCategory, RowIndex).Value <> "" And UserMovedOverItemCategory Then
        '    strCond += " And I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, RowIndex).Tag & "' "
        'End If

        'If Dgl1.Item(Col1ItemGroup, RowIndex).Value <> "" And UserMovedOverItemGroup Then
        '    strCond += " And I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, RowIndex).Tag & "' "
        'End If

        'mQry = "SELECT I.Code, I.Description, I.ManualCode,  
        '                I.Unit, I.PurchaseRate as Rate, I.SalesTaxPostingGroup , 
        '                I.DealQty As UnitMultiplier, I.DealUnit, 
        '                U.DecimalPlaces As QtyDecimalPlaces, U.showdimensiondetailInPurchase, U1.DecimalPlaces As DealDecimalPlaces, I.Specification
        '                FROM Item I  With (NoLock)
        '                Left JOIN Unit U  With (NoLock) On I.Unit = U.Code
        '                LEFT JOIN Unit U1  With (NoLock) On I.DealUnit = U1.Code 
        '                Where I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' 
        '                And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond


        'mQry += " UNION ALL "
        'mQry += "SELECT I.Code, I.Description, I.ManualCode,  
        '                I.Unit, I.PurchaseRate as Rate, I.SalesTaxPostingGroup , 
        '                I.DealQty As UnitMultiplier, I.DealUnit, 
        '                U.DecimalPlaces As QtyDecimalPlaces, U.showdimensiondetailInPurchase, U1.DecimalPlaces As DealDecimalPlaces, I.Specification
        '                FROM Item I  With (NoLock)
        '                Left JOIN Unit U  With (NoLock) On I.Unit = U.Code
        '                LEFT JOIN Unit U1  With (NoLock) On I.DealUnit = U1.Code 
        '                Where I.ItemType = '" & ItemTypeCode.ServiceProduct & "' 
        '                And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "


        'Dgl1.AgHelpDataSet(Col1Item, 7) = AgL.FillData(mQry, AgL.GCn)

        If Dgl1.Item(Col1ItemGroup, RowIndex).Tag <> "" Then
            mQry = "SELECT Code, Description  FROM Item WHERE ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, RowIndex).Tag) & " Order By Description"
        Else
            mQry = "SELECT Code, Description  FROM Item Order By Description"
        End If
        Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
    End Sub

End Class