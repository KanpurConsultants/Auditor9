Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsCustomerDiscountReport

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

    Public Const Col1SearchCode As String = "Search Code"

    Dim rowSite As Integer = 0
    Dim rowParty As Integer = 1
    Dim rowItemGroup As Integer = 2
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
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.Code As Code, Sg.Name AS Party, Sg.Address FROM ViewHelpSubgroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.SubgroupType In ('Master Customer')  "
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
        Try
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry,, 600, 650, 300)
            ReportFrm.CreateHelpGrid("ItemGroup", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry,, 600, 600, 300)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcCustomerDiscountReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcCustomerDiscountReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Customer Discount Report"

            If ReportFrm.FGetText(rowParty) = "All" And ReportFrm.FGetText(rowItemGroup) = "All" Then
                MsgBox("Select Party Or Item Group.", MsgBoxStyle.Information)
                Exit Sub
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("VSubGroup.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("VSubGroup.SubCode", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("IG.Code", rowItemGroup)

            mQry = "SELECT Sg.Name AS Party, Agent.Name as AgentName, Rt.Description as RateType, Ig.Description AS ItemGroup, S.Name AS Site,
                    IfNull(Igp.DiscountPer, IfNull(H.DiscountPer,0)) As PcsLess,
                    IfNull(Igp.AdditionalDiscountPer, IfNull(H.AdditionalDiscountPer,0)) As Discount,
                    IfNull(Igp.AdditionPer, IfNull(H.AdditionPer,0)) As Addition
                    From SubgroupSiteDivisionDetail VSubGroup, ItemGroup IG                    
                    Left Join RateType Rt on VSubgroup.RateType = Rt.Code
                    LEFT JOIN ItemGroupRateType H  On VSubGroup.RateType = H.RateType And H.Code = IG.Code                    
                    LEFT JOIN ViewHelpSubgroup Sg ON VSubGroup.SubCode = Sg.Code
                    LEFT JOIN SiteMast S ON VSubGroup.Site_Code = S.Code
                    LEFT JOIN ItemGroupPerson Igp on Ig.Code = Igp.ItemGroup And Sg.Code = Igp.Person
                    Left Join viewHelpSubgroup Agent On VSubgroup.Agent = Agent.Code
                    WHERE VSubgroup.Site_Code = IG.Site_Code  and VSubgroup.RateType is Not Null " & mCondStr & " Order By Sg.Name "
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Customer Discount Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcCustomerDiscountReport"

            ReportFrm.ProcFillGrid(DsHeader)



        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        Finally
            ReportFrm.DGL2.Visible = False
        End Try
    End Sub
End Class
