Imports AgLibrary.ClsMain.agConstants

Public Class ClsExportDataForBranch

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""

    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout

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
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpPartySingleQry$ = " Select Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Customer','Supplier','Cash') "
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
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description   FROM Tag H "


    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""

    Private Const rowFromDate As Integer = 0
    Private Const rowToDate As Integer = 1
    Private Const rowReportType As Integer = 2
    Private Const rowParty As Integer = 3

    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            mQry = "Select 'Masters' as Code, 'Masters' as Name 
                    Union All Select 'Masters & Transactions' as Code, 'Masters & Transactions' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Masters & Transactions")
            ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpPartySingleQry, "",,, 300)
            ReportFrm.BtnPrint.Text = "Export"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
            If AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'D100004259'" Or AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'E100005835'" Or AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'D100016337'" Or AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'D100016336'" Or AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'D100025715'" Or AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'D100025716'" Or AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'D100027005'" Or AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'D100027015'" Then ' for Sadhvi Kanpur Branch & Jaunpur
                ProcExportStockIssueDataToSqlite()
            ElseIf AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'SADHVIBEN'" Or AgL.XNull(ReportFrm.FGetCode(rowParty)) = "'SADHVIBEM'" Then ' for Sadhvi Bhopal Branch
                ProcExportSaleInvoiceDataToSqlite_Sadhvi()
            End If
            'ProcExportSaleInvoiceDataToSqlite_Sadhvi()
        ElseIf ClsMain.FDivisionNameForCustomization(18) = "SHRI PARWATI SAREE" Then
            ProcExportSaleInvoiceDataToSqlite()
        ElseIf ClsMain.FDivisionNameForCustomization(13) = "JAIN BROTHERS" Or
                ClsMain.FDivisionNameForCustomization(11) = "BOOK SHOPEE" Then
            If AgL.PubSiteCode = "1" Then
                ProcExportStockIssueDataToSqlite_JainBrothersHeadOffice()
            Else
                ProcExportSaleInvoiceDataToSqlite_JainBrothersBranch()
            End If
        ElseIf ClsMain.FDivisionNameForCustomization(14) = "PRATHAM APPARE" Then
            ProcExportSaleInvoiceDataToSqlite_Pratham()
        ElseIf ClsMain.FDivisionNameForCustomization(9) = "GUR SHEEL" Then
            ProcExportSaleInvoiceDataToSqlite_Gurusheel()
        End If
    End Sub
    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub
    Private Sub ProcExportStockIssueDataToSqlite()
        Dim DtStockHead As DataTable
        Dim DtStockHeadDetail As DataTable
        Dim DtStockHeadDimensionDetail As DataTable
        Dim DtStock As DataTable
        Dim DtItemCategory As DataTable
        Dim DtItemGroup As DataTable
        Dim DtItem As DataTable
        Dim mStrMainQry As String = ""
        Dim mSaleToParty As String = ""
        Dim mPartyCode As String = ""
        mSaleToParty = AgL.XNull(ReportFrm.FGetCode(rowParty))


        If AgL.XNull(ReportFrm.FGetText(rowParty)) = "" Then
            MsgBox("Party Name is Required...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        mQry = "SELECT SG.ManualCode  FROM Subgroup SG WHERE SG.Subcode = " & mSaleToParty & ""
        mPartyCode = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar()

        mStrMainQry = "Select H.DocId From StockHead H
                    Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " 
                    And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " 
                    And H.V_Type = 'ISS' 
                    And H.Subcode = " & mSaleToParty & ""

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN StockHead H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStockHead = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN StockHeadDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStockHeadDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN StockHeadDimensionDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStockHeadDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN Stock H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStock = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "IC.", "OmsID")

        mQry = " Select Distinct " & mQry & ", IC.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN StockHeadDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                Where L.DocId Is Not Null "
        DtItemCategory = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "IG.", "Description,OmsID")
        mQry = " Select Distinct " & mQry & ", IfNull(Ig.PrintingDescription,Ig.Description) As Description, 
                IG.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN StockHeadDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                Where L.DocId Is Not Null "
        DtItemGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "I.", "Description,DisplayName,PurchaseRate,OmsID")
        mQry = " Select Distinct " & mQry & ", I.Specification || '-' || IfNull(Ig.PrintingDescription,Ig.Description) || '-' || Ic.Description As Description, 
                Null As DisplayName, I.Rate As PurchaseRate, I.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN StockHeadDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                Where L.DocId Is Not Null "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim SaveFileDialogBox As SaveFileDialog = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        SaveFileDialogBox.InitialDirectory = FilePath
        If ReportFrm.FGetText(rowFromDate) <> ReportFrm.FGetText(rowToDate) Then
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + mPartyCode + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "") + "_To_" + ReportFrm.FGetText(rowToDate).ToString.Replace("/", "")
        Else
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + mPartyCode + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "")
        End If
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = SaveFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        SQLite.SQLiteConnection.CreateFile(mDbPath)

        Dim Command As New SQLite.SQLiteCommand()
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Else
            Connection.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection.Open()
        Command.Connection = Connection
        Dim bTransaction As SQLite.SQLiteTransaction = Connection.BeginTransaction()
        Command.Transaction = bTransaction

        Try
            FExportToSqliteTable("Item", DtItemCategory, Connection, Command)
            FExportToSqliteTable("Item", DtItemGroup, Connection, Command)
            FExportToSqliteTable("Item", DtItem, Connection, Command)
            FExportToSqliteTable("StockHead", DtStockHead, Connection, Command)
            FExportToSqliteTable("StockHeadDetail", DtStockHeadDetail, Connection, Command)
            FExportToSqliteTable("StockHeadDimensionDetail", DtStockHeadDimensionDetail, Connection, Command)
            FExportToSqliteTable("Stock", DtStock, Connection, Command)




            bTransaction.Commit()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            bTransaction.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub


    Function GetTableColumnNameCsv(ByVal TableName As String, TableAlias As String, ExcludeColumns As String) As String
        Dim mQry$
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim mStr As String = ""

        If ExcludeColumns <> "" Then
            ExcludeColumns = ExcludeColumns & ","
            ExcludeColumns = ExcludeColumns.Replace(" ", "")
            ExcludeColumns = ExcludeColumns.Replace(",,", ",")
        End If


        If AgL.PubServerName = "" Then
            mQry = "PRAGMA table_info(" + TableName + ");"
            DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        Else
            mQry = "Select Column_Name as Name from Information_Schema.Columns Where Table_Name='" + TableName + "';"
            DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        End If

        ExcludeColumns = "|" + ExcludeColumns.Replace(",", "|,|") + "|"

        For I = 0 To DtTemp.Rows.Count - 1
            If Not ExcludeColumns.ToUpper.Contains("|" + UCase(DtTemp.Rows(I)("Name").ToString()) + "|") Then
                mStr += IIf(mStr = "", "", ",") + TableAlias + DtTemp.Rows(I)("name").ToString()
            End If
        Next

        GetTableColumnNameCsv = mStr
    End Function



    Private Sub ProcExportSaleInvoiceDataToSqlite()
        Dim DtSaleInvoice As DataTable
        Dim DtSaleInvoiceTrnSetting As DataTable
        Dim DtSaleInvoiceTransport As DataTable
        Dim DtSaleInvoicePayment As DataTable
        Dim DtSaleInvoiceDetail As DataTable
        Dim DtSaleInvoiceDimensionDetail As DataTable
        Dim DtSaleInvoiceDetailHelpValues As DataTable
        Dim DtLedger As DataTable
        Dim DtStock As DataTable
        Dim mStrMainQry As String = ""

        mStrMainQry = "Select H.DocId From SaleInvoice H
                    Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " 
                    And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " 
                    And H.V_Type = 'SID' "

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN SaleInvoice H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceTrnSetting H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceTrnSetting = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceTransport H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceTransport = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN SaleInvoicePayment H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoicePayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetailHelpValues H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetailHelpValues = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN Ledger H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN Stock H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStock = AgL.FillData(mQry, AgL.GCn).Tables(0)


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim SaveFileDialogBox As SaveFileDialog = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        SaveFileDialogBox.InitialDirectory = FilePath
        If ReportFrm.FGetText(rowFromDate) <> ReportFrm.FGetText(rowToDate) Then
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "") + "_To_" + ReportFrm.FGetText(rowToDate).ToString.Replace("/", "")
        Else
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "")
        End If
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = SaveFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        SQLite.SQLiteConnection.CreateFile(mDbPath)

        Dim Command As New SQLite.SQLiteCommand()
        Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Connection.Open()
        Command.Connection = Connection
        Dim bTransaction As SQLite.SQLiteTransaction = Connection.BeginTransaction()
        Command.Transaction = bTransaction

        Try
            FExportToSqliteTable("SaleInvoice", DtSaleInvoice, Connection, Command)
            FExportToSqliteTable("SaleInvoiceTrnSetting", DtSaleInvoiceTrnSetting, Connection, Command)
            FExportToSqliteTable("SaleInvoiceTransport", DtSaleInvoiceTransport, Connection, Command)
            FExportToSqliteTable("SaleInvoicePayment", DtSaleInvoicePayment, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetail", DtSaleInvoiceDetail, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetailHelpValues", DtSaleInvoiceDetailHelpValues, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDimensionDetail", DtSaleInvoiceDimensionDetail, Connection, Command)
            FExportToSqliteTable("Ledger", DtLedger, Connection, Command)
            FExportToSqliteTable("Stock", DtStock, Connection, Command)

            bTransaction.Commit()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            bTransaction.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Private Sub FExportToSqliteTable(bTableName As String, DtTable As DataTable,
                            Conn As SQLite.SQLiteConnection, Cmd As SQLite.SQLiteCommand)
        Dim DtFields As DataTable
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim StrQry As String = ""
        Dim StrInsertionQry As String = ""
        Dim StrValuesQry As String = ""

        mQry = " SELECT ORDINAL_POSITION, COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = '" & bTableName & "' "
        DtFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If Not AgL.IsTableExist(bTableName, Conn) Then
            For I = 0 To DtFields.Rows.Count - 1
                If I = 0 Then
                    StrQry = "CREATE TABLE [" & bTableName & "] ("
                    StrQry += "[" & AgL.XNull(DtFields.Rows(I)("COLUMN_NAME")) & "] 
                    " & AgL.XNull(DtFields.Rows(I)("DATA_TYPE")) &
                    " (" & AgL.VNull(DtFields.Rows(I)("CHARACTER_MAXIMUM_LENGTH")).ToString & ") 
                    " & IIf(AgL.XNull(DtFields.Rows(I)("IS_NULLABLE")) = "No", " Not Null", "Null") & ")"
                    AgL.Dman_ExecuteNonQry(StrQry, Conn, Cmd)
                Else
                    AgL.AddFieldSqlite(Conn, bTableName, AgL.XNull(DtFields.Rows(I)("COLUMN_NAME")),
                                   AgL.XNull(DtFields.Rows(I)("DATA_TYPE")) + "(" + AgL.VNull(DtFields.Rows(I)("CHARACTER_MAXIMUM_LENGTH")).ToString + ")", "",
                                   True)
                End If
            Next
        End If

        mQry = "PRAGMA table_info(" & bTableName & ");"
        Dim DtTableInfo As DataTable = AgL.FillData(mQry, Conn).Tables(0)

        For J = 0 To DtTable.Columns.Count - 1
            If J = 0 Then
                StrInsertionQry = " INSERT INTO " & bTableName & "(" & DtTable.Columns(J).ColumnName
                'ElseIf J = DtTable.Columns.Count - 1 Then
                '    StrInsertionQry += ", " & DtTable.Columns(J).ColumnName + ")"
            Else
                StrInsertionQry += ", " & DtTable.Columns(J).ColumnName
            End If
        Next
        StrInsertionQry += ")"

        For K = 0 To DtTable.Rows.Count - 1
            StrValuesQry = ""
            For J = 0 To DtTable.Columns.Count - 1
                If StrValuesQry = "" Then
                    StrValuesQry = " Values( " & AgL.Chk_Text(DtTable.Rows(K)(DtTable.Columns(J).ColumnName))
                Else
                    If DtTable.Columns(J).ColumnName.ToString.EndsWith("Date") Then
                        StrValuesQry += ", " & AgL.Chk_Date(AgL.XNull(DtTable.Rows(K)(DtTable.Columns(J).ColumnName)))
                    ElseIf DtTableInfo.Select("name = '" & DtTable.Columns(J).ColumnName.ToString & "'")(0)("Type") = "bit(0)" Then
                        StrValuesQry += ", " & Math.Abs(AgL.VNull(DtTable.Rows(K)(DtTable.Columns(J).ColumnName)))
                    Else
                        StrValuesQry += ", " & AgL.Chk_Text(AgL.XNull(DtTable.Rows(K)(DtTable.Columns(J).ColumnName)))
                    End If
                End If
            Next
            StrValuesQry += ")"
            AgL.Dman_ExecuteNonQry(StrInsertionQry + StrValuesQry, Conn, Cmd)
        Next
    End Sub
    Private Sub ProcExportStockIssueDataToSqlite_JainBrothersHeadOffice()
        Dim DtPurchInvoice As DataTable
        Dim DtPurchInvoiceDetail As DataTable
        Dim DtPurchInvoiceDimensionDetail As DataTable
        Dim DtStock As DataTable
        Dim DtItemCategory As DataTable
        Dim DtItemGroup As DataTable
        Dim DtItem As DataTable
        Dim DtItemState As DataTable
        Dim DtCatalog As DataTable
        Dim DtCatalogDetail As DataTable

        Dim mStrMainQry As String = ""

        mStrMainQry = "Select H.DocId From PurchInvoice H
                    Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " 
                    And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " 
                    And H.V_Type = 'ISS' "
        If AgL.XNull(ReportFrm.FGetText(rowReportType)) = "Masters" Then
            mStrMainQry += " And 1=2"
        End If

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN PurchInvoice H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtPurchInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN PurchInvoiceDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtPurchInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN PurchInvoiceDimensionDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtPurchInvoiceDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN Stock H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStock = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select Distinct C.Code, C.Description, C.IsDeleted, C.EntryBy, C.EntryDate, C.EntryType, C.EntryStatus, 
                    C.ApproveBy, C.ApproveDate, C.MoveToLog, C.MoveToLogDate, C.Status, C.Div_Code, C.UID, C.Code As OmsId, 
                    C.UploadDate, C.Site_Code, C.Specification  
                    From Catalog C 
                    Where C.Code Is Not Null "
        DtCatalog = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select Distinct C.Code, C.Sr, C.Item, C.Qty, C.Rate, C.UploadDate, C.DiscountPer, 
                    C.AdditionalDiscountPer, C.AdditionPer, C.Unit, C.ItemState 
                    From CatalogDetail C
                    Where C.Code Is Not Null "
        DtCatalogDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "IC.", "OmsID")

        mQry = "Select Distinct " & mQry & ", IC.Code as OmsID 
                From Item IC
                Where IC.V_type='" & ItemV_Type.ItemCategory & "' 
                And IC.Code Is Not Null "
        DtItemCategory = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "IG.", "OmsID")
        mQry = "Select Distinct " & mQry & ", IG.Code as OmsID 
                From Item IG
                Where IG.V_Type='" & ItemV_Type.ItemGroup & "'                 
                And IG.Code Is Not Null "
        DtItemGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "I.", "OmsID")
        mQry = "Select Distinct " & mQry & ", I.Code as OmsID 
                From Item I
                Where I.V_Type = '" & ItemV_Type.ItemState & "' 
                And I.Code Is Not Null"
        DtItemState = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "I.", "OmsID")
        mQry = "Select Distinct " & mQry & ", I.Code as OmsID
                From Item I 
                Where I.V_Type not In ('" & ItemV_Type.ItemCategory & "', '" & ItemV_Type.ItemGroup & "', '" & ItemV_Type.ItemState & "')
                And I.Code Is Not Null
                "

        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)





        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim SaveFileDialogBox As SaveFileDialog = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        SaveFileDialogBox.InitialDirectory = FilePath
        If ReportFrm.FGetText(rowFromDate) <> ReportFrm.FGetText(rowToDate) Then
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "") + "_To_" + ReportFrm.FGetText(rowToDate).ToString.Replace("/", "")
        Else
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "")
        End If
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = SaveFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        SQLite.SQLiteConnection.CreateFile(mDbPath)

        Dim Command As New SQLite.SQLiteCommand()
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Else
            Connection.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection.Open()
        Command.Connection = Connection
        Dim bTransaction As SQLite.SQLiteTransaction = Connection.BeginTransaction()
        Command.Transaction = bTransaction

        Try
            FExportToSqliteTable("Item", DtItemCategory, Connection, Command)
            FExportToSqliteTable("Item", DtItemGroup, Connection, Command)
            FExportToSqliteTable("Item", DtItem, Connection, Command)
            FExportToSqliteTable("Item", DtItemState, Connection, Command)
            FExportToSqliteTable("Catalog", DtCatalog, Connection, Command)
            FExportToSqliteTable("CatalogDetail", DtCatalogDetail, Connection, Command)
            FExportToSqliteTable("PurchInvoice", DtPurchInvoice, Connection, Command)
            FExportToSqliteTable("PurchInvoiceDetail", DtPurchInvoiceDetail, Connection, Command)
            FExportToSqliteTable("PurchInvoiceDimensionDetail", DtPurchInvoiceDimensionDetail, Connection, Command)
            FExportToSqliteTable("Stock", DtStock, Connection, Command)

            bTransaction.Commit()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            bTransaction.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Private Sub ProcExportSaleInvoiceDataToSqlite_Pratham()
        Dim DtSaleInvoice As DataTable
        Dim DtSaleInvoiceDetail As DataTable
        Dim DtSaleInvoiceDetailSku As DataTable
        Dim DtSaleInvoiceDimensionDetail As DataTable
        Dim DtSaleInvoiceDimensionDetailSku As DataTable
        Dim DtItemCategory As DataTable
        Dim DtItemGroup As DataTable
        Dim DtDimension1 As DataTable
        Dim DtDimension2 As DataTable
        Dim DtDimension3 As DataTable
        Dim DtDimension4 As DataTable
        Dim DtSize As DataTable
        Dim DtItem As DataTable
        Dim DtBarcode As DataTable

        Dim mStrMainQry As String = ""

        mStrMainQry = "Select H.DocId From SaleInvoice H
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " 
                    And H.SaleToParty = 'D100000069'
                    And Vt.NCat = '" & Ncat.SaleInvoice & "' "
        If AgL.XNull(ReportFrm.FGetText(rowReportType)) = "Masters" Then
            mStrMainQry += " And 1=2"
        End If

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN SaleInvoice H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetailSku H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetailSku = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetailSku H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDimensionDetailSku = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "IC.", "OmsID")
        mQry = "Select Distinct " & mQry & ", IC.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                Where IC.V_type='" & ItemV_Type.ItemCategory & "' 
                And IC.Code Is Not Null "
        DtItemCategory = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "IG.", "OmsID")
        mQry = "Select Distinct " & mQry & ", IG.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                Where IG.V_Type='" & ItemV_Type.ItemGroup & "'                 
                And IG.Code Is Not Null "
        DtItemGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "D1.", "OmsID")
        mQry = "Select Distinct " & mQry & ", D1.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item D1 On I.Dimension1 = D1.Code
                Where D1.V_Type = '" & ItemV_Type.Dimension1 & "'
                And D1.Code Is Not Null "
        DtDimension1 = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "D2.", "OmsID")
        mQry = "Select Distinct " & mQry & ", D2.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item D2 On I.Dimension2 = D2.Code
                Where D2.V_Type = '" & ItemV_Type.Dimension2 & "'
                And D2.Code Is Not Null "
        DtDimension2 = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "D3.", "OmsID")
        mQry = "Select Distinct " & mQry & ", D3.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item D3 On I.Dimension3 = D3.Code
                Where D3.V_Type = '" & ItemV_Type.Dimension3 & "'
                And D3.Code Is Not Null "
        DtDimension3 = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "D4.", "OmsID")
        mQry = "Select Distinct " & mQry & ", D4.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item D4 On I.Dimension4 = D4.Code
                Where D4.V_Type = '" & ItemV_Type.Dimension4 & "'
                And D4.Code Is Not Null "
        DtDimension4 = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "S.", "OmsID")
        mQry = "Select Distinct " & mQry & ", S.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item S On I.Size = S.Code
                Where S.V_Type = '" & ItemV_Type.SIZE & "'
                And S.Code Is Not Null "
        DtSize = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "I.", "OmsID")
        mQry = "Select Distinct " & mQry & ", I.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                Where I.V_Type not In ('" & ItemV_Type.ItemCategory & "', '" & ItemV_Type.ItemGroup & "', '" & ItemV_Type.ItemState & "')
                And I.Code Is Not Null "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Barcode", "Bc.", "GenDocId,OmsID")
        mQry = "Select Distinct " & mQry & ", Bc.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Barcode Bc On I.Barcode = Bc.Code
                Where Bc.Code Is Not Null "
        DtBarcode = AgL.FillData(mQry, AgL.GCn).Tables(0)



        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim SaveFileDialogBox As SaveFileDialog = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        SaveFileDialogBox.InitialDirectory = FilePath
        If ReportFrm.FGetText(rowFromDate) <> ReportFrm.FGetText(rowToDate) Then
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "") + "_To_" + ReportFrm.FGetText(rowToDate).ToString.Replace("/", "")
        Else
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "")
        End If
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = SaveFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        SQLite.SQLiteConnection.CreateFile(mDbPath)

        Dim Command As New SQLite.SQLiteCommand()
        'If AgL.PubIsDatabaseEncrypted = "N" Then
        '    Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        'Else
        Connection.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        'End If
        Connection.Open()
        Command.Connection = Connection
        Dim bTransaction As SQLite.SQLiteTransaction = Connection.BeginTransaction()
        Command.Transaction = bTransaction

        Try
            FExportToSqliteTable("Item", DtItemCategory, Connection, Command)
            FExportToSqliteTable("Item", DtItemGroup, Connection, Command)
            FExportToSqliteTable("Item", DtDimension1, Connection, Command)
            FExportToSqliteTable("Item", DtDimension2, Connection, Command)
            FExportToSqliteTable("Item", DtDimension3, Connection, Command)
            FExportToSqliteTable("Item", DtDimension4, Connection, Command)
            FExportToSqliteTable("Item", DtSize, Connection, Command)
            FExportToSqliteTable("Item", DtItem, Connection, Command)
            FExportToSqliteTable("Barcode", DtBarcode, Connection, Command)
            FExportToSqliteTable("SaleInvoice", DtSaleInvoice, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetail", DtSaleInvoiceDetail, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetailSku", DtSaleInvoiceDetailSku, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDimensionDetail", DtSaleInvoiceDimensionDetail, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDimensionDetailSku", DtSaleInvoiceDimensionDetailSku, Connection, Command)

            bTransaction.Commit()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            bTransaction.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Private Sub ProcExportSaleInvoiceDataToSqlite_JainBrothersBranch()
        'Dim DtSaleInvoice As DataTable
        'Dim DtSaleInvoiceDetail As DataTable
        'Dim DtSaleInvoiceDetailSku As DataTable
        'Dim DtSaleInvoiceDimensionDetail As DataTable
        'Dim DtSaleInvoiceDimensionDetailSku As DataTable
        'Dim DtItemCategory As DataTable
        'Dim DtItemGroup As DataTable
        'Dim DtItem As DataTable
        'Dim DtSubGroup As DataTable


        'Dim mStrMainQry As String = ""

        'mStrMainQry = "Select H.DocId From SaleInvoice H
        '            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
        '            Where Vt.NCat = '" & Ncat.SaleInvoice & "' "
        'If AgL.XNull(ReportFrm.FGetText(rowReportType)) = "Masters" Then
        '    mStrMainQry += " And 1=2"
        'End If

        'mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
        '            LEFT JOIN SaleInvoice H ON VMain.DocId = H.DocId 
        '            Where H.DocId Is Not Null "
        'DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetail H ON VMain.DocId = H.DocId 
        '            Where H.DocId Is Not Null "
        'DtSaleInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetailSku H ON VMain.DocId = H.DocId 
        '            Where H.DocId Is Not Null "
        'DtSaleInvoiceDetailSku = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetail H ON VMain.DocId = H.DocId 
        '            Where H.DocId Is Not Null "
        'DtSaleInvoiceDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetailSku H ON VMain.DocId = H.DocId 
        '            Where H.DocId Is Not Null "
        'DtSaleInvoiceDimensionDetailSku = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = GetTableColumnNameCsv("Item", "IC.", "OmsID")
        'mQry = "Select Distinct " & mQry & ", IC.Code as OmsID 
        '        From (" & mStrMainQry & ") As VMain 
        '        LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
        '        LEFT JOIN Item I On L.Item = I.Code
        '        LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
        '        Where IC.V_type='" & ItemV_Type.ItemCategory & "' 
        '        And IC.Code Is Not Null "
        'DtItemCategory = AgL.FillData(mQry, AgL.GCn).Tables(0)


        'mQry = GetTableColumnNameCsv("Item", "IG.", "OmsID")
        'mQry = "Select Distinct " & mQry & ", IG.Code as OmsID 
        '        From (" & mStrMainQry & ") As VMain 
        '        LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
        '        LEFT JOIN Item I On L.Item = I.Code
        '        LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
        '        Where IG.V_Type='" & ItemV_Type.ItemGroup & "'                 
        '        And IG.Code Is Not Null "
        'DtItemGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = GetTableColumnNameCsv("Item", "I.", "OmsID")
        'mQry = "Select Distinct " & mQry & ", I.Code as OmsID
        '        From (" & mStrMainQry & ") As VMain 
        '        LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
        '        LEFT JOIN Item I On L.Item = I.Code
        '        Where I.V_Type not In ('" & ItemV_Type.ItemCategory & "', '" & ItemV_Type.ItemGroup & "', '" & ItemV_Type.ItemState & "')
        '        And I.Code Is Not Null "
        'DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = GetTableColumnNameCsv("SubGroup", "Sg.", "OmsID")
        'mQry = "Select " & mQry & ", Sg.SubCode as OmsID From SubGroup Sg "
        'DtSubGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = "Select * From City C "
        'DtCity = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = "Select * From State S "
        'DtState = AgL.FillData(mQry, AgL.GCn).Tables(0)

        'mQry = "Select * From Area A "
        'DtArea = AgL.FillData(mQry, AgL.GCn).Tables(0)



        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim SaveFileDialogBox As SaveFileDialog = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        SaveFileDialogBox.InitialDirectory = FilePath
        If ReportFrm.FGetText(rowFromDate) <> ReportFrm.FGetText(rowToDate) Then
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "") + "_To_" + ReportFrm.FGetText(rowToDate).ToString.Replace("/", "")
        Else
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "")
        End If
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = SaveFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        SQLite.SQLiteConnection.CreateFile(mDbPath)

        Dim Command As New SQLite.SQLiteCommand()
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Else
            Connection.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection.Open()
        Command.Connection = Connection
        Dim bTransaction As SQLite.SQLiteTransaction = Connection.BeginTransaction()
        Command.Transaction = bTransaction

        Try
            mQry = "SELECT * FROM INFORMATION_SCHEMA.Tables 
                    WHERE TABLE_TYPE   = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%-%'
                    And TABLE_NAME Not In ('LogTable','Stock')
                    Order By TABLE_NAME "
            Dim DtTables As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I As Integer = 0 To DtTables.Rows.Count - 1
                If AgL.XNull(DtTables.Rows(I)("TABLE_NAME")) = "SaleInvoice" Then
                    mQry = " Select * From  " & AgL.XNull(DtTables.Rows(I)("TABLE_NAME")) & " Where V_Date Between " & AgL.Chk_Date(AgL.XNull(ReportFrm.FGetText(rowFromDate))) & " And " & AgL.Chk_Date(AgL.XNull(ReportFrm.FGetText(rowToDate))) & " "
                    Dim DtData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    FExportToSqliteTable(AgL.XNull(DtTables.Rows(I)("TABLE_NAME")), DtData, Connection, Command)
                Else
                    mQry = " Select * From  " & AgL.XNull(DtTables.Rows(I)("TABLE_NAME")) & " "
                    Dim DtData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    FExportToSqliteTable(AgL.XNull(DtTables.Rows(I)("TABLE_NAME")), DtData, Connection, Command)
                End If
            Next

            bTransaction.Commit()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            bTransaction.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Private Sub ProcExportSaleInvoiceDataToSqlite_Gurusheel()
        Dim DtSaleInvoice As DataTable
        Dim DtSaleInvoicePayment As DataTable
        Dim DtSaleInvoiceDetail As DataTable
        Dim DtSaleInvoiceDetailSku As DataTable
        Dim DtSaleInvoiceDimensionDetail As DataTable
        Dim DtSaleInvoiceDimensionDetailSku As DataTable
        Dim DtItemCategory As DataTable
        Dim DtItemGroup As DataTable
        Dim DtDimension1 As DataTable
        Dim DtDimension2 As DataTable
        Dim DtDimension3 As DataTable
        Dim DtDimension4 As DataTable
        Dim DtSize As DataTable
        Dim DtItem As DataTable
        Dim DtBarcode As DataTable
        Dim mSaleToParty As String = ""
        Dim mStrMainQry As String = ""

        If AgL.XNull(ReportFrm.FGetText(rowParty)) = "" Then
            MsgBox("Party Name is Required...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        mSaleToParty = AgL.XNull(ReportFrm.FGetCode(rowParty))

        mStrMainQry = "Select H.DocId From SaleInvoice H
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SaleInvoicePayment Sip On H.DocId = Sip.DocId
                    Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " 
                    And Sip.PostToAc = " & mSaleToParty & "
                    And Vt.NCat = '" & Ncat.SaleInvoice & "' "
        If AgL.XNull(ReportFrm.FGetText(rowReportType)) = "Masters" Then
            mStrMainQry += " And 1=2"
        End If

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN SaleInvoice H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN SaleInvoicePayment H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoicePayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetailSku H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetailSku = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetailSku H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDimensionDetailSku = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "IC.", "OmsID")
        mQry = "Select Distinct " & mQry & ", IC.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                Where IC.V_type='" & ItemV_Type.ItemCategory & "' 
                And IC.Code Is Not Null "
        DtItemCategory = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "IG.", "OmsID")
        mQry = "Select Distinct " & mQry & ", IG.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                Where IG.V_Type='" & ItemV_Type.ItemGroup & "'                 
                And IG.Code Is Not Null "
        DtItemGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "D1.", "OmsID")
        mQry = "Select Distinct " & mQry & ", D1.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item D1 On I.Dimension1 = D1.Code
                Where D1.V_Type = '" & ItemV_Type.Dimension1 & "'
                And D1.Code Is Not Null "
        DtDimension1 = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "D2.", "OmsID")
        mQry = "Select Distinct " & mQry & ", D2.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item D2 On I.Dimension2 = D2.Code
                Where D2.V_Type = '" & ItemV_Type.Dimension2 & "'
                And D2.Code Is Not Null "
        DtDimension2 = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "D3.", "OmsID")
        mQry = "Select Distinct " & mQry & ", D3.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item D3 On I.Dimension3 = D3.Code
                Where D3.V_Type = '" & ItemV_Type.Dimension3 & "'
                And D3.Code Is Not Null "
        DtDimension3 = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "D4.", "OmsID")
        mQry = "Select Distinct " & mQry & ", D4.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item D4 On I.Dimension4 = D4.Code
                Where D4.V_Type = '" & ItemV_Type.Dimension4 & "'
                And D4.Code Is Not Null "
        DtDimension4 = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "S.", "OmsID")
        mQry = "Select Distinct " & mQry & ", S.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item S On I.Size = S.Code
                Where S.V_Type = '" & ItemV_Type.SIZE & "'
                And S.Code Is Not Null "
        DtSize = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "I.", "OmsID")
        mQry = "Select Distinct " & mQry & ", I.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                Where I.V_Type not In ('" & ItemV_Type.ItemCategory & "', '" & ItemV_Type.ItemGroup & "', '" & ItemV_Type.ItemState & "')
                And I.Code Is Not Null "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Barcode", "Bc.", "GenDocId,OmsID")
        mQry = "Select Distinct " & mQry & ", Bc.Code as OmsID
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Barcode Bc On L.Barcode = Bc.Code
                Where Bc.Code Is Not Null "
        DtBarcode = AgL.FillData(mQry, AgL.GCn).Tables(0)



        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim SaveFileDialogBox As SaveFileDialog = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        SaveFileDialogBox.InitialDirectory = FilePath
        If ReportFrm.FGetText(rowFromDate) <> ReportFrm.FGetText(rowToDate) Then
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "") + "_To_" + ReportFrm.FGetText(rowToDate).ToString.Replace("/", "")
        Else
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "")
        End If
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = SaveFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        SQLite.SQLiteConnection.CreateFile(mDbPath)

        Dim Command As New SQLite.SQLiteCommand()
        'If AgL.PubIsDatabaseEncrypted = "N" Then
        '    Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        'Else
        Connection.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        'End If
        Connection.Open()
        Command.Connection = Connection
        Dim bTransaction As SQLite.SQLiteTransaction = Connection.BeginTransaction()
        Command.Transaction = bTransaction

        Try
            FExportToSqliteTable("Item", DtItemCategory, Connection, Command)
            FExportToSqliteTable("Item", DtItemGroup, Connection, Command)
            FExportToSqliteTable("Item", DtDimension1, Connection, Command)
            FExportToSqliteTable("Item", DtDimension2, Connection, Command)
            FExportToSqliteTable("Item", DtDimension3, Connection, Command)
            FExportToSqliteTable("Item", DtDimension4, Connection, Command)
            FExportToSqliteTable("Item", DtSize, Connection, Command)
            FExportToSqliteTable("Item", DtItem, Connection, Command)
            FExportToSqliteTable("Barcode", DtBarcode, Connection, Command)
            FExportToSqliteTable("SaleInvoice", DtSaleInvoice, Connection, Command)
            FExportToSqliteTable("SaleInvoicePayment", DtSaleInvoicePayment, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetail", DtSaleInvoiceDetail, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetailSku", DtSaleInvoiceDetailSku, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDimensionDetail", DtSaleInvoiceDimensionDetail, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDimensionDetailSku", DtSaleInvoiceDimensionDetailSku, Connection, Command)

            bTransaction.Commit()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            bTransaction.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub











    Private Sub ProcExportSaleInvoiceDataToSqlite_Sadhvi()
        Dim DtSaleInvoice As DataTable
        Dim DtSaleInvoiceDetail As DataTable
        Dim DtSaleInvoiceDetailSku As DataTable
        Dim DtSaleInvoiceDimensionDetail As DataTable
        Dim DtSaleInvoiceDimensionDetailSku As DataTable
        Dim DtStock As DataTable
        Dim DtItemCategory As DataTable
        Dim DtItemGroup As DataTable
        Dim DtItem As DataTable
        Dim mStrMainQry As String = ""
        Dim mSaleToParty As String = ""
        Dim mPartyCode As String = ""

        If AgL.XNull(ReportFrm.FGetText(rowParty)) = "" Then
            MsgBox("Party Name is Required...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        mSaleToParty = AgL.XNull(ReportFrm.FGetCode(rowParty))

        mQry = "SELECT SG.ManualCode  FROM Subgroup SG WHERE SG.Subcode = " & mSaleToParty & ""
        mPartyCode = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar()


        mStrMainQry = "Select H.DocId From SaleInvoice H
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " 
                    And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " 
                    And H.SaleToParty = " & mSaleToParty & "
                    And Vt.NCat = '" & Ncat.SaleInvoice & "' "

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN SaleInvoice H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetailSku H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetailSku = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetailSku H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDimensionDetailSku = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN Stock H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStock = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = GetTableColumnNameCsv("Item", "IC.", "OmsID")

        mQry = " Select Distinct " & mQry & ", IC.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                Where L.DocId Is Not Null "
        DtItemCategory = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "IG.", "Description,OmsID")
        mQry = " Select Distinct " & mQry & ", IfNull(Ig.PrintingDescription,Ig.Description) As Description, 
                IG.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                Where L.DocId Is Not Null "
        DtItemGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = GetTableColumnNameCsv("Item", "I.", "Description,DisplayName,PurchaseRate,OmsID")
        mQry = " Select Distinct " & mQry & ", I.Specification || '-' || IfNull(Ig.PrintingDescription,Ig.Description) || '-' || Ic.Description As Description, 
                Null As DisplayName, I.Rate As PurchaseRate, I.Code as OmsID 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN SaleInvoiceDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                Where L.DocId Is Not Null "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim SaveFileDialogBox As SaveFileDialog = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        SaveFileDialogBox.InitialDirectory = FilePath
        If ReportFrm.FGetText(rowFromDate) <> ReportFrm.FGetText(rowToDate) Then
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + mPartyCode + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "") + "_To_" + ReportFrm.FGetText(rowToDate).ToString.Replace("/", "")
        Else
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + mPartyCode + "_" + ReportFrm.FGetText(rowFromDate).ToString.Replace("/", "")
        End If
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = SaveFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        SQLite.SQLiteConnection.CreateFile(mDbPath)

        Dim Command As New SQLite.SQLiteCommand()
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Else
            Connection.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection.Open()
        Command.Connection = Connection
        Dim bTransaction As SQLite.SQLiteTransaction = Connection.BeginTransaction()
        Command.Transaction = bTransaction

        Try
            FExportToSqliteTable("Item", DtItemCategory, Connection, Command)
            FExportToSqliteTable("Item", DtItemGroup, Connection, Command)
            FExportToSqliteTable("Item", DtItem, Connection, Command)
            FExportToSqliteTable("SaleInvoice", DtSaleInvoice, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetail", DtSaleInvoiceDetail, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetailSku", DtSaleInvoiceDetailSku, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDimensionDetail", DtSaleInvoiceDimensionDetail, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDimensionDetailSku", DtSaleInvoiceDimensionDetailSku, Connection, Command)
            FExportToSqliteTable("Stock", DtStock, Connection, Command)




            bTransaction.Commit()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            bTransaction.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub

End Class
