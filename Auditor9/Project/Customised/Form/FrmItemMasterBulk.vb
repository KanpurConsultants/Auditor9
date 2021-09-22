Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmItemMasterBulk
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const ColSNo As String = "S.No."
    Protected Const Col1SearchCode As String = "Search Code"
    Protected Const Col1ItemType As String = "Item Type"
    Protected Const Col1ItemName As String = "Item Name"
    Protected Const Col1ItemCategory As String = "Item Category"
    Protected Const Col1ItemGroup As String = "Item Group"
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group"
    Protected Const Col1PurchaseRate As String = "Purchase Rate"
    Protected Const Col1SaleRate As String = "Sale Rate"
    Protected Const Col1HSNCode As String = "HSN Code"
    Protected Const Col1Division As String = "Division"



    Dim mQry As String = ""

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String
    Dim DtItemTypeSetting As DataTable
    Dim DtRateTypes As DataTable

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub Ini_Grid()
        Dim I As Integer = 0
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 50, 5, ColSNo, True, True, False, , DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SearchCode, 140, 0, Col1SearchCode, False, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1ItemType, 150, 0, Col1ItemType, False, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1ItemName, 300, 0, Col1ItemName, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 80, 0, Col1ItemCode, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Specification, 150, 0, Col1Specification, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Unit, 80, 0, Col1Unit, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 70, 0, Col1SalesTaxGroup, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgNumberColumn(Dgl1, Col1PurchaseRate, 60, 10, 0, False, Col1PurchaseRate,,,,, DataGridViewColumnSortMode.Automatic)
            .AddAgNumberColumn(Dgl1, Col1SaleRate, 60, 10, 0, False, Col1SaleRate,,,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1HSNCode, 80, 0, Col1HSNCode, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Division, 70, 0, Col1Division, True, False,,, DataGridViewColumnSortMode.Automatic)

            For I = 0 To DtRateTypes.Rows.Count - 1
                .AddAgNumberColumn(Dgl1, AgL.XNull(DtRateTypes.Rows(I)("Description")), 60, 10, 0, False, AgL.XNull(DtRateTypes.Rows(I)("Description")),,,,, DataGridViewColumnSortMode.Automatic)
            Next



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



        Dgl1.BackgroundColor = Color.White
        Dgl1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        For I = 0 To Dgl1.Columns.Count - 1
            Dgl1.Columns(I).ContextMenuStrip = MnuOptions
        Next
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        DtRateTypes = AgL.FillData("Select Code, Description From RateType ", AgL.GCn).Tables(0)
        Ini_Grid()
        MovRec()
        Me.WindowState = FormWindowState.Maximized


    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name

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


    Private Sub ProcRunQuries(mQryList As String)
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

            Dim mQryArr As String() = mQryList.Split(";")

            For I = 0 To mQryArr.Length - 1
                AgL.Dman_ExecuteNonQry(mQryArr(I), AgL.GCn, AgL.ECmd)
            Next


            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcSaveRate(TableName As String, PrimaryKey As String, Code As String, FieldName As String, Value As Object, Division As String, RateType As String)
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

            If RateType = "" Then
                mQry = "UPDATE " + TableName + " Set " + FieldName + " = " + "'" + Value + "'" + " Where " & PrimaryKey & " = " + "'" + Code + "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If AgL.Dman_Execute("Select Count(*) From RateList Where Code = '" & Code & "'", AgL.GCn).ExecuteScalar() = 0 Then
                mQry = " INSERT INTO RateList(Code, WEF, RateType, EntryBy, EntryDate, EntryType, " &
                        " EntryStatus, Status, Div_Code) " &
                        " VALUES (" & AgL.Chk_Text(Code) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ",	" &
                        " NULL,	" & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " &
                        " " & AgL.Chk_Text("E") & ", 'Open', " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                        " '" & Division & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If AgL.Dman_Execute("Select Count(*) From RateListDetail Where Code = '" & Code & "' 
                    And IfNUll(RateType,'') = '" & RateType & "' ", AgL.GCn).ExecuteScalar() = 0 Then
                mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) " &
                              " VALUES (" & AgL.Chk_Text(Code) & ", " &
                              " 0, " & AgL.Chk_Date(AgL.PubStartDate) & ", " &
                              " " & AgL.Chk_Text(Code) & ", " &
                              " " & AgL.Chk_Text(RateType) & ", " & Val(Value) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = " UPDATE RateListDetail Set Rate = " & Val(Value) & "
                        Where Code = '" & Code & "'
                        And Item = '" & Code & "'
                        And IfNUll(RateType,'') = " & AgL.Chk_Text(RateType) & " "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

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
                Case Col1ItemCategory
                    If Dgl1.Item(Col1ItemCategory, mRowIndex).Value = "" Or Dgl1.Item(Col1ItemCategory, mRowIndex).Value = Nothing Then
                        MsgBox("Item Categpry is a required fields.It Can not have blank value.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    Dim mNewProductName As String = GetProductName(mRowIndex)
                    mQry = "Select count(*) From Item Where Description='" & mNewProductName & "' And Code <> '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "' "
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                        MsgBox("Description Already Exist.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    Dgl1.Item(Col1ItemName, mRowIndex).Value = mNewProductName
                    Dgl1.Item(Col1ItemName, mRowIndex).Style.BackColor = Color.BurlyWood
                    mQry = "UPDATE Item Set ItemCategory = '" & Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag & "' 
                            Where Code = '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "'; "
                    mQry += "UPDATE Item Set Description = '" & Dgl1.Item(Col1ItemName, mRowIndex).Value & "' 
                            Where Code = '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "' "
                    ProcRunQuries(mQry)
                Case Col1ItemGroup
                    If Dgl1.Item(Col1ItemGroup, mRowIndex).Value = "" Or Dgl1.Item(Col1ItemGroup, mRowIndex).Value = Nothing Then
                        MsgBox("Item Group is a required fields.It Can not have blank value.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    Dim mNewProductName As String = GetProductName(mRowIndex)
                    mQry = "Select count(*) From Item Where Description='" & mNewProductName & "' And Code <> '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "' "
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                        MsgBox("Description Already Exist.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    Dgl1.Item(Col1ItemName, mRowIndex).Value = mNewProductName
                    Dgl1.Item(Col1ItemName, mRowIndex).Style.BackColor = Color.BurlyWood
                    mQry = "UPDATE Item Set ItemGroup = '" & Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag & "' 
                            Where Code = '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "'; "
                    mQry += "UPDATE Item Set Description = '" & Dgl1.Item(Col1ItemName, mRowIndex).Value & "' 
                            Where Code = '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "' "
                    ProcRunQuries(mQry)
                Case Col1ItemCode
                    If Dgl1.Item(Col1ItemCode, mRowIndex).Value = "" Or Dgl1.Item(Col1ItemCode, mRowIndex).Value = Nothing Then
                        MsgBox("Code is a required fields.It Can not have blank value.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    mQry = "Select count(*) From Item Where ManualCode='" & Dgl1.Item(Col1ItemCode, mRowIndex).Value & "' And Code <> '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "' "
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                        MsgBox("Short Name Already Exist.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    ProcSave("Item", "Code", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Code",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1Specification
                    If Dgl1.Item(Col1Specification, mRowIndex).Value = "" Or Dgl1.Item(Col1Specification, mRowIndex).Value = Nothing Then
                        MsgBox("Specification is a required fields.It Can not have blank value.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    Dim mNewProductName As String = GetProductName(mRowIndex)
                    mQry = "Select count(*) From Item Where Description='" & mNewProductName & "' And Code <> '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "' "
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                        MsgBox("Description Already Exist.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    Dgl1.Item(Col1ItemName, mRowIndex).Value = mNewProductName
                    Dgl1.Item(Col1ItemName, mRowIndex).Style.BackColor = Color.BurlyWood
                    mQry = "UPDATE Item Set Specification = '" & Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value & "' 
                            Where Code = '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "'; "
                    mQry += "UPDATE Item Set Description = '" & Dgl1.Item(Col1ItemName, mRowIndex).Value & "' 
                            Where Code = '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "' "
                    ProcRunQuries(mQry)
                Case Col1Unit
                    ProcSave("Item", "Code", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Unit",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1SalesTaxGroup
                    If Dgl1.Item(Col1SalesTaxGroup, mRowIndex).Value = "" Or Dgl1.Item(Col1SalesTaxGroup, mRowIndex).Value = Nothing Then
                        MsgBox("Sales Tax Group is a required fields.It Can not have blank value.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    ProcSave("Item", "Code", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "SalesTaxPostingGroup",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1PurchaseRate
                    ProcSave("Item", "Code", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "PurchaseRate",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1SaleRate
                    ProcSaveRate("Item", "Code", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Rate",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value,
                            Dgl1.Item(Col1Division, mRowIndex).Tag, "")
                Case Col1HSNCode
                    ProcSave("Item", "Code", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "HSN",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1Division
                    If Dgl1.Item(Col1Division, mRowIndex).Value = "" Or Dgl1.Item(Col1Division, mRowIndex).Value = Nothing Then
                        MsgBox("Division is a required fields.It Can not have blank value.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    ProcSave("Item", "Code", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Div_Code",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
            End Select


            For I = 0 To DtRateTypes.Rows.Count - 1
                If AgL.StrCmp(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, DtRateTypes.Rows(I)("Description")) Then
                    ProcSaveRate("Item", "Code", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Rate",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value,
                            Dgl1.Item(Col1Division, mRowIndex).Tag, DtRateTypes.Rows(I)("Code"))
                End If
            Next

            Dgl1.CurrentCell.Style.BackColor = Color.BurlyWood
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub MovRec()
        GetItemDetail()
    End Sub
    Public Sub GetItemDetail()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim DtRows_RateList As DataRow()

        mQry = "Select Rt.Description, L.Rate, L.Item
                From RateListDetail L 
                LEFT JOIN RateType Rt On L.RateType = Rt.Code
                Where RateType Is Not Null "
        Dim DtRateList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select I.*, Ig.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, " &
                " IT.Name AS ItemTypeName, D.Div_Name " &
                " From Item I " &
                " LEFT JOIN ItemGroup Ig ON I.ItemGroup = IG.Code " &
                " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                " LEFT JOIN Division D On I.Div_Code = D.Div_Code " &
                " Where I.Div_Code = '" & AgL.PubDivCode & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                Dgl1.Item(Col1SearchCode, I).Value = AgL.XNull(DtTemp.Rows(I)("Code"))
                Dgl1.Item(Col1ItemType, I).Tag = AgL.XNull(DtTemp.Rows(I)("ItemType"))
                Dgl1.Item(Col1ItemType, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemTypeName"))
                Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(DtTemp.Rows(I)("ItemCategory"))
                Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemCategoryDesc"))
                Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(DtTemp.Rows(I)("ItemGroup"))
                Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(DtTemp.Rows(I)("ManualCode"))
                Dgl1.Item(Col1ItemName, I).Value = AgL.XNull(DtTemp.Rows(I)("Description"))
                Dgl1.Item(Col1Specification, I).Value = AgL.XNull(DtTemp.Rows(I)("Specification"))
                Dgl1.Item(Col1Unit, I).Value = AgL.XNull(DtTemp.Rows(I)("Unit"))
                Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(DtTemp.Rows(I)("SalesTaxPostingGroup"))
                Dgl1.Item(Col1PurchaseRate, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchaseRate"))
                Dgl1.Item(Col1SaleRate, I).Value = AgL.XNull(DtTemp.Rows(I)("Rate"))
                Dgl1.Item(Col1HSNCode, I).Value = AgL.XNull(DtTemp.Rows(I)("Hsn"))
                Dgl1.Item(Col1Division, I).Tag = AgL.XNull(DtTemp.Rows(I)("Div_Code"))
                Dgl1.Item(Col1Division, I).Value = AgL.XNull(DtTemp.Rows(I)("Div_Name"))

                DtRows_RateList = DtRateList.Select("Item = '" & AgL.XNull(DtTemp.Rows(I)("Code")) & "'")

                For J As Integer = 0 To DtRows_RateList.Length - 1
                    Dgl1.Item(AgL.XNull(DtRows_RateList(J)("Description")), I).Value = AgL.VNull(DtRows_RateList(J)("Rate"))
                Next

                'mQry = "Select Rt.Description, L.Rate
                '        From RateListDetail L 
                '        LEFT JOIN RateType Rt On L.RateType = Rt.Code
                '        Where L.Item = '" & AgL.XNull(DtTemp.Rows(I)("Code")) & "' 
                '        And RateType Is Not Null "
                'Dim DtRateList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                'For J As Integer = 0 To DtRateList.Rows.Count - 1
                '    Dgl1.Item(AgL.XNull(DtRateList.Rows(J)("Description")), I).Value = AgL.VNull(DtRateList.Rows(J)("Rate"))
                'Next

            Next
            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
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

            FGetItemTypeSetting(Dgl1.Item(Col1ItemType, bRowIndex).Tag, Dgl1.Item(Col1Division, bRowIndex).Tag)

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemCategory
                    If Dgl1.AgHelpDataSet(Col1ItemCategory) Is Nothing Then
                        mQry = "SELECT IC.Code, IC.Description, IC.ItemType, IT.Name as ItemTypeName, IC.SalesTaxGroup, IC.Unit, IC.Hsn 
                                    FROM ItemCategory IC 
                                    Left Join ItemType IT On IC.ItemType = IT.Code 
                                    Where IfNull(IT.Parent, IT.Code) in ('TP','" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "') Order by IC.Description  "
                        Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1ItemGroup
                    If Dgl1.AgHelpDataSet(Col1ItemGroup) Is Nothing Then
                        If DtItemTypeSetting.Rows.Count > 0 Then
                            If DtItemTypeSetting.Rows(0)("IsItemGroupLinkedWithItemCategory") Then
                                mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " &
                                            " From ItemGroup I " &
                                            " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                                            " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                                            " WHERE I.ItemCategory='" & Dgl1.Item(Col1ItemCategory, bRowIndex).Tag & "' And I.ItemType = '" & Dgl1.Item(Col1ItemType, bRowIndex).Tag & "' "
                                Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
                            Else
                                mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " &
                                            " From ItemGroup I " &
                                            " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                                            " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                                            " WHERE  I.ItemType = '" & Dgl1.Item(Col1ItemType, bRowIndex).Tag & "' "
                                Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
                            End If
                        Else
                            mQry = " Select I.Code As Code, I.Description As ItemGroup " &
                                            " From ItemGroup I " &
                                            " WHERE  1=2 "
                            Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Unit
                    If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                        mQry = "SELECT Code, Code AS Unit FROM Unit "
                        Dgl1.AgHelpDataSet(Col1Unit) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1SalesTaxGroup
                    If Dgl1.AgHelpDataSet(Col1SalesTaxGroup) Is Nothing Then
                        mQry = "SELECT Description as  Code, Description AS PostingGroupSalesTaxItem FROM PostingGroupSalesTaxItem "
                        Dgl1.AgHelpDataSet(Col1SalesTaxGroup) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Division
                    If Dgl1.AgHelpDataSet(Col1Division) Is Nothing Then
                        mQry = "Select Div_Code, Div_Name  From Division "
                        Dgl1.AgHelpDataSet(Col1Division) = AgL.FillData(mQry, AgL.GCn)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub

    Private Sub FrmReportWindow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub FGetItemTypeSetting(bItemType As String, bDivision As String)
        mQry = "Select * From ItemTypeSetting Where ItemType = '" & bItemType & "' And Div_Code = '" & bDivision & "' "
        DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
        If DtItemTypeSetting.Rows.Count = 0 Then
            mQry = "Select * From ItemTypeSetting Where ItemType = '" & bItemType & "' And Div_Code Is Null "
            DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
            If DtItemTypeSetting.Rows.Count = 0 Then
                mQry = "Select * From ItemTypeSetting Where ItemType Is Null And Div_Code Is Null "
                DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
            End If
        End If
    End Sub
    Private Function GetProductName(mRowIndex As Integer)
        Dim mProductName As String = Dgl1.Item(Col1Specification, mRowIndex).Value + Space(10) +
            "[" + Dgl1.Item(Col1ItemGroup, mRowIndex).Value + " | " + Dgl1.Item(Col1ItemCategory, mRowIndex).Value + "]"
        Return mProductName
    End Function
    Private Sub MnuExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuExportToExcel.Click, MnuFreezeColumns.Click
        Dim FileName As String = ""
        Dim bColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Select Case sender.Name
            Case MnuExportToExcel.Name
                If MsgBox("Want to Export Grid Data", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Export Grid?...") = vbNo Then Exit Sub
                FileName = AgControls.Export.GetFileName(My.Computer.FileSystem.SpecialDirectories.Desktop)
                If FileName.Trim <> "" Then
                    Call AgControls.Export.exportExcel(Dgl1, FileName, Dgl1.Handle)
                End If

            Case MnuFreezeColumns.Name
                If MnuFreezeColumns.Checked Then
                    Dgl1.Columns(bColumnIndex).Frozen = True
                Else
                    For I As Integer = 0 To bColumnIndex
                        Dgl1.Columns(I).Frozen = False
                    Next
                End If
        End Select
    End Sub
End Class