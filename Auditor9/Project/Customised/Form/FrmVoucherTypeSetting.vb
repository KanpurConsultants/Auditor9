Imports System.Data.SQLite
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports AgLibrary.ClsMain.agConstants

Public Class FrmVoucherTypeSetting
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const Col1Code As String = "Code"
    Protected Const Col1Category As String = "Category"
    Protected Const Col1NCat As String = "NCat"
    Protected Const Col1VoucherType As String = "Voucher Type"
    Protected Const Col1SiteName As String = "Site"
    Protected Const Col1DivisionName As String = "Division"
    Protected Const Col1LockTillDate As String = "Lock Till Date"

    Dim mQry As String = ""
    Dim mGridRowNumber As Integer = 0

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String

    Dim DtSettingsData As New DataTable

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub InitSettingData()
        DtSettingsData.Columns.Add(Col1Code)
        DtSettingsData.Columns.Add(Col1Category)
        DtSettingsData.Columns.Add(Col1NCat)
        DtSettingsData.Columns.Add(Col1VoucherType)
        DtSettingsData.Columns.Add(Col1SiteName)
        DtSettingsData.Columns.Add(Col1DivisionName)
        DtSettingsData.Columns.Add(Col1LockTillDate)
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

        Dgl1.Columns(Col1Category).Width = 200
        Dgl1.Columns(Col1NCat).Width = 140
        Dgl1.Columns(Col1VoucherType).Width = 200
        Dgl1.Columns(Col1SiteName).Width = 230
        Dgl1.Columns(Col1DivisionName).Width = 75
        Dgl1.Columns(Col1LockTillDate).Width = 130

        Dgl1.Columns(Col1Category).ReadOnly = True
        Dgl1.Columns(Col1NCat).ReadOnly = True
        Dgl1.Columns(Col1VoucherType).ReadOnly = True
        Dgl1.Columns(Col1SiteName).ReadOnly = True
        Dgl1.Columns(Col1DivisionName).ReadOnly = True

        Dgl1.Columns(Col1Code).Visible = False


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
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnClose.Name
                Me.Close()
        End Select
    End Sub
    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
    End Sub
    Private Sub ProcSave(TableName As String, Code As String, FieldName As String, Value As Object)
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

            mQry = "UPDATE " + TableName + " Set " + FieldName + " = " + "" + Value.ToString + "" + " Where Code = " + "'" + Code + "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub MovRec()
        GetSettingDataForTable("EntryHeaderUISetting", "Header")

        Dgl1.DataSource = DtSettingsData
        Ini_Grid()

        Dim DtRow_NCat As DataRow() = DtSettingsData.Select("[" + Col1NCat + "]" + " <> '' ")
        If DtRow_NCat.Length = 0 Then Dgl1.Columns(Col1NCat).Visible = False

        Dim DtRow_VoucherType As DataRow() = DtSettingsData.Select("[" + Col1VoucherType + "]" + " <> '' ")
        If DtRow_VoucherType.Length = 0 Then Dgl1.Columns(Col1VoucherType).Visible = False

        Dim DtRow_SiteName As DataRow() = DtSettingsData.Select("[" + Col1SiteName + "]" + " <> '' ")
        If DtRow_SiteName.Length = 0 Then Dgl1.Columns(Col1SiteName).Visible = False

        Dim DtRow_DivisionName As DataRow() = DtSettingsData.Select("[" + Col1DivisionName + "]" + " <> '' ")
        If DtRow_DivisionName.Length = 0 Then Dgl1.Columns(Col1DivisionName).Visible = False
    End Sub
    Public Sub GetSettingDataForTable(mTableName As String, mTableDispName As String)
        Dim I As Integer = 0

        mQry = "SELECT H.Code As [Code], 
                S.Name AS [Site], D.Div_Name AS [Division], H.Category, H.NCat As [NCat], 
                Vt.Description AS [Voucher Type], H.LockTillDate As [Lock Till Date]
                FROM VoucherTypeSetting H  
                LEFT JOIN SiteMast S ON H.Site_Code = S.Code
                LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                DtSettingsData.Rows.Add()
                DtSettingsData.Rows(mGridRowNumber)(Col1Code) = AgL.XNull(DtTemp.Rows(I)("Code"))
                DtSettingsData.Rows(mGridRowNumber)(Col1SiteName) = AgL.XNull(DtTemp.Rows(I)("Site"))
                DtSettingsData.Rows(mGridRowNumber)(Col1DivisionName) = AgL.XNull(DtTemp.Rows(I)("Division"))
                DtSettingsData.Rows(mGridRowNumber)(Col1Category) = GetFormattedString(FGetCategoryDesc(AgL.XNull(DtTemp.Rows(I)("Category"))))
                DtSettingsData.Rows(mGridRowNumber)(Col1NCat) = GetFormattedString(FGetNCatDesc(AgL.XNull(DtTemp.Rows(I)("NCat"))))
                DtSettingsData.Rows(mGridRowNumber)(Col1VoucherType) = AgL.XNull(DtTemp.Rows(I)("Voucher Type"))
                DtSettingsData.Rows(mGridRowNumber)(Col1LockTillDate) = AgL.XNull(DtTemp.Rows(I)("Lock Till Date"))
                mGridRowNumber += 1
            Next
        End If
    End Sub
    Private Function FGetNCatDesc(bNCat As String)
        Dim NCatDesc As String
        Select Case bNCat
            Case Ncat.CreditNote
                NCatDesc = "CreditNote"
            Case Ncat.DebitNote
                NCatDesc = "DebitNote"
            Case Ncat.JournalVoucher
                NCatDesc = "JournalVoucher"
            Case Ncat.LrEntry
                NCatDesc = "LrEntry"
            Case Ncat.LrTransfer
                NCatDesc = "LrTransfer"
            Case Ncat.OpeningStock
                NCatDesc = "OpeningStock"
            Case Ncat.Payment
                NCatDesc = "Payment"
            Case Ncat.PurchaseDelivery
                NCatDesc = "PurchaseDelivery"
            Case Ncat.PurchaseInvoice
                NCatDesc = "PurchaseInvoice"
            Case Ncat.PurchaseOrder
                NCatDesc = "PurchaseOrder"
            Case Ncat.PurchaseReturn
                NCatDesc = "PurchaseReturn"
            Case Ncat.Receipt
                NCatDesc = "Receipt"
            Case Ncat.SaleDelivery
                NCatDesc = "SaleDelivery"
            Case Ncat.SaleInvoice
                NCatDesc = "SaleInvoice"
            Case Ncat.SaleOrder
                NCatDesc = "SaleOrder"
            Case Ncat.SaleOrderCancel
                NCatDesc = "SaleOrderCancel"
            Case Ncat.SaleReturn
                NCatDesc = "SaleReturn"
            Case Ncat.StockTransfer
                NCatDesc = "StockTransfer"
            Case ItemTypeCode.TradingProduct
                NCatDesc = "TradingProduct"
            Case Else
                NCatDesc = bNCat
        End Select

        Return NCatDesc
    End Function
    Private Function FGetCategoryDesc(bCategory As String)
        Dim bCategoryDesc As String
        Select Case bCategory
            Case Ncat.CreditNote
                bCategoryDesc = "CreditNote"
            Case Ncat.DebitNote
                bCategoryDesc = "DebitNote"
            Case Ncat.JournalVoucher
                bCategoryDesc = "JournalVoucher"
            Case Ncat.LrEntry
                bCategoryDesc = "LrEntry"
            Case Ncat.LrTransfer
                bCategoryDesc = "LrTransfer"
            Case Ncat.OpeningStock
                bCategoryDesc = "OpeningStock"
            Case Ncat.Payment
                bCategoryDesc = "Payment"
            Case Ncat.PurchaseDelivery
                bCategoryDesc = "PurchaseDelivery"
            Case Ncat.PurchaseInvoice
                bCategoryDesc = "PurchaseInvoice"
            Case Ncat.PurchaseOrder
                bCategoryDesc = "PurchaseOrder"
            Case Ncat.PurchaseReturn
                bCategoryDesc = "PurchaseReturn"
            Case Ncat.Receipt
                bCategoryDesc = "Receipt"
            Case Ncat.SaleDelivery
                bCategoryDesc = "SaleDelivery"
            Case Ncat.SaleInvoice
                bCategoryDesc = "SaleInvoice"
            Case Ncat.SaleOrder
                bCategoryDesc = "SaleOrder"
            Case Ncat.SaleOrderCancel
                bCategoryDesc = "SaleOrderCancel"
            Case Ncat.SaleReturn
                bCategoryDesc = "SaleReturn"
            Case Ncat.StockTransfer
                bCategoryDesc = "StockTransfer"
            Case ItemTypeCode.TradingProduct
                bCategoryDesc = "TradingProduct"
            Case Else
                bCategoryDesc = bCategory
        End Select

        Return bCategoryDesc
    End Function
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1LockTillDate).Index Then
                Exit Sub
            End If


            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub
    Private Sub FrmVoucherTypeSetting_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
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
    Private Sub Dgl1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dgl1.KeyPress
        Try
            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1LockTillDate).Index Then
                    Exit Sub
                End If
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
        If e.KeyCode = Keys.Delete Then TxtFind.Text = "" : FManageFindTextboxVisibility() : DtSettingsData.DefaultView.RowFilter = Nothing : Dgl1.CurrentCell = Dgl1(fld, 0) : DtSettingsData.DefaultView.RowFilter = Nothing
    End Sub
    Private Sub FManageFindTextboxVisibility()
        If TxtFind.Text = "" Then TxtFind.Visible = False : TxtFind.Visible = True
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
                Case Col1LockTillDate
                    If AgL.XNull(Dgl1.Item(Col1LockTillDate, mRowIndex).Value) <> "" Then
                        Dgl1.Item(mColumnIndex, mRowIndex).Value = AgL.RetDate(Dgl1.Item(mColumnIndex, mRowIndex).Value)
                    End If
                    ProcSave("VoucherTypeSetting", Dgl1.Item(Col1Code, mRowIndex).Value,
                        "LockTillDate", AgL.Chk_Date(AgL.XNull(Dgl1.Item(Col1LockTillDate, mRowIndex).Value)))
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Dgl1.DataBindingComplete
        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
    End Sub
End Class