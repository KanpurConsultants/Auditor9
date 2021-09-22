Imports System.IO
Imports System.Net
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsSmsCustomerLedgerBalance
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
    Dim DtSmsConfiguration As DataTable = Nothing

    Dim rowAccountType As Integer = 0
    Dim rowParty As Integer = 1
    Dim rowCity As Integer = 2
    Dim rowSite As Integer = 3
    Dim rowDivision As Integer = 4
    Dim rowMessage As Integer = 5
    Dim rowOwnerMobileNo As Integer = 6

    Private Const Col1Name As String = "Name"
    Private Const Col1Mobile As String = "Mobile"
    Private Const Col1Balance As String = "Balance"
    Private Const Col1Message As String = "Message"

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

    Dim mHelpAccountTypeQry$ = " Select Sg.SubgroupType As Code, Sg.SubgroupType as Name FROM SubGroupType Sg Where Sg.SubgroupType In ('Customer','Sales Agent') Order by SubgroupType "
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
        Try
            ReportFrm.CreateHelpGrid("Account Type", "Account Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpAccountTypeQry, "Customer", 450, 825, 300)
            ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, , 450, 825, 300)
            ReportFrm.CreateHelpGrid("City", "City", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpCityQry, , 450, 825, 300)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Message", "Message", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.StringType, "", "Dear <Customer> your account balance is <Amount>.")
            ReportFrm.CreateHelpGrid("Owner Mobile", "Owner Mobile", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", "")
            ReportFrm.BtnProceed.Visible = True
            ReportFrm.BtnProceed.Text = "Send"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcSmsCustomerLedgerBalance()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcSmsCustomerLedgerBalance(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Sms To Customers"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then

                Else
                    Exit Sub
                End If
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Subcode", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Citycode", rowCity)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'")


            If ReportFrm.FGetText(rowAccountType).ToString.ToUpper = "SALES AGENT" Then
                mQry = "SELECT Max(Sg.Name) AS Name, Max(Sg.Mobile) As Mobile, IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) AS Balance,
                    Replace(Replace('" & AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowMessage).Value) & "','<Customer>',Max(Sg.Name)),'<Amount>',IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0)) AS Message
                    FROM Ledger L 
                    Left Join Subgroup Party On L.Subcode = Party.Subcode
                    Left Join (Select SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code) as LTV On Party.Subcode = LTV.Subcode And L.Site_Code = LTV.Site_Code And L.DivCode = LTV.Div_Code
                    LEFT JOIN Subgroup Sg ON LTV.Agent = Sg.Subcode
                    WHERE Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' " & mCondStr &
                    " GROUP BY Sg.SubCode
                    HAVING IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) > 0 "

                mQry = mQry & " Union All SELECT 'Owner' AS Name, " & AgL.Chk_Text(AgL.XNull(ReportFrm.FGetText(rowOwnerMobileNo))) & " As Mobile, IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) AS Balance,
                    Replace(Replace('" & AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowMessage).Value) & "','<Customer>','Sir'),'<Amount>',IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0)) AS Message
                    FROM Ledger L 
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode
                    WHERE Sg.SubgroupType = '" & SubgroupType.Customer & "' " & mCondStr & "  "

                DsHeader = AgL.FillData(mQry, AgL.GCn)
            Else
                mQry = "SELECT Max(Sg.Name) AS Name, Max(Sg.Mobile) As Mobile, IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) AS Balance,
                    Replace(Replace('" & AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowMessage).Value) & "','<Customer>',Max(Sg.Name)),'<Amount>',IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0)) AS Message
                    FROM Ledger L 
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode
                    WHERE Sg.SubgroupType = '" & SubgroupType.Customer & "' " & mCondStr &
                    " GROUP BY L.SubCode
                    HAVING IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0) > 0 "
                DsHeader = AgL.FillData(mQry, AgL.GCn)
            End If


            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sms Customer Ledger Balance"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcSmsCustomerLedgerBalance"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.DGL1.Columns(Col1Name).Width = 300
            ReportFrm.DGL1.Columns(Col1Mobile).Width = 100
            ReportFrm.DGL1.Columns(Col1Balance).Width = 150
            ReportFrm.DGL1.Columns(Col1Message).Width = 500

            ReportFrm.DGL1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
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
    Private Sub FGetSmsConfiguration()
        mQry = "Select * From SmsSender Where Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "'"
        DtSmsConfiguration = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtSmsConfiguration.Rows.Count = 0 Then
            mQry = "Select * From SmsSender Where Div_Code = '" & AgL.PubDivCode & "' "
            DtSmsConfiguration = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtSmsConfiguration.Rows.Count = 0 Then
                mQry = "Select * From SmsSender Where Site_Code = '" & AgL.PubSiteCode & "' "
                DtSmsConfiguration = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtSmsConfiguration.Rows.Count = 0 Then
                    mQry = "Select * From SmsSender "
                    DtSmsConfiguration = AgL.FillData(mQry, AgL.GCn).Tables(0)
                End If
            End If
        End If

        If DtSmsConfiguration.Rows.Count = 0 Then
            MsgBox("Please define Sms settings...!", MsgBoxStyle.Information)
            Exit Sub
        End If
    End Sub
    Public Function FSendSms(MobileNo As String, Message As String)
        Dim mQry As String = ""
        Try
            'Dim SmsAPI As String = "http://my.msgwow.com/api/sendhttp.php?authkey=163094A1yq0cNVbFr85953f3cf&mobiles=" + MobileNoList + "&message=" + TxtMessage.Text + "&sender=Kanpur&route=4"
            Dim SmsAPI As String = DtSmsConfiguration.Rows(0)("SmsAPI").ToString().Replace("<MobileNo>", MobileNo).Replace("<Message>", Message)
            Dim myReq As HttpWebRequest = System.Net.WebRequest.Create(SmsAPI)
            Dim myResp As HttpWebResponse = myReq.GetResponse()
            Dim respStreamReader As System.IO.StreamReader = New System.IO.StreamReader(myResp.GetResponseStream())
            Dim responseString As String = respStreamReader.ReadToEnd()
            respStreamReader.Close()
            myResp.Close()

            FSendSms = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        FGetSmsConfiguration()

        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            If AgL.XNull(ReportFrm.DGL1.Item(Col1Mobile, I).Value) <> "" And
                AgL.XNull(ReportFrm.DGL1.Item(Col1Message, I).Value) <> "" Then
                FSendSms(ReportFrm.DGL1.Item(Col1Mobile, I).Value, ReportFrm.DGL1.Item(Col1Message, I).Value)
            End If
        Next

        MsgBox("Process Complete...!", MsgBoxStyle.Information)
    End Sub
End Class
