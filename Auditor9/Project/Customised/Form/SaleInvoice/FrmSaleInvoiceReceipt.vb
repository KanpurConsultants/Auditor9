Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Public Class FrmSaleInvoiceReceipt
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Public Const rowV_Type As Integer = 0
    Public Const rowSubCode As Integer = 1
    Public Const rowChqRefNo As Integer = 2
    Public Const rowChqRefDate As Integer = 3
    Public Const rowAmount As Integer = 4

    Public Const HcV_Type As String = "Receipt Type"
    Public Const HcSubCode As String = "Payment Account"
    Public Const HcChqRefNo As String = "Chq/Ref No"
    Public Const HcChqRefDate As String = "Chq/Ref Date"
    Public Const HcAmount As String = "Amount"

    Public DrSelected As DataRow()
    Dim mObjFrmSaleInvoice As Object
    Dim mSearchcode As String
    Dim mEntryMode$ = ""
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property
    Public Property objFrmSaleInvoice() As Object
        Get
            objFrmSaleInvoice = mObjFrmSaleInvoice
        End Get
        Set(ByVal value As Object)
            mObjFrmSaleInvoice = value
        End Set
    End Property
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
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
    Public Sub IniGrid(SearchCode As String)
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, False, True)
            .AddAgTextColumn(Dgl1, Col1Value, 350, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.TabIndex = Pnl1.TabIndex
        AgL.GridDesign(Dgl1)

        Dgl1.Rows.Add(5)
        Dgl1.Item(Col1Head, rowV_Type).Value = HcV_Type
        Dgl1.Item(Col1Head, rowSubCode).Value = HcSubCode
        Dgl1.Item(Col1Head, rowChqRefNo).Value = HcChqRefNo
        Dgl1.Item(Col1Head, rowChqRefDate).Value = HcChqRefDate
        Dgl1.Item(Col1Head, rowAmount).Value = HcAmount

        FMoveRec(SearchCode)
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Me.Top = 230
            Me.Left = 300
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
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Select Case Dgl1.CurrentCell.RowIndex
                Case rowV_Type
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT V_Type AS Code, Description  FROM Voucher_Type WHERE NCat = '" & Ncat.Receipt & "' "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowSubCode
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Sg.Subcode AS Code, Sg.Name 
                                FROM Subgroup Sg
                                WHERE Sg.Nature IN ('Cash','Bank') "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim dtTemp As DataTable
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.CurrentCell.RowIndex
                Case rowV_Type
            End Select
            'Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0

        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then
                    If mSearchcode <> "" Then
                        If DataValidation() = False Then Exit Sub
                        Try
                            Dim mTrans As String = ""
                            AgL.ECmd = AgL.GCn.CreateCommand
                            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                            AgL.ECmd.Transaction = AgL.ETrans
                            mTrans = "Begin"
                            FSave(mSearchcode, AgL.GCn, AgL.ECmd)
                            AgL.ETrans.Commit()
                            mTrans = "Commit"
                        Catch ex As Exception
                            AgL.ETrans.Rollback()
                            MsgBox(ex.Message)
                        End Try
                    End If
                    Me.Close()
                    Exit Sub
                Else
                    mOkButtonPressed = True
                    Me.Close()
                End If
        End Select
    End Sub
    Private Sub FrmSaleInvoiceParty_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub
    Private Sub FrmSaleInvoiceParty_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Dgl1 IsNot Nothing Then
            If Dgl1.FirstDisplayedCell IsNot Nothing Then
                Dgl1.CurrentCell = Dgl1.FirstDisplayedCell 'Dgl1(Col1Value, rowMobile)
                Dgl1.Focus()
            End If
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
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
                Case rowChqRefDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FSave(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing

        mQry = "Delete From LedgerHead Where GenDocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        Dim LedgerHeadTableList(0) As FrmVoucherEntry.StructLedgerHead
        Dim LedgerHeadTable As New FrmVoucherEntry.StructLedgerHead

        LedgerHeadTable.DocID = ""
        LedgerHeadTable.V_Type = Dgl1.Item(Col1Value, rowV_Type).Tag
        LedgerHeadTable.V_Prefix = mObjFrmSaleInvoice.LblPrefix.Text
        LedgerHeadTable.Site_Code = mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowSite_Code).Tag
        LedgerHeadTable.Div_Code = mObjFrmSaleInvoice.TxtDivision.Tag
        LedgerHeadTable.V_No = 0
        LedgerHeadTable.V_Date = ClsMain.FormatDate(mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowV_Date).value)
        LedgerHeadTable.ManualRefNo = ""
        LedgerHeadTable.Subcode = Dgl1.Item(Col1Value, rowSubCode).Tag
        LedgerHeadTable.LinkedSubcode = ""
        LedgerHeadTable.SubcodeName = ""
        LedgerHeadTable.SalesTaxGroupParty = ""
        LedgerHeadTable.PlaceOfSupply = ""
        LedgerHeadTable.StructureCode = ""
        LedgerHeadTable.CustomFields = ""
        LedgerHeadTable.PartyDocNo = ""
        LedgerHeadTable.PartyDocDate = ""
        LedgerHeadTable.Remarks = ""
        LedgerHeadTable.Status = "Active"
        LedgerHeadTable.EntryBy = AgL.PubUserName
        LedgerHeadTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
        LedgerHeadTable.ApproveBy = ""
        LedgerHeadTable.ApproveDate = ""
        LedgerHeadTable.MoveToLog = ""
        LedgerHeadTable.MoveToLogDate = ""
        LedgerHeadTable.UploadDate = ""
        LedgerHeadTable.OMSId = ""
        LedgerHeadTable.LockText = ""
        LedgerHeadTable.GenDocId = SearchCode

        LedgerHeadTable.Gross_Amount = 0
        LedgerHeadTable.Taxable_Amount = 0
        LedgerHeadTable.Tax1 = 0
        LedgerHeadTable.Tax2 = 0
        LedgerHeadTable.Tax3 = 0
        LedgerHeadTable.Tax4 = 0
        LedgerHeadTable.Tax5 = 0
        LedgerHeadTable.SubTotal1 = 0
        LedgerHeadTable.Other_Charge = 0
        LedgerHeadTable.Deduction = 0
        LedgerHeadTable.Round_Off = 0
        LedgerHeadTable.Net_Amount = 0


        LedgerHeadTable.Line_Sr = 1
        LedgerHeadTable.Line_SubCode = mObjFrmSaleInvoice.DglMain.Item(FrmSaleInvoiceDirect_WithDimension.Col1Value, mObjFrmSaleInvoice.rowBillToParty).Tag
        LedgerHeadTable.Line_SubCodeName = ""
        LedgerHeadTable.Line_LinkedSubCode = ""
        LedgerHeadTable.Line_LinkedSubCodeName = ""
        LedgerHeadTable.Line_Specification = ""
        LedgerHeadTable.Line_SalesTaxGroupItem = ""
        LedgerHeadTable.Line_Qty = 0
        LedgerHeadTable.Line_Unit = ""
        LedgerHeadTable.Line_Rate = 0
        LedgerHeadTable.Line_Amount = AgL.VNull(Dgl1.Item(Col1Value, rowAmount).Value)
        LedgerHeadTable.Line_Amount_Cr = 0
        LedgerHeadTable.Line_ChqRefNo = AgL.XNull(Dgl1.Item(Col1Value, rowChqRefNo).Value)
        LedgerHeadTable.Line_ChqRefDate = AgL.XNull(Dgl1.Item(Col1Value, rowChqRefDate).Value)
        LedgerHeadTable.Line_Remarks = ""

        LedgerHeadTable.Line_Gross_Amount = 0
        LedgerHeadTable.Line_Taxable_Amount = 0
        LedgerHeadTable.Line_Tax1_Per = 0
        LedgerHeadTable.Line_Tax1 = 0
        LedgerHeadTable.Line_Tax2_Per = 0
        LedgerHeadTable.Line_Tax2 = 0
        LedgerHeadTable.Line_Tax3_Per = 0
        LedgerHeadTable.Line_Tax3 = 0
        LedgerHeadTable.Line_Tax4_Per = 0
        LedgerHeadTable.Line_Tax4 = 0
        LedgerHeadTable.Line_Tax5_Per = 0
        LedgerHeadTable.Line_Tax5 = 0
        LedgerHeadTable.Line_SubTotal1 = 0
        LedgerHeadTable.Line_Other_Charge = 0
        LedgerHeadTable.Line_Deduction = 0
        LedgerHeadTable.Line_Round_Off = 0
        LedgerHeadTable.Line_Net_Amount = 0

        LedgerHeadTableList(UBound(LedgerHeadTableList)) = LedgerHeadTable
        ReDim Preserve LedgerHeadTableList(UBound(LedgerHeadTableList) + 1)

        FrmVoucherEntry.InsertLedgerHead(LedgerHeadTableList)
    End Sub
    Public Sub FMoveRec(ByVal SearchCode As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        If SearchCode = "" Then Exit Sub
        mSearchcode = SearchCode

        Try
            mQry = "SELECT H.V_Type, Vt.Description As V_TypeDesc, 
                    H.SubCode, Sg.Name As CashBankName, 
                    L.ChqRefNo, L.ChqRefDate, L.Amount
                    FROM LedgerHead H       
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type               
                    LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                    LEFT JOIN LedgerHeadDetail L On H.DocId = L.DocId
                    WHERE H.GenDocId = '" & SearchCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1Value, rowV_Type).Tag = AgL.XNull(DtTemp.Rows(0)("V_Type"))
                    Dgl1.Item(Col1Value, rowV_Type).Value = AgL.XNull(.Rows(0)("V_TypeDesc"))
                    Dgl1.Item(Col1Value, rowSubCode).Tag = AgL.XNull(.Rows(0)("SubCode"))
                    Dgl1.Item(Col1Value, rowSubCode).Value = AgL.XNull(.Rows(0)("CashBankName"))
                    Dgl1.Item(Col1Value, rowChqRefNo).Value = AgL.XNull(.Rows(0)("ChqRefNo"))
                    Dgl1.Item(Col1Value, rowChqRefDate).Value = AgL.RetDate(AgL.XNull(.Rows(0)("ChqRefDate")))
                    Dgl1.Item(Col1Value, rowAmount).Value = AgL.XNull(.Rows(0)("Amount"))
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class