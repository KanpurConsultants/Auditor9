Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields
Public Class FrmPackingDetail
    Dim mQry As String = ""

    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = "*"
    Public Const Col1Value As String = "Value"



    Public Const rowBarcode As Integer = 0
    Public Const rowItem As Integer = 1
    Public Const rowDimension1 As Integer = 2
    Public Const rowDimension2 As Integer = 3
    Public Const rowDimension3 As Integer = 4
    Public Const rowDimension4 As Integer = 5
    Public Const rowLotNo As Integer = 6
    Public Const rowBaleNo As Integer = 7
    Public Const rowPartyItem As Integer = 8
    Public Const rowPartyItemSpecification1 As Integer = 9
    Public Const rowPartyItemSpecification2 As Integer = 10
    Public Const rowPartyItemSpecification3 As Integer = 11
    Public Const rowPartyItemSpecification4 As Integer = 12
    Public Const rowQty As Integer = 13
    Public Const rowLength As Integer = 14
    Public Const rowWidth As Integer = 15
    Public Const rowDealQty As Integer = 16
    Public Const rowUnitMultiplier As Integer = 17
    Public Const rowDealUnit As Integer = 18
    Public Const rowWeight As Integer = 19
    Public Const rowGrossWeight As Integer = 20
    Public Const rowRemark As Integer = 21
    Public Const rowSaleOrder As Integer = 22
    Public Const rowSaleOrderDocId As Integer = 23


    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mToQtyDecimalPlace As Integer
    Dim mAcGroupNature As String
    Dim mDivisionCode As String
    Dim mSiteCode As String
    Dim mPackingDocId As String
    Dim mPackingDocIdSr As Integer
    Dim mGodown As String
    Dim mDealUnit As String
    Dim mNcat As String
    Dim mObjFrmPacking As FrmPacking
    Public Property objFrmPacking() As FrmPacking
        Get
            objFrmPacking = mObjFrmPacking
        End Get
        Set(ByVal value As FrmPacking)
            mObjFrmPacking = value
        End Set
    End Property

    Public Property NCat() As String
        Get
            NCat = mNcat
        End Get
        Set(ByVal value As String)
            mNcat = value
        End Set
    End Property

    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property

    Public Property DivisionCode() As String
        Get
            DivisionCode = mDivisionCode
        End Get
        Set(ByVal value As String)
            mDivisionCode = value
        End Set
    End Property

    Public Property SiteCode() As String
        Get
            SiteCode = mSiteCode
        End Get
        Set(ByVal value As String)
            mSiteCode = value
        End Set
    End Property
    Public Property Godown() As String
        Get
            Godown = mGodown
        End Get
        Set(ByVal value As String)
            mGodown = value
        End Set
    End Property
    Public Property DealUnit() As String
        Get
            DealUnit = mDealUnit
        End Get
        Set(ByVal value As String)
            mDealUnit = value
        End Set
    End Property
    Public Property PackingDocId() As String
        Get
            PackingDocId = mPackingDocId
        End Get
        Set(ByVal value As String)
            mPackingDocId = value
        End Set
    End Property
    Public Property PackingDocIdSr() As Integer
        Get
            PackingDocIdSr = mPackingDocIdSr
        End Get
        Set(ByVal value As Integer)
            mPackingDocIdSr = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    AgL.FPaintForm(Me, e, 0)
    'End Sub

    Public Sub ApplyPackingSettings()
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer
        mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName= '" & Me.Name & "'  And NCat = '" & NCat & "' And GridName ='" & Dgl1.Name & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                For J = 0 To Dgl1.Rows.Count - 1
                    If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Item(Col1Head, J).Value Then
                        Dgl1.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                        If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
                        Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                        If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                            Dgl1.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                        End If
                    End If
                Next
            Next
        End If
        If mDgl1RowCount = 0 Then
            Dgl1.Visible = False
        Else
            Dgl1.Visible = True
        End If
    End Sub

    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 500, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.TabIndex = Pnl1.TabIndex
        AgL.GridDesign(Dgl1)
        Dgl1.Name = "Dgl1"

        Dgl1.Rows.Add(24)
        Dgl1.Item(Col1Head, rowBarcode).Value = FrmPacking.Col1Barcode
        Dgl1.Item(Col1Head, rowItem).Value = FrmPacking.Col1Item
        Dgl1.Item(Col1Head, rowDimension1).Value = FrmPacking.Col1Dimension1
        Dgl1.Item(Col1Head, rowDimension2).Value = FrmPacking.Col1Dimension2
        Dgl1.Item(Col1Head, rowDimension3).Value = FrmPacking.Col1Dimension3
        Dgl1.Item(Col1Head, rowDimension4).Value = FrmPacking.Col1Dimension4
        Dgl1.Item(Col1Head, rowLotNo).Value = FrmPacking.Col1LotNo
        Dgl1.Item(Col1Head, rowBaleNo).Value = FrmPacking.Col1BaleNo
        Dgl1.Item(Col1Head, rowPartyItem).Value = FrmPacking.Col1PartyItem
        Dgl1.Item(Col1Head, rowPartyItemSpecification1).Value = FrmPacking.Col1PartyItemSpecification1
        Dgl1.Item(Col1Head, rowPartyItemSpecification2).Value = FrmPacking.Col1PartyItemSpecification2
        Dgl1.Item(Col1Head, rowPartyItemSpecification3).Value = FrmPacking.Col1PartyItemSpecification3
        Dgl1.Item(Col1Head, rowPartyItemSpecification4).Value = FrmPacking.Col1PartyItemSpecification4
        Dgl1.Item(Col1Head, rowQty).Value = FrmPacking.Col1Qty
        Dgl1.Item(Col1Head, rowLength).Value = FrmPacking.Col1Length
        Dgl1.Item(Col1Head, rowWidth).Value = FrmPacking.Col1Width
        Dgl1.Item(Col1Head, rowDealQty).Value = FrmPacking.Col1DealQty
        Dgl1.Item(Col1Head, rowUnitMultiplier).Value = FrmPacking.Col1UnitMultiplier
        Dgl1.Item(Col1Head, rowDealUnit).Value = FrmPacking.Col1DealUnit
        Dgl1.Item(Col1Head, rowWeight).Value = FrmPacking.Col1Weight
        Dgl1.Item(Col1Head, rowGrossWeight).Value = FrmPacking.Col1GrossWeight
        Dgl1.Item(Col1Head, rowRemark).Value = FrmPacking.Col1Remark
        Dgl1.Item(Col1Head, rowSaleOrder).Value = FrmPacking.Col1SaleOrder
        Dgl1.Item(Col1Head, rowSaleOrderDocId).Value = FrmPacking.Col1SaleOrderDocId

        ApplyPackingSettings()

        Dgl1.Item(Col1Value, rowDealUnit).Tag = mDealUnit
        Dgl1.Item(Col1Value, rowDealUnit).Value = mDealUnit



        Dgl1.Rows(rowDealQty).ReadOnly = True
        Dgl1.Rows(rowUnitMultiplier).ReadOnly = True
        Dgl1.Rows(rowDealUnit).ReadOnly = True
        Dgl1.Rows(rowSaleOrderDocId).Visible = False

    End Sub

    'Function FData_Validation() As Boolean
    '    Dim I As Integer
    '    For I = 0 To Dgl1.Rows.Count - 1
    '        'If Dgl1.Item(Col1FromUnit, I).Value = Dgl1.Item(Col1ToUnit, I).Value Then
    '        '    MsgBox("From Unit And To Unit should not be same at row no. " & I & ". can't continue.")
    '        '    Exit Function
    '        'End If
    '    Next
    '    FData_Validation = True
    'End Function

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Me.Top = 100
            Me.Left = 300
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            'If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If mEntryMode.ToUpper() = "BROWSE" Then
                Dgl1.CurrentCell.ReadOnly = True
            End If

            If Me.Visible And Dgl1.ReadOnly = False And Dgl1.CurrentCell.RowIndex > 0 Then
                If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Head).Index Then
                    SendKeys.Send("{Tab}")
                End If
            End If

            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub



            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            Select Case Dgl1.CurrentCell.RowIndex
                Case rowBaleNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 20

                Case rowQty, rowDealQty, rowLength, rowWidth, rowWeight, rowGrossWeight, rowUnitMultiplier
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 3
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            'If e.KeyCode = Keys.Enter Then Exit Sub
            If mEntryMode = "Browse" Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Select Case Dgl1.CurrentCell.RowIndex
                Case rowItem
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description  FROM Item With (NoLock) Order by Description "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowDealUnit
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Code AS Unit  FROM Unit  Order By Code "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowDimension1
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description  FROM Dimension1  ORDER BY Description  "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowDimension2
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description  FROM Dimension2  ORDER BY Description  "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowDimension3
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description  FROM Dimension3  ORDER BY Description  "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowDimension4
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description  FROM Dimension4  ORDER BY Description  "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowSaleOrder
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT H.DocID+'-'+Convert(NVARCHAR,H.Sr) AS Code,VT.V_Type +'-'+SO.ManualRefNo AS Name 
                                FROM ViewSaleOrderBalanceForPacking H
                                LEFT JOIN SaleOrder SO ON SO.DocID = H.DocID 
                                LEFT JOIN SaleOrderdetail SOD ON SOD.DocID = H.DocID AND SOD.Sr = H.Sr 
                                LEFT JOIN voucher_Type VT ON VT.V_Type = SO.V_Type 
                                WHERE SO.SaleToParty = (SELECT SubCode FROM StockHead  WHERE DocID = '" & mPackingDocId & "' ) AND SOD.Item =" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowItem).Tag) & " "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If EntryMode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.CurrentCell.RowIndex
                Case rowLength, rowWidth
                    Dgl1.Item(Col1Value, rowUnitMultiplier).Value = GetUnitMultiplier(Dgl1.Item(Col1Value, rowQty).Value, "Pcs", Dgl1.Item(Col1Value, rowLength).Value, Dgl1.Item(Col1Value, rowWidth).Value, Dgl1.Item(Col1Value, rowDealUnit).Value)
                Case rowItem
                    If Dgl1.Item(Col1Value, rowItem).Tag <> "" Then
                        GetProductDetail(Dgl1.Item(Col1Value, rowItem).Tag, Dgl1.Item(Col1Value, rowDealUnit).Value)
                        Dgl1.Item(Col1Value, rowUnitMultiplier).Value = GetUnitMultiplier(Dgl1.Item(Col1Value, rowQty).Value, "Pcs", Dgl1.Item(Col1Value, rowLength).Value, Dgl1.Item(Col1Value, rowWidth).Value, Dgl1.Item(Col1Value, rowDealUnit).Value)
                        Dgl1.Item(Col1Value, rowBaleNo).Value = GetBaleNo(Dgl1.Item(Col1Value, rowItem).Tag)
                    End If
                Case rowBarcode
                    If Dgl1.Item(Col1Value, rowBarcode).Value <> "" Then
                        BarcodeValidating(Dgl1.Item(Col1Value, rowBarcode).Value)
                        GetProductDetail(Dgl1.Item(Col1Value, rowItem).Tag, Dgl1.Item(Col1Value, rowDealUnit).Value)
                        Dgl1.Item(Col1Value, rowUnitMultiplier).Value = GetUnitMultiplier(Dgl1.Item(Col1Value, rowQty).Value, "Pcs", Dgl1.Item(Col1Value, rowLength).Value, Dgl1.Item(Col1Value, rowWidth).Value, Dgl1.Item(Col1Value, rowDealUnit).Value)
                        Dgl1.Item(Col1Value, rowBaleNo).Value = GetBaleNo(Dgl1.Item(Col1Value, rowItem).Tag)
                    End If
                Case rowSaleOrder
                    If Dgl1.Item(Col1Value, rowSaleOrder).Tag <> "" Then
                        Dim DsMain As DataSet
                        mQry = "SELECT H.DocID, H.Sr, H.Item, H.Qty, H.InvQty, H.BalQty  FROM ViewSaleOrderBalanceForPacking H WHERE H.DocID+'-'+Convert(NVARCHAR,H.Sr) = '" + Dgl1.Item(Col1Value, rowSaleOrder).Tag + "'"
                        DsMain = AgL.FillData(mQry, AgL.GCn)

                        With DsMain.Tables(0)
                            If .Rows.Count > 0 Then
                                Dgl1.Item(Col1Value, rowSaleOrderDocId).Value = AgL.XNull(AgL.XNull(.Rows(0)("DocID")))
                                Dgl1.Item(Col1Value, rowSaleOrderDocId).Tag = AgL.XNull(AgL.XNull(.Rows(0)("Sr")))

                                GetSaleOrderPartyItemDetail(Dgl1.Item(Col1Value, rowSaleOrderDocId).Value, Dgl1.Item(Col1Value, rowSaleOrderDocId).Tag)
                            End If
                        End With

                    End If


            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub GetProductDetail(ByVal ItemCode As String, ByVal DealUnit As String)
        Dim SizeUnit As String
        Dim DsMain As DataSet

        mQry = "SELECT S.Unit
                FROM Item I WITH (Nolock)
                LEFT JOIN Size S WITH (Nolock) ON S.Code = I.Size 
                WHERE I.Code =" & AgL.Chk_Text(ItemCode) & ""
        SizeUnit = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()

        If SizeUnit.ToString.ToUpper() = "METER" Then
            If DealUnit.ToString.ToUpper() = "SQ. METER" Then
                mQry = "SELECT Convert(DECIMAL,IsNull(S.Length,0))*100+ Convert(DECIMAL,IsNull(S.LengthFraction,0)) AS Length,
                        Convert(DECIMAL,IsNull(S.Width ,0))*100+ Convert(DECIMAL,IsNull(S.WidthFraction,0))  AS Width,
                        convert(DECIMAL,0) AS Height
                        FROM Item I WITH (Nolock)
                        LEFT JOIN Size S WITH (Nolock) ON S.Code = I.Size 
                        WHERE I.Code =" & AgL.Chk_Text(ItemCode) & ""
            ElseIf DealUnit.ToString.ToUpper() = "SQ. FEET" Or DealUnit.ToString.ToUpper() = "SQ. YARD" Then
                mQry = "DECLARE @LengthInch INTEGER
                        DECLARE @WidthInch INTEGER

                        SELECT @LengthInch=Round((Convert(DECIMAL,IsNull(S.Length,0))*100+ Convert(DECIMAL,IsNull(S.LengthFraction,0)))*.3937,0),
                        @WidthInch=Round((Convert(DECIMAL,IsNull(S.Width ,0))*100+ Convert(DECIMAL,IsNull(S.WidthFraction,0)))*.3937,0) 
                        FROM Item I WITH (Nolock)
                        LEFT JOIN Size S WITH (Nolock) ON S.Code = I.Size 
                        WHERE I.Code =" & AgL.Chk_Text(ItemCode) & "                     
                       

                        SELECT convert(DECIMAL(18,2),str(@LengthInch/12)+'.'+ str( @LengthInch%12,2)) AS Length,
                        convert(DECIMAL(18,2),str(@WidthInch/12)+'.'+ str( @WidthInch%12,2)) AS Width,
                        convert(DECIMAL,0) AS Height "

            End If
        ElseIf SizeUnit.ToString.ToUpper() = "FEET"
            If DealUnit.ToString.ToUpper() = "SQ. METER" Then
                mQry = "SELECT round(convert(INT,0.393701*(convert(DECIMAL(18,5),S.Length)*100+ convert(DECIMAL(18,5),S.LengthFraction)))/12,0)
                        + (round(((0.393701*(convert(DECIMAL(18,5),S.Length)*100+ convert(DECIMAL(18,5),S.LengthFraction)))/100)%12,0))/100 AS Length,  
                        round(convert(INT,0.393701*(convert(DECIMAL(18,5),S.Width)*100+ convert(DECIMAL(18,5),S.WidthFraction)))/12,0)
                        + (round(((0.393701*(convert(DECIMAL(18,5),S.Width)*100+ convert(DECIMAL(18,5),S.WidthFraction)))/100)%12,0))/100 AS Width
                        FROM Item I WITH (Nolock)
                        LEFT JOIN Size S WITH (Nolock) ON S.Code = I.Size 
                        WHERE I.Code =" & AgL.Chk_Text(ItemCode) & ""
            ElseIf DealUnit.ToString.ToUpper() = "SQ. FEET" Or DealUnit.ToString.ToUpper() = "SQ. YARD" Then
                mQry = "SELECT S.Length + S.LengthFraction/100 AS Length,
	                    S.Width + S.WidthFraction/100 AS Width,
	                    S.Height + S.HeightFraction/100 AS Height
                        FROM Item I WITH (Nolock)
                        LEFT JOIN Size S WITH (Nolock) ON S.Code = I.Size 
                        WHERE I.Code =" & AgL.Chk_Text(ItemCode) & ""
            End If

        End If


        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                Dgl1.Item(Col1Value, rowLength).Value = AgL.XNull(AgL.XNull(.Rows(0)("Length")))
                Dgl1.Item(Col1Value, rowWidth).Value = AgL.XNull(AgL.XNull(.Rows(0)("Width")))
            End If
        End With

    End Sub

    Sub BarcodeValidating(ByVal Barcode As String)
        Dim DsMain As DataSet
        mQry = "SELECT H.Code, H.Qty, H.Description , H.Item, I.Description AS ItemName
                FROM Barcode H WITH (Nolock)
                LEFT JOIN Item I WITH (Nolock) ON I.Code = H.Item 
                WHERE H.Description = " & AgL.Chk_Text(Barcode) & ""
        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                Dgl1.Item(Col1Value, rowQty).Value = AgL.VNull(AgL.VNull(.Rows(0)("Qty")))
                Dgl1.Item(Col1Value, rowItem).Value = AgL.XNull(AgL.XNull(.Rows(0)("ItemName")))
                Dgl1.Item(Col1Value, rowItem).Tag = AgL.XNull(AgL.XNull(.Rows(0)("Item")))
                Dgl1.Item(Col1Value, rowBarcode).Tag = AgL.XNull(AgL.XNull(.Rows(0)("Code")))
            Else
                MsgBox("Invalid Barcode !")
            End If
        End With

    End Sub

    Sub GetLastBalanceSaleOrder(ByVal ItemCode As String)
        Dim SizeUnit As String
        Dim DsMain As DataSet

        mQry = "SELECT S.Unit
                FROM Item I WITH (Nolock)
                LEFT JOIN Size S WITH (Nolock) ON S.Code = I.Size 
                WHERE I.Code =" & AgL.Chk_Text(ItemCode) & ""



        DsMain = AgL.FillData(mQry, AgL.GCn)

        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                Dgl1.Item(Col1Value, rowLength).Value = AgL.XNull(AgL.XNull(.Rows(0)("Length")))
                Dgl1.Item(Col1Value, rowWidth).Value = AgL.XNull(AgL.XNull(.Rows(0)("Width")))
            End If
        End With

    End Sub

    Sub GetSaleOrderPartyItemDetail(ByVal DocId As String, ByVal Sr As Integer)
        Dim DsMain As DataSet
        mQry = "SELECT SED.PartyItem, SED.PartyItemSpecification1, SED.PartyItemSpecification2, SED.PartyItemSpecification3, SED.PartyItemSpecification4, SED.PartyItemSpecification5  
                FROM SaleOrderDetail L With (Nolock)
                LEFT JOIN SaleenquiryDetail SED With (Nolock) ON SED.DocID = L.GenDocId AND SED.Sr = L.GenDocIdSr 
                WHERE L.DocID =  " & AgL.Chk_Text(DocId) & " AND L.Sr =" & Sr & ""
        DsMain = AgL.FillData(mQry, AgL.GCn)
        With DsMain.Tables(0)
            If .Rows.Count > 0 Then
                Dgl1.Item(Col1Value, rowPartyItem).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItem")))
                Dgl1.Item(Col1Value, rowPartyItemSpecification1).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItemSpecification1")))
                Dgl1.Item(Col1Value, rowPartyItemSpecification2).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItemSpecification2")))
                Dgl1.Item(Col1Value, rowPartyItemSpecification3).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItemSpecification3")))
                Dgl1.Item(Col1Value, rowPartyItemSpecification4).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItemSpecification4")))
            End If
        End With

    End Sub


    Public Function GetBaleNo(ByVal ItemCode As String) As String
        mQry = "Select isnull(Max(convert(Decimal(18,0),L.BaleNo)),0)+1 FROM StockHeadDetail L With (Nolock) WHERE L.DocID ='" & PackingDocId & "' AND isnumeric(L.BaleNo) =1"
        GetBaleNo = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()
    End Function

    Public Function GetUnitMultiplier(ByVal FromQty As Decimal, ByVal FromUnit As String, ByVal Length As Decimal, ByVal Width As Decimal, ByVal ToUnit As String) As Decimal
        Dim LengthFeet As Decimal = Math.Floor(Length)
        Dim LengthFractionFeet As Decimal = (Length - Math.Floor(Length)) * 100
        Dim WidthFeet As Decimal = Math.Floor(Width)
        Dim WidthFractionFeet As Decimal = (Width - Math.Floor(Width)) * 100
        Dim mLength As Decimal = Math.Round(LengthFeet + (LengthFractionFeet / 12), 3)
        Dim mWidth As Decimal = Math.Round(WidthFeet + (WidthFractionFeet / 12), 3)

        Dim mLengthInch As Decimal = LengthFeet * 12 + LengthFractionFeet
        Dim mWidthInch As Decimal = WidthFeet * 12 + WidthFractionFeet


        If FromUnit = "PCS" And ToUnit = "Sq. Yard" Then
            Dim mAreaInch As Decimal = mLengthInch * mWidthInch
            GetUnitMultiplier = Math.Round(mAreaInch / 81, 0) / 16
        ElseIf FromUnit = "PCS" And ToUnit = "Sq. Feet" Then
            Dim mAreaInch As Decimal = mLengthInch * mWidthInch
            GetUnitMultiplier = Math.Round(mAreaInch / 144, 2)
        ElseIf FromUnit = "PCS" And ToUnit = "Sq. Meter" Then
            GetUnitMultiplier = Math.Round((mLength * mWidth) / 10000, 2)
        End If
    End Function

    Sub Calculation()
        If Dgl1.Item(Col1Value, rowItem).Tag <> "" Then
            Dgl1.Item(Col1Value, rowDealQty).Value = Dgl1.Item(Col1Value, rowQty).Value * Dgl1.Item(Col1Value, rowUnitMultiplier).Value
        End If
    End Sub


    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim I As Integer = 0

        Select Case sender.Name
            Case BtnSave.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                If Validate_Data() = False Then Exit Sub
                FSave(mPackingDocId)

                MsgBox("Data Saved Sucessfully!")
                mOkButtonPressed = True

                If mPackingDocIdSr > 0 Then
                    Me.Close()
                Else
                    IniGrid()
                End If

        End Select
    End Sub


    Public Sub FMoveRec(ByVal SearchCode As String, ByVal Sr As Integer)
        Dim DsMain As DataSet
        Dim I As Integer = 0

        Try
            mQry = "SELECT I.Description AS ItemName, B.Description AS BarcodeName, VT.V_Type +'-'+SH.ManualRefNo AS SaleOrderNo, SED.PartyItem, SED.PartyItemSpecification1, SED.PartyItemSpecification2, SED.PartyItemSpecification3, SED.PartyItemSpecification4, SED.PartyItemSpecification5,
                    L.ReferenceDocID+'-'+Convert(NVARCHAR,L.ReferenceSr) AS SaleOrderCode, L.* 
                    FROM StockHeadDetail L WITH (Nolock)
                    LEFT JOIN Item I WITH (Nolock) ON I.Code = L.Item 
                    LEFT JOIN Barcode B WITH (Nolock) ON B.Code = L.Barcode 
                    LEFT JOIN SaleInvoiceDetail SL WITH (Nolock) ON SL.DocID = L.ReferenceDocID AND SL.Sr = L.ReferenceSr
                    LEFT JOIN SaleInvoice SH WITH (Nolock) ON SH.DocID = SL.DocID 
                    LEFT JOIN Voucher_Type VT ON VT.V_Type = SH.V_Type 
                    LEFT JOIN SaleenquiryDetail SED ON SED.DocID = SL.GenDocId AND SED.Sr = SL.GenDocIdSr
                    WHERE L.DocID ='" & SearchCode & "' AND L.Sr =" & Sr & " "

            DsMain = AgL.FillData(mQry, AgL.GCn)

            With DsMain.Tables(0)
                If .Rows.Count > 0 Then
                    Dgl1(Col1Value, rowBarcode).Tag = AgL.XNull(AgL.XNull(.Rows(0)("Barcode")))
                    Dgl1(Col1Value, rowBarcode).Value = AgL.XNull(AgL.XNull(.Rows(0)("BarcodeName")))
                    Dgl1(Col1Value, rowItem).Tag = AgL.XNull(AgL.XNull(.Rows(0)("Item")))
                    Dgl1(Col1Value, rowItem).Value = AgL.XNull(AgL.XNull(.Rows(0)("ItemName")))
                    Dgl1(Col1Value, rowDimension1).Tag = AgL.XNull(AgL.XNull(.Rows(0)("Dimension1")))
                    Dgl1(Col1Value, rowDimension2).Tag = AgL.XNull(AgL.XNull(.Rows(0)("Dimension2")))
                    Dgl1(Col1Value, rowDimension3).Tag = AgL.XNull(AgL.XNull(.Rows(0)("Dimension3")))
                    Dgl1(Col1Value, rowDimension4).Tag = AgL.XNull(AgL.XNull(.Rows(0)("Dimension4")))

                    Dgl1(Col1Value, rowLotNo).Value = AgL.XNull(AgL.XNull(.Rows(0)("LotNo")))
                    Dgl1(Col1Value, rowBaleNo).Value = AgL.XNull(AgL.XNull(.Rows(0)("BaleNo")))
                    Dgl1(Col1Value, rowPartyItem).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItem")))
                    Dgl1(Col1Value, rowPartyItemSpecification1).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItemSpecification1")))
                    Dgl1(Col1Value, rowPartyItemSpecification2).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItemSpecification2")))
                    Dgl1(Col1Value, rowPartyItemSpecification3).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItemSpecification3")))
                    Dgl1(Col1Value, rowPartyItemSpecification4).Value = AgL.XNull(AgL.XNull(.Rows(0)("PartyItemSpecification4")))
                    Dgl1(Col1Value, rowQty).Value = AgL.XNull(AgL.XNull(.Rows(0)("Qty")))
                    Dgl1(Col1Value, rowLength).Value = AgL.XNull(AgL.XNull(.Rows(0)("Length")))
                    Dgl1(Col1Value, rowWidth).Value = AgL.XNull(AgL.XNull(.Rows(0)("Width")))
                    Dgl1(Col1Value, rowDealQty).Value = AgL.XNull(AgL.XNull(.Rows(0)("DealQty")))
                    Dgl1(Col1Value, rowUnitMultiplier).Value = AgL.XNull(AgL.XNull(.Rows(0)("UnitMultiplier")))
                    Dgl1(Col1Value, rowDealUnit).Value = AgL.XNull(AgL.XNull(.Rows(0)("DealUnit")))
                    Dgl1(Col1Value, rowWeight).Value = AgL.XNull(AgL.XNull(.Rows(0)("Weight")))
                    Dgl1(Col1Value, rowGrossWeight).Value = AgL.XNull(AgL.XNull(.Rows(0)("GrossWeight")))
                    Dgl1(Col1Value, rowSaleOrder).Value = AgL.XNull(AgL.XNull(.Rows(0)("SaleOrderNo")))
                    Dgl1(Col1Value, rowSaleOrder).Tag = AgL.XNull(AgL.XNull(.Rows(0)("SaleOrderCode")))
                    Dgl1(Col1Value, rowSaleOrderDocId).Value = AgL.XNull(AgL.XNull(.Rows(0)("ReferenceDocID")))
                    Dgl1(Col1Value, rowSaleOrderDocId).Tag = AgL.XNull(AgL.XNull(.Rows(0)("ReferenceSr")))
                    Dgl1(Col1Value, rowRemark).Value = AgL.XNull(AgL.XNull(.Rows(0)("Remarks")))

                End If
            End With




        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function Validate_Data() As Boolean
        Dim I As Integer


        'With Dgl2
        '    For I = 0 To .Rows.Count - 1
        '        If Dgl2.Rows(I).Visible Then
        '            If .Item(Col2PaymentMode, I).Value <> "" And Val(.Item(Col2Amount, I).Value) > 0 Then
        '                If .Item(Col2PostToAc, I).Value = "" Then
        '                    MsgBox("Post To A/c Is Blank At Row No " & Dgl2.Item(ColSNo, I).Value & "")
        '                    .CurrentCell = .Item(Col2PostToAc, I) : Dgl2.Focus()
        '                    Exit Function
        '                End If
        '            End If
        '        End If
        '    Next
        'End With

        'If ClsFunction.ValidateGstNo(Dgl1.Item(Col1Value, rowDimension2).Value, Dgl1.Item(Col1Value, rowSalesTaxGroup).Value, Dgl1.Item(Col1Value, rowItem).Value) = False Then
        '    Exit Function
        'End If


        Validate_Data = True
    End Function


    Public Sub FSave(ByVal SearchCode As String)
        Dim mSr As Integer
        If Validate_Data() = False Then Exit Sub

        If Val(mPackingDocIdSr) > 0 Then
            mQry = " Delete From Stock Where DocId = '" & SearchCode & "' And ReferenceDocIDSr = " & mPackingDocIdSr & "  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            mQry = " Delete From StockHeadDetail Where DocId = '" & SearchCode & "' And Sr = " & mPackingDocIdSr & "  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From StockHeadDetail  With (NoLock) Where DocID = '" & SearchCode & "'", AgL.GcnRead).ExecuteScalar)

        mQry = "INSERT INTO StockHeadDetail (DocID, Sr, Item, LotNo, BaleNo, Godown, Qty, Unit, UnitMultiplier, DealQty, DealUnit,  Remarks,  Barcode, Specification, Dimension1, Dimension2, Dimension3, Dimension4,  Length, Width, Thickness, Weight, GrossWeight, ReferenceDocID, ReferenceSr, Tag)
                VALUES ('" & SearchCode & "'," & mSr + 1 & "," & AgL.Chk_Text(Dgl1.Item(Col1Value, rowItem).Tag) & ", 
                         " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLotNo).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBaleNo).Value) & ", 
                        '" & Godown & "', " & Val(Dgl1.Item(Col1Value, rowQty).Value) & ", 'Pcs',  " & Val(Dgl1.Item(Col1Value, rowUnitMultiplier).Value) & ",  " & Val(Dgl1.Item(Col1Value, rowDealQty).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDealUnit).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRemark).Value) & ",  " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcode).Tag) & ",
                         " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPartyItemSpecification1).Value) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension1).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension2).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension3).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension4).Tag) & ", 
                         " & Val(Dgl1.Item(Col1Value, rowLength).Value) & ", " & Val(Dgl1.Item(Col1Value, rowWidth).Value) & ", 0,  " & Val(Dgl1.Item(Col1Value, rowWeight).Value) & ", " & Val(Dgl1.Item(Col1Value, rowGrossWeight).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSaleOrderDocId).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSaleOrderDocId).Tag) & ", null) "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = "INSERT INTO Stock (DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, SubCode, SalesTaxGroupParty, Item, LotNo, Godown, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss, DealQty_Rec, DealUnit, ReferenceDocID, BaleNo, ReferenceDocIDSr, EType_IR, Dimension1, Dimension2, Barcode, ReferenceV_Type, ReferenceTSr, Dimension3, Dimension4)
                SELECT L.DocID, L.Sr*2-1, L.Sr*2-1, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.SubCode, NULL SalesTaxGroupParty, L.Item, L.LotNo, L.Godown, L.Qty AS Qty_Iss, 0 Qty_Rec, 
                L.Unit , L.UnitMultiplier, L.DealQty DealQty_Iss, 0 DealQty_Rec, L.DealUnit, L.DocID  ReferenceDocID, L.BaleNo, L.Sr ReferenceDocIDSr, 
                'I'EType_IR, L.Dimension1, L.Dimension2, L.Barcode, H.V_Type ReferenceV_Type, NULL ReferenceTSr, L.Dimension3, L.Dimension4  
                FROM StockHeadDetail L
                LEFT JOIN StockHead H ON H.DocID = L.DocID Where L.DocID = '" & SearchCode & "' And L.Sr= " & mSr + 1 & ""
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = "INSERT INTO Stock (DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, SubCode, SalesTaxGroupParty, Item, LotNo, Godown, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss, DealQty_Rec, DealUnit, ReferenceDocID, BaleNo, ReferenceDocIDSr, EType_IR, Dimension1, Dimension2, Barcode, ReferenceV_Type, ReferenceTSr, Dimension3, Dimension4)
                SELECT L.DocID, L.Sr*2, L.Sr*2, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.SubCode, NULL SalesTaxGroupParty, L.Item, L.LotNo, L.Godown, 0 AS Qty_Iss,  L.Qty Qty_Rec, 
                L.Unit , L.UnitMultiplier, 0 DealQty_Iss, L.DealQty DealQty_Rec, L.DealUnit, L.DocID  ReferenceDocID, L.BaleNo, L.Sr ReferenceDocIDSr, 
                'R'EType_IR, L.Dimension1, L.Dimension2, L.Barcode, H.V_Type ReferenceV_Type, NULL ReferenceTSr, L.Dimension3, L.Dimension4  
                FROM StockHeadDetail L
                LEFT JOIN StockHead H ON H.DocID = L.DocID Where L.DocID = '" & SearchCode & "' And L.Sr= " & mSr + 1 & ""
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

    End Sub


    Private Sub FrmPackingDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Private Sub FrmPackingDetail_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Dgl1 IsNot Nothing Then
            If Dgl1.FirstDisplayedCell IsNot Nothing Then
                Dgl1.CurrentCell = Dgl1(Col1Value, rowBaleNo) 'Dgl1.FirstDisplayedCell
                Dgl1.Focus()
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub TxtFreshEnquiryReceived_GotFocus(sender As Object, e As EventArgs)
        If Dgl1 IsNot Nothing Then
            If Dgl1.FirstDisplayedCell IsNot Nothing Then
                If Dgl1.Item(Col1Value, rowBaleNo).Value = "" Then
                    Dgl1.CurrentCell = Dgl1(Col1Value, rowBaleNo) 'Dgl1.FirstDisplayedCell
                    Dgl1.Focus()
                End If
            End If
        End If
    End Sub
End Class