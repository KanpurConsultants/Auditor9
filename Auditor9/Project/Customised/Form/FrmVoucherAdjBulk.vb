Imports System.ComponentModel

Public Class FrmVoucherAdjBulk
    Public WithEvents DglInv As New AgControls.AgDataGrid
    Public WithEvents DglPmt As New AgControls.AgDataGrid
    Public WithEvents DglAdj As New AgControls.AgDataGrid

    Protected Const Col_Sno As String = "Sr"

    Protected Const ColInv_DocID As String = "DocID"
    Protected Const ColInv_Sr As String = "V_SNo"
    Protected Const ColInv_DocNo As String = "DocNo"
    Protected Const ColInv_DocDate As String = "DocDate"
    Protected Const ColInv_Party As String = "Party"
    Protected Const ColInv_Narration As String = "Narration"
    Protected Const ColInv_Amt As String = "Amount"
    Protected Const ColInv_AdjAmt As String = "AdjAmt"
    Protected Const ColInv_BalAmt As String = "BalAmt"
    Protected Const ColInv_Div_Code As String = "DivCode"
    Protected Const ColInv_Site_Code As String = "Site_Code"
    Protected Const ColInv_AdjType As String = "AdjType"

    Protected Const ColPmt_DocID As String = "DocID"
    Protected Const ColPmt_Sr As String = "V_SNo"
    Protected Const ColPmt_DocNo As String = "DocNo"
    Protected Const ColPmt_DocDate As String = "DocDate"
    Protected Const ColPmt_Party As String = "Party"
    Protected Const ColPmt_Narration As String = "Narration"
    Protected Const ColPmt_Amt As String = "Amount"
    Protected Const ColPmt_AdjAmt As String = "AdjAmt"
    Protected Const ColPmt_BalAmt As String = "BalAmt"
    Protected Const ColPmt_AdjBtn As String = "Adj"
    Protected Const ColPmt_Div_Code As String = "DivCode"
    Protected Const ColPmt_Site_Code As String = "Site_Code"
    Protected Const ColPmt_AdjType As String = "AdjType"

    Protected Const ColSNo As String = "Sr."
    Protected Const ColAdj_InvDocID As String = "ADJ_DocID"
    Protected Const ColAdj_InvSr As String = "ADJ_V_Sno"
    Protected Const ColAdj_InvNo As String = "Inv.No"
    Protected Const ColAdj_InvDate As String = "Inv.Date"
    Protected Const ColAdj_AdjDocId As String = "VR_DocID"
    Protected Const ColAdj_AdjSr As String = "VR_V_Sno"
    Protected Const ColAdj_AdjNo As String = "Adj.No"
    Protected Const ColAdj_AdjDate As String = "Adj.Date"
    Protected Const ColAdj_Amt As String = "Amount"
    Protected Const ColAdj_Div_Code As String = "Div_Code"
    Protected Const ColAdj_Site_Code As String = "Site_Code"
    Protected Const ColAdj_AdjType As String = "Adj.Type"

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
        Try

            DglInv.EnableHeadersVisualStyles = False
            DglInv.AgSkipReadOnlyColumns = True
            DglInv.ColumnHeadersHeight = 35
            'DglSI.ReadOnly = True
            DglInv.AllowUserToAddRows = False
            'DglSI.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            DglInv.ReadOnly = True
            AgL.GridDesign(DglInv)

            DglInv.Columns(ColInv_DocID).Visible = False
            DglInv.Columns(ColInv_Sr).Visible = False
            DglInv.Columns(ColInv_DocNo).HeaderText = "Inv.No"
            DglInv.Columns(ColInv_DocNo).Width = 100
            DglInv.Columns(ColInv_Party).Width = 300
            DglInv.Columns(ColInv_Narration).Width = 200
            DglInv.Columns(ColInv_Amt).Width = 100
            DglInv.Columns(ColInv_AdjAmt).Width = 100
            DglInv.Columns(ColInv_BalAmt).Width = 100
            DglInv.Columns(ColInv_Div_Code).Visible = False
            DglInv.Columns(ColInv_Site_Code).Visible = False
            DglInv.Columns(ColInv_AdjType).Visible = False


            DglPmt.EnableHeadersVisualStyles = False
            DglPmt.AgSkipReadOnlyColumns = True
            DglPmt.ColumnHeadersHeight = 35
            'DglPI.ReadOnly = True
            DglPmt.AllowUserToAddRows = False
            'DglPI.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            DglPmt.ReadOnly = True
            AgL.GridDesign(DglPmt)

            DglPmt.Columns(ColPmt_DocID).Visible = False
            DglPmt.Columns(ColPmt_Sr).Visible = False
            DglPmt.Columns(ColPmt_DocNo).Width = 100
            DglInv.Columns(ColInv_DocNo).HeaderText = "Adj.No"
            DglPmt.Columns(ColPmt_Party).Width = 100
            DglPmt.Columns(ColPmt_Narration).Width = 150
            DglPmt.Columns(ColPmt_Amt).Width = 100
            DglPmt.Columns(ColPmt_AdjAmt).Width = 100
            DglPmt.Columns(ColPmt_BalAmt).Width = 80
            DglPmt.Columns(ColPmt_Div_Code).Visible = False
            DglPmt.Columns(ColPmt_Site_Code).Visible = False
            DglPmt.Columns(ColPmt_AdjType).Visible = False

            If DglPmt.Columns(ColPmt_AdjBtn) Is Nothing Then
                AgCL.AddAgButtonColumn(DglPmt, ColPmt_AdjBtn, 40, ColPmt_AdjBtn, True, False)
                DglPmt.Columns(ColPmt_AdjBtn).DisplayIndex = 0
            End If





            DglAdj.ColumnCount = 0
            With AgCL
                .AddAgTextColumn(DglAdj, ColSNo, 40, 5, ColSNo, True, True, False)
                .AddAgTextColumn(DglAdj, ColAdj_InvDocID, 100, 0, ColAdj_InvDocID, False, True)
                .AddAgTextColumn(DglAdj, ColAdj_InvSr, 100, 0, ColAdj_InvSr, False, True)
                .AddAgTextColumn(DglAdj, ColAdj_InvNo, 100, 0, ColAdj_InvNo, True, True)
                .AddAgTextColumn(DglAdj, ColAdj_InvDate, 100, 0, ColAdj_InvDate, False, True)
                .AddAgTextColumn(DglAdj, ColAdj_AdjDocId, 100, 0, ColAdj_AdjDocId, False, True)
                .AddAgTextColumn(DglAdj, ColAdj_AdjSr, 100, 0, ColAdj_AdjSr, False, True)
                .AddAgTextColumn(DglAdj, ColAdj_AdjNo, 100, 0, ColAdj_AdjNo, True, True)
                .AddAgTextColumn(DglAdj, ColAdj_AdjDate, 100, 0, ColAdj_AdjDate, False, True)
                .AddAgNumberColumn(DglAdj, ColAdj_Amt, 100, 8, 2, False, ColAdj_Amt, True, True, True)
                .AddAgTextColumn(DglAdj, ColAdj_Div_Code, 100, 0, ColAdj_Div_Code, False, True)
                .AddAgTextColumn(DglAdj, ColAdj_Site_Code, 100, 0, ColAdj_Site_Code, False, True)
                .AddAgTextColumn(DglAdj, ColAdj_AdjType, 100, 0, ColAdj_AdjType, False, True)
            End With
            AgL.AddAgDataGrid(DglAdj, Panel3)
            DglAdj.EnableHeadersVisualStyles = False
            DglAdj.ColumnHeadersHeight = 40
            DglAdj.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
            AgL.GridDesign(DglAdj)



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub FrmSaleInvoiceAdj_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AgL.WinSetting(Me, 660, 992, 0, 0)

        AgL.GridDesign(DglInv)
        AgL.GridDesign(DglPmt)
        AgL.GridDesign(DglAdj)
        AgL.AddAgDataGrid(DglInv, Panel1)
        AgL.AddAgDataGrid(DglPmt, Panel2)
        AgL.AddAgDataGrid(DglAdj, Panel3)

        TxtAcNature.Text = "Customer"

        MoveRec()
        Ini_Grid()
    End Sub

    Private Sub MoveRec()
        Try

            Dim mQry As String
            Dim mQryDebit As String
            Dim mQryCredit As String

            If TxtAcNature.Text = "" Then
                MsgBox("Please select any nature.")
                TxtAcNature.Focus()
                Exit Sub
            End If



            mQryDebit = "                
                SELECT L.DocId, L.V_SNo, L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId AS [DocNo], 
                Sg.Name AS Party, L.Narration , Abs(L.AmtDr-L.AmtCr) as Amount, IfNull(Adj.AdjAmt,0.0) as AdjAmt, 0.0 as BalAmt, 
                L.DivCode, L.Site_Code, (Case When L.AmtDr > 0 Then 'Dr' Else 'Cr' End) as AdjType
                FROM Ledger L                 
                Left JOIN viewHelpSubGroup Sg On L.Subcode = Sg.Code  
                Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                           abs(Sum(Amount)) as AdjAmt 
                           From LedgerAdj LA
                           Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                           Group By Adj_DocID, Adj_V_Sno
                           Union All 
                           Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                           abs(Sum(Amount)) as AdjAmt 
                           From LedgerAdj LA
                           Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                           Group By Vr_DocID, Vr_V_Sno                    
                          ) as Adj On L.DocID = Adj.DocID And L.V_Sno = Adj.V_Sno                
                WHERE  Abs(L.AmtDr-L.AmtCr)-IfNull(Adj.AdjAmt,0) >0 
                And Sg.Nature='" & TxtAcNature.Text & "'
                And L.DivCode = '" & AgL.PubDivCode & "' And L.Site_Code = '" & AgL.PubSiteCode & "' 
                And L.AmtDr >0 
                Order By Sg.name, L.V_Date, L.DocID 
              "


            mQryCredit = "                
                SELECT L.DocId, L.V_SNo, L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId AS [DocNo], 
                Sg.Name AS Party, L.Narration , Abs(L.AmtDr-L.AmtCr) as Amount, IfNull(Adj.AdjAmt,0.0) as AdjAmt, 0.0 as BalAmt, 
                L.DivCode, L.Site_Code, (Case When L.AmtDr > 0 Then 'Dr' Else 'Cr' End) as AdjType
                FROM Ledger L                 
                Left JOIN viewHelpSubGroup Sg On L.Subcode = Sg.Code  
                Left Join (
                           Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                           abs(Sum(Amount)) as AdjAmt 
                           From LedgerAdj LA
                           Left Join Ledger L1 On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                           Group By Adj_DocID, Adj_V_Sno
                           Union All 
                           Select Vr_DocID as DocID, Vr_V_Sno as V_Sno, 
                           abs(Sum(Amount)) as AdjAmt 
                           From LedgerAdj LA
                           Left Join Ledger L1 On L1.DocId = LA.Adj_DocID And L1.V_SNo = LA.Adj_V_Sno
                           Group By Vr_DocID, Vr_V_Sno
                          ) as Adj On L.DocID = Adj.DocID And L.V_Sno = Adj.V_Sno                
                WHERE  Abs(L.AmtDr-L.AmtCr)-IfNull(Adj.AdjAmt,0) >0 
                And Sg.Nature='" & TxtAcNature.Text & "'
                And L.DivCode = '" & AgL.PubDivCode & "' And L.Site_Code = '" & AgL.PubSiteCode & "' 
                And L.AmtCr >0 
                Order By Sg.name, L.V_Date, L.DocID 
              "


            If TxtAcNature.Text = "Customer" Then
                DtSO = AgL.FillData(mQryDebit, AgL.GCn).Tables(0)
                DtSO.Columns("BalAmt").Expression = "(Amount - [AdjAmt])*1.0"
                DglInv.DataSource = DtSO


                DtSI = AgL.FillData(mQryCredit, AgL.GCn).Tables(0)
                DtSI.Columns("BalAmt").Expression = "(Amount - [AdjAmt])*1.0"
                DglPmt.DataSource = DtSI
            Else
                DtSO = AgL.FillData(mQryCredit, AgL.GCn).Tables(0)
                DtSO.Columns("BalAmt").Expression = "(Amount - [AdjAmt])*1.0"
                DglInv.DataSource = DtSO


                DtSI = AgL.FillData(mQryDebit, AgL.GCn).Tables(0)
                DtSI.Columns("BalAmt").Expression = "(Amount - [AdjAmt])*1.0"
                DglPmt.DataSource = DtSI
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DglPI_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglPmt.CellContentClick
        Dim DrDtAdj As DataRow = Nothing
        Dim mRow As Integer
        Select Case DglPmt.Columns(DglPmt.CurrentCell.ColumnIndex).Name
            Case ColPmt_AdjBtn
                If Val(DglInv.Item(ColInv_BalAmt, DglInv.CurrentCell.RowIndex).Value) <= 0 Then Exit Sub
                If DglInv.SelectedRows IsNot Nothing Then
                    If DglInv.Item(ColInv_Party, DglInv.CurrentCell.RowIndex).Value = DglPmt.Item(ColPmt_Party, DglPmt.CurrentCell.RowIndex).Value Then
                        If Val(DglInv.Item(ColInv_BalAmt, DglInv.CurrentCell.RowIndex).Value) <= Val(DglPmt.Item(ColPmt_BalAmt, DglPmt.CurrentCell.RowIndex).Value) Then
                            'DrDtAdj = DtAdj.NewRow
                            mRow = DglAdj.Rows.Count - 1
                            DglAdj.Rows.Add()
                            DglAdj.Item(ColAdj_InvDocID, mRow).Value = DglInv.Item(ColInv_DocID, DglInv.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_InvSr, mRow).Value = DglInv.Item(ColInv_Sr, DglInv.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_InvNo, mRow).Value = DglInv.Item(ColInv_DocNo, DglInv.CurrentCell.RowIndex).Value
                            'DglAdj.Item(ColAdj_InvDate, mRow).Value = DglSO.Item(ColSO_DocDate, DglSO.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_AdjDocId, mRow).Value = DglPmt.Item(ColPmt_DocID, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_AdjSr, mRow).Value = DglPmt.Item(ColPmt_Sr, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_AdjNo, mRow).Value = DglPmt.Item(ColPmt_DocNo, DglPmt.CurrentCell.RowIndex).Value
                            'DglAdj.Item(ColAdj_AdjDate, mRow).Value = DglSI.Item(ColSI_DocDate, DglSI.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_Div_Code, mRow).Value = DglPmt.Item(ColPmt_Div_Code, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_Site_Code, mRow).Value = DglPmt.Item(ColPmt_Site_Code, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_AdjType, mRow).Value = DglPmt.Item(ColPmt_AdjType, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_Amt, mRow).Value = DglInv.Item(ColInv_BalAmt, DglInv.CurrentCell.RowIndex).Value

                            'DtAdj.Rows.Add(DrDtAdj)

                            DglInv.Item(ColInv_AdjAmt, DglInv.CurrentCell.RowIndex).Value = Format(Val(DglInv.Item(ColInv_AdjAmt, DglInv.CurrentCell.RowIndex).Value) + Val(DglInv.Item(ColInv_BalAmt, DglInv.CurrentCell.RowIndex).Value), "0.00")
                            DglPmt.Item(ColPmt_AdjAmt, DglPmt.CurrentCell.RowIndex).Value = Format(Val(DglPmt.Item(ColPmt_AdjAmt, DglPmt.CurrentCell.RowIndex).Value) + Val(DglInv.Item(ColInv_BalAmt, DglInv.CurrentCell.RowIndex).Value), "0.00")

                            DtSO.AcceptChanges()
                            DtSI.AcceptChanges()
                        ElseIf Val(DglInv.Item(ColInv_BalAmt, DglInv.CurrentCell.RowIndex).Value) > Val(DglPmt.Item(ColPmt_BalAmt, DglPmt.CurrentCell.RowIndex).Value) Then
                            'DrDtAdj = DtAdj.NewRow
                            mRow = DglAdj.Rows.Count - 1
                            DglAdj.Rows.Add()
                            DglAdj.Item(ColAdj_InvDocID, mRow).Value = DglInv.Item(ColInv_DocID, DglInv.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_InvSr, mRow).Value = DglInv.Item(ColInv_Sr, DglInv.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_InvNo, mRow).Value = DglInv.Item(ColInv_DocNo, DglInv.CurrentCell.RowIndex).Value
                            'DglAdj.Item(ColAdj_InvDate, mRow).Value = DglSO.Item(ColSO_DocDate, DglSO.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_AdjDocId, mRow).Value = DglPmt.Item(ColPmt_DocID, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_AdjSr, mRow).Value = DglPmt.Item(ColPmt_Sr, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_AdjNo, mRow).Value = DglPmt.Item(ColPmt_DocNo, DglPmt.CurrentCell.RowIndex).Value
                            'DglAdj.Item(ColAdj_AdjDate, mRow).Value = DglSI.Item(ColSI_DocDate, DglSI.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_Div_Code, mRow).Value = DglPmt.Item(ColPmt_Div_Code, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_Site_Code, mRow).Value = DglPmt.Item(ColPmt_Site_Code, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_AdjType, mRow).Value = DglPmt.Item(ColPmt_AdjType, DglPmt.CurrentCell.RowIndex).Value
                            DglAdj.Item(ColAdj_Amt, mRow).Value = DglPmt.Item(ColPmt_BalAmt, DglPmt.CurrentCell.RowIndex).Value

                            'DtAdj.Rows.Add(DrDtAdj)

                            DglInv.Item(ColInv_AdjAmt, DglInv.CurrentCell.RowIndex).Value = Val(DglInv.Item(ColInv_AdjAmt, DglInv.CurrentCell.RowIndex).Value) + Val(DglPmt.Item(ColPmt_BalAmt, DglPmt.CurrentCell.RowIndex).Value)
                            DglPmt.Item(ColPmt_AdjAmt, DglPmt.CurrentCell.RowIndex).Value = Val(DglPmt.Item(ColPmt_AdjAmt, DglPmt.CurrentCell.RowIndex).Value) + Val(DglPmt.Item(ColPmt_BalAmt, DglPmt.CurrentCell.RowIndex).Value)

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

    Private Sub DglSO_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglInv.CellEnter
        If DtSI Is Nothing Then Exit Sub
        If DglInv.CurrentCell Is Nothing Then Exit Sub


        DtSI.DefaultView.RowFilter = Nothing
        DtSI.DefaultView.RowFilter = " Party = '" & DglInv.Item(ColInv_Party, DglInv.CurrentCell.RowIndex).Value & "' and [BalAmt]>0 "
    End Sub

    Sub Fill()
        Try

            Dim intSoRowIndex As Integer
            Dim intSiRowIndex As Integer
            Dim dblAdjQty As Double
            Dim mRow As Integer

            Dim objProgressbar As New AgLibrary.FrmProgressBar
            objProgressbar.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog



            For intSoRowIndex = 0 To DglInv.Rows.Count - 1
                If Val(DglInv.Item(ColInv_BalAmt, intSoRowIndex).Value) > 0 Then
                    DtSI.DefaultView.RowFilter = Nothing
                    DtSI.DefaultView.RowFilter = " Party = '" & DglInv.Item(ColInv_Party, intSoRowIndex).Value & "' and [BalAmt]>0 "

                    For intSiRowIndex = 0 To DtSI.DefaultView.Count - 1
                        dblAdjQty = 0
                        DtSI.DefaultView.RowFilter = Nothing
                        DtSI.DefaultView.RowFilter = " Party = '" & DglInv.Item(ColInv_Party, intSoRowIndex).Value & "' and [BalAmt]>0 "

                        If Val(DglInv.Item(ColInv_BalAmt, intSoRowIndex).Value) <= 0 Then Continue For
                        If DglInv.Item(ColInv_Party, intSoRowIndex).Value = DglPmt.Item(ColPmt_Party, 0).Value Then
                            If Val(DglInv.Item(ColInv_BalAmt, intSoRowIndex).Value) <= Val(DglPmt.Item(ColPmt_BalAmt, 0).Value) Then
                                dblAdjQty = Val(DglInv.Item(ColInv_BalAmt, intSoRowIndex).Value)
                                'DrDtAdj = DtAdj.NewRow
                                mRow = DglAdj.Rows.Count - 1
                                DglAdj.Rows.Add()
                                DglAdj.Item(ColSNo, mRow).Value = DglAdj.Rows.Count
                                DglAdj.Item(ColAdj_InvDocID, mRow).Value = DglInv.Item(ColInv_DocID, intSoRowIndex).Value
                                DglAdj.Item(ColAdj_InvSr, mRow).Value = DglInv.Item(ColInv_Sr, intSoRowIndex).Value
                                DglAdj.Item(ColAdj_InvNo, mRow).Value = DglInv.Item(ColInv_DocNo, intSoRowIndex).Value
                                'DglAdj.Item(ColAdj_InvDate, mRow).Value = DglSO.Item(ColSO_DocDate, intSoRowIndex).Value
                                DglAdj.Item(ColAdj_AdjDocId, mRow).Value = DglPmt.Item(ColPmt_DocID, 0).Value
                                DglAdj.Item(ColAdj_AdjSr, mRow).Value = DglPmt.Item(ColPmt_Sr, 0).Value
                                DglAdj.Item(ColAdj_AdjNo, mRow).Value = DglPmt.Item(ColPmt_DocNo, 0).Value
                                'DglAdj.Item(ColAdj_AdjDate, mRow).Value = DglSI.Item(ColSI_DocDate, 0).Value
                                DglAdj.Item(ColAdj_Div_Code, mRow).Value = DglPmt.Item(ColPmt_Div_Code, 0).Value
                                DglAdj.Item(ColAdj_Site_Code, mRow).Value = DglPmt.Item(ColPmt_Site_Code, 0).Value
                                DglAdj.Item(ColAdj_AdjType, mRow).Value = DglPmt.Item(ColPmt_AdjType, 0).Value
                                DglAdj.Item(ColAdj_Amt, mRow).Value = dblAdjQty

                                'DtAdj.Rows.Add(DrDtAdj)

                                DglInv.Item(ColInv_AdjAmt, intSoRowIndex).Value = Val(DglInv.Item(ColInv_AdjAmt, intSoRowIndex).Value) + dblAdjQty
                                DglPmt.Item(ColPmt_AdjAmt, 0).Value = Val(DglPmt.Item(ColPmt_AdjAmt, 0).Value) + dblAdjQty

                                DtSO.AcceptChanges()
                                DtSI.AcceptChanges()
                            ElseIf Val(DglInv.Item(ColInv_BalAmt, intSoRowIndex).Value) > Val(DglPmt.Item(ColPmt_BalAmt, 0).Value) Then
                                dblAdjQty = Val(DglPmt.Item(ColPmt_BalAmt, 0).Value)
                                'DrDtAdj = DtAdj.NewRow
                                mRow = DglAdj.Rows.Count - 1
                                DglAdj.Rows.Add()
                                DglAdj.Item(ColSNo, mRow).Value = DglAdj.Rows.Count

                                DglAdj.Item(ColAdj_InvDocID, mRow).Value = DglInv.Item(ColInv_DocID, intSoRowIndex).Value
                                DglAdj.Item(ColAdj_InvSr, mRow).Value = DglInv.Item(ColInv_Sr, intSoRowIndex).Value
                                DglAdj.Item(ColAdj_InvNo, mRow).Value = DglInv.Item(ColInv_DocNo, intSoRowIndex).Value
                                'DglAdj.Item(ColAdj_InvDate, mRow).Value = DglSO.Item(ColSO_DocDate, intSoRowIndex).Value
                                DglAdj.Item(ColAdj_AdjDocId, mRow).Value = DglPmt.Item(ColPmt_DocID, 0).Value
                                DglAdj.Item(ColAdj_AdjSr, mRow).Value = DglPmt.Item(ColPmt_Sr, 0).Value
                                DglAdj.Item(ColAdj_AdjNo, mRow).Value = DglPmt.Item(ColPmt_DocNo, 0).Value
                                'DglAdj.Item(ColAdj_AdjDate, mRow).Value = DglSI.Item(ColSI_DocDate, 0).Value
                                DglAdj.Item(ColAdj_Div_Code, mRow).Value = DglPmt.Item(ColPmt_Div_Code, 0).Value
                                DglAdj.Item(ColAdj_Site_Code, mRow).Value = DglPmt.Item(ColPmt_Site_Code, 0).Value
                                DglAdj.Item(ColAdj_AdjType, mRow).Value = DglPmt.Item(ColPmt_AdjType, 0).Value
                                DglAdj.Item(ColAdj_Amt, mRow).Value = dblAdjQty

                                'DtAdj.Rows.Add(DrDtAdj)

                                DglInv.Item(ColInv_AdjAmt, intSoRowIndex).Value = Val(DglInv.Item(ColInv_AdjAmt, intSoRowIndex).Value) + dblAdjQty
                                DglPmt.Item(ColPmt_AdjAmt, 0).Value = Val(DglPmt.Item(ColPmt_AdjAmt, 0).Value) + dblAdjQty

                                DtSO.AcceptChanges()
                                DtSI.AcceptChanges()
                            End If
                        Else
                            MsgBox("Items of stock out and stock in doesn't match")
                        End If



                    Next


                End If

                objProgressbar.Show()
                objProgressbar.Text = "Adjusting : " + DglInv.Rows.Count.ToString + " \ " + (intSoRowIndex + 1).ToString
                objProgressbar.Refresh()

                'DtAdj.AcceptChanges()
                'Threading.Thread.Sleep(100)
            Next


            objProgressbar.Dispose()

            If DglInv.SelectedRows IsNot Nothing Then
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
        MoveRec()
        Ini_Grid()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Dispose()

    End Sub

    Private Sub UpdateDb()
        Try

        Catch ex As Exception

        End Try
        Dim i As Integer, mQry As String
        Dim objProgressbar As New AgLibrary.FrmProgressBar
        objProgressbar.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog


        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans

        For i = 0 To DglAdj.Rows.Count - 1
            If DglAdj.Item(ColAdj_InvDocID, i).Value <> "" Then
                If TxtAcNature.Text = "Customer" Then
                    mQry = "Insert Into LedgerAdj(Vr_DocID, Vr_V_SNo, Adj_DocID, Adj_V_SNo, Amount, Site_Code, Div_Code, Adj_Type)
                   Values (" & AgL.Chk_Text(DglAdj.Item(ColAdj_AdjDocId, i).Value) & "," & AgL.Chk_Text(DglAdj.Item(ColAdj_AdjSr, i).Value) & ", " & AgL.Chk_Text(DglAdj.Item(ColAdj_InvDocID, i).Value) & ", " & AgL.Chk_Text(DglAdj.Item(ColAdj_InvSr, i).Value) & ", " & -1.0 * Val(DglAdj.Item(ColAdj_Amt, i).Value) & ", " & AgL.Chk_Text(DglAdj.Item(ColAdj_Site_Code, i).Value) & ", " & AgL.Chk_Text(DglAdj.Item(ColAdj_Div_Code, i).Value) & ", 'Adjustment') "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Else
                    mQry = "Insert Into LedgerAdj(Vr_DocID, Vr_V_SNo, Adj_DocID, Adj_V_SNo, Amount, Site_Code, Div_Code, Adj_Type)
                   Values (" & AgL.Chk_Text(DglAdj.Item(ColAdj_AdjDocId, i).Value) & "," & AgL.Chk_Text(DglAdj.Item(ColAdj_AdjSr, i).Value) & ", " & AgL.Chk_Text(DglAdj.Item(ColAdj_InvDocID, i).Value) & ", " & AgL.Chk_Text(DglAdj.Item(ColAdj_InvSr, i).Value) & ", " & 1.0 * Val(DglAdj.Item(ColAdj_Amt, i).Value) & ", " & AgL.Chk_Text(DglAdj.Item(ColAdj_Site_Code, i).Value) & ", " & AgL.Chk_Text(DglAdj.Item(ColAdj_Div_Code, i).Value) & ", 'Adjustment') "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                End If
            End If


            'mQry = "Insert Into LedgerAdj(Vr_DocID, Vr_V_SNo, Adj_DocID, Adj_V_SNo, Amount, Site_Code, Div_Code, Adj_Type)
            '       Values (" & AgL.Chk_Text(DtAdj.Rows(i)("Adj_DocId")) & "," & AgL.Chk_Text(DtAdj.Rows(i)("Adj_Sr")) & ", " & AgL.Chk_Text(DtAdj.Rows(i)("Vr_DocId")) & ", " & AgL.Chk_Text(DtAdj.Rows(i)("Vr_Sr")) & ", " & Val(DtAdj.Rows(i)("AdjAmt")) & ", Null, Null, Null) "
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            'LblStatus.Text = "Saving : " + DtAdj.Rows.Count.ToString + " \ " + (i + 1).ToString

            If Not objProgressbar.Visible Then objProgressbar.Show()
            objProgressbar.Text = "Saving : " + DglAdj.Rows.Count.ToString + " \ " + (i + 1).ToString
            objProgressbar.Refresh()

            'Threading.Thread.Sleep(100)
        Next

        AgL.ETrans.Commit()
        objProgressbar.Dispose()

        'mQry = "Select Count(*) from StockAdj Where StockInDocId = StockOutDocID"
        'If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
        '    AgL.Dman_ExecuteNonQry("Delete from stockadj Where StockInDocId = StockOutDocID", AgL.GCn)
        '    MsgBox("Adjustment is not completed successfully. Please do adjustment again.")
        '    Me.Dispose()
        'End If
    End Sub

    Private Sub TxtAcNature_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtAcNature.KeyDown
        Dim mQry As String
        If e.KeyCode <> Keys.Enter Then
            mQry = "select 'Customer' as Code, 'Customer' as Name Union All Select 'Supplier' as Code, 'Supplier' as Name "
            TxtAcNature.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub TxtAcNature_Validating(sender As Object, e As CancelEventArgs) Handles TxtAcNature.Validating
        MoveRec()
        Ini_Grid()
    End Sub
End Class
