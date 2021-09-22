Imports AgLibrary.ClsMain.agConstants
Imports AgTemplate.ClsMain
Imports System.Threading
Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Data.SqlClient

Public Class FrmImportDataFromBranch
    Dim mQry As String = ""
    Dim mTrans As String = ""
    Dim Connection_Pakka As New SQLite.SQLiteConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnImport.Click
        If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
            ProcImportStockIssueDataFromSqlite()
        ElseIf ClsMain.FDivisionNameForCustomization(18) = "SHRI PARWATI SAREE" Then
            ProcImportSaleInvoiceDataFromSqlite()
        End If
    End Sub

    Private Sub ProcImportSaleInvoiceDataFromSqlite()
        Dim mStrMainQry As String = ""
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim bSelectionQry As String = ""
        Dim mSqliteDataQry As String = ""


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Connection.Open()

        Dim mSqlConn As New SqlClient.SqlConnection
        Dim mSqlCmd As New SqlClient.SqlCommand
        Dim mSqlTrans As SqlClient.SqlTransaction

        mSqlConn.ConnectionString = AgL.GCn.ConnectionString
        mSqlConn.Open()
        mSqlCmd.Connection = mSqlConn
        mSqlTrans = mSqlConn.BeginTransaction()
        mSqlCmd.Transaction = mSqlTrans

        Try
            FImportDataFromSqliteTable("SaleInvoice", "H.DocId = H_Temp.DocId", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceTrnSetting", "H.DocId = H_Temp.DocId", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceTransport", "H.DocId = H_Temp.DocId", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoicePayment", "H.DocId = H_Temp.DocId And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceDetail", "H.DocId = H_Temp.DocId And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceDetailHelpValues", "H.DocId = H_Temp.DocId And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceDimensionDetail", "H.DocId = H_Temp.DocId And H.TSr = H_Temp.TSr And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("Ledger", "H.DocId = H_Temp.DocId And H.V_SNo = H_Temp.V_SNo", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("Stock", "H.DocId = H_Temp.DocId And H.TSr = H_Temp.TSr And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)

            mQry = "UPDATE Voucher_Prefix
                    SET Voucher_Prefix.Start_Srl_No = V1.V_No_Max
                    FROM (
	                    SELECT H.V_Type, H.V_Prefix, IfNull(Max(H.V_No),0) AS V_No_Max
	                    FROM SaleInvoice H
	                    WHERE H.V_Type = 'SID'
	                    GROUP BY H.V_Type, H.V_Prefix
                    ) AS V1 WHERE V1.V_Type = Voucher_Prefix.V_Type AND V1.V_Prefix = Voucher_Prefix.Prefix"
            AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)

            mSqlTrans.Commit()
            mSqlConn.Close()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            mSqlTrans.Rollback()
            mSqlConn.Close()
            Connection.Close()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ProcImportStockIssueDataFromSqlite()
        Dim mStrMainQry As String = ""
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim bSelectionQry As String = ""
        Dim mSqliteDataQry As String = ""


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Else
            Connection.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection.Open()


        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"

        Try
            FImportDataFromSqliteTable("Item", "H.Code = H_Temp.Code", "Code", Connection, AgL.GCn, AgL.ECmd, mDbPath)

            mQry = " INSERT INTO RateList(Code, WEF, EntryBy, EntryDate, EntryType,
                    EntryStatus, Status, Div_Code)
                    Select I.Code, " & AgL.Chk_Date(AgL.PubLoginDate) & ",
                    " & AgL.Chk_Text(AgL.PubUserName) & ", 
                    " & AgL.Chk_Date(AgL.PubLoginDate) & ", 
                    'A', 'Open', I.Status, I.Div_Code
                    From Item I 
                    LEFT JOIN RateList H On I.Code = H.Code 
                    Where H.Code Is Null "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "INSERT INTO RateListDetail(Code, Sr, Item, RateType, Rate) 
                  Select I.Code, 0,  I.Code, NULL, I.Rate
                  From Item I 
                  LEFT JOIN RateListDetail L On I.Code = L.Code 
                  Where L.Code Is Null "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = " Select H.*
                    From StockHead H "
            Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)

            mQry = " Select H.V_Type, H.ManualRefNo, I.Description As ItemDesc,
                L.*
                From StockHead H 
                LEFT JOIN StockHeadDetail L ON H.DocID = L.DocID
                LEFT JOIN Item I ON L.Item = I.Code "
            Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)

            mQry = " Select * From PurchInvoice "
            Dim DtPurchInvoice As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)


            For I = 0 To DtHeaderSource.Rows.Count - 1
                If DtPurchInvoice.Select("OMSId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'").Length = 0 Then
                    Dim Tot_Gross_Amount As Double = 0
                    Dim Tot_Taxable_Amount As Double = 0
                    Dim Tot_Tax1 As Double = 0
                    Dim Tot_Tax2 As Double = 0
                    Dim Tot_Tax3 As Double = 0
                    Dim Tot_Tax4 As Double = 0
                    Dim Tot_Tax5 As Double = 0
                    Dim Tot_SubTotal1 As Double = 0


                    Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect.StructPurchInvoice
                    Dim PurchInvoiceTable As New FrmPurchInvoiceDirect.StructPurchInvoice

                    PurchInvoiceTable.DocID = ""
                    PurchInvoiceTable.V_Type = "PI"
                    PurchInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                    PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                    PurchInvoiceTable.Div_Code = AgL.PubDivCode
                    PurchInvoiceTable.V_No = 0
                    PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.ManualRefNo = ""
                    PurchInvoiceTable.Vendor = ""
                    PurchInvoiceTable.VendorName = "SADHVI ENTERPRISES"
                    PurchInvoiceTable.AgentCode = ""
                    PurchInvoiceTable.AgentName = ""
                    PurchInvoiceTable.BillToPartyCode = ""
                    PurchInvoiceTable.BillToPartyName = "SADHVI ENTERPRISES"
                    PurchInvoiceTable.VendorAddress = ""
                    PurchInvoiceTable.VendorCity = ""
                    PurchInvoiceTable.VendorMobile = ""
                    PurchInvoiceTable.VendorSalesTaxNo = ""
                    PurchInvoiceTable.SalesTaxGroupParty =
                    PurchInvoiceTable.PlaceOfSupply = ""
                    PurchInvoiceTable.StructureCode = ""
                    PurchInvoiceTable.CustomFields = ""
                    PurchInvoiceTable.VendorDocNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                    PurchInvoiceTable.VendorDocDate = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.ReferenceDocId = ""
                    PurchInvoiceTable.Tags = ""
                    PurchInvoiceTable.Remarks = IIf(AgL.XNull(DtHeaderSource.Rows(I)("Div_Code")) = "E", "From Sadhvi Embroidery ", "From Sadhvi Enterprises ") +
                        AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
                    PurchInvoiceTable.Status = "Active"
                    PurchInvoiceTable.EntryBy = AgL.PubUserName
                    PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    PurchInvoiceTable.ApproveBy = ""
                    PurchInvoiceTable.ApproveDate = ""
                    PurchInvoiceTable.MoveToLog = ""
                    PurchInvoiceTable.MoveToLogDate = ""
                    PurchInvoiceTable.UploadDate = ""
                    PurchInvoiceTable.OmsId = AgL.XNull(DtHeaderSource.Rows(I)("DocId"))
                    PurchInvoiceTable.LockText = "Synced From Other Database."

                    PurchInvoiceTable.Gross_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Gross_Amount"))
                    PurchInvoiceTable.Taxable_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Taxable_Amount"))
                    PurchInvoiceTable.Tax1 = AgL.VNull(DtHeaderSource.Rows(I)("Tax1"))
                    PurchInvoiceTable.Tax2 = AgL.VNull(DtHeaderSource.Rows(I)("Tax2"))
                    PurchInvoiceTable.Tax3 = AgL.VNull(DtHeaderSource.Rows(I)("Tax3"))
                    PurchInvoiceTable.Tax4 = AgL.VNull(DtHeaderSource.Rows(I)("Tax4"))
                    PurchInvoiceTable.Tax5 = AgL.VNull(DtHeaderSource.Rows(I)("Tax5"))
                    PurchInvoiceTable.SubTotal1 = AgL.VNull(DtHeaderSource.Rows(I)("SubTotal1"))
                    PurchInvoiceTable.Other_Charge = AgL.VNull(DtHeaderSource.Rows(I)("Other_Charge"))
                    PurchInvoiceTable.Deduction = AgL.VNull(DtHeaderSource.Rows(I)("Deduction"))
                    PurchInvoiceTable.Round_Off = AgL.VNull(DtHeaderSource.Rows(I)("Round_Off"))
                    PurchInvoiceTable.Net_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Net_Amount"))

                    Dim DtPurchInvoiceDetail_ForHeader As New DataTable
                    For M As Integer = 0 To DtLineDetailSource.Columns.Count - 1
                        Dim DColumn As New DataColumn
                        DColumn.ColumnName = DtLineDetailSource.Columns(M).ColumnName
                        DtPurchInvoiceDetail_ForHeader.Columns.Add(DColumn)
                    Next

                    Dim DtRowPurchInvoiceDetail_ForHeader As DataRow() = DtLineDetailSource.Select("DocId = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("DocId"))))
                    If DtRowPurchInvoiceDetail_ForHeader.Length > 0 Then
                        For M As Integer = 0 To DtRowPurchInvoiceDetail_ForHeader.Length - 1
                            DtPurchInvoiceDetail_ForHeader.Rows.Add()
                            For N As Integer = 0 To DtPurchInvoiceDetail_ForHeader.Columns.Count - 1
                                DtPurchInvoiceDetail_ForHeader.Rows(M)(N) = DtRowPurchInvoiceDetail_ForHeader(M)(N)
                            Next
                        Next
                    End If


                    For J = 0 To DtPurchInvoiceDetail_ForHeader.Rows.Count - 1
                        PurchInvoiceTable.Line_Sr = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr"))
                        PurchInvoiceTable.Line_ItemCode = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Item"))
                        PurchInvoiceTable.Line_ItemName = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemDesc"))
                        PurchInvoiceTable.Line_Specification = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Specification"))
                        PurchInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SalesTaxGroupItem"))
                        PurchInvoiceTable.Line_ReferenceNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ReferenceNo"))
                        PurchInvoiceTable.Line_DocQty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DocQty"))
                        PurchInvoiceTable.Line_FreeQty = 0
                        PurchInvoiceTable.Line_Qty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Qty"))
                        PurchInvoiceTable.Line_Unit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Unit"))
                        PurchInvoiceTable.Line_Pcs = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Pcs"))
                        PurchInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("UnitMultiplier"))
                        PurchInvoiceTable.Line_DealUnit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DealUnit"))
                        PurchInvoiceTable.Line_DocDealQty = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DealQty"))

                        PurchInvoiceTable.Line_OmsId = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DocId")) + AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr"))
                        PurchInvoiceTable.Line_Rate = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Rate"))
                        PurchInvoiceTable.Line_DiscountPer = 0
                        PurchInvoiceTable.Line_DiscountAmount = 0
                        PurchInvoiceTable.Line_AdditionalDiscountPer = 0
                        PurchInvoiceTable.Line_AdditionalDiscountAmount = 0
                        PurchInvoiceTable.Line_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                        PurchInvoiceTable.Line_Remark = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Remark"))
                        PurchInvoiceTable.Line_BaleNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("BaleNo"))
                        PurchInvoiceTable.Line_LotNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("LotNo"))
                        PurchInvoiceTable.Line_ReferenceDocId = ""
                        PurchInvoiceTable.Line_GrossWeight = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("GrossWeight"))
                        PurchInvoiceTable.Line_NetWeight = 0
                        PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                        PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                        PurchInvoiceTable.Line_Tax1_Per = 0
                        PurchInvoiceTable.Line_Tax1 = 0
                        PurchInvoiceTable.Line_Tax2_Per = 0
                        PurchInvoiceTable.Line_Tax2 = 0
                        PurchInvoiceTable.Line_Tax3_Per = 0
                        PurchInvoiceTable.Line_Tax3 = 0
                        PurchInvoiceTable.Line_Tax4_Per = 0
                        PurchInvoiceTable.Line_Tax4 = 0
                        PurchInvoiceTable.Line_Tax5_Per = 0
                        PurchInvoiceTable.Line_Tax5 = 0
                        PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                        PurchInvoiceTable.Line_Other_Charge = 0
                        PurchInvoiceTable.Line_Deduction = 0
                        PurchInvoiceTable.Line_Round_Off = 0
                        PurchInvoiceTable.Line_Net_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))

                        'For Header Values
                        Tot_Gross_Amount += PurchInvoiceTable.Line_Gross_Amount
                        Tot_Taxable_Amount += PurchInvoiceTable.Line_Taxable_Amount
                        Tot_Tax1 += PurchInvoiceTable.Line_Tax1
                        Tot_Tax2 += PurchInvoiceTable.Line_Tax2
                        Tot_Tax3 += PurchInvoiceTable.Line_Tax3
                        Tot_Tax4 += PurchInvoiceTable.Line_Tax4
                        Tot_Tax5 += PurchInvoiceTable.Line_Tax5
                        Tot_SubTotal1 += PurchInvoiceTable.Line_SubTotal1


                        PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                        ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                    Next

                    PurchInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
                    PurchInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
                    PurchInvoiceTableList(0).Tax1 = Tot_Tax1
                    PurchInvoiceTableList(0).Tax2 = Tot_Tax2
                    PurchInvoiceTableList(0).Tax3 = Tot_Tax3
                    PurchInvoiceTableList(0).Tax4 = Tot_Tax4
                    PurchInvoiceTableList(0).Tax5 = Tot_Tax5
                    PurchInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
                    PurchInvoiceTableList(0).Other_Charge = 0
                    PurchInvoiceTableList(0).Deduction = 0
                    PurchInvoiceTableList(0).Round_Off = Math.Round(Math.Round(PurchInvoiceTableList(0).SubTotal1) - PurchInvoiceTableList(0).SubTotal1, 2)
                    PurchInvoiceTableList(0).Net_Amount = Math.Round(PurchInvoiceTableList(0).SubTotal1)


                    FrmPurchInvoiceDirect.InsertPurchInvoice(PurchInvoiceTableList)
                End If
            Next


            AgL.ETrans.Commit()
            mTrans = "Commit"
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            AgL.ETrans.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FImportDataFromSqliteTable(bTableName As String, bJoinCondStr As String, bPrimaryField As String,
                Connection As Object, mSqlConn As Object, mSqlCmd As Object, mDbPath As String)
        Dim mTrans As String = ""
        Dim DtFields As DataTable = Nothing
        Dim DtSqliteTableData As DataTable = Nothing
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim StrColumnList As String = ""
        Dim bTempTableName As String = "[#Temp_" + bTableName + "]"

        If AgL.PubServerName = "" Then
            mQry = "PRAGMA table_info(Item)"
        Else
            mQry = "SELECT COLUMN_NAME As Name FROM INFORMATION_SCHEMA.Columns WHERE TABLE_NAME = '" & bTableName & "'  
                ORDER BY ORDINAL_POSITION "
        End If
        DtFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        StrColumnList = ""
        For J = 0 To DtFields.Rows.Count - 1
            If StrColumnList = "" Then
                StrColumnList = DtFields.Rows(J)("Name")
            Else
                StrColumnList += ", " & DtFields.Rows(J)("Name")
            End If
        Next

        If AgL.PubServerName = "" Then
            mQry = "DROP TABLE IF EXISTS " & bTempTableName & " ;
                    CREATE TABLE " & bTempTableName & " AS SELECT * FROM " & bTableName & ""
            AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
        Else
            mQry = "SELECT * INTO " & bTempTableName & " FROM " & bTableName & " WHERE 1 = 2 "
            AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
        End If

        If AgL.PubServerName = "" Then
            Try
                mQry = "Attach '" & mDbPath & "' AS Source "
                AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
            Catch ex As Exception
            End Try

            mQry = " INSERT INTO " & bTempTableName & "(" & StrColumnList & ")"
            mQry += " Select " & StrColumnList & " From Source." & bTableName & ""
            AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
        Else
            Dim commandSourceData As SQLiteCommand = New SQLiteCommand("Select " & StrColumnList & " From " & bTableName & " ", Connection)
            Dim reader As SQLiteDataReader = commandSourceData.ExecuteReader

            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(mSqlConn, SqlBulkCopyOptions.Default, mSqlCmd.Transaction)
                bulkCopy.DestinationTableName = bTempTableName
                bulkCopy.BulkCopyTimeout = 500
                bulkCopy.WriteToServer(reader)
                reader.Close()
            End Using
        End If


        StrColumnList = StrColumnList.Replace("00", "DateTime")

        mQry = "INSERT INTO " & bTableName & "(" & StrColumnList & ")
                Select H_Temp." & Replace(StrColumnList, ",", ",H_Temp.") & "
                From " & bTempTableName & " H_Temp 
                LEFT JOIN " & bTableName & " H On " & bJoinCondStr &
                " Where H." & bPrimaryField & " Is Null "
        AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
    End Sub
End Class