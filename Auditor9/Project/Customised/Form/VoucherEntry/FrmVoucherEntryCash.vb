Imports System.ComponentModel
Imports System.Data.SQLite
Imports Customised.ClsMain

Public Class FrmVoucherEntryCash

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = "", mEntryNCat$ = "", mV_Date$ = "", mPartyAccount$ = ""
    Dim mEffective_Date As String
    Dim mNarration As String
    Dim mHeaderAccount As String
    Dim mHeaderAccountName As String
    Dim mPartyName As String
    Dim mDivisionCode As String
    Dim mSiteCode As String
    Dim mVoucherCategory As String
    Public IsDeleteAllButtonPressed As Boolean = False

    Public DtV_TypeSettings As DataTable
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1EntryDate As String = "Entry Date"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remark As String = "Remark"
    Protected Const Col1ReferenceDocId As String = "Reference DocId"
    Protected Const Col1ReferenceDocIdSr As String = "Reference DocId Sr"


    Dim mQry As String = ""
    Dim mTransactionType As String = ""
    Dim mTotalAmount As Double



    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Property EntryNCat() As String
        Get
            EntryNCat = mEntryNCat
        End Get
        Set(ByVal value As String)
            mEntryNCat = value
        End Set
    End Property
    Public Property V_Date() As String
        Get
            V_Date = mV_Date
        End Get
        Set(ByVal value As String)
            mV_Date = value
        End Set
    End Property

    Public Property Effective_Date() As String
        Get
            Effective_Date = mEffective_Date
        End Get
        Set(ByVal value As String)
            mEffective_Date = value
        End Set
    End Property


    Public Property HeaderAccount() As String
        Get
            HeaderAccount = mHeaderAccount
        End Get
        Set(ByVal value As String)
            mHeaderAccount = value
        End Set
    End Property

    Public Property HeaderAccountName() As String
        Get
            HeaderAccountName = mHeaderAccountName
        End Get
        Set(ByVal value As String)
            mHeaderAccountName = value
        End Set
    End Property


    Public Property PartyAccount() As String
        Get
            PartyAccount = mPartyAccount
        End Get
        Set(ByVal value As String)
            mPartyAccount = value
        End Set
    End Property

    Public Property PartyName() As String
        Get
            PartyName = mPartyName
        End Get
        Set(ByVal value As String)
            mPartyName = value
        End Set
    End Property

    Public Property TotalAmount() As Double
        Get
            TotalAmount = mTotalAmount
        End Get
        Set(ByVal value As Double)
            mTotalAmount = value
        End Set
    End Property

    Public Property Narration() As String
        Get
            Narration = mNarration
        End Get
        Set(ByVal value As String)
            mNarration = value
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

    Public Property VoucherCategory() As String
        Get
            VoucherCategory = mVoucherCategory
        End Get
        Set(ByVal value As String)
            mVoucherCategory = value
        End Set
    End Property


    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgDateColumn(Dgl1, Col1EntryDate, 100, Col1EntryDate, True, False)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 355, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocId, 40, 5, Col1ReferenceDocId, False, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocIdSr, 40, 5, Col1ReferenceDocIdSr, False, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)

        'Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True


        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub


    Public Sub MoveRec(Searchcode As String, SearchcodeSr As Integer)
        Dim mQry As String = ""
        Dim DsTemp As DataSet
        Dim I As Integer = 0
        Dim mRemainningAmt As Double
        Dim mNextDate As Date
        Dim WeekOffDays() As String
        Dim DtHolidays As DataTable
        Dim J As Integer = 0
        mRemainningAmt = mTotalAmount
        mNextDate = V_Date

        mQry = "Select V_Date From HRM_Holiday Where V_Date > " & AgL.Chk_Date(V_Date) & ""
        DtHolidays = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If SearchcodeSr = 0 Then
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If AgL.XNull(DtV_TypeSettings.Rows(0)("ActionIfMaximumCashTransactionLimitExceeds")) = ActionsOfMaximumCashTransactionLimitExceeds.SplitOnFutureDates Or AgL.XNull(DtV_TypeSettings.Rows(0)("ActionIfMaximumCashTransactionLimitExceeds")) = ActionsOfMaximumCashTransactionLimitExceeds.SplitOnPastDates Then
                While (mRemainningAmt > 0)
                    WeekOffDays = Split(AgL.PubDtDivisionSiteSetting.Rows(0)("WeekOffDays"), ",")

                    For I = 0 To WeekOffDays.Length - 1
                        If mNextDate.DayOfWeek = WeekOffDays(I) Then
                            If AgL.XNull(DtV_TypeSettings.Rows(0)("ActionIfMaximumCashTransactionLimitExceeds")) = ActionsOfMaximumCashTransactionLimitExceeds.SplitOnFutureDates Then
                                mNextDate = DateAdd(DateInterval.Day, 1, mNextDate)
                            Else
                                mNextDate = DateAdd(DateInterval.Day, -1, mNextDate)
                            End If
                        End If
                    Next

                    For I = 0 To DtHolidays.Rows.Count - 1
                        If mNextDate = DtHolidays.Rows(I)("V_Date") Then
                            If AgL.XNull(DtV_TypeSettings.Rows(0)("ActionIfMaximumCashTransactionLimitExceeds")) = ActionsOfMaximumCashTransactionLimitExceeds.SplitOnFutureDates Then
                                mNextDate = DateAdd(DateInterval.Day, 1, mNextDate)
                            Else
                                mNextDate = DateAdd(DateInterval.Day, -1, mNextDate)
                            End If
                        End If
                    Next



                    Dgl1.Rows.Add()

                    Dgl1.Item(ColSNo, J).Value = Dgl1.Rows.Count - 1


                    Dgl1.Item(Col1EntryDate, J).Value = ClsMain.FormatDate(AgL.XNull(mNextDate))
                    Dgl1.Item(Col1Amount, J).Value = IIf(AgL.VNull(AgL.PubDtEnviro.Rows(0)("MaximumCashTransactionLimit")) < mRemainningAmt, AgL.VNull(AgL.PubDtEnviro.Rows(0)("MaximumCashTransactionLimit")), mRemainningAmt)
                    Dgl1.Item(Col1Amount, J).Value = Format(Dgl1.Item(Col1Amount, J).Value, "0.00")
                    'Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                    J += 1
                    If AgL.XNull(DtV_TypeSettings.Rows(0)("ActionIfMaximumCashTransactionLimitExceeds")) = ActionsOfMaximumCashTransactionLimitExceeds.SplitOnFutureDates Then
                        mNextDate = DateAdd(DateInterval.Day, 1, mNextDate)
                    Else
                        mNextDate = DateAdd(DateInterval.Day, -1, mNextDate)
                    End If
                    mRemainningAmt = mRemainningAmt - IIf(AgL.VNull(AgL.PubDtEnviro.Rows(0)("MaximumCashTransactionLimit")) < mRemainningAmt, AgL.VNull(AgL.PubDtEnviro.Rows(0)("MaximumCashTransactionLimit")), mRemainningAmt)

                End While
            End If
        Else

            mQry = " Select  LH.V_Date,  L.*  
                From (Select * From LedgerHeadDetail  Where DocID='" & Searchcode & "' And Sr = " & SearchcodeSr & ") H 
                Left Join LedgerHeadDetail L On L.ReferenceDocID = H.DocID And L.ReferenceDocIDSr = H.Sr
                Left Join LedgerHead LH On L.DocID = LH.DocID
                Order By L.DocID"
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))

                        Dgl1.Item(Col1EntryDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("V_Date")))
                        Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(.Rows(I)("DocId"))
                        Dgl1.Item(Col1ReferenceDocIdSr, I).Value = AgL.VNull(.Rows(I)("Sr"))
                        Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")
                        Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))





                        LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                        BtnDelete.Visible = False
                    Next I
                    Dgl1.AllowUserToAddRows = False
                End If
            End With

        End If

        Calculation()

    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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



    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub Calculation()
        Dim I As Integer

        LblTotalAmount.Text = 0


        For I = 0 To Dgl1.RowCount - 1
            If Val(Dgl1.Item(Col1Amount, I).Value) <> 0 Then
                'Footer Calculation
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next

        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub

    Public Sub FSave(ByVal SearchCode As String, ByVal SearchCodeSr As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim mTrans As String = ""
        Dim I As Integer
        Dim mDebitAmt As Double
        Dim mCreditAmt As Double
        Dim mDebitNarration As String = ""
        Dim mCreditNarration As String = ""

        Dim mV_Type As String = "CR"
        mQry = "Delete From TransactionReferences Where DocID = '" & SearchCode & "' And DocIDSr=" & Val(SearchCodeSr) & ""
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Dim mRecId As String
        For I = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1Amount, I).Value) > 0 Then
                If Dgl1.Item(Col1ReferenceDocId, I).Value <> "" Then
                    StrDocID = Dgl1.Item(Col1ReferenceDocId, I).Value
                    mRecId = AgL.Dman_Execute("Select ManualRefNo From LedgerHead With (NoLock) Where DocId = '" & StrDocID & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar
                    mQry = "Delete From Ledger Where DocId = '" & StrDocID & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    mQry = "Delete From LedgerHeadDetail Where DocId = '" & StrDocID & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    mQry = "Delete From LedgerHead Where DocId = '" & StrDocID & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    mQry = "Delete From LedgerM Where DocId = '" & StrDocID & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Else
                    'StrDocID = AgL.GetDocId(mV_Type, CStr(0), CDate(Dgl1.Item(Col1EntryDate, I).Value), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                    StrDocID = AgL.CreateDocId(AgL, "LedgerHead", mV_Type, CStr(0), CDate(Dgl1.Item(Col1EntryDate, I).Value), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                    'mRecId = AgTemplate.ClsMain.FGetManualRefNo("RecId", "LedgerM", mV_Type, Dgl1.Item(Col1EntryDate, I).Value, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)
                    mRecId = FGetManualRefNoForMultiReceipts("RecId", "LedgerM", mV_Type, Dgl1.Item(Col1EntryDate, I).Value, AgL.PubDivCode, AgL.PubSiteCode)
                End If
                Dim mV_No As String = Val(AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                Dim mV_Prefix As String = AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)


                mQry = "Insert Into LedgerM(DocId,V_Type,v_Prefix,Site_Code, Div_Code,V_No,V_Date,SubCode,
                    Narration,PostedBy,RecId,
                    U_Name,U_EntDt,U_AE,PreparedBy) Values 
                    ('" & (StrDocID) & "','" & mV_Type & "','" & mV_Prefix & "','" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', 
                    '" & mV_No & "'," & AgL.Chk_Date(Dgl1.Item(Col1EntryDate, I).Value) & ",Null, 
                    Null,'" & AgL.PubUserName & "','" & mRecId & "',
                    '" & AgL.PubUserName & "'," & AgL.Chk_Date(AgL.PubLoginDate) & ",
                    'A','" & AgL.PubUserName & "')"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                mQry = "INSERT INTO LedgerHead (DocID, V_Type, V_Prefix, V_Date, V_No, 
                                            Div_Code, Site_Code, ManualRefNo, Subcode, PartyName, DrCr,
                                            UptoDate, Remarks, Status, SalesTaxGroupParty, PlaceOfSupply,                                            
                                            EntryBy, EntryDate, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate
                                           )
                       VALUES ('" & StrDocID & "', " & AgL.Chk_Text(mV_Type) & ", " & AgL.Chk_Text(mV_Prefix) & ", " & AgL.Chk_Date(Dgl1.Item(Col1EntryDate, I).Value) & ", " & Val(mV_No) & ",
                           " & AgL.Chk_Text(mDivisionCode) & ", " & AgL.Chk_Text(mSiteCode) & ", " & AgL.Chk_Text(mRecId) & ", " & AgL.Chk_Text(mHeaderAccount) & ", " & AgL.Chk_Text(mHeaderAccountName) & ", 'Dr',
                           Null, Null, Null, Null, Null,                           
                           " & AgL.Chk_Text(AgL.PubUserName) & "," & AgL.Chk_Date(AgL.PubLoginDate) & ", Null, Null, " & AgL.Chk_Text(AgL.PubUserName) & ",
                           " & AgL.Chk_Date(AgL.PubLoginDate) & "
                       );"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                'Effective_Date = ClsMain.FormatDate(AgL.XNull(IIf(Effective_Date = "", V_Date, Effective_Date)))
                Effective_Date = Dgl1.Item(Col1EntryDate, I).Value

                mQry = "Insert Into LedgerHeadDetail (DocID, Sr, Subcode, Amount, Remarks, EffectiveDate,ReferenceDocID, ReferenceDocIdSr)
                            Values ('" & StrDocID & "', " & SearchCodeSr & ",
                            " & AgL.Chk_Text(mPartyAccount) & ",
                            " & Val(Dgl1.Item(Col1Amount, I).Value) & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " & AgL.Chk_Date(Effective_Date) & ", " & AgL.Chk_Text(SearchCode) & ", " & SearchCodeSr & ")"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                If mVoucherCategory = AgLibrary.ClsMain.agConstants.VoucherCategory.Receipt Then
                    mDebitAmt = Dgl1.Item(Col1Amount, I).Value
                    mCreditAmt = 0
                    mDebitNarration = "Payment Received From " & mPartyName & " [" & Dgl1.Item(Col1Remark, I).Value & "]"
                    mCreditNarration = "Cash Received"
                Else
                    mDebitAmt = 0
                    mCreditAmt = Dgl1.Item(Col1Amount, I).Value
                    mDebitNarration = "Cash Paid"
                    mCreditNarration = "Payment Given to " & mPartyName & " [" & Dgl1.Item(Col1Remark, I).Value & "]"
                End If


                mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr,
                          Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,
                          System_Generated, EffectiveDate, ReferenceDocId, ReferenceDocIdSr) Values 
                          ('" & StrDocID & "','" & mRecId & "',1," & AgL.Chk_Date(Dgl1.Item(Col1EntryDate, I).Value) & "," & AgL.Chk_Text(mHeaderAccount) & "," & AgL.Chk_Text(mPartyAccount) & ", 
                          " & mDebitAmt & "," & mCreditAmt & ", 
                          " & AgL.Chk_Text(mDebitNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "',
                          '" & mSiteCode & "','" & mDivisionCode & "','Y', " & AgL.Chk_Date(Effective_Date) & ", " & AgL.Chk_Text(SearchCode) & ", " & SearchCodeSr & ")"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr,
                          Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,
                          System_Generated, EffectiveDate, ReferenceDocId, ReferenceDocIdSr) Values 
                          ('" & StrDocID & "','" & mRecId & "',2," & AgL.Chk_Date(Dgl1.Item(Col1EntryDate, I).Value) & "," & AgL.Chk_Text(mPartyAccount) & "," & AgL.Chk_Text(mHeaderAccount) & ", 
                          " & mCreditAmt & "," & mDebitAmt & ", 
                          " & AgL.Chk_Text(mCreditNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "',
                          '" & mSiteCode & "','" & mDivisionCode & "','Y', " & AgL.Chk_Date(Effective_Date) & ", " & AgL.Chk_Text(SearchCode) & ", " & SearchCodeSr & ")"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                mQry = "INSERT INTO TransactionReferences(DocId, DocIDSr, ReferenceDocId, ReferenceSr, Remark) 
                    Select '" & SearchCode & "', " & Val(SearchCodeSr) & ", '" & StrDocID & "', Null,
                    " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Tag) & ""
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                mQry = "Update voucher_prefix set start_srl_no = " & Val(mV_No) & " 
                    where v_type = " & AgL.Chk_Text(mV_Type) & " and prefix=" & AgL.Chk_Text(mV_Prefix) & "  And Start_Srl_No < " & Val(mV_No) & " "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next
    End Sub
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click, BtnDelete.Click
        Select Case sender.name
            Case BtnOk.Name
                Me.Close()
            Case BtnDelete.Name
                IsDeleteAllButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Public Shared Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String,
                                       ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2
        StrContraName = AgL.XNull(AgL.Dman_Execute("Select Name from Subgroup  Where SubCode = '" & StrContraName & "'  ", AgL.GcnRead).ExecuteScalar)

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                'Case Col1AdditionalChargeAc
                '    If e.KeyCode <> Keys.Enter Then
                '        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                '            mQry = "Select SubCode as Code, Name as Name From SubGroup "
                '            Dgl1.AgHelpDataSet(Col1AdditionalChargeAc) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmVoucherEntryCash_Load(sender As Object, e As EventArgs) Handles Me.Load
        IsDeleteAllButtonPressed = False
    End Sub

    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Calculation()
    End Sub
    Public Shared Function FGetManualRefNoForMultiReceipts(ByVal FieldName As String, ByVal TableName As String, ByVal V_Type As String, ByVal V_Date As String, ByVal Div_Code As String, ByVal Site_Code As String) As String
        Dim mQry$
        Dim mStartSrlNo As Integer = 0
        Dim mStartDate As String, mEndDate As String
        Dim mRef_Prefix$ = ""
        Dim mRef_PadLength As Integer = 0

        mStartDate = AgL.PubStartDate
        mEndDate = AgL.PubEndDate

        If CDate(V_Date) > mEndDate Then
            mStartDate = DateAdd(DateInterval.Year, 1, CDate(AgL.PubStartDate))
            mEndDate = DateAdd(DateInterval.Year, 1, CDate(AgL.PubEndDate))
        End If


        mRef_Prefix = AgL.XNull(AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix With (NoLock) Where V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' And Site_Code = '" & Site_Code & "' And Date_From = " & AgL.Chk_Date(CDate(mStartDate).ToString("s")) & " And Date_To = " & AgL.Chk_Date(CDate(mEndDate).ToString("s")) & "", AgL.GcnRead).ExecuteScalar)
        If mRef_Prefix = "" Then
            If CDate(V_Date) >= CDate("01/Apr/2013") And CDate(V_Date) <= CDate("31/Mar/2014") Then
                mQry = "Select IfNull(Max(Cast(Replace(Replace(" & FieldName & ",'-',''),'.','') as integer)),0)+1 From " & TableName & " With (NoLock)  WHERE  ABS(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "'  And Date(V_Date) Between " & AgL.Chk_Date(CDate(mStartDate).ToString("s")) & " and  " & AgL.Chk_Date(CDate(mEndDate).ToString("s")) & " "
                FGetManualRefNoForMultiReceipts = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                If Val(FGetManualRefNoForMultiReceipts) > 1300000 Then
                    FGetManualRefNoForMultiReceipts = Val(FGetManualRefNoForMultiReceipts) - 1300000
                ElseIf Val(FGetManualRefNoForMultiReceipts) > 130000 Then
                    FGetManualRefNoForMultiReceipts = Val(FGetManualRefNoForMultiReceipts) - 130000
                Else
                    FGetManualRefNoForMultiReceipts = FGetManualRefNoForMultiReceipts
                End If
                FGetManualRefNoForMultiReceipts = "13-" + FGetManualRefNoForMultiReceipts.ToString.PadLeft(4, "0")

            Else
                If AgL.PubServerName = "" Then
                    mQry = "Select IfNull(Max(Cast(Replace(Replace(" & FieldName & ",'-',''),'.','') as integer)),0)+1 From " & TableName & " With (NoLock)  WHERE ABS(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "'  And Date(V_Date) Between " & AgL.Chk_Date(CDate(mStartDate).ToString("s")) & " and  " & AgL.Chk_Date(CDate(mEndDate).ToString("s")) & "    "
                    FGetManualRefNoForMultiReceipts = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar
                Else
                    mQry = "Select IfNull(Max(Cast(Replace(Replace(" & FieldName & ",'-',''),'.','') as integer)),0)+1 From " & TableName & " With (NoLock)  WHERE IsNumeric(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "'  And Date(V_Date) Between " & AgL.Chk_Date(CDate(mStartDate).ToString("s")) & " and  " & AgL.Chk_Date(CDate(mEndDate).ToString("s")) & "    "
                    FGetManualRefNoForMultiReceipts = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                End If
            End If
        Else
            If AgL.PubServerName = "" Then
                mQry = "Select IfNull(Max(Cast(Replace(Replace(Replace(substr(" & FieldName & "," & mRef_Prefix.Length & ", 20)  ,'-',''),'.',''),'" & mRef_Prefix & "','') as Integer)),0) + 1 From " & TableName & " With (NoLock)  WHERE Abs(Replace(Replace(Replace(" & FieldName & ",'-',''),'.',''),'" & mRef_Prefix & "',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "'  And Date(V_Date) Between " & AgL.Chk_Date(CDate(mStartDate).ToString("s")) & " and  " & AgL.Chk_Date(CDate(mEndDate).ToString("s")) & " And substr(" & FieldName & ",1," & mRef_Prefix.Length & ")='" & mRef_Prefix & "'   "
                FGetManualRefNoForMultiReceipts = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar
            Else
                mQry = "Select IfNull(Max(Cast(Replace(Replace(Substring(" & FieldName & "," & mRef_Prefix.Length & ", 20),'-',''),'.','') as integer)),0)+1 From " & TableName & " With (NoLock)  WHERE IsNumeric(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "'  And Date(V_Date) Between " & AgL.Chk_Date(CDate(mStartDate).ToString("s")) & " and  " & AgL.Chk_Date(CDate(mEndDate).ToString("s")) & "    "
                FGetManualRefNoForMultiReceipts = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            End If


            mRef_PadLength = AgL.VNull(AgL.Dman_Execute("Select Ref_PadLength From Voucher_Prefix With (NoLock) Where V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' And Site_Code = '" & Site_Code & "' And Date_From = " & AgL.Chk_Date(CDate(mStartDate).ToString("s")) & " And Date_To = " & AgL.Chk_Date(CDate(mEndDate).ToString("s")) & "", AgL.GcnRead).ExecuteScalar)
            If mRef_PadLength = 0 Then
                FGetManualRefNoForMultiReceipts = mRef_Prefix & FGetManualRefNoForMultiReceipts.ToString.PadLeft(4, "0")
            Else
                FGetManualRefNoForMultiReceipts = mRef_Prefix & FGetManualRefNoForMultiReceipts.ToString.PadLeft(mRef_PadLength, "0")
            End If
        End If
    End Function
End Class