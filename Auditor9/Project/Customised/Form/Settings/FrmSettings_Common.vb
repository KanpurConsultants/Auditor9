Imports System.Data.SQLite
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmSettings_Common
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const Col1Code As String = "Code"
    Protected Const Col1SettingType As String = "SettingType"
    Protected Const Col1SiteName As String = "Site"
    Protected Const Col1DivisionName As String = "Division"
    Protected Const Col1Category As String = "Category"
    Protected Const Col1NCat As String = "NCat"
    Protected Const Col1VoucherType As String = "Voucher Type"
    Protected Const Col1Process As String = "Process"
    Protected Const Col1SettingGroup As String = "Setting Group"
    Protected Const Col1FieldName As String = "Field Name"
    Protected Const Col1Value As String = "Value"
    Protected Const Col1ValueTag As String = "ValueTag"
    Protected Const Col1DataType As String = "Data Type"
    Protected Const Col1DataLength As String = "Data Length"
    Protected Const Col1HelpQuery As String = "HelpQry"
    Protected Const Col1HelpQueryType As String = "HelpQueryType"
    Protected Const Col1HelpSelectionType As String = "HelpSelectionType"

    Dim mQry As String = ""

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String
    Dim mSettingType As String = ""

    Dim DtSettingsData As New DataTable

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal SettingType As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        mSettingType = SettingType
    End Sub
    Public Sub InitSettingData()
        DtSettingsData.Columns.Add(Col1Code)
        DtSettingsData.Columns.Add(Col1SettingType)
        DtSettingsData.Columns.Add(Col1SiteName)
        DtSettingsData.Columns.Add(Col1DivisionName)
        DtSettingsData.Columns.Add(Col1Category)
        DtSettingsData.Columns.Add(Col1NCat)
        DtSettingsData.Columns.Add(Col1VoucherType)
        DtSettingsData.Columns.Add(Col1Process)
        DtSettingsData.Columns.Add(Col1SettingGroup)
        DtSettingsData.Columns.Add(Col1FieldName)
        DtSettingsData.Columns.Add(Col1Value)
        DtSettingsData.Columns.Add(Col1ValueTag)
        DtSettingsData.Columns.Add(Col1DataType)
        DtSettingsData.Columns.Add(Col1DataLength)
        DtSettingsData.Columns.Add(Col1HelpQuery)
        DtSettingsData.Columns.Add(Col1HelpQueryType)
        DtSettingsData.Columns.Add(Col1HelpSelectionType)
    End Sub

    Private Sub Ini_Grid()
        Dgl1.ColumnHeadersHeight = 40

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgAllowFind = False


        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)


        Dgl1.Columns(Col1SettingType).Width = 120
        Dgl1.Columns(Col1SiteName).Width = 120
        Dgl1.Columns(Col1DivisionName).Width = 90
        Dgl1.Columns(Col1Category).Width = 100
        Dgl1.Columns(Col1NCat).Width = 160
        Dgl1.Columns(Col1VoucherType).Width = 100
        Dgl1.Columns(Col1Process).Width = 100
        Dgl1.Columns(Col1SettingGroup).Width = 100
        Dgl1.Columns(Col1FieldName).Width = 330
        Dgl1.Columns(Col1Value).Width = 300


        Dgl1.Columns(Col1SettingType).ReadOnly = True
        Dgl1.Columns(Col1SiteName).ReadOnly = True
        Dgl1.Columns(Col1DivisionName).ReadOnly = True
        Dgl1.Columns(Col1Category).ReadOnly = True
        Dgl1.Columns(Col1NCat).ReadOnly = True
        Dgl1.Columns(Col1VoucherType).ReadOnly = True
        Dgl1.Columns(Col1Process).ReadOnly = True
        Dgl1.Columns(Col1SettingGroup).ReadOnly = True
        Dgl1.Columns(Col1FieldName).ReadOnly = True
        Dgl1.Columns(Col1Value).ReadOnly = True

        Dgl1.Columns(Col1Code).Visible = False
        Dgl1.Columns(Col1ValueTag).Visible = False
        Dgl1.Columns(Col1DataType).Visible = False
        Dgl1.Columns(Col1DataLength).Visible = False
        Dgl1.Columns(Col1HelpQuery).Visible = False
        Dgl1.Columns(Col1HelpQueryType).Visible = False
        Dgl1.Columns(Col1HelpSelectionType).Visible = False


        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        InitSettingData()
        MovRec()
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click, BtnAdd.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnClose.Name
                Me.Close()
                ClsMain.FCreateSettingDataTable()

            Case BtnAdd.Name
                Dim FrmObj As New FrmSettings_Common_Add()
                FrmObj.StartPosition = FormStartPosition.CenterScreen
                FrmObj.ShowDialog()
                If Not AgL.StrCmp(FrmObj.UserAction, "OK") Then Exit Sub
                MovRec()
        End Select
    End Sub
    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
    End Sub
    Private Sub ProcSave(Code As String, Value As Object)
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = "UPDATE Setting Set Value = " + "'" + Value + "'" + " Where Code = " + "'" + Code + "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Value
                    If Dgl1.Columns(mColumnIndex).Name = Col1Value Then
                        If Dgl1.Item(Col1FieldName, mRowIndex).Value.ToString().Contains("Password") Then
                            Dgl1.Item(Col1ValueTag, mRowIndex).Value = Dgl1.Item(Col1Value, mRowIndex).Value
                            Dgl1.Item(Col1Value, mRowIndex).Value = ""
                            Dgl1.Item(Col1Value, mRowIndex).Value = New String("*", Len(Dgl1.Item(Col1ValueTag, mRowIndex).Value))
                        End If
                    End If


                    If AgL.XNull(Dgl1.Item(Col1ValueTag, mRowIndex).Value) <> "" Then
                        ProcSave(Dgl1.Item(Col1Code, mRowIndex).Value, Dgl1.Item(Col1ValueTag, mRowIndex).Value)
                    Else
                        ProcSave(Dgl1.Item(Col1Code, mRowIndex).Value, Dgl1.Item(Col1Value, mRowIndex).Value)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FillData()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        mQry = " SELECT S.*, Sm.Name AS SiteName, D.Div_Name AS DivisionName, IfNull(Vt.Description,S.VoucherType) AS Voucher_TypeDesc, P.Name As ProcessName , Stg.Name As SettingGroupName 
                    FROM Setting S
                    LEFT JOIN SiteMast Sm ON S.Site_Code = Sm.Code
                    LEFT JOIN Division D ON S.Div_Code = D.Div_Code
                    LEFT JOIN Voucher_Type Vt ON S.VoucherType = Vt.V_Type 
                    LEFT JOIN SubGroup P On S.Process = P.SubCode 
                    LEFT JOIN SettingGroup Stg On S.SettingGroup = Stg.Code "

        If mSettingType <> "" Then
            mQry += " Where SettingType = '" & mSettingType & "'"
        Else
            mQry += " Where SettingType <> 'E Invoice'"
        End If

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I = 0 To DtTemp.Rows.Count - 1
            DtSettingsData.Rows.Add()


            DtSettingsData.Rows(I)(Col1Code) = AgL.XNull(DtTemp.Rows(I)("Code"))
            DtSettingsData.Rows(I)(Col1SettingType) = AgL.XNull(DtTemp.Rows(I)("SettingType"))
            DtSettingsData.Rows(I)(Col1SiteName) = AgL.XNull(DtTemp.Rows(I)("SiteName"))
            DtSettingsData.Rows(I)(Col1DivisionName) = AgL.XNull(DtTemp.Rows(I)("DivisionName"))
            DtSettingsData.Rows(I)(Col1Category) = GetFormattedString(ClsMain.FGetVoucherCategoryDesc(AgL.XNull(DtTemp.Rows(I)("Category"))))
            DtSettingsData.Rows(I)(Col1NCat) = GetFormattedString(ClsMain.FGetNCatDesc(AgL.XNull(DtTemp.Rows(I)("NCat"))))
            DtSettingsData.Rows(I)(Col1VoucherType) = AgL.XNull(DtTemp.Rows(I)("Voucher_TypeDesc"))
            DtSettingsData.Rows(I)(Col1Process) = AgL.XNull(DtTemp.Rows(I)("ProcessName"))
            DtSettingsData.Rows(I)(Col1SettingGroup) = AgL.XNull(DtTemp.Rows(I)("SettingGroupName"))
            DtSettingsData.Rows(I)(Col1FieldName) = AgL.XNull(DtTemp.Rows(I)("FieldName"))
            DtSettingsData.Rows(I)(Col1DataType) = AgL.XNull(DtTemp.Rows(I)("DataType"))
            DtSettingsData.Rows(I)(Col1DataLength) = AgL.XNull(DtTemp.Rows(I)("DataLength"))
            DtSettingsData.Rows(I)(Col1HelpQuery) = AgL.XNull(DtTemp.Rows(I)("HelpQuery"))
            DtSettingsData.Rows(I)(Col1HelpQueryType) = AgL.XNull(DtTemp.Rows(I)("HelpQueryType"))
            DtSettingsData.Rows(I)(Col1HelpSelectionType) = AgL.XNull(DtTemp.Rows(I)("HelpSelectionType"))


            If AgL.XNull(DtSettingsData.Rows(I)(Col1HelpQuery)) <> "" Then
                If AgL.XNull(DtSettingsData.Rows(I)(Col1HelpQueryType)) = AgHelpQueryType.ClassName Then
                    DtSettingsData.Rows(I)(Col1ValueTag) = ""
                    DtSettingsData.Rows(I)(Col1Value) = AgL.XNull(DtTemp.Rows(I)("Value"))
                ElseIf AgL.XNull(DtSettingsData.Rows(I)(Col1HelpQueryType)) = AgHelpQueryType.CSV Then
                    DtSettingsData.Rows(I)(Col1ValueTag) = ""
                    DtSettingsData.Rows(I)(Col1Value) = AgL.XNull(DtTemp.Rows(I)("Value"))
                Else
                    DtSettingsData.Rows(I)(Col1ValueTag) = AgL.XNull(DtTemp.Rows(I)("Value"))
                    Dim DtResult As DataTable = AgL.FillData(DtSettingsData.Rows(I)(Col1HelpQuery), AgL.GCn).Tables(0)
                    Dim DrResultRow As DataRow() = DtResult.Select(DtResult.Columns(0).ColumnName + " In ('" & DtSettingsData.Rows(I)(Col1ValueTag).ToString().Replace("+", "','") & "')")
                    For K As Integer = 0 To DrResultRow.Length - 1
                        If AgL.XNull(DtSettingsData.Rows(I)(Col1HelpSelectionType)) = AgHelpSelectionType.MultiSelect Then
                            DtSettingsData.Rows(I)(Col1Value) += "+" + AgL.XNull(DrResultRow(K)(1))
                        Else
                            DtSettingsData.Rows(I)(Col1Value) = AgL.XNull(DrResultRow(K)(1))
                        End If
                    Next
                End If
            ElseIf AgL.XNull(DtSettingsData.Rows(I)(Col1DataType)) = AgDataType.YesNo Then
                If AgL.XNull(DtTemp.Rows(I)("Value")) = "1" Then
                    DtSettingsData.Rows(I)(Col1Value) = "Yes"
                Else
                    DtSettingsData.Rows(I)(Col1Value) = "No"
                    End If
                Else
                    DtSettingsData.Rows(I)(Col1ValueTag) = ""
                DtSettingsData.Rows(I)(Col1Value) = AgL.XNull(DtTemp.Rows(I)("Value"))
            End If

            If DtSettingsData.Rows(I)(Col1FieldName).ToString().Contains("Password") Then
                DtSettingsData.Rows(I)(Col1ValueTag) = DtSettingsData.Rows(I)(Col1Value)
                DtSettingsData.Rows(I)(Col1Value) = ""
                DtSettingsData.Rows(I)(Col1Value) = New String("*", Len(DtSettingsData.Rows(I)(Col1ValueTag)))
            End If
        Next
        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
    End Sub


    Public Sub MovRec()
        Try
            Dgl1.DataSource = Nothing
            'Dgl1.Rows.Clear()
            FillData()
            Dgl1.DataSource = DtSettingsData
            Ini_Grid()

            For I As Integer = 0 To Dgl1.Columns.Count - 1
                Dim BlankValueColumn As DataRow() = DtSettingsData.Select("[" + Dgl1.Columns(I).Name + "] <> '' ")
                If BlankValueColumn.Length = 0 Then
                    Dgl1.Columns(I).Visible = False
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Down Or
                e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Then
                Exit Sub
            End If

            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            If e.Control Or e.Shift Or e.Alt Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Value
                    If AgL.StrCmp(Dgl1.Item(Col1DataType, Dgl1.CurrentCell.RowIndex).Value, AgDataType.YesNo) Then
                        If AgL.StrCmp(ChrW(e.KeyCode), "Y") Then
                            Dgl1.Item(Col1ValueTag, bRowIndex).Value = 1
                            Dgl1.Item(Col1Value, bRowIndex).Value = "Yes"
                        ElseIf AgL.StrCmp(ChrW(e.KeyCode), "N") Then
                            Dgl1.Item(Col1ValueTag, bRowIndex).Value = 0
                            Dgl1.Item(Col1Value, bRowIndex).Value = "No"
                        End If

                        If AgL.StrCmp(ChrW(e.KeyCode), "Y") Or AgL.StrCmp(ChrW(e.KeyCode), "N") Then
                            If Dgl1.Item(Col1ValueTag, bRowIndex).Value = -1 Then
                                Dgl1.Item(Col1ValueTag, bRowIndex).Value = 1
                            End If
                        End If


                        If Dgl1.Item(Col1ValueTag, bRowIndex).Value IsNot Nothing Then
                            ProcSave(Dgl1.Item(Col1Code, bRowIndex).Value, Dgl1.Item(Col1ValueTag, bRowIndex).Value)
                        Else
                            ProcSave(Dgl1.Item(Col1Code, bRowIndex).Value, Dgl1.Item(Col1Value, bRowIndex).Value)
                        End If
                    Else
                        FShowSingleHelp(bRowIndex, bColumnIndex)

                        If Dgl1.Item(Col1ValueTag, bRowIndex).Value IsNot Nothing Then
                            ProcSave(Dgl1.Item(Col1Code, bRowIndex).Value, Dgl1.Item(Col1ValueTag, bRowIndex).Value)
                        Else
                            ProcSave(Dgl1.Item(Col1Code, bRowIndex).Value, Dgl1.Item(Col1Value, bRowIndex).Value)
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub
    Private Function GetCodeColumns(mTableName As String) As String
        Dim mRetStr = ""

        Select Case UCase(mTableName)
            Case UCase("AcGroup")
                mRetStr = " GroupCode "
            Case UCase("SubGroup")
                mRetStr = " SubCode "
            Case UCase("SubGroupType")
                mRetStr = " SubgroupType "
            Case UCase("PostingGroupSalesTaxParty")
                mRetStr = " Description "
            Case UCase("PostingGroupSalesTaxItem")
                mRetStr = " Description "
            Case Else
                mRetStr = " Code "
        End Select
        GetCodeColumns = mRetStr
    End Function
    Private Function GetDescriptionColumns(mTableName As String) As String
        Dim mRetStr = ""
        Select Case UCase(mTableName)
            Case UCase("AcGroup")
                mRetStr = " GroupName "
            Case UCase("ItemType")
                mRetStr = " Name "
            Case UCase("SubGroup")
                mRetStr = " Name "
            Case UCase("SubGroupType")
                mRetStr = " SubgroupType "
            Case Else
                mRetStr = " Description "
        End Select
        GetDescriptionColumns = mRetStr
    End Function

    Private Sub FrmSettings_Common_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            ClsMain.FCreateSettingDataTable()
        End If
    End Sub

    Private Function GetFormattedString(FieldName As String)
        Dim FieldNameArr As MatchCollection = Regex.Matches(FieldName.Trim(), "[A-Z][a-z]+")
        Dim strFieldName As String = ""
        For J As Integer = 0 To FieldNameArr.Count - 1
            If strFieldName = "" Then
                strFieldName = FieldNameArr(J).ToString
            Else
                strFieldName += " " + FieldNameArr(J).ToString
            End If
        Next
        If strFieldName <> "" Then
            If strFieldName.ToUpper().Trim().Replace(" ", "").Replace("_", "") <> FieldName.ToUpper().Trim().Replace(" ", "").Replace("_", "") Then
                Return FieldName
            Else
                Return strFieldName
            End If
        Else
            Return FieldName
        End If
    End Function
    Private Sub FGetOtherHelpLists()
        If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
            Select Case Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim
                Case "DiscountCalculationPattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(DiscountCalculationPattern)), AgL.GCn)
                Case "BarcodePattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(BarcodePattern)), AgL.GCn)
                Case "BarcodeType"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(BarcodeType)), AgL.GCn)
                Case "DiscountSuggestionPattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(DiscountSuggestPattern)), AgL.GCn)
                Case "IndustryType"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(IndustryType)), AgL.GCn)
                Case "PlaceOfSupplay"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(PlaceOfSupplay)), AgL.GCn)
                Case "SaleInvoicePattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(SaleInvoicePattern)), AgL.GCn)
                Case "SubgroupRegistrationType"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(SubgroupRegistrationType)), AgL.GCn)
                Case "ActionOnDuplicateItem"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(ActionOnDuplicateItem)), AgL.GCn)
                Case "ActionIfCreditLimitExceeds"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(ActionIfCreditLimitExceeds)), AgL.GCn)
                Case "LedgerPostingPartyAcType"
                    If AgL.StrCmp(Dgl1.Item(Col1SettingType, Dgl1.CurrentCell.RowIndex).Value, "SaleInvoiceSetting") Then
                        Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(SaleInvoiceLedgerPostingPartyAcType)), AgL.GCn)
                    ElseIf AgL.StrCmp(Dgl1.Item(Col1SettingType, Dgl1.CurrentCell.RowIndex).Value, "PurchaseInvoiceSetting") Then
                        Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(PurchInvoiceLedgerPostingPartyAcType)), AgL.GCn)
                    End If
                Case "ActionIfMaximumCashTransactionLimitExceeds"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(ClsMain.ActionsOfMaximumCashTransactionLimitExceeds)), AgL.GCn)
                Case "LrGenerationPattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(LrGenerationPattern)), AgL.GCn)
            End Select

            If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("DiscountCalculationPattern") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(DiscountCalculationPattern)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("BarcodePattern") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(BarcodePattern)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("BarcodeType") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(BarcodeType)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("DiscountSuggestionPattern") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(DiscountSuggestPattern)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("IndustryType") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(IndustryType)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("PlaceOfSupplay") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(PlaceOfSupplay)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("SaleInvoicePattern") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(SaleInvoicePattern)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("SubgroupRegistrationType") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(SubgroupRegistrationType)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("ActionOnDuplicateItem") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(ActionOnDuplicateItem)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("ActionIfCreditLimitExceeds") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(ActionOnDuplicateItem)), AgL.GCn)
                End If
            End If
        End If
    End Sub
    Function GetStringsFromClassConstants(ByVal type As System.Type) As String
        Dim constants As New ArrayList()
        Dim fieldInfos As FieldInfo() =
            type.GetFields(BindingFlags.[Public] Or
                           BindingFlags.[Static] Or
                           BindingFlags.FlattenHierarchy)
        For Each fi As FieldInfo In fieldInfos
            If fi.IsLiteral AndAlso Not fi.IsInitOnly Then
                constants.Add(fi)
            End If
        Next
        Dim ConstantsStringArray As New System.Collections.Specialized.StringCollection
        For Each fi As FieldInfo In DirectCast(constants.ToArray(GetType(FieldInfo)), FieldInfo())
            ConstantsStringArray.Add(CStr(fi.GetValue(Nothing)))
        Next
        Dim retVal(ConstantsStringArray.Count - 1) As String
        ConstantsStringArray.CopyTo(retVal, 0)

        Dim bStrQry = ""
        For I As Integer = 0 To retVal.Length - 1
            If bStrQry <> "" Then bStrQry += " UNION ALL "
            bStrQry += "Select '" & retVal(I) & "' As Code, '" & retVal(I) & "' As Description "
        Next
        Return bStrQry
    End Function
    Private Sub Dgl1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dgl1.KeyPress
        Try
            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then Exit Sub
            End If

            If e.KeyChar = vbCr Or e.KeyChar = vbCrLf Or e.KeyChar = vbTab Or e.KeyChar = ChrW(27) Then Exit Sub

            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = "Tick" Then Exit Sub
                fld = Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End If

            If Dgl1.CurrentCell Is Nothing Then
                DtSettingsData.DefaultView.RowFilter = Nothing
            End If

            If Asc(e.KeyChar) = Keys.Back Then
                If TxtFind.Text <> "" Then TxtFind.Text = Microsoft.VisualBasic.Left(TxtFind.Text, Len(TxtFind.Text) - 1)
            End If

            FManageFindTextboxVisibility()

            TxtFind_KeyPress(TxtFind, e)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtFind_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFind.KeyPress
        RowsFilter(HlpSt, Dgl1, sender, e, fld, DtSettingsData)
    End Sub

    Private Function RowsFilter(ByVal selStr As String, ByVal CtrlObj As Object, ByVal TXT As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal FndFldName As String, ByVal DTable As DataTable) As Integer
        Try
            Dim strExpr As String, findStr As String, bSelStr As String = ""
            Dim sa As String
            Dim IntRow As Integer
            Dim i As Integer
            sa = TXT.Text
            bSelStr = selStr

            If sa.Length = 0 And Asc(e.KeyChar) = 8 Then IntRow = 0 : CtrlObj.CurrentCell = CtrlObj(FndFldName, IntRow) : DtSettingsData.DefaultView.RowFilter = Nothing : Dgl1.CurrentCell = Dgl1(FndFldName, 0) : Exit Function
            If TXT.Text = "(null)" Then
                findStr = e.KeyChar
            Else
                findStr = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, TXT.Text, TXT.Text + e.KeyChar)
            End If
            strExpr = "ltrim([" & FndFldName & "])  like '" & findStr & "%' "
            i = InStr(selStr, "where", CompareMethod.Text)
            If i = 0 Then
                selStr = selStr + " where " + strExpr + "order by [" & FndFldName & "]"
            Else
                selStr = selStr + " and " + strExpr + "order by [" & FndFldName & "]"
            End If

            ''==================================< Filter DTFind For Searching >====================================================
            DtSettingsData.DefaultView.RowFilter = Nothing
            'DtSettingsData.DefaultView.RowFilter = " [" & FndFldName & "] like '%" & findStr & "%' "
            If DtSettingsData.DefaultView.RowFilter <> "" And DtSettingsData.DefaultView.RowFilter <> Nothing Then
                DtSettingsData.DefaultView.RowFilter += " And " + " [" & FndFldName & "] like '" & findStr & "%' "
            Else
                DtSettingsData.DefaultView.RowFilter += " [" & FndFldName & "] like '" & findStr & "%' "
            End If
            Try
                Dgl1.CurrentCell = Dgl1(FndFldName, 0)
            Catch ex As Exception
            End Try
            TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)

            FManageFindTextboxVisibility()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub DGL1_Click(sender As Object, e As EventArgs) Handles Dgl1.Click
        TxtFind.Text = ""
        FManageFindTextboxVisibility()
    End Sub
    Private Sub DGL1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles Dgl1.PreviewKeyDown
        If Dgl1.CurrentCell Is Nothing Then Exit Sub

        If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Value Then
            If e.KeyCode = Keys.Delete And
                    Not AgL.StrCmp(Dgl1.Item(Col1DataType, Dgl1.CurrentCell.RowIndex).Value, AgDataType.YesNo) Then
                Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value = ""
                Dgl1.Item(Col1ValueTag, Dgl1.CurrentCell.RowIndex).Value = ""
                ProcSave(Dgl1.Item(Col1Code, Dgl1.CurrentCell.RowIndex).Value, Dgl1.Item(Col1ValueTag, Dgl1.CurrentCell.RowIndex).Value)
            End If
        Else
            If e.KeyCode = Keys.Delete Then
                TxtFind.Text = ""
                FManageFindTextboxVisibility()
                DtSettingsData.DefaultView.RowFilter = Nothing
                Dgl1.CurrentCell = Dgl1(fld, 0)
                DtSettingsData.DefaultView.RowFilter = Nothing
            End If
        End If
    End Sub
    Private Sub FManageFindTextboxVisibility()
        If TxtFind.Text = "" Then TxtFind.Visible = False : TxtFind.Visible = True
    End Sub
    Private Sub Dgl1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Dgl1.DataBindingComplete
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1HelpSelectionType, I).Value) = AgHelpSelectionType.MultiSelect Or
                    AgL.StrCmp(Dgl1.Item(Col1DataType, I).Value, "Bit") Then
                Dgl1.Item(Col1Value, I).ReadOnly = True
            End If
        Next

        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)

        'FAddButtonColumn()
    End Sub
    Private Sub FShowSingleHelp(bRowIndex As Integer, bColumnIndex As Integer)
        If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub
        Dim bHelpQry As String = ""

        Select Case Dgl1.Columns(bColumnIndex).Name
            Case Col1Value
                If AgL.XNull(Dgl1.Item(Col1HelpQuery, Dgl1.CurrentCell.RowIndex).Value) <> "" Then
                    If AgL.XNull(Dgl1.Item(Col1HelpQueryType, Dgl1.CurrentCell.RowIndex).Value) = AgHelpQueryType.ClassName Then
                        Dim bAssemblyQualifiedName As String = "AgLibrary.ClsMain+agConstants+" & AgL.XNull(Dgl1.Item(Col1HelpQuery, Dgl1.CurrentCell.RowIndex).Value) & ", AgLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                        bHelpQry = GetStringsFromClassConstants(Type.GetType(bAssemblyQualifiedName))
                    ElseIf AgL.XNull(Dgl1.Item(Col1HelpQueryType, Dgl1.CurrentCell.RowIndex).Value) = AgHelpQueryType.CSV Then
                        Dim CsvArr() As String = AgL.XNull(Dgl1.Item(Col1HelpQuery, Dgl1.CurrentCell.RowIndex).Value).ToString.Split(",")
                        Dim bCsvQry As String = ""
                        For I As Integer = 0 To CsvArr.Length - 1
                            If bCsvQry <> "" Then bCsvQry += " UNION ALL "
                            bCsvQry += " Select " + AgL.Chk_Text(CsvArr(I).ToString) + " As Code, " + AgL.Chk_Text(CsvArr(I).ToString) + " As Description "
                        Next
                        bHelpQry = bCsvQry
                    Else
                        bHelpQry = AgL.XNull(Dgl1.Item(Col1HelpQuery, Dgl1.CurrentCell.RowIndex).Value)
                    End If
                End If
        End Select

        If bHelpQry <> "" Then
            If AgL.XNull(Dgl1.Item(Col1HelpSelectionType, Dgl1.CurrentCell.RowIndex).Value) = AgHelpSelectionType.MultiSelect Then
                bHelpQry = bHelpQry.ToString.ToUpper.Replace("SELECT ", "Select 'o' As Tick, ")
                Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(bHelpQry, AgL.GCn)

                Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
                FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CType(Dgl1.Item(Col1FieldName, bRowIndex).Tag, DataSet).Tables(0)), "", 400, 400, , , False)
                FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
                FRH_Multiple.FFormatColumn(1, , 0, , False)
                FRH_Multiple.FFormatColumn(2, "Description", 250, DataGridViewContentAlignment.MiddleLeft)
                FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
                FRH_Multiple.ShowDialog()

                If FRH_Multiple.BytBtnValue = 0 Then
                    If FRH_Multiple.FFetchData(1, "'", "'", "+", True) <> "" Then
                        Dgl1.Item(Col1ValueTag, bRowIndex).Value = "+" + FRH_Multiple.FFetchData(1, "", "", "+", True)
                        Dgl1.Item(Col1Value, bRowIndex).Value = "+" + FRH_Multiple.FFetchData(2, "", "", "+", True)
                    Else
                        Dgl1.Item(Col1ValueTag, bRowIndex).Value = ""
                        Dgl1.Item(Col1Value, bRowIndex).Value = ""
                    End If
                End If
            Else
                Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(bHelpQry, AgL.GCn)

                Dim FRH_Single As DMHelpGrid.FrmHelpGrid
                FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(CType(Dgl1.Item(Col1FieldName, bRowIndex).Tag, DataSet).Tables(0)), "", 350, 300, 150, 520, False)
                FRH_Single.FFormatColumn(0, , 0, , False)
                FRH_Single.FFormatColumn(1, "Description", 200, DataGridViewContentAlignment.MiddleLeft)
                FRH_Single.StartPosition = FormStartPosition.Manual
                FRH_Single.ShowDialog()

                If FRH_Single.BytBtnValue = 0 Then
                    Dgl1.Item(Col1ValueTag, bRowIndex).Value = FRH_Single.DRReturn(0)
                    Dgl1.Item(Col1Value, bRowIndex).Value = FRH_Single.DRReturn(1)
                End If
            End If
        ElseIf AgL.XNull(Dgl1.Item(Col1HelpSelectionType, bRowIndex).Value) = AgHelpSelectionType.MultiSelect Or
                    AgL.StrCmp(Dgl1.Item(Col1DataType, bRowIndex).Value, "Bit") Then
            Dgl1.Item(Col1Value, bRowIndex).ReadOnly = True
        Else
            Dgl1.Item(Col1Value, bRowIndex).ReadOnly = False
        End If
    End Sub
End Class