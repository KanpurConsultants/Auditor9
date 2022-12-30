Imports AgLibrary.ClsMain.agConstants

Public Class ClsDeleteLedgerAcData

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""
    Dim mLogText As String = ""
    Dim mSearchCode As String = ""

    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout
    Dim Connection_Pakka As SQLite.SQLiteConnection

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

    Dim mHelpAreaQry$ = "Select 'o' As Tick, Code, Description From Area "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
    'Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division Where Div_Code In (" & AgL.PubDivisionList & ") "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name From ItemType "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpVendorQry$ = " Select 'o' As Tick,  H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpTableQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM HT_Table H "
    Dim mHelpPaymentModeQry$ = "Select 'o' As Tick, 'Cash' As Code, 'Cash' As Description " &
                                " UNION ALL " &
                                " Select 'o' As Tick, 'Credit' As Code, 'Credit' As Description "
    Dim mHelpOutletQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM Outlet H "
    Dim mHelpStewardQry$ = "Select 'o' As Tick,  Sg.SubCode AS Code, Sg.DispName AS Steward FROM SubGroup Sg  "
    Dim mHelpPartyQry$ = " Select Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  
                            Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode 
                            Where Sg.Nature In ('Customer','Supplier','Cash') 
                            And Sg.SubGroupType = 'Master Customer'"
    Dim mHelpAgentQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpYesNo$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpSaleOrderQry$ = " Select 'o' As Tick,  H.DocID AS Code, H.V_Type || '-' || H.ManualRefNo  FROM SaleOrder H "
    Dim mHelpSaleBillQry$ = " SELECT 'o' As Tick,DocId, ReferenceNo AS BillNo, V_Date AS Date FROM SaleChallan "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick,I.Code,I.Description  AS ItemReportingGroup FROM ItemReportingGroup I "
    Dim mHelpSalesRepresentativeQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Sales Representative] FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code ='SREP' "
    Dim mHelpResponsiblePersonQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code <>'SREP' "
    Dim mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpDimension1Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension1 & "' Order By Specification "
    Dim mHelpDimension2Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension2 & "' Order By Specification "
    Dim mHelpDimension3Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension3 & "' Order By Specification "
    Dim mHelpDimension4Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension4 & "' Order By Specification "
    Dim mHelpSingleDimension1Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension1 & "' Order By Specification "
    Dim mHelpSingleDimension2Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension2 & "' Order By Specification "
    Dim mHelpSingleDimension3Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension3 & "' Order By Specification "
    Dim mHelpSingleDimension4Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension4 & "' Order By Specification "
    Dim mHelpSingleProcessQry$ = "Select Subcode as Code, Name From Subgroup Where SubgroupType = '" & SubgroupType.Process & "' Order By Name "
    Dim mHelpSingleJobProcessQry$ = "Select Subcode as Code, Name From Subgroup Where SubgroupType = '" & SubgroupType.Process & "' And Subcode Not In ('" & Process.Sales & "', '" & Process.Purchase & "', '" & Process.Stock & "')  Order By Name "
    Dim mHelpSizeQry$ = "Select 'o' As Tick, Code, Description As Name From Item Where V_Type = '" & ItemV_Type.SIZE & "' Order By Specification "
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description FROM Tag H "
    Dim mHelpAccountGroupQry$ = "SELECT GroupCode As Code, GroupName FROM AcGroup WHERE GroupName IN ('Sundry Creditors','Sundry Debtors') "
    Dim mHelpAccountQry$ = "SELECT SG.Subcode As Code, SG.Name  
                            FROM Subgroup SG
                            LEFT JOIN AcGroup AG ON AG.GroupCode = SG.GroupCode "

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""

    Private Const rowAsOnDate As Integer = 0
    Private Const rowParty As Integer = 1
    Private Const rowDivision As Integer = 2
    Private Const rowSite As Integer = 3
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            'ReportFrm.CreateHelpGrid("Account Group", "Account Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpAccountGroupQry, "")
            ReportFrm.CreateHelpGrid("Account", "Account", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpAccountQry, "")
            ReportFrm.BtnPrint.Text = "Delete"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcDeleteData()
    End Sub
    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub
    Private Sub ProcDeleteData()
        Dim mTrans As String
        Dim bConStr$ = ""
        Dim bOMSIdConStr$ = ""
        Connection_Pakka = New SQLite.SQLiteConnection

        Dim mDbPath As String = ""
        Dim mDbEncryption As String = ""

        mLogText = ""
        mSearchCode = AgL.GetGUID(AgL.GCn)

        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        mDbEncryption = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Encryption", "")
        If mDbEncryption = "N" Then
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
        Else
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection_Pakka.Open()


        If ReportFrm.FGetText(0) = "" Then MsgBox("As On Date is required.", MsgBoxStyle.Information) : Exit Sub
        'If ReportFrm.FGetText(1) = "" Then MsgBox("Account Group is required.", MsgBoxStyle.Information) : Exit Sub
        If ReportFrm.FGetText(1) = "" Then MsgBox("Account is required.", MsgBoxStyle.Information) : Exit Sub

        mQry = "select SubGroupType from SubGroup Where SubCode = " & ReportFrm.FGetCode(1) & ""
        Dim mSubGroupType As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).executeScalar())
        If mSubGroupType <> "Ledger Account" Then MsgBox("Invalid Party.", MsgBoxStyle.Information) : Exit Sub

        mQry = "SELECT Count(*) AS Cnt 
                FROM
                (
                SELECT DocID FROM SaleInvoice  WHERE SaleToParty = " & ReportFrm.FGetCode(1) & "
                UNION ALL 
                SELECT DocID FROM SaleInvoice  WHERE BillToParty = " & ReportFrm.FGetCode(1) & "
                UNION ALL 
                SELECT DocID FROM PurchInvoice  WHERE Vendor = " & ReportFrm.FGetCode(1) & "
                UNION ALL 
                SELECT DocID FROM PurchInvoice  WHERE BillToParty = " & ReportFrm.FGetCode(1) & "
                ) H "
        Dim TransCount As Int16 = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).executeScalar())
        If TransCount > 0 Then MsgBox("Customer or Supplier Not Allowed.", MsgBoxStyle.Information) : Exit Sub


        If MsgBox("Are you sure you want to proceed delete data ?" & vbNewLine & "This will wash selected data.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                mQry = "SELECT H.DocId
                        FROM Ledger H 
                        WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & "
                        And H.SubCode = " & ReportFrm.FGetCode(1) & " Group By H.DocId "
                Dim DtSelectedData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To DtSelectedData.Rows.Count - 1
                    bConStr = " Where SubCode = " & ReportFrm.FGetCode(1) & " AND DocId = '" & DtSelectedData.Rows(I)("DocId") & "' "
                    mQry = "DELETE FROM Ledger " & bConStr
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "DELETE FROM LedgerHeadDetail " & bConStr
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "DELETE FROM LedgerHead " & bConStr
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                Next

                'mQry = " DELETE FROM LedgerHeadDetail  Where DocId in (select L.DocId 
                '        from LedgerHeadDetail L 
                '        left join LedgerHead H on H.DocId = L.DocId
                '        Where H.DocId Is Null
                '        Group By L.DocId) "
                'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Call AgL.LogTableEntry(mSearchCode, ReportFrm.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), AgL.GCn, AgL.ECmd,,,,,, AgL.PubSiteCode, AgL.PubDivCode, mLogText)

                AgL.ETrans.Commit()
                mTrans = "Commit"
                MsgBox("Process Complete.", MsgBoxStyle.Information)
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

End Class
