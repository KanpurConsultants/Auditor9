Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmDisplayGSTR1
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Particulars As String = "Particulars"
    Protected Const Col1VoucherCount As String = "Voucher Count"
    Protected Const Col1TaxableValue As String = "Taxable Value"
    Protected Const Col1IntegratedTaxAmount As String = "Integrated Tax Amount"
    Protected Const Col1CentralTaxAmount As String = "Central Tax Amount"
    Protected Const Col1StateTaxAmount As String = "State Tax Amount"
    Protected Const Col1CessAmount As String = "Cess Amount"
    Protected Const Col1TaxAmount As String = "Tax Amount"
    Protected Const Col1InvoiceAmount As String = "Invoice Amount"

    Dim mQry As String = ""

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String
    Dim DtItemTypeSetting As DataTable
    Dim DtRateTypes As DataTable

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub Ini_Grid()
        Dim I As Integer = 0
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 50, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Particulars, 280, 0, Col1Particulars, True, True)
            .AddAgNumberColumn(Dgl1, Col1VoucherCount, 80, 10, 0, False, Col1VoucherCount,, True)
            .AddAgNumberColumn(Dgl1, Col1TaxableValue, 80, 10, 0, False, Col1TaxableValue,, True)
            .AddAgNumberColumn(Dgl1, Col1IntegratedTaxAmount, 80, 10, 0, False, Col1IntegratedTaxAmount,, True)
            .AddAgNumberColumn(Dgl1, Col1CentralTaxAmount, 80, 10, 0, False, Col1CentralTaxAmount,, True)
            .AddAgNumberColumn(Dgl1, Col1StateTaxAmount, 80, 10, 0, False, Col1StateTaxAmount,, True)
            .AddAgNumberColumn(Dgl1, Col1CessAmount, 80, 10, 0, False, Col1CessAmount,, True)
            .AddAgNumberColumn(Dgl1, Col1TaxAmount, 80, 10, 0, False, Col1TaxAmount,, True)
            .AddAgNumberColumn(Dgl1, Col1InvoiceAmount, 80, 10, 0, False, Col1InvoiceAmount,, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgAllowFind = True

        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)
        Dgl1.BackgroundColor = Color.White
        Dgl1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Dgl1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top

        AgL.AddAgDataGrid(Dgl2, Pnl2)
        AgL.GridDesign(Dgl2)
        Dgl2.BackgroundColor = Color.White
        Dgl2.ColumnHeadersVisible = False
        Dgl2.AllowUserToAddRows = False
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ScrollBars = ScrollBars.None
        Dgl2.RowHeadersVisible = False
        Dgl2.ReadOnly = True
        Dgl2.AllowUserToResizeColumns = False
        Dgl2.AgAllowFind = False
        Dgl2.ColumnCount = 0
        For I = 0 To Dgl1.Columns.Count - 1
            Dim mColumn As New DataGridViewColumn
            mColumn = Dgl1.Columns(I).Clone
            Dgl2.Columns.Add(mColumn)
        Next
        Dgl2.RowCount = 1
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 654, 990, 0, 0)
        DtRateTypes = AgL.FillData("Select Code, Description From RateType ", AgL.GCn).Tables(0)
        Ini_Grid()
        MovRec()
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name

        End Select
    End Sub
    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
    End Sub
    Public Sub MovRec()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim mTotalVoucherCount As Double = 0, mTotalTaxableValue As Double = 0, mTotalIntegratedTaxAmount As Double = 0, mTotalCentralTaxAmount As Double = 0
        Dim mTotalStateTaxAmount As Double = 0, mTotalCessAmount As Double = 0, mTotalTaxAmount As Double = 0, mTotalInvoiceAmount As Double = 0

        mQry = "Select H.Type As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceAmount) As InvoiceAmount 
                From (" + FGetB2BQry() + ") As H Group By Type "

        mQry = mQry + "UNION ALL "

        mQry = mQry + "Select H.Type As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceAmount) As InvoiceAmount 
                From (" + FGetB2CLargeQry() + ") As H Group By Type "

        mQry = mQry + "UNION ALL "

        mQry = mQry + "Select H.Type As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceAmount) As InvoiceAmount 
                From (" + FGetB2CSmallQry() + ") As H Group By Type "

        mQry = mQry + "UNION ALL "

        mQry = mQry + "Select H.Type As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceAmount) As InvoiceAmount 
                From (" + FGetCreditDebitNoteRegisteredQry() + ") As H Group By Type "

        mQry = mQry + "UNION ALL "

        mQry = mQry + "Select H.Type As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceAmount) As InvoiceAmount 
                From (" + FGetCreditDebitNoteUnRegisteredQry() + ") As H Group By Type "

        mQry = mQry + "UNION ALL "

        mQry = mQry + "Select H.Type As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceAmount) As InvoiceAmount 
                From (" + FGetExportInvoiceQry() + ") As H Group By Type "

        mQry = mQry + "UNION ALL "

        mQry = mQry + "Select H.Type As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceAmount) As InvoiceAmount 
                From (" + FGetTaxLiabilityAdvanceRecQry() + ") As H Group By Type "

        mQry = mQry + "UNION ALL "

        mQry = mQry + "Select H.Type As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceAmount) As InvoiceAmount 
                From (" + FGetAdjOfAdvanceQry() + ") As H Group By Type "

        mQry = mQry + "UNION ALL "

        mQry = mQry + "Select H.Type As Particulars, Count(*) As VoucherCount, Sum(H.TaxableValue) As TaxableValue, Sum(H.IntegratedTaxAmount) As IntegratedTaxAmount, 
                Sum(H.CentralTaxAmount) As CentralTaxAmount, Sum(H.StateTaxAmount) As StateTaxAmount, 
                Sum(H.CessAmount) As CessAmount, Sum(H.TaxAmount) As TaxAmount, Sum(H.InvoiceAmount) As InvoiceAmount 
                From (" + FGetNilRatedInvoiceQry() + ") As H Group By Type "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                Dgl1.Item(Col1Particulars, I).Value = AgL.XNull(DtTemp.Rows(I)("Particulars"))
                Dgl1.Item(Col1VoucherCount, I).Value = IIf(AgL.VNull(DtTemp.Rows(I)("VoucherCount")) = 0, "", AgL.XNull(DtTemp.Rows(I)("VoucherCount")))
                Dgl1.Item(Col1TaxableValue, I).Value = IIf(AgL.VNull(DtTemp.Rows(I)("TaxableValue")) = 0, "", AgL.XNull(DtTemp.Rows(I)("TaxableValue")))
                Dgl1.Item(Col1IntegratedTaxAmount, I).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IntegratedTaxAmount")) = 0, "", AgL.XNull(DtTemp.Rows(I)("IntegratedTaxAmount")))
                Dgl1.Item(Col1CentralTaxAmount, I).Value = IIf(AgL.VNull(DtTemp.Rows(I)("CentralTaxAmount")) = 0, "", AgL.XNull(DtTemp.Rows(I)("CentralTaxAmount")))
                Dgl1.Item(Col1StateTaxAmount, I).Value = IIf(AgL.VNull(DtTemp.Rows(I)("StateTaxAmount")) = 0, "", AgL.XNull(DtTemp.Rows(I)("StateTaxAmount")))
                Dgl1.Item(Col1CessAmount, I).Value = IIf(AgL.VNull(DtTemp.Rows(I)("CessAmount")) = 0, "", AgL.XNull(DtTemp.Rows(I)("CessAmount")))
                Dgl1.Item(Col1TaxAmount, I).Value = IIf(AgL.VNull(DtTemp.Rows(I)("TaxAmount")) = 0, "", AgL.XNull(DtTemp.Rows(I)("TaxAmount")))
                Dgl1.Item(Col1InvoiceAmount, I).Value = IIf(AgL.VNull(DtTemp.Rows(I)("InvoiceAmount")) = 0, "", AgL.XNull(DtTemp.Rows(I)("InvoiceAmount")))


                mTotalVoucherCount += Val(Dgl1.Item(Col1VoucherCount, I).Value)
                mTotalTaxableValue += Val(Dgl1.Item(Col1TaxableValue, I).Value)
                mTotalIntegratedTaxAmount += Val(Dgl1.Item(Col1IntegratedTaxAmount, I).Value)
                mTotalCentralTaxAmount += Val(Dgl1.Item(Col1CentralTaxAmount, I).Value)
                mTotalStateTaxAmount += Val(Dgl1.Item(Col1StateTaxAmount, I).Value)
                mTotalCessAmount += Val(Dgl1.Item(Col1CessAmount, I).Value)
                mTotalTaxAmount += Val(Dgl1.Item(Col1TaxAmount, I).Value)
                mTotalInvoiceAmount += Val(Dgl1.Item(Col1InvoiceAmount, I).Value)
            Next
            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
            Dgl1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical

            Dgl2.DefaultCellStyle.Font = New Font(New FontFamily("verdana"), 9, FontStyle.Bold)
            Dgl2.Item(Col1Particulars, 0).Value = "Total"
            Dgl2.Item(Col1VoucherCount, 0).Value = AgL.VNull(mTotalVoucherCount)
            Dgl2.Item(Col1TaxableValue, 0).Value = AgL.VNull(mTotalTaxableValue)
            Dgl2.Item(Col1IntegratedTaxAmount, 0).Value = AgL.VNull(mTotalIntegratedTaxAmount)
            Dgl2.Item(Col1CentralTaxAmount, 0).Value = AgL.VNull(mTotalCentralTaxAmount)
            Dgl2.Item(Col1StateTaxAmount, 0).Value = AgL.VNull(mTotalStateTaxAmount)
            Dgl2.Item(Col1CessAmount, 0).Value = AgL.VNull(mTotalCessAmount)
            Dgl2.Item(Col1TaxAmount, 0).Value = AgL.VNull(mTotalTaxAmount)
            Dgl2.Item(Col1InvoiceAmount, 0).Value = AgL.VNull(mTotalInvoiceAmount)
        End If
    End Sub
    Private Sub FrmReportWindow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Function FGetB2BQry() As String
        Dim mStrQry As String = " Select 'B2B Invoices - 4A, 4B, 4C, 6B, 6C' As Type, Null As V_Date, Null As InvoiceNo, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceAmount "
        Return mStrQry
    End Function
    Private Function FGetB2CLargeQry() As String
        Dim mStrQry As String = " Select 'B2C (Large) Invoices - 5A, 5B' As Type, Null As V_Date, Null As InvoiceNo, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceAmount "
        Return mStrQry
    End Function
    Private Function FGetB2CSmallQry() As String
        Dim mStrQry As String = " Select 'B2C (Small) Invoices - 7' As Type, Null As V_Date, Null As InvoiceNo, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceAmount "
        Return mStrQry
    End Function
    Private Function FGetCreditDebitNoteRegisteredQry() As String
        Dim mStrQry As String = " Select 'Credit/Debit Note (Registered) - 9B' As Type, Null As V_Date, Null As InvoiceNo, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceAmount "
        Return mStrQry
    End Function
    Private Function FGetCreditDebitNoteUnRegisteredQry() As String
        Dim mStrQry As String = " Select 'Credit/Debit Note (UnRegistered) - 9B' As Type, Null As V_Date, Null As InvoiceNo, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceAmount "
        Return mStrQry
    End Function
    Private Function FGetExportInvoiceQry() As String
        Dim mStrQry As String = " Select 'Export Invoice - 6A' As Type, Null As V_Date, Null As InvoiceNo, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceAmount "
        Return mStrQry
    End Function
    Private Function FGetTaxLiabilityAdvanceRecQry() As String
        Dim mStrQry As String = " Select 'Tax Liability (Advance Received) - 11A(1), 11A(2)' As Type, Null As V_Date, Null As InvoiceNo, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceAmount "
        Return mStrQry
    End Function
    Private Function FGetAdjOfAdvanceQry() As String
        Dim mStrQry As String = " Select 'Adjustment of Advance - 11B(1), 11B(2)' As Type, Null As V_Date, Null As InvoiceNo, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceAmount "
        Return mStrQry
    End Function
    Private Function FGetNilRatedInvoiceQry() As String
        Dim mStrQry As String = " Select 'Nil Rated Invoices - 8A, 8B, 8C, 8D' As Type, Null As V_Date, Null As InvoiceNo, 0 As TaxableValue, 0 As IntegratedTaxAmount,
                    0 As CentralTaxAmount, 0 As StateTaxAmount, 0 As CessAmount, 0 As TaxAmount, 0 As InvoiceAmount "
        Return mStrQry
    End Function
    Private Sub DGL1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles Dgl1.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            Dgl2.HorizontalScrollingOffset = e.NewValue
            Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
        End If
        If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then
            If e.Type = ScrollEventType.LargeIncrement Or e.Type = ScrollEventType.LargeDecrement Then
                Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
            End If
        End If
    End Sub
End Class