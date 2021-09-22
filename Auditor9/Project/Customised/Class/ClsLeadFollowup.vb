Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports CrystalDecisions.CrystalReports.Engine
Imports Microsoft.Reporting.WinForms
Public Class ClsLeadFollowup

    Enum ShowDataIn
        Grid = 1
        Crystal = 2
    End Enum

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

    Public Shared mHelpStatusQry$ = " Select 'o' As Tick, 'Cold' as Code, 'Cold' as Description
                    Union All Select 'o' As Tick, 'Warm' as Code, 'Warm' as Description
                    Union All Select 'o' As Tick, 'Hot' as Code, 'Hot' as Description
                    Union All Select 'o' As Tick, 'Close' as Code, 'Close' as Description
                    Union All Select 'o' As Tick, 'Lost' as Code, 'Lost' as Description "

    Public Shared mHelpActionQry$ = " Select 'o' As Tick, 'Phone' as Code, 'Phone' as Description
                    Union All Select 'o' As Tick, 'Visit' as Code, 'Visit' as Description "



    Public Const Col1SearchCode As String = "Search Code"

    Dim rowDivision As Integer = 0
    Dim rowSite As Integer = 1
    Dim rowStatus As Integer = 2
    Dim rowLastActionTook As Integer = 3
    Dim rowNextActionToBeTaken As Integer = 4
    Dim rowRecordType As Integer = 5

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

    Public Sub Ini_Grid()
        Dim mDefaultValue As String = ""
        Try
            Dim mQry As String
            Dim I As Integer = 0

            mDefaultValue = ClsMain.FGetSettings(ClsMain.SettingFields.DefaultDivisionNameInReportFilters, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If mDefaultValue = "All" Then
                mDefaultValue = "All"
            Else
                mDefaultValue = "[DIVISIONCODE]"
            End If
            mQry = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, mDefaultValue)
            If AgL.PubDivisionCount = 1 Then ReportFrm.FilterGrid.Rows(rowDivision).Visible = False

            mDefaultValue = ClsMain.FGetSettings(ClsMain.SettingFields.DefaultSiteNameInReportFilters, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If mDefaultValue = "All" Then
                mDefaultValue = "All"
            Else
                mDefaultValue = "[SITECODE]"
            End If
            mQry = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, mDefaultValue)

            ReportFrm.CreateHelpGrid("Current Status", "Current Status", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStatusQry)
            ReportFrm.CreateHelpGrid("Last Action Took", "Last Action Took", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpActionQry)
            ReportFrm.CreateHelpGrid("Next Action To Be Taken", "Next Action To Be Taken", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpActionQry)

            mQry = " Select 'Not Started' as Code, 'Not Started' as Description
                    Union All Select  'Pending To Followup' as Code, 'Pending To Followup' as Description
                    Union All Select 'Started' as Code, 'Started' as Description
                    Union All Select 'All' as Code, 'All' as Description
                     "
            ReportFrm.CreateHelpGrid("Record Type", "Record Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Pending To Followup")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcFillReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
        ReportFrm.ClsRep = Me
    End Sub
    Public Sub ProcFillReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            RepTitle = ""

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                End If
            End If

            mCondStr = " Where 1=1 "
            If ReportFrm.FGetText(rowStatus) <> "All" Then
                mCondStr = mCondStr & " AND L.Status In ('" & ReportFrm.FGetText(rowStatus).ToString.Replace(",", "','") & "')"
            End If


            If ReportFrm.FGetText(rowLastActionTook) <> "All" Then
                mCondStr = mCondStr & " AND L.CurrentAction In ('" & ReportFrm.FGetText(rowLastActionTook).ToString.Replace(",", "','") & "')"
            End If


            If ReportFrm.FGetText(rowNextActionToBeTaken) <> "All" Then
                mCondStr = mCondStr & " AND L.NextAction In ('" & ReportFrm.FGetText(rowNextActionToBeTaken).ToString.Replace(",", "','") & "')"
            End If


            If ReportFrm.FGetText(rowRecordType) = "Not Started" Then
                mCondStr = mCondStr & " AND L.Id is Null "
            ElseIf ReportFrm.FGetText(rowRecordType) = "Started" Then
                mCondStr = mCondStr & " AND L.Id is Not Null "
            ElseIf ReportFrm.FGetText(rowRecordType) = "Pending To Followup" Then
                mCondStr = mCondStr & " AND (L.NextDate Is Null or L.NextDate <= " & AgL.Chk_Date(AgL.PubLoginDate) & ") "
                mCondStr = mCondStr & " AND (L.Status Is Null or L.Status Not In ('Lost','Closed')) "
            ElseIf ReportFrm.FGetText(rowRecordType) = "Lost" Then
                mCondStr = mCondStr & " AND L.Status ='Lost' "
            ElseIf ReportFrm.FGetText(rowRecordType) = "Closed" Then
                mCondStr = mCondStr & " AND L.Status ='Closed' "
            End If


            mQry = "SELECT H.Code AS SearchCode, H.Name, H.Address, H.Mobile, H.Email, H.Remark, L.Status 
                    FROM Lead H 
                    LEFT JOIN (
                            Select La.LeadCode, Max(La.Id) As LeadActivityId
                            From LeadActivity La 
                            Group By La.LeadCode
                    ) As VLastActivity On H.Code = VLastActivity.LeadCode 
                    LEFT JOIN LeadActivity L On VLastActivity.LeadActivityId = L.Id " & mCondStr &
                    " ORDER BY H.Name "
            DsHeader = AgL.FillData(mQry, AgL.GCn)


            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")
            ReportFrm.Text = "Lead Followup"

            ReportFrm.ReportProcName = "ProcFillReport"

            ReportFrm.ProcFillGrid(DsHeader)
            ReportFrm.DGL1.MultiSelect = False
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        Finally
            ReportFrm.DGL2.Visible = False
        End Try
    End Sub
    Private Sub ReportFrm_Dgl1KeyDown(sender As Object, e As KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        If e.KeyCode = Keys.F2 Then
            ShowFrmCustomerPaymenFollowup()
        End If
    End Sub
    Private Sub ShowFrmCustomerPaymenFollowup()

        Try
            Dim mPartyCode As String
            Dim mObj As FrmLeadFollowup
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            mPartyCode = AgL.XNull(ReportFrm.DGL1.Item("Search Code", ReportFrm.DGL1.CurrentCell.RowIndex).Value)
            If ReportFrm.DGL1.Rows(ReportFrm.DGL1.CurrentCell.RowIndex).Tag IsNot Nothing Then
                mObj = ReportFrm.DGL1.Rows(ReportFrm.DGL1.CurrentCell.RowIndex).Tag
                mObj.EntryMode = "Edit"
            Else
                mObj = New FrmLeadFollowup
                mObj.StartPosition = FormStartPosition.CenterScreen
                'mObj.Top = 100
                mObj.EntryMode = "Add"
                mObj.LeadCode = AgL.XNull(ReportFrm.DGL1.Item("Search Code", ReportFrm.DGL1.CurrentCell.RowIndex).Value)
                mObj.IniGrid()
                mObj.FMoverec(AgL.XNull(ReportFrm.DGL1.Item("Search Code", ReportFrm.DGL1.CurrentCell.RowIndex).Value))
            End If


            mObj.ShowDialog()
            If mObj.mOkButtonPressed Then
                'Dim i As Integer
                'Dim mRow As Integer = ReportFrm.DGL1.CurrentCell.RowIndex
                'Dim mCol As Integer = ReportFrm.DGL1.CurrentCell.ColumnIndex
                'ReportFrm.DGL1.CurrentCell = Nothing
                'For i = 0 To ReportFrm.DGL1.Rows.Count - 1
                '    If AgL.XNull(ReportFrm.DGL1.Item("Party Code", i).Value) = mPartyCode Then
                '        ReportFrm.DGL1.Rows(i).Visible = False
                '    End If
                'Next
                'ReportFrm.DGL1.CurrentCell = ReportFrm.DGL1.FirstDisplayedCell ' ReportFrm.DGL1(mRow, mCol)
                'ReportFrm.DGL1.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
