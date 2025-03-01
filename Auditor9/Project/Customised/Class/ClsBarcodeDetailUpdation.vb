Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsBarcodeDetailUpdation

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
    Dim mHelpCustomerQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name, Sg.SubgroupType FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('" & SubgroupType.Customer & "')  "


    Dim mShowReportType As String = ""
    Dim DsHeader As DataSet = Nothing

    Const mReportType_PendingForRegistration As String = "Pending For Registration"
    Const mReportType_PendingForInsurance As String = "Pending For Insurance"
    Const mReportType_All As String = "All"

    Private Const rowReportType As Integer = 0
    Private Const rowFromDate As Integer = 1
    Private Const rowToDate As Integer = 2
    Private Const rowCustomer As Integer = 3
    Private Const rowVoucherType As Integer = 4

    Public Col1SearchCode As String = "Search Code"
    Public Col1BarcodeId As String = "BarcodeId"
    Public Col1Barcode As String = "Barcode"
    Public Col1Remark1 As String = "Remark1"
    Public Col1Remark2 As String = "Remark2"
    Public Col1Remark3 As String = "Remark3"
    Public Col1Remark4 As String = "Remark4"
    Public Col1Remark5 As String = "Remark5"
    Public Col1Remark6 As String = "Remark6"
    Public Col1Remark7 As String = "Remark7"
    Public Col1Remark8 As String = "Remark8"
    Public Col1Remark9 As String = "Remark9"
    Public Col1Remark10 As String = "Remark10"
    Public Col1Remark11 As String = "Remark11"
    Public Col1Remark12 As String = "Remark12"
    Public Col1Remark13 As String = "Remark13"
    Public Col1Remark14 As String = "Remark14"
    Public Col1Remark15 As String = "Remark15"
    Public Col1Remark16 As String = "Remark16"
    Public Col1Remark17 As String = "Remark17"
    Public Col1Remark18 As String = "Remark18"

    Public Sub Ini_Grid()
        Try
            mQry = "Select '" & mReportType_PendingForRegistration & "' as Code, '" & mReportType_PendingForRegistration & "' as Name 
                    Union All 
                    Select '" & mReportType_PendingForRegistration & "' as Code, '" & mReportType_PendingForRegistration & "' as Name 
                    Union All 
                    Select '" & mReportType_All & "' as Code, '" & mReportType_All & "' as Name"
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, mReportType_PendingForRegistration,,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Customer", "Customer", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCustomerQry)
            ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsMain.FGetVoucher_TypeQry("SaleInvoice", Ncat.SaleInvoice + "," + Ncat.SaleReturn))
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



            RepTitle = "Barcode Detail Updation"
            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " "
            mCondStr += " And Date(H.V_Date) Between " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr += ReportFrm.GetWhereCondition("H.SaleToParty", rowCustomer)
            mCondStr += ReportFrm.GetWhereCondition("H.V_Type", rowVoucherType)
            If AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_PendingForRegistration Then
                mCondStr += " And (B.Remark5 IS NULL Or B.Remark6 IS NULL OR B.Remark7 IS NULL OR B.Remark8 IS NULL) "
            ElseIf AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_PendingForInsurance Then
                mCondStr += " And (B.Remark9 IS NULL Or B.Remark10 IS NULL OR B.Remark11 IS NULL OR B.Remark12 IS NULL) "
            End If

            mQry = "SELECT H.DocID As SearchCode, H.V_Type + '-' + H.ManualRefNo AS InvoiceNo, 
                    H.V_Date AS InvoiceDate, Sg.Name AS Party, I.Description as Item, B.Description As Barcode,
                    B.Remark5,B.Remark6,B.Remark7,B.Remark8,B.Remark9,B.Remark10,B.Remark11,B.Remark12,B.Remark13,B.Remark14,B.Remark15,B.Remark16,B.Remark17,B.Remark18
                    FROM SaleInvoice H 
                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                    Left Join Item I on I.Code = L.Item
                    LEFT JOIN Barcode B on B.Code = L.Barcode
                    LEFT JOIN ViewHelpSubgroup Sg ON H.SaleToParty = Sg.code
                    LEFT JOIN SubGroup G On G.Subcode = L.Godown
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE 1=1 " & mCondStr
            mQry = mQry + " Order By H.V_Date,H.V_Type,H.V_No "

            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            ReportFrm.Text = "Barcode Update"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"


            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.IsHideZeroColumns = False

            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next


            ReportFrm.DGL1.Columns(Col1Barcode).Visible = True
            'ReportFrm.DGL1.Columns(Col1Remark1).Visible = True
            'ReportFrm.DGL1.Columns(Col1Remark2).Visible = True
            'ReportFrm.DGL1.Columns(Col1Remark3).Visible = True
            'ReportFrm.DGL1.Columns(Col1Remark4).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark5).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark6).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark7).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark8).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark9).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark10).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark11).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark12).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark13).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark14).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark15).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark16).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark17).Visible = True
            ReportFrm.DGL1.Columns(Col1Remark18).Visible = True

            ReportFrm.DGL1.Columns(Col1Remark5).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark6).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark7).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark8).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark9).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark10).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark11).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark12).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark13).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark14).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark15).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark16).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark17).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Remark18).ReadOnly = False

            ReportFrm.DGL1.Columns(Col1Remark5).HeaderText = "Registration Broker"
            ReportFrm.DGL1.Columns(Col1Remark6).HeaderText = "Registration No"
            ReportFrm.DGL1.Columns(Col1Remark7).HeaderText = "Registration Date"
            ReportFrm.DGL1.Columns(Col1Remark8).HeaderText = "Registration Fees"
            ReportFrm.DGL1.Columns(Col1Remark9).HeaderText = "Insurance Broker"
            ReportFrm.DGL1.Columns(Col1Remark10).HeaderText = "Insurance Company"
            ReportFrm.DGL1.Columns(Col1Remark11).HeaderText = "Policy No"
            ReportFrm.DGL1.Columns(Col1Remark12).HeaderText = "Insurance Fees"
            ReportFrm.DGL1.Columns(Col1Remark13).HeaderText = "Eirthing Agent"
            ReportFrm.DGL1.Columns(Col1Remark14).HeaderText = "Eirthing Date"
            ReportFrm.DGL1.Columns(Col1Remark15).HeaderText = "Eirthing Fees"
            ReportFrm.DGL1.Columns(Col1Remark16).HeaderText = "Hypothication"
            ReportFrm.DGL1.Columns(Col1Remark17).HeaderText = "Hypothication Date"
            ReportFrm.DGL1.Columns(Col1Remark18).HeaderText = "Hypothication Fees"

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
                Case Col1Remark7
                    ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value = AgL.RetDate(AgL.XNull(ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value))
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
            '        From SaleInvoiceTransport With (NoLock)
            '        Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
            '    mQry = "INSERT INTO SaleInvoiceTransport (DocID)
            '            VALUES(" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value) & ", 
            '            " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1Remark5, mRow).Value) & ", 
            '            " & AgL.Chk_Date(ReportFrm.DGL1.Item(Col1Remark5, mRow).Value) & ")"
            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            'End If

            Dim bUpdateStr As String = ""
            If FieldName = "LrDate" Then
                bUpdateStr = FieldName + " = " + AgL.Chk_Date(Value)
            Else
                bUpdateStr = FieldName + " = " + AgL.Chk_Text(Value)
            End If

            mQry = " Update Barcode Set " & bUpdateStr &
                    " Where Description = '" & ReportFrm.DGL1.Item(Col1Barcode, mRow).Value & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


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
            'ReportFrm.InputColumnsStr = Col1Remark5

            'If ClsMain.IsSpecialKeyPressed(e) = True Then
            '    If e.KeyCode = Keys.F2 Then
            '        Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
            '            Case Col1Remark5
            '                ReportFrm.InputColumnsStr = Col1Remark5
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
                Case Col1Remark5
                    If e.KeyCode = Keys.F2 Then
                        mQry = " SELECT Sg.Subcode AS Code, Sg.Name AS Name FROM Subgroup Sg WHERE Sg.SubgroupType = 'Broker' "
                        dsTemp = AgL.FillData(mQry, AgL.GCn)
                        FSingleSelectForm(Col1Remark5, bRowIndex, dsTemp)

                        ProcSave(bRowIndex, Col1Remark5, ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Tag)
                        ReportFrm.DGL1.CurrentCell.Style.BackColor = Color.BurlyWood
                    End If
                Case Col1Remark9
                    If e.KeyCode = Keys.F2 Then
                        mQry = " SELECT Sg.Subcode AS Code, Sg.Name AS Name FROM Subgroup Sg WHERE Sg.SubgroupType = 'Broker' "
                        dsTemp = AgL.FillData(mQry, AgL.GCn)
                        FSingleSelectForm(Col1Remark9, bRowIndex, dsTemp)

                        ProcSave(bRowIndex, Col1Remark9, ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Tag)
                        ReportFrm.DGL1.CurrentCell.Style.BackColor = Color.BurlyWood
                    End If
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
