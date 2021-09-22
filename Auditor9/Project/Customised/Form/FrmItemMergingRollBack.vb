Imports Customised.ClsMain

Public Class FrmItemMergingRollBack
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public Const ColSNo As String = "S.No."
    Public Const Col1Code As String = "Code"
    Public Const Col1MergingItem As String = "Merging Item"
    Public Const Col1MainItem As String = "Main Item"
    Public Const Col1MergeBy As String = "Merge By"
    Public Const Col1MergeDate As String = "Merge Date"
    Public Const Col1RollBackBy As String = "Roll Back By"
    Public Const Col1RollBackDate As String = "Roll Back Date"
    Public Const Col1BtnRollBack As String = "Roll Back"

    Dim mQry As String = ""
    Dim bMasterName As String = "Item Master"
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Code, 90, 0, Col1Code, False, True)
            .AddAgTextColumn(Dgl1, Col1MergingItem, 170, 0, Col1MergingItem, True, True)
            .AddAgTextColumn(Dgl1, Col1MainItem, 170, 0, Col1MainItem, True, True)
            .AddAgTextColumn(Dgl1, Col1MergeBy, 90, 0, Col1MergeBy, True, True)
            .AddAgTextColumn(Dgl1, Col1MergeDate, 180, 0, Col1MergeDate, True, True)
            .AddAgTextColumn(Dgl1, Col1RollBackBy, 90, 0, Col1RollBackBy, True, True)
            .AddAgTextColumn(Dgl1, Col1RollBackDate, 180, 0, Col1RollBackDate, True, True)
            .AddAgButtonColumn(Dgl1, Col1BtnRollBack, 30, " ", True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)

        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
    End Sub

    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        MovRec()
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
    Private Sub FSave(bRowIndex As Integer)
        Dim I As Integer = 0
        Dim mTrans As String = ""
        Dim DtTemp As DataTable

        mQry = " Select 'UPDATE ' + L.TableName + ' Set ' + L.FieldName + ' = ' + '''' + H.OldValue + '''' + ' Where ' + L.SearchKey + ' = ' + '''' + L.SearchCode + '''' AS Qry
                FROM MasterMergingLog H 
                LEFT JOIN MasterMergingLogDetail L On H.Code = L.Code  
                WHERE H.Code In ('" & Dgl1.Item(Col1Code, bRowIndex).Value & "') "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        Try
            If MsgBox("Are you sure To want To Continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                For I = 0 To DtTemp.Rows.Count - 1
                    mQry = AgL.XNull(DtTemp.Rows(I)("Qry"))
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Next

                mQry = "UPDATE MasterMergingLog Set RollBackBy = '" & AgL.PubUserName & "', 
                        RollBackDate = '" & DateTime.Now & "'
                        Where Code = '" & Dgl1.Item(Col1Code, bRowIndex).Value & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " UPDATE Item Set Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "'
                        Where Code = '" & Dgl1.Item(Col1MergingItem, bRowIndex).Tag & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                AgL.ETrans.Commit()
                mTrans = "Commit"

                Dgl1.Item(Col1RollBackBy, bRowIndex).Value = AgL.PubUserName
                Dgl1.Item(Col1RollBackDate, bRowIndex).Value = DateTime.Now
                Dgl1.Item(Col1BtnRollBack, bRowIndex) = New DataGridViewTextBoxCell
                Dgl1.Item(Col1BtnRollBack, bRowIndex).ReadOnly = True

                MsgBox("Process Complete...!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Public Sub MovRec()
        Dim DtTemp As DataTable
        Dim I As Integer = 0

        mQry = "SELECT H.Code, H.OldValue AS MergingItem, OI.Description AS MergingItemDesc, 
                H.NewValue AS MainItem, NI.Description AS MainItemDesc,
                H.EntryBy, H.EntryDate, H.RollBackBy, H.RollBackDate
                FROM MasterMergingLog H
                LEFT JOIN Item OI ON H.OldValue = OI.Code
                LEFT JOIN Item NI ON H.NewValue = NI.Code 
                Order By H.EntryDate Desc "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I = 0 To DtTemp.Rows.Count - 1
            Dgl1.Rows.Add()
            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
            Dgl1.Item(Col1Code, I).Value = AgL.XNull(DtTemp.Rows(I)("Code"))
            Dgl1.Item(Col1MergingItem, I).Tag = AgL.XNull(DtTemp.Rows(I)("MergingItem"))
            Dgl1.Item(Col1MergingItem, I).Value = AgL.XNull(DtTemp.Rows(I)("MergingItemDesc"))
            Dgl1.Item(Col1MainItem, I).Tag = AgL.XNull(DtTemp.Rows(I)("MainItem"))
            Dgl1.Item(Col1MainItem, I).Value = AgL.XNull(DtTemp.Rows(I)("MainItemDesc"))
            Dgl1.Item(Col1MergeBy, I).Value = AgL.XNull(DtTemp.Rows(I)("EntryBy"))
            Dgl1.Item(Col1MergeDate, I).Value = AgL.XNull(DtTemp.Rows(I)("EntryDate"))
            Dgl1.Item(Col1RollBackBy, I).Value = AgL.XNull(DtTemp.Rows(I)("RollBackBy"))
            Dgl1.Item(Col1RollBackDate, I).Value = AgL.XNull(DtTemp.Rows(I)("RollBackDate"))

            If AgL.XNull(DtTemp.Rows(I)("RollBackBy")) <> "" Then
                Dgl1.Item(Col1BtnRollBack, I) = New DataGridViewTextBoxCell
                Dgl1.Item(Col1BtnRollBack, I).ReadOnly = True
            End If
        Next
        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
    End Sub

    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim mRowIndex As Integer = 0, mColumnIndex As Integer = 0
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1BtnRollBack
                    If Dgl1.Item(Col1RollBackBy, mRowIndex).Value = "" Then
                        FSave(mRowIndex)
                    End If
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class