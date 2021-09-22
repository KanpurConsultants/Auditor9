Public Class FrmSaleInvoiceAdj
    Public WithEvents DglSO As New AgControls.AgDataGrid
    Public WithEvents DglSI As New AgControls.AgDataGrid
    Public WithEvents DglAdj As New AgControls.AgDataGrid

    Protected Const Col_Sno As String = "Sr"

    Protected Const ColSO_DocID As String = "DocID"
    Protected Const ColSO_Sr As String = "Sr"
    Protected Const ColSO_StockOutNo As String = "Stock Out No"
    Protected Const ColSO_Party As String = "Party"
    Protected Const ColSO_Item As String = "Item"
    Protected Const ColSO_Qty As String = "Qty"
    Protected Const ColSO_AdjQty As String = "Adj Qty"
    Protected Const ColSO_BalQty As String = "Bal Qty"


    Protected Const ColSI_DocID As String = "DocID"
    Protected Const ColSI_Sr As String = "Sr"
    Protected Const ColSI_StockOutNo As String = "Stock In No"
    Protected Const ColSI_Party As String = "Party"
    Protected Const ColSI_Item As String = "Item"
    Protected Const ColSI_Qty As String = "Qty"
    Protected Const ColSI_AdjQty As String = "Adj Qty"
    Protected Const ColSI_BalQty As String = "Bal Qty"
    Protected Const ColSI_Adj As String = "Adj"

    Protected Const ColAdj_SalesInvoice As String = "Sales Invoice"
    Protected Const ColAdj_SalesInvoiceSr As String = "Sales Invoice Sr"
    Protected Const ColAdj_PurchaseInvoice As String = "Purchase Invoice"
    Protected Const ColAdj_PurchaseInvoiceSr As String = "Purchase Invoice Sr"
    Protected Const ColAdj_Qty As String = "Adj. Qty"

    Dim DtSO As DataTable
    Dim DtSI As DataTable
    Dim DtAdj As DataTable

    Private trdSave As Threading.Thread
    Private trdFill As Threading.Thread

    Private Property ProgressStatus() As String
        Get
            Return LblStatus.Text
        End Get
        Set(ByVal value As String)
            LblStatus.Text = value
        End Set
    End Property

    Private Sub Ini_Grid()
        DglSO.EnableHeadersVisualStyles = False
        DglSO.AgSkipReadOnlyColumns = True
        DglSO.ColumnHeadersHeight = 35
        'DglSI.ReadOnly = True
        DglSO.AllowUserToAddRows = False
        'DglSI.DefaultCellStyle.WrapMode = DataGridViewTriState.True


        DglSO.Columns(ColSO_DocID).Visible = False
        DglSO.Columns(ColSO_Sr).Visible = False
        DglSO.Columns(ColSO_StockOutNo).Width = 100
        DglSO.Columns(ColSO_Party).Width = 100
        DglSO.Columns(ColSO_Item).Width = 150
        DglSO.Columns(ColSO_Qty).Visible = False
        DglSO.Columns(ColSO_AdjQty).Visible = False
        DglSO.Columns(ColSO_BalQty).Width = 80

        DglSI.EnableHeadersVisualStyles = False
        DglSI.AgSkipReadOnlyColumns = True
        DglSI.ColumnHeadersHeight = 35
        'DglPI.ReadOnly = True
        DglSI.AllowUserToAddRows = False
        'DglPI.DefaultCellStyle.WrapMode = DataGridViewTriState.True


        DglSI.Columns(ColSI_DocID).Visible = False
        DglSI.Columns(ColSI_Sr).Visible = False
        DglSI.Columns(ColSI_StockOutNo).Width = 100
        DglSI.Columns(ColSI_Party).Width = 100
        DglSI.Columns(ColSI_Item).Width = 150
        DglSI.Columns(ColSI_Qty).Visible = False
        DglSI.Columns(ColSI_AdjQty).Visible = False
        DglSI.Columns(ColSI_BalQty).Width = 80

        AgCL.AddAgButtonColumn(DglSI, ColSI_Adj, 40, ColSI_Adj, True, False)
        DglSI.Columns(ColSI_Adj).DisplayIndex = 0


    End Sub

    Private Sub FrmSaleInvoiceAdj_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 660, 992, 0, 0)

        AgL.GridDesign(DglSO)
        AgL.GridDesign(DglSI)
        AgL.GridDesign(DglAdj)
        AgL.AddAgDataGrid(DglSO, Panel1)
        AgL.AddAgDataGrid(DglSI, Panel2)
        AgL.AddAgDataGrid(DglAdj, Panel3)

        MoveRec()
        Ini_Grid()
    End Sub

    Private Sub MoveRec()
        Dim mQry As String

        mQry = "SELECT L.DocId, L.Sr, H.ReferenceNo + '-'  + H.V_Type AS [Stock Out No], Sg.Name + (CASE WHEN C.CityName IS NULL THEN '' ELSE ', ' + C.CityName End) AS Party, I.Description AS Item, L.Qty, 0 AS [Adj Qty], 0 as [Bal Qty] " & _
               "FROM SaleInvoiceDetail L " & _
               "LEFT JOIN SaleInvoice H ON L.DocId = H.DocID " & _
               "LEFT JOIN Item I ON L.Item = I.Code  " & _
               "LEFT JOIN SubGroup Sg ON H.SaleToParty = Sg.SubCode  " & _
               "LEFT JOIN City c ON sg.CityCode = c.CityCode " & _
               "WHERE L.ReferenceDocId IS NULL AND L.Qty >0 " & _
               "Order By H.V_Date, H.DocID"


        mQry = "SELECT S.DocID, S.Sr, S.RecId + '-' + S.V_Type  AS [Stock Out No], Sg.Name + (CASE WHEN C.CityName IS NULL THEN '' ELSE ', ' + C.CityName End) AS Party, I.Description AS Item, IsNull(S.Qty_Iss,0) - IsNull(SAdj.AdjQty,0) Qty, 0 AS [Adj Qty], 0 as [Bal Qty]  " & _
               "FROM Stock S " & _
               "LEFT JOIN (SELECT StockOutDocID, StockOutSr, Sum(AdjQty) AS AdjQty FROM StockAdj GROUP BY StockOutDocID, stockOutSr  " & _
               "          ) AS SAdj ON S.DocID = SAdj.StockOutDocID AND S.Sr = Sadj.StockOutSr " & _
               "LEFT JOIN Item I ON S.Item = I.Code " & _
               "LEFT JOIN SubGroup Sg ON S.SubCode = Sg.SubCode " & _
               "LEFT JOIN City C ON Sg.CityCode = C.CityCode   " & _
               "WHERE IsNull(S.Qty_Iss, 0) - IsNull(SAdj.AdjQty, 0) > 0 " & _
               "And S.Site_Code = '" & AgL.PubSiteCode & "' And S.Div_Code = '" & AgL.PubDivCode & "' " & _
               "Order By S.V_Date, S.DocID "
        DtSO = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DtSO.Columns("Bal Qty").Expression = "Qty - [Adj Qty]"
        DglSO.DataSource = DtSO


        mQry = "SELECT L.DocId, L.Sr, H.ReferenceNo + '-'  + H.V_Type AS [Stock In No], Sg.Name + (CASE WHEN C.CityName IS NULL THEN '' ELSE ', ' + C.CityName End) AS Party, I.Description AS Item, L.Qty - IsNull(SI.SI_Qty,0) AS Qty, 0 AS [Adj Qty], 0 as [Bal Qty] " & _
               "FROM PurchChallanDetail L " & _
               "LEFT JOIN PurchChallan H ON L.DocId = H.DocID " & _
               "LEFT JOIN Item I ON L.Item = I.Code  " & _
               "LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode " & _
               "LEFT JOIN City c ON sg.CityCode = c.CityCode  " & _
               "LEFT JOIN (SELECT L.ReferenceDocId, L.ReferenceDocIdSr, Sum(L.Qty) AS SI_Qty " & _
               "           FROM SaleInvoiceDetail L GROUP BY L.ReferenceDocId, L.ReferenceDocIdSr " & _
               "          )AS SI ON L.DocId = SI.ReferenceDocId AND L.Sr = SI.ReferenceDocIdSr " & _
               "WHERE L.Qty - IsNull(SI.SI_Qty, 0) > 0 " & _
               "Order by H.V_Date, H.DocID "

        mQry = "SELECT S.DocID, S.Sr, S.RecId + '-' + S.V_Type  AS [Stock In No], Sg.Name + (CASE WHEN C.CityName IS NULL THEN '' ELSE ', ' + C.CityName End) AS Party, I.Description AS Item, IsNull(S.Qty_Rec,0) - IsNull(SAdj.AdjQty,0) Qty, 0 AS [Adj Qty], 0 as [Bal Qty]  " & _
               "FROM Stock S " & _
               "LEFT JOIN (SELECT StockInDocID, StockInSr, Sum(AdjQty) AS AdjQty FROM StockAdj GROUP BY StockInDocID, StockInSr  " & _
               "          ) AS SAdj ON S.DocID = SAdj.StockInDocID AND S.Sr = Sadj.StockInSr " & _
               "LEFT JOIN Item I ON S.Item = I.Code " & _
               "LEFT JOIN SubGroup Sg ON S.SubCode = Sg.SubCode " & _
               "LEFT JOIN City C ON Sg.CityCode = C.CityCode   " & _
               "WHERE IsNull(S.Qty_Rec, 0) - IsNull(SAdj.AdjQty, 0) > 0  " & _
               "And S.Site_Code = '" & AgL.PubSiteCode & "' And S.Div_Code = '" & AgL.PubDivCode & "' " & _
               "Order By S.V_Date, S.DocID "

        DtSI = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DtSI.Columns("Bal Qty").Expression = "Qty - [Adj Qty]"
        DglSI.DataSource = DtSI

        mQry = " Declare @TblTemp AS Table(StockOutDocID Varchar(21), StockOutSr INT, StockInDocID Varchar(21), StockInSr INT, AdjQty FLOAT)"
        mQry += " Select * from @TblTemp "
        DtAdj = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglAdj.DataSource = DtAdj

    End Sub

    Private Sub DglPI_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglSI.CellContentClick
        Dim DrDtAdj As DataRow = Nothing
        Select Case DglSI.Columns(DglSI.CurrentCell.ColumnIndex).Name
            Case ColSI_Adj
                If Val(DglSO.Item(ColSO_BalQty, DglSO.CurrentCell.RowIndex).Value) <= 0 Then Exit Sub
                If DglSO.SelectedRows IsNot Nothing Then
                    If DglSO.Item(ColSO_Item, DglSO.CurrentCell.RowIndex).Value = DglSI.Item(ColSI_Item, DglSI.CurrentCell.RowIndex).Value Then
                        If Val(DglSO.Item(ColSO_BalQty, DglSO.CurrentCell.RowIndex).Value) <= Val(DglSI.Item(ColSI_BalQty, DglSI.CurrentCell.RowIndex).Value) Then
                            DrDtAdj = DtAdj.NewRow
                            DrDtAdj("StockOutDocID") = DglSO.Item(ColSO_DocID, DglSO.CurrentCell.RowIndex).Value
                            DrDtAdj("StockOutSr") = DglSO.Item(ColSO_Sr, DglSO.CurrentCell.RowIndex).Value
                            DrDtAdj("StockInDocID") = DglSI.Item(ColSI_DocID, DglSI.CurrentCell.RowIndex).Value
                            DrDtAdj("StockInSr") = DglSI.Item(ColSI_Sr, DglSI.CurrentCell.RowIndex).Value
                            DrDtAdj("AdjQty") = DglSO.Item(ColSO_BalQty, DglSO.CurrentCell.RowIndex).Value

                            DtAdj.Rows.Add(DrDtAdj)

                            DglSO.Item(ColSO_AdjQty, DglSO.CurrentCell.RowIndex).Value = Val(DglSO.Item(ColSO_AdjQty, DglSO.CurrentCell.RowIndex).Value) + Val(DglSO.Item(ColSO_BalQty, DglSO.CurrentCell.RowIndex).Value)
                            DglSI.Item(ColSI_AdjQty, DglSI.CurrentCell.RowIndex).Value = Val(DglSI.Item(ColSI_AdjQty, DglSI.CurrentCell.RowIndex).Value) + Val(DglSO.Item(ColSO_BalQty, DglSO.CurrentCell.RowIndex).Value)

                            DtSO.AcceptChanges()
                            DtSI.AcceptChanges()
                        ElseIf Val(DglSO.Item(ColSO_BalQty, DglSO.CurrentCell.RowIndex).Value) > Val(DglSI.Item(ColSI_BalQty, DglSI.CurrentCell.RowIndex).Value) Then
                            DrDtAdj = DtAdj.NewRow
                            DrDtAdj("StockOutDocID") = DglSO.Item(ColSO_DocID, DglSO.CurrentCell.RowIndex).Value
                            DrDtAdj("StockOutSr") = DglSO.Item(ColSO_Sr, DglSO.CurrentCell.RowIndex).Value
                            DrDtAdj("StockInDocID") = DglSI.Item(ColSI_DocID, DglSI.CurrentCell.RowIndex).Value
                            DrDtAdj("StockInSr") = DglSI.Item(ColSI_Sr, DglSI.CurrentCell.RowIndex).Value
                            DrDtAdj("AdjQty") = DglSI.Item(ColSI_BalQty, DglSI.CurrentCell.RowIndex).Value

                            DtAdj.Rows.Add(DrDtAdj)

                            DglSO.Item(ColSO_AdjQty, DglSO.CurrentCell.RowIndex).Value = Val(DglSO.Item(ColSO_AdjQty, DglSO.CurrentCell.RowIndex).Value) + Val(DglSI.Item(ColSI_BalQty, DglSI.CurrentCell.RowIndex).Value)
                            DglSI.Item(ColSI_AdjQty, DglSI.CurrentCell.RowIndex).Value = Val(DglSI.Item(ColSI_AdjQty, DglSI.CurrentCell.RowIndex).Value) + Val(DglSI.Item(ColSI_BalQty, DglSI.CurrentCell.RowIndex).Value)

                            DtSO.AcceptChanges()
                            DtSI.AcceptChanges()
                        End If
                    Else
                        MsgBox("Items of stock out and stock in doesn't match")
                    End If
                Else
                    MsgBox("Select any row in stock out")
                End If
        End Select
    End Sub

    Private Sub DglSO_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglSO.CellEnter
        If DtSI Is Nothing Then Exit Sub
        If DglSO.CurrentCell Is Nothing Then Exit Sub


        DtSI.DefaultView.RowFilter = Nothing
        DtSI.DefaultView.RowFilter = " Item = '" & DglSO.Item(ColSO_Item, DglSO.CurrentCell.RowIndex).Value & "' and [Bal Qty]>0 "
    End Sub

    Sub Fill()
        Try
            Dim DrDtAdj As DataRow
            Dim intSoRowIndex As Integer
            Dim intSiRowIndex As Integer
            Dim dblAdjQty As Double

            Dim objProgressbar As New AgLibrary.FrmProgressBar
            objProgressbar.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog



            For intSoRowIndex = 0 To DglSO.Rows.Count - 1
                If Val(DglSO.Item(ColSO_BalQty, intSoRowIndex).Value) > 0 Then
                    DtSI.DefaultView.RowFilter = Nothing
                    DtSI.DefaultView.RowFilter = " Item = '" & DglSO.Item(ColSO_Item, intSoRowIndex).Value & "' and [Bal Qty]>0 "

                    For intSiRowIndex = 0 To DtSI.DefaultView.Count - 1
                        dblAdjQty = 0
                        DtSI.DefaultView.RowFilter = Nothing
                        DtSI.DefaultView.RowFilter = " Item = '" & DglSO.Item(ColSO_Item, intSoRowIndex).Value & "' and [Bal Qty]>0 "

                        If Val(DglSO.Item(ColSO_BalQty, intSoRowIndex).Value) <= 0 Then Continue For
                        If DglSO.Item(ColSO_Item, intSoRowIndex).Value = DglSI.Item(ColSI_Item, 0).Value Then
                            If Val(DglSO.Item(ColSO_BalQty, intSoRowIndex).Value) <= Val(DglSI.Item(ColSI_BalQty, 0).Value) Then
                                dblAdjQty = Val(DglSO.Item(ColSO_BalQty, intSoRowIndex).Value)
                                DrDtAdj = DtAdj.NewRow
                                DrDtAdj("StockOutDocID") = DglSO.Item(ColSO_DocID, intSoRowIndex).Value
                                DrDtAdj("StockOutSr") = DglSO.Item(ColSO_Sr, intSoRowIndex).Value
                                DrDtAdj("StockInDocID") = DglSI.Item(ColSI_DocID, 0).Value
                                DrDtAdj("StockInSr") = DglSI.Item(ColSI_Sr, 0).Value
                                DrDtAdj("AdjQty") = dblAdjQty

                                DtAdj.Rows.Add(DrDtAdj)

                                DglSO.Item(ColSO_AdjQty, intSoRowIndex).Value = Val(DglSO.Item(ColSO_AdjQty, intSoRowIndex).Value) + dblAdjQty
                                DglSI.Item(ColSI_AdjQty, 0).Value = Val(DglSI.Item(ColSI_AdjQty, 0).Value) + dblAdjQty

                                DtSO.AcceptChanges()
                                DtSI.AcceptChanges()
                            ElseIf Val(DglSO.Item(ColSO_BalQty, intSoRowIndex).Value) > Val(DglSI.Item(ColSI_BalQty, 0).Value) Then
                                dblAdjQty = Val(DglSI.Item(ColSI_BalQty, 0).Value)
                                DrDtAdj = DtAdj.NewRow
                                DrDtAdj("StockOutDocID") = DglSO.Item(ColSO_DocID, intSoRowIndex).Value
                                DrDtAdj("StockOutSr") = DglSO.Item(ColSO_Sr, intSoRowIndex).Value
                                DrDtAdj("StockInDocID") = DglSI.Item(ColSI_DocID, 0).Value
                                DrDtAdj("StockInSr") = DglSI.Item(ColSI_Sr, 0).Value
                                DrDtAdj("AdjQty") = dblAdjQty

                                DtAdj.Rows.Add(DrDtAdj)

                                DglSO.Item(ColSO_AdjQty, intSoRowIndex).Value = Val(DglSO.Item(ColSO_AdjQty, intSoRowIndex).Value) + dblAdjQty
                                DglSI.Item(ColSI_AdjQty, 0).Value = Val(DglSI.Item(ColSI_AdjQty, 0).Value) + dblAdjQty

                                DtSO.AcceptChanges()
                                DtSI.AcceptChanges()
                            End If
                        Else
                            MsgBox("Items of stock out and stock in doesn't match")
                        End If



                    Next


                End If

                objProgressbar.Show()
                objProgressbar.Text = "Adjusting : " + DglSO.Rows.Count.ToString + " \ " + (intSoRowIndex + 1).ToString
                objProgressbar.Refresh()

                DtAdj.AcceptChanges()
                'Threading.Thread.Sleep(100)
            Next


            objProgressbar.Dispose()

            If DglSO.SelectedRows IsNot Nothing Then
            Else
                MsgBox("Select any row in stock out")
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'trdFill = New Threading.Thread(AddressOf Fill)
        'trdFill.IsBackground = True
        'trdFill.Start()
        Fill()
        'DtSO.AcceptChanges()
        'DtSI.AcceptChanges()
        'DtAdj.AcceptChanges()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        'trdSave = New Threading.Thread(AddressOf UpdateDb)
        'trdSave.IsBackground = True
        'trdSave.Start()
        UpdateDb()
        Me.Dispose()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Dispose()

    End Sub

    Private Sub UpdateDb()
        Dim i As Integer, mQry As String

        Dim objProgressbar As New AgLibrary.FrmProgressBar
        objProgressbar.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog


        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans

        For i = 0 To DtAdj.Rows.Count - 1
            mQry = "Update SaleChallanDetail Set " & _
                   "ReferenceDocID = " & AgL.Chk_Text(DtAdj.Rows(i)("StockInDocID")) & ", ReferenceDocIDSr =" & AgL.Chk_Text(DtAdj.Rows(i)("StockInSr")) & " " & _
                   "Where DocID = '" & DtAdj.Rows(i)("StockOutDocID") & "' And Sr ='" & DtAdj.Rows(i)("StockOutSr") & "'  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            mQry = "Update SaleInvoiceDetail Set " & _
                   "ReferenceDocID = " & AgL.Chk_Text(DtAdj.Rows(i)("StockInDocID")) & ", ReferenceDocIDSr =" & AgL.Chk_Text(DtAdj.Rows(i)("StockInSr")) & " " & _
                   "Where SaleChallan = '" & DtAdj.Rows(i)("StockOutDocID") & "' And SaleChallanSr ='" & DtAdj.Rows(i)("StockOutSr") & "'  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "INSERT INTO dbo.StockAdj(StockInDocID,StockInSr,StockOutDocID,StockOutSr,Site_Code,Div_Code,AdjQty)" & _
                   "Values (" & AgL.Chk_Text(DtAdj.Rows(i)("StockInDocID")) & ", " & AgL.Chk_Text(DtAdj.Rows(i)("StockInSr")) & ", " & AgL.Chk_Text(DtAdj.Rows(i)("StockOutDocID")) & "," & AgL.Chk_Text(DtAdj.Rows(i)("StockOutSr")) & "," & AgL.Chk_Text(AgL.PubSiteCode) & ", " & AgL.Chk_Text(AgL.PubDivCode) & "," & AgL.Chk_Text(DtAdj.Rows(i)("AdjQty")) & ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            'LblStatus.Text = "Saving : " + DtAdj.Rows.Count.ToString + " \ " + (i + 1).ToString

            If Not objProgressbar.Visible Then objProgressbar.Show()
            objProgressbar.Text = "Saving : " + DtAdj.Rows.Count.ToString + " \ " + (i + 1).ToString
            objProgressbar.Refresh()

            'Threading.Thread.Sleep(100)
        Next

        AgL.ETrans.Commit()
        objProgressbar.Dispose()

        mQry = "Select Count(*) from StockAdj Where StockInDocId = StockOutDocID"
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
            AgL.Dman_ExecuteNonQry("Delete from stockadj Where StockInDocId = StockOutDocID", AgL.GCn)
            MsgBox("Adjustment is not completed successfully. Please do adjustment again.")
            Me.Dispose()
        End If
    End Sub

End Class
