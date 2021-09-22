Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPurchaseInvoicePayment
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"

    Public Const rowBankCashAc As Integer = 0
    Public Const rowOpeningBalance As Integer = 1
    Public Const rowInvoiceValue As Integer = 2
    Public Const rowToPayAmount As Integer = 3
    Public Const rowPaidAmount As Integer = 4
    Public Const rowChqNo As Integer = 5
    Public Const rowChqDate As Integer = 6

    Public Const rowTempAmountForTdsCalculation As Integer = 7
    Public Const rowTdsCategory As Integer = 8
    Public Const rowTdsGroup As Integer = 9
    Public Const rowTdsLedgerAccount As Integer = 10
    Public Const rowTdsMonthlyLimit As Integer = 11
    Public Const rowTdsYearlyLimit As Integer = 12
    Public Const rowPartyMonthTransaction As Integer = 13
    Public Const rowPartyYearTransaction As Integer = 14
    Public Const rowTdsTaxableAmount As Integer = 15
    Public Const rowTdsPer As Integer = 16
    Public Const rowTdsAmount As Integer = 17

    Public Const HcBankCashAc As String = "Bank Cash Ac"
    Public Const HcOpeningBalance As String = "Opening Balance"
    Public Const HcInvoiceValue As String = "Invoice Value"
    Public Const HcToPayAmount As String = "To Pay Amount"
    Public Const HcPaidAmount As String = "Paid Amount"
    Public Const HcChqNo As String = "Chq No"
    Public Const HcChqDate As String = "Chq Date"

    Public Const HcTempAmountForTdsCalculation As String = "Temp Amount For Tds Calculation"
    Public Const HcTdsCategory As String = "Tds Category"
    Public Const HcTdsGroup As String = "Tds Group"
    Public Const HcTdsLedgerAccount As String = "Tds Ledger Account"
    Public Const HcTdsMonthlyLimit As String = "Tds Monthly Limit"
    Public Const HcTdsYearlyLimit As String = "Tds Yearly Limit"
    Public Const HcPartyMonthTransaction As String = "Party Month Transaction"
    Public Const HcPartyYearTransaction As String = "Party Year Transaction"
    Public Const HcTdsTaxableAmount As String = "Tds Taxable Amount"
    Public Const HcTdsPer As String = "Tds Per"
    Public Const HcTdsAmount As String = "Tds Amount"

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
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
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

        Dgl1.Rows.Add(18)
        Dgl1.Item(Col1Head, rowBankCashAc).Value = HcBankCashAc
        Dgl1.Item(Col1Head, rowOpeningBalance).Value = HcOpeningBalance
        Dgl1.Item(Col1Head, rowInvoiceValue).Value = HcInvoiceValue
        Dgl1.Item(Col1Head, rowToPayAmount).Value = HcToPayAmount
        Dgl1.Item(Col1Head, rowPaidAmount).Value = HcPaidAmount
        Dgl1.Item(Col1Head, rowChqNo).Value = HcChqNo
        Dgl1.Item(Col1Head, rowChqDate).Value = HcChqDate


        Dgl1.Item(Col1Head, rowTempAmountForTdsCalculation).Value = HcTempAmountForTdsCalculation
        Dgl1.Item(Col1Head, rowTdsCategory).Value = HcTdsCategory
        Dgl1.Item(Col1Head, rowTdsGroup).Value = HcTdsGroup
        Dgl1.Item(Col1Head, rowTdsLedgerAccount).Value = HcTdsLedgerAccount
        Dgl1.Item(Col1Head, rowTdsMonthlyLimit).Value = HcTdsMonthlyLimit
        Dgl1.Item(Col1Head, rowTdsYearlyLimit).Value = HcTdsYearlyLimit
        Dgl1.Item(Col1Head, rowPartyMonthTransaction).Value = HcPartyMonthTransaction
        Dgl1.Item(Col1Head, rowPartyYearTransaction).Value = HcPartyYearTransaction
        Dgl1.Item(Col1Head, rowTdsTaxableAmount).Value = HcTdsTaxableAmount
        Dgl1.Item(Col1Head, rowTdsPer).Value = HcTdsPer
        Dgl1.Item(Col1Head, rowTdsAmount).Value = HcTdsAmount





        Dim bEntryNCat As String = AgL.Dman_Execute("Select NCat From Voucher_Type Where V_Type = '" & mV_Type & "'", AgL.GCn).ExecuteScalar()
        ApplyUISettings(bEntryNCat)

        FMoveRec(SearchCode)
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            If AgL.StrCmp(EntryMode, "Browse") Then
                Me.Close()
            End If
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
                Case rowChqNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 50
                Case rowChqDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                Case rowOpeningBalance, rowInvoiceValue, rowToPayAmount
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 8
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                    Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
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
                Case rowBankCashAc
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Name From viewHelpSubgroup H  With (NoLock) 
                                Where H.SubgroupType='" & SubgroupType.LedgerAccount & "'
                                And H.Nature In ('Cash','Bank') "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowChqDate
                    If e.KeyCode = Keys.Enter Then
                        BtnOk.Focus()
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                If Val(Dgl1.Item(Col1Value, rowPaidAmount).Value) > 0 And
                    Dgl1.Item(Col1Value, rowBankCashAc).Value = "" Then
                    MsgBox("Bank/Cash is mandatory.", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1Value, rowBankCashAc)
                    Exit Sub
                End If
                mOkButtonPressed = True
                Me.Close()
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


        mSearchcode = SearchCode

        Try
            mQry = "Select H.BankCashAc, Sg.Name As BankCashAcName, 
                    H.OpeningBalance, Pi.Net_Amount As InvoiceValue, H.ToPayAmount, 
                    H.PaidAmount, H.ChqNo, H.ChqDate,
                    Tg.Description AS TdsGroupDesc, Tc.Description AS TdsCategoryDesc, 
                    TSg.Name As TdsLedgerAccountName, PiTds.*
                    From PurchInvoicePayment H  With (NoLock)
                    LEFT JOIN SubGroup Sg On H.BankCashAc = Sg.SubCode
                    LEFT JOIN PurchInvoice Pi With (NoLock) On H.DocId = Pi.DocId
                    LEFT JOIN PurchInvoicePaymentTds PiTds  With (NoLock) on H.DocID = PiTds.DocID
                    LEFT JOIN TdsGroup Tg ON PiTds.TdsGroup = Tg.Code
                    LEFT JOIN TdsCategory Tc ON PiTds.TdsCategory = Tc.Code
                    LEFT JOIN SubGroup TSg On PiTds.TdsLedgerAccount = TSg.SubCode
                    Where H.DocId = '" & mSearchcode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1Value, rowBankCashAc).Tag = AgL.XNull(DtTemp.Rows(0)("BankCashAc"))
                    Dgl1.Item(Col1Value, rowBankCashAc).Value = AgL.XNull(.Rows(0)("BankCashAcName"))
                    Dgl1.Item(Col1Value, rowOpeningBalance).Value = AgL.XNull(.Rows(0)("OpeningBalance"))
                    Dgl1.Item(Col1Value, rowInvoiceValue).Value = AgL.VNull(.Rows(0)("InvoiceValue"))
                    Dgl1.Item(Col1Value, rowToPayAmount).Value = AgL.VNull(.Rows(0)("ToPayAmount"))
                    Dgl1.Item(Col1Value, rowPaidAmount).Value = AgL.VNull(.Rows(0)("PaidAmount"))
                    Dgl1.Item(Col1Value, rowChqNo).Value = AgL.XNull(.Rows(0)("ChqNo"))
                    Dgl1.Item(Col1Value, rowChqDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("ChqDate")))

                    Dgl1.Item(Col1Value, rowTdsCategory).Tag = AgL.XNull(.Rows(I)("TdsCategory"))
                    Dgl1.Item(Col1Value, rowTdsCategory).Value = AgL.XNull(.Rows(I)("TdsCategoryDesc"))
                    Dgl1.Item(Col1Value, rowTdsGroup).Tag = AgL.XNull(.Rows(I)("TdsGroup"))
                    Dgl1.Item(Col1Value, rowTdsGroup).Value = AgL.XNull(.Rows(I)("TdsGroupDesc"))
                    Dgl1.Item(Col1Value, rowTdsLedgerAccount).Tag = AgL.XNull(.Rows(I)("TdsLedgerAccount"))
                    Dgl1.Item(Col1Value, rowTdsLedgerAccount).Value = AgL.XNull(.Rows(I)("TdsLedgerAccountName"))
                    Dgl1.Item(Col1Value, rowTdsMonthlyLimit).Value = AgL.VNull(.Rows(I)("TdsMonthlyLimit"))
                    Dgl1.Item(Col1Value, rowTdsYearlyLimit).Value = AgL.VNull(.Rows(I)("TdsYearlyLimit"))
                    Dgl1.Item(Col1Value, rowTdsPer).Value = AgL.VNull(.Rows(I)("TdsPer"))
                    Dgl1.Item(Col1Value, rowPartyMonthTransaction).Value = AgL.VNull(.Rows(I)("PartyMonthTransaction"))
                    Dgl1.Item(Col1Value, rowPartyYearTransaction).Value = AgL.VNull(.Rows(I)("PartyYearTransaction"))
                    Dgl1.Item(Col1Value, rowTdsAmount).Value = AgL.VNull(.Rows(I)("TdsAmount"))
                    Dgl1.Item(Col1Value, rowTempAmountForTdsCalculation).Value = Val(Dgl1.Item(Col1Value, rowPaidAmount).Value) + Val(Dgl1.Item(Col1Value, rowTdsAmount).Value)
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FSave(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) 
                    From PurchInvoicePayment With (NoLock) 
                    Where DocId = '" & SearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
            mQry = " Insert Into PurchInvoicePayment(DocId, BankCashAc, OpeningBalance, ToPayAmount, PaidAmount, ChqNo, ChqDate)
                    Values('" & mSearchcode & "', " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBankCashAc).Tag) & ",
                    " & Val(Dgl1.Item(Col1Value, rowOpeningBalance).Value) & ",
                    " & Val(Dgl1.Item(Col1Value, rowToPayAmount).Value) & ",
                    " & Val(Dgl1.Item(Col1Value, rowPaidAmount).Value) & ",
                    " & Val(Dgl1.Item(Col1Value, rowChqNo).Value) & ",
                    " & AgL.Chk_Date(Dgl1.Item(Col1Value, rowChqDate).Value) & ")"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Else
            mQry = " Update PurchInvoicePayment Set 
                BankCashAc = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBankCashAc).Tag) & ",
                OpeningBalance = " & Val(Dgl1.Item(Col1Value, rowOpeningBalance).Value) & ",
                ToPayAmount = " & Val(Dgl1.Item(Col1Value, rowToPayAmount).Value) & ",
                PaidAmount = " & Val(Dgl1.Item(Col1Value, rowPaidAmount).Value) & ",
                ChqNo = " & Val(Dgl1.Item(Col1Value, rowChqNo).Value) & ",
                ChqDate = " & AgL.Chk_Date(Dgl1.Item(Col1Value, rowChqDate).Value) & "
                Where DocId = '" & mSearchcode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If Val(Dgl1.Item(Col1Value, rowTdsAmount).Value) > 0 Then
            mQry = "INSERT INTO PurchInvoicePaymentTds (DocID, TdsCategory, TdsGroup, TdsLedgerAccount, 
                    TdsMonthlyLimit, TdsYearlyLimit, PartyMonthTransaction, PartyYearTransaction, 
                    TdsTaxableAmount, TdsPer, TdsAmount)
                    VALUES ('" & mSearchcode & "', 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTdsCategory).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTdsGroup).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTdsLedgerAccount).Tag) & ", 
                    " & Val(Dgl1.Item(Col1Value, rowTdsMonthlyLimit).Value) & ", 
                    " & Val(Dgl1.Item(Col1Value, rowTdsYearlyLimit).Value) & ", 
                    " & Val(Dgl1.Item(Col1Value, rowPartyMonthTransaction).Value) & ", 
                    " & Val(Dgl1.Item(Col1Value, rowPartyYearTransaction).Value) & ", 
                    " & Val(Dgl1.Item(Col1Value, rowTdsTaxableAmount).Value) & ", 
                    " & Val(Dgl1.Item(Col1Value, rowTdsPer).Value) & ", 
                    " & Val(Dgl1.Item(Col1Value, rowTdsAmount).Value) & ")"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Else
            mQry = "UPDATE PurchInvoicePaymentTds
                    SET TdsCategory = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTdsCategory).Tag) & ",
	                TdsGroup = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTdsGroup).Tag) & ",
	                TdsLedgerAccount = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTdsLedgerAccount).Tag) & ",
	                TdsMonthlyLimit = " & Val(Dgl1.Item(Col1Value, rowTdsMonthlyLimit).Value) & ",
	                TdsYearlyLimit = " & Val(Dgl1.Item(Col1Value, rowTdsYearlyLimit).Value) & ",
	                PartyMonthTransaction = " & Val(Dgl1.Item(Col1Value, rowPartyMonthTransaction).Value) & ",
	                PartyYearTransaction = " & Val(Dgl1.Item(Col1Value, rowPartyYearTransaction).Value) & ",
	                TdsTaxableAmount = " & Val(Dgl1.Item(Col1Value, rowTdsTaxableAmount).Value) & ",
	                TdsPer = " & Val(Dgl1.Item(Col1Value, rowTdsPer).Value) & ",
	                TdsAmount = " & Val(Dgl1.Item(Col1Value, rowTdsAmount).Value) & "
                    Where DocId = '" & mSearchcode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
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
End Class