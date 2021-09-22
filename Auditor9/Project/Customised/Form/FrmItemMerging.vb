Imports Customised.ClsMain

Public Class FrmItemMerging
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public Const ColSNo As String = "S.No."
    Public Const Col1MergingItem As String = "Merging Item"
    Public Const Col1MainItem As String = "Main Item"

    Dim mQry As String = ""
    Dim bMasterName As String = "Item Master"
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1MergingItem, 395, 0, Col1MergingItem, True, False)
            .AddAgTextColumn(Dgl1, Col1MainItem, 395, 0, Col1MainItem, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)

        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            MnuRollBackMerge.Visible = False
        End If
        Ini_Grid()
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
    Private Sub FSave()
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim DtReferenceTables As DataTable
        Dim DtReferenceTablePrimaryKey As DataTable
        Dim DtLogData As DataTable
        Dim mSearchKey As String = ""
        Dim bCode As String = ""
        Dim bSr As Integer = 0
        Dim mTrans As String = ""

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1MergingItem, I).Value <> "" And Dgl1.Item(Col1MainItem, I).Value <> "" Then
                'If Dgl1.Item(Col1MergingItem, I).Tag = Dgl1.Item(Col1MainItem, I).Tag Then
                '    MsgBox("Main Item and Merging Items are same in line " & Dgl1.Item(ColSNo, I).Value + "...!", MsgBoxStyle.Information)
                '    Exit Sub
                'End If

                For J = 0 To Dgl1.Rows.Count - 1
                    If I <> J Then
                        If Dgl1.Item(Col1MergingItem, I).Value = Dgl1.Item(Col1MergingItem, J).Value Then
                            MsgBox("Duplicate Merging Items found in line " & Dgl1.Item(ColSNo, I).Value + " And " & Dgl1.Item(ColSNo, J).Value + "...!", MsgBoxStyle.Information)
                            Exit Sub
                        End If
                    End If

                    If Dgl1.Item(Col1MergingItem, I).Value = Dgl1.Item(Col1MainItem, J).Value Then
                        MsgBox("Merging Item at line no " & Dgl1.Item(ColSNo, I).Value + " And Main Item at line no " & Dgl1.Item(ColSNo, J).Value + " are same...!", MsgBoxStyle.Information)
                        Exit Sub
                    End If
                Next
            End If
        Next

        Try
            If MsgBox("Are you sure to want to continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then


                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                If AgL.PubServerName = "" Then
                    DtReferenceTables = New DataTable
                    DtReferenceTables.Columns.Add("TableName")
                    DtReferenceTables.Columns.Add("FieldName")
                    DtReferenceTables.Columns.Add("PrimaryKey")

                    mQry = "SELECT name As TableName FROM sqlite_master WHERE type='table'
                            And name NOT IN ('UnitConversion','RateListDetail')"
                    Dim DtTableList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    Dim DtForeignKeyList As DataTable
                    For I = 0 To DtTableList.Rows.Count - 1
                        mQry = "PRAGMA foreign_key_list('" & DtTableList.Rows(I)("TableName") & "') "
                        DtForeignKeyList = AgL.FillData(mQry, AgL.GCn).Tables(0)


                        For J = 0 To DtForeignKeyList.Rows.Count - 1
                            If AgL.StrCmp(AgL.XNull(DtForeignKeyList.Rows(J)("Table")), "Item") Then
                                DtReferenceTables.Rows.Add()
                                DtReferenceTables.Rows(DtReferenceTables.Rows.Count - 1)("TableName") = AgL.XNull(DtTableList.Rows(I)("TableName"))
                                DtReferenceTables.Rows(DtReferenceTables.Rows.Count - 1)("FieldName") = AgL.XNull(DtForeignKeyList.Rows(J)("From"))
                            End If
                        Next
                    Next
                Else
                    mQry = "SELECT SO2.Name AS TableName, SC2.Name AS FieldName, '' As PrimaryKey
                            From SysObjects SO With (NoLock)
                            Left Join SysColumns SC With (NoLock) On SO.Id=SC.ID
                            Left Join SysForeignkeys SysFK With (NoLock) On SysFK.RKeyId=SO.ID And SysFK.RKey=SC.ColId
                            Left Join SysColumns SC2 With (NoLock) On SC2.Id=SysFK.FKeyId And SC2.ColId=SysFK.FKey
                            Left Join SysObjects SO2 With (NoLock) On SO2.Id=SC2.Id
                            Where SO.Name='Item' And SC.Name='Code'
                            AND SO2.name NOT IN ('UnitConversion','RateListDetail')
                            AND SC2.typestat <> 3"
                    DtReferenceTables = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
                End If






                For I = 0 To DtReferenceTables.Rows.Count - 1
                    If AgL.PubServerName = "" Then
                        mQry = "PRAGMA table_info('" & AgL.XNull(DtReferenceTables.Rows(I)("TableName")) & "') "
                        DtReferenceTablePrimaryKey = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                        mSearchKey = ""
                        For J = 0 To DtReferenceTablePrimaryKey.Rows.Count - 1
                            If AgL.VNull(DtReferenceTablePrimaryKey.Rows(J)("pk")) <> 0 Then
                                If mSearchKey <> "" Then mSearchKey = mSearchKey + "+"
                                If AgL.XNull(DtReferenceTablePrimaryKey.Rows(J)("Type")) = "Int" Then
                                    mSearchKey = mSearchKey + "CAST(" + AgL.XNull(DtReferenceTablePrimaryKey.Rows(J)("Name")) + " AS NVARCHAR)"
                                Else
                                    mSearchKey = mSearchKey + AgL.XNull(DtReferenceTablePrimaryKey.Rows(J)("Name"))
                                End If
                            End If
                        Next
                    Else
                        mQry = " SELECT KU.table_name as TableName,KU.column_name  AS PrimaryKey, C.DATA_TYPE AS Data_Type
                                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC With (NoLock) 
                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU With (NoLock) ON TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME 
                                LEFT JOIN INFORMATION_SCHEMA.Columns C With (NoLock) ON Ku.TABLE_NAME = C.TABLE_NAME AND KU.COLUMN_NAME = C.COLUMN_NAME
                                WHERE TC.CONSTRAINT_TYPE = 'PRIMARY KEY'
                                AND KU.TABLE_NAME = '" & AgL.XNull(DtReferenceTables.Rows(I)("TableName")) & "'"
                        DtReferenceTablePrimaryKey = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                        mSearchKey = ""
                        For J = 0 To DtReferenceTablePrimaryKey.Rows.Count - 1
                            If mSearchKey <> "" Then mSearchKey = mSearchKey + "+"
                            If AgL.XNull(DtReferenceTablePrimaryKey.Rows(J)("Data_Type")) = "Int" Then
                                mSearchKey = mSearchKey + "CAST(" + AgL.XNull(DtReferenceTablePrimaryKey.Rows(J)("PrimaryKey")) + " AS NVARCHAR)"
                            Else
                                mSearchKey = mSearchKey + AgL.XNull(DtReferenceTablePrimaryKey.Rows(J)("PrimaryKey"))
                            End If
                        Next
                    End If
                    DtReferenceTables.Rows(I)("PrimaryKey") = mSearchKey
                Next



                For I = 0 To Dgl1.Rows.Count - 1
                    If Dgl1.Item(Col1MergingItem, I).Value <> "" And Dgl1.Item(Col1MainItem, I).Value <> "" Then
                        bCode = AgL.GetMaxId("MasterMergingLog", "Code", AgL.GcnRead, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

                        mQry = " INSERT INTO MasterMergingLog (Code, MasterName, OldValue, NewValue, EntryBy, EntryDate)
                                VALUES ('" & bCode & "', '" & bMasterName & "', '" & Dgl1.Item(Col1MergingItem, I).Tag & "', 
                                '" & Dgl1.Item(Col1MainItem, I).Tag & "', '" & AgL.PubUserName & "', 
                                '" & DateTime.Now & "')"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                        bSr = 0
                        For J = 0 To DtReferenceTables.Rows.Count - 1
                            If AgL.XNull(DtReferenceTables.Rows(J)("PrimaryKey")) = "" Then
                                Err.Raise(1,, AgL.XNull(DtReferenceTables.Rows(J)("TableName")) + " Primary Key is not set.")
                            End If


                            mQry = "Select " & DtReferenceTables.Rows(J)("PrimaryKey") & " As SearchCode
                            From " & AgL.XNull(DtReferenceTables.Rows(J)("TableName")) & " With (NoLock)
                            Where " & DtReferenceTables.Rows(J)("FieldName") & " = " & AgL.Chk_Text(Dgl1.Item(Col1MergingItem, I).Tag) & " "
                            DtLogData = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                            For K = 0 To DtLogData.Rows.Count - 1
                                bSr = bSr + 1
                                mQry = " INSERT INTO MasterMergingLogDetail (Code, Sr, TableName, FieldName, SearchKey, SearchCode)
                                VALUES ('" & bCode & "', " & bSr & ", 
                                " & AgL.Chk_Text(AgL.XNull(DtReferenceTables.Rows(J)("TableName"))) & ", 
                                " & AgL.Chk_Text(AgL.XNull(DtReferenceTables.Rows(J)("FieldName"))) & ", 
                                " & AgL.Chk_Text(AgL.XNull(DtReferenceTables.Rows(J)("PrimaryKey"))) & ", 
                                " & AgL.Chk_Text(AgL.XNull(DtLogData.Rows(K)("SearchCode"))) & ")"
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            Next

                            If DtLogData.Rows.Count > 0 Then
                                mQry = "UPDATE " & AgL.XNull(DtReferenceTables.Rows(J)("TableName")) & " 
                                Set " & DtReferenceTables.Rows(J)("FieldName") & " = " & AgL.Chk_Text(Dgl1.Item(Col1MainItem, I).Tag) & "
                                Where " & DtReferenceTables.Rows(J)("FieldName") & " = " & AgL.Chk_Text(Dgl1.Item(Col1MergingItem, I).Tag) & ""
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next

                        mQry = " UPDATE Item Set Status = '" & AgTemplate.ClsMain.EntryStatus.Inactive & "'
                            Where Code = " & AgL.Chk_Text(Dgl1.Item(Col1MergingItem, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next

                AgL.ETrans.Commit()
                mTrans = "Commit"

                MsgBox("Process Complete...!", MsgBoxStyle.Information)
                Dgl1.Rows.Clear() : Dgl1.RowCount = 1
            End If
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1MainItem
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1MainItem) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description, I.ManualCode as ItemCode, Ig.Description As ItemGroup, Ic.Description As ItemCategory " &
                                  " FROM Item I  With (NoLock)  " &
                                  " LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.code " &
                                  " Left JOIN ItemCategory Ic On I.ItemCategory = Ic.Code " &
                                  " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            Dgl1.AgHelpDataSet(Col1MainItem) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1MergingItem
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1MergingItem) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description, I.ManualCode as ItemCode, Ig.Description As ItemGroup, Ic.Description As ItemCategory " &
                                  " FROM Item I  With (NoLock) " &
                                  " LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.code " &
                                  " Left JOIN ItemCategory Ic On I.ItemCategory = Ic.Code " &
                                  " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            Dgl1.AgHelpDataSet(Col1MergingItem) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnMerge_Click(sender As Object, e As EventArgs) Handles BtnMerge.Click
        FSave()
    End Sub
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.selected = True
        End If
    End Sub
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuRollBackMerge.Click
        Select Case sender.name
            Case MnuRollBackMerge.Name
                Dim FrmObj As New FrmItemMergingRollBack()
                FrmObj.MdiParent = Me.MdiParent
                FrmObj.Show()
        End Select
    End Sub
End Class