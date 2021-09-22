Imports System.Data.SQLite
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports AgLibrary.ClsMain.agConstants

Public Class FrmSettings
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const ColSNo As String = "S.No."
    Protected Const Col1TableName As String = "Table Name"
    Protected Const Col1TableDispName As String = "Table Disp Name"
    Protected Const Col1PrimaryKey As String = "Primary Key"
    Protected Const Col1Code As String = "Code"
    Protected Const Col1SiteName As String = "Site"
    Protected Const Col1DivisionName As String = "Division"

    Protected Const Col1VoucherType As String = "Voucher Type"
    Protected Const Col1FieldName As String = "Field Name"
    Protected Const Col1FieldDispName As String = "Field Disp Name"
    Protected Const Col1DataType As String = "Data Type"
    Protected Const Col1ReferenceTable As String = "Reference Table"
    Protected Const Col1Value As String = "Value"
    Protected Const Col1BtnSelection As String = "Selection"
    Protected Const Col1SelectionType As String = "Selection Type"


    Dim mQry As String = ""
    Dim mGridRowNumber As Integer = 0

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub Ini_Grid()
        Dim I As Integer = 0
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 50, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1TableName, 155, 0, Col1TableName, False, True)
            .AddAgTextColumn(Dgl1, Col1TableDispName, 170, 0, Col1TableDispName, True, True)
            .AddAgTextColumn(Dgl1, Col1PrimaryKey, 140, 0, Col1PrimaryKey, False, True)
            .AddAgTextColumn(Dgl1, Col1Code, 230, 0, Col1Code, False, True)
            .AddAgTextColumn(Dgl1, Col1SiteName, 120, 0, Col1SiteName, True, True)
            .AddAgTextColumn(Dgl1, Col1DivisionName, 100, 0, Col1DivisionName, True, True)

            .AddAgTextColumn(Dgl1, Col1VoucherType, 200, 0, Col1VoucherType, True, True)
            .AddAgTextColumn(Dgl1, Col1FieldName, 230, 0, Col1FieldName, False, True)
            .AddAgTextColumn(Dgl1, Col1FieldDispName, 230, 0, Col1FieldDispName, True, True)
            .AddAgTextColumn(Dgl1, Col1DataType, 100, 0, Col1DataType, False, True)
            .AddAgTextColumn(Dgl1, Col1ReferenceTable, 100, 0, Col1ReferenceTable, False, True)
            .AddAgTextColumn(Dgl1, Col1Value, 445, 0, Col1Value, True, False)
            .AddAgButtonColumn(Dgl1, Col1BtnSelection, 30, " ", True, False)
            .AddAgTextColumn(Dgl1, Col1SelectionType, 80, 0, Col1SelectionType, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgAllowFind = True


        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        Ini_Grid()
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
    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
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
                            Dgl1.Item(Col1Value, mRowIndex).Tag = Dgl1.Item(Col1Value, mRowIndex).Value
                            Dgl1.Item(Col1Value, mRowIndex).Value = ""
                            Dgl1.Item(Col1Value, mRowIndex).Value = New String("*", Len(Dgl1.Item(Col1Value, mRowIndex).Tag))
                        End If
                    End If


                    If Dgl1.Item(Col1Value, mRowIndex).Tag IsNot Nothing Then
                        ProcSave(Dgl1.Item(Col1TableName, mRowIndex).Value,
                                 Dgl1.Item(Col1PrimaryKey, mRowIndex).Value,
                                 Dgl1.Item(Col1Code, mRowIndex).Value,
                                 Dgl1.Item(Col1FieldName, mRowIndex).Value, Dgl1.Item(Col1Value, mRowIndex).Tag)
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
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "SubgroupType" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "EntryBy" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "EntryDate" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "EntryType" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "EntryStatus" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "MoveToLog" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "MoveToLogDate" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "ApproveBy" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "ApproveDate" And
                                AgL.XNull(DtFields.Rows(J)("Name")) <> "UploadDate" Then
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, mGridRowNumber).Value = Dgl1.Rows.Count
                        Dgl1.Item(Col1TableName, mGridRowNumber).Value = mTableName
                        Dgl1.Item(Col1TableDispName, mGridRowNumber).Value = GetFormattedString(Dgl1.Item(Col1TableName, mGridRowNumber).Value)
                        Dgl1.Item(Col1PrimaryKey, mGridRowNumber).Value = mPrimaryKey
                        Dgl1.Item(Col1FieldName, mGridRowNumber).Value = AgL.XNull(DtFields.Rows(J)("Name"))
                        Dgl1.Item(Col1FieldDispName, mGridRowNumber).Value = GetFormattedString(Dgl1.Item(Col1FieldName, mGridRowNumber).Value)

                        Dgl1.Item(Col1DataType, mGridRowNumber).Value = AgL.XNull(DtFields.Rows(J)("Type")).ToString()

                        For K = 0 To DtForeignKeys.Rows.Count - 1
                            If Dgl1.Item(Col1FieldName, mGridRowNumber).Value = AgL.XNull(DtForeignKeys.Rows(K)("from")) Then
                                Dgl1.Item(Col1ReferenceTable, mGridRowNumber).Value = AgL.XNull(DtForeignKeys.Rows(K)("Table"))
                            End If
                        Next

                        If AgL.XNull(DtFields.Rows(J)("Name")).ToString().ToUpper.Contains("FILTER") Then
                            Dim mReferneceTable$ = AgL.XNull(DtFields.Rows(J)("Name")).ToString().Replace("FilterInclude_", "").Replace("FilterExclude_", "").Replace("Head", "").Replace("Line", "")

                            If AgL.PubServerName = "" Then
                                mQry = "SELECT Count(*) FROM sqlite_master WHERE type='table' AND name='" & mReferneceTable$ & "'"
                            Else
                                mQry = "SELECT Count(*) FROM INFORMATION_SCHEMA.Tables WHERE TABLE_NAME = '" & mReferneceTable$ & "' AND TABLE_TYPE = 'BASE TABLE'"
                            End If
                            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
                                Dgl1.Item(Col1SelectionType, mGridRowNumber).Value = "Multi"
                                Dgl1.Item(Col1ReferenceTable, mGridRowNumber).Value = mReferneceTable
                                Dgl1.Item(Col1Value, mGridRowNumber).ReadOnly = True
                            ElseIf Dgl1.Item(Col1FieldName, mGridRowNumber).Value.ToString.Contains("Filter") And
                                    Dgl1.Item(Col1FieldName, mGridRowNumber).Value.ToString.Contains("Nature") Then
                                Dgl1.Item(Col1SelectionType, mGridRowNumber).Value = "Multi"
                                Dgl1.Item(Col1Value, mGridRowNumber).ReadOnly = True
                            Else
                                Dgl1.Item(Col1BtnSelection, mGridRowNumber) = New DataGridViewTextBoxCell
                                Dgl1.Item(Col1BtnSelection, mGridRowNumber).ReadOnly = True
                            End If
                        Else
                            Dgl1.Item(Col1BtnSelection, mGridRowNumber) = New DataGridViewTextBoxCell
                            Dgl1.Item(Col1BtnSelection, mGridRowNumber).ReadOnly = True
                        End If

                        If AgL.XNull(DtFields.Rows(J)("Type")).ToString() = "bit" Then
                            Dgl1.Item(Col1Value, mGridRowNumber).Tag = AgL.VNull(DtTemp.Rows(I)(Dgl1.Item(Col1FieldName, mGridRowNumber).Value))
                            Dgl1.Item(Col1Value, mGridRowNumber).Value = AgL.XNull(DtTemp.Rows(I)(Dgl1.Item(Col1FieldName, mGridRowNumber).Value))
                        ElseIf Dgl1.Item(Col1ReferenceTable, mGridRowNumber).Value <> Nothing And Dgl1.Item(Col1ReferenceTable, mGridRowNumber).Value <> "" Then
                            Dgl1.Item(Col1Value, mGridRowNumber).Tag = AgL.XNull(DtTemp.Rows(I)(Dgl1.Item(Col1FieldName, mGridRowNumber).Value))
                            Dim DtResult As DataTable = AgL.FillData(" Select " & GetDescriptionColumns(Dgl1.Item(Col1ReferenceTable, mGridRowNumber).Value) & " As Description
                                        From " + Dgl1.Item(Col1ReferenceTable, mGridRowNumber).Value + " 
                                        Where " & GetCodeColumns(Dgl1.Item(Col1ReferenceTable, mGridRowNumber).Value) & " In ('" & Dgl1.Item(Col1Value, mGridRowNumber).Tag.ToString().Replace("+", "','") & "') ", AgL.GCn).Tables(0)
                            For K = 0 To DtResult.Rows.Count - 1
                                If Dgl1.Item(Col1SelectionType, mGridRowNumber).Value = "Multi" Then
                                    Dgl1.Item(Col1Value, mGridRowNumber).Value += "+" + AgL.XNull(DtResult.Rows(K)("Description"))
                                Else
                                    Dgl1.Item(Col1Value, mGridRowNumber).Value = AgL.XNull(DtResult.Rows(K)("Description"))
                                End If
                            Next

                        Else
                            Dgl1.Item(Col1Value, mGridRowNumber).Value = AgL.XNull(DtTemp.Rows(I)(Dgl1.Item(Col1FieldName, mGridRowNumber).Value))
                        End If


                        Dgl1.Item(Col1Code, mGridRowNumber).Value = AgL.XNull(DtTemp.Rows(I)(mPrimaryKey))

                        If DtTemp.Columns.Contains("V_Type") = True Then
                            Dgl1.Item(Col1VoucherType, mGridRowNumber).Tag = AgL.XNull(DtTemp.Rows(I)("V_Type"))
                            Dgl1.Item(Col1VoucherType, mGridRowNumber).Value = AgL.XNull(AgL.Dman_Execute("Select  Description From Voucher_Type Where V_Type = '" & AgL.XNull(DtTemp.Rows(I)("V_Type")) & "'", AgL.GCn).ExecuteScalar)
                            If Dgl1.Item(Col1VoucherType, mGridRowNumber).Value = "" Then
                                Dgl1.Item(Col1VoucherType, mGridRowNumber).Value = Dgl1.Item(Col1VoucherType, mGridRowNumber).Tag
                            End If
                        End If
                        If DtTemp.Columns.Contains("Site_Code") = True Then
                            Dgl1.Item(Col1SiteName, mGridRowNumber).Value = AgL.XNull(AgL.Dman_Execute("Select Name From SiteMast WHERE Code = '" & AgL.XNull(DtTemp.Rows(I)("Site_Code")) & "'", AgL.GCn).ExecuteScalar)
                        End If
                        If DtTemp.Columns.Contains("Div_Code") = True Then
                            Dgl1.Item(Col1DivisionName, mGridRowNumber).Value = AgL.XNull(AgL.Dman_Execute("Select Div_Name From Division WHERE Div_Code = '" & AgL.XNull(DtTemp.Rows(I)("Div_Code")) & "'", AgL.GCn).ExecuteScalar)
                        End If
                        If DtTemp.Columns.Contains("SubGroupType") = True Then
                            Dgl1.Item(Col1VoucherType, mGridRowNumber).Value = AgL.XNull(DtTemp.Rows(I)("SubGroupType"))
                        End If


                        'Dgl1.Item(Col1SiteName, mGridRowNumber).Value = AgL.XNull(DtTemp.Rows(I)("SiteName"))
                        'Dgl1.Item(Col1DivisionName, mGridRowNumber).Value = AgL.XNull(DtTemp.Rows(I)("DivisionName"))
                        'Dgl1.Item(Col1VoucherType, mGridRowNumber).Value = AgL.XNull(DtTemp.Rows(I)("VoucherType"))

                        If Dgl1.Item(Col1FieldName, mGridRowNumber).Value.ToString().Contains("Password") Then
                            Dgl1.Item(Col1Value, mGridRowNumber).Tag = Dgl1.Item(Col1Value, mGridRowNumber).Value
                            Dgl1.Item(Col1Value, mGridRowNumber).Value = ""
                            Dgl1.Item(Col1Value, mGridRowNumber).Value = New String("*", Len(Dgl1.Item(Col1Value, mGridRowNumber).Tag))
                        End If



                        mGridRowNumber += 1

                    End If
                Next
            Next
            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        End If
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Value
                    If AgL.StrCmp(Dgl1.Item(Col1DataType, Dgl1.CurrentCell.RowIndex).Value, "Bit") Then
                        If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select 1 As Code, 'True' As Name 
                            UNION ALL 
                            Select 0 As Code, 'False' As Name "
                            Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                    Else
                        If Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value IsNot Nothing And Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                            If Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                                mQry = "Select " & GetCodeColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As Code, 
                                           " & GetDescriptionColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As  Description 
                                            From  " + Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value
                                Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                            End If
                        End If
                    End If

                    FGetOtherHelpLists()

                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag
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

            If Dgl1.Item(Col1SelectionType, Dgl1.CurrentCell.RowIndex).Value <> "Multi" Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1BtnSelection
                    If Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value IsNot Nothing And Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        mQry = "Select 'o' As Tick, " & GetCodeColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As Code, 
                                " & GetDescriptionColumns(Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value) & " As Description 
                                From  " + Dgl1.Item(Col1ReferenceTable, Dgl1.CurrentCell.RowIndex).Value
                    ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("Filter") And
                            Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("Nature") Then
                        mQry = "Select Distinct 'o' As Tick, Nature As Code, 
                                    Nature As Description 
                                    From  AcGroup Where Nature Is Not Null "
                    ElseIf Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("Filter") And
                            Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Value.ToString.Contains("ItemV_Type") Then
                        mQry = "Select Distinct 'o' As Tick, Nature As Code, 
                                    Nature As Description 
                                    From  AcGroup Where Nature Is Not Null "
                    End If

                    FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).Tables(0)), "", 500, 600, , , False)
                    FRH_Multiple.ChkAll.Visible = False
                    FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
                    FRH_Multiple.FFormatColumn(1, , 0, , False)
                    FRH_Multiple.FFormatColumn(2, "Description", 400, DataGridViewContentAlignment.MiddleLeft)
                    FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
                    FRH_Multiple.ShowDialog()

                    If FRH_Multiple.BytBtnValue = 0 Then
                        Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Tag = "+" + FRH_Multiple.FFetchData(1, "", "", "+", True)
                        Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value = "+" + FRH_Multiple.FFetchData(2, "", "", "+")
                    End If

                    Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
                    ProcSave(Dgl1.Item(Col1TableName, mRowIndex).Value,
                             Dgl1.Item(Col1PrimaryKey, mRowIndex).Value,
                             Dgl1.Item(Col1Code, mRowIndex).Value,
                             Dgl1.Item(Col1FieldName, mRowIndex).Value, Dgl1.Item(Col1Value, mRowIndex).Tag)


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

    Private Sub FrmSettings_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(DiscountCalculationPattern)), AgL.GCn)
                Case "BarcodePattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(BarcodePattern)), AgL.GCn)
                Case "BarcodeType"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(BarcodeType)), AgL.GCn)
                Case "DiscountSuggestionPattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(DiscountSuggestPattern)), AgL.GCn)
                Case "IndustryType"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(IndustryType)), AgL.GCn)
                Case "PlaceOfSupplay"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(PlaceOfSupplay)), AgL.GCn)
                Case "SaleInvoicePattern"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(SaleInvoicePattern)), AgL.GCn)
                Case "SubgroupRegistrationType"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(SubgroupRegistrationType)), AgL.GCn)
                Case "ActionOnDuplicateItem"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(ActionOnDuplicateItem)), AgL.GCn)
                Case "ActionIfCreditLimitExceeds"
                    Dgl1.Item(Col1FieldName, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(ClsMain.GetStringsFromClassConstants(GetType(ActionIfCreditLimitExceeds)), AgL.GCn)
            End Select
        End If
    End Sub

End Class