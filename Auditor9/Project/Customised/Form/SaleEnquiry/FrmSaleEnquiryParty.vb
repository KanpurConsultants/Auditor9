Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Public Class FrmSaleEnquiryParty
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"


    Public Const rowPartyName As Integer = 0
    Public Const rowAddress As Integer = 1
    Public Const rowCity As Integer = 2
    Public Const rowStateCode As Integer = 3
    Public Const rowPincode As Integer = 4
    Public Const rowMobile As Integer = 5
    'Public Const rowSalesTaxGroup As Integer = 6
    'Public Const rowPlaceOfSupply As Integer = 7
    Public Const rowSalesTaxNo As Integer = 8
    Public Const rowAadharNo As Integer = 9
    Public Const rowPanNo As Integer = 10
    Public Const rowShipToAddress As Integer = 11


    'Public WithEvents Dgl2 As New AgControls.AgDataGrid
    'Public Const Col2PaymentMode As String = "Payment Mode"
    'Public Const Col2Amount As String = "Amount"
    'Public Const Col2ReferenceNo As String = "Ref / Card / Chq No."
    'Public Const Col2PostToAc As String = "Post To A/c"
    'Public Const Col2ReferenceDocID As String = "Reference DocID"
    'Public Const Col2ReferenceV_Type As String = "Reference Voucher Type"
    'Public Const Col2ReferenceSr As String = "Reference Sr"

    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mToQtyDecimalPlace As Integer
    Dim mAcGroupNature As String
    Dim mDivisionCode As String
    Dim mSiteCode As String
    Dim mDtSaleEnquirySettings As DataTable
    Dim mObjFrmSaleEnquiry As FrmSaleEnquiry
    Public Property objFrmSaleEnquiry() As FrmSaleEnquiry
        Get
            objFrmSaleEnquiry = mObjFrmSaleEnquiry
        End Get
        Set(ByVal value As FrmSaleEnquiry)
            mObjFrmSaleEnquiry = value
        End Set
    End Property

    Public Property DtSaleEnquirySettings() As DataTable
        Get
            DtSaleEnquirySettings = mDtSaleEnquirySettings
        End Get
        Set(ByVal value As DataTable)
            mDtSaleEnquirySettings = value
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


    'Public Property InvoiceAmount() As Double
    '    Get
    '        InvoiceAmount = Val(LblInvoiceAmount.Text)
    '    End Get
    '    Set(ByVal value As Double)
    '        LblInvoiceAmount.Text = Format(value, "0.00")
    '        Calculation()
    '    End Set
    'End Property
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    AgL.FPaintForm(Me, e, 0)
    'End Sub

    Public Sub ApplySaleEnquirySettings(PartyNature As String)
        Dim IsPointOfSale As Boolean
        If DtSaleEnquirySettings IsNot Nothing Then
            If DtSaleEnquirySettings.Rows.Count > 0 Then
                With DtSaleEnquirySettings
                    If PartyNature = "FreshEnquiry" Then
                        Dgl1.Rows(rowPartyName).Visible = CBool(AgL.VNull(.Rows(0)("IsVisible_FreshEnquiryPartyName")))
                        Dgl1.Rows(rowAddress).Visible = CBool(AgL.VNull(.Rows(0)("IsVisible_FreshEnquiryPartyAddress")))
                        Dgl1.Rows(rowCity).Visible = CBool(AgL.VNull(.Rows(0)("IsVisible_FreshEnquiryPartyCity")))
                        Dgl1.Rows(rowMobile).Visible = CBool(AgL.VNull(.Rows(0)("IsVisible_FreshEnquiryPartyMobile")))
                        Dgl1.Rows(rowSalesTaxNo).Visible = CBool(AgL.VNull(.Rows(0)("IsVisible_FreshEnquiryPartySalesTaxNo")))
                        Dgl1.Rows(rowAadharNo).Visible = CBool(AgL.VNull(.Rows(0)("IsVisible_FreshEnquiryPartyAadharNo")))
                        Dgl1.Rows(rowPanNo).Visible = CBool(AgL.VNull(.Rows(0)("IsVisible_FreshEnquiryPartyPanNo")))
                    End If

                    'IsPointOfSale = AgL.XNull(.Rows(0)("SaleEnquiryPattern")) = SaleEnquiryPattern.PointOfSale

                    'LblInvoiceAmountText.Visible = IsPointOfSale
                    'LblInvoiceAmount.Visible = IsPointOfSale
                    'LblFreshEnquiryReceivedText.Visible = IsPointOfSale
                    'TxtFreshEnquiryReceived.Visible = IsPointOfSale
                    'LblFreshEnquiryToRefundText.Visible = IsPointOfSale
                    'LblFreshEnquiryToRefund.Visible = IsPointOfSale
                    'LblTotalReceiptText.Visible = IsPointOfSale
                    'LblTotalReceipt.Visible = IsPointOfSale
                    'LblBalanceToReceiptText.Visible = IsPointOfSale
                    'LblBalanceToReceipt.Visible = IsPointOfSale
                    'Pnl3.Visible = IsPointOfSale
                    'Dgl2.Visible = IsPointOfSale

                    If Not IsPointOfSale Then
                        Pnl4.Top = Pnl1.Top + Pnl1.Height
                        Me.Height = Pnl1.Height + Pnl4.Height + 30
                    End If

                End With
            End If
        End If
    End Sub

    Public Sub IniGrid(DocID As String, PartyCode As String, AcGroupNature As String)
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, False, True)
            .AddAgTextColumn(Dgl1, Col1Value, 350, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        'Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.TabIndex = Pnl1.TabIndex
        AgL.GridDesign(Dgl1)



        Dgl1.Rows.Add(12)
        Dgl1.Item(Col1Head, rowPartyName).Value = "Party Name"
        Dgl1.Item(Col1Head, rowAddress).Value = "Address"
        Dgl1.Item(Col1Head, rowCity).Value = "City"
        Dgl1.Item(Col1Head, rowStateCode).Value = "StateCode"
        Dgl1.Item(Col1Head, rowPincode).Value = "Pincode"
        Dgl1.Item(Col1Head, rowMobile).Value = "Mobile No."
        'Dgl1.Item(Col1Head, rowSalesTaxGroup).Value = "Sales Tax Group"
        'Dgl1.Item(Col1Head, rowPlaceOfSupply).Value = "Place Of Supply"
        Dgl1.Item(Col1Head, rowSalesTaxNo).Value = "GST No."
        Dgl1.Item(Col1Head, rowAadharNo).Value = "Aadhar No."
        Dgl1.Item(Col1Head, rowPanNo).Value = "Pan No."
        Dgl1.Item(Col1Head, rowShipToAddress).Value = "Ship To Address"




        'Dgl2.ColumnCount = 0
        'With AgCL
        '    .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
        '    .AddAgTextColumn(Dgl2, Col2PaymentMode, 150, 255, Col2PaymentMode, True, False)
        '    .AddAgNumberColumn(Dgl2, Col2Amount, 90, 8, 2, True, Col2Amount, True)
        '    .AddAgTextColumn(Dgl2, Col2ReferenceNo, 110, 50, Col2ReferenceNo, True, False)
        '    .AddAgTextColumn(Dgl2, Col2PostToAc, 175, 255, Col2PostToAc, True, False)
        '    .AddAgTextColumn(Dgl2, Col2ReferenceDocID, 300, 255, Col2ReferenceDocID, False, False)
        '    .AddAgTextColumn(Dgl2, Col2ReferenceV_Type, 300, 255, Col2ReferenceV_Type, False, False)
        '    .AddAgTextColumn(Dgl2, Col2ReferenceSr, 300, 255, Col2ReferenceSr, False, False)
        'End With
        'AgL.AddAgDataGrid(Dgl2, Pnl2)
        'Dgl2.EnableHeadersVisualStyles = False
        'Dgl2.ColumnHeadersHeight = 35
        'Dgl2.AgSkipReadOnlyColumns = True
        'Dgl2.TabIndex = Pnl2.TabIndex
        'AgL.GridDesign(Dgl2)


        mAcGroupNature = AcGroupNature
        FMoveRec(DocID, PartyCode, AcGroupNature)
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
            Me.Top = 230
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

            'If mAcGroupNature.ToUpper() <> "FreshEnquiry" Then
            '    Select Case Dgl1.CurrentCell.RowIndex
            '        Case rowQty
            '        Case Else
            '            Dgl1.CurrentCell.ReadOnly = True
            '    End Select
            'End If



            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            Select Case Dgl1.CurrentCell.RowIndex
                Case rowPartyName
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 100
                Case rowAddress
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 255
                Case rowMobile
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 10
                Case rowStateCode
                    Dgl1.CurrentCell.ReadOnly = True
                Case rowSalesTaxNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 15
                Case rowAadharNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 12
                Case rowPanNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 10
                Case rowShipToAddress
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 255
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
                Case rowCity
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select C.CityCode, C.CityName from City C  With (NoLock) Order by c.CityName "
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
                Case rowSalesTaxNo
                    'ClsFunction.ValidateGstNo(Dgl1.Item(Col1Value, rowSpecification2).Value, Dgl1.Item(Col1Value, rowSalesTaxGroup).Value, Dgl1.Item(Col1Value, rowStateCode).Value)
                Case rowCity
                    Dgl1.Item(Col1Value, rowStateCode).Value = AgL.Dman_Execute("Select S.ManualCode From City c  With (NoLock) Left Join State s  With (NoLock) On C.State = S.Code  Where C.CityCode = '" & Dgl1.Item(Col1Value, rowCity).Tag & "'", AgL.GCn).ExecuteScalar()
                    'Dgl1.Item(Col1Value, rowPlaceOfSupply).Value = ClsFunction.GetPlaceOfSupply(Dgl1.Item(Col1Value, rowCity).Tag)
                    'Case Col1FromUnit
                    '    Dgl1.Item(Col1Equal, mRowIndex).Value = "=" 
                    '    Dgl1.Item(Col1ToUnit, mRowIndex).Value = mUnit
                    '    Dgl1.Item(Col1ToQtyDecimalPlaces, mRowIndex).Value = mToQtyDecimalPlace
                    '    If Val(Dgl1.Item(Col1FromQty, mRowIndex).Value) = 0 Then
                    '        Dgl1.Item(Col1FromQty, mRowIndex).Value = "1"
                    '    End If

                    '    If Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) Is Nothing Then Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) = ""

                    '    If Dgl1.Item(Col1FromUnit, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex).ToString.Trim = "" Then
                    '        Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = ""
                    '    Else
                    '        If Dgl1.AgDataRow IsNot Nothing Then
                    '            Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DecimalPlaces").Value)
                    '        End If
                    '    End If


            End Select
            'Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub Calculation()
        Dim mTotalReceive As Double
        Dim I As Integer


        'If Val(TxtFreshEnquiryReceived.Text) >= Val(LblInvoiceAmount.Text) Then
        '    LblFreshEnquiryToRefund.Text = Format(Val(TxtFreshEnquiryReceived.Text) - Val(LblInvoiceAmount.Text), "0.00")
        'End If

        'If Val(TxtFreshEnquiryReceived.Text) >= Val(LblInvoiceAmount.Text) Then
        '    mTotalReceive = Val(LblInvoiceAmount.Text)
        'Else
        '    mTotalReceive = Val(TxtFreshEnquiryReceived.Text)
        'End If

        'For I = 0 To Dgl2.RowCount - 1
        '    If Dgl2.Rows(I).Visible Then
        '        If Val(Dgl2.Item(Col2Amount, I).Value) <> 0 Then
        '            mTotalReceive += Val(Dgl2.Item(Col2Amount, I).Value)
        '        End If
        '    End If
        'Next

        'LblTotalReceipt.Text = Format(mTotalReceive, "0.00")

        'If Val(LblInvoiceAmount.Text) - Val(LblTotalReceipt.Text) >= 0 Then
        '    LblBalanceToReceipt.Text = Format(Val(LblInvoiceAmount.Text) - Val(LblTotalReceipt.Text), "0.00")
        'End If
    End Sub


    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0

        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                If Validate_Data() = False Then Exit Sub



                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub


    Public Sub FMoveRec(ByVal SearchCode As String, ByVal PartyCode As String, ByVal PartyNature As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        Try
            If PartyCode <> "" Then
                If PartyNature.ToUpper() = "FreshEnquiry" Then
                    Dgl1.Item(Col1Value, rowCity).Value = AgL.PubSiteCity
                    Dgl1.Item(Col1Value, rowCity).Tag = AgL.PubSiteCityCode
                    Dgl1.Item(Col1Value, rowStateCode).Tag = AgL.PubSiteStateCode
                    'Dgl1.Item(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupParty"))
                    'Dgl1.Item(Col1Value, rowPlaceOfSupply).Value = ClsFunction.GetPlaceOfSupply(Dgl1.Item(Col1Value, rowCity).Tag)
                Else

                    'BtnHeaderDetail.Tag = FunRetNewUnitConversionObject()
                    'BtnHeaderDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
                    mQry = "SELECT H.DispName SaleToPartyName, H.Address as SaleToPartyAddress, H.CityCode as SaleToPartyCity, C.CityName, S.ManualCode as StateManualCode, 
                    H.Pin as SaleToPartyPincode, H.Mobile SaleToPartyMobile, H.SalesTaxPostingGroup,
                    (Select RegistrationNo From SubgroupRegistration SR  With (NoLock) Where SR.Subcode = H.Subcode and SR.RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "') as SaleToPartySalesTaxNo,
                    (Select RegistrationNo From SubgroupRegistration SR  With (NoLock) Where SR.Subcode = H.Subcode and SR.RegistrationType = '" & SubgroupRegistrationType.AadharNo & "') as SaleToPartyAadharNo,
                    (Select RegistrationNo From SubgroupRegistration SR  With (NoLock) Where SR.Subcode = H.Subcode and SR.RegistrationType = '" & SubgroupRegistrationType.PanNo & "') as SaleToPartyPanNo
                    FROM Subgroup H  With (NoLock)                     
                    Left Join City C With (NoLock) On H.CityCode = C.CityCode    
                    Left Join State S With (NoLock) On C.State = S.Code                    
                    WHERE H.Subcode = '" & PartyCode & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    With DtTemp
                        'BtnHeaderDetail.Tag.Dgl1.RowCount = 1 : BtnHeaderDetail.Tag.Dgl1.Rows.Clear()
                        If DtTemp.Rows.Count > 0 Then
                            Dgl1.Item(Col1Value, rowPartyName).Value = AgL.XNull(DtTemp.Rows(0)("SaleToPartyName"))
                            Dgl1.Item(Col1Value, rowAddress).Value = AgL.XNull(DtTemp.Rows(0)("SaleToPartyAddress"))
                            Dgl1.Item(Col1Value, rowCity).Value = AgL.XNull(DtTemp.Rows(0)("CityName"))
                            Dgl1.Item(Col1Value, rowCity).Tag = AgL.XNull(DtTemp.Rows(0)("SaleToPartyCity"))
                            Dgl1.Item(Col1Value, rowStateCode).Value = AgL.XNull(DtTemp.Rows(0)("StateManualCode"))
                            Dgl1.Item(Col1Value, rowPincode).Value = AgL.XNull(DtTemp.Rows(0)("SaleToPartyPincode"))
                            Dgl1.Item(Col1Value, rowMobile).Value = AgL.XNull(DtTemp.Rows(0)("SaleToPartyMobile"))
                            'Dgl1.Item(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(DtTemp.Rows(0)("SalesTaxPostingGroup"))
                            Dgl1.Item(Col1Value, rowSalesTaxNo).Value = AgL.XNull(.Rows(0)("SaleToPartySalesTaxNo"))
                            Dgl1.Item(Col1Value, rowAadharNo).Value = AgL.XNull(.Rows(0)("SaleToPartyAadharNo"))
                            Dgl1.Item(Col1Value, rowPanNo).Value = AgL.XNull(.Rows(0)("SaleToPartyPanNo"))
                            'Dgl1.Item(Col1Value, rowPlaceOfSupply).Value = ClsFunction.GetPlaceOfSupply(Dgl1.Item(Col1Value, rowCity).Tag)
                        End If
                    End With
                End If
            Else
                'BtnHeaderDetail.Tag = FunRetNewUnitConversionObject()
                'BtnHeaderDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
                mQry = "SELECT H.SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyCity, C.CityName, S.ManualCode as StateManualCode, H.SaleToPartyPincode, H.SaleToPartyMobile, 
                    H.SaleToPartySalesTaxNo, H.SaleToPartyAadharNo, H.SaleToPartyPanNo, H.ShipToAddress
                    FROM SaleEnquiry H  With (NoLock)                     
                    Left Join City C  With (NoLock) On H.SaleToPartyCity = C.CityCode                    
                    Left Join State S  With (NoLock) On C.State = S.Code
                    WHERE H.DocId = '" & SearchCode & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                With DtTemp
                    'BtnHeaderDetail.Tag.Dgl1.RowCount = 1 : BtnHeaderDetail.Tag.Dgl1.Rows.Clear()
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1.Item(Col1Value, rowPartyName).Value = AgL.XNull(DtTemp.Rows(0)("SaleToPartyName"))
                        Dgl1.Item(Col1Value, rowAddress).Value = AgL.XNull(DtTemp.Rows(0)("SaleToPartyAddress"))
                        Dgl1.Item(Col1Value, rowCity).Value = AgL.XNull(DtTemp.Rows(0)("CityName"))
                        Dgl1.Item(Col1Value, rowCity).Tag = AgL.XNull(DtTemp.Rows(0)("SaleToPartyCity"))
                        Dgl1.Item(Col1Value, rowStateCode).Value = AgL.XNull(DtTemp.Rows(0)("StateManualCode"))
                        Dgl1.Item(Col1Value, rowPincode).Value = AgL.XNull(DtTemp.Rows(0)("SaleToPartyPincode"))
                        Dgl1.Item(Col1Value, rowMobile).Value = AgL.XNull(DtTemp.Rows(0)("SaleToPartyMobile"))
                        'Dgl1.Item(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(DtTemp.Rows(0)("SalesTaxGroupParty"))
                        'Dgl1.Item(Col1Value, rowPlaceOfSupply).Value = AgL.XNull(.Rows(0)("PlaceOfSupply"))
                        Dgl1.Item(Col1Value, rowSalesTaxNo).Value = AgL.XNull(.Rows(0)("SaleToPartySalesTaxNo"))
                        Dgl1.Item(Col1Value, rowAadharNo).Value = AgL.XNull(.Rows(0)("SaleToPartyAadharNo"))
                        Dgl1.Item(Col1Value, rowPanNo).Value = AgL.XNull(.Rows(0)("SaleToPartyPanNo"))
                        Dgl1.Item(Col1Value, rowShipToAddress).Value = AgL.XNull(.Rows(0)("ShipToAddress"))
                        'TxtFreshEnquiryReceived.Text = Format(AgL.VNull(.Rows(0)("PaidAmt")), "0.00")
                    End If
                End With

            End If

            ApplySaleEnquirySettings(PartyNature)



            'mQry = "Select H.*, PM.Description as PaymentModeDescription, Sg.Name as PostToAcName 
            '        From SaleEnquiryPayment H  With (NoLock)
            '        Left Join PaymentMode PM  With (NoLock) on H.PaymentMode = PM.Code
            '        Left Join viewHelpSubgroup Sg  With (NoLock) On H.PostToAc = Sg.Code
            '        Where H.DocID = '" & SearchCode & "' Order By H.Sr "
            'DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            'With DtTemp
            '    If DtTemp.Rows.Count > 0 Then
            '        For I = 0 To DtTemp.Rows.Count - 1
            '            Dgl2.Rows.Add()
            '            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
            '            Dgl2.Item(ColSNo, I).Tag = AgL.XNull(DtTemp.Rows(I)("Sr"))
            '            Dgl2.Item(Col2PaymentMode, I).Tag = AgL.XNull(DtTemp.Rows(I)("PaymentMode"))
            '            Dgl2.Item(Col2PaymentMode, I).Value = AgL.XNull(DtTemp.Rows(I)("PaymentModeDescription"))
            '            Dgl2.Item(Col2Amount, I).Value = AgL.XNull(DtTemp.Rows(I)("Amount"))
            '            Dgl2.Item(Col2ReferenceNo, I).Value = AgL.XNull(DtTemp.Rows(I)("ReferenceNo"))
            '            Dgl2.Item(Col2ReferenceDocID, I).Value = AgL.XNull(DtTemp.Rows(I)("ReferenceDocID"))
            '            Dgl2.Item(Col2ReferenceV_Type, I).Value = AgL.XNull(DtTemp.Rows(I)("ReferenceV_Type"))
            '            Dgl2.Item(Col2ReferenceSr, I).Value = AgL.XNull(DtTemp.Rows(I)("ReferenceSr"))
            '            Dgl2.Item(Col2PostToAc, I).Value = AgL.XNull(DtTemp.Rows(I)("PostToAc"))
            '            Dgl2.Item(Col2PostToAc, I).Tag = AgL.XNull(DtTemp.Rows(I)("PostToAcName"))
            '        Next
            '    End If
            'End With
            Calculation()


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

        'If ClsFunction.ValidateGstNo(Dgl1.Item(Col1Value, rowSpecification2).Value, Dgl1.Item(Col1Value, rowSalesTaxGroup).Value, Dgl1.Item(Col1Value, rowStateCode).Value) = False Then
        '    Exit Function
        'End If


        Validate_Data = True
    End Function


    Public Sub FSave(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim I As Integer
        Dim mSr As Integer
        Dim mQry As String
        Dim mFreshEnquiryReceive As Double
        Dim mAmtDr As Double
        Dim mNarr As String

        If Validate_Data() = False Then Exit Sub

        mQry = "
                    Update SaleEnquiry Set 
                    SaleToPartyName=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPartyName).Value) & ",
                    SaleToPartyAddress=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowAddress).Value) & ",
                    SaleToPartyCity=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowCity).Tag) & ",
                    SaleToPartyPincode=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPincode).Value) & ",
                    SaleToPartyMobile=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowMobile).Value) & ",
                    SaleToPartySalesTaxNo=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSalesTaxNo).Value) & ",
                    SaleToPartyAadharNo=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowAadharNo).Value) & ",
                    SaleToPartyPanNo=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPanNo).Value) & ",
                    ShipToAddress=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowShipToAddress).Value) & "
                    Where DocId = '" & SearchCode & "'
                "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        'mQry = "Select IFNull(Max(Sr),0) From SaleEnquiryPayment With (NoLock) Where DocID = '" & SearchCode & "' "
        'mSr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar

        'For I = 0 To Dgl2.RowCount - 1
        '    If Dgl2.Rows(I).Visible Then
        '        If Val(Dgl2.Item(Col2Amount, I).Value) <> 0 Then
        '            If Dgl2.Item(ColSNo, I).Tag Is Nothing Then
        '                mSr += 1
        '                mQry = " INSERT INTO SaleEnquiryPayment 
        '                        (DocID, Sr, PaymentMode, Amount, 
        '                        ReferenceNo, ReferenceDocID, ReferenceV_Type, ReferenceSr, 
        '                        PostToAc) 
        '                        VALUES (" & AgL.Chk_Text(SearchCode) & ",  " & mSr & ", " & AgL.Chk_Text(Dgl2.Item(Col2PaymentMode, I).Tag) & ", " & Val(Dgl2.Item(Col2Amount, I).Value) & ",
        '                        " & AgL.Chk_Text(Dgl2.Item(Col2ReferenceNo, I).Value) & ", " & AgL.Chk_Text(Dgl2.Item(Col2ReferenceDocID, I).Value) & ", " & AgL.Chk_Text(Dgl2.Item(Col2ReferenceV_Type, I).Value) & ", " & AgL.Chk_Text(Dgl2.Item(Col2ReferenceSr, I).Value) & ", 
        '                        " & AgL.Chk_Text(Dgl2.Item(Col2PostToAc, I).Tag) & ") "
        '                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        '            Else
        '                mQry = " Update SaleEnquiryPayment 
        '                        Set
        '                        PaymentMode = " & AgL.Chk_Text(Dgl2.Item(Col2PaymentMode, I).Tag) & ", 
        '                        Amount = " & Val(Dgl2.Item(Col2Amount, I).Value) & ",
        '                        ReferenceNo = " & AgL.Chk_Text(Dgl2.Item(Col2ReferenceNo, I).Value) & ", 
        '                        ReferenceDocID = " & AgL.Chk_Text(Dgl2.Item(Col2ReferenceDocID, I).Value) & ", 
        '                        ReferenceV_Type = " & AgL.Chk_Text(Dgl2.Item(Col2ReferenceV_Type, I).Value) & ", 
        '                        ReferenceSr = " & AgL.Chk_Text(Dgl2.Item(Col2ReferenceSr, I).Value) & ", 
        '                        PostToAc = " & AgL.Chk_Text(Dgl2.Item(Col2PostToAc, I).Tag) & " 
        '                        Where DocID = " & AgL.Chk_Text(SearchCode) & " And Sr = " & Val(Dgl2.Item(ColSNo, I).Tag) & " "
        '                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        '            End If
        '        End If
        '    Else
        '        If Dgl2.Item(ColSNo, I).Tag IsNot Nothing Then
        '            mQry = "Delete from SaleEnquiryPayment  Where DocID = '" & SearchCode & "'  And Sr = " & Dgl2.Item(ColSNo, I).Tag & ""
        '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        '        End If
        '    End If
        'Next





        'If Val(TxtFreshEnquiryReceived.Text) >= Val(LblInvoiceAmount.Text) Then
        '    mFreshEnquiryReceive = Val(LblInvoiceAmount.Text)
        'Else
        '    mFreshEnquiryReceive = Val(TxtFreshEnquiryReceived.Text)
        'End If

        mSr = 1000
        'If mFreshEnquiryReceive > 0 Then
        '    mNarr = "Payment receive through FreshEnquiry"
        '    mSr += 1
        '    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr,
        '            Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode) 
        '            Values ('" & SearchCode & "','" & mObjFrmSaleEnquiry.TxtReferenceNo.Text & "'," & mSr & ", 
        '            " & AgL.Chk_Date(CDate(objFrmSaleEnquiry.TxtV_Date.Text).ToString("u")) & "," & AgL.Chk_Text(AgL.XNull(AgL.PubDtEnviro.Rows(0)("FreshEnquiryAc"))) & ", 
        '            " & AgL.Chk_Text(mObjFrmSaleEnquiry.TxtBillToParty.Tag) & ", 
        '            " & Val(mFreshEnquiryReceive) & ", 0, 
        '            " & AgL.Chk_Text(mNarr) & ",'" & objFrmSaleEnquiry.TxtV_Type.AgSelectedValue & "'," & Val(objFrmSaleEnquiry.TxtV_No.Text) & ", 
        '            '" & objFrmSaleEnquiry.LblPrefix.Text & "','" & objFrmSaleEnquiry.TxtSite_Code.Tag & "','" & objFrmSaleEnquiry.TxtDivision.Tag & "')"
        '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        '    mAmtDr += mFreshEnquiryReceive
        'End If

        'For I = 0 To Dgl2.RowCount - 1
        '    If Dgl2.Rows(I).Visible Then
        '        If Val(Dgl2.Item(Col2Amount, I).Value) <> 0 Then
        '            mSr += 1
        '            mNarr = "Payment received through " & Dgl2.Item(Col2PaymentMode, I).Value & IIf(Dgl2.Item(Col2ReferenceNo, I).Value <> "", " Ref. No - " & Dgl2.Item(Col2ReferenceNo, I).Value, "")

        '            mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr,
        '            Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode) 
        '            Values ('" & SearchCode & "','" & mObjFrmSaleEnquiry.TxtReferenceNo.Text & "'," & mSr & ", 
        '            " & AgL.Chk_Date(CDate(objFrmSaleEnquiry.TxtV_Date.Text).ToString("s")) & "," & AgL.Chk_Text(Dgl2.Item(Col2PostToAc, I).Tag) & ", 
        '            " & AgL.Chk_Text(mObjFrmSaleEnquiry.TxtBillToParty.Tag) & ", 
        '            " & Val(Dgl2.Item(Col2Amount, I).Value) & ", 0, 
        '            " & AgL.Chk_Text(mNarr) & ",'" & objFrmSaleEnquiry.TxtV_Type.AgSelectedValue & "'," & Val(objFrmSaleEnquiry.TxtV_No.Text) & ", 
        '            '" & objFrmSaleEnquiry.LblPrefix.Text & "','" & objFrmSaleEnquiry.TxtSite_Code.Tag & "','" & objFrmSaleEnquiry.TxtDivision.Tag & "')"
        '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        '            mAmtDr += Val(Dgl2.Item(Col2Amount, I).Value)
        '        End If
        '    End If
        'Next


        'If mAmtDr > 0 Then
        '    mSr += 1
        '    mNarr = "Payment received "
        '    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr,
        '            Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode) 
        '            Values ('" & SearchCode & "','" & mObjFrmSaleEnquiry.TxtReferenceNo.Text & "'," & mSr & ", 
        '            " & AgL.Chk_Date(CDate(objFrmSaleEnquiry.TxtV_Date.Text).ToString("s")) & "," & AgL.Chk_Text(mObjFrmSaleEnquiry.TxtBillToParty.Tag) & ", 
        '            " & AgL.Chk_Text(AgL.XNull(AgL.PubDtEnviro.Rows(0)("FreshEnquiryAc"))) & ", 
        '            0, " & Val(LblInvoiceAmount.Text) & ",
        '            " & AgL.Chk_Text(mNarr) & ",'" & objFrmSaleEnquiry.TxtV_Type.AgSelectedValue & "'," & Val(objFrmSaleEnquiry.TxtV_No.Text) & ", 
        '            '" & objFrmSaleEnquiry.LblPrefix.Text & "','" & objFrmSaleEnquiry.TxtSite_Code.Tag & "','" & objFrmSaleEnquiry.TxtDivision.Tag & "')"
        '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        'End If
    End Sub

    'Private Sub Dgl2_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl2.EditingControl_KeyDown
    '    Try
    '        Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
    '            Case Col2PaymentMode
    '                'If e.KeyCode = Keys.Insert Then Call FOpenSaleEnquiry()
    '                If e.KeyCode <> Keys.Enter Then
    '                    If Dgl2.AgHelpDataSet(Col2PaymentMode) Is Nothing Then
    '                        mQry = "Select H.Code, H.Description From PaymentMode H  With (NoLock) Order By H.Description"
    '                        Dgl2.AgHelpDataSet(Col2PaymentMode) = AgL.FillData(mQry, AgL.GCn)
    '                    End If
    '                End If

    '            Case Col2ReferenceNo
    '                If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
    '                    If Dgl2.AgHelpDataSet(Col2ReferenceNo) Is Nothing Then

    '                    End If
    '                End If

    '            Case Col2PostToAc
    '                If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
    '                    If Dgl2.AgHelpDataSet(Col2PostToAc) Is Nothing Then
    '                        mQry = "Select H.Code, H.Name From viewHelpSubgroup H  With (NoLock) where H.Nature In ('Customer') Order By H.Name"
    '                        Dgl2.AgHelpDataSet(Col2PostToAc) = AgL.FillData(mQry, AgL.GCn)
    '                    End If
    '                End If

    '        End Select
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub


    'Private Sub Dgl2_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl2.EditingControl_Validating
    '    If EntryMode = "Browse" Then Exit Sub
    '    Dim mRowIndex As Integer, mColumnIndex As Integer
    '    Dim DrTemp As DataRow() = Nothing
    '    Dim DtTemp As DataTable
    '    Try
    '        mRowIndex = Dgl2.CurrentCell.RowIndex
    '        mColumnIndex = Dgl2.CurrentCell.ColumnIndex
    '        If Dgl2.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl2.Item(mColumnIndex, mRowIndex).Value = ""
    '        Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name

    '            Case Col2PaymentMode
    '                mQry = "Select PostToAc, Sg.Name as PostToAcName From PaymentModeAccount H  With (NoLock) Left Join viewHelpSubgroup Sg On H.PostToAc = Sg.Code Where H.PaymentMode=" & AgL.Chk_Text(Dgl2.Item(Col2PaymentMode, mRowIndex).Tag) & " And Div_Code =" & AgL.Chk_Text(mDivisionCode) & " And Site_Code=" & AgL.Chk_Text(mSiteCode) & "  "
    '                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '                If DtTemp.Rows.Count = 0 Then
    '                    mQry = "Select PostToAc, Sg.Name as PostToAcName From PaymentModeAccount H  With (NoLock) Left Join viewHelpSubgroup Sg On H.PostToAc = Sg.Code Where H.PaymentMode=" & AgL.Chk_Text(Dgl2.Item(Col2PaymentMode, mRowIndex).Tag) & " And Div_Code =" & AgL.Chk_Text(mDivisionCode) & " And Site_Code Is Null  "
    '                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '                    If DtTemp.Rows.Count = 0 Then
    '                        mQry = "Select PostToAc, Sg.Name as PostToAcName From PaymentModeAccount H  With (NoLock) Left Join viewHelpSubgroup Sg On H.PostToAc = Sg.Code Where H.PaymentMode=" & AgL.Chk_Text(Dgl2.Item(Col2PaymentMode, mRowIndex).Tag) & " And Div_Code Is Null And Site_Code=" & AgL.Chk_Text(mSiteCode) & "  "
    '                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '                        If DtTemp.Rows.Count = 0 Then
    '                            mQry = "Select PostToAc, Sg.Name as PostToAcName From PaymentModeAccount H  With (NoLock) Left Join viewHelpSubgroup Sg On H.PostToAc = Sg.Code Where H.PaymentMode=" & AgL.Chk_Text(Dgl2.Item(Col2PaymentMode, mRowIndex).Tag) & " And Div_Code Is Null And Site_Code Is Null  "
    '                            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '                        End If
    '                    End If
    '                End If
    '                If DtTemp.Rows.Count > 0 Then
    '                    Dgl2.Item(Col2PostToAc, mRowIndex).Tag = AgL.XNull(DtTemp.Rows(0)("PostToAc"))
    '                    Dgl2.Item(Col2PostToAc, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("PostToAcName"))
    '                End If

    '                If Dgl2.Item(Col2PaymentMode, mRowIndex).Value <> "" Then
    '                    If Val(LblBalanceToReceipt.Text) > 0 Then
    '                        Dgl2.Item(Col2Amount, mRowIndex).Value = Format(Val(LblBalanceToReceipt.Text), "0.00")
    '                    End If
    '                End If
    '                Calculation()

    '                If Val(LblBalanceToReceipt.Text) = 0 Then BtnOk.Focus()

    '        End Select

    '        Call Calculation()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub TxtFreshEnquiryReceived_Validating(sender As Object, e As CancelEventArgs)
    '    Calculation()
    '    'If Val(LblBalanceToReceipt.Text) = 0 Then
    '    BtnOk.Focus()
    '        'Else
    '        '    Dgl2.Focus()
    '    End If
    'End Sub

    Private Sub TxtFreshEnquiryReceived_TextChanged(sender As Object, e As EventArgs)
        Calculation()
    End Sub

    Private Sub FrmSaleEnquiryParty_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Private Sub FrmSaleEnquiryParty_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Dgl1 IsNot Nothing Then
            If Dgl1.FirstDisplayedCell IsNot Nothing Then
                Dgl1.CurrentCell = Dgl1(Col1Value, rowPartyName) 'Dgl1.FirstDisplayedCell
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
                If Dgl1.Item(Col1Value, rowPartyName).Value = "" Then
                    Dgl1.CurrentCell = Dgl1(Col1Value, rowPartyName) 'Dgl1.FirstDisplayedCell
                    Dgl1.Focus()
                End If
            End If
        End If
    End Sub
End Class