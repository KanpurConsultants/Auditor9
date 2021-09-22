Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Microsoft.Reporting.WinForms
Public Class ClsChequePrinting

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""

    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4

    Public Const Col1Select As String = "Tick"
    Public Const Col1SearchCode As String = "Search Code"
    Public Const Col1SearchSr As String = "Search Sr"
    Public Const Col1Exception As String = "Exception"
    Public Const Col1DocDate As String = "Doc Date"
    Public Const Col1AccountName As String = "Account Name"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1ChequeNo As String = "Cheque No"
    Public Const Col1ChequeDate As String = "Cheque Date"
    Public Const Col1FavouringName As String = "Favouring Name"
    Public Const Col1AccountPayeeYn As String = "Account Payee Yn"
    Public Const Col1AmountInWords As String = "Amount In Words"
    Public Const Col1FormattedDate As String = "Formatted Date"
    Public Const Col1ChequeFormat As String = "Cheque Format"
    Public Const Col1ChequeFormatCode As String = "Cheque Format Code"
    Public Const Col1ChequeText As String = "Cheque Text"
    Public Const Col1DateFormat As String = "Date Format"
    Public Const Col1DateSpacing As String = "Date Spacing"
    Public Const Col1MasterChequeText As String = "Master Cheque Text"




    Dim mShowReportType As String = ""

    Dim DsHeader As DataSet = Nothing

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property
    Public Property ShowReportType() As String
        Get
            ShowReportType = mShowReportType
        End Get
        Set(ByVal value As String)
            mShowReportType = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpSubGroupQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('" & SubgroupType.Customer & "','" & SubgroupType.Supplier & "','" & SubgroupType.LedgerAccount & "')  "
    Dim mHelpSchemeQry$ = "Select Code, Description As [Scheme] From SchemeHead "
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Account Name", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupQry)
            ReportFrm.CreateHelpGrid("Left Margin", "Left Margin", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", "")
            ReportFrm.BtnProceed.Visible = True
            ReportFrm.BtnProceed.Text = "Print Cheque"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcMain()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Private Sub FPrepareTextForChequePrinting(mRow As Integer)
        DsHeader.Tables(0).Rows(mRow)("AmountInWords") = ClsMain.AmountInWordsInIndianFormat(AgL.VNull(DsHeader.Tables(0).Rows(mRow)("Amount"))).ToString.Replace("Rupees", "")
        If AgL.XNull(DsHeader.Tables(0).Rows(mRow)("DateFormat")) = "DDMMYYYY" Then
            If AgL.XNull(DsHeader.Tables(0).Rows(mRow)("ChequeDate")) = "" Then
                DsHeader.Tables(0).Rows(mRow)("FormattedDate") = Format(CDate(AgL.XNull(DsHeader.Tables(0).Rows(mRow)("DocDate"))), "ddMMyyyy")
            Else
                DsHeader.Tables(0).Rows(mRow)("FormattedDate") = Format(CDate(AgL.XNull(DsHeader.Tables(0).Rows(mRow)("ChequeDate"))), "ddMMyyyy")
            End If

            If AgL.VNull(DsHeader.Tables(0).Rows(mRow)("DateSpacing")) > 0 Then
                Dim mOldFormatteDate As String
                Dim mNewFormattedDate As String = ""
                Dim J As Integer
                mOldFormatteDate = DsHeader.Tables(0).Rows(mRow)("FormattedDate")
                For J = 0 To mOldFormatteDate.Length - 1
                    mNewFormattedDate += mOldFormatteDate.Chars(J) + Space(DsHeader.Tables(0).Rows(mRow)("DateSpacing"))
                Next
                DsHeader.Tables(0).Rows(mRow)("FormattedDate") = mNewFormattedDate
            End If
        Else
            If AgL.XNull(DsHeader.Tables(0).Rows(mRow)("ChequeDate")) = "" Then
                DsHeader.Tables(0).Rows(mRow)("FormattedDate") = Format(CDate(AgL.XNull(DsHeader.Tables(0).Rows(mRow)("DocDate"))), "dd-MMM-yyyy")
            Else
                DsHeader.Tables(0).Rows(mRow)("FormattedDate") = Format(CDate(AgL.XNull(DsHeader.Tables(0).Rows(mRow)("ChequeDate"))), "dd-MMM-yyyy")
            End If

        End If
        DsHeader.Tables(0).Rows(mRow)("ChequeText") = DsHeader.Tables(0).Rows(mRow)("MasterChequeText").ToString.ToUpper.
                                                               Replace("<CHQ_DATE>", DsHeader.Tables(0).Rows(mRow)("FormattedDate")).
                                                               Replace("<PARTY_NAME>", DsHeader.Tables(0).Rows(mRow)("FavouringName")).
                                                               Replace("<AMOUNT>", Format(DsHeader.Tables(0).Rows(mRow)("AMOUNT"), "0.00").ToString).
                                                               Replace("<AMOUNT_IN_WORDS>", DsHeader.Tables(0).Rows(mRow)("AmountInWords"))

    End Sub

    Private Sub FPrepareTextForChequePrintingAfterFilling(mRow As Integer)
        DsHeader.Tables(0).Rows(mRow)(Col1AmountInWords) = ClsMain.AmountInWordsInIndianFormat(AgL.VNull(DsHeader.Tables(0).Rows(mRow)(Col1Amount))).ToString.Replace("Rupees", "")
        If AgL.XNull(DsHeader.Tables(0).Rows(mRow)(Col1DateFormat)) = "DDMMYYYY" Then
            If AgL.XNull(DsHeader.Tables(0).Rows(mRow)(Col1ChequeDate)) = "" Then
                DsHeader.Tables(0).Rows(mRow)(Col1FormattedDate) = Format(CDate(AgL.XNull(DsHeader.Tables(0).Rows(mRow)(Col1DocDate))), "ddMMyyyy")
            Else
                DsHeader.Tables(0).Rows(mRow)(Col1FormattedDate) = Format(CDate(AgL.XNull(DsHeader.Tables(0).Rows(mRow)(Col1ChequeDate))), "ddMMyyyy")
            End If

            If AgL.VNull(DsHeader.Tables(0).Rows(mRow)(Col1DateSpacing)) > 0 Then
                Dim mOldFormatteDate As String
                Dim mNewFormattedDate As String = ""
                Dim J As Integer
                mOldFormatteDate = DsHeader.Tables(0).Rows(mRow)(Col1FormattedDate)
                For J = 0 To mOldFormatteDate.Length - 1
                    mNewFormattedDate += mOldFormatteDate.Chars(J) + Space(DsHeader.Tables(0).Rows(mRow)(Col1DateSpacing))
                Next
                DsHeader.Tables(0).Rows(mRow)(Col1FormattedDate) = mNewFormattedDate
            End If
        Else
            If AgL.XNull(DsHeader.Tables(0).Rows(mRow)(Col1ChequeDate)) = "" Then
                DsHeader.Tables(0).Rows(mRow)(Col1FormattedDate) = Format(CDate(AgL.XNull(DsHeader.Tables(0).Rows(mRow)(Col1DocDate))), "dd-MMM-yyyy")
            Else
                DsHeader.Tables(0).Rows(mRow)(Col1FormattedDate) = Format(CDate(AgL.XNull(DsHeader.Tables(0).Rows(mRow)(Col1ChequeDate))), "dd-MMM-yyyy")
            End If

        End If
        DsHeader.Tables(0).Rows(mRow)(Col1ChequeText) = DsHeader.Tables(0).Rows(mRow)(Col1MasterChequeText).ToString.ToUpper.
                                                               Replace("<CHQ_DATE>", DsHeader.Tables(0).Rows(mRow)(Col1FormattedDate)).
                                                               Replace("<PARTY_NAME>", DsHeader.Tables(0).Rows(mRow)(Col1FavouringName)).
                                                               Replace("<AMOUNT>", Format(DsHeader.Tables(0).Rows(mRow)(Col1Amount), "0.00").ToString).
                                                               Replace("<AMOUNT_IN_WORDS>", DsHeader.Tables(0).Rows(mRow)(Col1AmountInWords))

    End Sub

    Public Sub ProcMain(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing,
                                Optional bDocId As String = "",
                                Optional bNCat As String = "")
        Try
            Dim mCondStr$ = ""
            Dim mPurchaseReturnCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Cheque Printing"


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If


            If bDocId <> "" Then
                mQry = " Select V_Date From LedgerHead H Where H.DocId = '" & bDocId & "'"
                Dim DtInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtInvoiceDetail.Rows.Count > 0 Then
                    ReportFrm.FilterGrid.Item(GFilter, 0).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                    ReportFrm.FilterGrid.Item(GFilter, 1).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                End If


                mCondStr = " Where H.DocId = '" & bDocId & "' "
            Else
                mCondStr = " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            End If
            mCondStr += ReportFrm.GetWhereCondition("L.Subcode", 2)
            mCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr += " And Vt.NCat In ('" & Ncat.Payment & "', '" & Ncat.PaymentSettlement & "') "
            mCondStr += " And Bank.Nature = 'Bank' "
            'If bNCat = Ncat.PaymentSettlement Then
            '    mCondStr += " And Sg.Nature = 'Bank' "
            'Else
            '    mCondStr += " And Bank.Nature = 'Bank' "
            'End If

            'Replace(Replace(Replace(Replace(CF.Format,'<DOC_DATE>',H.V_Date),'<PARTY_NAME>',Sg.DispName),'<AMOUNT>',L.Amount), '<AMOUNT_IN_WORDS>',L.AmountInWords) as ChequeText

            mQry = "Select " & IIf(bDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, H.DocID as SearchCode, L.Sr as SearchSr, H.V_Date DocDate, Sg.Name as AccountName, L.Amount,
                    CF.Description as ChequeFormat, L.ChqRefNo as ChequeNo, L.ChqRefDate as ChequeDate, 
                    Sg.DispName as FavouringName, 'Yes' as AccountPayeeYn, 
                    CF.Code as ChequeFormatCode, CF.Format as ChequeText, CF.DateFormat, 
                    CF.DateSpacing, Space(500) as AmountInWords, Space(50) as FormattedDate, 
                    CF.Format as MasterChequeText                
                from LedgerHead H                
                Left Join LedgerHeadDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join Subgroup Sg On L.Subcode = Sg.Subcode
                Left Join Subgroup Bank On H.Subcode = Bank.Subcode
                Left join ChequeFormat CF On Bank.ChequeFormat = CF.Code                
                Left Join Voucher_Type VT On H.V_Type  = VT.V_Type
                " + mCondStr
            mQry = mQry + " Union All "

            'mQry = " Select " & IIf(bDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, H.DocID as SearchCode, L.Sr as SearchSr, H.V_Date DocDate, Sg.Name as AccountName, L.Amount,
            '        CF.Description as ChequeFormat, L.ChqRefNo as ChequeNo, L.ChqRefDate as ChequeDate, 
            '        Sg.DispName as FavouringName, 'Yes' as AccountPayeeYn, 
            '        CF.Code as ChequeFormatCode, CF.Format as ChequeText, CF.DateFormat, 
            '        CF.DateSpacing, Space(500) as AmountInWords, Space(50) as FormattedDate, 
            '        CF.Format as MasterChequeText                
            '    from LedgerHead H                
            '    Left Join LedgerHeadDetail L  With (NoLock) On H.DocID = L.DocID
            '    Left Join Subgroup Sg On H.Subcode = Sg.Subcode
            '    Left Join Subgroup Bank On L.Subcode = Bank.Subcode
            '    Left join ChequeFormat CF On Bank.ChequeFormat = CF.Code                
            '    Left Join Voucher_Type VT On H.V_Type  = VT.V_Type
            '    " + mCondStr

            mQry = " Select " & IIf(bDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, H.DocID as SearchCode, L.Sr as SearchSr, H.V_Date DocDate, Sg.Name as AccountName, L.Amount,
                    CF.Description as ChequeFormat, L.ChqRefNo as ChequeNo, L.ChqRefDate as ChequeDate, 
                    Sg.DispName as FavouringName, 'Yes' as AccountPayeeYn, 
                    CF.Code as ChequeFormatCode, CF.Format as ChequeText, CF.DateFormat, 
                    CF.DateSpacing, Space(500) as AmountInWords, Space(50) as FormattedDate, 
                    CF.Format as MasterChequeText                
                from LedgerHead H                
                Left Join LedgerHeadDetail L  With (NoLock) On H.DocID = L.DocID "


            If bNCat = Ncat.PaymentSettlement Then
                mQry += " Left Join Subgroup Bank On L.Subcode = Bank.Subcode
                Left Join Subgroup Sg On H.Subcode = Sg.Subcode "
            Else
                mQry += " Left Join Subgroup Sg On L.Subcode = Sg.Subcode
                Left Join Subgroup Bank On H.Subcode = Bank.Subcode "
            End If


            mQry += " Left join ChequeFormat CF On Bank.ChequeFormat = CF.Code                
                Left Join Voucher_Type VT On H.V_Type  = VT.V_Type
                " + mCondStr

            mQry = mQry + " Order By H.DocID, L.Sr "

            DsHeader = AgL.FillData(mQry, AgL.GCn)



            For I As Integer = 0 To DsHeader.Tables(0).Rows.Count - 1
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("ChequeFormat")) = "" Then
                    If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                    DsHeader.Tables(0).Rows(I)("Exception") += "Cheque Format Not Defined In Bank Account."
                Else
                    FPrepareTextForChequePrinting(I)
                End If
            Next

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            'mQry = "Select 'Create JSON File' As MenuText, 'FCreateJSONFile' As FunctionName"
            'Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            ReportFrm.Text = "Cheque Printing"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"
            'ReportFrm.InputColumnsStr = Col1ChequeNo + Col1ChequeDate + Col1AccountPayeeYn + Col1FavouringName
            ReportFrm.InputColumnsStr = Col1FavouringName
            'ReportFrm.DTCustomMenus = DtMenuList

            ReportFrm.ProcFillGrid(DsHeader)


            ReportFrm.DGL1.Columns(Col1ChequeNo).Visible = True
            ReportFrm.DGL1.Columns(Col1ChequeText).Visible = False
            ReportFrm.DGL1.Columns(Col1MasterChequeText).Visible = False
            ReportFrm.DGL1.Columns(Col1DateFormat).Visible = False
            ReportFrm.DGL1.Columns(Col1FormattedDate).Visible = False
            ReportFrm.DGL1.Columns(Col1ChequeFormatCode).Visible = False
            ReportFrm.DGL1.Columns(Col1AmountInWords).Visible = False
            ReportFrm.DGL1.Columns(Col1DateSpacing).Visible = False
            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next
            ReportFrm.DGL1.Columns(Col1FavouringName).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1ChequeNo).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1ChequeDate).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1AccountPayeeYn).ReadOnly = False
            ReportFrm.DGL1.AutoResizeRows()


        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Public Sub FProceed()
        Dim I As Integer = 0
        Dim mV_Type As String = ""
        Dim mTrans As String = ""
        Dim mMainQry As String
        Dim mRecordCount As Integer
        Dim mRecordCountException As Integer

        If ReportFrm.FGetText(1) = "" Then
            MsgBox("Please input Scheme Process Date...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        Try

            mMainQry = ""
            mRecordCount = 0
            mRecordCountException = 0
            For I = 0 To ReportFrm.DGL1.Rows.Count - 1
                If ReportFrm.DGL1.Item(Col1Select, I).Value = "þ" Then
                    If Val(ReportFrm.DGL1.Item(Col1Amount, I).Value) > 0 Then
                        mRecordCount += 1
                        If mMainQry <> "" Then mMainQry += " Union All "
                        mMainQry += " Select " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SearchCode, I).Value) & " as DocID,
                                           " & Val(ReportFrm.DGL1.Item(Col1SearchSr, I).Value) & " as Sr,
                                           " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ChequeText, I).Value) & " as ChequeText,
                                           " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1AccountPayeeYn, I).Value) & " as AccountPayeeYn
                                    "

                        If AgL.XNull(ReportFrm.DGL1.Item(Col1Exception, I).Value) <> "" Then
                            mRecordCountException += 1
                        End If
                    End If
                End If
            Next

            If mRecordCount = 0 Then
                MsgBox("No record selected to proceed")
                Exit Sub
            End If


            If mRecordCountException > 0 Then
                MsgBox("Please clear exception before proceeding")
                Exit Sub
            End If

            Dim objRepPrint As Object
            objRepPrint = New AgLibrary.RepView(AgL)

            FPrintThisDocument(ReportFrm, objRepPrint, "", mMainQry, "Cheque_Print.rpt", ".", , , , "", AgL.PubLoginDate, False)

            'ReportFrm.DGL1.DataSource = Nothing
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        FProceed()
    End Sub


    Private Sub ObjRepFormGlobal_Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) = True Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1AccountPayeeYn
                    If Not ClsMain.IsSpecialKeyPressed(e) Then
                        If e.KeyCode = Keys.N Then
                            ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value = "NO"
                        Else
                            ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value = "YES"
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ObjRepFormGlobal_Dgl1CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles ReportFrm.DGL1CellBeginEdit
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0

        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1AccountPayeeYn
                    e.Cancel = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub ObjRepFormGlobal_Dgl1CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ReportFrm.DGL1CellEnter
        Dim bRowIndex As Integer
        Dim bColumnIndex As Integer
        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ReportFrm_DGL1EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles ReportFrm.DGL1EditingControl_Validating
        Dim bRowIndex As Integer, bColumnIndex As Integer

        bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
        bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

        Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
            Case Col1FavouringName
                FPrepareTextForChequePrintingAfterFilling(bRowIndex)
            Case Col1ChequeDate
                ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value = AgL.RetDate(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value)
                FPrepareTextForChequePrintingAfterFilling(bRowIndex)
        End Select
    End Sub



    Public Sub FPrintThisDocument(ByVal objFrm As Object, ByVal objRepFrm As Object, ByVal V_Type As String,
                 Optional ByVal Report_QueryList As String = "", Optional ByVal Report_NameList As String = "",
                 Optional ByVal Report_TitleList As String = "", Optional ByVal Report_FormatList As String = "",
                 Optional ByVal SubReport_QueryList As String = "",
                 Optional ByVal SubReport_NameList As String = "", Optional ByVal PartyCode As String = "", Optional ByVal V_Date As String = "", Optional ByVal IsPrintToPrinter As Boolean = False,
                 Optional ByVal Division As String = "", Optional ByVal Site As String = ""
     )

        Dim DtVTypeSetting As DataTable = Nothing
        Dim mQry As String = ""
        Dim DsRep As New DataSet
        Dim strQry As String = ""

        Dim RepName As String = ""
        Dim RepTitle As String = ""
        Dim RepQry As String = ""

        Dim RetIndex As Integer = 0

        Dim Report_QryArr() As String = Nothing
        Dim Report_NameArr() As String = Nothing
        Dim Report_TitleArr() As String = Nothing
        Dim Report_FormatArr() As String = Nothing

        Dim SubReport_QryArr() As String = Nothing
        Dim SubReport_NameArr() As String = Nothing
        Dim SubReport_DataSetArr() As DataSet = Nothing

        Dim I As Integer = 0

        Try

            If TypeOf (objFrm) Is AgTemplate.TempTransaction Then
                If Division = "" Then
                    Division = objFrm.TxtDivision.Tag
                End If
            Else
                If Division = "" Then
                    Division = AgL.PubDivCode
                End If
            End If



            If Report_QueryList <> "" Then Report_QryArr = Split(Report_QueryList, "~")
            If Report_TitleList <> "" Then Report_TitleArr = Split(Report_TitleList, "|")
            If Report_NameList <> "" Then Report_NameArr = Split(Report_NameList, "|")

            If Report_FormatList <> "" Then
                Report_FormatArr = Split(Report_FormatList, "|")

                For I = 0 To Report_FormatArr.Length - 1
                    If strQry <> "" Then strQry += " UNION ALL "
                    strQry += " Select " & I & " As Code, '" & Report_FormatArr(I) & "' As Name "
                Next

                Dim FRH_Single As DMHelpGrid.FrmHelpGrid
                FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(strQry, AgL.GCn).TABLES(0)), "", 300, 350, , , False)
                FRH_Single.FFormatColumn(0, , 0, , False)
                FRH_Single.FFormatColumn(1, "Report Format", 250, DataGridViewContentAlignment.MiddleLeft)
                FRH_Single.StartPosition = FormStartPosition.CenterScreen
                FRH_Single.ShowDialog()

                If FRH_Single.BytBtnValue = 0 Then
                    RetIndex = FRH_Single.DRReturn("Code")
                End If

                If Report_NameArr.Length = Report_FormatArr.Length Then RepName = Report_NameArr(RetIndex) Else RepName = Report_NameArr(0)
                If Report_TitleArr.Length = Report_FormatArr.Length Then RepTitle = Report_TitleArr(RetIndex) Else RepTitle = Report_TitleArr(0)
                If Report_QryArr.Length = Report_FormatArr.Length Then RepQry = Report_QryArr(RetIndex) Else RepQry = Report_QryArr(0)
            Else
                RepName = Report_NameArr(0)
                RepTitle = Report_TitleArr(0)
                RepQry = Report_QryArr(0)
            End If



            AgL.PubTempStr = AgL.PubTempStr & "Start Execute Main Query to print : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            DsRep = AgL.FillData(RepQry, AgL.GCn)
            AgL.PubTempStr = AgL.PubTempStr & "End Execute Main Query to print : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


            AgL.PubTempStr = AgL.PubTempStr & "Start fetching logo & signature file name from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

            AgL.PubTempStr = AgL.PubTempStr & "End fetching logo & signature file name from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

            DsRep.Tables(0).Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"))
            DsRep.Tables(0).Columns.Add("CompanyAuthorisedSignature", System.Type.GetType("System.Byte[]"))

            AgL.PubTempStr = AgL.PubTempStr & "Start Reading Logo File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf



            AgL.PubTempStr = AgL.PubTempStr & "End Reading Logo File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


            AgL.PubTempStr = AgL.PubTempStr & "Start Reading Signature File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            Dim FileCompanySign() As Byte



            For I = 0 To DsRep.Tables(0).Rows.Count - 1
                DsRep.Tables(0).Rows(I)("CompanyAuthorisedSignature") = FileCompanySign
            Next
            AgL.PubTempStr = AgL.PubTempStr & "End Reading Signature File : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)

            If SubReport_QueryList <> "" Then SubReport_QryArr = Split(SubReport_QueryList, "^")
            If SubReport_NameList <> "" Then SubReport_NameArr = Split(SubReport_NameList, "^")


            AgL.PubTempStr = AgL.PubTempStr & "Start Executing Subreport Queries : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                If SubReport_QryArr.Length <> SubReport_NameArr.Length Then
                    MsgBox("Number Of SubReport Qries And SubReport Names Are Not Equal.", MsgBoxStyle.Information)
                    Exit Sub
                End If

                For I = 0 To SubReport_QryArr.Length - 1
                    ReDim Preserve SubReport_DataSetArr(I)
                    SubReport_DataSetArr(I) = New DataSet
                    SubReport_DataSetArr(I) = AgL.FillData(SubReport_QryArr(I).ToString, AgL.GCn)

                    AgPL.CreateFieldDefFile1(SubReport_DataSetArr(I), AgL.PubReportPath & "\" & Report_NameList & SubReport_NameArr(I).ToString & ".ttx", True)
                Next
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End Executing Subreport Queries : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

            AgL.PubCrystalDocument = New ReportDocument





            AgL.PubTempStr = AgL.PubTempStr & "Start Loading Crystal Report Document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            AgL.PubCrystalDocument.Load(AgL.PubReportPath & "\" & RepName)
            AgL.PubTempStr = AgL.PubTempStr & "End Loading Crystal Report Document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            AgL.PubTempStr = AgL.PubTempStr & "Start Setting Datasource to report document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            AgL.PubCrystalDocument.SetDataSource(DsRep.Tables(0))
            AgL.PubTempStr = AgL.PubTempStr & "End Setting Datasource to report document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

            Dim margins As PageMargins
            margins = AgL.PubCrystalDocument.PrintOptions.PageMargins
            margins.bottomMargin = AgL.PubCrystalDocument.PrintOptions.PageMargins.bottomMargin

            If AgL.VNull(ReportFrm.FGetText(3)) <> 0 Then
                margins.leftMargin = ReportFrm.FGetText(3)
            Else
                margins.leftMargin = AgL.PubCrystalDocument.PrintOptions.PageMargins.leftMargin
            End If

            margins.rightMargin = AgL.PubCrystalDocument.PrintOptions.PageMargins.rightMargin
            margins.topMargin = AgL.PubCrystalDocument.PrintOptions.PageMargins.topMargin
            AgL.PubCrystalDocument.PrintOptions.ApplyPageMargins(margins)

            AgL.PubTempStr = AgL.PubTempStr & "Start Setting Datasource to subreports : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                For I = 0 To SubReport_NameArr.Length - 1
                    Try
                        AgL.PubCrystalDocument.OpenSubreport(SubReport_NameArr(I).ToString).Database.Tables(0).SetDataSource(SubReport_DataSetArr(I).Tables(0))
                    Catch ex As Exception
                    End Try
                Next
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End Setting Datasource to subreports : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

            AgL.PubTempStr = AgL.PubTempStr & "Start Assigning PubCrystalDocument to Report Source : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            CType(objRepFrm.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = AgL.PubCrystalDocument
            AgL.PubTempStr = AgL.PubTempStr & "End Assigning PubCrystalDocument to Report Source : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf



            AgL.PubTempStr = AgL.PubTempStr & "Start setting Formulas : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            If TypeOf (objFrm) Is AgTemplate.TempTransaction Then
                ClsMain.Formula_Set(AgL.PubCrystalDocument, Division, CType(objFrm, AgTemplate.TempTransaction).TxtSite_Code.Tag, V_Type, RepTitle)
            ElseIf TypeOf (objFrm) Is AgLibrary.FrmRepDisplay Then
                ClsMain.Formula_Set(AgL.PubCrystalDocument, AgL.PubDivCode, AgL.PubSiteCode, V_Type, RepTitle)
                ClsMain.SetFormulaFilters(AgL.PubCrystalDocument, objFrm)
            Else
                ClsMain.Formula_Set(AgL.PubCrystalDocument, Division, AgL.PubSiteCode, V_Type, RepTitle)
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End setting Formulas : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            'AgPL.Show_Report(objRepFrm, "* " & RepTitle & " *", objFrm.MdiParent)

            If IsPrintToPrinter = True Then
                AgL.PubTempStr = AgL.PubTempStr & "Start Printing To Printer : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


                AgL.PubCrystalDocument.PrintToPrinter(1, True, 0, 0)

                AgL.PubTempStr = AgL.PubTempStr & "End Printing To Printer : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

            Else
                AgL.PubTempStr = AgL.PubTempStr & "Start Printing To Screen : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
                objRepFrm.MdiParent = objFrm.MdiParent
                objRepFrm.Show()
                AgL.PubTempStr = AgL.PubTempStr & "End Printing To Screen : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


            End If

            AgL.PubTempStr = AgL.PubTempStr & "Start Insert to Log Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            If TypeOf (objFrm) Is AgTemplate.TempTransaction Then
                Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
            Else
                Call AgL.LogTableEntry("", objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End Insert to Log Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
End Class
