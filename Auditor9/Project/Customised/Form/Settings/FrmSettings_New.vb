Imports System.Data.SQLite
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports AgLibrary.ClsMain.agConstants

Public Class FrmSettings_New
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const ColSNo As String = "S.No."
    Protected Const Col1TableName As String = "Table Name"
    Protected Const Col1TableDispName As String = "Table Disp Name"
    Protected Const Col1PrimaryKey As String = "Primary Key"
    Protected Const Col1Code As String = "Code"
    Protected Const Col1SiteName As String = "Site"
    Protected Const Col1DivisionName As String = "Division"
    Protected Const Col1NCat As String = "NCat"
    Protected Const Col1VoucherType As String = "Voucher Type"
    Protected Const Col1FieldName As String = "Field Name"
    Protected Const Col1FieldDispName As String = "Field Disp Name"
    Protected Const Col1DataType As String = "Data Type"
    Protected Const Col1ReferenceTable As String = "Reference Table"
    Protected Const Col1Value As String = "Value"
    Protected Const Col1BtnSelection As String = "Selection"
    Protected Const Col1SelectionType As String = "Selection Type"

    Protected Const Col1ValueTag As String = "ValueTag"


    Dim mQry As String = ""
    Dim mGridRowNumber As Integer = 0

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String

    Dim DtSettingsData As New DataTable

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub InitSettingData()
        DtSettingsData.Columns.Add(ColSNo)
        DtSettingsData.Columns.Add(Col1TableName)
        DtSettingsData.Columns.Add(Col1TableDispName)
        DtSettingsData.Columns.Add(Col1PrimaryKey)
        DtSettingsData.Columns.Add(Col1Code)
        DtSettingsData.Columns.Add(Col1SiteName)
        DtSettingsData.Columns.Add(Col1DivisionName)
        DtSettingsData.Columns.Add(Col1NCat)
        DtSettingsData.Columns.Add(Col1VoucherType)
        DtSettingsData.Columns.Add(Col1FieldName)
        DtSettingsData.Columns.Add(Col1FieldDispName)
        DtSettingsData.Columns.Add(Col1DataType)
        DtSettingsData.Columns.Add(Col1ReferenceTable)
        DtSettingsData.Columns.Add(Col1Value)
        DtSettingsData.Columns.Add(Col1ValueTag)
        DtSettingsData.Columns.Add(Col1SelectionType)



    End Sub

    Private Sub Ini_Grid()
        Dgl1.ColumnHeadersHeight = 35

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgAllowFind = False


        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)


        Dgl1.Columns(ColSNo).Width = 50
        Dgl1.Columns(Col1TableName).Width = 155
        Dgl1.Columns(Col1TableDispName).Width = 170
        Dgl1.Columns(Col1PrimaryKey).Width = 140
        Dgl1.Columns(Col1Code).Width = 230
        Dgl1.Columns(Col1SiteName).Width = 120
        Dgl1.Columns(Col1DivisionName).Width = 90
        Dgl1.Columns(Col1NCat).Width = 140
        Dgl1.Columns(Col1VoucherType).Width = 200
        Dgl1.Columns(Col1FieldName).Width = 230
        Dgl1.Columns(Col1FieldDispName).Width = 230
        Dgl1.Columns(Col1DataType).Width = 100
        Dgl1.Columns(Col1ReferenceTable).Width = 100
        Dgl1.Columns(Col1Value).Width = 445
        Dgl1.Columns(Col1SelectionType).Width = 80


        Dgl1.Columns(ColSNo).ReadOnly = True
        Dgl1.Columns(Col1TableDispName).ReadOnly = True
        Dgl1.Columns(Col1SiteName).ReadOnly = True
        Dgl1.Columns(Col1DivisionName).ReadOnly = True
        Dgl1.Columns(Col1NCat).ReadOnly = True
        Dgl1.Columns(Col1VoucherType).ReadOnly = True
        Dgl1.Columns(Col1FieldDispName).ReadOnly = True
        Dgl1.Columns(Col1Value).ReadOnly = True

        Dgl1.Columns(Col1TableName).Visible = False
        Dgl1.Columns(Col1PrimaryKey).Visible = False
        Dgl1.Columns(Col1Code).Visible = False
        Dgl1.Columns(Col1FieldName).Visible = False
        Dgl1.Columns(Col1DataType).Visible = False
        Dgl1.Columns(Col1ReferenceTable).Visible = False
        Dgl1.Columns(Col1ValueTag).Visible = False
        Dgl1.Columns(Col1SelectionType).Visible = False



        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

    End Sub
    Sub FAddButtonColumn()
        If Dgl1.Columns.Contains(Col1BtnSelection) = False Then
            Dim mButtongColumns As New DataGridViewButtonColumn
            mButtongColumns.Name = Col1BtnSelection
            Dgl1.Columns.Add(mButtongColumns)
            Dgl1.Columns(Col1BtnSelection).Width = 30
            Dgl1.Columns(Col1BtnSelection).HeaderText = " "
        End If

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1SelectionType, I).Value) <> "Multi" Then
                Dgl1.Item(Col1BtnSelection, I) = New DataGridViewTextBoxCell
                Dgl1.Item(Col1BtnSelection, I).ReadOnly = True
            End If
        Next
    End Sub





    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        InitSettingData()
        MovRec()
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click, BtnAdd.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnClose.Name
                Me.Close()

            Case BtnAdd.Name
                Dim FrmObj As New FrmSettings_Add()
                FrmObj.StartPosition = FormStartPosition.CenterScreen
                FrmObj.ShowDialog()
                If Not AgL.StrCmp(FrmObj.UserAction, "OK") Then Exit Sub
                MovRec()
        End Select
    End Sub
    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
    End Sub
    Private Sub ProcSave(TableName As String, PrimaryKey As String, Code As String, FieldName As String, Value As Object)
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = "UPDATE " + TableName + " Set " + FieldName + " = " + "'" + Value + "'" + " Where " & PrimaryKey & " = " + "'" + Code + "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Value
                    If Dgl1.Columns(mColumnIndex).Name = Col1Value Then
                        If Dgl1.Item(Col1FieldName, mRowIndex).Value.ToString().Contains("Password") Then
                            Dgl1.Item(Col1ValueTag, mRowIndex).Value = Dgl1.Item(Col1Value, mRowIndex).Value
                            Dgl1.Item(Col1Value, mRowIndex).Value = ""
                            Dgl1.Item(Col1Value, mRowIndex).Value = New String("*", Len(Dgl1.Item(Col1ValueTag, mRowIndex).Value))
                        End If
                    End If


                    If AgL.XNull(Dgl1.Item(Col1ValueTag, mRowIndex).Value) <> "" Then
                        ProcSave(Dgl1.Item(Col1TableName, mRowIndex).Value,
                                 Dgl1.Item(Col1PrimaryKey, mRowIndex).Value,
                                 Dgl1.Item(Col1Code, mRowIndex).Value,
                                 Dgl1.Item(Col1FieldName, mRowIndex).Value, Dgl1.Item(Col1ValueTag, mRowIndex).Value)
                    Else
                        ProcSave(Dgl1.Item(Col1TableName, mRowIndex).Value,
                                 Dgl1.Item(Col1PrimaryKey, mRowIndex).Value,
                                 Dgl1.Item(Col1Code, mRowIndex).Value,
                                 Dgl1.Item(Col1FieldName, mRowIndex).Value, Dgl1.Item(Col1Value, mRowIndex).Value)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub MovRec()
        Try
            mGridRowNumber = 0
            Dgl1.Rows.Clear()
            GetSettingDataForTable("SaleInvoiceSetting")
            GetSettingDataForTable("PurchaseInvoiceSetting")
            GetSettingDataForTable("LedgerHeadSetting")
            GetSettingDataForTable("StockHeadSetting")
            GetSettingDataForTable("DivisionSiteSetting")
            GetSettingDataForTable("Enviro")
            GetSettingDataForTable("ItemTypeSetting")
            GetSettingDataForTable("SubgroupTypeSetting")
            GetSettingDataForTable("MailSender")
            GetSettingDataForTable("SmsSender")
            GetSettingDataForTable("ComputerSetting")
            Dgl1.DataSource = DtSettingsData
            Ini_Grid()

            For I As Integer = 0 To Dgl1.Columns.Count - 1
                If Dgl1.Columns(I).Name <> "Selection" Then
                    Dim BlankValueColumn As DataRow() = DtSettingsData.Select("[" + Dgl1.Columns(I).Name + "] <> '' ")
                    If BlankValueColumn.Length = 0 Then
                        Dgl1.Columns(I).Visible = False
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub GetSettingDataForTable(mTableName As String)
        Dim DtFields As DataTable = Nothing
        Dim DtForeignKeys As DataTable = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim mPrimaryKey As String = ""
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0

        If AgL.PubServerName = "" Then
            mQry = " PRAGMA foreign_key_list('" & mTableName & "') "
            Else
            mQry = "select O.name  AS [Table], c.name as [From]
                From sys.foreign_key_columns as fk
                inner join sys.tables as t on fk.parent_object_id = t.object_id
                --LEFT JOIN sys.columns c1 ON fk.constraint_object_id = c1.object_id --and fk.constraint_column_id = c1.column_id
                inner join sys.columns as c on fk.parent_object_id = c.object_id and fk.parent_column_id = c.column_id
                LEFT JOIN sys.objects O ON fk.referenced_object_id = O.object_id
                WHERE t.name = '" & mTableName & "'"
        End If
        DtForeignKeys = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If AgL.PubServerName = "" Then
            mQry = "PRAGMA table_info('" & mTableName & "') "
        Else
            mQry = "SELECT C.COLUMN_NAME AS Name, CASE WHEN VPrimary.column_name IS NOT NULL THEN 1 ELSE 0 END AS pk, 
                            C.DATA_TYPE As Type  
                            FROM INFORMATION_SCHEMA.Columns C 
                            LEFT JOIN (
	                            SELECT KU.table_name ,column_name 
	                            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC 
	                            INNER JOIN
	                                INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
	                                      ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' AND
	                                         TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
                            ) AS VPrimary ON C.Table_Name = VPrimary.Table_Name AND C.column_name = VPrimary.column_name
                            WHERE C.TABLE_NAME = '" & mTableName & "'"
        End If
        DtFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select  H.* From " & mTableName & " H "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                If DtFields.Select(" pk = 1").Length > 0 Then
                    mPrimaryKey = DtFields.Select(" pk = 1")(0)("name").ToString()
                End If


                For J = 0 To DtFields.Rows.Count - 1
                    If AgL.XNull(DtFields.Rows(J)("Name")) <> "Code" And AgL.XNull(DtFields.Rows(J)("Name")) <> "Site_Code" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "Div_Code" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "V_Type" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "NCat" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "SubgroupType" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "ComputerName" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "ItemType" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "EntryBy" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "EntryDate" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "EntryType" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "EntryStatus" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "MoveToLog" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "MoveToLogDate" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "Id" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "ApproveBy" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "ApproveDate" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "UploadDate" Then

                        DtSettingsData.Rows.Add()
                        DtSettingsData.Rows(mGridRowNumber)(ColSNo) = DtSettingsData.Rows.Count
                        DtSettingsData.Rows(mGridRowNumber)(Col1TableName) = mTableName
                        DtSettingsData.Rows(mGridRowNumber)(Col1TableDispName) = GetFormattedString(DtSettingsData.Rows(mGridRowNumber)(Col1TableName))
                        DtSettingsData.Rows(mGridRowNumber)(Col1PrimaryKey) = mPrimaryKey
                        DtSettingsData.Rows(mGridRowNumber)(Col1FieldName) = AgL.XNull(DtFields.Rows(J)("Name"))
                        DtSettingsData.Rows(mGridRowNumber)(Col1FieldDispName) = GetFormattedString(DtSettingsData.Rows(mGridRowNumber)(Col1FieldName))
                        DtSettingsData.Rows(mGridRowNumber)(Col1DataType) = AgL.XNull(DtFields.Rows(J)("Type")).ToString()

                        For K = 0 To DtForeignKeys.Rows.Count - 1
                            If DtSettingsData.Rows(mGridRowNumber)(Col1FieldName) = AgL.XNull(DtForeignKeys.Rows(K)("from")) Then
                                DtSettingsData.Rows(mGridRowNumber)(Col1ReferenceTable) = AgL.XNull(DtForeignKeys.Rows(K)("Table"))
                            End If
                        Next

                        If AgL.XNull(DtFields.Rows(J)("Name")).ToString().ToUpper.Contains("FILTER") Then
                            Dim mReferneceTable$ = AgL.XNull(DtFields.Rows(J)("Name")).ToString().Replace("FilterInclude_", "").Replace("FilterExclude_", "").Replace("Head", "").Replace("Line", "")

                            If AgL.PubServerName = "" Then
                                mQry = "SELECT Count(*) FROM sqlite_master WHERE UPPER(name) = UPPER('" & mReferneceTable$ & "')"
                            Else
                                mQry = "SELECT Count(*) FROM INFORMATION_SCHEMA.Tables WHERE TABLE_NAME = '" & mReferneceTable$ & "' "
                            End If
                            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
                                DtSettingsData.Rows(mGridRowNumber)(Col1SelectionType) = "Multi"
                                DtSettingsData.Rows(mGridRowNumber)(Col1ReferenceTable) = mReferneceTable
                            ElseIf DtSettingsData.Rows(mGridRowNumber)(Col1FieldName).ToString.Contains("Filter") And
                                    (DtSettingsData.Rows(mGridRowNumber)(Col1FieldName).ToString.Contains("Nature") Or
                                    DtSettingsData.Rows(mGridRowNumber)(Col1FieldName).ToString.Contains("Tree") Or
                                    DtSettingsData.Rows(mGridRowNumber)(Col1FieldName).ToString.Contains("MasterParty") Or
                                    DtSettingsData.Rows(mGridRowNumber)(Col1FieldName).ToString.Contains("ItemV_Type")) Then
                                DtSettingsData.Rows(mGridRowNumber)(Col1SelectionType) = "Multi"
                            End If
                        End If




                        If AgL.XNull(DtFields.Rows(J)("Type")).ToString() = "bit" Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1ValueTag) = AgL.VNull(DtTemp.Rows(I)(DtSettingsData.Rows(mGridRowNumber)(Col1FieldName)))
                            If AgL.VNull(DtSettingsData.Rows(mGridRowNumber)(Col1ValueTag)) = 0 Then
                                DtSettingsData.Rows(mGridRowNumber)(Col1Value) = "No"
                            Else
                                DtSettingsData.Rows(mGridRowNumber)(Col1Value) = "Yes"
                            End If

                        ElseIf AgL.XNull(DtSettingsData.Rows(mGridRowNumber)(Col1ReferenceTable)) <> "" Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1ValueTag) = AgL.XNull(DtTemp.Rows(I)(DtSettingsData.Rows(mGridRowNumber)(Col1FieldName)))
                            Dim DtResult As DataTable = AgL.FillData(" Select " & GetDescriptionColumns(DtSettingsData.Rows(mGridRowNumber)(Col1ReferenceTable)) & " As Description
                                        From " + DtSettingsData.Rows(mGridRowNumber)(Col1ReferenceTable) + " 
                                        Where " & GetCodeColumns(DtSettingsData.Rows(mGridRowNumber)(Col1ReferenceTable)) & " In ('" & DtSettingsData.Rows(mGridRowNumber)(Col1ValueTag).ToString().Replace("+", "','") & "') ", AgL.GCn).Tables(0)
                            For K = 0 To DtResult.Rows.Count - 1
                                If AgL.XNull(DtSettingsData.Rows(mGridRowNumber)(Col1SelectionType)) = "Multi" Then
                                    DtSettingsData.Rows(mGridRowNumber)(Col1Value) += "+" + AgL.XNull(DtResult.Rows(K)("Description"))
                                Else
                                    DtSettingsData.Rows(mGridRowNumber)(Col1Value) = AgL.XNull(DtResult.Rows(K)("Description"))
                                End If
                            Next

                        Else
                            DtSettingsData.Rows(mGridRowNumber)(Col1Value) = AgL.XNull(DtTemp.Rows(I)(DtSettingsData.Rows(mGridRowNumber)(Col1FieldName)))
                        End If


                        DtSettingsData.Rows(mGridRowNumber)(Col1Code) = AgL.XNull(DtTemp.Rows(I)(mPrimaryKey))
                        If DtTemp.Columns.Contains("NCat") = True Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1NCat) = GetFormattedString(ClsMain.FGetNCatDesc(AgL.XNull(DtTemp.Rows(I)("NCat"))))
                        End If
                        If DtTemp.Columns.Contains("V_Type") = True Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1VoucherType) = AgL.XNull(AgL.Dman_Execute("Select  Description From Voucher_Type Where V_Type = '" & AgL.XNull(DtTemp.Rows(I)("V_Type")) & "'", AgL.GCn).ExecuteScalar)
                        End If
                        If DtTemp.Columns.Contains("Site_Code") = True Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1SiteName) = AgL.XNull(AgL.Dman_Execute("Select Name From SiteMast WHERE Code = '" & AgL.XNull(DtTemp.Rows(I)("Site_Code")) & "'", AgL.GCn).ExecuteScalar)
                        End If
                        If DtTemp.Columns.Contains("Div_Code") = True Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1DivisionName) = AgL.XNull(AgL.Dman_Execute("Select Div_Name From Division WHERE Div_Code = '" & AgL.XNull(DtTemp.Rows(I)("Div_Code")) & "'", AgL.GCn).ExecuteScalar)
                        End If
                        If DtTemp.Columns.Contains("SubGroupType") = True Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1VoucherType) = AgL.XNull(DtTemp.Rows(I)("SubGroupType"))
                        End If
                        If DtTemp.Columns.Contains("ItemType") = True Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1VoucherType) = AgL.XNull(AgL.Dman_Execute("Select  Name From ItemType Where Code = '" & AgL.XNull(DtTemp.Rows(I)("ItemType")) & "'", AgL.GCn).ExecuteScalar)
                        End If
                        If DtTemp.Columns.Contains("ComputerName") = True Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1VoucherType) = AgL.XNull(AgL.Dman_Execute("Select  Name From ItemType Where Code = '" & AgL.XNull(DtTemp.Rows(I)("ComputerName")) & "'", AgL.GCn).ExecuteScalar)
                        End If

                        If DtSettingsData.Rows(mGridRowNumber)(Col1FieldName).ToString().Contains("Password") Then
                            DtSettingsData.Rows(mGridRowNumber)(Col1ValueTag) = DtSettingsData.Rows(mGridRowNumber)(Col1Value)
                            DtSettingsData.Rows(mGridRowNumber)(Col1Value) = ""
                            DtSettingsData.Rows(mGridRowNumber)(Col1Value) = New String("*", Len(DtSettingsData.Rows(mGridRowNumber)(Col1ValueTag)))
                        End If

                        mGridRowNumber += 1

                    End If
                Next
            Next
            'Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            'Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        End If

    End Sub
    'Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
    '    Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
    '    Dim bItemCode As String = ""
    '    Dim DrTemp As DataRow() = Nothing
    '    Try
    '        bRowIndex = Dgl1.CurrentCell.RowIndex
    '        bColumnIndex = Dgl1.CurrentCell.ColumnIndex

    '        If e.KeyCode = Keys.Enter Then Exit Sub
    '        If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

    '        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
    '            Case Col1Value
    '                If AgL.XNull(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) <> "" Then
    '                    If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
    '                        mQry = "Select " & GetCodeColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As Code, 
    '                                    " & GetDescriptionColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As  Description 
    '                                    From  " + Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value
    '                        Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
    '                    End If
    '                End If

    '                FGetOtherHelpLists()

    '                If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
    '                    Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag
    '                End If
    '        End Select
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Down Or
                e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Then
                Exit Sub
            End If

            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            If e.Control Or e.Shift Or e.Alt Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Value
                    If AgL.StrCmp(Dgl1.Item(Col1DataType, Dgl1.CurrentCell.RowIndex).Value, "Bit") Then
                        If AgL.StrCmp(ChrW(e.KeyCode), "Y") Then
                            Dgl1.Item(Col1ValueTag, bRowIndex).Value = 1
                            Dgl1.Item(Col1Value, bRowIndex).Value = "Yes"
                        ElseIf AgL.StrCmp(ChrW(e.KeyCode), "N") Then
                            Dgl1.Item(Col1ValueTag, bRowIndex).Value = 0
                            Dgl1.Item(Col1Value, bRowIndex).Value = "No"
                        End If

                        If AgL.StrCmp(ChrW(e.KeyCode), "Y") Or AgL.StrCmp(ChrW(e.KeyCode), "N") Then
                            If Dgl1.Item(Col1ValueTag, bRowIndex).Value = -1 Then
                                Dgl1.Item(Col1ValueTag, bRowIndex).Value = 1
                            End If
                        End If


                        If Dgl1.Item(Col1ValueTag, bRowIndex).Value IsNot Nothing Then
                            ProcSave(Dgl1.Item(Col1TableName, bRowIndex).Value,
                                 Dgl1.Item(Col1PrimaryKey, bRowIndex).Value,
                                 Dgl1.Item(Col1Code, bRowIndex).Value,
                                 Dgl1.Item(Col1FieldName, bRowIndex).Value, Dgl1.Item(Col1ValueTag, bRowIndex).Value)
                        Else
                            ProcSave(Dgl1.Item(Col1TableName, bRowIndex).Value,
                                 Dgl1.Item(Col1PrimaryKey, bRowIndex).Value,
                                 Dgl1.Item(Col1Code, bRowIndex).Value,
                                 Dgl1.Item(Col1FieldName, bRowIndex).Value, Dgl1.Item(Col1Value, bRowIndex).Value)
                        End If
                    Else
                        FShowSingleHelp(bRowIndex, bColumnIndex)

                        If Dgl1.Item(Col1ValueTag, bRowIndex).Value IsNot Nothing Then
                            ProcSave(Dgl1.Item(Col1TableName, bRowIndex).Value,
                                 Dgl1.Item(Col1PrimaryKey, bRowIndex).Value,
                                 Dgl1.Item(Col1Code, bRowIndex).Value,
                                 Dgl1.Item(Col1FieldName, bRowIndex).Value, Dgl1.Item(Col1ValueTag, bRowIndex).Value)
                        Else
                            ProcSave(Dgl1.Item(Col1TableName, bRowIndex).Value,
                                 Dgl1.Item(Col1PrimaryKey, bRowIndex).Value,
                                 Dgl1.Item(Col1Code, bRowIndex).Value,
                                 Dgl1.Item(Col1FieldName, bRowIndex).Value, Dgl1.Item(Col1Value, bRowIndex).Value)
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim mRowIndex As Integer = 0, mColumnIndex As Integer = 0
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi

        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex


            If AgL.XNull(Dgl1.Item(Col1SelectionType, Dgl1.CurrentCell.RowIndex).Value) <> "Multi" Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1BtnSelection
                    If AgL.XNull(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) <> "" Then
                        mQry = "Select 'o' As Tick, " & GetCodeColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As Code, 
                                " & GetDescriptionColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As Description 
                                From  " + Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value
                    ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("Filter") And
                            Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("Nature") Then
                        mQry = "Select Distinct 'o' As Tick, Nature As Code, 
                                Nature As Description 
                                From  AcGroup Where Nature Is Not Null "
                    ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("Filter") And
                            Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("Tree") Then
                        mQry = GetStringsFromClassConstants(GetType(TreeNodeType)).Replace("Select", "Select 'o' As Tick,")
                    ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("Filter") And
                            Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("ItemV_Type") Then
                        mQry = GetStringsFromClassConstants(GetType(ItemV_Type)).Replace("Select", "Select 'o' As Tick,")
                    ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("Filter") And
                            Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("SubgroupType") Then
                        mQry = "SELECT 'o' As Tick, SubgroupType As Code, SubgroupType As Description FROM SubGroupType "
                    End If

                    FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).Tables(0)), "", 500, 600, , , False)
                    FRH_Multiple.ChkAll.Visible = False
                    FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
                    FRH_Multiple.FFormatColumn(1, , 0, , False)
                    FRH_Multiple.FFormatColumn(2, "Description", 400, DataGridViewContentAlignment.MiddleLeft)
                    FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
                    FRH_Multiple.ShowDialog()

                    If FRH_Multiple.BytBtnValue = 0 Then
                        If FRH_Multiple.FFetchData(1, "", "", "+", True) <> "" Then
                            Dgl1.Item(Col1ValueTag, Dgl1.CurrentCell.RowIndex).Value = "+" + FRH_Multiple.FFetchData(1, "", "", "+", True)
                            Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value = "+" + FRH_Multiple.FFetchData(2, "", "", "+")
                        Else
                            Dgl1.Item(Col1ValueTag, Dgl1.CurrentCell.RowIndex).Value = ""
                            Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value = ""
                        End If
                    End If

                    Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
                    ProcSave(Dgl1.Item(Col1TableName, mRowIndex).Value,
                             Dgl1.Item(Col1PrimaryKey, mRowIndex).Value,
                             Dgl1.Item(Col1Code, mRowIndex).Value,
                             Dgl1.Item(Col1FieldName, mRowIndex).Value, Dgl1.Item(Col1ValueTag, mRowIndex).Value)


            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function GetCodeColumns(mTableName As String) As String
        Dim mRetStr = ""

        Select Case UCase(mTableName)
            Case UCase("AcGroup")
                mRetStr = " GroupCode "
            Case UCase("SubGroup")
                mRetStr = " SubCode "
            Case UCase("SubGroupType")
                mRetStr = " SubgroupType "
            Case UCase("PostingGroupSalesTaxParty")
                mRetStr = " Description "
            Case UCase("PostingGroupSalesTaxItem")
                mRetStr = " Description "
            Case Else
                mRetStr = " Code "
        End Select
        GetCodeColumns = mRetStr
    End Function
    Private Function GetDescriptionColumns(mTableName As String) As String
        Dim mRetStr = ""
        Select Case UCase(mTableName)
            Case UCase("AcGroup")
                mRetStr = " GroupName "
            Case UCase("ItemType")
                mRetStr = " Name "
            Case UCase("SubGroup")
                mRetStr = " Name "
            Case UCase("SubGroupType")
                mRetStr = " SubgroupType "
            Case Else
                mRetStr = " Description "
        End Select
        GetDescriptionColumns = mRetStr
    End Function

    Private Sub FrmSettings_New_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Function GetFormattedString(FieldName As String)
        Dim FieldNameArr As MatchCollection = Regex.Matches(FieldName.Trim(), "[A-Z][a-z]+")
        Dim strFieldName As String = ""
        For J As Integer = 0 To FieldNameArr.Count - 1
            If strFieldName = "" Then
                strFieldName = FieldNameArr(J).ToString
            Else
                strFieldName += " " + FieldNameArr(J).ToString
            End If
        Next
        If strFieldName <> "" Then
            If strFieldName.ToUpper().Trim().Replace(" ", "").Replace("_", "") <> FieldName.ToUpper().Trim().Replace(" ", "").Replace("_", "") Then
                Return FieldName
            Else
                Return strFieldName
            End If
        Else
            Return FieldName
        End If
    End Function
    Private Sub FGetOtherHelpLists()
        If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
            Select Case Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim
                Case "DiscountCalculationPattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(DiscountCalculationPattern)), AgL.GCn)
                Case "BarcodePattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(BarcodePattern)), AgL.GCn)
                Case "BarcodeType"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(BarcodeType)), AgL.GCn)
                Case "DiscountSuggestionPattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(DiscountSuggestPattern)), AgL.GCn)
                Case "IndustryType"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(IndustryType)), AgL.GCn)
                Case "PlaceOfSupplay"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(PlaceOfSupplay)), AgL.GCn)
                Case "SaleInvoicePattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(SaleInvoicePattern)), AgL.GCn)
                Case "SubgroupRegistrationType"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(SubgroupRegistrationType)), AgL.GCn)
                Case "ActionOnDuplicateItem"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(ActionOnDuplicateItem)), AgL.GCn)
                Case "ActionIfCreditLimitExceeds"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(ActionIfCreditLimitExceeds)), AgL.GCn)
                Case "LedgerPostingPartyAcType"
                    If AgL.StrCmp(Dgl1.Item(Col1TableName, Dgl1.CurrentCell.RowIndex).Value, "SaleInvoiceSetting") Then
                        Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(SaleInvoiceLedgerPostingPartyAcType)), AgL.GCn)
                    ElseIf AgL.StrCmp(Dgl1.Item(Col1TableName, Dgl1.CurrentCell.RowIndex).Value, "PurchaseInvoiceSetting") Then
                        Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(PurchInvoiceLedgerPostingPartyAcType)), AgL.GCn)
                    End If
                Case "ActionIfMaximumCashTransactionLimitExceeds"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(ClsMain.ActionsOfMaximumCashTransactionLimitExceeds)), AgL.GCn)
                Case "LrGenerationPattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(LrGenerationPattern)), AgL.GCn)
            End Select

            If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("DiscountCalculationPattern") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(DiscountCalculationPattern)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("BarcodePattern") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(BarcodePattern)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("BarcodeType") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(BarcodeType)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("DiscountSuggestionPattern") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(DiscountSuggestPattern)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("IndustryType") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(IndustryType)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("PlaceOfSupplay") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(PlaceOfSupplay)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("SaleInvoicePattern") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(SaleInvoicePattern)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("SubgroupRegistrationType") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(SubgroupRegistrationType)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("ActionOnDuplicateItem") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(ActionOnDuplicateItem)), AgL.GCn)
                ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim.Contains("ActionIfCreditLimitExceeds") Then
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(GetStringsFromClassConstants(GetType(ActionOnDuplicateItem)), AgL.GCn)
                End If
            End If
        End If
    End Sub
    Function GetStringsFromClassConstants(ByVal type As System.Type) As String
        Dim constants As New ArrayList()
        Dim fieldInfos As FieldInfo() =
            type.GetFields(BindingFlags.[Public] Or
                           BindingFlags.[Static] Or
                           BindingFlags.FlattenHierarchy)
        For Each fi As FieldInfo In fieldInfos
            If fi.IsLiteral AndAlso Not fi.IsInitOnly Then
                constants.Add(fi)
            End If
        Next
        Dim ConstantsStringArray As New System.Collections.Specialized.StringCollection
        For Each fi As FieldInfo In DirectCast(constants.ToArray(GetType(FieldInfo)), FieldInfo())
            ConstantsStringArray.Add(CStr(fi.GetValue(Nothing)))
        Next
        Dim retVal(ConstantsStringArray.Count - 1) As String
        ConstantsStringArray.CopyTo(retVal, 0)

        Dim bStrQry = ""
        For I As Integer = 0 To retVal.Length - 1
            If bStrQry <> "" Then bStrQry += " UNION ALL "
            bStrQry += "Select '" & retVal(I) & "' As Code, '" & retVal(I) & "' As Description "
        Next
        Return bStrQry
    End Function
    Private Sub Dgl1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dgl1.KeyPress
        Try
            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then Exit Sub
                If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1BtnSelection).Index Then Exit Sub
            End If

            If e.KeyChar = vbCr Or e.KeyChar = vbCrLf Or e.KeyChar = vbTab Or e.KeyChar = ChrW(27) Then Exit Sub

            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = "Tick" Then Exit Sub
                fld = Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End If

            If Dgl1.CurrentCell Is Nothing Then
                DtSettingsData.DefaultView.RowFilter = Nothing
            End If

            If Asc(e.KeyChar) = Keys.Back Then
                If TxtFind.Text <> "" Then TxtFind.Text = Microsoft.VisualBasic.Left(TxtFind.Text, Len(TxtFind.Text) - 1)
            End If

            FManageFindTextboxVisibility()

            TxtFind_KeyPress(TxtFind, e)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtFind_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFind.KeyPress
        RowsFilter(HlpSt, Dgl1, sender, e, fld, DtSettingsData)
    End Sub

    Private Function RowsFilter(ByVal selStr As String, ByVal CtrlObj As Object, ByVal TXT As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal FndFldName As String, ByVal DTable As DataTable) As Integer
        Try
            Dim strExpr As String, findStr As String, bSelStr As String = ""
            Dim sa As String
            Dim IntRow As Integer
            Dim i As Integer
            sa = TXT.Text
            bSelStr = selStr

            If sa.Length = 0 And Asc(e.KeyChar) = 8 Then IntRow = 0 : CtrlObj.CurrentCell = CtrlObj(FndFldName, IntRow) : DtSettingsData.DefaultView.RowFilter = Nothing : Dgl1.CurrentCell = Dgl1(FndFldName, 0) : Exit Function
            If TXT.Text = "(null)" Then
                findStr = e.KeyChar
            Else
                findStr = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, TXT.Text, TXT.Text + e.KeyChar)
            End If
            strExpr = "ltrim([" & FndFldName & "])  like '" & findStr & "%' "
            i = InStr(selStr, "where", CompareMethod.Text)
            If i = 0 Then
                selStr = selStr + " where " + strExpr + "order by [" & FndFldName & "]"
            Else
                selStr = selStr + " and " + strExpr + "order by [" & FndFldName & "]"
            End If

            ''==================================< Filter DTFind For Searching >====================================================
            DtSettingsData.DefaultView.RowFilter = Nothing
            'DtSettingsData.DefaultView.RowFilter = " [" & FndFldName & "] like '%" & findStr & "%' "
            If DtSettingsData.DefaultView.RowFilter <> "" And DtSettingsData.DefaultView.RowFilter <> Nothing Then
                DtSettingsData.DefaultView.RowFilter += " And " + " [" & FndFldName & "] like '" & findStr & "%' "
            Else
                DtSettingsData.DefaultView.RowFilter += " [" & FndFldName & "] like '" & findStr & "%' "
            End If
            Try
                Dgl1.CurrentCell = Dgl1(FndFldName, 0)
            Catch ex As Exception
            End Try
            TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)

            FManageFindTextboxVisibility()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub DGL1_Click(sender As Object, e As EventArgs) Handles Dgl1.Click
        TxtFind.Text = ""
        FManageFindTextboxVisibility()
    End Sub

    Private Sub DGL1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles Dgl1.PreviewKeyDown
        If e.KeyCode = Keys.Delete Then TxtFind.Text = "" : FManageFindTextboxVisibility() : DtSettingsData.DefaultView.RowFilter = Nothing : Dgl1.CurrentCell = Dgl1(fld, 0) : DtSettingsData.DefaultView.RowFilter = Nothing
    End Sub
    Private Sub FManageFindTextboxVisibility()
        If TxtFind.Text = "" Then TxtFind.Visible = False : TxtFind.Visible = True
    End Sub
    Private Sub Dgl1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Dgl1.DataBindingComplete
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1SelectionType, I).Value) = "Multi" Or
                    AgL.StrCmp(Dgl1.Item(Col1DataType, I).Value, "Bit") Then
                Dgl1.Item(Col1Value, I).ReadOnly = True
            End If
        Next

        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)

        FAddButtonColumn()
    End Sub
    Private Sub FShowSingleHelp(bRowIndex As Integer, bColumnIndex As Integer)
        If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

        Select Case Dgl1.Columns(bColumnIndex).Name
            Case Col1Value
                If AgL.XNull(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) <> "" Then
                    If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select " & GetCodeColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As Code, 
                                        " & GetDescriptionColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As  Description 
                                        From  " + Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value
                        Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

                FGetOtherHelpLists()
        End Select


        If Dgl1.Item(Col1FieldName, bRowIndex).Tag IsNot Nothing Then
            Dim FRH_Single As DMHelpGrid.FrmHelpGrid
            FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(CType(Dgl1.Item(Col1FieldName, bRowIndex).Tag, DataSet).Tables(0)), "", 350, 300, 150, 520, False)
            FRH_Single.FFormatColumn(0, , 0, , False)
            FRH_Single.FFormatColumn(1, "Description", 200, DataGridViewContentAlignment.MiddleLeft)
            FRH_Single.StartPosition = FormStartPosition.Manual
            FRH_Single.ShowDialog()

            Dim bCode As String = ""
            If FRH_Single.BytBtnValue = 0 Then
                Dgl1.Item(Col1ValueTag, bRowIndex).Value = FRH_Single.DRReturn("Code")
                Dgl1.Item(Col1Value, bRowIndex).Value = FRH_Single.DRReturn("Description")
            End If
        ElseIf AgL.XNull(Dgl1.Item(Col1SelectionType, bRowIndex).Value) = "Multi" Or
                    AgL.StrCmp(Dgl1.Item(Col1DataType, bRowIndex).Value, "Bit") Then
            Dgl1.Item(Col1Value, bRowIndex).ReadOnly = True
        Else
            Dgl1.Item(Col1Value, bRowIndex).ReadOnly = False
        End If
    End Sub
End Class