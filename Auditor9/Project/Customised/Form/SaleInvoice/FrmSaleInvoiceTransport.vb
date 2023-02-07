Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmSaleInvoiceTransport
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"



    Public Const rowTransporter As Integer = 0
    Public Const rowLrNo As Integer = 1
    Public Const rowLrDate As Integer = 2
    Public Const rowNoOfBales As Integer = 3
    Public Const rowPrivateMark As Integer = 4
    Public Const rowWeight As Integer = 5
    Public Const rowChargedWeight As Integer = 6
    Public Const rowFreight As Integer = 7
    Public Const rowLrPaymentType As Integer = 8
    Public Const rowVehicleNo As Integer = 9
    Public Const rowShipMethod As Integer = 10
    Public Const rowPreCarriageBy As Integer = 11
    Public Const rowPreCarriagePlace As Integer = 12
    Public Const rowBookedFrom As Integer = 13
    Public Const rowBookedTo As Integer = 14
    Public Const rowDestination As Integer = 15
    Public Const rowDescriptionOfGoods As Integer = 16
    Public Const rowDescriptionOfPacking As Integer = 17
    Public Const rowRoadPermitNo As Integer = 18
    Public Const rowRoadPermitDate As Integer = 19


    Public Const hcTransporter As String = "Transporter"
    Public Const hcLrNo As String = "LR No"
    Public Const hcLrDate As String = "LR Date"
    Public Const hcNoOfBales As String = "No. Of Bales"
    Public Const hcPrivateMark As String = "Private Mark"
    Public Const hcWeight As String = "Weight"
    Public Const hcChargedWeight As String = "Charged Weight"
    Public Const hcFreight As String = "Freight"
    Public Const hcLrPaymentType As String = "Payment Type"
    Public Const hcVehicleNo As String = "Vehicle No"
    Public Const hcShipMethod As String = "Ship Method"
    Public Const hcPreCarriageBy As String = "Pre Carriage By"
    Public Const hcPreCarriagePlace As String = "Pre Carriage Place"
    Public Const hcBookedFrom As String = "Booked From"
    Public Const hcBookedTo As String = "Booked To"
    Public Const hcDestination As String = "Destination"
    Public Const hcDescriptionOfGoods As String = "Description of Goods"
    Public Const hcDescriptionOfPacking As String = "Description of Packing"
    Public Const hcRoadPermitNo As String = "Road Permit No"
    Public Const hcRoadPermitDate As String = "Road Permit Date"

    Dim mEntryMode$ = ""
    Dim mNcat As String = ""
    Dim mUnit$ = ""
    Dim mToQtyDecimalPlace As Integer
    Dim mSearchcode As String
    Dim mCopyToSearchCodesArr As String()

    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property

    Public Property Ncat() As String
        Get
            Ncat = mNcat
        End Get
        Set(ByVal value As String)
            mNcat = value
        End Set
    End Property



    Public Property CopyToSearchCodesArr() As String()
        Get
            CopyToSearchCodesArr = mCopyToSearchCodesArr
        End Get
        Set(ByVal value As String())
            mCopyToSearchCodesArr = value
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

    Private Sub ApplyUISetting(NCat As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer


        Try
            For I = 0 To Dgl1.Rows.Count - 1
                Dgl1.Rows(I).Visible = False
            Next


            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='" & Me.Name & "' And NCat = '" & NCat & "' And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Item(Col1HeadOriginal, J).Value Then
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
            If mDgl1RowCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True



        Catch ex As Exception
            MsgBox(ex.Message & " [ApplyUISetting]")
        End Try
    End Sub



    Public Sub IniGrid(SearchCode As String)
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 300, 255, Col1Value, True, False)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 0, 255, Col1HeadOriginal, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.Name = "Dgl1"


        Dgl1.Rows.Add(20)
        Dgl1.Item(Col1Head, rowTransporter).Value = hcTransporter
        Dgl1.Item(Col1Head, rowVehicleNo).Value = hcVehicleNo
        Dgl1.Item(Col1Head, rowShipMethod).Value = hcShipMethod
        Dgl1.Item(Col1Head, rowPreCarriageBy).Value = hcPreCarriageBy
        Dgl1.Item(Col1Head, rowPreCarriagePlace).Value = hcPreCarriagePlace
        Dgl1.Item(Col1Head, rowBookedFrom).Value = hcBookedFrom
        Dgl1.Item(Col1Head, rowBookedTo).Value = hcBookedTo
        Dgl1.Item(Col1Head, rowDestination).Value = hcDestination
        Dgl1.Item(Col1Head, rowDescriptionOfGoods).Value = hcDescriptionOfGoods
        Dgl1.Item(Col1Head, rowDescriptionOfPacking).Value = hcDescriptionOfPacking
        Dgl1.Item(Col1Head, rowLrNo).Value = hcLrNo
        Dgl1.Item(Col1Head, rowLrDate).Value = hcLrDate
        Dgl1.Item(Col1Head, rowNoOfBales).Value = hcNoOfBales
        Dgl1.Item(Col1Head, rowPrivateMark).Value = hcPrivateMark
        Dgl1.Item(Col1Head, rowWeight).Value = hcWeight
        Dgl1.Item(Col1Head, rowChargedWeight).Value = hcChargedWeight
        Dgl1.Item(Col1Head, rowFreight).Value = hcFreight
        Dgl1.Item(Col1Head, rowLrPaymentType).Value = hcLrPaymentType
        Dgl1.Item(Col1Head, rowRoadPermitNo).Value = hcRoadPermitNo
        Dgl1.Item(Col1Head, rowRoadPermitDate).Value = hcRoadPermitDate

        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1HeadOriginal, I).Value = Dgl1(Col1Head, I).Value
        Next

        ApplyUISetting(Ncat)
        FMoveRec(SearchCode)
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
            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            Select Case Dgl1.CurrentCell.RowIndex
                Case rowLrNo, rowPrivateMark
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 50
                Case rowRoadPermitNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 20
                Case rowLrDate, rowRoadPermitDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                Case rowWeight, rowFreight
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 8
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                Case rowNoOfBales
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 8
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 0
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

            'If e.KeyCode = Keys.Enter Then Exit Sub
            'If mEntryMode = "Browse" Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Select Case Dgl1.CurrentCell.RowIndex
                Case rowTransporter
                    If e.KeyCode = Keys.Insert Then
                        Dim bSearchCode As String
                        bSearchCode = ClsMain.FOpenPartyMaster(SubgroupType.Transporter)
                        If bSearchCode <> "" Then
                            Dgl1.Item(Col1Head, rowTransporter).Tag = Nothing
                            Dgl1(Col1Value, rowTransporter).Tag = bSearchCode
                            Dgl1(Col1Value, rowTransporter).Value = AgL.XNull(AgL.Dman_Execute("Select Name From viewHelpSubgroup Where Code = '" & bSearchCode & "'", AgL.GCn).ExecuteScalar)
                            SendKeys.Send("{Enter}")
                        End If
                    Else
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name From viewHelpSubgroup Where SubgroupType = 'Transporter' "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
                Case rowLrPaymentType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT 'TO PAY' as Code, 'TO PAY' as Name Union All SELECT 'PAID' as Code, 'PAID' as Name Union All SELECT 'TO BE BILLED' as Code, 'TO BE BILLED' as Name "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                Case rowBookedFrom
                    If e.KeyCode = Keys.Insert Then
                        Dim bCityCode As String
                        bCityCode = ClsMain.FOpenCityMaster()
                        If bCityCode <> "" Then
                            Dgl1(Col1Value, rowBookedFrom).Tag = bCityCode
                            Dgl1(Col1Value, rowBookedFrom).Value = AgL.XNull(AgL.Dman_Execute("Select CityName From City Where CityCode = '" & bCityCode & "'", AgL.GCn).ExecuteScalar)
                            SendKeys.Send("{Enter}")
                        End If
                    Else
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT C.CityCode AS Code, C.CityName AS Name, State.Description AS State FROM City C LEFT JOIN State ON State.Code = C.State ORDER BY C.CityName"
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowBookedTo
                    If e.KeyCode = Keys.Insert Then
                        Dim bCityCode As String
                        bCityCode = ClsMain.FOpenCityMaster()
                        If bCityCode <> "" Then
                            Dgl1(Col1Value, rowBookedTo).Tag = bCityCode
                            Dgl1(Col1Value, rowBookedTo).Value = AgL.XNull(AgL.Dman_Execute("Select CityName From City Where CityCode = '" & bCityCode & "'", AgL.GCn).ExecuteScalar)
                        End If
                    Else
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT C.CityCode AS Code, C.CityName AS Name, State.Description AS State FROM City C LEFT JOIN State ON State.Code = C.State ORDER BY C.CityName"
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
                Case rowDestination
                    If e.KeyCode = Keys.Insert Then
                        Dim bCityCode As String
                        bCityCode = ClsMain.FOpenCityMaster()
                        If bCityCode <> "" Then
                            Dgl1(Col1Value, rowDestination).Tag = bCityCode
                            Dgl1(Col1Value, rowDestination).Value = AgL.XNull(AgL.Dman_Execute("Select CityName From City Where CityCode = '" & bCityCode & "'", AgL.GCn).ExecuteScalar)
                        End If
                    Else
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT C.CityCode AS Code, C.CityName AS Name, State.Description AS State FROM City C LEFT JOIN State ON State.Code = C.State ORDER BY C.CityName"
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select

            If e.KeyCode = Keys.Enter Then
                Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(Dgl1)
                If Dgl1.CurrentCell.RowIndex = LastCell.RowIndex And Dgl1.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                    BtnOk.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
    '    If EntryMode = "Browse" Then Exit Sub
    '    Dim mRowIndex As Integer, mColumnIndex As Integer
    '    Try
    '        mRowIndex = Dgl1.CurrentCell.RowIndex
    '        mColumnIndex = Dgl1.CurrentCell.ColumnIndex
    '        If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
    '        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
    '            'Case Col1FromUnit
    '            '    Dgl1.Item(Col1Equal, mRowIndex).Value = "="
    '            '    Dgl1.Item(Col1ToUnit, mRowIndex).Value = mUnit
    '            '    Dgl1.Item(Col1ToQtyDecimalPlaces, mRowIndex).Value = mToQtyDecimalPlace
    '            '    If Val(Dgl1.Item(Col1FromQty, mRowIndex).Value) = 0 Then
    '            '        Dgl1.Item(Col1FromQty, mRowIndex).Value = "1"
    '            '    End If

    '            '    If Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) Is Nothing Then Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) = ""

    '            '    If Dgl1.Item(Col1FromUnit, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex).ToString.Trim = "" Then
    '            '        Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = ""
    '            '    Else
    '            '        If Dgl1.AgDataRow IsNot Nothing Then
    '            '            Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DecimalPlaces").Value)
    '            '        End If
    '            '    End If


    '        End Select
    '        Calculation()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Public Function DataValidation() As Boolean
        DataValidation = False

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Mandatory, I).Value <> "" Then
                If Dgl1(Col1Value, I).Value = "" Then
                    MsgBox(Dgl1.Item(Col1Head, I).Value & " can not be blank...!", MsgBoxStyle.Information)
                    Exit Function
                End If
            End If
        Next

        DataValidation = True
    End Function
    Public Shared Function DataValidationForMainInvoice(mDocId As String, NCat As String) As Boolean
        DataValidationForMainInvoice = False

        Dim mQry As String = ""

        mQry = "Select H.* From EntryHeaderUISetting H                   
                    Where EntryName='" & FrmSaleInvoiceTransport.Name & "' And NCat = '" & NCat & "' And GridName ='Dgl1' 
                    And IfNull(IsMandatory,0) <> 0 "
        Dim DtMandatoryFields As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtMandatoryFields.Rows.Count > 0 Then
            mQry = " Select * From SaleInvoiceTransport Where DocId = '" & mDocId & "'"
            Dim DtSaleInvoiceTransport As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtSaleInvoiceTransport.Rows.Count = 0 Then
                MsgBox(AgL.XNull(DtMandatoryFields.Rows(0)("FieldName")) & " is blank.", MsgBoxStyle.Information)
                Exit Function
            Else
                For I As Integer = 0 To DtMandatoryFields.Rows.Count - 1
                    If AgL.XNull(DtMandatoryFields.Rows(I)("FieldName")) = hcBookedFrom And
                            AgL.XNull(DtSaleInvoiceTransport.Rows(0)("BookedFrom")) = "" Then
                        MsgBox("Booked From is blank...!", MsgBoxStyle.Information) : Exit Function
                    End If
                Next
            End If
        End If
        DataValidationForMainInvoice = True
    End Function
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then
                    If mSearchcode <> "" Then
                        If DataValidation() = False Then Exit Sub
                        FSave(mSearchcode, AgL.GCn, AgL.ECmd)
                        If mCopyToSearchCodesArr IsNot Nothing Then
                            For I As Integer = 0 To mCopyToSearchCodesArr.Length - 1
                                If mCopyToSearchCodesArr(I) <> "" And mCopyToSearchCodesArr(I) IsNot Nothing Then
                                    FSave(mCopyToSearchCodesArr(I), AgL.GCn, AgL.ECmd)
                                End If
                            Next
                        End If
                    End If
                    Me.Close()
                    Exit Sub
                Else
                    mOkButtonPressed = True
                    Me.Close()
                End If
        End Select
    End Sub
    Public Sub FMoveRec(ByVal SearchCode As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        If SearchCode = "" Then Exit Sub
        mSearchcode = SearchCode

        Try
            'BtnHeaderDetail.Tag = FunRetNewUnitConversionObject()
            'BtnHeaderDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
            mQry = "SELECT H.*, Transporter.Name as TransporterName
                    FROM SaleInvoiceTransport H                      
                    LEFT JOIN subgroup Transporter On H.Transporter = Transporter.SubCode 
                    WHERE H.DocId = '" & SearchCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp

                'BtnHeaderDetail.Tag.Dgl1.RowCount = 1 : BtnHeaderDetail.Tag.Dgl1.Rows.Clear()
                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1Value, rowTransporter).Tag = AgL.XNull(DtTemp.Rows(0)("Transporter"))
                    Dgl1.Item(Col1Value, rowTransporter).Value = AgL.XNull(.Rows(0)("TransporterName"))
                    Dgl1.Item(Col1Value, rowVehicleNo).Value = AgL.XNull(.Rows(0)("VehicleNo"))
                    Dgl1.Item(Col1Value, rowShipMethod).Value = AgL.XNull(.Rows(0)("ShipMethod"))
                    Dgl1.Item(Col1Value, rowPreCarriageBy).Value = AgL.XNull(.Rows(0)("PreCarriageBy"))
                    Dgl1.Item(Col1Value, rowPreCarriagePlace).Value = AgL.XNull(.Rows(0)("PreCarriagePlace"))
                    Dgl1.Item(Col1Value, rowNoOfBales).Value = AgL.XNull(.Rows(0)("NoOfBales"))
                    Dgl1.Item(Col1Value, rowBookedFrom).Value = AgL.XNull(.Rows(0)("BookedFrom"))
                    Dgl1.Item(Col1Value, rowBookedTo).Value = AgL.XNull(.Rows(0)("BookedTo"))
                    Dgl1.Item(Col1Value, rowDestination).Value = AgL.XNull(.Rows(0)("Destination"))
                    Dgl1.Item(Col1Value, rowDescriptionOfGoods).Value = AgL.XNull(.Rows(0)("DescriptionOfGoods"))
                    Dgl1.Item(Col1Value, rowDescriptionOfPacking).Value = AgL.XNull(.Rows(0)("DescriptionOfPacking"))
                    Dgl1.Item(Col1Value, rowLrNo).Value = AgL.XNull(.Rows(0)("LRNo"))
                    Dgl1.Item(Col1Value, rowLrDate).Value = AgL.RetDate(AgL.XNull(.Rows(0)("LRDate")))
                    Dgl1.Item(Col1Value, rowPrivateMark).Value = AgL.XNull(.Rows(0)("PrivateMark"))
                    Dgl1.Item(Col1Value, rowWeight).Value = AgL.XNull(.Rows(0)("Weight"))
                    Dgl1.Item(Col1Value, rowWeight).Value = AgL.XNull(.Rows(0)("ChargedWeight"))
                    Dgl1.Item(Col1Value, rowFreight).Value = AgL.XNull(.Rows(0)("Freight"))
                    Dgl1.Item(Col1Value, rowLrPaymentType).Value = AgL.XNull(.Rows(0)("PaymentType"))
                    Dgl1.Item(Col1Value, rowRoadPermitNo).Value = AgL.XNull(.Rows(0)("RoadPermitNo"))
                    Dgl1.Item(Col1Value, rowRoadPermitDate).Value = AgL.RetDate(AgL.XNull(.Rows(0)("RoadPermitDate")))
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FSave(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)

        mQry = "Delete From SaleInvoiceTransport Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Dgl1.Item(Col1Value, rowTransporter).Tag <> "" Or
            Dgl1.Item(Col1Value, rowVehicleNo).Value <> "" Or
            Dgl1.Item(Col1Value, rowShipMethod).Value <> "" Or
            Dgl1.Item(Col1Value, rowPreCarriageBy).Value <> "" Or
            Dgl1.Item(Col1Value, rowPreCarriagePlace).Value <> "" Or
            Dgl1.Item(Col1Value, rowBookedFrom).Value <> "" Or
            Dgl1.Item(Col1Value, rowBookedTo).Value <> "" Or
            Dgl1.Item(Col1Value, rowDestination).Value <> "" Or
            Dgl1.Item(Col1Value, rowDescriptionOfGoods).Value <> "" Or
            Dgl1.Item(Col1Value, rowDescriptionOfPacking).Value <> "" Or
            Dgl1.Item(Col1Value, rowLrNo).Value <> "" Or
            Dgl1.Item(Col1Value, rowLrDate).Value <> "" Or
            Val(Dgl1.Item(Col1Value, rowNoOfBales).Value) > 0 Or
            Dgl1.Item(Col1Value, rowPrivateMark).Value <> "" Or
            Val(Dgl1.Item(Col1Value, rowWeight).Value) > 0 Or
            Val(Dgl1.Item(Col1Value, rowChargedWeight).Value) > 0 Or
            Val(Dgl1.Item(Col1Value, rowFreight).Value) > 0 Or
            Dgl1.Item(Col1Value, rowLrPaymentType).Value <> "" Or
            Dgl1.Item(Col1Value, rowRoadPermitNo).Value <> "" Or
            Dgl1.Item(Col1Value, rowRoadPermitDate).Value <> "" Then


            mQry = "Insert Into SaleInvoiceTransport (DocID, Transporter, LRNo, LRDate, NoOfBales, PrivateMark, Weight, Freight, PaymentType, RoadPermitNo, RoadPermitDate, 
                            VehicleNo, ShipMethod, PreCarriageBy, PreCarriagePlace, BookedFrom, 
                            BookedTo, Destination, DescriptionOfGoods, DescriptionOfPacking, ChargedWeight)
                Values ('" & SearchCode & "', 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTransporter).Tag) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLrNo).Value) & ",
                " & AgL.Chk_Date(Dgl1.Item(Col1Value, rowLrDate).Value) & ",
                " & Val(Dgl1.Item(Col1Value, rowNoOfBales).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPrivateMark).Value) & ",
                " & Val(Dgl1.Item(Col1Value, rowWeight).Value) & ",
                " & Val(Dgl1.Item(Col1Value, rowFreight).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLrPaymentType).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRoadPermitNo).Value) & ",
                " & AgL.Chk_Date(Dgl1.Item(Col1Value, rowRoadPermitDate).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVehicleNo).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowShipMethod).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPreCarriageBy).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPreCarriagePlace).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBookedFrom).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBookedTo).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDestination).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDescriptionOfGoods).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDescriptionOfPacking).Value) & ",
                " & Val(Dgl1.Item(Col1Value, rowChargedWeight).Value) & "
                )
               "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            mQry = "INSERT INTO PurchInvoiceTransport (DocID, Transporter, LrNo, LrDate, PrivateMark, Weight, Freight, PaymentType, RoadPermitNo, RoadPermitDate, UploadDate,   NoOfBales)
                    SELECT (Select DocID From PurchInvoice Where GenDocID = '" & SearchCode & "') as  DocID, Transporter, LrNo, LrDate, PrivateMark, Weight, Freight, PaymentType, RoadPermitNo, RoadPermitDate, UploadDate,    NoOfBales
                    FROM SaleInvoiceTransport WHERE DocID = '" & SearchCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub

        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then
            If e.KeyCode = Keys.Delete Then
                Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value = ""
                Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag = ""
            End If

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowBookedFrom
                    If e.KeyCode = Keys.Insert Then
                        Dim bCityCode As String
                        bCityCode = ClsMain.FOpenCityMaster()
                        If bCityCode <> "" Then
                            Dgl1(Col1Value, rowBookedFrom).Tag = bCityCode
                            Dgl1(Col1Value, rowBookedFrom).Value = AgL.XNull(AgL.Dman_Execute("Select CityName From City Where CityCode = '" & bCityCode & "'", AgL.GCn).ExecuteScalar)
                            SendKeys.Send("{Enter}")
                        End If
                    End If
                Case rowBookedTo
                    If e.KeyCode = Keys.Insert Then
                        Dim bCityCode As String
                        bCityCode = ClsMain.FOpenCityMaster()
                        If bCityCode <> "" Then
                            Dgl1(Col1Value, rowBookedTo).Tag = bCityCode
                            Dgl1(Col1Value, rowBookedTo).Value = AgL.XNull(AgL.Dman_Execute("Select CityName From City Where CityCode = '" & bCityCode & "'", AgL.GCn).ExecuteScalar)
                            SendKeys.Send("{Enter}")
                        End If
                    End If
                Case rowDestination
                    If e.KeyCode = Keys.Insert Then
                        Dim bCityCode As String
                        bCityCode = ClsMain.FOpenCityMaster()
                        If bCityCode <> "" Then
                            Dgl1(Col1Value, rowDestination).Tag = bCityCode
                            Dgl1(Col1Value, rowDestination).Value = AgL.XNull(AgL.Dman_Execute("Select CityName From City Where CityCode = '" & bCityCode & "'", AgL.GCn).ExecuteScalar)
                            SendKeys.Send("{Enter}")
                        End If
                    End If
                Case rowTransporter
                    If e.KeyCode = Keys.Insert Then
                        Dim bSearchCode As String
                        bSearchCode = ClsMain.FOpenPartyMaster(SubgroupType.Transporter)
                        If bSearchCode <> "" Then
                            Dgl1(Col1Value, rowTransporter).Tag = bSearchCode
                            Dgl1(Col1Value, rowTransporter).Value = AgL.XNull(AgL.Dman_Execute("Select Name From viewHelpSubgroup Where Code = '" & bSearchCode & "'", AgL.GCn).ExecuteScalar)
                            SendKeys.Send("{Enter}")
                        End If
                    End If

            End Select
        End If

        If e.KeyCode = Keys.Enter Then
            Dim LastCell As DataGridViewCell = ClsMain.LastDisplayedCell(Dgl1)
            If Dgl1.CurrentCell.RowIndex = LastCell.RowIndex And Dgl1.CurrentCell.ColumnIndex = LastCell.ColumnIndex Then
                BtnOk.Focus()
            End If
        End If
    End Sub
End Class