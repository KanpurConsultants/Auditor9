Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmQuickView
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = "D100001658"
    Public Const Col1Head As String = "Head"
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Dim rowPartyName As Integer = 0
    Dim rowAddress As Integer = 1
    Dim rowCity As Integer = 2
    Dim rowMobile As Integer = 3
    Dim rowEMail As Integer = 4
    Dim rowContactPerson As Integer = 5
    Dim rowCreditLimit As Integer = 6

    Public Const PartyName As String = "Party Name"
    Public Const Address As String = "Address"
    Public Const City As String = "City"
    Public Const Mobile As String = "Mobile"
    Public Const EMail As String = "EMail"
    Public Const ContactPerson As String = "Contact Person"
    Public Const CreditLimit As String = "Credit Limit"
    Public Const CurrentBalance As String = "Current Balance"
    Public Const LastSaleInvoice As String = "Last Sale Invoice"
    Public Const LastPurchaseInvoice As String = "Last Purchase Invoice"
    Public Const LastPayment As String = "Last Payment"
    Public Const LastReceipt As String = "Last Receipt"

    Dim mQry As String = ""
    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, Col1Head, 250, 0, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1Value, 400, 0, Col1Value, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.ColumnHeadersVisible = False
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)


        Dgl1.Rows.Add(7)

        Dgl1.Item(Col1Head, rowPartyName).Value = PartyName
        Dgl1.Item(Col1Head, rowAddress).Value = Address
        Dgl1.Item(Col1Head, rowCity).Value = City
        Dgl1.Item(Col1Head, rowMobile).Value = Mobile
        Dgl1.Item(Col1Head, rowEMail).Value = EMail
        Dgl1.Item(Col1Head, rowContactPerson).Value = ContactPerson
        Dgl1.Item(Col1Head, rowCreditLimit).Value = CreditLimit

        Dgl1.Item(Col1Value, rowPartyName).Style.BackColor = Color.White
    End Sub
    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        MovRec()
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub
    Private Sub MovRec()
        Dim mQry As String = ""

        mQry = " SELECT Sg.Name, Sg.Address, C.CityName, Sg.Mobile, Sg.Email, Sg.ContactPerson, Sg.CreditLimit
                FROM Subgroup Sg 
                LEFT JOIN City C ON Sg.CityCode = C.CityCode
                WHERE Sg.Subcode =  '" & mSearchCode & "' "
        Dim DtPartyPrimaryDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtPartyPrimaryDetail.Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowPartyName).Value = AgL.XNull(DtPartyPrimaryDetail.Rows(0)("Name"))
            Dgl1.Item(Col1Value, rowAddress).Value = AgL.XNull(DtPartyPrimaryDetail.Rows(0)("Address"))
            Dgl1.Item(Col1Value, rowCity).Value = AgL.XNull(DtPartyPrimaryDetail.Rows(0)("CityName"))
            Dgl1.Item(Col1Value, rowMobile).Value = AgL.XNull(DtPartyPrimaryDetail.Rows(0)("Mobile"))
            Dgl1.Item(Col1Value, rowEMail).Value = AgL.XNull(DtPartyPrimaryDetail.Rows(0)("Email"))
            Dgl1.Item(Col1Value, rowContactPerson).Value = AgL.XNull(DtPartyPrimaryDetail.Rows(0)("ContactPerson"))
            Dgl1.Item(Col1Value, rowCreditLimit).Value = AgL.XNull(DtPartyPrimaryDetail.Rows(0)("CreditLimit"))
        End If

        mQry = " SELECT L.DivCode, Max(D.Div_Name) As DivisionName, Abs(IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0)) As Balance, 
                CASE WHEN IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) > 0 THEN ' Dr' 
	                 WHEN IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) < 0 THEN ' Cr' 
	                 ELSE '' END AS BalanceDrCr
                FROM Ledger L 
                LEFT JOIN Division D On L.DivCode = D.Div_Code
                Where L.SubCode = '" & mSearchCode & "' " &
                IIf(IsFeatureApplicable_Overlay = False, " And L.DivCode = '" & AgL.PubDivCode & "'", "") &
                " GROUP BY L.DivCode, L.SubCode "
        Dim DtPartyLedgerDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I As Integer = 0 To DtPartyLedgerDetail.Rows.Count - 1
            Dgl1.Rows.Add()
            Dgl1.Item(Col1Head, Dgl1.Rows.Count - 1).Value = CurrentBalance
            Dgl1.Item(Col1Value, Dgl1.Rows.Count - 1).Value = AgL.XNull(DtPartyLedgerDetail.Rows(I)("DivisionName")) + " : "
            Dgl1.Item(Col1Value, Dgl1.Rows.Count - 1).Value += AgL.XNull(DtPartyLedgerDetail.Rows(I)("Balance")) + AgL.XNull(DtPartyLedgerDetail.Rows(I)("BalanceDrCr"))
            If AgL.VNull(DtPartyLedgerDetail.Rows(I)("Balance")) > 0 Then
                'Dgl1.Item(Col1Value, Dgl1.Rows.Count - 1).Value += vbCrLf + "[" + AmountInWords(AgL.XNull(DtPartyLedgerDetail.Rows(I)("Balance"))) + "]"
                Dgl1.Item(Col1Value, Dgl1.Rows.Count - 1).Value += vbCrLf + "[" + AmountInWordsInIndianFormat(AgL.XNull(DtPartyLedgerDetail.Rows(I)("Balance"))) + "]"
            End If
        Next

        FSetSaleAndPurchaseDetail(Ncat.SaleInvoice)
        FSetSaleAndPurchaseDetail(Ncat.PurchaseInvoice)

        FSetLedgerDetail(Ncat.Payment)
        FSetLedgerDetail(Ncat.Receipt)



        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
    End Sub
    Private Sub FSetSaleAndPurchaseDetail(bNCat As String)
        Dim bHeaderTableName As String = ""
        Dim bLineTableName As String = ""
        Dim bPartyField As String = ""

        If bNCat = Ncat.SaleInvoice Then
            bHeaderTableName = "SaleInvoice"
            bLineTableName = "SaleInvoiceDetail"
            bPartyField = "SaleToParty"
        ElseIf bNCat = Ncat.PurchaseInvoice Then
            bHeaderTableName = "PurchInvoice"
            bLineTableName = "PurchInvoiceDetail"
            bPartyField = "Vendor"
        End If

        mQry = " SELECT Max(V1.LastInvoiceDate) AS InvoiceDate, Sum(V2.Net_Amount) AS InvoiceAmount, 
                Max(Ic.Description) AS ItemCategoryDesc
                FROM (
                    SELECT H." & bPartyField & " As Party, Max(H.V_Date) AS LastInvoiceDate
                    FROM " & bHeaderTableName & " H 
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE H." & bPartyField & " = '" & mSearchCode & "'
                    AND Vt.NCat = '" & bNCat & "'
                    GROUP BY H." & bPartyField & "
                ) AS V1
                LEFT JOIN (
                    SELECT H.DocID, H." & bPartyField & " As Party, H.V_Date, H.Net_Amount 
                    FROM " & bHeaderTableName & " H 
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE H." & bPartyField & " = '" & mSearchCode & "'
                    AND Vt.NCat = '" & bNCat & "'
                ) AS V2 ON V1.Party = V2.Party AND V1.LastInvoiceDate = V2.V_Date
                LEFT JOIN (
                    SELECT L.DocID, Max(I.ItemCategory) AS ItemCategory
                    FROM " & bLineTableName & " L 
                    LEFT JOIN Item I ON L.Item = I.Code
                    GROUP BY L.DocID
                ) AS VItemCat ON V2.DocId = VItemCat.DocId
                LEFT JOIN ItemCategory Ic ON VItemCat.ItemCategory = Ic.Code "
        Dim DtPartyInvoices As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I As Integer = 0 To DtPartyInvoices.Rows.Count - 1
            If AgL.XNull(DtPartyInvoices.Rows(0)("InvoiceDate")) <> "" Then
                Dgl1.Rows.Add()
                Dgl1.Item(Col1Head, Dgl1.Rows.Count - 1).Value = IIf(bNCat = Ncat.SaleInvoice, LastSaleInvoice, LastPurchaseInvoice)
                Dgl1.Item(Col1Value, Dgl1.Rows.Count - 1).Value = "Date : " + AgL.XNull(DtPartyInvoices.Rows(0)("InvoiceDate")) + vbCrLf +
                        "Amount : " + AgL.XNull(DtPartyInvoices.Rows(0)("InvoiceAmount")) + vbCrLf +
                        "Item : " + AgL.XNull(DtPartyInvoices.Rows(0)("ItemCategoryDesc"))
            End If
        Next
    End Sub
    Private Sub FSetLedgerDetail(bNCat As String)
        Dim bRowNumber As Integer = 0

        mQry = " SELECT Max(V1.LastVDate) AS VDate, Sum(V2.Amount) AS Amount
                FROM (
	                SELECT H.SubCode, Max(H.V_Date) AS LastVDate
	                FROM Ledger H 
	                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                WHERE H.SubCode = '" & mSearchCode & "'
	                AND Vt.NCat = '" & bNCat & "'
	                GROUP BY H.SubCode
                ) AS V1
                LEFT JOIN (
	                SELECT H.SubCode, H.V_Date, H.AmtCr AS Amount
	                FROM Ledger H 
	                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                WHERE H.SubCode = '" & mSearchCode & "'
	                AND Vt.NCat = '" & bNCat & "'
                ) AS V2 ON V1.SubCode = V2.SubCode AND V1.LastVDate = V2.V_Date
                "
        Dim DtLedgerDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtLedgerDetail.Rows.Count - 1
            If AgL.XNull(DtLedgerDetail.Rows(0)("VDate")) <> "" Then
                Dgl1.Rows.Add()
                Dgl1.Item(Col1Head, Dgl1.Rows.Count - 1).Value = IIf(bNCat = Ncat.Payment, LastPayment, LastReceipt)
                Dgl1.Item(Col1Value, Dgl1.Rows.Count - 1).Value += "Date : " + AgL.XNull(DtLedgerDetail.Rows(0)("VDate")) + vbCrLf +
                            "Amount : " + AgL.XNull(DtLedgerDetail.Rows(0)("Amount"))
            End If
        Next
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
    Private Sub Dgl1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Dgl1.CellPainting
        e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
        If e.RowIndex < 1 Or e.ColumnIndex < 0 Then Return
        If (IsTheSameCellValue(e.ColumnIndex, e.RowIndex)) Then
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        Else
            e.AdvancedBorderStyle.Top = Dgl1.AdvancedCellBorderStyle.Top
        End If
    End Sub
    Private Function IsTheSameCellValue(column As Integer, row As Integer) As Boolean
        If column = Dgl1.Columns(Col1Head).Index Then
            Dim cell1 As DataGridViewCell = Dgl1(column, row)
            Dim cell2 As DataGridViewCell = Dgl1(column, row - 1)
            If cell1.Value Is Nothing Or cell2.Value Is Nothing Then Return False
            Return cell1.Value.ToString() = cell2.Value.ToString()
        End If
    End Function
    Private Sub Dgl1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgl1.CellFormatting
        If (e.RowIndex = 0) Then Return
        If (IsTheSameCellValue(e.ColumnIndex, e.RowIndex)) Then e.Value = ""
        e.FormattingApplied = True
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowPartyName
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg ORDER BY Sg.Name "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowPartyName
                    Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).ReadOnly = False
                Case Else
                    Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        mSearchCode = Dgl1.Item(Col1Value, rowPartyName).Tag
        Dim RowIndexList As New List(Of DataGridViewRow)()

        If rowCreditLimit + 1 < Dgl1.Rows.Count - 1 Then
            For I As Integer = rowCreditLimit + 1 To Dgl1.Rows.Count - 1
                RowIndexList.Add(Dgl1.Rows(I))
            Next

            If RowIndexList IsNot Nothing Then
                For I As Integer = 0 To RowIndexList.Count - 1
                    Dgl1.Rows.Remove(RowIndexList(I))
                Next
            End If
        End If
        MovRec()
    End Sub

End Class