Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsLedgerUpdation

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
    Dim mHelpSupplierQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name, Sg.SubgroupType FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('" & SubgroupType.Supplier & "')  "
    Dim mHelpTransportQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name, Sg.SubgroupType FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('" & SubgroupType.Site & "')  "
    Dim mHelpSubGroupQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name, Sg.SubgroupType FROM ViewHelpSubgroup Sg "

    Dim mShowReportType As String = ""
    Dim DsHeader As DataSet = Nothing

    Const mReportType_Pending As String = "Pending"
    Const mReportType_Approved As String = "Approved"
    Const mReportType_All As String = "All"

    Private Const rowReportType As Integer = 0
    Private Const rowFromDate As Integer = 1
    Private Const rowToDate As Integer = 2
    Private Const rowParty As Integer = 3


    Public Col1SearchCode As String = "Search Code"
    Public Col1SNo As String = "SNo"
    Public Col1Site As String = "Site"
    Public Col1VType As String = "Type"
    Public Col1Party As String = "Party"
    Public Col1EntryNo As String = "Entry No"
    Public Col1EntryDate As String = "Entry Date"
    Public Col1Narration As String = "Narration"
    Public Col1AmtCr As String = "AmtCr"
    Public Col1AmtDr As String = "AmtDr"
    Public Col1ApprovedDate As String = "Approved Date"
    Public Col1ApprovedBy As String = "Approved By"

    Public Sub Ini_Grid()
        Try
            mQry = "Select '" & mReportType_Pending & "' as Code, '" & mReportType_Pending & "' as Name 
                    Union All 
                    Select '" & mReportType_Approved & "' as Code, '" & mReportType_Approved & "' as Name 
                    Union All 
                    Select '" & mReportType_All & "' as Code, '" & mReportType_All & "' as Name"
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, mReportType_Pending,,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupQry)

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
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            Dim mDbPath As String
            mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
            Try
                AgL.Dman_ExecuteNonQry(" attach '" & mDbPath & "' as ODB", AgL.GCn)
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try



            RepTitle = "Ledger Updation"
            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If

            mCondStr = "  "

            mCondStr += " And Date(H.V_Date) Between " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr += " And H.DivCode = '" & AgL.PubDivCode & "' "
            mCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr += ReportFrm.GetWhereCondition("H.Subcode", rowParty)
            If AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_Pending Then
                mCondStr += " And H.ApprovedDate Is Null "
            ElseIf AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_Approved Then
                mCondStr += " And H.ApprovedDate Is Not Null "
            End If

            mQry = " SELECT H.DocId As SearchCode, H.V_SNo AS SNo, SM.Name AS Site, H.V_Type AS Type, H.V_Date, H.RecId AS RecId, H.Narration, H.AmtDr, H.AmtCr,
                    H.ApprovedBy, H.ApprovedDate 
                    FROM Ledger H
                    LEFT JOIN SiteMast SM ON SM.Code = H.Site_Code 
                    LEFT JOIN Subgroup SG ON SG.Subcode = H.Subcode Where 1=1 "
            mQry = mQry + mCondStr
            mQry = mQry + " Order By H.V_Date,H.V_Type,H.V_No "

            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            ReportFrm.Text = "Ledger Update"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"


            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.IsHideZeroColumns = False

            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next

            ReportFrm.DGL1.Columns(Col1ApprovedBy).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1ApprovedDate).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1ApprovedDate).HeaderText = "Received Date"

            'ReportFrm.DGL1.Columns(Col1SNo).Visible = False

            'ReportFrm.DGL1.Columns(Col1LrDate).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1NoOfBales).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1PrivateMark).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1Weight).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1Freight).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1PaymentType).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1RoadPermitNo).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1RoadPermitDate).ReadOnly = False

            ''ReportFrm.DGL1.Columns(Col1RoadPermitNo).HeaderText = "EWay Bill No"


            'ReportFrm.DGL1.Columns(Col1Site).Visible = True
            'ReportFrm.DGL1.Columns(Col1LrNo).Visible = True
            'ReportFrm.DGL1.Columns(Col1LrDate).Visible = True
            'ReportFrm.DGL1.Columns(Col1NoOfBales).Visible = True


            'ReportFrm.DGL1.Columns(Col1PrivateMark).Visible = True
            'ReportFrm.DGL1.Columns(Col1Weight).Visible = True
            'ReportFrm.DGL1.Columns(Col1Freight).Visible = True
            'ReportFrm.DGL1.Columns(Col1PaymentType).Visible = True
            'ReportFrm.DGL1.Columns(Col1RoadPermitNo).Visible = True
            'ReportFrm.DGL1.Columns(Col1RoadPermitDate).Visible = True

            'ReportFrm.DGL1.AutoResizeRows()

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
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
                Case Col1ApprovedDate
                    If Not IsDBNull(ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value) Then
                        ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value = AgL.RetDate(AgL.XNull(ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value))
                    Else
                        ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value = ""
                    End If
            End Select

            ProcSave(mRowIndex, ReportFrm.DGL1.Columns(mColumnIndex).Name.Replace(" ", ""), AgL.XNull(ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value))

            ReportFrm.DGL1.CurrentCell.Style.BackColor = Color.BurlyWood
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ProcSave(mRow As Integer, FieldName As String, Value As String)
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

            'If AgL.VNull(AgL.Dman_Execute(" Select Count(*) 
            '        From PurchInvoiceTransport With (NoLock)
            '        Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
            '    'mQry = "INSERT INTO PurchInvoiceTransport (DocID)
            '    '        VALUES(" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value) & ", 
            '    '        " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1BiltyNo, mRow).Value) & ", 
            '    '        " & AgL.Chk_Date(ReportFrm.DGL1.Item(Col1BiltyDate, mRow).Value) & ")"
            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            'End If




            If FieldName = "ApprovedDate" Then
                If (Value = "" Or Value = "00:00:00") Then
                    mQry = " UPDATE Ledger SET ApprovedBy= NULL, ApprovedDate = NULL WHERE DocId  = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "' "
                    mQry += ReportFrm.GetWhereCondition("Subcode", rowParty)
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    'mQry = " UPDATE Ledger SET ApprovedBy= '" & ReportFrm.DGL1.Item(Col1ApprovedBy, mRow).Value & "', ApprovedDate =" & AgL.Chk_Date(Value) & " WHERE DocId  = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "' AND  V_SNo = '" & ReportFrm.DGL1.Item(Col1SNo, mRow).Value & "' "
                    mQry = " UPDATE Ledger SET ApprovedBy= '" & ReportFrm.DGL1.Item(Col1ApprovedBy, mRow).Value & "', ApprovedDate =" & AgL.Chk_Date(Value) & " WHERE DocId  = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "' "
                    mQry += ReportFrm.GetWhereCondition("Subcode", rowParty)
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If

            ElseIf FieldName = "Amount" Then
                mQry = " Update PurchInvoiceDetail Set Rate =" & AgL.Chk_Text(Value) & ", Amount =" & AgL.Chk_Text(Value) & ", Gross_Amount =" & AgL.Chk_Text(Value) & ",  
                                 Taxable_Amount =" & AgL.Chk_Text(Value) & ", SubTotal1 =" & AgL.Chk_Text(Value) & ", Net_Amount =" & AgL.Chk_Text(Value) & " Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Update PurchInvoice Set Gross_Amount =" & AgL.Chk_Text(Value) & ",  Taxable_Amount =" & AgL.Chk_Text(Value) & ",
                                 SubTotal1 =" & AgL.Chk_Text(Value) & ", Net_Amount =" & AgL.Chk_Text(Value) & " Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " UPDATE Ledger SET AmtDr =" & AgL.Chk_Text(Value) & " Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "' AND AmtDr > 0 "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " UPDATE Ledger SET AmtCr =" & AgL.Chk_Text(Value) & " Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "' AND AmtCr > 0 "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            End If


            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
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
            'ReportFrm.InputColumnsStr = Col1Site

            'If ClsMain.IsSpecialKeyPressed(e) = True Then
            '    If e.KeyCode = Keys.F2 Then
            '        Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
            '            Case Col1Site
            '                ReportFrm.InputColumnsStr = Col1Site
            '                '    ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Tag = "Modify"
            '                ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).HeaderCell.Style.BackColor = Color.LightCyan
            '                ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).HeaderCell.Style.ForeColor = Color.Black
            '        End Select
            '    Else
            '        Exit Sub
            '    End If
            'End If

            'If ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Tag <> "Modify" Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                'Case Col1Site
                '    If e.KeyCode = Keys.F2 Then
                '        mQry = " SELECT Sg.Subcode AS Code, Sg.Name AS Name FROM Subgroup Sg WHERE Sg.SubgroupType = '" & SubgroupType.Site & "' "
                '        dsTemp = AgL.FillData(mQry, AgL.GCn)
                '        FSingleSelectForm(Col1Site, bRowIndex, dsTemp)

                '        ProcSave(bRowIndex, "Site", ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Tag)
                '        ReportFrm.DGL1.CurrentCell.Style.BackColor = Color.BurlyWood
                '    End If
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
End Class
