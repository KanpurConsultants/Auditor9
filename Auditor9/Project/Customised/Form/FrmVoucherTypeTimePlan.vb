Imports System.Data.SQLite
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports AgLibrary.ClsMain.agConstants

Public Class FrmVoucherTypeTimePlan
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Public WithEvents Dgl3 As New AgControls.AgDataGrid

    Protected Const Col1Code As String = "Code"
    Protected Const Col1Category As String = "Category"
    Protected Const Col1NCat As String = "NCat"
    Protected Const Col1VoucherType As String = "Voucher Type"
    Protected Const Col1SiteName As String = "Site"
    Protected Const Col1DivisionName As String = "Division"
    Protected Const Col1LockTillDate As String = "Lock Till Date"


    Protected Const Col2Code As String = "Code"
    Protected Const Col2Category As String = "Category"
    Protected Const Col2NCat As String = "NCat"
    Protected Const Col2VoucherType As String = "Voucher Type"
    Protected Const Col2SiteName As String = "Site"
    Protected Const Col2DivisionName As String = "Division"
    Protected Const Col2DayLimitAdd As String = "Day Limit Add"
    Protected Const Col2DayLimitEdit As String = "Day Limit Edit"
    Protected Const Col2DayLimitDelete As String = "Day Limit Delete"
    Protected Const Col2DayLimitPrint As String = "Day Limit Print"

    Protected Const Col3Code As String = "Code"
    Protected Const Col3Company As String = "Company"
    Protected Const Col3SiteName As String = "Site"
    Protected Const Col3DivisionName As String = "Division"
    Protected Const Col3IsLocked As String = "Is Locked"

    Dim mQry As String = ""

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String

    Dim DtVoucherTypeDateLockData As New DataTable
    Dim DtVoucherTypeTimePlanData As New DataTable
    Dim DtFinancialYearTimePlanData As New DataTable
    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub InitSettingData()
        DtVoucherTypeDateLockData.Columns.Add(Col1Code)
        DtVoucherTypeDateLockData.Columns.Add(Col1Category)
        DtVoucherTypeDateLockData.Columns.Add(Col1NCat)
        DtVoucherTypeDateLockData.Columns.Add(Col1VoucherType)
        DtVoucherTypeDateLockData.Columns.Add(Col1SiteName)
        DtVoucherTypeDateLockData.Columns.Add(Col1DivisionName)
        DtVoucherTypeDateLockData.Columns.Add(Col1LockTillDate)

        DtVoucherTypeTimePlanData.Columns.Add(Col2Code)
        DtVoucherTypeTimePlanData.Columns.Add(Col2Category)
        DtVoucherTypeTimePlanData.Columns.Add(Col2NCat)
        DtVoucherTypeTimePlanData.Columns.Add(Col2VoucherType)
        DtVoucherTypeTimePlanData.Columns.Add(Col2SiteName)
        DtVoucherTypeTimePlanData.Columns.Add(Col2DivisionName)
        DtVoucherTypeTimePlanData.Columns.Add(Col2DayLimitAdd)
        DtVoucherTypeTimePlanData.Columns.Add(Col2DayLimitEdit)
        DtVoucherTypeTimePlanData.Columns.Add(Col2DayLimitDelete)
        DtVoucherTypeTimePlanData.Columns.Add(Col2DayLimitPrint)

        DtFinancialYearTimePlanData.Columns.Add(Col3Code)
        DtFinancialYearTimePlanData.Columns.Add(Col3Company)
        DtFinancialYearTimePlanData.Columns.Add(Col3SiteName)
        DtFinancialYearTimePlanData.Columns.Add(Col3DivisionName)
        DtFinancialYearTimePlanData.Columns.Add(Col3IsLocked)
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
        Dgl1.Name = "Dgl1"

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



        Dgl2.ColumnHeadersHeight = 40

        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.AgAllowFind = False
        Dgl2.AllowUserToOrderColumns = True
        Dgl2.AgAllowFind = False


        Dgl2.AllowUserToAddRows = False
        Dgl2.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl2)
        Dgl2.Name = "Dgl2"

        Dgl2.Columns(Col2Category).Width = 200
        Dgl2.Columns(Col2NCat).Width = 140
        Dgl2.Columns(Col2VoucherType).Width = 200
        Dgl2.Columns(Col2SiteName).Width = 130
        Dgl2.Columns(Col2DivisionName).Width = 75
        Dgl2.Columns(Col2DayLimitAdd).Width = 120
        Dgl2.Columns(Col2DayLimitEdit).Width = 120
        Dgl2.Columns(Col2DayLimitDelete).Width = 120
        Dgl2.Columns(Col2DayLimitPrint).Width = 120

        Dgl2.Columns(Col2DayLimitAdd).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl2.Columns(Col2DayLimitEdit).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl2.Columns(Col2DayLimitDelete).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl2.Columns(Col2DayLimitPrint).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight


        Dgl2.Columns(Col2DayLimitAdd).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl2.Columns(Col2DayLimitEdit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl2.Columns(Col2DayLimitDelete).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl2.Columns(Col2DayLimitPrint).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        Dgl2.Columns(Col2Category).ReadOnly = True
        Dgl2.Columns(Col2NCat).ReadOnly = True
        Dgl2.Columns(Col2VoucherType).ReadOnly = True
        Dgl2.Columns(Col2SiteName).ReadOnly = True
        Dgl2.Columns(Col2DivisionName).ReadOnly = True

        Dgl2.Columns(Col2Code).Visible = False

        Dgl2.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top









        Dgl3.ColumnHeadersHeight = 40

        Dgl3.AgSkipReadOnlyColumns = True
        Dgl3.AgAllowFind = False
        Dgl3.AllowUserToOrderColumns = True
        Dgl3.AgAllowFind = False


        Dgl3.AllowUserToAddRows = False
        Dgl3.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl3)
        Dgl3.Name = "Dgl3"

        Dgl3.Columns(Col3Company).Width = 200
        Dgl3.Columns(Col3SiteName).Width = 230
        Dgl3.Columns(Col3DivisionName).Width = 75
        Dgl3.Columns(Col3IsLocked).Width = 230

        Dgl3.Columns(Col3Company).ReadOnly = True
        Dgl3.Columns(Col3SiteName).ReadOnly = True
        Dgl3.Columns(Col3DivisionName).ReadOnly = True
        Dgl3.Columns(Col3IsLocked).ReadOnly = True

        Dgl3.Columns(Col3Code).Visible = False


        Dgl3.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top





        Dim DtRow_NCat_DateLock As DataRow() = DtVoucherTypeDateLockData.Select("[" + Col1NCat + "]" + " <> '' ")
        If DtRow_NCat_DateLock.Length = 0 Then Dgl1.Columns(Col1NCat).Visible = False

        Dim DtRow_VoucherType_DateLock As DataRow() = DtVoucherTypeDateLockData.Select("[" + Col1VoucherType + "]" + " <> '' ")
        If DtRow_VoucherType_DateLock.Length = 0 Then Dgl1.Columns(Col1VoucherType).Visible = False

        Dim DtRow_SiteName_DateLock As DataRow() = DtVoucherTypeDateLockData.Select("[" + Col1SiteName + "]" + " <> '' ")
        If DtRow_SiteName_DateLock.Length = 0 Then Dgl1.Columns(Col1SiteName).Visible = False

        Dim DtRow_DivisionName_DateLock As DataRow() = DtVoucherTypeDateLockData.Select("[" + Col1DivisionName + "]" + " <> '' ")
        If DtRow_DivisionName_DateLock.Length = 0 Then Dgl1.Columns(Col1DivisionName).Visible = False

        Dim DtRow_NCat_TimePlan As DataRow() = DtVoucherTypeTimePlanData.Select("[" + Col2NCat + "]" + " <> '' ")
        If DtRow_NCat_TimePlan.Length = 0 Then Dgl2.Columns(Col2NCat).Visible = False

        Dim DtRow_VoucherType_TimePlan As DataRow() = DtVoucherTypeTimePlanData.Select("[" + Col2VoucherType + "]" + " <> '' ")
        If DtRow_VoucherType_TimePlan.Length = 0 Then Dgl2.Columns(Col2VoucherType).Visible = False

        Dim DtRow_SiteName_TimePlan As DataRow() = DtVoucherTypeTimePlanData.Select("[" + Col2SiteName + "]" + " <> '' ")
        If DtRow_SiteName_TimePlan.Length = 0 Then Dgl2.Columns(Col2SiteName).Visible = False

        Dim DtRow_DivisionName_TimePlan As DataRow() = DtVoucherTypeTimePlanData.Select("[" + Col2DivisionName + "]" + " <> '' ")
        If DtRow_DivisionName_TimePlan.Length = 0 Then Dgl2.Columns(Col2DivisionName).Visible = False

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & Dgl2.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl2, False)
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        InitSettingData()
        FGetDateLockData()
        FGetTimePlanData()
        FGetFinancialYearLockData()
        Ini_Grid()
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
    Public Sub FGetDateLockData()
        mQry = "SELECT H.Code As [Code], 
                S.Name AS [Site], D.Div_Name AS [Division], H.Category, H.NCat As [NCat], 
                Vt.Description AS [Voucher Type], H.LockTillDate As [Lock Till Date]
                FROM VoucherTypeDateLock H  
                LEFT JOIN SiteMast S ON H.Site_Code = S.Code
                LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I As Integer = 0 To DtTemp.Rows.Count - 1
                DtVoucherTypeDateLockData.Rows.Add()
                DtVoucherTypeDateLockData.Rows(I)(Col1Code) = AgL.XNull(DtTemp.Rows(I)("Code"))
                DtVoucherTypeDateLockData.Rows(I)(Col1SiteName) = AgL.XNull(DtTemp.Rows(I)("Site"))
                DtVoucherTypeDateLockData.Rows(I)(Col1DivisionName) = AgL.XNull(DtTemp.Rows(I)("Division"))
                DtVoucherTypeDateLockData.Rows(I)(Col1Category) = GetFormattedString(ClsMain.FGetVoucherCategoryDesc(AgL.XNull(DtTemp.Rows(I)("Category"))))
                DtVoucherTypeDateLockData.Rows(I)(Col1NCat) = GetFormattedString(ClsMain.FGetNCatDesc(AgL.XNull(DtTemp.Rows(I)("NCat"))))
                DtVoucherTypeDateLockData.Rows(I)(Col1VoucherType) = AgL.XNull(DtTemp.Rows(I)("Voucher Type"))
                DtVoucherTypeDateLockData.Rows(I)(Col1LockTillDate) = AgL.XNull(DtTemp.Rows(I)("Lock Till Date"))
            Next
        End If

        Dgl1.DataSource = DtVoucherTypeDateLockData
    End Sub

    Public Sub FGetTimePlanData()
        mQry = "SELECT H.Code As [Code], 
                S.Name AS [Site], D.Div_Name AS [Division], H.Category, H.NCat As [NCat], 
                Vt.Description AS [Voucher Type], H.DayLimitAdd, H.DayLimitEdit, H.DayLimitDelete, H.DayLimitPrint 
                FROM VoucherTypeTimePlan H  
                LEFT JOIN SiteMast S ON H.Site_Code = S.Code
                LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I As Integer = 0 To DtTemp.Rows.Count - 1
                DtVoucherTypeTimePlanData.Rows.Add()
                DtVoucherTypeTimePlanData.Rows(I)(Col2Code) = AgL.XNull(DtTemp.Rows(I)("Code"))
                DtVoucherTypeTimePlanData.Rows(I)(Col2SiteName) = AgL.XNull(DtTemp.Rows(I)("Site"))
                DtVoucherTypeTimePlanData.Rows(I)(Col2DivisionName) = AgL.XNull(DtTemp.Rows(I)("Division"))
                DtVoucherTypeTimePlanData.Rows(I)(Col2Category) = GetFormattedString(ClsMain.FGetVoucherCategoryDesc(AgL.XNull(DtTemp.Rows(I)("Category"))))
                DtVoucherTypeTimePlanData.Rows(I)(Col2NCat) = GetFormattedString(ClsMain.FGetNCatDesc(AgL.XNull(DtTemp.Rows(I)("NCat"))))
                DtVoucherTypeTimePlanData.Rows(I)(Col2VoucherType) = AgL.XNull(DtTemp.Rows(I)("Voucher Type"))
                DtVoucherTypeTimePlanData.Rows(I)(Col2DayLimitAdd) = AgL.VNull(DtTemp.Rows(I)("DayLimitAdd"))
                DtVoucherTypeTimePlanData.Rows(I)(Col2DayLimitEdit) = AgL.VNull(DtTemp.Rows(I)("DayLimitEdit"))
                DtVoucherTypeTimePlanData.Rows(I)(Col2DayLimitDelete) = AgL.VNull(DtTemp.Rows(I)("DayLimitDelete"))
                DtVoucherTypeTimePlanData.Rows(I)(Col2DayLimitPrint) = AgL.VNull(DtTemp.Rows(I)("DayLimitPrint"))
            Next
        End If

        Dgl2.DataSource = DtVoucherTypeTimePlanData
    End Sub

    Public Sub FGetFinancialYearLockData()
        mQry = "SELECT H.Code As [Code], C.cyear As [Company],
                S.Name AS [Site], D.Div_Name AS [Division], H.IsLocked
                FROM FinancialYearLock H  
                LEFT JOIN Company C On H.Comp_Code = C.Comp_Code
                LEFT JOIN SiteMast S ON H.Site_Code = S.Code
                LEFT JOIN Division D ON H.Div_Code = D.Div_Code "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I As Integer = 0 To DtTemp.Rows.Count - 1
                DtFinancialYearTimePlanData.Rows.Add()
                DtFinancialYearTimePlanData.Rows(I)(Col3Code) = AgL.XNull(DtTemp.Rows(I)("Code"))
                DtFinancialYearTimePlanData.Rows(I)(Col3Company) = AgL.XNull(DtTemp.Rows(I)("Company"))
                DtFinancialYearTimePlanData.Rows(I)(Col3SiteName) = AgL.XNull(DtTemp.Rows(I)("Site"))
                DtFinancialYearTimePlanData.Rows(I)(Col3DivisionName) = AgL.XNull(DtTemp.Rows(I)("Division"))
                If CType(AgL.VNull(DtTemp.Rows(I)("IsLocked")), Boolean) = True Then
                    DtFinancialYearTimePlanData.Rows(I)(Col3IsLocked) = "Yes"
                Else
                    DtFinancialYearTimePlanData.Rows(I)(Col3IsLocked) = "No"
                End If
            Next
        End If
        Dgl3.DataSource = DtFinancialYearTimePlanData
    End Sub
    'Private Function FGetNCatDesc(bNCat As String)
    '    Dim NCatDesc As String
    '    Select Case bNCat
    '        Case Ncat.CreditNoteCustomer, Ncat.CreditNoteSupplier
    '            NCatDesc = "CreditNote"
    '        Case Ncat.DebitNoteCustomer, Ncat.DebitNoteSupplier
    '            NCatDesc = "DebitNote"
    '        Case Ncat.JournalVoucher
    '            NCatDesc = "JournalVoucher"
    '        Case Ncat.LrEntry
    '            NCatDesc = "LrEntry"
    '        Case Ncat.LrTransfer
    '            NCatDesc = "LrTransfer"
    '        Case Ncat.OpeningStock
    '            NCatDesc = "OpeningStock"
    '        Case Ncat.Payment
    '            NCatDesc = "Payment"
    '        Case Ncat.PurchaseDelivery
    '            NCatDesc = "PurchaseDelivery"
    '        Case Ncat.PurchaseInvoice
    '            NCatDesc = "PurchaseInvoice"
    '        Case Ncat.PurchaseOrder
    '            NCatDesc = "PurchaseOrder"
    '        Case Ncat.PurchaseReturn
    '            NCatDesc = "PurchaseReturn"
    '        Case Ncat.Receipt
    '            NCatDesc = "Receipt"
    '        Case Ncat.SaleDelivery
    '            NCatDesc = "SaleDelivery"
    '        Case Ncat.SaleInvoice
    '            NCatDesc = "SaleInvoice"
    '        Case Ncat.SaleOrder
    '            NCatDesc = "SaleOrder"
    '        Case Ncat.SaleOrderCancel
    '            NCatDesc = "SaleOrderCancel"
    '        Case Ncat.SaleReturn
    '            NCatDesc = "SaleReturn"
    '        Case Ncat.StockTransfer
    '            NCatDesc = "StockTransfer"
    '        Case ItemTypeCode.TradingProduct
    '            NCatDesc = "TradingProduct"
    '        Case Else
    '            NCatDesc = bNCat
    '    End Select

    '    Return NCatDesc
    'End Function
    'Private Function FGetCategoryDesc(bCategory As String)
    '    Dim bCategoryDesc As String
    '    Select Case bCategory
    '        Case Ncat.CreditNoteCustomer, Ncat.CreditNoteSupplier
    '            bCategoryDesc = "CreditNote"
    '        Case Ncat.DebitNoteCustomer, Ncat.DebitNoteSupplier
    '            bCategoryDesc = "DebitNote"
    '        Case Ncat.JournalVoucher
    '            bCategoryDesc = "JournalVoucher"
    '        Case Ncat.LrEntry
    '            bCategoryDesc = "LrEntry"
    '        Case Ncat.LrTransfer
    '            bCategoryDesc = "LrTransfer"
    '        Case Ncat.OpeningStock
    '            bCategoryDesc = "OpeningStock"
    '        Case Ncat.Payment
    '            bCategoryDesc = "Payment"
    '        Case Ncat.PurchaseDelivery
    '            bCategoryDesc = "PurchaseDelivery"
    '        Case Ncat.PurchaseInvoice
    '            bCategoryDesc = "PurchaseInvoice"
    '        Case Ncat.PurchaseOrder
    '            bCategoryDesc = "PurchaseOrder"
    '        Case Ncat.PurchaseReturn
    '            bCategoryDesc = "PurchaseReturn"
    '        Case Ncat.Receipt
    '            bCategoryDesc = "Receipt"
    '        Case Ncat.SaleDelivery
    '            bCategoryDesc = "SaleDelivery"
    '        Case Ncat.SaleInvoice
    '            bCategoryDesc = "SaleInvoice"
    '        Case Ncat.SaleOrder
    '            bCategoryDesc = "SaleOrder"
    '        Case Ncat.SaleOrderCancel
    '            bCategoryDesc = "SaleOrderCancel"
    '        Case Ncat.SaleReturn
    '            bCategoryDesc = "SaleReturn"
    '        Case Ncat.StockTransfer
    '            bCategoryDesc = "StockTransfer"
    '        Case ItemTypeCode.TradingProduct
    '            bCategoryDesc = "TradingProduct"
    '        Case Else
    '            bCategoryDesc = bCategory
    '    End Select

    '    Return bCategoryDesc
    'End Function
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
    Private Sub Dgl1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dgl1.KeyPress, Dgl2.KeyPress, Dgl3.KeyPress
        Try
            If e.KeyChar = vbCr Or e.KeyChar = vbCrLf Or e.KeyChar = vbTab Or e.KeyChar = ChrW(27) Then Exit Sub
            If sender.CurrentCell Is Nothing Then Exit Sub
            fld = Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name

            Select Case sender.Name
                Case Dgl1.Name
                    If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1LockTillDate).Index Then Exit Sub
                    DtVoucherTypeDateLockData.DefaultView.RowFilter = Nothing
                    If Asc(e.KeyChar) = Keys.Back Then
                        If TxtFindDateLockSetting.Text <> "" Then TxtFindDateLockSetting.Text = Microsoft.VisualBasic.Left(TxtFindDateLockSetting.Text, Len(TxtFindDateLockSetting.Text) - 1)
                    End If
                    FManageFindTextboxVisibility(TxtFindDateLockSetting)
                    TxtFind_KeyPress(TxtFindDateLockSetting, e)

                Case Dgl2.Name
                    If Dgl2.CurrentCell.ColumnIndex = Dgl2.Columns(Col2DayLimitAdd).Index Or
                        Dgl2.CurrentCell.ColumnIndex = Dgl2.Columns(Col2DayLimitEdit).Index Or
                        Dgl2.CurrentCell.ColumnIndex = Dgl2.Columns(Col2DayLimitDelete).Index Or
                        Dgl2.CurrentCell.ColumnIndex = Dgl2.Columns(Col2DayLimitPrint).Index Then
                        Exit Sub
                    End If
                    DtVoucherTypeTimePlanData.DefaultView.RowFilter = Nothing
                    If Asc(e.KeyChar) = Keys.Back Then
                        If TxtFindTimePlanSetting.Text <> "" Then TxtFindTimePlanSetting.Text = Microsoft.VisualBasic.Left(TxtFindTimePlanSetting.Text, Len(TxtFindTimePlanSetting.Text) - 1)
                    End If
                    FManageFindTextboxVisibility(TxtFindTimePlanSetting)
                    TxtFind_KeyPress(TxtFindTimePlanSetting, e)

                Case Dgl3.Name
                    fld = Dgl3.Columns(Dgl3.CurrentCell.ColumnIndex).Name
                    If Dgl3.CurrentCell.ColumnIndex = Dgl3.Columns(Col3IsLocked).Index Then Exit Sub
                    DtFinancialYearTimePlanData.DefaultView.RowFilter = Nothing
                    If Asc(e.KeyChar) = Keys.Back Then
                        If TxtFindFinancialYearLockSetting.Text <> "" Then TxtFindFinancialYearLockSetting.Text = Microsoft.VisualBasic.Left(TxtFindFinancialYearLockSetting.Text, Len(TxtFindFinancialYearLockSetting.Text) - 1)
                    End If
                    FManageFindTextboxVisibility(TxtFindFinancialYearLockSetting)
                    TxtFind_KeyPress(TxtFindFinancialYearLockSetting, e)
            End Select
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TxtFind_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFindDateLockSetting.KeyPress, TxtFindTimePlanSetting.KeyPress
        Select Case sender.Name
            Case TxtFindDateLockSetting.Name
                RowsFilter(HlpSt, Dgl1, sender, e, fld, DtVoucherTypeDateLockData)
            Case TxtFindTimePlanSetting.Name
                RowsFilter(HlpSt, Dgl2, sender, e, fld, DtVoucherTypeTimePlanData)
            Case TxtFindFinancialYearLockSetting.Name
                RowsFilter(HlpSt, Dgl3, sender, e, fld, DtFinancialYearTimePlanData)
        End Select
    End Sub
    Private Function RowsFilter(ByVal selStr As String, ByVal CtrlObj As Object, ByVal TXT As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal FndFldName As String, ByVal DTable As DataTable) As Integer
        Try
            Dim strExpr As String, findStr As String, bSelStr As String = ""
            Dim sa As String
            Dim IntRow As Integer
            Dim i As Integer
            sa = TXT.Text
            bSelStr = selStr

            If sa.Length = 0 And Asc(e.KeyChar) = 8 Then IntRow = 0 : CtrlObj.CurrentCell = CtrlObj(FndFldName, IntRow) : DTable.DefaultView.RowFilter = Nothing : Dgl1.CurrentCell = Dgl1(FndFldName, 0) : Exit Function
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
            DTable.DefaultView.RowFilter = Nothing
            'DtSettingsData.DefaultView.RowFilter = " [" & FndFldName & "] like '%" & findStr & "%' "
            If DTable.DefaultView.RowFilter <> "" And DTable.DefaultView.RowFilter <> Nothing Then
                DTable.DefaultView.RowFilter += " And " + " [" & FndFldName & "] like '" & findStr & "%' "
            Else
                DTable.DefaultView.RowFilter += " [" & FndFldName & "] like '" & findStr & "%' "
            End If
            Try
                Dgl1.CurrentCell = Dgl1(FndFldName, 0)
            Catch ex As Exception
            End Try
            TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)

            FManageFindTextboxVisibility(TXT)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub DGL1_Click(sender As Object, e As EventArgs) Handles Dgl1.Click, Dgl2.Click
        Select Case sender.Name
            Case Dgl1.Name
                TxtFindDateLockSetting.Text = "" : FManageFindTextboxVisibility(TxtFindDateLockSetting)
            Case Dgl2.Name
                TxtFindTimePlanSetting.Text = "" : FManageFindTextboxVisibility(TxtFindTimePlanSetting)
        End Select
    End Sub
    Private Sub DGL1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles Dgl1.PreviewKeyDown, Dgl2.PreviewKeyDown
        Select Case sender.Name
            Case Dgl1.Name
                If e.KeyCode = Keys.Delete Then TxtFindDateLockSetting.Text = "" : FManageFindTextboxVisibility(TxtFindDateLockSetting) : DtVoucherTypeDateLockData.DefaultView.RowFilter = Nothing : Dgl1.CurrentCell = Dgl1(fld, 0) : DtVoucherTypeDateLockData.DefaultView.RowFilter = Nothing
            Case Dgl2.Name
                If e.KeyCode = Keys.Delete Then TxtFindTimePlanSetting.Text = "" : FManageFindTextboxVisibility(TxtFindTimePlanSetting) : DtVoucherTypeDateLockData.DefaultView.RowFilter = Nothing : Dgl1.CurrentCell = Dgl1(fld, 0) : DtVoucherTypeTimePlanData.DefaultView.RowFilter = Nothing
            Case Dgl3.Name
                If e.KeyCode = Keys.Delete Then TxtFindFinancialYearLockSetting.Text = "" : FManageFindTextboxVisibility(TxtFindFinancialYearLockSetting) : DtFinancialYearTimePlanData.DefaultView.RowFilter = Nothing : Dgl3.CurrentCell = Dgl3(fld, 0) : DtFinancialYearTimePlanData.DefaultView.RowFilter = Nothing
        End Select
    End Sub
    Private Sub FManageFindTextboxVisibility(FindTextBox As TextBox)
        If FindTextBox.Text = "" Then FindTextBox.Visible = False : FindTextBox.Visible = True
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
                    ProcSave("VoucherTypeDateLock", Dgl1.Item(Col1Code, mRowIndex).Value,
                        "LockTillDate", AgL.Chk_Date(AgL.XNull(Dgl1.Item(Col1LockTillDate, mRowIndex).Value)))
                    ClsMain.LoadVoucherTypeDateLock()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl2_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl2.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl2.CurrentCell.RowIndex
            mColumnIndex = Dgl2.CurrentCell.ColumnIndex
            If Dgl2.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl2.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                Case Col2DayLimitAdd
                    ProcSave("VoucherTypeTimePlan", Dgl2.Item(Col2Code, mRowIndex).Value, "DayLimitAdd", AgL.VNull(Dgl2.Item(mColumnIndex, mRowIndex).Value))
                    ClsMain.LoadVoucherTypeTimePlan()
                Case Col2DayLimitEdit
                    ProcSave("VoucherTypeTimePlan", Dgl2.Item(Col2Code, mRowIndex).Value, "DayLimitEdit", AgL.VNull(Dgl2.Item(mColumnIndex, mRowIndex).Value))
                    ClsMain.LoadVoucherTypeTimePlan()
                Case Col2DayLimitDelete
                    ProcSave("VoucherTypeTimePlan", Dgl2.Item(Col2Code, mRowIndex).Value, "DayLimitDelete", AgL.VNull(Dgl2.Item(mColumnIndex, mRowIndex).Value))
                    ClsMain.LoadVoucherTypeTimePlan()
                Case Col2DayLimitPrint
                    ProcSave("VoucherTypeTimePlan", Dgl2.Item(Col2Code, mRowIndex).Value, "DayLimitPrint", AgL.VNull(Dgl2.Item(mColumnIndex, mRowIndex).Value))
                    ClsMain.LoadVoucherTypeTimePlan()
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Dgl1.DataBindingComplete, Dgl2.DataBindingComplete
        sender.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        sender.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
    End Sub
    Private Sub Dgl3_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl3.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl3.CurrentCell.RowIndex
            mColumnIndex = Dgl3.CurrentCell.ColumnIndex
            If Dgl3.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl3.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case Dgl3.Columns(Dgl3.CurrentCell.ColumnIndex).Name
                Case Col3IsLocked
                    ProcSave("FinancialYearLock", Dgl3.Item(Col3Code, mRowIndex).Value,
                        "LockTillDate", AgL.Chk_Date(AgL.XNull(Dgl3.Item(Col3IsLocked, mRowIndex).Value)))
                    ClsMain.LoadVoucherTypeDateLock()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl3_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl3.KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If Dgl3.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = Dgl3.CurrentCell.RowIndex
            bColumnIndex = Dgl3.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl3.Columns(Col3IsLocked).Index Then
                Exit Sub
            End If

            Select Case Dgl3.Columns(Dgl3.CurrentCell.ColumnIndex).Name
                Case Col3IsLocked
                    FProcessYesNoColumns(e.KeyCode, Col3IsLocked, bRowIndex, "IsLocked")
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FProcessYesNoColumns(bKeyCode As Keys, bColumnName As String, bRowIndex As Integer, FieldToSave As String)
        If AgL.StrCmp(ChrW(bKeyCode), "Y") Then
            Dgl3.Item(bColumnName, bRowIndex).Tag = 1
            Dgl3.Item(bColumnName, bRowIndex).Value = "Yes"
        ElseIf AgL.StrCmp(ChrW(bKeyCode), "N") Then
            Dgl3.Item(bColumnName, bRowIndex).Tag = 0
            Dgl3.Item(bColumnName, bRowIndex).Value = "No"
        End If

        If AgL.StrCmp(ChrW(bKeyCode), "Y") Or AgL.StrCmp(ChrW(bKeyCode), "N") Then
            If Dgl3.Item(bColumnName, bRowIndex).Tag = -1 Then
                Dgl3.Item(bColumnName, bRowIndex).Tag = 1
            End If

            ProcSave("FinancialYearLock", Dgl3.Item(Col3Code, bRowIndex).Value,
                            FieldToSave, Dgl3.Item(bColumnName, bRowIndex).Tag)
        End If
    End Sub
End Class