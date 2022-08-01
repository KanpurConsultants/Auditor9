Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPurchaseInvoiceHeader
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"

    Public Const rowFill As Integer = 0
    Public Const rowTransporter As Integer = 1
    Public Const rowLrNo As Integer = 2
    Public Const rowLrDate As Integer = 3
    Public Const rowNoOfBales As Integer = 4
    Public Const rowPrivateMark As Integer = 5
    Public Const rowWeight As Integer = 6
    Public Const rowFreight As Integer = 7
    Public Const rowLrPaymentType As Integer = 8
    Public Const rowRoadPermitNo As Integer = 9
    Public Const rowRoadPermitDate As Integer = 10

    Public Const HcFill As String = "Fill"
    Public Const HcTransporter As String = "Transporter"
    Public Const HcLrNo As String = "Lr No."
    Public Const HcLrDate As String = "Lr Date"
    Public Const hcNoOfBales As String = "No. Of Bales"
    Public Const HcPrivateMark As String = "Private Mark"
    Public Const HcWeight As String = "Weight"
    Public Const HcFreight As String = "Freight"
    Public Const HcLrPaymentType As String = "Lr Payment Type"
    Public Const HcRoadPermitNo As String = "EWay Bill No."
    Public Const HcRoadPermitDate As String = "EWay Bill Date"

    Dim mSearchcode As String
    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mToQtyDecimalPlace As Integer
    Dim mPartyCode As String
    Dim mV_Type As String = ""
    Dim mDgl1LastRowIndex As Integer
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
    Public Property PartyCode() As String
        Get
            PartyCode = mPartyCode
        End Get
        Set(ByVal value As String)
            mPartyCode = value
        End Set
    End Property
    Public Property V_Type() As String
        Get
            V_Type = mV_Type
        End Get
        Set(ByVal value As String)
            mV_Type = value
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
        Dgl1.Name = "Dgl1"
        Dgl1.Tag = "VerticalGrid"

        Dgl1.Rows.Add(11)
        Dgl1.Item(Col1Head, rowFill).Value = HcFill
        Dgl1.Item(Col1Head, rowTransporter).Value = HcTransporter
        Dgl1.Item(Col1Head, rowLrNo).Value = HcLrNo
        Dgl1.Item(Col1Head, rowLrDate).Value = HcLrDate
        Dgl1.Item(Col1Head, rowNoOfBales).Value = hcNoOfBales
        Dgl1.Item(Col1Head, rowPrivateMark).Value = HcPrivateMark
        Dgl1.Item(Col1Head, rowWeight).Value = HcWeight
        Dgl1.Item(Col1Head, rowFreight).Value = HcFreight
        Dgl1.Item(Col1Head, rowLrPaymentType).Value = HcLrPaymentType
        Dgl1.Item(Col1Head, rowRoadPermitNo).Value = HcRoadPermitNo
        Dgl1.Item(Col1Head, rowRoadPermitDate).Value = HcRoadPermitDate


        Dgl1.Item(Col1Value, rowFill) = New DataGridViewButtonCell

        Dim bEntryNCat As String = AgL.Dman_Execute("Select NCat From Voucher_Type Where V_Type = '" & mV_Type & "'", AgL.GCn).ExecuteScalar()
        ApplyUISettings(bEntryNCat)

        If AgL.StrCmp(AgL.XNull(AgL.PubDtEnviro.Rows(0)("LrGenerationPattern")), LrGenerationPattern.FromLrEntry) Then
            Dgl1.Rows(rowFill).Visible = True
        Else
            Dgl1.Rows(rowFill).Visible = False
        End If

        If AgL.StrCmp(EntryMode, "Browse") Then
            Dgl1.Rows(rowFill).Visible = False
        ElseIf AgL.StrCmp(AgL.XNull(AgL.PubDtEnviro.Rows(0)("LrGenerationPattern")), LrGenerationPattern.FromLrEntry) Then
            Dgl1.Rows(rowFill).Visible = True
        End If


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
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 50
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

            If e.KeyCode = Keys.Enter Then Exit Sub
            'If mEntryMode = "Browse" Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Select Case Dgl1.CurrentCell.RowIndex
                Case rowTransporter
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Name From viewHelpSubgroup H  With (NoLock) Where H.SubgroupType='" & SubgroupType.Transporter & "' "
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
                'If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                'mOkButtonPressed = True
                'Me.Close()

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

    Public Sub FMoveRec(ByVal SearchCode As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        If SearchCode = "" Then
            mQry = "Select H.Transporter, Transporter.Name as TransporterName 
                    From SubgroupSiteDivisionDetail H  With (NoLock)
                    Left Join viewHelpSubgroup Transporter  With (NoLock) On H.Transporter = Transporter.Code
                    Where H.Subcode='" & mPartyCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1Value, rowTransporter).Tag = AgL.XNull(DtTemp.Rows(0)("Transporter"))
                Dgl1.Item(Col1Value, rowTransporter).Value = AgL.XNull(DtTemp.Rows(0)("TransporterName"))
            End If
            Exit Sub
        End If
        mSearchcode = SearchCode

        Try
            'BtnHeaderDetail.Tag = FunRetNewUnitConversionObject()
            'BtnHeaderDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
            mQry = "SELECT H.*, Transporter.Name as TransporterName
                    FROM PurchInvoiceTransport H  With (NoLock)                     
                    LEFT JOIN viewHelpSubgroup Transporter  With (NoLock) On H.Transporter = Transporter.Code 
                    WHERE H.DocId = '" & SearchCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                'BtnHeaderDetail.Tag.Dgl1.RowCount = 1 : BtnHeaderDetail.Tag.Dgl1.Rows.Clear()
                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1Value, rowTransporter).Tag = AgL.XNull(DtTemp.Rows(0)("Transporter"))
                    Dgl1.Item(Col1Value, rowTransporter).Value = AgL.XNull(.Rows(0)("TransporterName"))
                    Dgl1.Item(Col1Value, rowLrNo).Value = AgL.XNull(.Rows(0)("LRNo"))
                    Dgl1.Item(Col1Value, rowLrDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("LRDate")))
                    Dgl1.Item(Col1Value, rowNoOfBales).Value = AgL.XNull(.Rows(0)("NoOfBales"))
                    Dgl1.Item(Col1Value, rowPrivateMark).Value = AgL.XNull(.Rows(0)("PrivateMark"))
                    Dgl1.Item(Col1Value, rowWeight).Value = AgL.XNull(.Rows(0)("Weight"))
                    Dgl1.Item(Col1Value, rowFreight).Value = AgL.XNull(.Rows(0)("Freight"))
                    Dgl1.Item(Col1Value, rowLrPaymentType).Value = AgL.XNull(.Rows(0)("PaymentType"))
                    Dgl1.Item(Col1Value, rowRoadPermitNo).Value = AgL.XNull(.Rows(0)("RoadPermitNo"))
                    Dgl1.Item(Col1Value, rowRoadPermitDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("RoadPermitDate")))
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub FSave(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim dtTemp As DataTable
        Dim IsDuplicateLrNo As Integer = 0

        If (Dgl1.Item(Col1Value, rowTransporter).Tag <> Nothing) And (Dgl1.Item(Col1Value, rowLrNo).Value <> Nothing) Then
            If AgL.StrCmp(AgL.PubDBName, "Sadhvi") Then

            Else
                mQry = "Select count(*) From PurchInvoiceTransport Where Transporter ='" & Dgl1.Item(Col1Value, rowTransporter).Tag & "' AND LrNo = '" & Dgl1.Item(Col1Value, rowLrNo).Value & "' And DocID <>'" & SearchCode & "'"
                IsDuplicateLrNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar

                If IsDuplicateLrNo > 0 Then
                    Exit Sub
                End If
            End If
        End If

        mQry = "Delete From PurchInvoiceTransport Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Dgl1.Item(Col1Value, rowTransporter).Tag <> "" Or
           Dgl1.Item(Col1Value, rowLrNo).Value <> "" Or
           Dgl1.Item(Col1Value, rowLrDate).Value <> "" Or
           Val(Dgl1.Item(Col1Value, rowNoOfBales).Value) > 0 Or
           Dgl1.Item(Col1Value, rowPrivateMark).Value <> "" Or
           Val(Dgl1.Item(Col1Value, rowWeight).Value) > 0 Or
           Val(Dgl1.Item(Col1Value, rowFreight).Value) > 0 Or
           Dgl1.Item(Col1Value, rowLrPaymentType).Value <> "" Or
           Dgl1.Item(Col1Value, rowRoadPermitNo).Value <> "" Or
           Dgl1.Item(Col1Value, rowRoadPermitDate).Value <> "" Then


            mQry = "Insert Into PurchInvoiceTransport (DocID, Transporter, LRNo, LRDate, NoOfBales, PrivateMark, Weight, Freight, PaymentType, RoadPermitNo, RoadPermitDate)
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
                " & AgL.Chk_Date(Dgl1.Item(Col1Value, rowRoadPermitDate).Value) & "
                )
               "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "Update PurchInvoice Set UploadDate=Null Where DocId ='" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            If ClsMain.IsScopeOfWorkContains("+Cloth Aadhat Module") Then
                Dim mFieldCount As Integer = 0
                mQry = "Select GenDocID From PurchInvoice With (NoLock) Where DocId = '" & SearchCode & "' And GenDocID Is Not Null "
                dtTemp = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
                If dtTemp.Rows.Count > 0 Then
                    mQry = "Update SaleInvoiceTransport Set "
                    If AgL.XNull(Dgl1.Item(Col1Value, rowTransporter).Value) <> "" Then
                        If mFieldCount > 0 Then mQry += ", "
                        mQry += " Transporter = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTransporter).Tag)
                        mFieldCount += 1
                    End If
                    If AgL.XNull(Dgl1.Item(Col1Value, rowLrNo).Value) <> "" Then
                        If mFieldCount > 0 Then mQry += ", "
                        mQry += " LrNo = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLrNo).Value) & ","
                        mQry += " LrDate = " & AgL.Chk_Date(Dgl1.Item(Col1Value, rowLrDate).Value)
                        mFieldCount += 1
                    End If
                    If AgL.XNull(Dgl1.Item(Col1Value, rowPrivateMark).Value) <> "" Then
                        If mFieldCount > 0 Then mQry += ", "
                        mQry += " PrivateMark =" & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPrivateMark).Value)
                        mFieldCount += 1
                    End If
                    If Val(Dgl1.Item(Col1Value, rowWeight).Value) > 0 Then
                        If mFieldCount > 0 Then mQry += ", "
                        mQry += " Weight = " & Val(Dgl1.Item(Col1Value, rowWeight).Value)
                        mFieldCount += 1
                    End If
                    If Val(Dgl1.Item(Col1Value, rowFreight).Value) > 0 Then
                        If mFieldCount > 0 Then mQry += ", "
                        mQry += " Freight = " & Val(Dgl1.Item(Col1Value, rowFreight).Value)
                        mFieldCount += 1
                    End If
                    If AgL.XNull(Dgl1.Item(Col1Value, rowLrPaymentType).Value) <> "" Then
                        If mFieldCount > 0 Then mQry += ", "
                        mQry += " PaymentType = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLrPaymentType).Value)
                        mFieldCount += 1
                    End If
                    mQry += " Where DocID = '" & dtTemp.Rows(0)("GenDocId") & "'"
                    If mFieldCount > 0 Then
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        mQry = "Update SaleInvoice Set UploadDate=Null Where DocId ='" & SearchCode & "'"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Dgl1.CurrentCell.RowIndex = mDgl1LastRowIndex Then
                BtnOk.Focus()
            End If
        End If
    End Sub
    Private Sub ApplyUISettings(NCAT As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer
        Try
            For I = 0 To Dgl1.Rows.Count - 1
                Dgl1.Rows(I).Visible = False
            Next
            Dgl1.Visible = False

            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName= '" & Me.Name & "'  And NCat = '" & NCAT & "' And GridName ='" & Dgl1.Name & "' "
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
            If mDgl1RowCount > 0 Then
                Dgl1.Visible = True
            End If


            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Rows(I).Visible = True Then
                    mDgl1LastRowIndex = I
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        If e.ColumnIndex = Dgl1.Columns(Col1Value).Index And TypeOf (Dgl1(Col1Value, e.RowIndex)) Is DataGridViewButtonCell Then
            Select Case e.RowIndex
                Case rowFill
                    FillLrEntries()
            End Select
        End If
    End Sub

    Private Sub FillLrEntries()
        'mQry = " SELECT Lr.Code, Lr.LRNo, Lr.LRDate, Lr.Weight, Lr.PrivateMark, Sg.Name AS Transporter
        '        FROM Lr Lr 
        '        LEFT JOIN StockHead H ON Lr.GenDocID = H.DocID
        '        LEFT JOIN Subgroup SG ON H.Transporter = Sg.Subcode
        '        LEFT JOIN PurchInvoice Pi ON Pi.LrCode = Lr.Code
        '        WHERE Pi.DocID IS NULL
        '        And H.SubCode = '" & mPartyCode & "'"

        mQry = "SELECT VLr.Code, Max(Lr.LRNo) AS LrNo, Max(Lr.LRDate) AS LrDate, Max(Lr.Weight) AS Weight, 
                Max(Lr.PrivateMark) AS PrivateMark, Max(Sg.Name) AS Transporter
                FROM (
	                SELECT Lr.Code, L.LotNo
	                FROM Lr Lr 
	                LEFT JOIN StockHeadDetail L ON Lr.GenDocID = L.DocID
	                GROUP BY Lr.Code, L.LotNo
                ) AS VLr 
                LEFT JOIN Lr Lr ON VLr.Code = Lr.Code
                LEFT JOIN StockHead H ON Lr.GenDocID = H.DocID
                LEFT JOIN Subgroup SG ON H.Transporter = Sg.Subcode
                WHERE H.SubCode = '" & mPartyCode & "'
                GROUP BY VLr.Code
                HAVING Count(*) > (SELECT Count(DocId) FROM PurchInvoice WHERE LrCode = VLr.Code) "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(DtTemp), "", 350, 700, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Lr No", 80, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(2, "Lr Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(3, "Weight", 80, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(4, "Private Mark", 120, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.FFormatColumn(5, "Transporter", 220, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            bCode = FRH_Single.DRReturn("Code")
        End If

        mQry = " SELECT Lr.Code, Lr.LRNo, Lr.LRDate, Lr.Weight, Lr.PrivateMark, Lr.Transporter, Sg.Name AS TransporterName
                    FROM Lr Lr 
                    LEFT JOIN StockHead H ON Lr.GenDocID = H.DocID
                    LEFT JOIN Subgroup SG ON H.Transporter = Sg.Subcode
                    WHERE Lr.Code = '" & bCode & "'"
        Dim DtLrDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtLrDetail.Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowFill).Tag = AgL.XNull(DtLrDetail.Rows(0)("Code"))
            Dgl1.Item(Col1Value, rowTransporter).Tag = AgL.XNull(DtLrDetail.Rows(0)("Transporter"))
            Dgl1.Item(Col1Value, rowTransporter).Value = AgL.XNull(DtLrDetail.Rows(0)("TransporterName"))
            Dgl1.Item(Col1Value, rowLrNo).Tag = AgL.XNull(DtLrDetail.Rows(0)("Code"))
            Dgl1.Item(Col1Value, rowLrNo).Value = AgL.XNull(DtLrDetail.Rows(0)("LRNo"))
            Dgl1.Item(Col1Value, rowLrDate).Value = AgL.RetDate(AgL.XNull(DtLrDetail.Rows(0)("LRDate")))
            Dgl1.Item(Col1Value, rowPrivateMark).Value = AgL.XNull(DtLrDetail.Rows(0)("PrivateMark"))
            Dgl1.Item(Col1Value, rowWeight).Value = AgL.XNull(DtLrDetail.Rows(0)("Weight"))


            Dgl1.Item(Col1Value, rowTransporter).ReadOnly = True
            Dgl1.Item(Col1Value, rowLrNo).ReadOnly = True
            Dgl1.Item(Col1Value, rowLrDate).ReadOnly = True
            Dgl1.Item(Col1Value, rowPrivateMark).ReadOnly = True
            Dgl1.Item(Col1Value, rowWeight).ReadOnly = True
        End If
    End Sub
End Class