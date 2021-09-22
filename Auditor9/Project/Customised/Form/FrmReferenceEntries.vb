Imports Customised.ClsMain
Public Class FrmReferenceEntries
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""

    Public Const ColSNo As String = "S.No."
    Public Const Col1DocId As String = "DocId"
    Public Const Col1EntryType As String = "Entry Type"
    Public Const Col1EntryNo As String = "Entry No"
    Public Const Col1EntryDate As String = "Entry Date"
    Public Const Col1Remark As String = "Remark"

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
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1DocId, 130, 0, Col1DocId, False, True)
            .AddAgTextColumn(Dgl1, Col1EntryType, 130, 0, Col1EntryType, True, True)
            .AddAgTextColumn(Dgl1, Col1EntryNo, 130, 0, Col1EntryNo, True, True)
            .AddAgDateColumn(Dgl1, Col1EntryDate, 130, Col1EntryDate, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 220, 255, Col1Remark, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)

        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        MovRec()
    End Sub
    Private Sub MovRec()
        Dim mQry As String = ""

        mQry = " SELECT H.DocId, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Tr.Remark
                FROM TransactionReferences Tr
                LEFT JOIN PurchInvoice H ON Tr.ReferenceDocId = H.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                WHERE Tr.DocID = '" & mSearchCode & "' AND H.DocID IS NOT NULL
                UNION ALL 
                SELECT H.DocId, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Tr.Remark
                FROM TransactionReferences Tr
                LEFT JOIN SaleInvoice H ON Tr.ReferenceDocId = H.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                WHERE Tr.DocID = '" & mSearchCode & "' AND H.DocID IS NOT NULL
                UNION ALL 
                SELECT H.DocId, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Tr.Remark
                FROM TransactionReferences Tr
                LEFT JOIN LedgerHead H ON Tr.ReferenceDocId = H.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                WHERE Tr.DocID = '" & mSearchCode & "' AND H.DocID IS NOT NULL
                UNION ALL 
                SELECT H.DocId, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Tr.Remark
                FROM TransactionReferences Tr
                LEFT JOIN StockHead H ON Tr.ReferenceDocId = H.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                WHERE Tr.DocID = '" & mSearchCode & "' AND H.DocID IS NOT NULL "

        mQry += " UNION ALL "

        mQry += " SELECT H.DocId, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Tr.Remark
                FROM TransactionReferences Tr
                LEFT JOIN PurchInvoice H ON Tr.DocId = H.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                WHERE Tr.ReferenceDocId = '" & mSearchCode & "' AND H.DocID IS NOT NULL
                UNION ALL 
                SELECT H.DocId, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Tr.Remark
                FROM TransactionReferences Tr
                LEFT JOIN SaleInvoice H ON Tr.DocId = H.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                WHERE Tr.ReferenceDocId = '" & mSearchCode & "' AND H.DocID IS NOT NULL
                UNION ALL 
                SELECT H.DocId, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Tr.Remark
                FROM TransactionReferences Tr
                LEFT JOIN LedgerHead H ON Tr.DocId = H.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                WHERE Tr.ReferenceDocId = '" & mSearchCode & "' AND H.DocID IS NOT NULL
                UNION ALL 
                SELECT H.DocId, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Tr.Remark
                FROM TransactionReferences Tr
                LEFT JOIN StockHead H ON Tr.DocId = H.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                WHERE Tr.ReferenceDocId = '" & mSearchCode & "' AND H.DocID IS NOT NULL "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I As Integer = 0 To .Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1DocId, I).Value = AgL.XNull(.Rows(I)("DocId"))
                    Dgl1.Item(Col1EntryType, I).Value = AgL.XNull(.Rows(I)("EntryType"))
                    Dgl1.Item(Col1EntryNo, I).Value = AgL.XNull(.Rows(I)("EntryNo"))
                    Dgl1.Item(Col1EntryDate, I).Value = AgL.XNull(.Rows(I)("EntryDate"))
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                Next I
            End If
        End With
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
    Private Sub Dgl1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellDoubleClick
        ClsMain.FOpenForm(Dgl1.Item(Col1DocId, e.RowIndex).Value, Me)
    End Sub
End Class