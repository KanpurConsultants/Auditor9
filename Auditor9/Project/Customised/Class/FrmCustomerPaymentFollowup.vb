Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmCustomerPaymentFollowup


    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Select As String = "Tick"
    Public Const Col1DocId As String = "DocID"
    Public Const Col1V_Sno As String = "V_SNo"
    Public Const Col1DocNo As String = "Doc.No"
    Public Const Col1DocDate As String = "Doc.Date"
    Public Const Col1PartyName As String = "Party Name"
    Public Const Col1BillAmount As String = "Bill Amount"
    Public Const Col1BalanceAmount As String = "Balance Amount"
    Public Const Col1FollowupDate As String = "Followup Date"
    Public Const Col1FollowupRemarks As String = "Followup Remark"
    Public Const Col1FollowupNextDate As String = "Followup Next Date"
    Public Const Col1FollowupAmount As String = "Followup Amount"
    Public Const Col1FollowupCommittedAmount As String = "Followup Committed Amount"
    Public Const Col1OldAmount As String = "Old Amount"
    Public Const Col1OldCommittedAmount As String = "Old Committed Amount"
    Public Const Col1OldAdjustedAmount As String = "Old Adjusted Amount"
    Public Const Col1OldDate As String = "Old Date"
    Public Const Col1OldRemark As String = "Old Remark"
    Public Const Col1OldBalance As String = "Old Balance"
    Public Const Col1NewBalance As String = "New Balance"
    Public Const Col1CurrentBalance As String = "Current Balance"
    Public Const Col1FollowupType As String = "Followup Type"
    Public Const Col1Followups As String = "Followup Count"
    Public Const Col1UnableToConnect As String = "UnableToConnect"
    Public Const Col1UnableToConnectDate As String = "UnableToConnectDate"

    Public WithEvents DglMonthWiseDetail As New AgControls.AgDataGrid
    Public Const ColMonthWise_Month As String = "Month"
    Public Const ColMonthWise_BillAmount As String = "Bill Amount"
    Public Const ColMonthWise_BalanceAmount As String = "Balance Amount"
    Public Const ColMonthWise_Followups As String = "Followup Count"



    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"



    Public Const rowUnableToConnect As Integer = 0
    Public Const rowNewRemark As Integer = 1
    Public Const rowNextDate As Integer = 2
    Public Const rowNewCommittedAmount As Integer = 3
    Public Const rowOldRemark As Integer = 4
    Public Const rowOldDate As Integer = 5
    Public Const rowOldAmount As Integer = 6
    Public Const rowOldCommittedAmount As Integer = 7
    Public Const rowOldAdjusted As Integer = 8
    Public Const rowOldBalance As Integer = 9
    Public Const rowNewBalance As Integer = 10
    Public Const rowTotalDueBalance As Integer = 11
    Public Const rowCurrentBalance As Integer = 12
    Public Const rowCurrentBalancePakka As Integer = 13


    Public Const hcUnableToConnect As String = "Unable To Connect"
    Public Const hcNewRemark As String = "New Remark"
    Public Const hcNextDate As String = "Next Date"
    Public Const hcNewCommittedAmount As String = "New Committed Amount"
    Public Const hcOldCommittedStatus As String = "Old Committed Status"
    Public Const hcOldRemark As String = "Old Remark"
    Public Const hcOldDate As String = "Old Date"
    Public Const hcOldAmount As String = "Old Amount"
    Public Const hcOldCommittedAmount As String = "Old Committed Amount"
    Public Const hcOldReceipt As String = "Old Receipt"
    Public Const hcOldAdjusted As String = "Old Adjusted"
    Public Const hcOldBalance As String = "Old Balance"
    Public Const hcNewBalance As String = "New Balance"
    Public Const hcTotalDueBalance As String = "Total Due Balance"
    Public Const hcCurrentBalance As String = "Current Balance"
    Public Const hcCurrentBalancePakka As String = "Ledger Balance"

    Dim mSearchCode As String

    Dim mMainCondStr As New structCondStr

    Dim mEntryMode$ = ""
    Dim mFollowupType As String = "Payment"


    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property


    Public Property MainCondStr() As structCondStr
        Get
            MainCondStr = mMainCondStr
        End Get
        Set(ByVal value As structCondStr)
            mMainCondStr = value
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

    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1DocId, 160, 255, Col1DocId, False, False)
            .AddAgTextColumn(Dgl1, Col1V_Sno, 160, 255, Col1V_Sno, False, False)
            .AddAgTextColumn(Dgl1, Col1DocNo, 120, 255, Col1DocNo, True, False)
            .AddAgTextColumn(Dgl1, Col1DocDate, 120, 255, Col1DocDate, True, False)
            .AddAgTextColumn(Dgl1, Col1PartyName, 100, 255, Col1PartyName, False, False)
            .AddAgNumberColumn(Dgl1, Col1BillAmount, 120, 9, 2, False, Col1BillAmount, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1BalanceAmount, 120, 9, 2, False, Col1BalanceAmount, True, False, True)
            .AddAgTextColumn(Dgl1, Col1FollowupDate, 100, 255, Col1FollowupDate, False, False)
            .AddAgTextColumn(Dgl1, Col1FollowupRemarks, 100, 255, Col1FollowupRemarks, False, False)
            .AddAgTextColumn(Dgl1, Col1FollowupNextDate, 100, 255, Col1FollowupNextDate, False, False)
            .AddAgNumberColumn(Dgl1, Col1FollowupAmount, 100, 9, 2, False, Col1FollowupAmount, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1FollowupCommittedAmount, 100, 9, 2, False, Col1FollowupCommittedAmount, False, False, True)
            .AddAgTextColumn(Dgl1, Col1FollowupType, 100, 255, Col1FollowupType, False, False)
            .AddAgTextColumn(Dgl1, Col1Followups, 100, 255, Col1Followups, True, False)
            .AddAgTextColumn(Dgl1, Col1OldAmount, 100, 255, Col1OldAmount, False, False)
            .AddAgTextColumn(Dgl1, Col1OldCommittedAmount, 100, 255, Col1OldCommittedAmount, False, False)
            .AddAgTextColumn(Dgl1, Col1OldAdjustedAmount, 100, 255, Col1OldAdjustedAmount, False, False)
            .AddAgTextColumn(Dgl1, Col1OldDate, 100, 255, Col1OldDate, False, False)
            .AddAgTextColumn(Dgl1, Col1OldRemark, 100, 255, Col1OldRemark, False, False)
            .AddAgTextColumn(Dgl1, Col1OldBalance, 100, 255, Col1OldBalance, False, False)
            .AddAgTextColumn(Dgl1, Col1NewBalance, 100, 255, Col1NewBalance, False, False)
            .AddAgTextColumn(Dgl1, Col1CurrentBalance, 100, 255, Col1CurrentBalance, False, False)
            .AddAgTextColumn(Dgl1, Col1UnableToConnect, 100, 255, Col1UnableToConnect, False, False)
            .AddAgTextColumn(Dgl1, Col1UnableToConnectDate, 100, 255, Col1UnableToConnectDate, False, False)

        End With
        AgL.AddAgDataGrid(Dgl1, PnlBillWiseDetail)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.ReadOnly = True
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl2, Col1Head, 200, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl2, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl2, Col1Value, 350, 255, Col1Value, True, False)
            .AddAgTextColumn(Dgl2, Col1HeadOriginal, 0, 255, Col1HeadOriginal, False, False)
        End With
        AgL.AddAgDataGrid(Dgl2, PNL1)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl2.ColumnHeadersHeight = 35
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.AllowUserToAddRows = False
        Dgl2.RowHeadersVisible = False
        Dgl2.Name = "Dgl2"
        AgL.GridDesign(Dgl2)


        Dgl2.Rows.Add(14)
        Dgl2.Item(Col1Head, rowUnableToConnect).Value = hcUnableToConnect
        Dgl2.Item(Col1Head, rowNewRemark).Value = hcNewRemark
        Dgl2.Item(Col1Head, rowNextDate).Value = hcNextDate
        Dgl2.Item(Col1Head, rowNewCommittedAmount).Value = hcNewCommittedAmount
        Dgl2.Item(Col1Head, rowOldRemark).Value = hcOldRemark
        Dgl2.Item(Col1Head, rowOldDate).Value = hcOldDate
        Dgl2.Item(Col1Head, rowOldAmount).Value = hcOldAmount
        Dgl2.Item(Col1Head, rowOldCommittedAmount).Value = hcOldCommittedAmount
        Dgl2.Item(Col1Head, rowOldAdjusted).Value = hcOldAdjusted
        Dgl2.Item(Col1Head, rowOldBalance).Value = hcOldBalance
        Dgl2.Item(Col1Head, rowNewBalance).Value = hcNewBalance
        Dgl2.Item(Col1Head, rowTotalDueBalance).Value = hcTotalDueBalance
        Dgl2.Item(Col1Head, rowCurrentBalance).Value = hcCurrentBalance
        Dgl2.Item(Col1Head, rowCurrentBalancePakka).Value = hcCurrentBalancePakka
        If Not ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            Dgl2.Rows(rowCurrentBalancePakka).Visible = False
        End If

        For I As Integer = 0 To Dgl2.Rows.Count - 1
            Select Case I
                Case rowOldDate, rowOldAmount, rowNewBalance, rowOldAdjusted, rowOldBalance, rowTotalDueBalance, rowOldRemark, rowOldCommittedAmount, rowCurrentBalance, rowCurrentBalancePakka
                    Dgl2.Item(Col1Value, I).Style.BackColor = Color.MistyRose
            End Select
        Next



        DglMonthWiseDetail.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMonthWiseDetail, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMonthWiseDetail, ColMonthWise_Month, 100, 255, ColMonthWise_Month, True, False)
            .AddAgNumberColumn(DglMonthWiseDetail, ColMonthWise_BillAmount, 120, 9, 2, False, ColMonthWise_BillAmount, True, False, True)
            .AddAgNumberColumn(DglMonthWiseDetail, ColMonthWise_BalanceAmount, 120, 9, 2, False, ColMonthWise_BalanceAmount, True, False, True)
            .AddAgTextColumn(DglMonthWiseDetail, ColMonthWise_Followups, 100, 255, ColMonthWise_Followups, True, False)
        End With
        AgL.AddAgDataGrid(DglMonthWiseDetail, PnlMonthWiseDetail)
        DglMonthWiseDetail.EnableHeadersVisualStyles = False
        DglMonthWiseDetail.ColumnHeadersHeight = 35
        DglMonthWiseDetail.AgSkipReadOnlyColumns = True
        DglMonthWiseDetail.AllowUserToOrderColumns = True
        DglMonthWiseDetail.AllowUserToAddRows = False
        DglMonthWiseDetail.ReadOnly = True
        AgCL.GridSetiingShowXml(Me.Text & DglMonthWiseDetail.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMonthWiseDetail, False)
    End Sub

    Public Structure structCondStr
        Public GroupOn As String
        Public BillsUptoDate As String
        Public DueUptoDate As String
        Public PartyCodes As String
        Public MasterPartyCodes As String
        Public LinkedPartyCodes As String
        Public Div_Code As String
        Public Site_Code As String
    End Structure
    Private Sub CreateTemporaryTables()
        Try
            mQry = "Drop Table #FifoOutstanding"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Catch ex As Exception
        End Try


        mQry = " CREATE Temporary TABLE #FifoOutstanding 
                    (DocId  nvarchar(21), 
                    V_Sno Int,
                    V_type  nvarchar(20),
                    RecId  nvarchar(50), 
                    V_Date  DateTime,
                    Site_Code  nvarchar(2), 
                    Div_Code nVarchar(1),                         
                    Subcode nvarchar(10),
                    BillAmount Float, 
                    BalanceAmount Float,
                    DrCr nVarchar(10),                  
                    Narration  varchar(2000),
                    FollowupCount Int
                    ); "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

    End Sub


    Public Sub FillPending(condStruct As structCondStr)
        Dim mCondStr As String
        Dim DsHeader As DataSet

        CreateTemporaryTables()


        mCondStr = " And Sg.Nature In ('Customer') "
        mCondStr = mCondStr & " AND Date(LG.V_Date) <= (Case 
                                                        When Sg.Nature='Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(condStruct.BillsUptoDate) & " 
                                                        When Sg.Nature='Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(condStruct.DueUptoDate) & " 
                                                        When Sg.Nature<>'Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(condStruct.DueUptoDate) & " 
                                                        When Sg.Nature<>'Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(condStruct.BillsUptoDate) & " 
                                                        End) "

        If condStruct.PartyCodes <> "" Then
            mCondStr = mCondStr & " And SG.Subcode In ('" & condStruct.PartyCodes.Replace(",", "','") & "') "
        End If

        If condStruct.MasterPartyCodes <> "" Then
            mCondStr = mCondStr & " And SG.Subcode In ('" & condStruct.MasterPartyCodes.Replace(",", "','") & "') "
        End If

        If condStruct.LinkedPartyCodes <> "" Then
            mCondStr = mCondStr & " And LG.LinkedSubcode In ('" & condStruct.LinkedPartyCodes.Replace(",", "','") & "') "
        End If

        If condStruct.Site_Code <> "" Then
            mCondStr = mCondStr & " And LG.Site_Code In (" & condStruct.Site_Code.Replace("''", "'") & ") "
        End If

        If condStruct.Div_Code <> "" Then
            mCondStr = mCondStr & " And LG.DivCode In (" & condStruct.Div_Code.Replace("''", "'") & ") "
        End If


        mCondStr = mCondStr & " And LG.DocID || LG.V_SNo Not In (SELECT H.PurchaseInvoiceDocId || IfNull(H.PurchaseInvoiceDocIdSr,'')   FROM Cloth_SupplierSettlementInvoices H
                                                                       UNION ALL 
                                                                       SELECT H.PaymentDocId || IfNull(H.PaymentDocIdSr,'')   FROM Cloth_SupplierSettlementPayments H
                                                                       ) "
        If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('WPS','WRS') ) "
        Else
            mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('" & Ncat.PaymentSettlement & "','" & Ncat.ReceiptSettlement & "') ) "
        End If



        Dim mRemainingBalance As Double
        Dim i As Integer, j As Integer
        Dim dtParty As DataTable

        'Dim DtMain As DataTable
        Dim BalAmount As Double
        Dim DrCr As String
        Dim dtCr As DataTable
        Dim drowCr As DataRow()




        mQry = "Select Sg.Subcode, Max(Sg.Nature) as Nature, Sum(Lg.AmtDr)-Sum(Lg.AmtCr) as Balance
                            From Ledger Lg "
        If condStruct.GroupOn = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
        End If
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Where  Sg.Nature='Customer' "
        mQry = mQry & mCondStr
        mQry = mQry & " Group By Sg.Subcode"
        mQry = mQry & " Having Sum(Lg.AmtDr)-Sum(Lg.AmtCr) <> 0 "


        dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select Lg.DocID, LG.V_SNo, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtDr as Amount                                
                                    From Ledger Lg  With (NoLock) "
        If condStruct.GroupOn = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(LG.LinkedSubcode,LG.SubCode) "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
        End If
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Lg.AmtDr > 0 " & mCondStr & " 
                                    Order By Sg.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
        dtCr = AgL.FillData(mQry, AgL.GCn).Tables(0)


        If dtParty.Rows.Count > 0 Then
            For i = 0 To dtParty.Rows.Count - 1
                mQry = ""
                If AgL.XNull(dtParty.Rows(i)("Nature")) = "Customer" Then
                    If AgL.VNull(dtParty.Rows(i)("Balance")) > 0 Then
                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtDr as Amount                                
                                    From Ledger Lg  With (NoLock) "
                        mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode  "
                        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
                                    Where Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  
                                    And Lg.AmtDr > 0  " & mCondStr & "                               
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
                    End If
                Else
                    If AgL.VNull(dtParty.Rows(i)("Balance")) < 0 Then
                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtCr as Amount                                
                                    From Ledger Lg  With (NoLock) "
                        mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
                        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtCr > 0 " & mCondStr & " 
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
                    Else
                        mQry = ""
                    End If
                End If


                BalAmount = 0 : DrCr = ""
                mRemainingBalance = Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))
                If mQry <> "" Then
                    'DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    drowCr = dtCr.Select("Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'", " V_Date Desc ")
                    If drowCr.Length > 0 Then
                        For j = 0 To drowCr.Length - 1

                            If mRemainingBalance > 0 Then

                                If mRemainingBalance > AgL.VNull(drowCr(j)("Amount")) Then
                                    BalAmount = Format(AgL.VNull(drowCr(j)("Amount")), "0.00")
                                    mRemainingBalance = mRemainingBalance - AgL.VNull(drowCr(j)("Amount"))
                                Else
                                    BalAmount = Format(mRemainingBalance, "0.00")
                                    mRemainingBalance = mRemainingBalance - mRemainingBalance
                                End If
                                DrCr = IIf(AgL.VNull(dtParty.Rows(i)("Balance")) > 0, "Dr", "Cr")


                                mQry = "Insert Into #FifoOutstanding
                                                (DocID, V_SNo, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode, BillAmount,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(" & AgL.Chk_Text(AgL.XNull(drowCr(j)("DocID"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("V_Sno"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("V_Type"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("RecID"))) & ",
                                                " & AgL.Chk_Date(AgL.XNull(drowCr(j)("V_Date"))) & ",                                            
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Site_Code"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("DivCode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Subcode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Amount"))) & ",
                                                " & BalAmount & ",
                                                " & AgL.Chk_Text(DrCr) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Narration"))) & "
                                                )
                                                "

                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            End If
                        Next
                    End If
                End If
            Next
        End If
        mQry = "Select  'o' As Tick, H.DocID, H.V_SNo,H.Div_code || H.Site_Code || '-'  || H.V_Type || '-' || H.RecID as EntryNo, strftime('%d-%m-%Y',H.V_Date) as EntryDate, 
                S.Name as Site, Sg.Subcode as PartyCode, Sg.DispName as PartyName, bSg.Name as MasterPartyName,  H.BillAmount, H.BalanceAmount,  
                strftime('%d-%m-%Y',Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day'))  as DueDate, Fup.FollowupType,
                Fup.FollowupAmount, Fup.FollowupCommittedAmount, Fup.FollowupDate, Fup.FollowupRemark, Fup.NextFollowupDate, IfNull(FupH.FollowupCount,0) + (Case When Fup.FollowupDate Is Not Null Then 1 Else 0 End) as FollowUpCount,
                Fup.OldAmount, Fup.OldCommittedAmount, Fup.OldAdjustedAmount, Fup.OldDate, Fup.OldRemark, Fup.OldBalance, Fup.NewBalance, Fup.CurrentBalance, Fup.UnableToConnect, Fup.UnableToConnectDate
                from #FifoOutstanding H
                Left Join SiteMast S On H.Site_Code = S.Code
                Left Join Division D On H.Div_code = D.Div_Code
                Left Join Subgroup Sg On H.Subcode = Sg.Subcode                 
                Left Join viewHelpSubgroup bsg on Sg.Parent = bsg.code
                Left Join Subgroup P On Sg.Parent = P.Subcode 
                Left Join SaleInvoice PI On H.DocID = PI.DocId
                Left Join LedgerPaymentFollowup Fup on H.DocID = Fup.DocID and H.V_SNo = Fup.V_SNo
                Left Join (Select FupH.DocID, FupH.V_Sno, Count(*) as FollowupCount
                           From   LedgerPaymentFollowupHistory FupH 
                           Group By FupH.DocID, FupH.V_SNo 
                          ) as FupH On  H.DocID = FupH.DocID and H.V_SNo = FupH.V_SNo
                Where Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') <= " & AgL.Chk_Date(condStruct.DueUptoDate) & " 
                And H.BalanceAmount>0
                Order By Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') "
        DsHeader = AgL.FillData(mQry, AgL.GCn)
        With DsHeader.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For i = 0 To DsHeader.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, i).Value = Dgl1.Rows.Count - 1
                    'Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1DocId, i).Value = AgL.XNull(.Rows(i)("DocID"))
                    Dgl1.Item(Col1V_Sno, i).Value = AgL.XNull(.Rows(i)("V_SNo"))
                    Dgl1.Item(Col1DocDate, i).Value = AgL.XNull(.Rows(i)("EntryDate"))
                    Dgl1.Item(Col1DocNo, i).Value = AgL.XNull(.Rows(i)("EntryNo"))
                    Dgl1.Item(Col1PartyName, i).Tag = AgL.XNull(.Rows(i)("PartyCode"))
                    Dgl1.Item(Col1PartyName, i).Value = AgL.XNull(.Rows(i)("PartyName"))
                    Dgl1.Item(Col1BillAmount, i).Value = AgL.VNull(.Rows(i)("BillAmount"))
                    Dgl1.Item(Col1BalanceAmount, i).Value = AgL.VNull(.Rows(i)("BalanceAmount"))
                    Dgl1.Item(Col1FollowupAmount, i).Value = Format(Math.Abs(AgL.VNull(.Rows(i)("FollowupAmount"))), "0.00")
                    Dgl1.Item(Col1FollowupCommittedAmount, i).Value = Format(Math.Abs(AgL.VNull(.Rows(i)("FollowupCommittedAmount"))), "0.00")
                    Dgl1.Item(Col1FollowupDate, i).Value = AgL.XNull(.Rows(i)("FollowupDate"))
                    Dgl1.Item(Col1FollowupNextDate, i).Value = AgL.XNull(.Rows(i)("NextFollowupDate"))
                    Dgl1.Item(Col1FollowupRemarks, i).Value = AgL.XNull(.Rows(i)("FollowupRemark"))
                    Dgl1.Item(Col1Followups, i).Value = AgL.VNull(.Rows(i)("FollowupCount"))
                    Dgl1.Item(Col1OldAmount, i).Value = AgL.VNull(.Rows(i)("OldAmount"))
                    Dgl1.Item(Col1OldCommittedAmount, i).Value = AgL.VNull(.Rows(i)("OldCommittedAmount"))
                    Dgl1.Item(Col1OldAdjustedAmount, i).Value = AgL.VNull(.Rows(i)("OldAdjustedAmount"))
                    Dgl1.Item(Col1OldDate, i).Value = AgL.XNull(.Rows(i)("OldDate"))
                    Dgl1.Item(Col1OldRemark, i).Value = AgL.XNull(.Rows(i)("OldRemark"))
                    Dgl1.Item(Col1OldBalance, i).Value = AgL.VNull(.Rows(i)("OldBalance"))
                    Dgl1.Item(Col1NewBalance, i).Value = AgL.VNull(.Rows(i)("NewBalance"))
                    Dgl1.Item(Col1CurrentBalance, i).Value = AgL.VNull(.Rows(i)("CurrentBalance"))
                    Dgl1.Item(Col1UnableToConnect, i).Value = AgL.XNull(.Rows(i)("UnableToConnect"))
                    Dgl1.Item(Col1UnableToConnectDate, i).Value = AgL.XNull(.Rows(i)("UnableToConnectDate"))
                Next i
            End If
        End With
        Calculation()

    End Sub


    Public Sub FillMonthWiseDetail(condStruct As structCondStr)
        Dim I As Integer = 0
        Dim mCondStr As String
        Dim DsHeader As DataSet

        mQry = "Select  'o' As Tick, H.DocID, H.V_SNo,H.Div_code || H.Site_Code || '-'  || H.V_Type || '-' || H.RecID as EntryNo, strftime('%d-%m-%Y',H.V_Date) as EntryDate, 
                S.Name as Site, Sg.Subcode as PartyCode, Sg.DispName as PartyName, bSg.Name as MasterPartyName,  H.BillAmount, H.BalanceAmount,  
                strftime('%d-%m-%Y',Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day'))  as DueDate, Fup.FollowupType,
                Fup.FollowupAmount, Fup.FollowupCommittedAmount, Fup.FollowupDate, Fup.FollowupRemark, Fup.NextFollowupDate, IfNull(FupH.FollowupCount,0) + (Case When Fup.FollowupDate Is Not Null Then 1 Else 0 End) as FollowUpCount,
                Fup.OldAmount, Fup.OldCommittedAmount, Fup.OldAdjustedAmount, Fup.OldDate, Fup.OldRemark, Fup.OldBalance, Fup.NewBalance, Fup.CurrentBalance, Fup.UnableToConnect, Fup.UnableToConnectDate
                from #FifoOutstanding H
                Left Join SiteMast S On H.Site_Code = S.Code
                Left Join Division D On H.Div_code = D.Div_Code 
                Left Join Subgroup Sg On H.Subcode = Sg.Subcode                 
                Left Join viewHelpSubgroup bsg on Sg.Parent = bsg.code
                Left Join Subgroup P On Sg.Parent = P.Subcode 
                Left Join SaleInvoice PI On H.DocID = PI.DocId
                Left Join LedgerPaymentFollowup Fup on H.DocID = Fup.DocID and H.V_SNo = Fup.V_SNo
                Left Join (Select FupH.DocID, FupH.V_Sno, Count(*) as FollowupCount
                           From   LedgerPaymentFollowupHistory FupH 
                           Group By FupH.DocID, FupH.V_SNo 
                          ) as FupH On  H.DocID = FupH.DocID and H.V_SNo = FupH.V_SNo
                Where Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') <= " & AgL.Chk_Date(condStruct.DueUptoDate) & " 
                And H.BalanceAmount>0
                Order By Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') "


        Dim DsAgeing As New DataSet


        If condStruct.PartyCodes <> "" Then
            If condStruct.GroupOn = "Linked Party" Then
                mCondStr = mCondStr & " And IfNull(Lg.LinkedSubcode,LG.Subcode) In ('" & condStruct.PartyCodes.Replace(",", "','") & "') "
            Else
                mCondStr = mCondStr & " And LG.Subcode In ('" & condStruct.PartyCodes.Replace(",", "','") & "') "
            End If

        End If

        DsHeader = FillFifoOutstanding(mCondStr, "Interest Ledger")
        'DsAgeing = FillFifoOutstanding(CreateCondStr, "Interest Ledger")

        'mQry = " Select 'Jan/2021' As Month, 0 As BillAmount, 
        '        0 As BalanceAmount, 0 As FollowupCount "
        'DsHeader = AgL.FillData(mQry, AgL.GCn)
        With DsHeader.Tables(0)
            DglMonthWiseDetail.RowCount = 1
            DglMonthWiseDetail.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsHeader.Tables(0).Rows.Count - 1
                    DglMonthWiseDetail.Rows.Add()
                    DglMonthWiseDetail.Item(ColSNo, I).Value = DglMonthWiseDetail.Rows.Count - 1
                    DglMonthWiseDetail.Item(ColMonthWise_Month, I).Value = AgL.XNull(.Rows(I)("Month"))
                    DglMonthWiseDetail.Item(ColMonthWise_BillAmount, I).Value = AgL.VNull(.Rows(I)("BillAmount"))
                    DglMonthWiseDetail.Item(ColMonthWise_BalanceAmount, I).Value = AgL.VNull(.Rows(I)("BalanceAmount"))
                    DglMonthWiseDetail.Item(ColMonthWise_Followups, I).Value = AgL.VNull(.Rows(I)("FollowupCount"))
                Next i
            End If
        End With
        Calculation()
    End Sub
    Public Sub FMoverec(DocID As String)
        Dim dtTemp As DataTable
        Dim I As Integer
        Dim mQry As String



        FillPending(mMainCondStr)
        FillMonthWiseDetail(mMainCondStr)

        Dim mTotalOldAmount As Double = 0
        Dim mTotalNewAmount As Double = 0
        Dim mTotalAmount As Double = 0
        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1FollowupRemarks, 0).Value <> "" Or Dgl1.Item(Col1FollowupDate, 0).Value <> "" Then
                mTotalOldAmount = mTotalOldAmount + Val(Dgl1.Item(Col1BalanceAmount, I).Value)
            Else
                mTotalNewAmount = mTotalNewAmount + Val(Dgl1.Item(Col1BalanceAmount, I).Value)
            End If
            mTotalAmount = mTotalAmount + Val(Dgl1.Item(Col1BalanceAmount, I).Value)
        Next
        Dgl2.Item(Col1Value, rowNewBalance).Value = Format(mTotalNewAmount, "0.00")
        Dgl2.Item(Col1Value, rowOldBalance).Value = Format(mTotalOldAmount, "0.00")
        Dgl2.Item(Col1Value, rowTotalDueBalance).Value = Format(mTotalAmount, "0.00")

        If Dgl1.Rows.Count > 0 Then
            If Dgl1.Item(Col1FollowupRemarks, 0).Value <> "" Or Dgl1.Item(Col1FollowupDate, 0).Value <> "" Then
                Dgl2.Item(Col1Value, rowOldRemark).Value = Dgl1.Item(Col1FollowupRemarks, 0).Value
                Dgl2.Item(Col1Value, rowOldAmount).Value = Dgl1.Item(Col1FollowupAmount, 0).Value
                Dgl2.Item(Col1Value, rowOldCommittedAmount).Value = Dgl1.Item(Col1FollowupCommittedAmount, 0).Value
                Dgl2.Item(Col1Value, rowOldAdjusted).Value = Format(Val(Dgl1.Item(Col1FollowupAmount, 0).Value) - Val(Dgl2.Item(Col1Value, rowOldBalance).Value), "0.00")
                Dgl2.Item(Col1Value, rowOldDate).Value = Dgl1.Item(Col1FollowupDate, 0).Value
            End If
        End If

        Dim mDbPath As String
        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        Try
            AgL.Dman_ExecuteNonQry(" attach '" & mDbPath & "' as ODB", AgL.GCn)
        Catch ex As Exception
        End Try

        If mMainCondStr.GroupOn = "Linked Party" Then
            Dgl2.Item(Col1Value, rowCurrentBalance).Value = AgL.Dman_Execute("Select Sum(AmtDr-AmtCr)  as Balance From Ledger ODBL Where IfNull(LinkedSubcode,Subcode) In ('" & mMainCondStr.PartyCodes.Replace(",", "','") & "') ", AgL.GCn).ExecuteScalar()
            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                Dgl2.Item(Col1Value, rowCurrentBalancePakka).Value = AgL.Dman_Execute("Select Sum(AmtDr-AmtCr)  as Balance From ODB.Ledger ODBL Where IfNull(LinkedSubcode,Subcode) In (Select OmsID From Subgroup Where Subcode In  ('" & mMainCondStr.PartyCodes.Replace(",", "','") & "')) ", AgL.GCn).ExecuteScalar()
            End If
        Else
            Dgl2.Item(Col1Value, rowCurrentBalance).Value = AgL.Dman_Execute("Select Sum(AmtDr-AmtCr)  as Balance From Ledger ODBL Where Subcode In ('" & mMainCondStr.PartyCodes.Replace(",", "','") & "') ", AgL.GCn).ExecuteScalar()
            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                Dgl2.Item(Col1Value, rowCurrentBalancePakka).Value = AgL.Dman_Execute("Select Sum(AmtDr-AmtCr)  as Balance From ODB.Ledger ODBL Where Subcode In (Select OmsID From Subgroup Where Subcode In  ('" & mMainCondStr.PartyCodes.Replace(",", "','") & "')) ", AgL.GCn).ExecuteScalar()
            End If

        End If


        Calculation()
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            AgL.GridDesign(DglMonthWiseDetail)

            'Me.Top = 400
            'Me.Left = 400
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Dgl2_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Dim mQry As String
        Try
            'If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl2.CurrentCell Is Nothing Then Exit Sub
            If Dgl2.CurrentCell.ColumnIndex <> Dgl2.Columns(Col1Value).Index Then Exit Sub
            Dgl2.AgHelpDataSet(Dgl2.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            Select Case Dgl2.CurrentCell.RowIndex
                Case rowNewRemark
                    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 100
                Case rowNextDate
                    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                Case rowNewCommittedAmount
                    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 9
                    CType(Dgl2.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                Case rowUnableToConnect
                    mQry = " Select 'N/A' as Code, 'N/A' as Description "
                    mQry += " Union All Select 'Phone Not Picked' as Code, 'Phone Not Picked' as Description "
                    mQry += " Union All Select 'Phone Out of Reach' as Code, 'Phone Out of Reach' as Description "
                    mQry += " Union All Select 'Phone Busy' as Code, 'Phone Busy' as Description "
                    Dgl2.AgHelpDataSet(Col1Value) = AgL.FillData(mQry, AgL.GCn)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing

            Select Case Dgl1.CurrentCell.RowIndex




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

            If e.KeyCode = Keys.Enter Then Exit Sub
            If mEntryMode = "Browse" Then Exit Sub


            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1DocDate
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1DocDate) Is Nothing Then
                            mQry = "Select Code, Description from ItemGroup Order By Description"
                            Dgl1.AgHelpDataSet(Col1DocDate) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1DocNo
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1DocNo) Is Nothing Then
                            mQry = "Select Code, Description from ItemCategory Order By Description"
                            Dgl1.AgHelpDataSet(Col1DocNo) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1PartyName
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1PartyName) Is Nothing Then
                            mQry = ClsMain.GetStringsFromClassConstants(GetType(DiscountCalculationPattern))
                            Dgl1.AgHelpDataSet(Col1PartyName) = AgL.FillData(mQry, AgL.GCn)
                        End If
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
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub
    Public Sub Calculation()
        Dim I As Integer
        Dim mTotalNewAmount







    End Sub
    Public Sub FSave()
        Dim I As Integer
        Dim mTrans As String

        Try

            If AgL.XNull(Dgl2.Item(Col1Value, rowUnableToConnect).Value).ToString.ToUpper = "N/A" Then
                If Dgl2.Item(Col1Value, rowNewRemark).Value = "" Then
                    MsgBox("Remark can not be blank")
                    Dgl2.CurrentCell = Dgl2(Col1Value, rowNewRemark)
                    Dgl2.Focus()
                    Exit Sub
                End If
            End If


            If Dgl2.Item(Col1Value, rowNewRemark).Value <> "" Then
                If AgL.XNull(Dgl2.Item(Col1Value, rowUnableToConnect).Value).ToString.ToUpper = "" Then
                    MsgBox("Unable to connect can not be blank")
                    Dgl2.CurrentCell = Dgl2(Col1Value, rowUnableToConnect)
                    Dgl2.Focus()
                    Exit Sub
                End If


                If AgL.XNull(Dgl2.Item(Col1Value, rowNextDate).Value).ToString.ToUpper = "" Then
                    MsgBox("Next date can not be blank")
                    Dgl2.CurrentCell = Dgl2(Col1Value, rowNextDate)
                    Dgl2.Focus()
                    Exit Sub
                End If


                If DateDiff(DateInterval.Day, CDate(AgL.PubLoginDate), AgL.XNull(Dgl2.Item(Col1Value, rowNextDate).Value)) > 365 Then
                    MsgBox("Next date can not exceed 365 days")
                    Dgl2.CurrentCell = Dgl2(Col1Value, rowNextDate)
                    Dgl2.Focus()
                    Exit Sub
                End If

                If DateDiff(DateInterval.Day, CDate(AgL.PubLoginDate), AgL.XNull(Dgl2.Item(Col1Value, rowNextDate).Value)) > 15 Then
                    If MsgBox("Next date is exceeding 15 days do you want to continue?", vbYesNo) = MsgBoxResult.No Then
                        Dgl2.CurrentCell = Dgl2(Col1Value, rowNextDate)
                        Dgl2.Focus()
                        Exit Sub
                    End If
                End If

            End If


            If Dgl2.Item(Col1Value, rowNewRemark).Value = "" And Dgl2.Item(Col1Value, rowNextDate).Value = "" And Dgl2.Item(Col1Value, rowUnableToConnect).Value = "" Then Exit Sub


            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"




            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Rows(I).Visible Then
                    If Dgl1.Item(Col1DocId, I).Value <> "" Then
                        If (Dgl1.Item(Col1FollowupRemarks, I).Value <> "" Or Dgl1.Item(Col1FollowupNextDate, I).Value <> "" Or Dgl1.Item(Col1UnableToConnect, I).Value <> "") Then



                            mQry = " Insert Into LedgerPaymentFollowupHistory(DocID, V_SNo, FollowupType, FollowupAmount, 
                            FollowupCommittedAmount, FollowupDate, FollowupRemark, NextFollowupDate,
                            OldAmount, OldCommittedAmount,OldAdjustedAmount, OldDate, 
                            OldRemark, OldBalance, NewBalance, CurrentBalance, UnableToConnect, UnabletoConnectDate) 
                            Values(" & AgL.Chk_Text(Dgl1.Item(Col1DocId, I).Value) & ", 
                            " & AgL.Chk_Text(Dgl1.Item(Col1V_Sno, I).Value) & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1FollowupType, I).Value) & ", 
                            " & AgL.VNull(Dgl1.Item(Col1FollowupAmount, I).Value) & ",
                            " & AgL.VNull(Dgl1.Item(Col1FollowupCommittedAmount, I).Value) & ",
                            " & AgL.Chk_DateTime(Dgl1.Item(Col1FollowupDate, I).Value) & ", 
                            " & AgL.Chk_Text(Dgl1.Item(Col1FollowupRemarks, I).Value) & ", 
                            " & AgL.Chk_Date(Dgl1.Item(Col1FollowupNextDate, I).Value) & ",
                            " & AgL.VNull(Dgl1.Item(Col1OldAmount, I).Value) & ",
                            " & AgL.VNull(Dgl1.Item(Col1OldCommittedAmount, I).Value) & ",
                            " & AgL.VNull(Dgl1.Item(Col1OldAdjustedAmount, I).Value) & ",
                            " & AgL.Chk_DateTime(Dgl1.Item(Col1OldDate, I).Value) & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1OldRemark, I).Value) & ", 
                            " & AgL.VNull(Dgl1.Item(Col1OldBalance, I).Value) & ",
                            " & AgL.VNull(Dgl1.Item(Col1NewBalance, I).Value) & ",
                            " & AgL.VNull(Dgl1.Item(Col1CurrentBalance, I).Value) & ",
                            " & AgL.Chk_Text(Dgl1.Item(Col1UnableToConnect, I).Value) & ",
                            " & AgL.Chk_DateTime(Dgl1.Item(Col1UnableToConnectDate, I).Value) & "
                            ) "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        End If

                        If AgL.XNull(Dgl2.Item(Col1Value, rowUnableToConnect).Value) <> "N/A" Then
                            mQry = "Update LedgerPaymentFollowup Set 
                                    UnableToConnect = " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowUnableToConnect).Value) & ",
                                    UnableToConnectDate = " & AgL.Chk_DateTime(AgL.PubLoginDate & " " & DateTime.Now().ToString("hh:mm tt")) & "
                                    where DocID = " & AgL.Chk_Text(Dgl1.Item(Col1DocId, I).Value) & "
                                    And V_SNo = " & AgL.Chk_Text(Dgl1.Item(Col1V_Sno, I).Value) & "
                                    "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        Else
                            mQry = " Delete From LedgerPaymentFollowup Where DocId=" & AgL.Chk_Text(Dgl1.Item(Col1DocId, I).Value) & " 
                             And V_SNo = " & AgL.Chk_Text(Dgl1.Item(Col1V_Sno, I).Value) & ""
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                            mQry = " Insert Into LedgerPaymentFollowup(DocID, V_SNo, FollowupType, FollowupAmount, 
                            FollowupCommittedAmount, FollowupDate, FollowupRemark, NextFollowupDate,
                            OldAmount, OldCommittedAmount,OldAdjustedAmount, OldDate, 
                            OldRemark, OldBalance, NewBalance, CurrentBalance, UnableToConnect) 
                            Values(" & AgL.Chk_Text(Dgl1.Item(Col1DocId, I).Value) & ", 
                            " & AgL.Chk_Text(Dgl1.Item(Col1V_Sno, I).Value) & ",
                            " & AgL.Chk_Text(mFollowupType) & ", 
                            " & AgL.VNull(Dgl2.Item(Col1Value, rowTotalDueBalance).Value) & ",
                            " & AgL.VNull(Dgl2.Item(Col1Value, rowNewCommittedAmount).Value) & ",
                            " & AgL.Chk_DateTime(AgL.PubLoginDate & " " & DateTime.Now().ToString("hh:mm tt")) & ", 
                            " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowNewRemark).Value) & ", 
                            " & AgL.Chk_Date(Dgl2.Item(Col1Value, rowNextDate).Value) & ",
                            " & AgL.VNull(Dgl2.Item(Col1Value, rowOldAmount).Value) & ",
                            " & AgL.VNull(Dgl2.Item(Col1Value, rowOldCommittedAmount).Value) & ",
                            " & AgL.VNull(Dgl2.Item(Col1Value, rowOldAdjusted).Value) & ",
                            " & AgL.Chk_Date(Dgl2.Item(Col1Value, rowOldDate).Value) & ",
                            " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowOldRemark).Value) & ", 
                            " & AgL.VNull(Dgl2.Item(Col1Value, rowOldBalance).Value) & ",
                            " & AgL.VNull(Dgl2.Item(Col1Value, rowNewBalance).Value) & ",
                            " & AgL.VNull(Dgl2.Item(Col1Value, rowCurrentBalance).Value) & ",
                            " & AgL.Chk_Text(Dgl2.Item(Col1Value, rowUnableToConnect).Value) & "
                            ) "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If


                    End If
                End If
            Next


            AgL.ETrans.Commit()
            mTrans = "Commit"

            mOkButtonPressed = True
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try

        'If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
        '    AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        'End If
    End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If EntryMode = "Browse" Then
            Select Case e.KeyCode
                Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
                Case Else
                    e.Handled = True
            End Select
            Exit Sub
        End If

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                sender.Rows(sender.currentcell.rowindex).Visible = False
                Calculation()
                e.Handled = True
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub



    End Sub

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        FSave()
        If mOkButtonPressed Then Me.Close()
    End Sub

    Private Sub Dgl2_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl2.CellBeginEdit
        Select Case Dgl2.CurrentCell.RowIndex
            Case rowOldDate, rowOldAmount, rowNewBalance, rowOldAdjusted, rowOldBalance, rowTotalDueBalance, rowOldRemark, rowOldCommittedAmount, rowCurrentBalance, rowCurrentBalancePakka
                e.Cancel = True
            Case rowNextDate, rowNewRemark, rowNewCommittedAmount
                If Dgl2.Item(Col1Value, rowUnableToConnect).Value.ToString.ToUpper <> "N/A" Then
                    e.Cancel = True
                End If
        End Select
    End Sub





    Private Function FillFifoOutstanding(mCondstr As String, Optional Purpose As String = "") As DataSet
        Dim mRemainingBalance As Double
        Dim i As Integer, j As Integer
        Dim dtParty As DataTable

        Dim DtMain As DataTable
        Dim BalAmount As Double
        Dim DrCr As String
        Dim dtLedger As DataTable
        Dim dtPayments As DataTable
        Dim drInvoices As DataRow()
        Dim drPayments As DataRow()

        Dim bGroupOn As String = "Linked Party"
        Dim bReportType As String = "Party Wise Balance - Ageing"
        Dim DsHeader As DataSet


        mQry = " Delete From #FifoOutstanding "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = "Select Sg.Subcode, Max(Sg.Nature) as Nature, Sum(Lg.AmtDr)-Sum(Lg.AmtCr) as Balance
                            From Ledger Lg "
        If bGroupOn = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = PSg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On Sg.Parent = PSg.Subcode "
        End If
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Where 1 = 1 "
        mQry = mQry & mCondstr
        mQry = mQry & " Group By Sg.Subcode"
        If bReportType = "Party Wise Balance - Ageing" Then
            mQry = mQry & " Having Sum(Lg.AmtDr)-Sum(Lg.AmtCr) <> 0 "
        End If

        dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)



        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, VT.NCat, Vt.Description as V_TypeDesc, 
                                    IfNull(PI.VendorDocNo,Lg.RecId) as RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, 
                                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else LG.AmtDr End) + (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else LG.AmtCr End) as Amount,
                                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else LG.AmtDr End) AmtDr, 
                                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else LG.AmtCr End) AmtCr,
                                    IfNull(FupH.FollowupCount,0) + (Case When Fup.FollowupDate Is Not Null Then 1 Else 0 End) as FollowUpCount
                                    From Ledger Lg  With (NoLock) 
                                    Left Join PurchInvoice PI On Lg.DocID = PI.DocID "
        If bGroupOn = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
            mQry = mQry & " Left Join Subgroup PSg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = PSg.Subcode "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
            mQry = mQry & " Left Join Subgroup PSg On Sg.Parent = PSg.Subcode "
        End If
        mQry = mQry & " Left Join LedgerPaymentFollowup Fup on Lg.DocID = Fup.DocID And Lg.V_SNo = Fup.V_SNo
                        Left Join (Select FupH.DocID, FupH.V_Sno, Count(*) as FollowupCount
                    From   LedgerPaymentFollowupHistory FupH 
                    Group By FupH.DocID, FupH.V_SNo 
                    ) as FupH On  Lg.DocID = FupH.DocID and Lg.V_SNo = FupH.V_SNo "
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) As LTV On Sg.Subcode = LTV.Subcode
                        Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                        Left Join TransactionReferences Trd With (NoLock) On Lg.DocID = Trd.DocId And Trd.DocIDSr=LG.V_Sno And Lg.V_Date >= '2019-07-01'
                        Left Join TransactionReferences Trr With (NoLock) On Lg.DocID = Trr.ReferenceDocId And Trr.ReferenceSr=Lg.V_Sno And Lg.V_Date >= '2019-07-01'
                        Where 1=1  " & mCondstr & " 
						And LG.DocID || LG.V_SNo Not In (SELECT H.PurchaseInvoiceDocId || H.PurchaseInvoiceDocIdSr   FROM Cloth_SupplierSettlementInvoices H
                                                                UNION ALL 
                                                                SELECT H.PaymentDocId || H.PaymentDocIdSr   FROM Cloth_SupplierSettlementPayments H
                                                                )  And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('WPS','WRS') ) 
                        Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"

        dtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If dtParty.Rows.Count > 0 Then
            For i = 0 To dtParty.Rows.Count - 1
                mQry = ""
                If AgL.XNull(dtParty.Rows(i)("Nature")) = "Customer" Then
                    If AgL.VNull(dtParty.Rows(i)("Balance")) > 0 Then
                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    IfNull(PI.VendorDocNo,Lg.RecId) as RecID, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtDr as Amount                                
                                    From Ledger Lg  With (NoLock) 
                                    Left Join PurchInvoice PI On LG.DocID = PI.DocId "
                        If bGroupOn = "Linked Party" Then
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(LG.LinkedSubcode,LG.SubCode)  "
                        Else
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode  "
                        End If
                        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
                                    Left Join (Select FupH.DocID, FupH.V_Sno, Count(*) as FollowupCount
                                               From   LedgerPaymentFollowupHistory FupH 
                                               Group By FupH.DocID, FupH.V_SNo 
                                              ) as FupH On  H.DocID = FupH.DocID and H.V_SNo = FupH.V_SNo
                                    Where Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  
                                    And Lg.AmtDr > 0  " & mCondstr & "                               
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
                    Else
                        If bReportType = "Party Wise Summary - Ageing" Or Purpose <> "" Then
                            mQry = "Insert Into #FifoOutstanding
                                                (DocID, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode, BillAmount,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(Null,
                                                Null,
                                                Null,
                                                Null,
                                                Null,
                                                Null,
                                                " & AgL.Chk_Text(AgL.XNull(dtParty.Rows(i)("Subcode"))) & ",
                                                0,
                                                " & Val((AgL.VNull(dtParty.Rows(i)("Balance")))) & ",
                                                'Cr',
                                                Null
                                                )
                                                "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mQry = ""
                        End If
                    End If
                Else
                    If AgL.VNull(dtParty.Rows(i)("Balance")) < 0 Then
                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    IfNull(PI.VendorDocNo,Lg.RecId) as RecID, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtCr as Amount                                
                                    From Ledger Lg  With (NoLock) 
                                    Left Join PurchInvoice PI On LG.DocID = PI.DocID"
                        If bGroupOn = "Linked Party" Then
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
                        Else
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
                        End If

                        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Left Join (Select FupH.DocID, FupH.V_Sno, Count(*) as FollowupCount
                                               From LedgerPaymentFollowupHistory FupH 
                                               Group By FupH.DocID, FupH.V_SNo 
                                              ) as FupH On  H.DocID = FupH.DocID and H.V_SNo = FupH.V_SNo
                                    Where Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtCr > 0 " & mCondstr & " 
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, IfNull(PI.VendorDocNo,Lg.RecId) desc"
                    Else
                        If bReportType = "Party Wise Summary - Ageing" Or Purpose <> "" Then
                            mQry = "Insert Into #FifoOutstanding
                                                (DocID, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode, BillAmount,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(Null,
                                                Null,
                                                Null,
                                                Null,
                                                Null,
                                                Null,
                                                " & AgL.Chk_Text(AgL.XNull(dtParty.Rows(i)("Subcode"))) & ",
                                                0,
                                                " & Val(AgL.VNull(dtParty.Rows(i)("Balance"))) * -1.0 & ",
                                                'Dr',
                                                Null
                                                )
                                                "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mQry = ""
                        End If
                    End If
                End If


                BalAmount = 0 : DrCr = ""
                mRemainingBalance = Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))
                If mQry <> "" Then
                    If AgL.XNull(dtParty.Rows(i)("Nature")) = "Customer" Then
                        drInvoices = dtLedger.Select(" Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And AmtDr > 0  ", " V_Date Desc ")
                    Else
                        drInvoices = dtLedger.Select(" Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And AmtCr > 0  ", " V_Date Desc ")
                    End If
                    'DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If drInvoices.Length > 0 Then
                        For j = 0 To drInvoices.Length - 1

                            If mRemainingBalance > 0 Then

                                If mRemainingBalance > AgL.VNull(drInvoices(j)("Amount")) Then
                                    BalAmount = Format(AgL.VNull(drInvoices(j)("Amount")), "0.00")
                                    mRemainingBalance = mRemainingBalance - AgL.VNull(drInvoices(j)("Amount"))
                                Else
                                    BalAmount = Format(mRemainingBalance, "0.00")
                                    mRemainingBalance = mRemainingBalance - mRemainingBalance
                                End If
                                DrCr = IIf(AgL.VNull(dtParty.Rows(i)("Balance")) > 0, "Dr", "Cr")


                                mQry = "Insert Into #FifoOutstanding
                                                (DocID, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode, BillAmount,
                                                BalanceAmount, DrCr, Narration, FollowupCount)    
                                                Values(" & AgL.Chk_Text(AgL.XNull(drInvoices(j)("DocID"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("V_Type"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("RecID"))) & ",
                                                " & AgL.Chk_Date(AgL.XNull(drInvoices(j)("V_Date"))) & ",                                            
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("Site_Code"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("DivCode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("Subcode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("Amount"))) & ",
                                                " & BalAmount & ",
                                                " & AgL.Chk_Text(DrCr) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("Narration"))) & ",
                                                " & AgL.Chk_Text(AgL.VNull(drInvoices(j)("FollowupCount"))) & "
                                                )
                                                "
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            End If
                        Next
                    End If
                End If
            Next
        End If
        mQry = "Select * from #FifoOutstanding"
        DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If AgL.PubServerName = "" Then
            mQry = " Select strftime('%m-%Y',VMain.V_Date) As [Month], "
        Else
            mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date,103),4,7) As [Month], "
        End If
        mQry += " Sum(VMain.BillAmount) As BillAmount, IfNull(Sum(VMain.BalanceAmount),0) As BalanceAmount, 
                    Sum(VMain.FollowupCount) As FollowupCount
                    From #FifoOutstanding As VMain "
        If AgL.PubServerName = "" Then
            mQry += " GROUP By strftime('%m-%Y',VMain.V_Date)  
                    Order By strftime('%Y',VMain.V_Date), strftime('%m',VMain.V_Date)"
        Else
            mQry += " GROUP By Substring(Convert(NVARCHAR, VMain.V_Date,103),4,7), Year(VMain.V_Date), Month(VMain.V_Date)  
                    Order By Year(VMain.V_Date), Month(VMain.V_Date) "
        End If
        Dim DsMain As DataSet = AgL.FillData(mQry, AgL.GCn)

        FillFifoOutstanding = DsMain

        'mQry = "Select Null as DocID, Null as V_Type, strftime('%m-%Y', H.V_Date) as RecID, Null as V_Date, 
        '                    Null Site_Code, Null Div_Code, Null As LrNo, H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as PartyName,
        '                    City.CityName, Null As Narration, 0 As TaxableAmount, 0 As TaxAmount,
        '                    0 as Addition, 0 as BillAmount, 0 as GoodsReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
        '                    0 as Balance,Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else 0 End) as AmtDr, Sum(Case When H.DrCr='Cr' Then H.BalanceAmount Else 0 End) as AmtCr
        '                    From #FifoOutstanding H
        '                    Left Join Subgroup Sg on H.Subcode = Sg.Subcode
        '                    Left Join City On Sg.CityCode = City.CityCode
        '                    Group By H.Subcode, strftime('%m-%Y', H.V_Date)
        '                    Order By strftime('%Y', H.V_Date), strftime('%m', H.V_Date)
        '                    "
        'DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'Dim mBillsUpToDate As Date = CDate(MainCondStr.BillsUptoDate).ToString("s")
        'Dim CurrentMonth As Date = CDate(mBillsUpToDate)
        'Dim OneMonthBack As Date = DateAdd(DateInterval.Month, -1, mBillsUpToDate)
        'Dim TwoMonthBack As Date = DateAdd(DateInterval.Month, -2, mBillsUpToDate)
        'Dim ThreeMonthBack As Date = DateAdd(DateInterval.Month, -3, mBillsUpToDate)
        'Dim FourMonthBack As Date = DateAdd(DateInterval.Month, -4, mBillsUpToDate)
        'Dim FiveMonthBack As Date = DateAdd(DateInterval.Month, -5, mBillsUpToDate)
        'Dim SixMonthBack As Date = DateAdd(DateInterval.Month, -6, mBillsUpToDate)
        'Dim SevenMonthBack As Date = DateAdd(DateInterval.Month, -7, mBillsUpToDate)
        'Dim EightMonthBack As Date = DateAdd(DateInterval.Month, -8, mBillsUpToDate)
        'Dim NineMonthBack As Date = DateAdd(DateInterval.Month, -9, mBillsUpToDate)

        'If Purpose = "" Then

        '    mQry = "Select H.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & CurrentMonth.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & OneMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & TwoMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & ThreeMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FourMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FiveMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SixMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SevenMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [Before " & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN H.V_Date <= " & AgL.Chk_Date(mBillsUpToDate) & " Then H.BalanceAmount ELSE 0 END ) As [Balance],
        '                    Sum(H.DrCr) As [DrCr]                                                      
        '                    From #FifoOutstanding H
        '                    Left Join Subgroup Sg on H.Subcode = Sg.Subcode
        '                    Left Join City On Sg.CityCode = City.CityCode
        '                    Left join PurchInvoice PI On H.DocID = PI.DocId
        '                    Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
        '                    Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
        '                    "

        '    DsHeader = AgL.FillData(mQry, AgL.GCn)
        '    'FillFifoOutstanding = DsHeader
        'Else

        '    Dim mMultiplier As Double
        '    If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
        '        mMultiplier = 0.01
        '    Else
        '        mMultiplier = 1.0
        '    End If

        '    mQry = "Select H.Subcode as SearchCode, 1 as Sr, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
        '                    Sum(CASE WHEN H.V_Date <= " & AgL.Chk_Date(mBillsUpToDate) & " Then H.BalanceAmount ELSE 0 END ) * " & mMultiplier & " as BalanceAmount, 

        '                    (CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then " & AgL.Chk_Date(CurrentMonth) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then " & AgL.Chk_Date(OneMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then " & AgL.Chk_Date(TwoMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then " & AgL.Chk_Date(ThreeMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then " & AgL.Chk_Date(FourMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then " & AgL.Chk_Date(FiveMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then " & AgL.Chk_Date(SixMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then " & AgL.Chk_Date(SevenMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(EightMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(NineMonthBack) & "
        '                     Else Null End) as BalanceMonth   
        '                    From #FifoOutstanding H
        '                    Left Join Subgroup Sg on H.Subcode = Sg.Subcode
        '                    Left Join City On Sg.CityCode = City.CityCode
        '                    Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End),
        '                    (CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then " & AgL.Chk_Date(CurrentMonth) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then " & AgL.Chk_Date(OneMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then " & AgL.Chk_Date(TwoMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then " & AgL.Chk_Date(ThreeMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then " & AgL.Chk_Date(FourMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then " & AgL.Chk_Date(FiveMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then " & AgL.Chk_Date(SixMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then " & AgL.Chk_Date(SevenMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(EightMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(NineMonthBack) & "
        '                     Else Null End)
        '                    Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End), H.V_Date
        '            "
        '    FillFifoOutstanding = AgL.FillData(mQry, AgL.GCn)
        'End If
    End Function
End Class