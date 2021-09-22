Imports System.Data.SQLite
Public Class FrmSaleInvoiceTransport
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"


    Public Const rowTransporter As Integer = 0
    Public Const rowLrNo As Integer = 1
    Public Const rowLrDate As Integer = 2
    Public Const rowPrivateMark As Integer = 3
    Public Const rowWeight As Integer = 4
    Public Const rowFreight As Integer = 5
    Public Const rowLrPaymentType As Integer = 6
    Public Const rowRoadPermitNo As Integer = 7
    Public Const rowRoadPermitDate As Integer = 8

    Dim mEntryMode$ = ""
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

            'If mEntryMode.ToString.ToUpper() = "BROWSE" Then
            '    Dgl1.ReadOnly = True
            'Else
            '    Dgl1.ReadOnly = False
            'End If
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

    Public Sub IniGrid(SearchCode As String)
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
        Dgl1.RowHeadersVisible = False


        Dgl1.Rows.Add(9)
        Dgl1.Item(Col1Head, rowTransporter).Value = "Transporter"
        Dgl1.Item(Col1Head, rowLrNo).Value = "L.R. No."
        Dgl1.Item(Col1Head, rowLrDate).Value = "L.R. Date"
        Dgl1.Item(Col1Head, rowPrivateMark).Value = "Private Mark"
        Dgl1.Item(Col1Head, rowWeight).Value = "Weight"
        Dgl1.Item(Col1Head, rowFreight).Value = "Freight"
        Dgl1.Item(Col1Head, rowLrPaymentType).Value = "L.R. Payment Type"
        Dgl1.Item(Col1Head, rowRoadPermitNo).Value = "EWay Bill No"
        Dgl1.Item(Col1Head, rowRoadPermitDate).Value = "EWay Bill Date"


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
            'If mEntryMode = "Browse" Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Select Case Dgl1.CurrentCell.RowIndex
                Case rowTransporter
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Name From viewHelpSubgroup Where SubgroupType = 'Transporter' "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                Case rowLrPaymentType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT 'TO PAY' as Code, 'TO PAY' as Name Union All SELECT 'PAID' as Code, 'PAID' as Name Union All SELECT 'TO BE BILLED' as Code, 'TO BE BILLED' as Name "
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

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then
                    If mSearchcode <> "" Then
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
                    LEFT JOIN viewHelpSubgroup Transporter On H.Transporter = Transporter.Code 
                    WHERE H.DocId = '" & SearchCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp

                'BtnHeaderDetail.Tag.Dgl1.RowCount = 1 : BtnHeaderDetail.Tag.Dgl1.Rows.Clear()
                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1Value, rowTransporter).Tag = AgL.XNull(DtTemp.Rows(0)("Transporter"))
                    Dgl1.Item(Col1Value, rowTransporter).Value = AgL.XNull(.Rows(0)("TransporterName"))
                    Dgl1.Item(Col1Value, rowLrNo).Value = AgL.XNull(.Rows(0)("LRNo"))
                    Dgl1.Item(Col1Value, rowLrDate).Value = AgL.RetDate(AgL.XNull(.Rows(0)("LRDate")))
                    Dgl1.Item(Col1Value, rowPrivateMark).Value = AgL.XNull(.Rows(0)("PrivateMark"))
                    Dgl1.Item(Col1Value, rowWeight).Value = AgL.XNull(.Rows(0)("Weight"))
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
           Dgl1.Item(Col1Value, rowLrNo).Value <> "" Or
           Dgl1.Item(Col1Value, rowLrDate).Value <> "" Or
           Dgl1.Item(Col1Value, rowPrivateMark).Value <> "" Or
           Val(Dgl1.Item(Col1Value, rowWeight).Value) > 0 Or
           Val(Dgl1.Item(Col1Value, rowFreight).Value) > 0 Or
           Dgl1.Item(Col1Value, rowLrPaymentType).Value <> "" Or
           Dgl1.Item(Col1Value, rowRoadPermitNo).Value <> "" Or
           Dgl1.Item(Col1Value, rowRoadPermitDate).Value <> "" Then


            mQry = "Insert Into SaleInvoiceTransport (DocID, Transporter, LRNo, LRDate, PrivateMark, Weight, Freight, PaymentType, RoadPermitNo, RoadPermitDate)
                Values ('" & SearchCode & "', 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTransporter).Tag) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLrNo).Value) & ",
                " & AgL.Chk_Date(Dgl1.Item(Col1Value, rowLrDate).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPrivateMark).Value) & ",
                " & Val(Dgl1.Item(Col1Value, rowWeight).Value) & ",
                " & Val(Dgl1.Item(Col1Value, rowFreight).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLrPaymentType).Value) & ",
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRoadPermitNo).Value) & ",
                " & AgL.Chk_Date(Dgl1.Item(Col1Value, rowRoadPermitDate).Value) & "
                )
               "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


    End Sub

End Class