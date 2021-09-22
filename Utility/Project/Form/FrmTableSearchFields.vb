Public Class FrmTableSearchFields
    Dim mQry$
    Private Const Col_SNo As Byte = 0
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Private Const Col1TableName As Byte = 1
    Private Const Col1SearchField As Byte = 2
    Private Const Col1UniqueField As Byte = 3
    Private Const Col1TransactionYn As Byte = 4
    Private Const Col1LineItemYn As Byte = 5

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 300, 770, 0, 0)
            IniGrid()
            FIniMaster()
            Ini_PrimaryFields()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub IniGrid()
        DGL1.Height = Pnl1.Height
        DGL1.Width = Pnl1.Width
        DGL1.Top = Pnl1.Top
        DGL1.Left = Pnl1.Left
        DGL1.ColumnHeadersHeight = 25
        DGL1.RowHeadersVisible = False
        Pnl1.Visible = False
        Controls.Add(DGL1)
        DGL1.Visible = True
        DGL1.BringToFront()
        With AgCL
            .AddAgTextColumn(DGL1, "DGL1SNo", 40, 5, "S.No.", True, True, False)
            .AddAgTextColumn(DGL1, "Dgl1TableName", 250, 180, "Table Name", True, False, False)
            .AddAgTextColumn(DGL1, "Dgl1FieldName", 250, 180, "Field Name", True, False, False)
            .AddAgTextColumn(DGL1, "Dgl1UniqueField", 250, 0, "Unique Field Name", True, False, False)
            .AddAgCheckBoxColumn(DGL1, "Dgl1TransactionYn", 70, "Transaction Y/N")
            .AddAgCheckBoxColumn(DGL1, "Dgl1LineItemYn", 70, "Line Table Y/N")
        End With
        DGL1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(DGL1, Col_SNo)
        DGL1.TabIndex = Pnl1.TabIndex
        DGL1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        DGL1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)

    End Sub


    Sub FIniMaster()
        Dim DtTemp As DataTable
        Dim I As Integer

        mQry = "Insert Into Table_SearchField (Table_Name) " & _
               "Select S.Table_Name From Information_Schema.Tables S  " & _
               "Left Join Table_SearchField T ON S.TABLE_NAME = T.Table_Name " & _
               "WHERE T.Table_Name IS Null And S.Table_Type='BASE TABLE' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = "Select Table_Name, Search_Field, UniqueField, TransactionYn, LineItemYn From Table_SearchField Order By Table_Name"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        With DtTemp
            DGL1.RowCount = 1
            DGL1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To .Rows.Count - 1
                    DGL1.Rows.Add()
                    DGL1.Item(Col_SNo, I).Value = DGL1.Rows.Count - 1
                    DGL1.Item(Col1TableName, I).Value = AgL.XNull(.Rows(I)("Table_Name"))
                    DGL1.Item(Col1SearchField, I).Value = AgL.XNull(.Rows(I)("Search_field"))
                    DGL1.Item(Col1UniqueField, I).Value = AgL.XNull(.Rows(I)("UniqueField"))
                    DGL1.Item(Col1TransactionYn, I).Value = .Rows(I)("TransactionYn")
                    DGL1.Item(Col1LineItemYn, I).Value = .Rows(I)("LineItemYn")
                Next I
            End If
        End With

    End Sub


    Sub Ini_PrimaryFields(Optional ByVal TableName As String = "")
        Dim mCondition$ = ""
        If TableName <> "" Then mCondition = " AND T.TABLE_NAME = '" & TableName & "'"

        mQry = "SELECT   C.CONSTRAINT_NAME+C.COLUMN_NAME   AS Code, C.COLUMN_NAME " & _
               "FROM INFORMATION_SCHEMA.Table_Constraints T  " & _
               "LEFT JOIN INFORMATION_SCHEMA.Constraint_Column_Usage C ON T.CONSTRAINT_NAME =C.CONSTRAINT_NAME  " & _
               "WHERE T.CONSTRAINT_TYPE ='PRIMARY KEY'  "
        mQry += mCondition
        DGL1.AgHelpDataSet(Col1SearchField) = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim I As Integer
        If MsgBox("Sure to Save?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Try
            With DGL1
                For I = 0 To DGL1.RowCount - 1
                    If AgL.XNull(.Item(Col1TableName, I).Value) <> "" Then
                        mQry = "Delete From Table_SearchField Where Table_Name  = '" & .Item(Col1TableName, I).Value & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                        mQry = "Insert Into Table_SearchField(Table_Name, Search_Field, UniqueField, TransactionYn, LineItemYn) " & _
                               "Values (" & AgL.Chk_Text(.Item(Col1TableName, I).Value) & "," & AgL.Chk_Text(.Item(Col1SearchField, I).Value) & "," & AgL.Chk_Text(.Item(Col1UniqueField, I).Value) & "," & IIf(.Item(Col1TransactionYn, I).Value, 1, 0) & "," & IIf(.Item(Col1LineItemYn, I).Value, 1, 0) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End If
                Next
            End With
        Catch Ex As Exception
            MsgBox(Ex.Message)            
        End Try
    End Sub

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEnter
        Select Case e.ColumnIndex
            Case Col1SearchField
                If AgL.XNull(DGL1.Item(Col1TableName, DGL1.CurrentCell.RowIndex).Value) <> "" Then
                    Ini_PrimaryFields(DGL1.Item(Col1TableName, DGL1.CurrentCell.RowIndex).Value)

                End If
        End Select
    End Sub


    Sub Ini_UniqueFields()
        Dim PkCol(1) As DataColumn
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mUniqueFieldStr$

        mQry = "SELECT   C.CONSTRAINT_NAME+C.COLUMN_NAME   AS Code, C.Table_Name, C.COLUMN_NAME " & _
               "FROM INFORMATION_SCHEMA.Table_Constraints T  " & _
               "LEFT JOIN INFORMATION_SCHEMA.Constraint_Column_Usage C ON T.CONSTRAINT_NAME =C.CONSTRAINT_NAME  " & _
               "WHERE T.CONSTRAINT_TYPE ='PRIMARY KEY' Order By Code "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        PkCol(0) = DtTemp.Columns(0)
        DtTemp.PrimaryKey = PkCol

        For I = 0 To DGL1.Rows.Count - 1
            If DGL1.Item(Col1SearchField, I).Value <> "" Then
                DtTemp.DefaultView.RowFilter = Nothing
                DtTemp.DefaultView.RowFilter = "[Table_Name] = '" + DGL1.Item(Col1TableName, I).Value + "' "
                mUniqueFieldStr = ""
                If DtTemp.DefaultView.Count > 0 Then
                    For J = 0 To DtTemp.DefaultView.Count - 1
                        mUniqueFieldStr = mUniqueFieldStr + "Convert(nVarChar," & DtTemp.DefaultView.Item(J)("Column_Name") & ")" & IIf(J < DtTemp.DefaultView.Count - 1, "+", "")
                    Next
                End If
                If mUniqueFieldStr <> "" Then
                    DGL1.Item(Col1UniqueField, I).Value = mUniqueFieldStr
                End If
            End If
        Next

    End Sub

    Private Sub BtnFillUniqueField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillUniqueField.Click
        Ini_UniqueFields()
    End Sub
End Class