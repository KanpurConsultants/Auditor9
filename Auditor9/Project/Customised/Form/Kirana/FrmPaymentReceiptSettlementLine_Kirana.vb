Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class FrmPaymentReceiptSettlementLine_Kirana
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1BtnDetail As String = "Detail"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1LastValue As String = "Last Value"


    Public Const ColSNo As String = "S.No."
    Public Const ColItem As String = "Item"
    Public Const ColPcs As String = "Pcs"
    Public Const ColQty As String = "Qty"
    Public Const ColRate As String = "Rate"


    Public rowBillType As Integer = 0
    Public rowBillNo As Integer = 1
    Public rowPartyName As Integer = 2
    Public rowAmount As Integer = 3
    Public rowInterestPer As Integer = 4
    Public rowInterestAmount As Integer = 5
    Public rowDiscountPer As Integer = 6
    Public rowDiscountAmount As Integer = 7
    Public rowSubTotal As Integer = 8
    Public rowBrokeragePer As Integer = 9
    Public rowBrokerageAmount As Integer = 10



    Public hcBillType As String = "Bill Type"
    Public hcBillNo As String = "Bill No"
    Public hcPartyName As String = "Party Name"
    Public hcAmount As String = "Amount"
    Public hcInterestPer As String = "Interest Per"
    Public hcInterestAmount As String = "Interest Amount"
    Public hcDiscountPer As String = "Discount Per"
    Public hcDiscountAmount As String = "Discount Amount"
    Public hcSubTotal As String = "Sub Total"
    Public hcBrokeragePer As String = "Brokerage Per"
    Public hcBrokerageAmount As String = "Brokerage Amount"



    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mSearchCode As String = ""
    Dim mToQtyDecimalPlace As Integer
    Dim mPartyCode As String
    Dim mProcessCode As String = ""
    Dim mDglMainLastRowIndex As Integer
    Dim mCopyToSearchCodesArr As String()
    Dim DtItemRelation As DataTable
    Dim mTransNature As String = NCatNature.Issue
    Dim mObjFrmPurchInvoice As FrmPurchInvoiceDirect_WithDimension

    Public mDimensionSrl As Integer
    Public Property objFrmPurchInvoice() As FrmPurchInvoiceDirect_WithDimension
        Get
            objFrmPurchInvoice = mObjFrmPurchInvoice
        End Get
        Set(ByVal value As FrmPurchInvoiceDirect_WithDimension)
            mObjFrmPurchInvoice = value
        End Set
    End Property
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub IniGrid(SearchCode As String)
        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 250, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 500, 255, Col1Value, True, False)
            .AddAgTextColumn(DglMain, Col1LastValue, 170, 255, Col1LastValue, False, False)
        End With
        AgL.AddAgDataGrid(DglMain, PnlMain)
        AgL.GridDesign(DglMain)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.RowHeadersVisible = False
        DglMain.ColumnHeadersVisible = False
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None

        DglMain.Rows.Add(11)
        'For I As Integer = 0 To DglMain.Rows.Count - 1
        '    DglMain.Rows(I).Visible = False
        'Next
        DglMain.Item(Col1Head, rowBillType).Value = hcBillType
        DglMain.Item(Col1Head, rowBillNo).Value = hcBillNo
        DglMain.Item(Col1Head, rowPartyName).Value = hcPartyName
        DglMain.Item(Col1Head, rowAmount).Value = hcAmount
        DglMain.Item(Col1Head, rowInterestPer).Value = hcInterestPer
        DglMain.Item(Col1Head, rowInterestAmount).Value = hcInterestAmount
        DglMain.Item(Col1Head, rowDiscountPer).Value = hcDiscountPer
        DglMain.Item(Col1Head, rowDiscountAmount).Value = hcDiscountAmount
        DglMain.Item(Col1Head, rowSubTotal).Value = hcSubTotal
        DglMain.Item(Col1Head, rowBrokeragePer).Value = hcBrokeragePer
        DglMain.Item(Col1Head, rowBrokerageAmount).Value = hcBrokerageAmount




        DglMain.Name = "DglMain"
        DglMain.Tag = "VerticalGrid"

        For I As Integer = 0 To DglMain.Rows.Count - 1
            If AgL.XNull(DglMain(Col1HeadOriginal, I).Value) = "" Then
                DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
            End If
        Next


        Dgl1.Name = "Dgl1"
        FDesignColumns(Dgl1)
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)


        ApplyUISetting()

        If AgL.StrCmp(EntryMode, "Browse") Then
            Dgl1.ReadOnly = True
        Else
            Dgl1.ReadOnly = False
        End If



        mSearchCode = SearchCode

        'AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Private Sub FDesignColumns(DglControl As AgControls.AgDataGrid)
        With AgCL
            .AddAgTextColumn(DglControl, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglControl, ColItem, 100, 0, ColItem, True, True)
            .AddAgNumberColumn(DglControl, ColPcs, 80, 8, 4, False, ColPcs, True, True, True)
            .AddAgNumberColumn(DglControl, ColQty, 70, 8, 4, False, ColQty, True, True, True)
            .AddAgNumberColumn(DglControl, ColRate, 80, 8, 2, False, ColRate, True, True, True)
        End With
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            mOkButtonPressed = False
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            'Me.Top = 300
            'Me.Left = 300
            'FIniList()

            If AgL.StrCmp(EntryMode, "Browse") Then
                DglMain.ReadOnly = True
                Dgl1.ReadOnly = True
            Else
                DglMain.ReadOnly = False
                Dgl1.ReadOnly = False
            End If

            If DglMain.Rows(rowInterestPer).Visible = True Then
                DglMain.CurrentCell = DglMain.Item(Col1Value, rowInterestPer)
                DglMain.Focus()
            End If

            If mEntryMode = "Browse" Then
                DglMain.ReadOnly = True
                Dgl1.ReadOnly = True
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        FOkButtonClick()
    End Sub
    Public Sub FMoveRecForGrid()
        Dim DtTemp As DataTable = Nothing
        Dim DsMain As DataSet
        Dim I As Integer = 0
        Dim mQryStockSr As String = ""

        Try
            mQry = " Select L.Item, I.Description As ItemDesc, L.Qty, L.Pcs, L.Rate,
                    U.DecimalPlaces As QtyDecimalPlaces 
                    From SaleInvoiceDetail L
                    LEFT JOIN Item I On L.Item = I.Code
                    LEFT JOIN Unit U  With (NoLock) On L.Unit = U.Code 
                    Where DocId = '" & DglMain.Item(Col1Value, rowBillNo).Tag & "'"
            DsMain = AgL.FillData(mQry, AgL.GCn)
            With DsMain.Tables(0)
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To DsMain.Tables(0).Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(ColItem, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(ColItem, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(ColQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(ColRate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                    Next I
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FOkButtonClick()
        Dim I As Integer = 0
        If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
        mOkButtonPressed = True
        Me.Close()
    End Sub
    Private Sub ApplyUISetting()
        'Dim bEntryNCat As String = AgL.Dman_Execute("Select NCat From Voucher_Type Where V_Type = '" & mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag & "'", AgL.GCn).ExecuteScalar()

        'GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bEntryNCat, mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag, "", "", ClsMain.GridTypeConstants.VerticalGrid)
        'GetUISetting_WithDataTables(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bEntryNCat, mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag, "", "", ClsMain.GridTypeConstants.HorizontalGrid)
        'GetUISetting_WithDataTables(Dgl2, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, bEntryNCat, mObjFrmPurchInvoice.DglMain.Item(FrmPurchInvoiceDirect_WithDimension.Col1Value, mObjFrmPurchInvoice.rowV_Type).Tag, "", "", ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Public Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim bEntryNCat As String = Ncat.StockIssue
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode,
                AgL.PubSiteCode, "", bEntryNCat, "", mProcessCode, "")
        FGetSettings = mValue
    End Function
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub Calculation()
        Dim I As Integer

        If Val(DglMain.Item(Col1Value, rowInterestPer).Value) <> 0 Then
            DglMain.Item(Col1Value, rowInterestAmount).Value = Val(DglMain.Item(Col1Value, rowAmount).Value) * Val(DglMain.Item(Col1Value, rowInterestPer).Value) / 100
        End If

        If Val(DglMain.Item(Col1Value, rowDiscountPer).Value) <> 0 Then
            DglMain.Item(Col1Value, rowDiscountAmount).Value = Val(DglMain.Item(Col1Value, rowAmount).Value) * Val(DglMain.Item(Col1Value, rowDiscountPer).Value) / 100
        End If

        DglMain.Item(Col1Value, rowSubTotal).Value = Val(DglMain.Item(Col1Value, rowAmount).Value) + DglMain.Item(Col1Value, rowInterestAmount).Value - DglMain.Item(Col1Value, rowDiscountAmount).Value

        If Val(DglMain.Item(Col1Value, rowBrokeragePer).Value) <> 0 Then
            DglMain.Item(Col1Value, rowBrokerageAmount).Value = Val(DglMain.Item(Col1Value, rowSubTotal).Value) * Val(DglMain.Item(Col1Value, rowBrokeragePer).Value) / 100
        End If
    End Sub
    Private Function FDataValidation(DglControl As AgControls.AgDataGrid) As Boolean
        FDataValidation = False

        FDataValidation = True
    End Function
    Private Sub DglMain_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.KeyDown
        Dim mRow As Integer
        Dim mColumn As Integer

        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub

            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex

            Select Case mRow

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        Calculation()
    End Sub
    Private Sub DglMain_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellEnter

        Select Case DglMain.CurrentCell.RowIndex
            Case rowBillType, rowBillNo, rowPartyName, rowAmount
                DglMain.Item(Col1Value, DglMain.CurrentCell.RowIndex).ReadOnly = True
        End Select
    End Sub
End Class