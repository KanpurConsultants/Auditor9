Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Public Class FrmPaymentDetail
    Dim AgL As AgLibrary.ClsMain
    Dim mEntryMode As String
    Private DTMaster As New DataTable()
    'Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = ""    

    Public Enum TransactionType
        Received
        Payment
    End Enum

    Public PubPaymentRec As AgLibrary.ClsMain.PaymentDetail

    Dim mSearchCode As String = "", mV_Date As Date = Nothing, mSite_Code As String = "", mPartyAc As String = "", mPartyDrCr As String = ""
    Dim mBank_Code$ = "", mBank_Code2$ = "", mBank_Code3$ = "", mCardBank_Code$ = "", mAcTransferBank_Code$ = ""
    Dim mPayRec As AgLibrary.ClsMain.PaymentDetail = Nothing
    Dim mTransType As TransactionType

    Dim mV_No As Long = 0, mV_Prefix As String = ""

    Public Sub New(ByVal AgLibVar As ClsMain, ByVal EntryMode As String, ByVal SearchCode As String, ByVal V_Date As Date, ByVal Site_Code As String, ByVal PartyAc As String, ByVal PartyDrCr As String, ByVal TransType As TransactionType)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar

        mEntryMode = EntryMode
        mSearchCode = SearchCode
        mV_Date = V_Date
        mSite_Code = Site_Code
        mPartyAc = PartyAc
        mPartyDrCr = PartyDrCr
        mTransType = TransType
    End Sub


    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub


    Private Sub IniGrid()
        ''<Executable Code>
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub


    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)

        Select Case Me.ActiveControl.Name
            'Case Control.Name
            '<<Executable Code>>
        End Select
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            With Me
                .Height = 450
                .Width = 900
            End With

            Call IniGrid()
            Call FIniMaster()
            Call Ini_List()
            Call DispText()
            Call MoveRec()

            Call DispText()

            If AgL.PubBlnIsBankMasterActive Then
                TxtBank_Code.AgMasterHelp = False
                TxtBank_Code2.AgMasterHelp = False
                TxtBank_Code3.AgMasterHelp = False
                TxtCardBank_Code.AgMasterHelp = False
                TxtAcTransferBank_Code.AgMasterHelp = False
            End If

            TxtCashAc.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        'mQry = "Select D.DocId As SearchCode " & _
        '        " From DocketBooking D " & _
        '        " Where 1=1 " & AgL.CondStrFinancialYear("D.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
        '        " And " & PLib.PubSiteCondition("D.Site_Code", AgL.PubSiteCode) & ""

        'Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        Dim GcnRead As New SqlConnection
        Try
            GcnRead.ConnectionString = AgL.Gcn_ConnectionString
            GcnRead.Open()

            mQry = "Select Sg.SubCode As Code, Sg.Name As [A/c Name], Sg.Nature " & _
                    " From SubGroup Sg With (NoLock) " & _
                    " Where Sg.Nature ='Cash' And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Sg.Site_Code", AgL.PubSiteCode, "Sg.CommonAc") & " " & _
                    " Order By Sg.Name"
            TxtCashAc.AgHelpDataSet() = AgL.FillData(mQry, GcnRead)

            mQry = "Select Sg.SubCode As Code, Sg.Name As [A/c Name], Sg.Nature " & _
                    " From SubGroup Sg With (NoLock) " & _
                    " Where Sg.Nature ='Bank' And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Sg.Site_Code", AgL.PubSiteCode, "Sg.CommonAc") & " " & _
                    " Order By Sg.Name"
            TxtBankAc.AgHelpDataSet() = AgL.FillData(mQry, GcnRead)
            TxtBankAc2.AgHelpDataSet() = TxtBankAc.AgHelpDataSet.Copy
            TxtBankAc3.AgHelpDataSet() = TxtBankAc.AgHelpDataSet.Copy
            TxtAcTransferBankAc.AgHelpDataSet() = TxtBankAc.AgHelpDataSet.Copy
            TxtCardAc.AgHelpDataSet() = TxtBankAc.AgHelpDataSet.Copy

            mQry = "Select Sg.SubCode As Code, Sg.Name As [A/c Name], Sg.Nature " & _
                    " From SubGroup Sg With (NoLock) " & _
                    " Where " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Sg.Site_Code", AgL.PubSiteCode, "Sg.CommonAc") & " " & _
                    " Order By Sg.Name"
            TxtAdjustmentAc.AgHelpDataSet() = AgL.FillData(mQry, GcnRead)

            mQry = "Select Sg.SubCode As Code, Sg.Name As [A/c Name], Sg.Nature " & _
                    " From SubGroup Sg With (NoLock) " & _
                    " Where 1=1 And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Sg.Site_Code", AgL.PubSiteCode, "Sg.CommonAc") & " " & _
                    " Order By Sg.Name"
            TxtPartyAc.AgHelpDataSet() = AgL.FillData(mQry, GcnRead)

            Call IniBankNameHelp()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub IniBankNameHelp()
        Try
            mQry = "SELECT B.Bank_Code AS Code, B.Bank_Name AS Name, " & _
                    " Case When IsNull(IsActive,0) <> 0 Then 'Yes' Else 'No' End As IsActive " & _
                    " FROM Bank B With (NoLock) " & _
                    " ORDER BY B.Bank_Name"
            TxtBank_Code.AgHelpDataSet() = AgL.FillData(mQry, AgL.GcnRead)
            TxtBank_Code2.AgHelpDataSet() = TxtBank_Code.AgHelpDataSet.Copy
            TxtBank_Code3.AgHelpDataSet() = TxtBank_Code.AgHelpDataSet.Copy
            TxtCardBank_Code.AgHelpDataSet() = TxtBank_Code.AgHelpDataSet.Copy
            TxtAcTransferBank_Code.AgHelpDataSet() = TxtBank_Code.AgHelpDataSet.Copy
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long = 0
        Dim mTransFlag As Boolean = False
        Try
            FClear()
            BlankText()
            If mSearchCode.Trim <> "" Then
                'MastPos = BMBMaster.Position
                mV_Prefix = AgL.DeCodeDocID(mSearchCode, ClsMain.DocIdPart.VoucherPrefix)
                mV_No = Val(AgL.DeCodeDocID(mSearchCode, ClsMain.DocIdPart.VoucherNo))

                TxtDocId.Text = mSearchCode
                TxtPartyAc.AgSelectedValue = mPartyAc

                mQry = "Select P.*  " & _
                        " From PaymentDetail P " & _
                        " Where DocId='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        If Me.PubPaymentRec.TotalAmount > 0 Then
                            Call FillData()
                        Else
                            mPartyDrCr = AgL.XNull(.Rows(0)("PartyDrCr"))
                            TxtCashAc.AgSelectedValue = AgL.XNull(.Rows(0)("CashAc"))
                            TxtCashAmount.Text = Format(AgL.VNull(.Rows(0)("CashAmount")), "0.00")

                            TxtBankAc.AgSelectedValue = AgL.XNull(.Rows(0)("BankAc"))
                            TxtBankAmount.Text = Format(AgL.VNull(.Rows(0)("BankAmount")), "0.00")
                            TxtBank_Code.AgSelectedValue = AgL.XNull(.Rows(0)("Bank_Code"))
                            mBank_Code = AgL.XNull(.Rows(0)("Bank_Code"))
                            TxtChq_No.Text = AgL.XNull(.Rows(0)("Chq_No"))
                            TxtChq_Date.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Chq_Date")))
                            TxtClg_Date.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Clg_Date")))

                            TxtBankAc2.AgSelectedValue = AgL.XNull(.Rows(0)("BankAc2"))
                            TxtBankAmount2.Text = Format(AgL.VNull(.Rows(0)("BankAmount2")), "0.00")
                            TxtBank_Code2.AgSelectedValue = AgL.XNull(.Rows(0)("Bank_Code2"))
                            mBank_Code2 = AgL.XNull(.Rows(0)("Bank_Code2"))
                            TxtChq_No2.Text = AgL.XNull(.Rows(0)("Chq_No2"))
                            TxtChq_Date2.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Chq_Date2")))
                            TxtClg_Date2.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Clg_Date2")))

                            TxtBankAc3.AgSelectedValue = AgL.XNull(.Rows(0)("BankAc3"))
                            TxtBankAmount3.Text = Format(AgL.VNull(.Rows(0)("BankAmount3")), "0.00")
                            TxtBank_Code3.AgSelectedValue = AgL.XNull(.Rows(0)("Bank_Code3"))
                            mBank_Code3 = AgL.XNull(.Rows(0)("Bank_Code3"))
                            TxtChq_No3.Text = AgL.XNull(.Rows(0)("Chq_No3"))
                            TxtChq_Date3.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Chq_Date3")))
                            TxtClg_Date3.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Clg_Date3")))

                            TxtCardAc.AgSelectedValue = AgL.XNull(.Rows(0)("CardAc"))
                            TxtCardAmount.Text = Format(AgL.VNull(.Rows(0)("CardAmount")), "0.00")
                            TxtCardBank_Code.AgSelectedValue = AgL.XNull(.Rows(0)("CardBank_Code"))
                            mCardBank_Code = AgL.XNull(.Rows(0)("CardBank_Code"))
                            TxtCard_No.Text = AgL.XNull(.Rows(0)("Card_No"))

                            TxtAcTransferBankAc.AgSelectedValue = AgL.XNull(.Rows(0)("AcTransferBankAc"))
                            TxtAcTransferAmount.Text = Format(AgL.VNull(.Rows(0)("AcTransferAmount")), "0.00")
                            TxtAcTransferBank_Code.AgSelectedValue = AgL.XNull(.Rows(0)("AcTransferBank_Code"))
                            mAcTransferBank_Code = AgL.XNull(.Rows(0)("AcTransferBank_Code"))
                            TxtAcTransferAcNo.Text = AgL.XNull(.Rows(0)("AcTransferAcNo"))

                            TxtAdjustmentAc.AgSelectedValue = AgL.XNull(.Rows(0)("AdjustmentAc"))
                            TxtAdjustmentAmount.Text = Format(AgL.VNull(.Rows(0)("AdjustmentAmount")), "0.00")
                            TxtAdjustmentRemark.Text = AgL.XNull(.Rows(0)("AdjustmentRemark"))

                            TxtTotalPayment.Text = Format(AgL.VNull(.Rows(0)("TotalAmount")), "0.00")
                        End If
                    Else
                        Call FillData()
                    End If
                End With
            Else
                BlankText()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub

    Private Sub FillData()
        With Me.PubPaymentRec
            mPartyDrCr = IIf(AgL.XNull(.PartyDrCr) <> "", AgL.XNull(.PartyDrCr), mPartyDrCr)
            TxtCashAc.AgSelectedValue = AgL.XNull(.CashAc)
            TxtCashAmount.Text = Format(AgL.VNull(.CashAmount), "0.00")

            TxtBankAc.AgSelectedValue = AgL.XNull(.BankAc)
            TxtBankAmount.Text = Format(AgL.VNull(.BankAmount), "0.00")
            TxtBank_Code.AgSelectedValue = AgL.XNull(.Bank_Code)
            mBank_Code = AgL.XNull(.Bank_Code)
            TxtChq_No.Text = AgL.XNull(.Chq_No)
            TxtChq_Date.Text = AgL.RetDate(AgL.XNull(.Chq_Date))
            TxtClg_Date.Text = AgL.RetDate(AgL.XNull(.Clg_Date))

            TxtBankAc2.AgSelectedValue = AgL.XNull(.BankAc2)
            TxtBankAmount2.Text = Format(AgL.VNull(.BankAmount2), "0.00")
            TxtBank_Code2.AgSelectedValue = AgL.XNull(.Bank_Code2)
            mBank_Code2 = AgL.XNull(.Bank_Code2)
            TxtChq_No2.Text = AgL.XNull(.Chq_No2)
            TxtChq_Date2.Text = AgL.RetDate(AgL.XNull(.Chq_Date2))
            TxtClg_Date2.Text = AgL.RetDate(AgL.XNull(.Clg_Date2))

            TxtBankAc3.AgSelectedValue = AgL.XNull(.BankAc3)
            TxtBankAmount3.Text = Format(AgL.VNull(.BankAmount3), "0.00")
            TxtBank_Code3.AgSelectedValue = AgL.XNull(.Bank_Code3)
            mBank_Code3 = AgL.XNull(.Bank_Code3)
            TxtChq_No3.Text = AgL.XNull(.Chq_No3)
            TxtChq_Date3.Text = AgL.RetDate(AgL.XNull(.Chq_Date3))
            TxtClg_Date3.Text = AgL.RetDate(AgL.XNull(.Clg_Date3))

            TxtCardAc.AgSelectedValue = AgL.XNull(.CardAc)
            TxtCardAmount.Text = Format(AgL.VNull(.CardAmount), "0.00")
            TxtCardBank_Code.AgSelectedValue = AgL.XNull(.CardBank_Code)
            mCardBank_Code = AgL.XNull(.CardBank_Code)
            TxtCard_No.Text = AgL.XNull(.Card_No)

            TxtAcTransferBankAc.AgSelectedValue = AgL.XNull(.AcTransferBankAc)
            TxtAcTransferAmount.Text = Format(AgL.VNull(.AcTransferAmount), "0.00")
            TxtAcTransferBank_Code.AgSelectedValue = AgL.XNull(.AcTransferBank_Code)
            mAcTransferBank_Code = AgL.XNull(.AcTransferBank_Code)
            TxtAcTransferAcNo.Text = AgL.XNull(.AcTransferAcNo)

            TxtAdjustmentAc.AgSelectedValue = AgL.XNull(.AdjustmentAc)
            TxtAdjustmentAmount.Text = Format(AgL.VNull(.AdjustmentAmount), "0.00")
            TxtAdjustmentRemark.Text = AgL.XNull(.AdjustmentRemark)

            TxtTotalPayment.Text = Format(AgL.VNull(.TotalAmount), "0.00")
        End With
    End Sub

    Private Sub BlankText()
        'Coding To Blank Controls
        AgL.BlankCtrl(Me)
        mBank_Code = "" : mBank_Code2 = "" : mBank_Code3 = "" : mCardBank_Code = "" : mAcTransferBank_Code = ""
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        Call EnableDisableControls(Me, mEntryMode)
        TxtPartyAc.Enabled = False
    End Sub


    Private Sub FClear()
        DTStruct.Clear()
    End Sub
    Private Sub FAddRowStructure()
        Dim DRStruct As DataRow
        Try
            DRStruct = DTStruct.NewRow
            DTStruct.Rows.Add(DRStruct)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TxtCashAc.Enter, TxtBank_Code.Enter, TxtChq_No.Enter, TxtChq_Date.Enter, _
        TxtBank_Code2.Enter, TxtBank_Code3.Enter, TxtCardBank_Code.Enter, TxtAcTransferBank_Code.Enter

        Try
            Select Case sender.name
                Case TxtBank_Code.Name, TxtBank_Code2.Name, TxtBank_Code3.Name, TxtCardBank_Code.Name, TxtAcTransferBank_Code.Name
                    sender.AgRowFilter = " IsActive = 'Yes' "
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
      TxtCashAmount.Validating, TxtCashAc.Validating, TxtBankAmount.Validating, TxtBank_Code.Validating, TxtChq_No.Validating, TxtChq_Date.Validating, _
      TxtBankAc.Validating, TxtAcTransferBankAc.Validating, TxtAcTransferBank_Code.Validating, TxtAcTransferAcNo.Validating, TxtAcTransferAmount.Validating, _
      TxtCardAc.Validating, TxtCard_No.Validating, TxtCardAmount.Validating, TxtCardBank_Code.Validating, TxtClg_Date.Validating, _
      TxtBankAc2.Validating, TxtBankAmount2.Validating, TxtChq_No2.Validating, TxtChq_Date2.Validating, TxtClg_Date2.Validating, TxtBank_Code2.Validating, _
      TxtBankAc3.Validating, TxtBankAmount3.Validating, TxtChq_No3.Validating, TxtChq_Date3.Validating, TxtClg_Date3.Validating, TxtBank_Code3.Validating, _
      TxtAdjustmentAc.Validating, TxtAdjustmentAmount.Validating, TxtAdjustmentRemark.Validating

        Dim DrTemp As DataRow() = Nothing
        Dim DaTbl As DataTable
        Dim bStrBankName$ = ""
        Try
            Select Case sender.NAME
                Case TxtBank_Code.Name, TxtBank_Code2.Name, TxtBank_Code3.Name, TxtCardBank_Code.Name, TxtAcTransferBank_Code.Name
                    bStrBankName = sender.text

                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        '<Executable Code>
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            If Not AgL.StrCmp(sender.Text, AgL.XNull(DrTemp(0)("Name"))) Then
                                sender.AgSelectedValue = ""
                                sender.text = bStrBankName
                            End If
                        End If
                    End If
                Case TxtChq_No.Name, TxtChq_No2.Name, TxtChq_No3.Name
                    If Val(sender.Text) > 0 Then
                        sender.Text = sender.Text.PadLeft(6, "0")
                    Else
                        sender.Text = ""
                    End If
            End Select

            Call Calculation()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DrTemp = Nothing
            DaTbl = Nothing
        End Try
    End Sub


    Private Function Data_Validation() As Boolean
        Dim mTmDocId As String = ""
        Dim I As Integer = 0
        Dim bBlnBankNameFlag As Boolean = False
        Try
            Call Calculation()

            If AgL.RequiredField(TxtDocId, "Document ID") Then Exit Function

            If mPartyDrCr.Trim = "" Then MsgBox("Fatel Error!" & vbCrLf & "Party Dr/Cr Not Defined!...") : Exit Function

            If Val(TxtCashAmount.Text) > 0 Then
                If AgL.RequiredField(TxtCashAc, "Cash A/c") Then Exit Function
            Else
                TxtCashAc.AgSelectedValue = ""
            End If

            If Val(TxtAdjustmentAmount.Text) > 0 Then
                If AgL.RequiredField(TxtAdjustmentAc, "Adjustment A/c") Then Exit Function
            Else
                TxtAdjustmentAc.AgSelectedValue = ""
                TxtAdjustmentRemark.Text = ""
            End If

            If Val(TxtBankAmount.Text) > 0 Then
                If AgL.RequiredField(TxtBankAc, "Bank A/c") Then Exit Function
                If AgL.RequiredField(TxtBank_Code, "Bank Name") Then Exit Function
                If TxtBank_Code.AgSelectedValue.Trim = "" Then
                    mBank_Code = AgL.funGetBankCode(TxtBank_Code.Text, mSite_Code, AgL.PubDivCode)
                    If mBank_Code.Trim = "" Then MsgBox("Problem in Bank Name Code Generation!...") : TxtBank_Code.Focus() : Exit Function
                    If Not bBlnBankNameFlag Then bBlnBankNameFlag = True
                Else
                    mBank_Code = TxtBank_Code.AgSelectedValue
                End If

                If AgL.RequiredField(TxtChq_No, "Cheque No.") Then Exit Function
                If AgL.RequiredField(TxtChq_Date, "Cheque Date") Then Exit Function
                If TxtClg_Date.Text.Trim <> "" Then
                    If CDate(TxtClg_Date.Text) < CDate(TxtChq_Date.Text) Then
                        MsgBox("Cheque Clearing Date Can't be less than from Cheque Date!...")
                        TxtClg_Date.Focus() : Exit Function
                    End If
                End If
            Else
                TxtBankAc.AgSelectedValue = ""
                TxtBank_Code.AgSelectedValue = ""
                mBank_Code = ""
                TxtChq_No.Text = ""
                TxtChq_Date.Text = ""
                TxtClg_Date.Text = ""
            End If

            If Val(TxtBankAmount2.Text) > 0 Then
                If AgL.RequiredField(TxtBankAc2, "Bank A/c") Then Exit Function
                If AgL.RequiredField(TxtBank_Code2, "Bank Name") Then Exit Function
                If TxtBank_Code2.AgSelectedValue.Trim = "" Then
                    mBank_Code2 = AgL.funGetBankCode(TxtBank_Code2.Text, mSite_Code, AgL.PubDivCode)
                    If mBank_Code2.Trim = "" Then MsgBox("Problem in Bank Name Code Generation!...") : TxtBank_Code2.Focus() : Exit Function
                    If Not bBlnBankNameFlag Then bBlnBankNameFlag = True
                Else
                    mBank_Code2 = TxtBank_Code2.AgSelectedValue
                End If                

                If AgL.RequiredField(TxtChq_No2, "Cheque No.") Then Exit Function
                If AgL.RequiredField(TxtChq_Date2, "Cheque Date") Then Exit Function
                If TxtClg_Date2.Text.Trim <> "" Then
                    If CDate(TxtClg_Date2.Text) < CDate(TxtChq_Date2.Text) Then
                        MsgBox("Cheque Clearing Date Can't be less than from Cheque Date!...")
                        TxtClg_Date2.Focus() : Exit Function
                    End If
                End If
            Else
                TxtBankAc2.AgSelectedValue = ""
                TxtBank_Code2.AgSelectedValue = ""
                mBank_Code2 = ""
                TxtChq_No2.Text = ""
                TxtChq_Date2.Text = ""
                TxtClg_Date2.Text = ""
            End If

            If Val(TxtBankAmount3.Text) > 0 Then
                If AgL.RequiredField(TxtBankAc3, "Bank A/c") Then Exit Function
                If AgL.RequiredField(TxtBank_Code3, "Bank Name") Then Exit Function
                If TxtBank_Code3.AgSelectedValue.Trim = "" Then
                    mBank_Code3 = AgL.funGetBankCode(TxtBank_Code3.Text, mSite_Code, AgL.PubDivCode)
                    If mBank_Code3.Trim = "" Then MsgBox("Problem in Bank Name Code Generation!...") : TxtBank_Code3.Focus() : Exit Function
                    If Not bBlnBankNameFlag Then bBlnBankNameFlag = True
                Else
                    mBank_Code3 = TxtBank_Code3.AgSelectedValue
                End If

                If AgL.RequiredField(TxtChq_No3, "Cheque No.") Then Exit Function
                If AgL.RequiredField(TxtChq_Date3, "Cheque Date") Then Exit Function
                If TxtClg_Date3.Text.Trim <> "" Then
                    If CDate(TxtClg_Date3.Text) < CDate(TxtChq_Date3.Text) Then
                        MsgBox("Cheque Clearing Date Can't be less than from Cheque Date!...")
                        TxtClg_Date3.Focus() : Exit Function
                    End If
                End If
            Else
                TxtBankAc3.AgSelectedValue = ""
                TxtBank_Code3.AgSelectedValue = ""
                mBank_Code3 = ""
                TxtChq_No3.Text = ""
                TxtChq_Date3.Text = ""
                TxtClg_Date3.Text = ""
            End If

            If Val(TxtCardAmount.Text) > 0 Then
                If AgL.RequiredField(TxtCardAc, "Card A/c") Then Exit Function
                If AgL.RequiredField(TxtCardBank_Code, "Card Bank") Then Exit Function
                If TxtCardBank_Code.AgSelectedValue.Trim = "" Then
                    mCardBank_Code = AgL.funGetBankCode(TxtCardBank_Code.Text, mSite_Code, AgL.PubDivCode)
                    If mCardBank_Code.Trim = "" Then MsgBox("Problem in Card Bank Code Generation!...") : TxtCardBank_Code.Focus() : Exit Function
                    If Not bBlnBankNameFlag Then bBlnBankNameFlag = True
                Else
                    mCardBank_Code = TxtCardBank_Code.AgSelectedValue
                End If

                If AgL.RequiredField(TxtCard_No, "Card No.") Then Exit Function
            Else
                TxtCardAc.AgSelectedValue = ""
                TxtCardBank_Code.AgSelectedValue = ""
                mCardBank_Code = ""
                TxtCard_No.Text = ""
            End If

            If Val(TxtAcTransferAmount.Text) > 0 Then
                If AgL.RequiredField(TxtAcTransferBankAc, "Account Transfer Bank A/c") Then Exit Function
                If AgL.RequiredField(TxtAcTransferBank_Code, "Account Transfer Bank") Then Exit Function
                If TxtAcTransferBank_Code.AgSelectedValue.Trim = "" Then
                    mAcTransferBank_Code = AgL.funGetBankCode(TxtAcTransferBank_Code.Text, mSite_Code, AgL.PubDivCode)
                    If mAcTransferBank_Code.Trim = "" Then MsgBox("Problem in Account Transfer Bank Code Generation!...") : TxtAcTransferBank_Code.Focus() : Exit Function
                    If Not bBlnBankNameFlag Then bBlnBankNameFlag = True
                Else
                    mBank_Code3 = TxtBank_Code3.AgSelectedValue
                End If

                If AgL.RequiredField(TxtAcTransferAcNo, "Account Transfer A/c No.") Then Exit Function
            Else
                TxtAcTransferBankAc.AgSelectedValue = ""
                TxtAcTransferBank_Code.AgSelectedValue = ""
                mAcTransferBank_Code = ""
                TxtAcTransferAcNo.Text = ""
            End If

            Call IniBankNameHelp()

            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub Calculation()
        If AgL.StrCmp(mEntryMode, "Browse") Then Exit Sub
        Dim bTotal As Double = 0

        bTotal = Val(TxtCashAmount.Text) + Val(TxtBankAmount.Text) + Val(TxtBankAmount2.Text) + _
                    Val(TxtBankAmount3.Text) + Val(TxtCardAmount.Text) + Val(TxtAcTransferAmount.Text) + _
                    Val(TxtAdjustmentAmount.Text)

        TxtTotalPayment.Text = Format(bTotal, "0.00")
    End Sub

    Public Function DeletePaymentDetail(ByVal mConn As SqlClient.SqlConnection, ByVal mCmd As SqlClient.SqlCommand) As Boolean
        Try
            mQry = "Delete From PaymentDetail Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)

            AgL.LedgerUnPost(mConn, mCmd, mSearchCode)

            DeletePaymentDetail = True
        Catch ex As Exception
            DeletePaymentDetail = False
        End Try
    End Function

    Public Function SavePaymentDetail(ByRef PaymentRec As AgLibrary.ClsMain.PaymentDetail, ByVal mConn As SqlClient.SqlConnection, ByVal mCmd As SqlClient.SqlCommand) As Boolean
        Dim mTrans As Boolean = False
        Try
            If mEntryMode = "Edit" Then
                mQry = "Delete From PaymentDetail Where DocId = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
            End If

            If Val(PaymentRec.TotalAmount) > 0 Then
                mQry = "INSERT INTO PaymentDetail ( DocId, CashAc, CashAmount, BankAc, BankAmount, Bank_Code, Chq_No, " & _
                        " Chq_Date, Clg_Date, CardAc, CardAmount, CardBank_Code, Card_No, AcTransferBankAc, AcTransferAmount," & _
                        " AcTransferBank_Code, AcTransferAcNo, TotalAmount, PartyDrCr, " & _
                        " BankAc2, BankAmount2, Bank_Code2, Chq_No2, Chq_Date2, Clg_Date2, " & _
                        " BankAc3, BankAmount3, Bank_Code3, Chq_No3, Chq_Date3, Clg_Date3, " & _
                        " AdjustmentAc, AdjustmentAmount, AdjustmentRemark) VALUES  ( " & _
                        " " & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(PaymentRec.CashAc) & ", " & Val(PaymentRec.CashAmount) & "," & _
                        " " & AgL.Chk_Text(PaymentRec.BankAc) & ", " & Val(PaymentRec.BankAmount) & ", " & AgL.Chk_Text(PaymentRec.Bank_Code) & "," & _
                        " " & AgL.Chk_Text(PaymentRec.Chq_No) & ", " & AgL.ConvertDate(PaymentRec.Chq_Date) & ", " & AgL.ConvertDate(PaymentRec.Clg_Date) & "," & _
                        " " & AgL.Chk_Text(PaymentRec.CardAc) & ", " & Val(PaymentRec.CardAmount) & ", " & AgL.Chk_Text(PaymentRec.CardBank_Code) & ", " & AgL.Chk_Text(PaymentRec.Card_No) & "," & _
                        " " & AgL.Chk_Text(PaymentRec.AcTransferBankAc) & ", " & Val(PaymentRec.AcTransferAmount) & ", " & AgL.Chk_Text(PaymentRec.AcTransferBank_Code) & "," & _
                        " " & AgL.Chk_Text(PaymentRec.AcTransferAcNo) & ", " & Val(PaymentRec.TotalAmount) & ", " & AgL.Chk_Text(PaymentRec.PartyDrCr) & ", " & _
                        " " & AgL.Chk_Text(PaymentRec.BankAc2) & ", " & Val(PaymentRec.BankAmount2) & ", " & AgL.Chk_Text(PaymentRec.Bank_Code2) & "," & _
                        " " & AgL.Chk_Text(PaymentRec.Chq_No2) & ", " & AgL.ConvertDate(PaymentRec.Chq_Date2) & ", " & AgL.ConvertDate(PaymentRec.Clg_Date2) & "," & _
                        " " & AgL.Chk_Text(PaymentRec.BankAc3) & ", " & Val(PaymentRec.BankAmount3) & ", " & AgL.Chk_Text(PaymentRec.Bank_Code3) & "," & _
                        " " & AgL.Chk_Text(PaymentRec.Chq_No3) & ", " & AgL.ConvertDate(PaymentRec.Chq_Date3) & ", " & AgL.ConvertDate(PaymentRec.Clg_Date3) & ", " & _
                        " " & AgL.Chk_Text(PaymentRec.AdjustmentAc) & ", " & Val(PaymentRec.AdjustmentAmount) & ", " & AgL.Chk_Text(PaymentRec.AdjustmentRemark) & " " & _
                        " )"
                AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
            End If

            SavePaymentDetail = True
        Catch ex As Exception

            SavePaymentDetail = False
        End Try
    End Function

    'Public Function AccountPosting(ByRef PaymentRec As AgLibrary.ClsMain.PaymentDetail, ByVal mConn As SqlClient.SqlConnection, ByVal mConnectionString As String, ByVal mCmd As SqlClient.SqlCommand, ByRef LedgAry() As AgLibrary.ClsMain.LedgRec, _
    '                                Optional ByVal mCommonNarr As String = "", Optional ByVal mPostTotalAmountYesNo As Boolean = True) As Boolean
    '    Dim I As Integer = 0
    '    Dim mNarr$ = "", mTransStr$ = "", mBankName$ = "", bContraSub$ = ""
    '    Dim mVNo As Long = Val(AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))
    '    Dim mPostFlag As Boolean = False, bFlag As Boolean = False
    '    Dim GcnRead As New SqlConnection

    '    GcnRead.ConnectionString = mConnectionString
    '    GcnRead.Open()

    '    AccountPosting = True

    '    If mTransType = TransactionType.Payment Then
    '        mTransStr = "Paid"
    '    ElseIf mTransType = TransactionType.Received Then
    '        mTransStr = "Received"
    '    End If

    '    If LedgAry Is Nothing Then
    '        I = 0
    '        ReDim Preserve LedgAry(I)
    '        mPostFlag = True
    '    Else
    '        I = UBound(LedgAry) + 1
    '        ReDim Preserve LedgAry(I)
    '    End If

    '    If Val(PaymentRec.TotalAmount) > 0 Then
    '        If Val(PaymentRec.CashAmount) > 0 Then
    '            mNarr = "Being Cash Amount of Rs. " & Val(PaymentRec.CashAmount) & " " & mTransStr & ""

    '            If bFlag = False Then
    '                bFlag = True
    '            Else
    '                I = UBound(LedgAry) + 1
    '                ReDim Preserve LedgAry(I)
    '            End If

    '            LedgAry(I).SubCode = PaymentRec.CashAc
    '            LedgAry(I).ContraSub = mPartyAc
    '            LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.CashAmount))
    '            LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.CashAmount), 0)
    '            LedgAry(I).Narration = mNarr

    '            If bContraSub.Trim = "" Then bContraSub = PaymentRec.CashAc
    '        End If

    '        If Val(PaymentRec.BankAmount) > 0 Then
    '            mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.Bank_Code & "'"
    '            mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

    '            mNarr = "Being Amount " & mTransStr & " Via Cheque No. """ & PaymentRec.Chq_No & """ , Cheque Date " & PaymentRec.Chq_Date & " Cheque Bank " & mBankName & ""

    '            If bFlag = False Then
    '                bFlag = True
    '            Else
    '                I = UBound(LedgAry) + 1
    '                ReDim Preserve LedgAry(I)
    '            End If

    '            LedgAry(I).SubCode = PaymentRec.BankAc
    '            LedgAry(I).ContraSub = mPartyAc
    '            LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.BankAmount))
    '            LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.BankAmount), 0)
    '            LedgAry(I).Narration = mNarr
    '            LedgAry(I).ChqNo = PaymentRec.Chq_No
    '            LedgAry(I).ChqDt = PaymentRec.Chq_Date
    '            LedgAry(I).ClrChqDt = PaymentRec.Clg_Date

    '            If bContraSub.Trim = "" Then bContraSub = PaymentRec.BankAc
    '        End If


    '        If Val(PaymentRec.BankAmount2) > 0 Then
    '            mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.Bank_Code2 & "'"
    '            mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

    '            mNarr = "Being Amount " & mTransStr & " Via Cheque No. """ & PaymentRec.Chq_No2 & """ , Cheque Date " & PaymentRec.Chq_Date2 & " Cheque Bank " & mBankName & ""

    '            If bFlag = False Then
    '                bFlag = True
    '            Else
    '                I = UBound(LedgAry) + 1
    '                ReDim Preserve LedgAry(I)
    '            End If

    '            LedgAry(I).SubCode = PaymentRec.BankAc2
    '            LedgAry(I).ContraSub = mPartyAc
    '            LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.BankAmount2))
    '            LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.BankAmount2), 0)
    '            LedgAry(I).Narration = mNarr
    '            LedgAry(I).ChqNo = PaymentRec.Chq_No2
    '            LedgAry(I).ChqDt = PaymentRec.Chq_Date2
    '            LedgAry(I).ClrChqDt = PaymentRec.Clg_Date2

    '            If bContraSub.Trim = "" Then bContraSub = PaymentRec.BankAc2
    '        End If

    '        If Val(PaymentRec.BankAmount3) > 0 Then
    '            mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.Bank_Code3 & "'"
    '            mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

    '            mNarr = "Being Amount " & mTransStr & " Via Cheque No. """ & PaymentRec.Chq_No3 & """ , Cheque Date " & PaymentRec.Chq_Date3 & " Cheque Bank " & mBankName & ""

    '            If bFlag = False Then
    '                bFlag = True
    '            Else
    '                I = UBound(LedgAry) + 1
    '                ReDim Preserve LedgAry(I)
    '            End If

    '            LedgAry(I).SubCode = PaymentRec.BankAc3
    '            LedgAry(I).ContraSub = mPartyAc
    '            LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.BankAmount3))
    '            LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.BankAmount3), 0)
    '            LedgAry(I).Narration = mNarr
    '            LedgAry(I).ChqNo = PaymentRec.Chq_No3
    '            LedgAry(I).ChqDt = PaymentRec.Chq_Date3
    '            LedgAry(I).ClrChqDt = PaymentRec.Clg_Date3

    '            If bContraSub.Trim = "" Then bContraSub = PaymentRec.BankAc3
    '        End If

    '        If Val(PaymentRec.CardAmount) > 0 Then
    '            mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.CardBank_Code & "'"
    '            mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

    '            mNarr = "Being Amount " & mTransStr & " Via Card No. """ & PaymentRec.Card_No & """ ,Card Bank " & mBankName & ""

    '            If bFlag = False Then
    '                bFlag = True
    '            Else
    '                I = UBound(LedgAry) + 1
    '                ReDim Preserve LedgAry(I)
    '            End If

    '            LedgAry(I).SubCode = PaymentRec.CardAc
    '            LedgAry(I).ContraSub = mPartyAc
    '            LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.CardAmount))
    '            LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.CardAmount), 0)
    '            LedgAry(I).Narration = mNarr
    '            LedgAry(I).ChqNo = PaymentRec.Card_No

    '            If bContraSub.Trim = "" Then bContraSub = PaymentRec.CardAc
    '        End If

    '        If Val(PaymentRec.AcTransferAmount) > 0 Then
    '            mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.AcTransferBank_Code & "'"
    '            mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

    '            mNarr = "Being Amount Transferred Via Bank " & mBankName & ""

    '            If bFlag = False Then
    '                bFlag = True
    '            Else
    '                I = UBound(LedgAry) + 1
    '                ReDim Preserve LedgAry(I)
    '            End If

    '            LedgAry(I).SubCode = PaymentRec.AcTransferBankAc
    '            LedgAry(I).ContraSub = mPartyAc
    '            LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.AcTransferAmount))
    '            LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.AcTransferAmount), 0)
    '            LedgAry(I).Narration = mNarr
    '            LedgAry(I).ChqNo = PaymentRec.AcTransferAcNo

    '            If bContraSub.Trim = "" Then bContraSub = PaymentRec.AcTransferBankAc
    '        End If


    '        If Val(PaymentRec.AdjustmentAmount) > 0 Then
    '            mNarr = TxtAdjustmentRemark.Text

    '            If bFlag = False Then
    '                bFlag = True
    '            Else
    '                I = UBound(LedgAry) + 1
    '                ReDim Preserve LedgAry(I)
    '            End If

    '            LedgAry(I).SubCode = PaymentRec.AdjustmentAc
    '            LedgAry(I).ContraSub = mPartyAc
    '            LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.AdjustmentAmount))
    '            LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.AdjustmentAmount), 0)
    '            LedgAry(I).Narration = mNarr

    '            If bContraSub.Trim = "" Then bContraSub = PaymentRec.AdjustmentAc
    '        End If


    '        If mPostTotalAmountYesNo Then
    '            I = UBound(LedgAry) + 1
    '            ReDim Preserve LedgAry(I)

    '            mNarr = "Being Total Amount of Rs. " & Val(PaymentRec.TotalAmount) & " " & mTransStr & ""

    '            LedgAry(I).SubCode = mPartyAc
    '            LedgAry(I).ContraSub = bContraSub
    '            LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.TotalAmount), 0)
    '            LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.TotalAmount))
    '            LedgAry(I).Narration = mNarr
    '        End If

    '        If mPostFlag = True Then
    '            If AgL.LedgerPost(AgL.MidStr(mEntryMode, 0, 1), LedgAry, mConn, mCmd, mSearchCode, mV_Date, AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , mConnectionString) = False Then
    '                AccountPosting = False
    '                Err.Raise(1, "Error in Ledger Posting")
    '            End If
    '        End If
    '    Else
    '        If mPostFlag = True Then
    '            AgL.LedgerUnPost(mConn, mCmd, mSearchCode)
    '        End If
    '    End If
    'End Function

    Public Function AccountPosting(ByRef PaymentRec As AgLibrary.ClsMain.PaymentDetail, ByVal mConn As SqlClient.SqlConnection, ByVal mConnectionString As String, ByVal mCmd As SqlClient.SqlCommand, ByRef LedgAry() As AgLibrary.ClsMain.LedgRec, _
                                        Optional ByVal mCommonNarr As String = "", Optional ByVal mPostTotalAmountYesNo As Boolean = True) As Boolean
        Dim I As Integer = 0
        Dim mNarr$ = "", mTransStr$ = "", mBankName$ = "", bContraSub$ = ""
        Dim mVNo As Long = Val(AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))
        Dim mPostFlag As Boolean = False, bFlag As Boolean = False
        Dim GcnRead As New SqlConnection

        GcnRead.ConnectionString = mConnectionString
        GcnRead.Open()

        AccountPosting = True

        If mTransType = TransactionType.Payment Then
            mTransStr = "Paid"
        ElseIf mTransType = TransactionType.Received Then
            mTransStr = "Received"
        End If

        If LedgAry Is Nothing Then
            I = 0
            ReDim Preserve LedgAry(I)
            mPostFlag = True
        Else
            I = UBound(LedgAry) + 1
            ReDim Preserve LedgAry(I)
        End If

        If Val(PaymentRec.TotalAmount) > 0 Then
            If Val(PaymentRec.CashAmount) > 0 Then
                mNarr = "Being Cash Amount of Rs. " & Val(PaymentRec.CashAmount) & " " & mTransStr & ""

                If bFlag = False Then
                    bFlag = True
                Else
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)
                End If

                LedgAry(I).SubCode = PaymentRec.CashAc
                LedgAry(I).ContraSub = mPartyAc
                LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.CashAmount))
                LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.CashAmount), 0)
                LedgAry(I).Narration = mNarr

                If bContraSub.Trim = "" Then bContraSub = PaymentRec.CashAc

                If mPostTotalAmountYesNo Then
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)

                    LedgAry(I).SubCode = mPartyAc
                    LedgAry(I).ContraSub = bContraSub
                    LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.CashAmount), 0)
                    LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.CashAmount))
                    LedgAry(I).Narration = mNarr
                End If

            End If

            If Val(PaymentRec.BankAmount) > 0 Then
                mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.Bank_Code & "'"
                mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

                mNarr = "Being Amount " & mTransStr & " Via Cheque No. """ & PaymentRec.Chq_No & """ , Cheque Date " & PaymentRec.Chq_Date & " Cheque Bank " & mBankName & ""

                If bFlag = False Then
                    bFlag = True
                Else
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)
                End If

                LedgAry(I).SubCode = PaymentRec.BankAc
                LedgAry(I).ContraSub = mPartyAc
                LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.BankAmount))
                LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.BankAmount), 0)
                LedgAry(I).Narration = mNarr
                LedgAry(I).ChqNo = PaymentRec.Chq_No
                LedgAry(I).ChqDt = PaymentRec.Chq_Date
                LedgAry(I).ClrChqDt = PaymentRec.Clg_Date

                If bContraSub.Trim = "" Then bContraSub = PaymentRec.BankAc

                If mPostTotalAmountYesNo Then
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)

                    LedgAry(I).SubCode = mPartyAc
                    LedgAry(I).ContraSub = bContraSub
                    LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.BankAmount), 0)
                    LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.BankAmount))
                    LedgAry(I).Narration = mNarr
                    LedgAry(I).ChqNo = PaymentRec.Chq_No
                    LedgAry(I).ChqDt = PaymentRec.Chq_Date
                    LedgAry(I).ClrChqDt = PaymentRec.Clg_Date
                End If

            End If


            If Val(PaymentRec.BankAmount2) > 0 Then
                mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.Bank_Code2 & "'"
                mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

                mNarr = "Being Amount " & mTransStr & " Via Cheque No. """ & PaymentRec.Chq_No2 & """ , Cheque Date " & PaymentRec.Chq_Date2 & " Cheque Bank " & mBankName & ""

                If bFlag = False Then
                    bFlag = True
                Else
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)
                End If

                LedgAry(I).SubCode = PaymentRec.BankAc2
                LedgAry(I).ContraSub = mPartyAc
                LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.BankAmount2))
                LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.BankAmount2), 0)
                LedgAry(I).Narration = mNarr
                LedgAry(I).ChqNo = PaymentRec.Chq_No2
                LedgAry(I).ChqDt = PaymentRec.Chq_Date2
                LedgAry(I).ClrChqDt = PaymentRec.Clg_Date2

                If bContraSub.Trim = "" Then bContraSub = PaymentRec.BankAc2

                If mPostTotalAmountYesNo Then
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)

                    LedgAry(I).SubCode = mPartyAc
                    LedgAry(I).ContraSub = bContraSub
                    LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.BankAmount2), 0)
                    LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.BankAmount2))
                    LedgAry(I).Narration = mNarr
                    LedgAry(I).ChqNo = PaymentRec.Chq_No2
                    LedgAry(I).ChqDt = PaymentRec.Chq_Date2
                    LedgAry(I).ClrChqDt = PaymentRec.Clg_Date2
                End If

            End If

            If Val(PaymentRec.BankAmount3) > 0 Then
                mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.Bank_Code3 & "'"
                mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

                mNarr = "Being Amount " & mTransStr & " Via Cheque No. """ & PaymentRec.Chq_No3 & """ , Cheque Date " & PaymentRec.Chq_Date3 & " Cheque Bank " & mBankName & ""

                If bFlag = False Then
                    bFlag = True
                Else
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)
                End If

                LedgAry(I).SubCode = PaymentRec.BankAc3
                LedgAry(I).ContraSub = mPartyAc
                LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.BankAmount3))
                LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.BankAmount3), 0)
                LedgAry(I).Narration = mNarr
                LedgAry(I).ChqNo = PaymentRec.Chq_No3
                LedgAry(I).ChqDt = PaymentRec.Chq_Date3
                LedgAry(I).ClrChqDt = PaymentRec.Clg_Date3

                If bContraSub.Trim = "" Then bContraSub = PaymentRec.BankAc3

                If mPostTotalAmountYesNo Then
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)

                    LedgAry(I).SubCode = mPartyAc
                    LedgAry(I).ContraSub = bContraSub
                    LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.BankAmount3), 0)
                    LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.BankAmount3))
                    LedgAry(I).Narration = mNarr
                    LedgAry(I).ChqNo = PaymentRec.Chq_No3
                    LedgAry(I).ChqDt = PaymentRec.Chq_Date3
                    LedgAry(I).ClrChqDt = PaymentRec.Clg_Date3
                End If

            End If

            If Val(PaymentRec.CardAmount) > 0 Then
                mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.CardBank_Code & "'"
                mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

                mNarr = "Being Amount " & mTransStr & " Via Card No. """ & PaymentRec.Card_No & """ ,Card Bank " & mBankName & ""

                If bFlag = False Then
                    bFlag = True
                Else
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)
                End If

                LedgAry(I).SubCode = PaymentRec.CardAc
                LedgAry(I).ContraSub = mPartyAc
                LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.CardAmount))
                LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.CardAmount), 0)
                LedgAry(I).Narration = mNarr
                LedgAry(I).ChqNo = PaymentRec.Card_No

                If bContraSub.Trim = "" Then bContraSub = PaymentRec.CardAc

                If mPostTotalAmountYesNo Then
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)

                    LedgAry(I).SubCode = mPartyAc
                    LedgAry(I).ContraSub = bContraSub
                    LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.CardAmount), 0)
                    LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.CardAmount))
                    LedgAry(I).Narration = mNarr
                    LedgAry(I).ChqNo = PaymentRec.Card_No
                End If
            End If

            If Val(PaymentRec.AcTransferAmount) > 0 Then
                mQry = "Select Bank_Name From Bank With (NoLock) Where Bank_Code = '" & PaymentRec.AcTransferBank_Code & "'"
                mBankName = AgL.Dman_Execute(mQry, mConn, mCmd).ExecuteScalar()

                mNarr = "Being Amount Transferred Via Bank " & mBankName & ""

                If bFlag = False Then
                    bFlag = True
                Else
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)
                End If

                LedgAry(I).SubCode = PaymentRec.AcTransferBankAc
                LedgAry(I).ContraSub = mPartyAc
                LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.AcTransferAmount))
                LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.AcTransferAmount), 0)
                LedgAry(I).Narration = mNarr
                LedgAry(I).ChqNo = PaymentRec.AcTransferAcNo

                If bContraSub.Trim = "" Then bContraSub = PaymentRec.AcTransferBankAc

                If mPostTotalAmountYesNo Then
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)

                    LedgAry(I).SubCode = mPartyAc
                    LedgAry(I).ContraSub = bContraSub
                    LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.AcTransferAmount), 0)
                    LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.AcTransferAmount))
                    LedgAry(I).Narration = mNarr
                    LedgAry(I).ChqNo = PaymentRec.AcTransferAcNo
                End If
            End If


            If Val(PaymentRec.AdjustmentAmount) > 0 Then
                mNarr = TxtAdjustmentRemark.Text

                If bFlag = False Then
                    bFlag = True
                Else
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)
                End If

                LedgAry(I).SubCode = PaymentRec.AdjustmentAc
                LedgAry(I).ContraSub = mPartyAc
                LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.AdjustmentAmount))
                LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.AdjustmentAmount), 0)
                LedgAry(I).Narration = mNarr

                If bContraSub.Trim = "" Then bContraSub = PaymentRec.AdjustmentAc

                If mPostTotalAmountYesNo Then
                    I = UBound(LedgAry) + 1
                    ReDim Preserve LedgAry(I)

                    LedgAry(I).SubCode = mPartyAc
                    LedgAry(I).ContraSub = bContraSub
                    LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.AdjustmentAmount), 0)
                    LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.AdjustmentAmount))
                    LedgAry(I).Narration = mNarr
                End If
            End If


            'If mPostTotalAmountYesNo Then
            '    I = UBound(LedgAry) + 1
            '    ReDim Preserve LedgAry(I)

            '    mNarr = "Being Total Amount of Rs. " & Val(PaymentRec.TotalAmount) & " " & mTransStr & ""

            '    LedgAry(I).SubCode = mPartyAc
            '    LedgAry(I).ContraSub = bContraSub
            '    LedgAry(I).AmtDr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), Val(PaymentRec.TotalAmount), 0)
            '    LedgAry(I).AmtCr = IIf(AgL.StrCmp(mPartyDrCr, "Dr"), 0, Val(PaymentRec.TotalAmount))
            '    LedgAry(I).Narration = mNarr
            'End If

            If mPostFlag = True Then
                If AgL.LedgerPost(AgL.MidStr(mEntryMode, 0, 1), LedgAry, mConn, mCmd, mSearchCode, mV_Date, AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , mConnectionString) = False Then
                    AccountPosting = False
                    Err.Raise(1, "Error in Ledger Posting")
                End If
            End If
        Else
            If mPostFlag = True Then
                AgL.LedgerUnPost(mConn, mCmd, mSearchCode)
            End If
        End If
    End Function


    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Try
            If Not Data_Validation() Then Exit Sub

            With Me.PubPaymentRec
                '===========< Cash Detail>=====================
                .CashAc = TxtCashAc.AgSelectedValue
                .CashAmount = Val(TxtCashAmount.Text)

                '===========< Bank-1 Detail>=====================
                .BankAc = TxtBankAc.AgSelectedValue
                .BankAmount = Val(TxtBankAmount.Text)
                If TxtBank_Code.AgSelectedValue.Trim = "" Then
                    .Bank_Code = mBank_Code
                Else
                    .Bank_Code = TxtBank_Code.AgSelectedValue
                End If
                .Bank_Name = TxtBank_Code.Text
                .Chq_No = TxtChq_No.Text
                .Chq_Date = TxtChq_Date.Text
                .Clg_Date = TxtClg_Date.Text

                '===========< Bank-2 Detail>=====================
                .BankAc2 = TxtBankAc2.AgSelectedValue
                .BankAmount2 = Val(TxtBankAmount2.Text)
                If TxtBank_Code.AgSelectedValue.Trim = "" Then
                    .Bank_Code2 = mBank_Code2
                Else
                    .Bank_Code2 = TxtBank_Code2.AgSelectedValue
                End If
                .Bank_Name2 = TxtBank_Code2.Text
                .Chq_No2 = TxtChq_No2.Text
                .Chq_Date2 = TxtChq_Date2.Text
                .Clg_Date2 = TxtClg_Date2.Text

                '===========< Bank-3 Detail>=====================
                .BankAc3 = TxtBankAc3.AgSelectedValue
                .BankAmount3 = Val(TxtBankAmount3.Text)
                If TxtBank_Code3.AgSelectedValue.Trim = "" Then
                    .Bank_Code3 = mBank_Code3
                Else
                    .Bank_Code3 = TxtBank_Code3.AgSelectedValue
                End If
                .Bank_Name3 = TxtBank_Code3.Text
                .Chq_No3 = TxtChq_No3.Text
                .Chq_Date3 = TxtChq_Date3.Text
                .Clg_Date3 = TxtClg_Date3.Text

                '===========< Card Detail>=====================
                .CardAc = TxtCardAc.AgSelectedValue
                .CardAmount = Val(TxtCardAmount.Text)
                If TxtCardBank_Code.AgSelectedValue.Trim = "" Then
                    .CardBank_Code = mCardBank_Code
                Else
                    .CardBank_Code = TxtCardBank_Code.AgSelectedValue
                End If
                .CardBank_Name = TxtCardBank_Code.Text
                .Card_No = TxtCard_No.Text

                '===========< Account Transfer Detail>=====================
                .AcTransferBankAc = TxtAcTransferBankAc.AgSelectedValue
                .AcTransferAmount = Val(TxtAcTransferAmount.Text)
                If TxtAcTransferBank_Code.AgSelectedValue.Trim = "" Then
                    .AcTransferBank_Code = mAcTransferBank_Code
                Else
                    .AcTransferBank_Code = TxtAcTransferBank_Code.AgSelectedValue
                End If
                .AcTransferBank_Name = TxtAcTransferBank_Code.Text
                .AcTransferAcNo = TxtAcTransferAcNo.Text

                '===========< Adustment Detail>=====================
                .AdjustmentAc = TxtAdjustmentAc.AgSelectedValue
                .AdjustmentAmount = Val(TxtAdjustmentAmount.Text)
                .AdjustmentRemark = TxtAdjustmentRemark.Text

                .TotalAmount = Val(TxtTotalPayment.Text)
                .PartyDrCr = mPartyDrCr
            End With

            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillPaymentRec()
        Dim DsTemp As DataSet = Nothing

        mQry = "Select P.*, B1.Bank_Name , B2.Bank_Name  AS Bank_Name2, B3.Bank_Name AS Bank_Name3, " & _
                " Bat.Bank_Name AS AcTransferBank_Name, Bc.Bank_Name AS CardBank_Name  " & _
                " FROM PaymentDetail P " & _
                " LEFT JOIN Bank B1 ON P.Bank_Code = B1.Bank_Code " & _
                " LEFT JOIN Bank B2 ON P.Bank_Code2 = B2.Bank_Code " & _
                " LEFT JOIN Bank B3 ON P.Bank_Code3 = B3.Bank_Code " & _
                " LEFT JOIN Bank Bat ON P.AcTransferBank_Code = Bat.Bank_Code " & _
                " LEFT JOIN Bank Bc ON P.CardBank_Code  = Bc.Bank_Code " & _
                " Where DocId='" & mSearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                PubPaymentRec.PartyDrCr = AgL.XNull(.Rows(0)("PartyDrCr"))
                PubPaymentRec.CashAc = AgL.XNull(.Rows(0)("CashAc"))
                PubPaymentRec.CashAmount = Format(AgL.VNull(.Rows(0)("CashAmount")), "0.00")

                PubPaymentRec.BankAc = AgL.XNull(.Rows(0)("BankAc"))
                PubPaymentRec.BankAmount = Format(AgL.VNull(.Rows(0)("BankAmount")), "0.00")
                PubPaymentRec.Bank_Code = AgL.XNull(.Rows(0)("Bank_Code"))
                PubPaymentRec.Bank_Name = AgL.XNull(.Rows(0)("Bank_Name"))
                PubPaymentRec.Chq_No = AgL.XNull(.Rows(0)("Chq_No"))
                PubPaymentRec.Chq_Date = AgL.RetDate(AgL.XNull(.Rows(0)("Chq_Date")))
                PubPaymentRec.Clg_Date = AgL.RetDate(AgL.XNull(.Rows(0)("Clg_Date")))

                PubPaymentRec.BankAc2 = AgL.XNull(.Rows(0)("BankAc2"))
                PubPaymentRec.BankAmount2 = Format(AgL.VNull(.Rows(0)("BankAmount2")), "0.00")
                PubPaymentRec.Bank_Code2 = AgL.XNull(.Rows(0)("Bank_Code2"))
                PubPaymentRec.Bank_Name2 = AgL.XNull(.Rows(0)("Bank_Name2"))
                PubPaymentRec.Chq_No2 = AgL.XNull(.Rows(0)("Chq_No2"))
                PubPaymentRec.Chq_Date2 = AgL.RetDate(AgL.XNull(.Rows(0)("Chq_Date2")))
                PubPaymentRec.Clg_Date2 = AgL.RetDate(AgL.XNull(.Rows(0)("Clg_Date2")))

                PubPaymentRec.BankAc3 = AgL.XNull(.Rows(0)("BankAc3"))
                PubPaymentRec.BankAmount3 = Format(AgL.VNull(.Rows(0)("BankAmount3")), "0.00")
                PubPaymentRec.Bank_Code3 = AgL.XNull(.Rows(0)("Bank_Code3"))
                PubPaymentRec.Bank_Name3 = AgL.XNull(.Rows(0)("Bank_Name3"))
                PubPaymentRec.Chq_No3 = AgL.XNull(.Rows(0)("Chq_No3"))
                PubPaymentRec.Chq_Date3 = AgL.RetDate(AgL.XNull(.Rows(0)("Chq_Date3")))
                PubPaymentRec.Clg_Date3 = AgL.RetDate(AgL.XNull(.Rows(0)("Clg_Date3")))

                PubPaymentRec.CardAc = AgL.XNull(.Rows(0)("CardAc"))
                PubPaymentRec.CardAmount = Format(AgL.VNull(.Rows(0)("CardAmount")), "0.00")
                PubPaymentRec.CardBank_Code = AgL.XNull(.Rows(0)("CardBank_Code"))
                PubPaymentRec.CardBank_Name = AgL.XNull(.Rows(0)("CardBank_Name"))
                PubPaymentRec.Card_No = AgL.XNull(.Rows(0)("Card_No"))

                PubPaymentRec.AcTransferBankAc = AgL.XNull(.Rows(0)("AcTransferBankAc"))
                PubPaymentRec.AcTransferAmount = Format(AgL.VNull(.Rows(0)("AcTransferAmount")), "0.00")
                PubPaymentRec.AcTransferBank_Code = AgL.XNull(.Rows(0)("AcTransferBank_Code"))
                PubPaymentRec.AcTransferBank_Name = AgL.XNull(.Rows(0)("AcTransferBank_Name"))
                PubPaymentRec.AcTransferAcNo = AgL.XNull(.Rows(0)("AcTransferAcNo"))

                PubPaymentRec.TotalAmount = Format(AgL.VNull(.Rows(0)("TotalAmount")), "0.00")
            Else
                PubPaymentRec = Nothing
            End If
        End With
        DsTemp = Nothing
    End Sub

End Class
