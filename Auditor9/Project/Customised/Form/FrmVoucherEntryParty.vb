Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Public Class FrmVoucherEntryParty
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
    Public Const rowPincode As Integer = 3
    Public Const rowMobile As Integer = 4
    Public Const rowSalesTaxGroup As Integer = 5
    Public Const rowPlaceOfSupply As Integer = 6
    Public Const rowSalesTaxNo As Integer = 7
    Public Const rowShipToAddress As Integer = 8

    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mToQtyDecimalPlace As Integer
    Dim mAcGroupNature As String


    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
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

    Public Sub IniGrid(DocID As String, PartyCode As String, AcGroupNature As String)
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 300, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False


        Dgl1.Rows.Add(9)
        Dgl1.Item(Col1Head, rowPartyName).Value = "Party Name"
        Dgl1.Item(Col1Head, rowAddress).Value = "Address"
        Dgl1.Item(Col1Head, rowCity).Value = "City"
        Dgl1.Item(Col1Head, rowPincode).Value = "Pincode"
        Dgl1.Item(Col1Head, rowMobile).Value = "Mobile No."
        Dgl1.Item(Col1Head, rowSalesTaxGroup).Value = "Sales Tax Group"
        Dgl1.Item(Col1Head, rowPlaceOfSupply).Value = "Place Of Supply"
        Dgl1.Item(Col1Head, rowSalesTaxNo).Value = "GST No."
        Dgl1.Item(Col1Head, rowShipToAddress).Value = "Ship To Address"



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
            Me.Top = 300
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


            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            If mAcGroupNature.ToUpper() <> "CASH" Then
                Select Case Dgl1.CurrentCell.RowIndex
                    Case rowShipToAddress
                    Case Else
                        Dgl1.CurrentCell.ReadOnly = True
                End Select
            End If



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
                Case rowPlaceOfSupply
                    Dgl1.CurrentCell.ReadOnly = True
                Case rowSalesTaxNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 15
                Case rowShipToAddress
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 255
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
    '    If e.Control And e.KeyCode = Keys.D Then
    '        sender.CurrentRow.Selected = True
    '    End If
    '    If e.Control Or e.Shift Or e.Alt Then Exit Sub
    'End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If mEntryMode = "Browse" Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Select Case Dgl1.CurrentCell.RowIndex
                Case rowCity
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select C.CityCode, C.CityName from City C Order by c.CityName "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowSalesTaxGroup
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select H.Description as Code, H.Description from PostingGroupSalesTaxParty H Order By H.Description"
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
                Case rowCity
                    Dgl1.Item(Col1Value, rowPlaceOfSupply).Value = ClsFunction.GetPlaceOfSupply(Dgl1.Item(Col1Value, rowCity).Tag)
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

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub


    Public Sub FMoveRec(ByVal SearchCode As String, ByVal PartyCode As String, ByVal PartyNature As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0


        Try

            If PartyCode <> "" Then
                If PartyNature.ToUpper() = "CASH" Then
                    Dgl1.Item(Col1Value, rowCity).Value = AgL.PubSiteCity
                    Dgl1.Item(Col1Value, rowCity).Tag = AgL.PubSiteCityCode
                    Dgl1.Item(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupParty"))
                    Dgl1.Item(Col1Value, rowPlaceOfSupply).Value = ClsFunction.GetPlaceOfSupply(Dgl1.Item(Col1Value, rowCity).Tag)
                Else

                    'BtnHeaderDetail.Tag = FunRetNewUnitConversionObject()
                    'BtnHeaderDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
                    mQry = "SELECT H.DispName PartyName, H.Address as PartyAddress, H.CityCode as PartyCity, C.CityName, H.Pin as PartyPincode, H.Mobile PartyMobile, 
                    SR.RegistrationNo PartySalesTaxNo, H.SalesTaxPostingGroup
                    FROM Subgroup H                      
                    Left Join City C On H.CityCode = C.CityCode    
                    Left Join SubgroupRegistration SR on SR.Subcode = H.Subcode and SR.RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'                
                    WHERE H.Subcode = '" & PartyCode & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    With DtTemp
                        'BtnHeaderDetail.Tag.Dgl1.RowCount = 1 : BtnHeaderDetail.Tag.Dgl1.Rows.Clear()
                        If DtTemp.Rows.Count > 0 Then
                            Dgl1.Item(Col1Value, rowPartyName).Value = AgL.XNull(DtTemp.Rows(0)("PartyName"))
                            Dgl1.Item(Col1Value, rowAddress).Value = AgL.XNull(DtTemp.Rows(0)("PartyAddress"))
                            Dgl1.Item(Col1Value, rowCity).Value = AgL.XNull(DtTemp.Rows(0)("CityName"))
                            Dgl1.Item(Col1Value, rowCity).Tag = AgL.XNull(DtTemp.Rows(0)("PartyCity"))
                            Dgl1.Item(Col1Value, rowPincode).Value = AgL.XNull(DtTemp.Rows(0)("PartyPincode"))
                            Dgl1.Item(Col1Value, rowMobile).Value = AgL.XNull(DtTemp.Rows(0)("PartyMobile"))
                            Dgl1.Item(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(DtTemp.Rows(0)("SalesTaxPostingGroup"))
                            Dgl1.Item(Col1Value, rowSalesTaxNo).Value = AgL.XNull(.Rows(0)("PartySalesTaxNo"))
                            Dgl1.Item(Col1Value, rowPlaceOfSupply).Value = ClsFunction.GetPlaceOfSupply(Dgl1.Item(Col1Value, rowCity).Tag)
                        End If
                    End With
                End If
            Else
                'BtnHeaderDetail.Tag = FunRetNewUnitConversionObject()
                'BtnHeaderDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
                mQry = "SELECT H.PartyName, H.PartyAddress, H.PartyCity, C.CityName, H.PartyPincode, H.PartyMobile, 
                    H.PartySalesTaxNo, H.SalesTaxGroupParty, H.PlaceOfSupply, H.ShipToAddress
                    FROM LedgerHead H                      
                    Left Join City C On H.PartyCity = C.CityCode                    
                    WHERE H.DocId = '" & SearchCode & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                With DtTemp

                    'BtnHeaderDetail.Tag.Dgl1.RowCount = 1 : BtnHeaderDetail.Tag.Dgl1.Rows.Clear()
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1.Item(Col1Value, rowPartyName).Value = AgL.XNull(DtTemp.Rows(0)("PartyName"))
                        Dgl1.Item(Col1Value, rowAddress).Value = AgL.XNull(DtTemp.Rows(0)("PartyAddress"))
                        Dgl1.Item(Col1Value, rowCity).Value = AgL.XNull(DtTemp.Rows(0)("CityName"))
                        Dgl1.Item(Col1Value, rowCity).Tag = AgL.XNull(DtTemp.Rows(0)("PartyCity"))
                        Dgl1.Item(Col1Value, rowPincode).Value = AgL.XNull(DtTemp.Rows(0)("PartyPincode"))
                        Dgl1.Item(Col1Value, rowMobile).Value = AgL.XNull(DtTemp.Rows(0)("PartyMobile"))
                        Dgl1.Item(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(DtTemp.Rows(0)("SalesTaxGroupParty"))
                        Dgl1.Item(Col1Value, rowPlaceOfSupply).Value = AgL.XNull(.Rows(0)("PlaceOfSupply"))
                        Dgl1.Item(Col1Value, rowSalesTaxNo).Value = AgL.XNull(.Rows(0)("PartySalesTaxNo"))
                        Dgl1.Item(Col1Value, rowShipToAddress).Value = AgL.XNull(.Rows(0)("ShipToAddress"))
                    End If
                End With

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Public Sub FSave(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)

        mQry = "
                    Update LedgerHead Set 
                    PartyName=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPartyName).Value) & ",
                    PartyAddress=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowAddress).Value) & ",
                    PartyCity=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowCity).Tag) & ",
                    PartyPincode=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPincode).Value) & ",
                    PartyMobile=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowMobile).Value) & ",
                    SalesTaxGroupParty=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSalesTaxGroup).Value) & ",
                    PlaceOfSupply=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPlaceOfSupply).Value) & ",
                    PartySalesTaxNo=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSalesTaxNo).Value) & ",
                    ShipToAddress=" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowShipToAddress).Value) & "
                    Where DocId = '" & SearchCode & "'
                "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


    End Sub

End Class