Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants

Public Class FrmSaleReturnInvoiceSummary
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public Const ColSNo As String = "S.No."
    Public Const Col1SaleOrderNo As String = "Order No"
    Public Const Col1SaleOrderDate As String = "Order Date"
    Public Const Col1ParentSupplier As String = "Parent Supplier"
    Public Const Col1Supplier As String = "Supplier"
    Public Const Col1PurchInvoiceNo As String = "Purch Invoice No"
    Public Const Col1PurchInvoiceDate As String = "Purch Invoice Date"

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
            .AddAgTextColumn(Dgl1, Col1SaleOrderNo, 100, 0, Col1SaleOrderNo, True, True)
            .AddAgTextColumn(Dgl1, Col1SaleOrderDate, 100, 0, Col1SaleOrderDate, True, True)
            .AddAgTextColumn(Dgl1, Col1ParentSupplier, 250, 0, Col1ParentSupplier, False, False)
            .AddAgTextColumn(Dgl1, Col1Supplier, 250, 0, Col1Supplier, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchInvoiceNo, 130, 0, Col1PurchInvoiceNo, True, False)
            .AddAgDateColumn(Dgl1, Col1PurchInvoiceDate, 110, Col1PurchInvoiceDate, True, False)
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
        TxtOrderNo.Focus()
        TxtPartyName.Enabled = False
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
                    mQry = "Select H.SaleToParty, Sg.Name As SaleToPartyName, Sg.Parent
                            From SaleInvoice H  With (NoLock)                            
                            LEFT JOIN viewHelpSubGroup Sg On H.SaleToParty = Sg.Code
                            Where H.DocId = '" & TxtOrderNo.Tag & "'"
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        TxtPartyName.Tag = AgL.XNull(DtTemp.Rows(0)("SaleToParty"))
                        TxtPartyName.Text = AgL.XNull(DtTemp.Rows(0)("SaleToPartyName"))


                        If ClsMain.IsPartyBlocked(AgL.XNull(DtTemp.Rows(0)("SaleToParty")), Ncat.SaleInvoice) Then
                            MsgBox("Party is blocked for Sale Invoice. Record will not be saved")
                        End If

                        If AgL.XNull(DtTemp.Rows(0)("Parent")) <> "" Then
                            If ClsMain.IsPartyBlocked(AgL.XNull(DtTemp.Rows(0)("Parent")), Ncat.SaleInvoice) Then
                                MsgBox("Party is blocked for Sale Invoice. Record will not be saved")
                            End If
                        End If
                    End If



                    mQry = "Select H.DocId As SaleOrderDocId, Max(H.ManualRefNo) As SaleOrderNo, Max(H.MinDeliveryDate) as MinDeliveryDate, 
                            Cast(strftime('%d/%m/%Y', Max(H.V_Date)) As nvarchar) As SaleOrderDate, 
                            Max(Supp.SubCode) As DefaultSupplierCode, Max(Supp.Name) As DefaultSupplierName
                            From SaleInvoice H  With (NoLock)
                            LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                            LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocId
                            LEFT JOIN Item I ON L.Item = I.Code
                            LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                            LEFT JOIN SubGroup Supp On Ig.DefaultSupplier = Supp.SubCode
                            Where IfNull(H.ReferenceDocId, H.DocId) = '" & TxtOrderNo.Tag & "'
                            Group By H.DocId, Sg.SubCode "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then

                        If AgL.XNull(DtTemp.Rows(0)("MinDeliveryDate")) <> "" Then
                            If CDate(AgL.XNull(DtTemp.Rows(0)("MinDeliveryDate"))) > CDate(AgL.PubLoginDate) Then
                                MsgBox("Order's Min. Delivery Date is " & ClsMain.FormatDate(AgL.XNull(DtTemp.Rows(0)("MinDeliveryDate")) & vbCrLf & " Can't Create Invoice Now. "))
                                e.Cancel = True
                                Exit Sub
                            End If
                        End If

                        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                        For I = 0 To DtTemp.Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(Col1SaleOrderNo, I).Tag = AgL.XNull(DtTemp.Rows(I)("SaleOrderDocId"))
                            Dgl1.Item(Col1SaleOrderNo, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleOrderNo"))
                            Dgl1.Item(Col1SaleOrderDate, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleOrderDate"))
                            Dgl1.Item(Col1ParentSupplier, I).Tag = AgL.XNull(DtTemp.Rows(I)("DefaultSupplierCode"))
                            Dgl1.Item(Col1ParentSupplier, I).Value = AgL.XNull(DtTemp.Rows(I)("DefaultSupplierName"))
                            If ClsMain.IsPartyBlocked(AgL.XNull(DtTemp.Rows(I)("DefaultSupplierCode")), Ncat.PurchaseInvoice) Then
                                MsgBox(AgL.XNull(DtTemp.Rows(I)("DefaultSupplierCode")) & " is blocked for Purchase Invoice. Record will not be saved")
                            End If



                            mQry = " Select SubCode As SupplierCode, Name As SupplierName
                                    From SubGroup 
                                    Where Parent = '" & Dgl1.Item(Col1ParentSupplier, I).Tag & "' "
                            Dim DtChildParty As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtChildParty.Rows.Count = 1 Then
                                Dgl1.Item(Col1Supplier, I).Tag = AgL.XNull(DtChildParty.Rows(0)("SupplierCode"))
                                Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtChildParty.Rows(0)("SupplierName"))

                                If ClsMain.IsPartyBlocked(AgL.XNull(DtChildParty.Rows(0)("SupplierCode")), Ncat.PurchaseInvoice) Then
                                    MsgBox(AgL.XNull(DtChildParty.Rows(0)("SupplierName")) & " is blocked for Purchase Invoice. Record will not be saved")
                                End If
                            End If


                        Next I
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If TxtOrderNo.Focused = True And e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtOrderNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtOrderNo.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            'mQry = "SELECT H.DocId, H.ManualRefNo As OrderNo, H.V_Date As OrderDate " &
                            '        " FROM SaleInvoice H With (NoLock) 
                            '            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                            '            Where Vt.NCat = '" & mOrderNCat & "'
                            '            Order By H.V_Date, ManualRefNo "


                            mQry = "SELECT H.DocID, Max(H.V_Type || '-' || H.ManualRefNo) AS OrderNo, Max(H.V_Date) as OrderDate, Round(Sum(VOrderBalance.OrderBalanceAmount),2) AS OrderBalanceAmount
                                    FROM (" & FGetSaleOrderBalanceQry(True) & " ) AS VOrderBalance
                                    LEFT JOIN SaleInvoice H ON VOrderBalance.DocId = H.DocID
                                    Where H.Div_Code = '" & AgL.PubDivCode & "' 
                                    And H.Site_Code = '" & AgL.PubSiteCode & "'
                                    GROUP BY H.DocID "

                            'mQry = "SELECT H.DocId, H.ManualRefNo As OrderNo, H.V_Date As OrderDate
                            '        FROM SaleInvoice H With (NoLock) 
                            '        LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type
                            '        LEFT JOIN SaleInvoiceDetail L With (NoLock) ON H.DocID = L.DocId
                            '        LEFT JOIN (
                            '         SELECT InvoiceLine.ReferenceDocId, InvoiceLine.ReferenceDocIdTSr, Sum(InvoiceLine.Qty) AS InvoiceQty
                            '         FROM SaleInvoice InvoiceHead With (NoLock) 
                            '         LEFT JOIN Voucher_Type InvoiceV_Type With (NoLock) On InvoiceHead.V_Type = InvoiceV_Type.V_Type
                            '         LEFT JOIN SaleInvoiceDetail InvoiceLine With (NoLock) ON InvoiceHead.DocID = InvoiceLine.DocId
                            '         WHERE InvoiceV_Type.NCat = '" & Ncat.SaleInvoice & "' AND InvoiceLine.ReferenceDocId IS NOT NULL
                            '         GROUP BY InvoiceLine.ReferenceDocId, InvoiceLine.ReferenceDocIdTSr
                            '        ) AS VInvoice ON L.DocId = VInvoice.ReferenceDocId AND L.Sr = VInvoice.ReferenceDocIdTSr
                            '        LEFT JOIN (
                            '         SELECT InvoiceLine.ReferenceDocId, InvoiceLine.ReferenceDocIdTSr, Sum(ReturnLine.Qty) AS ReturnQty
                            '         FROM SaleInvoice ReturnHead With (NoLock) 
                            '         LEFT JOIN Voucher_Type ReturnV_Type With (NoLock) On ReturnHead.V_Type = ReturnV_Type.V_Type
                            '         LEFT JOIN SaleInvoiceDetail ReturnLine With (NoLock) ON ReturnHead.DocID = ReturnLine.DocId
                            '         LEFT JOIN SaleInvoiceDetail InvoiceLine With (NoLock) ON ReturnLine.ReferenceDocId = InvoiceLine.DocId
                            '              AND ReturnLine.ReferenceDocIdTSr = InvoiceLine.Sr
                            '         WHERE ReturnV_Type.NCat = '" & Ncat.SaleInvoice & "' AND InvoiceLine.ReferenceDocId IS NOT NULL
                            '         GROUP BY InvoiceLine.ReferenceDocId, InvoiceLine.ReferenceDocIdTSr
                            '        ) AS VReturn ON L.DocId = VReturn.ReferenceDocId AND L.Sr = VReturn.ReferenceDocIdTSr
                            '        WHERE Vt.NCat = '" & Ncat.SaleOrder & "' 
                            '        AND IfNull(L.Qty,0) - (IfNull(VInvoice.InvoiceQty,0) - IfNull(VReturn.ReturnQty,0)) > 0 
                            '        ORDER By H.V_Date, ManualRefNo "
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
                            mQry = " WITH cte AS  (
                                        SELECT Sg.Code, (Case When Sg.Code = Sg.Parent Then Null Else Sg.Parent End) As Parent , Sg.name
                                        FROM ViewHelpSubgroup Sg 
                                        WHERE Sg.Code  = '" & Dgl1.Item(Col1ParentSupplier, bRowIndex).Tag & "'
                                        UNION ALL
                                        SELECT Sg.Code, (Case When Sg.Code = Sg.Parent Then Null Else Sg.Parent End) As Paremt , Sg.Name
                                        FROM ViewHelpSubgroup Sg 
                                        JOIN cte c ON (Case When Sg.Code = Sg.Parent Then Null Else Sg.Parent End) = c.Code
                                    ) SELECT Code, Name FROM cte Where Parent Is Not Null "


                            'mQry = " WITH cte AS  (
                            '            SELECT Sg.SubCode, (Case When Sg.SubCode = Sg.Parent Then Null Else Sg.Parent End) As Parent , Sg.name
                            '            FROM ViewHelpSubgroup Sg 
                            '            WHERE Sg.Subcode  = '" & Dgl1.Item(Col1ParentSupplier, bRowIndex).Tag & "'
                            '            UNION ALL
                            '            SELECT Sg.SubCode, (Case When Sg.SubCode = Sg.Parent Then Null Else Sg.Parent End) As Paremt , Sg.Name
                            '            FROM ViewHelpSubgroup Sg 
                            '            JOIN cte c ON (Case When Sg.SubCode = Sg.Parent Then Null Else Sg.Parent End) = c.SubCode
                            '        ) SELECT SubCode, Name FROM cte "

                            'mQry = " WITH cte AS  (
                            '        SELECT Sg.SubCode, Sg.Parent , Sg.name
                            '        FROM Subgroup Sg WHERE Sg.Subcode  = '" & bParentSubCode & "'
                            '        UNION ALL
                            '        SELECT Sg.SubCode, Sg.Parent, Sg.Name
                            '        FROM Subgroup Sg JOIN cte c ON Sg.Parent = c.SubCode
                            '    ) SELECT SubCode, Name FROM cte "
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

            If CDate(Dgl1.Item(Col1PurchInvoiceDate, I).Value) > CDate(AgL.PubLoginDate) Then
                MsgBox("Future date transaction is not allowed.", MsgBoxStyle.Information)
                Dgl1.CurrentCell = Dgl1.Item(Col1PurchInvoiceDate, I)
                Dgl1.Focus()
                FDataValidation = False
                Exit Function
            End If



            If Dgl1.Item(Col1Supplier, I).Value = "" Then
                MsgBox("Supplier is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If

            If AgL.Dman_Execute("Select Count(*) From PurchInvoice With (NoLock) Where V_Type = 'PI'
                        And VendorDocNo = '" & Dgl1.Item(Col1PurchInvoiceNo, I).Value & "'
                        And Vendor = '" & Dgl1.Item(Col1Supplier, I).Tag & "'
                        And Date(V_Date) >= " & AgL.Chk_Date(AgL.PubStartDate) & "
                        And Date(V_Date) <= " & AgL.Chk_Date(AgL.PubEndDate) & "
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
    Private Sub FrmSaleReturnInvoiceSummary_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If FDataValidation() = False Then e.Cancel = True : Exit Sub
    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Dispose()
        End If
    End Sub

    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Select Case Dgl1.CurrentCell.ColumnIndex
            Case Dgl1.Columns(Col1Supplier).Index
                If ClsMain.IsPartyBlocked(Dgl1.Item(Col1Supplier, Dgl1.CurrentCell.RowIndex).Tag, Ncat.PurchaseInvoice) Then
                    MsgBox("Party is blocked for Purchase Invoice. Record will not be saved")
                End If
        End Select
    End Sub

    Public Shared Function FGetSaleOrderBalanceQry(Optional CalculateContraBalanceOnValueYN As Boolean = False,
                                           Optional bParty As String = "") As String
        Dim mQry As String = "Select L.DocID, L.Sr, L.Amount - IfNull(VInvoiceReturn.ReturnAmount, 0) As OrderBalanceAmount,
                        L.Qty - IfNull(VInvoiceReturn.ReturnQty, 0) AS OrderBalanceQty
                        From SaleInvoice H 
	                    Left Join SaleInvoiceDetail L ON H.DocID = L.DocID
                        Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                    Left Join(
		                    SELECT L.SaleInvoice, L.SaleInvoiceSr, Sum(L.Qty) As ReturnQty,
                            Sum(Sid.Amount) As ReturnAmount
		                    From SaleInvoice H 
		                    Left Join SaleInvoiceDetail L ON H.DocID = L.DocID
                            Left Join SaleInvoiceDetail Sid ON L.SaleInvoice = Sid.DocId And L.SaleInvoiceSr = Sid.Sr
                            Left Join Voucher_Type Vt ON H.V_Type = Vt.V_Type
                            WHERE Vt.NCat = '" & Ncat.SaleReturn & "'	
                            Group BY L.SaleInvoice, L.SaleInvoiceSr
	                    ) AS VInvoiceReturn ON L.DocID = VInvoiceReturn.SaleInvoice And L.Sr = VInvoiceReturn.SaleInvoiceSr
	                    WHERE IfNull(H.Status,'Active') = 'Active' "
        If bParty <> "" Then
            mQry += " And H.SaleToParty = '" & bParty & "' "
        End If

        mQry += " And Vt.NCat = '" & Ncat.SaleInvoice & "' "

        If CalculateContraBalanceOnValueYN = True Then
            mQry += " And L.Amount - IfNull(VInvoiceReturn.ReturnAmount,0) > 0 "
        Else
            mQry += " And L.Qty - IfNull(VInvoiceReturn.ReturnQty,0) > 0 "
        End If
        FGetSaleOrderBalanceQry = mQry
    End Function
End Class