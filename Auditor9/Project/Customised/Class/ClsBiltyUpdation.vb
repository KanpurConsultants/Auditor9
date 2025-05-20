Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsBiltyUpdation

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
    Dim mHelpTransportQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name, Sg.SubgroupType FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('" & SubgroupType.Transporter & "')  "

    Dim mShowReportType As String = ""
    Dim DsHeader As DataSet = Nothing

    Const mReportType_Pending As String = "Pending"
    Const mReportType_Received As String = "Recived"
    Const mReportType_All As String = "All"

    Private Const rowReportType As Integer = 0
    Private Const rowFromDate As Integer = 1
    Private Const rowToDate As Integer = 2
    Private Const rowTransport As Integer = 3
    Private Const rowSupplier As Integer = 4


    Public Col1SearchCode As String = "Search Code"
    Public Col1Transporter As String = "Transporter"
    Public Col1Party As String = "Party"
    Public Col1BookedFrom As String = "Booked From"
    Public Col1BookedTo As String = "Booked To"
    Public Col1BiltyNo As String = "Bilty No"
    Public Col1BiltyDate As String = "Bilty Date"
    Public Col1PrivateMark As String = "Private Mark"
    Public Col1NoOfBales As String = "No Of Bales"
    Public Col1Amount As String = "Amount"
    Public Col1UploadDate As String = "Upload Date"

    Public Sub Ini_Grid()
        Try
            mQry = "Select '" & mReportType_Pending & "' as Code, '" & mReportType_Pending & "' as Name 
                    Union All 
                    Select '" & mReportType_Received & "' as Code, '" & mReportType_Received & "' as Name 
                    Union All 
                    Select '" & mReportType_All & "' as Code, '" & mReportType_All & "' as Name"
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, mReportType_Pending,,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Transport", "Transport", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTransportQry)
            ReportFrm.CreateHelpGrid("Supplier", "Supplier", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSupplierQry)

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

    'Public Sub ProcMain(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
    '                            Optional mGridRow As DataGridViewRow = Nothing, Optional bDocId As String = "")
    '    Try
    '        Dim mCondStr$ = ""
    '        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

    '        Dim mDbPath As String
    '        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
    '        Try
    '            AgL.Dman_ExecuteNonQry(" attach '" & mDbPath & "' as ODB", AgL.GCn)
    '        Catch ex As Exception
    '            'MsgBox(ex.Message)
    '        End Try



    '        RepTitle = "Bilty Updation"
    '        If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
    '            If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
    '                ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
    '                ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
    '            Else
    '                Exit Sub
    '            End If
    '        End If

    '        mCondStr = " AND H.V_Type ='PGR' "

    '        mCondStr += " And Date(H.V_Date) Between " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
    '        mCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
    '        mCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
    '        mCondStr += ReportFrm.GetWhereCondition("H.BillToParty", rowSupplier)
    '        mCondStr += ReportFrm.GetWhereCondition("Sit.Transporter", rowTransport)
    '        If AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_Pending Then
    '            mCondStr += " And SIt.UploadDate IS NULL "
    '        ElseIf AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_Received Then
    '            mCondStr += " And SIt.UploadDate IS Not NULL "
    '        End If

    '        mQry = "SELECT H.DocID As SearchCode, H.V_Type + '-' + H.ManualRefNo AS InvoiceNo, 
    '                H.V_Date AS InvoiceDate, H.VendorName As Transporter, Sg.Name AS Party, City.CityName BookedFrom, 'KANPUR' BookedTo,
    '                 SIt.LrNo as BiltyNo, SIt.LrDate As BiltyDate, SIt.PrivateMark, SIt.NoOfBales AS NoOfBales, L.Remark, L.Net_Amount AS Amount, SIt.UploadDate AS UploadDate
    '                FROM PurchInvoice H 
    '                LEFT JOIN PurchInvoiceTransport SIt ON H.DocID = SIt.DocID
    '                LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
    '                LEFT JOIN ViewHelpSubgroup Sg ON H.LinkedParty  = Sg.code
    '                LEFT JOIN SubGroup T On Sit.Transporter = T.SubCode
    '                LEFT JOIN City ON City.CityCode = Sg.CityCode 
    '                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
    '                WHERE 1=1 " & mCondStr
    '        mQry = mQry + " Order By H.V_Date,H.V_Type,H.V_No "

    '        DsHeader = AgL.FillData(mQry, AgL.GCn)

    '        If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


    '        ReportFrm.Text = "Bilty Update"
    '        ReportFrm.ClsRep = Me
    '        ReportFrm.ReportProcName = "ProcMain"


    '        ReportFrm.ProcFillGrid(DsHeader)

    '        ReportFrm.IsHideZeroColumns = False

    '        ReportFrm.DGL1.ReadOnly = False
    '        For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
    '            ReportFrm.DGL1.Columns(I).ReadOnly = True
    '        Next

    '        ReportFrm.DGL1.Columns(Col1Amount).ReadOnly = False
    '        ReportFrm.DGL1.Columns(Col1UploadDate).ReadOnly = False
    '        ReportFrm.DGL1.Columns(Col1UploadDate).HeaderText = "Received Date"

    '        'ReportFrm.DGL1.Columns(Col1Transporter).ReadOnly = False

    '        'ReportFrm.DGL1.Columns(Col1LrDate).ReadOnly = False
    '        'ReportFrm.DGL1.Columns(Col1NoOfBales).ReadOnly = False
    '        'ReportFrm.DGL1.Columns(Col1PrivateMark).ReadOnly = False
    '        'ReportFrm.DGL1.Columns(Col1Weight).ReadOnly = False
    '        'ReportFrm.DGL1.Columns(Col1Freight).ReadOnly = False
    '        'ReportFrm.DGL1.Columns(Col1PaymentType).ReadOnly = False
    '        'ReportFrm.DGL1.Columns(Col1RoadPermitNo).ReadOnly = False
    '        'ReportFrm.DGL1.Columns(Col1RoadPermitDate).ReadOnly = False

    '        ''ReportFrm.DGL1.Columns(Col1RoadPermitNo).HeaderText = "EWay Bill No"


    '        'ReportFrm.DGL1.Columns(Col1Transporter).Visible = True
    '        'ReportFrm.DGL1.Columns(Col1LrNo).Visible = True
    '        'ReportFrm.DGL1.Columns(Col1LrDate).Visible = True
    '        'ReportFrm.DGL1.Columns(Col1NoOfBales).Visible = True


    '        'ReportFrm.DGL1.Columns(Col1PrivateMark).Visible = True
    '        'ReportFrm.DGL1.Columns(Col1Weight).Visible = True
    '        'ReportFrm.DGL1.Columns(Col1Freight).Visible = True
    '        'ReportFrm.DGL1.Columns(Col1PaymentType).Visible = True
    '        'ReportFrm.DGL1.Columns(Col1RoadPermitNo).Visible = True
    '        'ReportFrm.DGL1.Columns(Col1RoadPermitDate).Visible = True

    '        'ReportFrm.DGL1.AutoResizeRows()

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        DsHeader = Nothing
    '    End Try
    'End Sub

    Public Sub ProcMain(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing, Optional bDocId As String = "")
        Try
            Dim mCondStr$ = ""
            Dim mCondStr1$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            Dim mDbPath As String
            mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
            Try
                AgL.Dman_ExecuteNonQry(" attach '" & mDbPath & "' as ODB", AgL.GCn)
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try



            RepTitle = "Bilty Updation"
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
            mCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr += ReportFrm.GetWhereCondition("H.Vendor", rowTransport)
            mCondStr += ReportFrm.GetWhereCondition("H.LinkedParty", rowSupplier)
            If AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_Pending Then
                mCondStr1 += " And SI.Qty_REC - ISNull(SR.Qty_Ret,0) >0 "
            ElseIf AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_Received Then
                mCondStr1 += " And SI.Qty_REC - ISNull(SR.Qty_Ret,0) <=0 "
            End If

            mQry = " SELECT H.DocID As SearchCode, H.V_Type + '-' + H.ManualRefNo AS InvoiceNo, 
                    H.V_Date AS InvoiceDate, H.VendorName As Transporter, Sg.Name AS Party, City.CityName BookedFrom, 'KANPUR' BookedTo,
                     SIt.LrNo as BiltyNo, SIt.LrDate As BiltyDate, SIt.PrivateMark, SIt.NoOfBales AS NoOfBales, L.Remark, L.Net_Amount AS Amount, SR.ReceivedDate
                From
                    (    
                    select S.DocID, S.Sr,  S.Item, S.Qty AS Qty_Rec, S.Unit, S.Rate 
                    FROM PurchInvoice H With (NoLock)
                    LEFT JOIN PurchInvoiceDetail S  With (NoLock) ON S.DocID = H.DocID
                    LEFT JOIN Item Sku With (NoLock) On S.Item = Sku.Code
                    Left Join Voucher_Type Vt  With (NoLock) on H.V_Type = VT.V_Type
                    where VT.NCat = 'WB'  " & mCondStr & "
                    ) as SI
                Left Join 
                    (
                    select S.ReferenceDocID,  S.ReferenceSr, Max(H.V_Date) AS ReceivedDate, Sum(S.Qty) as Qty_Ret
                    FROM PurchInvoice H With (NoLock)
                    LEFT JOIN PurchInvoiceDetail S  With (NoLock) ON S.DocID = H.DocID
                    LEFT JOIN Item Sku With (NoLock) On S.Item = Sku.Code
                    Left Join Voucher_Type Vt  With (NoLock) on H.V_Type = VT.V_Type
                    where VT.nCat='WBI'  Group By S.ReferenceDocID,  S.ReferenceSr
                    ) As SR On SI.DocID = SR.ReferenceDocID  And SI.Sr = SR.ReferenceSr
                Left Join PurchInvoice H  With (NoLock) On SI.DocID = H.DocID
                LEFT JOIN PurchInvoiceTransport SIt ON H.DocID = SIt.DocID
                LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN ViewHelpSubgroup Sg ON H.LinkedParty  = Sg.code
                LEFT JOIN SubGroup T On Sit.Transporter = T.SubCode
                LEFT JOIN City ON City.CityCode = Sg.CityCode Where 1=1 "
            mQry = mQry + mCondStr1
            mQry = mQry + " Order By H.V_Date,H.V_Type,H.V_No "

            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            ReportFrm.Text = "Bilty Update"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"


            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.IsHideZeroColumns = False

            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next

            'ReportFrm.DGL1.Columns(Col1Amount).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1UploadDate).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1UploadDate).HeaderText = "Received Date"

            'ReportFrm.DGL1.Columns(Col1Transporter).ReadOnly = False

            'ReportFrm.DGL1.Columns(Col1LrDate).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1NoOfBales).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1PrivateMark).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1Weight).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1Freight).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1PaymentType).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1RoadPermitNo).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1RoadPermitDate).ReadOnly = False

            ''ReportFrm.DGL1.Columns(Col1RoadPermitNo).HeaderText = "EWay Bill No"


            'ReportFrm.DGL1.Columns(Col1Transporter).Visible = True
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
                Case Col1UploadDate
                    If Not IsDBNull(ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value) Then
                        ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value = AgL.RetDate(AgL.XNull(ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value))
                    Else
                        ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value = ""
                    End If
            End Select

            'ProcSave(mRowIndex, ReportFrm.DGL1.Columns(mColumnIndex).Name.Replace(" ", ""), AgL.XNull(ReportFrm.DGL1.Item(mColumnIndex, mRowIndex).Value))

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

            If AgL.VNull(AgL.Dman_Execute(" Select Count(*) 
                    From PurchInvoiceTransport With (NoLock)
                    Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                mQry = "INSERT INTO PurchInvoiceTransport (DocID)
                        VALUES(" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value) & ", 
                        " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1BiltyNo, mRow).Value) & ", 
                        " & AgL.Chk_Date(ReportFrm.DGL1.Item(Col1BiltyDate, mRow).Value) & ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If




            If FieldName = "UploadDate" Then
                If (Value = "" Or Value = "00:00:00") Then
                    mQry = "Update PurchInvoiceTransport Set UploadDate = Null
                        WHERE DocID ='" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "Delete From Ledger
                        WHERE DocID ='" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "UPDATE PurchInvoice SET LockText = Null
                        WHERE DocID ='" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                Else

                    mQry = " Update PurchInvoiceTransport Set UploadDate =" & AgL.Chk_Date(Value) & " Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = " Delete From Ledger Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, AmtDr, AmtCr, TdsOnAmt, TdsPer, Tds_Of_V_Sno, Site_Code, DivCode, System_Generated, ContraText, RecId) 
                        SELECT H.DocId, 1 V_SNo, H.V_No, H.V_Type, H.V_Prefix, H.V_Date, T.Transporter AS SubCode, 0 AmtDr, L.Net_Amount AS AmtCr, 0 TdsOnAmt, 0 TdsPer, 0 Tds_Of_V_Sno, H.Site_Code, H.Div_Code DivCode, 'Y' System_Generated,  'A.Name '+ convert(NVARCHAR,L.Amount)+ ' Cr '  AS  ContraText, H.ManualRefNo  RecId 
                        FROM PurchInvoice H WITH (Nolock)
                        LEFT JOIN PurchInvoiceTransport  T ON T.DocID = H.DocID 
                        LEFT JOIN PurchInvoiceDetail L WITH (Nolock) ON L.DocID = H.DocID 
                        LEFT JOIN Item I WITH (Nolock) ON I.Code = L.Item 
                        WHERE H.DocID ='" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, AmtDr, AmtCr, TdsOnAmt, TdsPer, Tds_Of_V_Sno, Site_Code, DivCode, System_Generated, ContraText, RecId) 
                        SELECT H.DocId, 2 V_SNo, H.V_No, H.V_Type, H.V_Prefix, H.V_Date, I.PurchaseAc  AS SubCode, L.Net_Amount AmtDr, 0 AS AmtCr, 0 TdsOnAmt, 0 TdsPer, 0 Tds_Of_V_Sno, H.Site_Code, H.Div_Code DivCode, 'Y' System_Generated,  'A.Name '+ convert(NVARCHAR,L.Amount)+ ' Cr '  AS  ContraText, H.ManualRefNo  RecId 
                        FROM PurchInvoice H WITH (Nolock)
                        LEFT JOIN Subgroup A ON A.Subcode = H.Agent 
                        LEFT JOIN PurchInvoiceDetail L WITH (Nolock) ON L.DocID = H.DocID 
                        LEFT JOIN Item I WITH (Nolock) ON I.Code = L.Item 
                        WHERE H.DocID ='" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "UPDATE PurchInvoice SET LockText = 'Goods Received !'
                        WHERE DocID ='" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
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
            'ReportFrm.InputColumnsStr = Col1Transporter

            'If ClsMain.IsSpecialKeyPressed(e) = True Then
            '    If e.KeyCode = Keys.F2 Then
            '        Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
            '            Case Col1Transporter
            '                ReportFrm.InputColumnsStr = Col1Transporter
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
                'Case Col1Transporter
                '    If e.KeyCode = Keys.F2 Then
                '        mQry = " SELECT Sg.Subcode AS Code, Sg.Name AS Name FROM Subgroup Sg WHERE Sg.SubgroupType = '" & SubgroupType.Transporter & "' "
                '        dsTemp = AgL.FillData(mQry, AgL.GCn)
                '        FSingleSelectForm(Col1Transporter, bRowIndex, dsTemp)

                '        ProcSave(bRowIndex, "Transporter", ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Tag)
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
