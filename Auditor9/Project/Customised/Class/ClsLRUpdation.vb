Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Microsoft.Reporting.WinForms
Public Class ClsLRUpdation

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

    Const mReportType_PendingForLr As String = "Pending For Lr"
    Const mReportType_All As String = "All"

    Private Const rowReportType As Integer = 0
    Private Const rowFromDate As Integer = 1
    Private Const rowToDate As Integer = 2
    Private Const rowCustomer As Integer = 3
    Private Const rowVoucherType As Integer = 4

    Public Col1SearchCode As String = "Search Code"
    Public Col1Transporter As String = "Transporter"
    Public Col1LrNo As String = "Lr No"
    Public Col1LrDate As String = "Lr Date"
    Public Col1NoOfBales As String = "No Of Bales"
    Public Col1PrivateMark As String = "Private Mark"
    Public Col1Weight As String = "Weight"
    Public Col1ChargedWeight As String = "Charged Weight"
    Public Col1Freight As String = "Freight"
    Public Col1PaymentType As String = "Payment Type"
    Public Col1VehicleNo As String = "Vehicle No"
    Public Col1ShipMethod As String = "Ship Method"
    Public Col1PreCarriageBy As String = "Pre Carriage By"
    Public Col1PreCarriagePlace As String = "Pre Carriage Place"
    Public Col1BookedFrom As String = "Booked From"
    Public Col1BookedTo As String = "Booked To"
    Public Col1Destination As String = "Destination"
    Public Col1DescriptionOfGoods As String = "Description Of Goods"
    Public Col1DescriptionOfPacking As String = "Description Of Packing"
    Public Col1RoadPermitNo As String = "Road Permit No"
    Public Col1RoadPermitDate As String = "Road Permit Date"
    Public Col1EInvoiceACKNo As String = "E-Invoice ACKNo"
    Public Col1EInvoiceACKDate As String = "E-Invoice ACKDate"
    Public Sub Ini_Grid()
        Try
            mQry = "Select '" & mReportType_PendingForLr & "' as Code, '" & mReportType_PendingForLr & "' as Name 
                    Union All 
                    Select '" & mReportType_All & "' as Code, '" & mReportType_All & "' as Name"
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, mReportType_PendingForLr,,, 300)
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



            RepTitle = "Lr Updation"
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
            If AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_PendingForLr Then
                mCondStr += " And (SIt.Transporter IS NULL Or SIt.LrNo IS NULL OR SIt.LrDate IS NULL) "
            End If

            'H.V_Date AS InvoiceDate, Sg.Name AS Party, H.EInvoiceACKNo, H.EInvoiceACKDate,

            mQry = "SELECT H.DocID As SearchCode, H.V_Type + '-' + H.ManualRefNo AS InvoiceNo, 
                    H.V_Date AS InvoiceDate, Sg.Name AS Party, H.eInvoiceAckNo, H.eInvoiceAckDate,
                    T.Name As Transporter, SIt.LrNo, Cast(SIt.LrDate As nvarchar) As LrDate,
                    SIt.PrivateMark, SIt.Weight, SIt.Freight, SIt.PaymentType, SIt.RoadPermitNo, 
                    Cast(SIt.RoadPermitDate As nvarchar) As RoadPermitDate, 
                    SIt.VehicleNo, SIt.ShipMethod, SIt.PreCarriageBy, 
                    SIt.PreCarriagePlace, SIt.BookedFrom, SIt.BookedTo, SIt.Destination, 
                    SIt.DescriptionOfGoods, SIt.DescriptionOfPacking, SIt.ChargedWeight, SIt.NoOfBales
                    FROM SaleInvoice H 
                    LEFT JOIN SaleInvoiceTransport SIt ON H.DocID = SIt.DocID
                    LEFT JOIN ViewHelpSubgroup Sg ON H.SaleToParty = Sg.code
                    LEFT JOIN SubGroup T On Sit.Transporter = T.SubCode
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    WHERE 1=1 " & mCondStr
            mQry = mQry + " Order By H.V_Date,H.V_Type,H.V_No "

            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            ReportFrm.Text = "LR Update"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"


            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.IsHideZeroColumns = False

            ReportFrm.DGL1.ReadOnly = False
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next

            'ReportFrm.DGL1.Columns(Col1Transporter).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1LrNo).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1LrDate).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1NoOfBales).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1PrivateMark).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Weight).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1Freight).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1PaymentType).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1RoadPermitNo).ReadOnly = False
            ReportFrm.DGL1.Columns(Col1RoadPermitDate).ReadOnly = False

            ReportFrm.DGL1.Columns(Col1RoadPermitNo).HeaderText = "EWay Bill No"
            ReportFrm.DGL1.Columns(Col1RoadPermitDate).HeaderText = "EWay Bill Date"

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
                Case Col1LrDate, Col1RoadPermitDate
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

            If AgL.VNull(AgL.Dman_Execute(" Select Count(*) 
                    From SaleInvoiceTransport With (NoLock)
                    Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                mQry = "INSERT INTO SaleInvoiceTransport (DocID)
                        VALUES(" & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value) & ", 
                        " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1LrNo, mRow).Value) & ", 
                        " & AgL.Chk_Date(ReportFrm.DGL1.Item(Col1LrDate, mRow).Value) & ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            Dim bUpdateStr As String = ""
            If FieldName = "LrDate" Then
                bUpdateStr = FieldName + " = " + AgL.Chk_Date(Value)
            Else
                bUpdateStr = FieldName + " = " + AgL.Chk_Text(Value)
            End If

            mQry = " Update SaleInvoiceTransport Set " & bUpdateStr &
                    " Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            AgL.ETrans.Commit()
            mTrans = "Commit"

            Dim valLRNo = ReportFrm.DGL1.Item(Col1LrNo, mRow).Value
            Dim valLRDate = ReportFrm.DGL1.Item(Col1LrDate, mRow).Value

            If valLRNo IsNot Nothing AndAlso Not IsDBNull(valLRNo) And valLRDate IsNot Nothing AndAlso Not IsDBNull(valLRDate) Then
                FSendWhatsapp(ReportFrm.DGL1.Item(Col1SearchCode, mRow).Value)
            End If

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FSendWhatsapp(SearchCode As String)
        Dim IsSuccess As Boolean
        Dim ToMobileNo As String
        Dim ToMessage As String
        mQry = "Select 
                    Sg.DispName As DivisionName, VT.Short_Name +'-'+ H.ManualRefNo AS SaleNo, replace(Convert(NVARCHAR,H.V_Date,106),' ','/') AS SaleDate,
                    Party.DispName As PartyName, Party.Mobile As PartyMobile,
                    T.Name As TransporterName, SIt.LrNo, replace(Convert(NVARCHAR,SIt.LrDate,106),' ','/') As LrDate, H.Net_Amount
                    From SaleInvoice H 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    LEFT JOIN Voucher_Type VT ON VT.V_Type = H.V_Type 
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN SubGroup Party On H.SaleToParty = Party.SubCode
                    LEFT JOIN SaleInvoiceTransport SIt ON H.DocID = SIt.DocID
                    LEFT JOIN SubGroup T On SIT.Transporter = T.SubCode
                    Where H.DocId = '" & SearchCode & "'"
        Dim DtDocData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        ToMobileNo = AgL.XNull(DtDocData.Rows(0)("PartyMobile"))
        'ToMobileNo = "8299399688"
        'ToMessage = FGetSettings(SettingFields.SmsMessage, SettingType.General)
        ToMessage = "Dear <PartyName>,

                    Your Inv.No. <EntryNo> Dated <EntryDate> of Rs.<NetAmount> has been dispatched
                    By Transport <TransporterName> with LR No. <LRNo> on Date <LRDate> .

                    Sincerely
                    <DivisionName>"
        ToMessage = ToMessage.
                Replace("<PartyName>", AgL.XNull(DtDocData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", AgL.XNull(DtDocData.Rows(0)("SaleNo"))).
                Replace("<EntryDate>", AgL.XNull(DtDocData.Rows(0)("SaleDate"))).
                Replace("<LRNo>", AgL.XNull(DtDocData.Rows(0)("LRNo"))).
                Replace("<LRDate>", AgL.XNull(DtDocData.Rows(0)("LRDate"))).
                Replace("<DivisionName>", AgL.XNull(DtDocData.Rows(0)("DivisionName"))).
                Replace("<TransporterName>", AgL.XNull(DtDocData.Rows(0)("TransporterName"))).
                Replace("<NetAmount>", Format(AgL.VNull(DtDocData.Rows(0)("Net_Amount")), "0.00")).
                Replace("&", "And")
        IsSuccess = FSendWhatsappMessage(ToMobileNo, ToMessage, "Message", "")
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function

    Private Sub ObjRepFormGlobal_Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Dim dsTemp As DataSet
        Try

            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex
            ReportFrm.InputColumnsStr = Col1Transporter

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
                Case Col1Transporter
                    If e.KeyCode = Keys.F2 Then
                        mQry = " SELECT Sg.Subcode AS Code, Sg.Name AS Name FROM Subgroup Sg WHERE Sg.SubgroupType = '" & SubgroupType.Transporter & "' "
                        dsTemp = AgL.FillData(mQry, AgL.GCn)
                        FSingleSelectForm(Col1Transporter, bRowIndex, dsTemp)

                        ProcSave(bRowIndex, "Transporter", ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Tag)
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
