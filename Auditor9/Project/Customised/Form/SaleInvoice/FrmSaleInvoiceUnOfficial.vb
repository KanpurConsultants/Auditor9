Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmSaleInvoiceUnOfficial
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public Const ColSNo As String = "S.No."
    Public Const Col1SaleOrderNo As String = "Order No"
    Public Const Col1SaleOrderDate As String = "Order Date"
    Public Const Col1Supplier As String = "Supplier"
    Public Const Col1PurchInvoiceNo As String = "Purch Invoice No"
    Public Const Col1PurchInvoiceDate As String = "Purch Invoice Date"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Freight As String = "Freight"
    Public Const Col1Packing As String = "Packing"
    Public Const Col1PurchAmount As String = "Purch Amount"

    Dim mQry As String = ""
    Dim mOrderNCat As String = "SO"

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
            .AddAgTextColumn(Dgl1, Col1SaleOrderNo, 100, 0, Col1SaleOrderNo, True, True)
            .AddAgTextColumn(Dgl1, Col1SaleOrderDate, 100, 0, Col1SaleOrderDate, True, True)
            .AddAgTextColumn(Dgl1, Col1Supplier, 250, 0, Col1Supplier, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchInvoiceNo, 130, 0, Col1PurchInvoiceNo, True, False)
            .AddAgDateColumn(Dgl1, Col1PurchInvoiceDate, 110, Col1PurchInvoiceDate, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 130, 0, 0, False, Col1Qty)
            .AddAgNumberColumn(Dgl1, Col1Freight, 130, 0, 0, False, Col1Freight)
            .AddAgNumberColumn(Dgl1, Col1Packing, 130, 0, 0, False, Col1Packing)
            .AddAgNumberColumn(Dgl1, Col1PurchAmount, 130, 0, 0, False, Col1PurchAmount)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
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
        TxtOrderNo.Focus()
        TxtPartyName.Enabled = False

        FSyncSaleOrders()
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
    Private Sub FSave(DocId As String, Sr As Integer, SelectValue As String, mRowIndex As Integer)

    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtOrderNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        Try
            Select Case sender.NAME
                Case TxtOrderNo.Name
                    mQry = "Select H.SaleToParty, Sg.Name As SaleToPartyName
                            From SaleInvoice H  With (NoLock)
                            LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                            Where H.DocId = '" & TxtOrderNo.Tag & "'"
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        TxtPartyName.Tag = AgL.XNull(DtTemp.Rows(0)("SaleToParty"))
                        TxtPartyName.Text = AgL.XNull(DtTemp.Rows(0)("SaleToPartyName"))
                    End If


                    mQry = "Select H.DocId As SaleOrderDocId, Max(H.ManualRefNo) As SaleOrderNo, 
                            Max(H.V_Date) As SaleOrderDate, 
                            Max(Supp.SubCode) As SupplierCode, Max(Supp.Name) As SupplierName
                            From SaleInvoice H  With (NoLock)
                            LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                            LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocId
                            LEFT JOIN Item I On L.Item = I.Code
                            LEFT JOIN ItemGroupPerson Igp ON I.Code = Igp.ItemGroup 
                            LEFT JOIN SubGroup Supp On Igp.Person = Supp.SubCode
                            Where H.DocId = '" & TxtOrderNo.Tag & "'
                            Group By H.DocId, Sg.SubCode "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                        For I = 0 To DtTemp.Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(Col1SaleOrderNo, I).Tag = AgL.XNull(DtTemp.Rows(I)("SaleOrderDocId"))
                            Dgl1.Item(Col1SaleOrderNo, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleOrderNo"))
                            Dgl1.Item(Col1SaleOrderDate, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleOrderDate"))
                            Dgl1.Item(Col1Supplier, I).Tag = AgL.XNull(DtTemp.Rows(I)("SupplierCode"))
                            Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtTemp.Rows(I)("SupplierName"))

                            Dgl1.Item(Col1PurchInvoiceNo, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchInvoiceNo"))
                            Dgl1.Item(Col1PurchInvoiceDate, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchInvoiceDate"))
                            Dgl1.Item(Col1Qty, I).Value = AgL.XNull(DtTemp.Rows(I)("Qty"))
                            Dgl1.Item(Col1Freight, I).Value = AgL.XNull(DtTemp.Rows(I)("Freight"))
                            Dgl1.Item(Col1Packing, I).Value = AgL.XNull(DtTemp.Rows(I)("Packing"))
                            Dgl1.Item(Col1PurchAmount, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchAmount"))
                        Next I
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtOrderNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtOrderNo.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "SELECT H.DocId, H.ManualRefNo As OrderNo, H.V_Date As OrderDate
                                    FROM SaleInvoice H With (NoLock) 
                                    LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type
                                    LEFT JOIN SaleInvoiceDetail L With (NoLock) ON H.DocID = L.DocId
                                    LEFT JOIN (
	                                    SELECT InvoiceLine.ReferenceDocId, InvoiceLine.ReferenceDocIdTSr, Sum(InvoiceLine.Qty) AS InvoiceQty
	                                    FROM SaleInvoice InvoiceHead With (NoLock) 
	                                    LEFT JOIN Voucher_Type InvoiceV_Type With (NoLock) On InvoiceHead.V_Type = InvoiceV_Type.V_Type
	                                    LEFT JOIN SaleInvoiceDetail InvoiceLine With (NoLock) ON InvoiceHead.DocID = InvoiceLine.DocId
	                                    WHERE InvoiceV_Type.NCat = '" & Ncat.SaleInvoice & "' AND InvoiceLine.ReferenceDocId IS NOT NULL
	                                    GROUP BY InvoiceLine.ReferenceDocId, InvoiceLine.ReferenceDocIdTSr
                                    ) AS VInvoice ON L.DocId = VInvoice.ReferenceDocId AND L.Sr = VInvoice.ReferenceDocIdTSr
                                    LEFT JOIN (
	                                    SELECT InvoiceLine.ReferenceDocId, InvoiceLine.ReferenceDocIdTSr, Sum(ReturnLine.Qty) AS ReturnQty
	                                    FROM SaleInvoice ReturnHead With (NoLock) 
	                                    LEFT JOIN Voucher_Type ReturnV_Type With (NoLock) On ReturnHead.V_Type = ReturnV_Type.V_Type
	                                    LEFT JOIN SaleInvoiceDetail ReturnLine With (NoLock) ON ReturnHead.DocID = ReturnLine.DocId
	                                    LEFT JOIN SaleInvoiceDetail InvoiceLine With (NoLock) ON ReturnLine.ReferenceDocId = InvoiceLine.DocId
						                                    AND ReturnLine.ReferenceDocIdTSr = InvoiceLine.Sr
	                                    WHERE ReturnV_Type.NCat = '" & Ncat.SaleInvoice & "' AND InvoiceLine.ReferenceDocId IS NOT NULL
	                                    GROUP BY InvoiceLine.ReferenceDocId, InvoiceLine.ReferenceDocIdTSr
                                    ) AS VReturn ON L.DocId = VReturn.ReferenceDocId AND L.Sr = VReturn.ReferenceDocIdTSr
                                    WHERE Vt.NCat = '" & mOrderNCat & "' 
                                    AND IsNull(L.Qty,0) - (IsNull(VInvoice.InvoiceQty,0) - IsNull(VReturn.ReturnQty,0)) > 0 
                                    ORDER By H.V_Date, ManualRefNo "
                            TxtOrderNo.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Dim bRowIndex As Integer = Dgl1.CurrentCell.RowIndex
            Dim bColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Supplier
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            Dim bParentSubCode = AgL.Dman_Execute("SELECT Max(Sg.Subcode) AS SubCode
                                    FROM SaleInvoice H 
                                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                                    LEFT JOIN Item I ON L.Item = I.Code
                                    LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code
                                    LEFT JOIN ItemGroupPerson Igp ON Ig.Code = Igp.ItemGroup
                                    LEFT JOIN Subgroup Sg ON Igp.Person = Sg.Subcode
                                    WHERE H.DocID = '" & Dgl1.Item(Col1SaleOrderNo, bRowIndex).Tag & "'
                                    AND Sg.Subcode IS NOT NULL", AgL.GCn).ExecuteScalar()

                            mQry = " WITH cte AS  (
                                    SELECT Sg.SubCode, Sg.Parent , Sg.name
                                    FROM Subgroup Sg WHERE Sg.Subcode  = '" & bParentSubCode & "'
                                    UNION ALL
                                    SELECT Sg.SubCode, Sg.Parent, Sg.Name
                                    FROM Subgroup Sg JOIN cte c ON Sg.Parent = c.SubCode
                                ) SELECT SubCode, Name FROM cte "
                            Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Supplier) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Supplier) = Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Supplier
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FDataValidation() As Boolean
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1PurchInvoiceNo, I).Value = "" Then
                MsgBox("Purchase Invoice No is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If

            If Dgl1.Item(Col1PurchInvoiceDate, I).Value = "" Then
                MsgBox("Purchase Invoice Date is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If

            If Dgl1.Item(Col1PurchInvoiceDate, I).Value = "" Then
                MsgBox("Supplier is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If

            If AgL.Dman_Execute("Select Count(*) From PurchInvoice With (NoLock) Where V_Type = 'PI'
                        And VendorDocNo = '" & Dgl1.Item(Col1PurchInvoiceNo, I).Value & "'
                        And Vendor = '" & Dgl1.Item(Col1Supplier, I).Tag & "'
                        And Div_Code = '" & AgL.PubDivCode & "'
                        And Site_Code = '" & AgL.PubSiteCode & "'
                        ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() > 0 Then
                MsgBox("Purchase Invoice No " & Dgl1.Item(Col1PurchInvoiceNo, I).Value & " 
                        already exist for " & Dgl1.Item(Col1Supplier, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If
        Next
        FDataValidation = True
    End Function
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        Me.Close()
    End Sub
    Private Sub FrmSaleInvoiceUnOfficial_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If FDataValidation() = False Then e.Cancel = True : Exit Sub
    End Sub
    Private Sub FSyncSaleOrders()

        Dim mDbPath As String = "D:\DatabaseFilesSqlite\ShivaSareeCenter"
        Dim Connection As New SQLite.SQLiteConnection
        Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        Connection.Open()

        mQry = " Select H.*
            From SaleInvoice H 
            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            Where Vt.NCat = '" & mOrderNCat & "'"
        Dim DtSaleOrderSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)

        mQry = " Select L.*
            From SaleInvoice H 
            LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocId
            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            Where Vt.NCat = '" & mOrderNCat & "'"
        Dim DtSaleOrderDetailSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)

        FImportDataFromSqliteTable("SaleInvoice", "H.DocId = H_Temp.DocId", Connection, AgL.GCn, AgL.ECmd, DtSaleOrderSource)
        FImportDataFromSqliteTable("SaleInvoiceDetail", "H.DocId = H_Temp.DocId And H.Sr = H_Temp.Sr", Connection, AgL.GCn, AgL.ECmd, DtSaleOrderDetailSource)
    End Sub

    Private Sub FImportDataFromSqliteTable(bTableName As String, bJoinCondStr As String,
                                           mSourceConn As Object,
                                           mDestinationConn As Object,
                                           mDestinationCmd As Object,
                                           DtSource As DataTable)
        Dim mTrans As String = ""
        Dim DtFields As DataTable = Nothing
        Dim DtSqliteTableData As DataTable = Nothing
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim StrColumnList As String = ""
        Dim bTempTableName As String = "[#Temp_" + bTableName + "]"

        If AgL.PubServerName = "" Then
            mQry = "PRAGMA table_info('" & bTableName & "') "
        Else
            mQry = "SELECT COLUMN_NAME As Name, DATA_TYPE + IsNull('(' + Convert(NVARCHAR,CHARACTER_MAXIMUM_LENGTH) + ')','') AS Type 
                FROM INFORMATION_SCHEMA.Columns WHERE TABLE_NAME = '" & bTableName & "'  
                ORDER BY ORDINAL_POSITION "
        End If
        DtFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        StrColumnList = ""
        For J = 0 To DtFields.Rows.Count - 1
            If StrColumnList = "" Then
                StrColumnList = DtFields.Rows(J)("Name")
            Else
                StrColumnList += ", " & DtFields.Rows(J)("Name")
            End If
        Next

        mQry = " CREATE " & IIf(AgL.PubServerName = "", "Temp", "") & " TABLE " & bTempTableName & "( "
        For I = 0 To DtFields.Rows.Count - 1
            mQry += DtFields.Rows(I)("Name") + " " + DtFields.Rows(I)("Type") + IIf(I = DtFields.Rows.Count - 1, "", ",")
        Next
        mQry += " ) "
        AgL.Dman_ExecuteNonQry(mQry, mDestinationConn, mDestinationCmd)

        If AgL.PubServerName = "" Then
            For I = 0 To DtSource.Rows.Count - 1
                mQry = " INSERT INTO (" + StrColumnList + ")"
                For J = 0 To DtFields.Rows.Count - 1
                    If J = 0 Then mQry += " Select "
                    mQry += DtFields.Rows(0)(DtFields.Rows(J)("Name"))
                Next
                AgL.Dman_ExecuteNonQry(mQry, mDestinationConn, mDestinationCmd)
            Next
        Else
            Using bulkCopy As SqlClient.SqlBulkCopy = New SqlClient.SqlBulkCopy(mDestinationConn, SqlClient.SqlBulkCopyOptions.Default, mDestinationCmd.Transaction)
                bulkCopy.DestinationTableName = bTempTableName
                bulkCopy.BulkCopyTimeout = 500
                bulkCopy.WriteToServer(DtSource)
            End Using
        End If


        StrColumnList = StrColumnList.Replace("00", "DateTime")

        mQry = "INSERT INTO " & bTableName & "(" & StrColumnList & ")
                Select H_Temp." & Replace(StrColumnList, ",", ",H_Temp.") & "
                From " & bTempTableName & " H_Temp 
                LEFT JOIN " & bTableName & " H On " & bJoinCondStr &
                " Where H.DocId Is Null "
        AgL.Dman_ExecuteNonQry(mQry, mDestinationConn, mDestinationCmd)
    End Sub
End Class