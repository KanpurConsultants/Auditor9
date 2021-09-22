Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsTdsParameters

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""


    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4


    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Public Const Col1TdsCategoryCode As String = "Tds Category Code"
    Public Const Col1TdsCategory As String = "Tds Category"
    Public Const Col1TdsGroupCode As String = "Tds Group Code"
    Public Const Col1TdsGroup As String = "Tds Group"
    Public Const Col1TdsMonthlyLimit As String = "Tds Monthly Limit"
    Public Const Col1TdsYearlyLimit As String = "Tds Yearly Limit"
    Public Const Col1TdsPer As String = "Tds Per"
    Public Const Col1LedgerAccount As String = "Ledger Account"
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
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name FROM ItemType "
    Dim mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
    Dim mHelpTagQry$ = "Select Distinct 'o' As Tick, H.Tags as Code, H.Tags as Description  FROM PurchInvoiceDetail H "
    Public Sub Ini_Grid()
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcTdsParameters()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcTdsParameters(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mPurchCondStr$ = ""
            Dim mLedgerHeadCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"



            RepTitle = "Tds Parameters"


            mQry = "SELECT H.TdsCategoryCode, Tc.Description AS TdsCategory, H.TdsGroupCode, Tg.Description AS TdsGroup, 
                    Tp.TdsMonthlyLimit, Tp.TdsYearlyLimit, Tp.TdsPer, Sg.Name AS LedgerAccount
                    FROM (SELECT T1.Code AS TdsCategoryCode, T2.Code AS TdsGroupCode
		                    FROM TdsCategory T1
		                    LEFT JOIN TdsGroup T2 ON 1=1) AS H 
                    LEFT JOIN TdsCategory Tc ON H.TdsCategoryCode = Tc.Code
                    LEFT JOIN TdsGroup Tg ON H.TdsGroupCode = Tg.Code
                    LEFT JOIN TdsParameters Tp ON H.TdsCategoryCode = Tp.TdsCategory AND H.TdsGroupCode = Tp.TdsGroup
                    LEFT JOIN Subgroup Sg ON Tp.LedgerAccount = Sg.Subcode "

            DsHeader = AgL.FillData(mQry, AgL.GCn)


            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Tds Parameters"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcTdsParameters"
            ReportFrm.InputColumnsStr = "|" + Col1TdsMonthlyLimit + "|" + "|" + Col1TdsYearlyLimit + "|" + "|" + Col1TdsPer + "|" + "|" + Col1LedgerAccount + "|"

            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.DGL1.Columns(Col1TdsCategoryCode).Visible = False
            ReportFrm.DGL1.Columns(Col1TdsGroupCode).Visible = False

            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                If ReportFrm.DGL1.Columns(I).Name <> Col1TdsMonthlyLimit And
                        ReportFrm.DGL1.Columns(I).Name <> Col1TdsYearlyLimit And
                        ReportFrm.DGL1.Columns(I).Name <> Col1TdsPer Then
                    ReportFrm.DGL1.Columns(I).ReadOnly = True
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        Finally
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
                ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
                ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
            Next
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet
        Try

            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) = True Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1LedgerAccount
                    mQry = " SELECT Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg WHERE SubgroupType = 'Ledger Account' "
                    DsTemp = AgL.FillData(mQry, AgL.GCn)
                    FSingleSelectForm(Col1LedgerAccount, bRowIndex, DsTemp)
                    FSave("LedgerAccount", ReportFrm.DGL1.Item(Col1LedgerAccount, bRowIndex).Tag, bRowIndex)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FSingleSelectForm(bColumnName As String, bRowIndex As Integer, bDataSet As DataSet)
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(CType(bDataSet, DataSet).Tables(0)), "", 500, 500, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Name", 400, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Tag = FRH_Single.DRReturn("Code")
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Value = FRH_Single.DRReturn("Name")
        End If
    End Sub
    Private Sub FSave(FieldName As String, Value As String, RowIndex As Integer)
        Dim mMaxId As String = ""
        mQry = "Select Count(*) From TdsParameters 
                Where TdsCategory = '" & ReportFrm.DGL1.Item(Col1TdsCategoryCode, RowIndex).Value & "'
                And TdsGroup = '" & ReportFrm.DGL1.Item(Col1TdsGroupCode, RowIndex).Value & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) > 0 Then
            mQry = " UPDATE TdsParameters Set " & FieldName & " = '" & Value & "'
                    Where TdsCategory = '" & ReportFrm.DGL1.Item(Col1TdsCategoryCode, RowIndex).Value & "'
                    And TdsGroup = '" & ReportFrm.DGL1.Item(Col1TdsGroupCode, RowIndex).Value & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Else
            mMaxId = AgL.GetMaxId("TdsParameters", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = " INSERT INTO TdsParameters (Code, TdsCategory, TdsGroup, " & FieldName & ", EntryBy, EntryDate)
                    SELECT '" & mMaxId & "' As Code, '" & ReportFrm.DGL1.Item(Col1TdsCategoryCode, RowIndex).Value & "' As TdsCategory, 
                    '" & ReportFrm.DGL1.Item(Col1TdsGroupCode, RowIndex).Value & "' As TdsGroup, 
                    '" & Value & "',
                    '" & AgL.PubUserName & "' AS EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " EntryDate "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub ReportFrm_DGL1EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles ReportFrm.DGL1EditingControl_Validating
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub
            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1TdsMonthlyLimit
                    FSave("TdsMonthlyLimit", ReportFrm.DGL1.Item(Col1TdsMonthlyLimit, bRowIndex).Value, bRowIndex)
                Case Col1TdsYearlyLimit
                    FSave("TdsYearlyLimit", ReportFrm.DGL1.Item(Col1TdsYearlyLimit, bRowIndex).Value, bRowIndex)
                Case Col1TdsPer
                    FSave("TdsPer", ReportFrm.DGL1.Item(Col1TdsPer, bRowIndex).Value, bRowIndex)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ReportFrm_Shown(sender As Object, e As EventArgs) Handles ReportFrm.Shown
        ProcTdsParameters()
    End Sub
End Class
