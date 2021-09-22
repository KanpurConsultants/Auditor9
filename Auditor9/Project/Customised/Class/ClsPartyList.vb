Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsPartyList

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""

    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4


    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City Order By CityName "
    Dim mHelpSubGroupTypeQry$ = "Select 'o' As Tick, SubgroupType as Code, SubgroupType as Name FROM SubgroupType Sg Where IfNull(IsCustomUI,0)=0 Order By SubgroupType  "
    Dim mHelpAgentQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name, Sg.SubgroupType FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('" & SubgroupType.PurchaseAgent & "','" & SubgroupType.SalesAgent & "')  "


    Dim mShowReportType As String = ""
    Dim DsHeader As DataSet = Nothing

    Dim rowAccountType As Integer = 0
    Dim rowCity As Integer = 1
    Dim rowAgent As Integer = 2

    Public Col1Status As String = "Status"
    Public Col1SearchCode As String = "Search Code"



    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("Account Type", "Account Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupTypeQry)
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAgentQry)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


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



    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcMain()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub

    Public Sub ProcMain(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing, Optional bDocId As String = "")
        Try
            Dim mCondStr$ = ""
            Dim mPurchaseReturnCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            Dim mDbPath As String
            mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
            Try
                AgL.Dman_ExecuteNonQry(" attach '" & mDbPath & "' as ODB", AgL.GCn)
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try



            RepTitle = "Party List"
            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If



            mCondStr = " Where 1=1 "
            mCondStr += ReportFrm.GetWhereCondition("H.SubgroupType", rowAccountType)
            mCondStr += ReportFrm.GetWhereCondition("H.CityCode", rowCity)
            mCondStr += ReportFrm.GetWhereCondition("SDD.Agent", rowAgent)



            mQry = "
                    SELECT H.Subcode AS SearchCode, H.Name, H.DispName AS PrintingName, H.Address, 
                    C.CityName, S.Description as State, H.PIN, Area.Description AS Area, H.Phone, H.Mobile, H.Email, H.CreditLimit, 
                    H.CreditDays, H.SalesTaxPostingGroup AS SalesTaxGroup,  
                    (SELECT RegistrationNo FROM SubgroupRegistration WHERE Subcode = H.Subcode AND RegistrationType ='Sales Tax No') AS GstNo,
                    (SELECT RegistrationNo FROM SubgroupRegistration WHERE Subcode = H.Subcode AND RegistrationType ='AADHAR NO') AS AadharNo,
                    (SELECT RegistrationNo FROM SubgroupRegistration WHERE Subcode = H.Subcode AND RegistrationType ='PAN No') AS PanNo,
                    Agent.Name as AgentName, BD.BankName, BD.BankAccount, BD.BankIFSC, Ag.GroupName as AccountGroup, H.ContactPerson, 
                    Parent.Name as ParentName, H.LockText, H.Status
                    FROM Subgroup H
                    LEFT JOIN city C ON H.CityCode = C.CityCode 
                    Left Join State S On C.State = S.Code
                    Left Join viewHelpSubgroup Parent On H.Parent = Parent.Code
                    LEFT JOIN (
			                    SELECT SubCode, Max(Agent) AS Agent 
			                    FROM SubgroupSiteDivisionDetail
			                    GROUP BY SubCode 
		                      ) AS SDD ON H.Subcode = SDD.Subcode
                    Left Join (
                                Select Subcode,BankName, BankAccount, BankIFSC
                                From SubgroupBankAccount Where Sr=0
                              ) BD On H.Subcode = BD.Subcode
                    LEFT JOIN AcGroup Ag ON H.groupCode = Ag.GroupCode 
                    LEFT JOIN Area ON H.Area = Area.Code 
                    Left Join viewHelpSubgroup agent On SDD.Agent = Agent.Code
                   " & mCondStr



            mQry = mQry + " Order By H.Name "

            DsHeader = AgL.FillData(mQry, AgL.GCn)




            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            ReportFrm.Text = "Party List"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"


            ReportFrm.ProcFillGrid(DsHeader)


            ReportFrm.DGL1.AutoResizeRows()

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    Private Sub ObjRepFormGlobal_Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Dim dsTemp As DataSet
        Try

            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) = True Then
                If e.KeyCode = Keys.F2 Then
                    Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                        Case Col1Status
                            ReportFrm.InputColumnsStr = Col1Status
                            ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Tag = "Modify"
                            ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).HeaderCell.Style.BackColor = Color.LightCyan
                            ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).HeaderCell.Style.ForeColor = Color.Black
                    End Select
                Else
                    Exit Sub
                End If
            End If

            If ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Tag <> "Modify" Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1Status
                    mQry = " 
                            Select 'Active' as Code, 'Active' as Description 
                            Union All
                            Select 'Inactive' as Code, 'Inactive' as Description 
                           "
                    dsTemp = AgL.FillData(mQry, AgL.GCn)
                    FSingleSelectForm(Col1Status, bRowIndex, dsTemp)


                    mQry = "Update Subgroup
                            Set Status = " & AgL.Chk_Text(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value) & " 
                            Where Subcode = '" & ReportFrm.DGL1.Item(Col1SearchCode, bRowIndex).Value & "'
                            "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FSingleSelectForm(bColumnName As String, bRowIndex As Integer, bDataSet As DataSet)
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(CType(bDataSet, DataSet).Tables(0)), "", 500, 500, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Description", 400, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Tag = FRH_Single.DRReturn("Code")
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Value = FRH_Single.DRReturn("Description")
        End If
    End Sub

    Public Sub FProceed()
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        FProceed()
    End Sub
End Class
