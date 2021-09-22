Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsItemMasterBulk
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""


    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0
    Dim DtRateTypes As DataTable

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4


    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Dim rowItemCategory As Integer = 0
    Dim rowItemGroup As Integer = 1
    Dim rowShowOnlyZeroRate As Integer = 2

    Protected Const Col1SearchCode As String = "Search Code"
    Protected Const Col1Division As String = "Division"
    Protected Const Col1SaleRate As String = "Sale Rate"
    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property
    Public Property ShowReportType() As String
        Get
            ShowReportType = mShowReportType
        End Get
        Set(ByVal value As String)
            mShowReportType = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("ItemCategory", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
            ReportFrm.CreateHelpGrid("ItemGroup", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
            ReportFrm.CreateHelpGrid("ShowOnlyZeroRate", "Show Only Zero Rate", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "Yes")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcItemMasterBulk()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
        DtRateTypes = AgL.FillData("Select Code, Description From RateType ", AgL.GCn).Tables(0)
    End Sub
    Public Sub ProcItemMasterBulk(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"



            RepTitle = "Item Master Bulk"


            mCondStr = " Where 1=1  "
            mCondStr = mCondStr & "And I.V_Type In ('" & ItemV_Type.Item & "','" & ItemV_Type.SKU & "')  "
            mCondStr = mCondStr & "And I.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "','" & ItemTypeCode.InternalProduct & "')"
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", rowItemCategory)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", rowItemGroup)

            If ReportFrm.FilterGrid.Item(GFilter, rowShowOnlyZeroRate).Value = "Yes" Then
                mCondStr = mCondStr & "And IfNull(I.Rate,0) = 0 "
            End If


            Dim bQry As String = ""
            bQry = "SELECT L.Item As ItemCode "
            For I As Integer = 0 To DtRateTypes.Rows.Count - 1
                bQry += ", Max(Case When L.RateType = '" & AgL.XNull(DtRateTypes.Rows(I)("Code")) & "' Then L.Rate Else 0 End) As [" & AgL.XNull(DtRateTypes.Rows(I)("Description")) & "]"
            Next
            bQry += " FROM RateListDetail L 
                    LEFT JOIN RateType Rt ON L.RateType = Rt.Code
                    LEFT JOIN Item I ON L.Item = I.Code 
                    " & mCondStr &
                    " Group By L.Item "


            mQry = " SELECT I.Code AS SearchCode, I.Div_Code As Division, 
                    Ic.Description AS ItemCategory, Ig.Description AS ItemGroup, I.Specification AS Item, 
                    D1.Description as Dimension1, D2.Description as Dimension2,
                    D3.Description as Dimension3, D4.Description as Dimension4, Size.Description as Size,
                    I.Rate As SaleRate, ItemRateList.* 
                    FROM Item I 
                    Left Join Item IC On I.ItemCategory = IC.Code
                    Left Join Item IG On I.ItemGroup = IG.Code
                    LEFT JOIN Item D1 ON I.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON I.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON I.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON I.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON I.Size = Size.Code
                    LEFT JOIN (" & bQry & ") As ItemRateList On I.Code = ItemRateList.ItemCode
                    " & mCondStr
            DsHeader = AgL.FillData(mQry, AgL.GCn)


            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Item Master Bulk"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcItemMasterBulk"

            ReportFrm.IsHideZeroColumns = True

            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.DGL1.Columns("Item Code").Visible = False
            ReportFrm.DGL1.Columns("Division").Visible = False

            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next

            ReportFrm.DGL1.Columns(Col1SaleRate).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1SaleRate).Visible = True


            For I As Integer = 0 To DtRateTypes.Rows.Count - 1
                For J As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                    If AgL.StrCmp(ReportFrm.DGL1.Columns(J).Name, DtRateTypes.Rows(I)("Description")) Then
                        ReportFrm.DGL1.Columns(ReportFrm.DGL1.Columns(J).Name).ReadOnly = False
                    End If
                Next
            Next


            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
                ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
                ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
            Next

            ReportFrm.DGL2.Visible = False

            AgL.FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1, AgL)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
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
                mQry = "UPDATE " + TableName + " Set " + FieldName + " = " + "'" + Value.ToString + "'" + " Where " & PrimaryKey & " = " + "'" + Code + "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If AgL.Dman_Execute("Select Count(*) From RateList With (NoLock) Where Code = '" & Code & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then
                mQry = " INSERT INTO RateList(Code, WEF, EntryBy, EntryDate, EntryType, " &
                        " EntryStatus, Status, Div_Code) " &
                        " VALUES (" & AgL.Chk_Text(Code) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ",	" &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " &
                        " " & AgL.Chk_Text("E") & ", 'Open', " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                        " '" & Division & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If AgL.Dman_Execute("Select Count(*) From RateListDetail With (NoLock) Where Code = '" & Code & "' 
                    And IfNUll(RateType,'') = '" & RateType & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then

                Dim bMaxSr As Integer = AgL.VNull(AgL.Dman_Execute("Select IfNull(Max(Sr),0) + 1 From RateListDetail
                                Where Code = '" & Code & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

                mQry = "INSERT INTO RateListDetail(Code, Sr, Item, RateType, Rate) " &
                              " VALUES (" & AgL.Chk_Text(Code) & ", " &
                              " " & bMaxSr & ", " &
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
    Private Sub ReportFrm_DGL1EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles ReportFrm.DGL1EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            mColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex
            If ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name
                Case Col1SaleRate
                    ProcSaveRate("Item", "Code", ReportFrm.DGL1.Item(Col1SearchCode, mRowIndex).Value, "Rate",
                            ReportFrm.DGL1.Item(ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name, mRowIndex).Value,
                            ReportFrm.DGL1.Item(Col1Division, mRowIndex).Tag, "")
            End Select

            For I = 0 To DtRateTypes.Rows.Count - 1
                If AgL.StrCmp(ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name, DtRateTypes.Rows(I)("Description")) Then
                    ProcSaveRate("Item", "Code", ReportFrm.DGL1.Item(Col1SearchCode, mRowIndex).Value, "Rate",
                            ReportFrm.DGL1.Item(ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name, mRowIndex).Value,
                            ReportFrm.DGL1.Item(Col1Division, mRowIndex).Tag, DtRateTypes.Rows(I)("Code"))
                End If
            Next

            ReportFrm.DGL1.CurrentCell.Style.BackColor = Color.BurlyWood
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
