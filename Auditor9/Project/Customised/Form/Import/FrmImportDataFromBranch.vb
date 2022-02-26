Imports AgLibrary.ClsMain.agConstants
Imports AgTemplate.ClsMain
Imports System.Threading
Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Data.SqlClient
Imports Customised.ClsMain

Public Class FrmImportDataFromBranch
    Dim mQry As String = ""
    Dim mTrans As String = ""
    Dim Connection_Pakka As New SQLite.SQLiteConnection
    Dim DtItem As DataTable
    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnImport.Click
        If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
            If AgL.StrCmp(AgL.PubDBName, "SHADHVINEW") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIKANPURB2") Or AgL.StrCmp(AgL.PubDBName, "SHADHVIjaunpur") Then
                ProcImportStockIssueDataFromSqlite_Sadhvi()
            Else
                ProcImportSaleInvoiceDataFromSqlite_Sadhvi()
            End If
            'ProcImportSaleInvoiceDataFromSqlite_Sadhvi()
        ElseIf ClsMain.FDivisionNameForCustomization(12) = "NANDI SAREES" Then
            ProcImportStockIssueDataFromSqlite_SadhviRetail()
        ElseIf ClsMain.FDivisionNameForCustomization(18) = "SHRI PARWATI SAREE" Then
            ProcImportSaleInvoiceDataFromSqlite_Parwati()
        ElseIf ClsMain.FDivisionNameForCustomization(13) = "JAIN BROTHERS" Or
                    ClsMain.FDivisionNameForCustomization(11) = "BOOK SHOPEE" Then
            ProcImportStockIssueDataFromSqlite_JainBrothers()
        ElseIf ClsMain.FDivisionNameForCustomization(6) = "KISHOR" Then
            ProcImportStockIssueDataFromSqlite_Kishor()
        ElseIf ClsMain.FDivisionNameForCustomization(9) = "GUR SHEEL" Then
            ProcImportSaleInvoiceDataFromSqlite_Gursheel()
        End If
    End Sub
    Private Sub ProcImportSaleInvoiceDataFromSqlite_Parwati()
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
    Private Sub ProcImportStockIssueDataFromSqlite_Sadhvi()
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

            Dim HO_Subcode As String = AgL.XNull(AgL.Dman_Execute("Select HO_Subcode from division  Where SubCode = '" & AgL.PubDivCode & "'  ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)

            For I = 0 To DtHeaderSource.Rows.Count - 1
                If (AgL.XNull(DtHeaderSource.Rows(I)("Subcode")) = HO_Subcode) Then
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
                        PurchInvoiceTable.Div_Code = AgL.XNull(DtHeaderSource.Rows(I)("Div_Code"))
                        PurchInvoiceTable.V_No = 0
                        PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                        PurchInvoiceTable.ManualRefNo = ""
                        PurchInvoiceTable.Vendor = ""
                        If PurchInvoiceTable.Div_Code = "E" Then
                            PurchInvoiceTable.VendorName = "SADHVI EMBROIDERY"
                        Else
                            PurchInvoiceTable.VendorName = "SADHVI ENTERPRISES"
                        End If
                        PurchInvoiceTable.AgentCode = ""
                        PurchInvoiceTable.AgentName = ""
                        PurchInvoiceTable.BillToPartyCode = ""
                        PurchInvoiceTable.BillToPartyName = PurchInvoiceTable.VendorName
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
                        PurchInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
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

                            If AgL.VNull(AgL.Dman_Execute(" Select Rate From RateListDetail 
                                Where Code = '" & PurchInvoiceTable.Line_ItemCode & "'
                                And RateType Is Null ", AgL.GCn).ExecuteScalar()) <> PurchInvoiceTable.Line_Rate Then
                                mQry = " Update RateListDetail Set Rate = " & PurchInvoiceTable.Line_Rate & " 
                                    Where Code = '" & PurchInvoiceTable.Line_ItemCode & "' 
                                    And RateType Is Null "
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                                mQry = " Update Item Set Rate = " & PurchInvoiceTable.Line_Rate & " 
                                    Where Code = '" & PurchInvoiceTable.Line_ItemCode & "'"
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If



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

    Private Sub ProcImportStockIssueDataFromSqlite_SadhviRetail()
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
            FImportDataFromSqliteTable_Retail("Item", "H.Code = H_Temp.Code", "Code", Connection, AgL.GCn, AgL.ECmd, mDbPath)

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

            mQry = " Select H.V_Type, H.ManualRefNo, I.Description As ItemDesc, I.ItemGroup AS ItemGroupCode, I.ItemCategory AS ItemCategoryCode, I.SalesTaxPostingGroup AS SalesTaxGroupItem,
                L.MRP*L.Qty AS Amount1, L.*
                From StockHead H 
                LEFT JOIN StockHeadDetail L ON H.DocID = L.DocID
                LEFT JOIN Item I ON L.Item = I.Code "
            Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)

            mQry = " Select * From PurchInvoice "
            Dim DtPurchInvoice As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

            Dim HO_Subcode As String = AgL.XNull(AgL.Dman_Execute("Select HO_Subcode from division  Where SubCode = '" & AgL.PubDivCode & "'  ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)

            For I = 0 To DtHeaderSource.Rows.Count - 1
                If (AgL.XNull(DtHeaderSource.Rows(I)("Subcode")) = HO_Subcode) Then
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
                        PurchInvoiceTable.Div_Code = AgL.XNull(DtHeaderSource.Rows(I)("Div_Code"))
                        PurchInvoiceTable.V_No = 0
                        PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                        PurchInvoiceTable.ManualRefNo = ""
                        PurchInvoiceTable.Vendor = ""
                        If PurchInvoiceTable.Div_Code = "E" Then
                            PurchInvoiceTable.VendorName = "SADHVI EMBROIDERY"
                        Else
                            PurchInvoiceTable.VendorName = "SADHVI ENTERPRISES"
                        End If
                        PurchInvoiceTable.AgentCode = ""
                        PurchInvoiceTable.AgentName = ""
                        PurchInvoiceTable.BillToPartyCode = ""
                        PurchInvoiceTable.BillToPartyName = PurchInvoiceTable.VendorName
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
                        PurchInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
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

                            Dim Itemcode As String = AgL.XNull(AgL.Dman_Execute("Select code from Item  Where GenDocId = '" & DtPurchInvoiceDetail_ForHeader.Rows(J)("DocId") & "' AND OmsId = '" & DtPurchInvoiceDetail_ForHeader.Rows(J)("Item") & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)
                            PurchInvoiceTable.Line_Sr = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr"))
                            PurchInvoiceTable.Line_ItemCode = AgL.XNull(Itemcode)
                            PurchInvoiceTable.Line_ItemGroupCode = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemGroupCode"))
                            PurchInvoiceTable.Line_ItemCategoryCode = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemCategoryCode"))
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
                            PurchInvoiceTable.Line_Rate = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("MRP"))
                            PurchInvoiceTable.Line_MRP = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("MRP"))
                            PurchInvoiceTable.Line_Sale_Rate = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("MRP"))

                            'If AgL.VNull(AgL.Dman_Execute(" Select Rate From RateListDetail 
                            '    Where Code = '" & PurchInvoiceTable.Line_ItemCode & "'
                            '    And RateType Is Null ", AgL.GCn).ExecuteScalar()) <> PurchInvoiceTable.Line_Rate Then
                            '    mQry = " Update RateListDetail Set Rate = " & PurchInvoiceTable.Line_Rate & " 
                            '        Where Code = '" & PurchInvoiceTable.Line_ItemCode & "' 
                            '        And RateType Is Null "
                            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            '    mQry = " Update Item Set Rate = " & PurchInvoiceTable.Line_Rate & " 
                            '        Where Code = '" & PurchInvoiceTable.Line_ItemCode & "'"
                            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            'End If



                            PurchInvoiceTable.Line_DiscountPer = 0
                            PurchInvoiceTable.Line_DiscountAmount = 0
                            PurchInvoiceTable.Line_AdditionalDiscountPer = 0
                            PurchInvoiceTable.Line_AdditionalDiscountAmount = 0
                            PurchInvoiceTable.Line_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount1"))
                            PurchInvoiceTable.Line_Remark = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Remark"))
                            PurchInvoiceTable.Line_BaleNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("BaleNo"))
                            PurchInvoiceTable.Line_LotNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("LotNo"))
                            PurchInvoiceTable.Line_ReferenceDocId = ""
                            PurchInvoiceTable.Line_GrossWeight = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("GrossWeight"))
                            PurchInvoiceTable.Line_NetWeight = 0
                            PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount1"))
                            PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount1"))
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
                            PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount1"))
                            PurchInvoiceTable.Line_Other_Charge = 0
                            PurchInvoiceTable.Line_Deduction = 0
                            PurchInvoiceTable.Line_Round_Off = 0
                            PurchInvoiceTable.Line_Net_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount1"))

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
                End If
            Next

            mQry = " SELECT H.* 
                    FROM PurchInvoice H
                    LEFT JOIN Barcode B ON B.GenDocID = H.DocID
                    WHERE B.Code IS NULL  "
            Dim DtPurchInvoiceList As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

            For I = 0 To DtPurchInvoiceList.Rows.Count - 1
                GenerateAndInsertBarcode(AgL.XNull(DtPurchInvoiceList.Rows(I)("DocID")), AgL.GCn, AgL.ECmd)
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

    Private Sub GenerateAndInsertBarcode(DocID As String, ByRef Conn As Object, ByRef Cmd As Object)
        Dim DtStock As DataTable
        Dim I As Integer
        mQry = "Select Pid.Rate As PurchaseRate, Pid.Sale_Rate As SaleRate, Pid.Mrp, 
                H.V_Type, H.Vendor, H.ManualRefNo, H.Process,
                IfNull(S.Item,Pid.Item) As Item, S.Qty_Rec, S.DocId, S.Sr
                From Stock S With (NoLock) 
                LEFT JOIN PurchInvoiceDetail Pid With (NoLock) On S.DocId = Pid.DocId and S.Tsr = Pid.Sr
                LEFT JOIN PurchInvoice H ON H.DocId = Pid.DocId 
                Where S.DocID = '" & DocID & "' 
                And IfNull(S.Qty_Rec,0) > 0 "
        DtStock = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        If DtStock.Rows.Count > 0 Then
            For I = 0 To DtStock.Rows.Count - 1
                Dim BarcodeCntForDocIdSr As Integer = 0
                mQry = "Select (Case When IsNull(Ig.BarcodeType,'N/A') = 'N/A' Then Ic.BarcodeType Else IsNull(Ig.BarcodeType,'N/A') End) As BarcodeType, 
                        (Case When IsNull(Ig.BarcodePattern,'N/A') = 'N/A' Then Ic.BarcodePattern Else IsNull(Ig.BarcodePattern,'N/A') End) As BarcodePattern
                        From Item I  With (NoLock) 
                        LEFT JOIN ItemGroup Ig  With (NoLock) On I.ItemGroup = Ig.Code 
                        LEFT JOIN ItemCategory Ic With (NoLock) ON I.ItemCategory = Ic.Code
                        Where I.Code = '" & AgL.XNull(DtStock.Rows(I)("Item")) & "'"
                Dim DtBarcodeType As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
                If (AgL.XNull(DtBarcodeType.Rows(0)("BarCodePattern")) = AgLibrary.ClsMain.agConstants.BarcodePattern.Auto) Then
                    BarcodeCntForDocIdSr = AgL.Dman_Execute("Select Count(*) From BarCode  With (NoLock) Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & "", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar
                    If BarcodeCntForDocIdSr = 0 Then
                        If AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.UniquePerPcs Then
                            InsertBarCodes(DocID, AgL.VNull(DtStock.Rows(I)("Sr")), AgL.XNull(DtStock.Rows(I)("Item")), AgL.VNull(DtStock.Rows(I)("Qty_Rec")), 1, AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")), AgL.VNull(DtStock.Rows(I)("SaleRate")), AgL.VNull(DtStock.Rows(I)("PurchaseRate")), AgL.VNull(DtStock.Rows(I)("MRP")), AgL.VNull(DtStock.Rows(I)("Vendor")), AgL.VNull(DtStock.Rows(I)("Process")), AgL.VNull(DtStock.Rows(I)("V_Type")), AgL.VNull(DtStock.Rows(I)("ManualRefNo")), Conn, Cmd)
                        ElseIf AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.LotWise Or AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.Fixed Then
                            InsertBarCodes(DocID, AgL.VNull(DtStock.Rows(I)("Sr")), AgL.XNull(DtStock.Rows(I)("Item")), 1, AgL.VNull(DtStock.Rows(I)("Qty_Rec")), AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")), AgL.VNull(DtStock.Rows(I)("SaleRate")), AgL.VNull(DtStock.Rows(I)("PurchaseRate")), AgL.VNull(DtStock.Rows(I)("MRP")), AgL.VNull(DtStock.Rows(I)("Vendor")), AgL.VNull(DtStock.Rows(I)("Process")), AgL.VNull(DtStock.Rows(I)("V_Type")), AgL.VNull(DtStock.Rows(I)("ManualRefNo")), Conn, Cmd)
                        End If
                    Else
                        If AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.UniquePerPcs Then
                            If BarcodeCntForDocIdSr < AgL.VNull(DtStock.Rows(I)("Qty_Rec")) Then
                                InsertBarCodes(DocID, AgL.VNull(DtStock.Rows(I)("Sr")), AgL.XNull(DtStock.Rows(I)("Item")), AgL.VNull(DtStock.Rows(I)("Qty_Rec")) - BarcodeCntForDocIdSr, 1, AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")), AgL.VNull(DtStock.Rows(I)("SaleRate")), AgL.VNull(DtStock.Rows(I)("PurchaseRate")), AgL.VNull(DtStock.Rows(I)("MRP")), AgL.VNull(DtStock.Rows(I)("Vendor")), AgL.VNull(DtStock.Rows(I)("Process")), AgL.VNull(DtStock.Rows(I)("V_Type")), AgL.VNull(DtStock.Rows(I)("ManualRefNo")), Conn, Cmd)
                            ElseIf BarcodeCntForDocIdSr > AgL.VNull(DtStock.Rows(I)("Qty_Rec")) Then
                                mQry = " DELETE From BarcodeSiteDetail Where Code in
                                        (Select Code From Barcode  With (NoLock) Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " 
                                        LIMIT " & BarcodeCntForDocIdSr - AgL.VNull(DtStock.Rows(I)("Qty_Rec")) & ") "
                                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                                mQry = " DELETE From Barcode Where Code in
                                        (Select Code From Barcode  With (NoLock) Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " 
                                        LIMIT " & BarcodeCntForDocIdSr - AgL.VNull(DtStock.Rows(I)("Qty_Rec")) & ") "
                                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                            End If
                        ElseIf AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.LotWise Or
                                AgL.XNull(DtBarcodeType.Rows(0)("BarCodeType")) = BarcodeType.Fixed Then
                            mQry = "UPDATE Barcode 
                                    Set Qty = " & AgL.VNull(DtStock.Rows(I)("Qty_Rec")) & ", 
                                    SaleRate = " & AgL.VNull(DtStock.Rows(I)("SaleRate")) & ", 
                                    PurchaseRate = " & AgL.VNull(DtStock.Rows(I)("PurchaseRate")) & ", 
                                    MRP = " & AgL.VNull(DtStock.Rows(I)("MRP")) & " 
                                    Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    End If
                    If (AgL.Dman_Execute("Select Count(*) From BarCode  With (NoLock) Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " 
                                    And Item <> '" & AgL.XNull(DtStock.Rows(I)("Item")) & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar) Then
                        mQry = "UPDATE Barcode Set Item = '" & AgL.XNull(DtStock.Rows(I)("Item")) & "' Where GenDocId = '" & DocID & "' And GenSr = " & AgL.VNull(DtStock.Rows(I)("Sr")) & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            Next
        End If
    End Sub
    Public Sub InsertBarCodes(mDocId As String, mSr As Integer, mItemCode As String, mQty As Integer, mLotQty As Double, mBarcodeType As String,
                              mSaleRate As Double, mPurchaseRate As Double, mMrp As Double, Vendor As String, Process As String, V_Type As String, ReferenceNo As String,
                              ByRef Conn As Object, ByRef Cmd As Object)
        Dim J As Integer = 0



        For J = 0 To mQty - 1
            Dim mBarcodeCode$ = ""
            Dim mBarcodeDesc$ = ""

            If mBarcodeType = BarcodeType.Fixed Then
                mQry = " Select Code From Barcode With (NoLock) Where Item = '" & mItemCode & "'"
                mBarcodeCode = AgL.XNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
            End If

            If mBarcodeCode = "" Then
                mBarcodeCode = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode  With (NoLock)", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()

                Dim mBarcodeMinimumValue As Integer = AgL.VNull(FGetSettings(SettingFields.BarcodeMinimumValue, SettingType.General, Process, V_Type))
                Dim mBarcodeMaximumValue As Integer = AgL.VNull(FGetSettings(SettingFields.BarcodeMaximumValue, SettingType.General, Process, V_Type))

                If AgL.PubServerName = "" Then
                    mQry = "Select IfNull(Max(CAST(Description as BIGINT)),0) + 1 From BarCode  With (NoLock) Where Item Not In ('" & ItemCode.Lr & "','" & ItemCode.LrBale & "') "
                Else
                    mQry = "Select IfNull(Max(CAST(Description as BIGINT)),0) + 1 From BarCode  With (NoLock) Where Isnumeric(Description) <> 0 And Item Not In ('" & ItemCode.Lr & "','" & ItemCode.LrBale & "') "
                End If
                If mBarcodeMinimumValue <> 0 Then mQry += " And IfNull(CAST(Description as BIGINT),0) >= " & mBarcodeMinimumValue & " "
                If mBarcodeMaximumValue <> 0 Then mQry += " And IfNull(CAST(Description as BIGINT),0) <= " & mBarcodeMaximumValue & " "
                mBarcodeDesc = AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar()

                If mBarcodeMinimumValue <> 0 And Convert.ToInt64(mBarcodeDesc) < mBarcodeMinimumValue Then
                    mBarcodeDesc = mBarcodeMinimumValue + Convert.ToInt64(mBarcodeDesc)
                End If

                If mBarcodeMaximumValue <> 0 And Convert.ToInt64(mBarcodeDesc) > mBarcodeMaximumValue Then
                    Err.Raise(1, "", "Barcode Value is going to upper limit.")
                End If

                mQry = " INSERT INTO Barcode (Code, Description, Div_Code, Item, GenDocID, GenSr, Qty, SaleRate, PurchaseRate, MRP, BarcodeType)
                    VALUES (" & AgL.Chk_Text(mBarcodeCode) & ", " & AgL.Chk_Text(mBarcodeDesc) & ", " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(mItemCode) & ",
                    " & AgL.Chk_Text(mDocId) & ", " & mSr & ", " & mLotQty & ", 
                    " & Val(mSaleRate) & "," & Val(mPurchaseRate) & "," & Val(mMrp) & ",
                    " & AgL.Chk_Text(mBarcodeType) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " INSERT INTO BarcodeSiteDetail (Code,Div_Code, Site_Code, LastTrnDocID,
                    LastTrnSr, LastTrnV_Type, LastTrnManualRefNo,
                    LastTrnSubcode, LastTrnProcess, CurrentGodown, Status, CurrentStock)
                    VALUES (" & AgL.Chk_Text(mBarcodeCode) & ", " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(AgL.PubSiteCode) & ",
                    " & AgL.Chk_Text(mDocId) & ", " & Val(mSr) & ", " & AgL.Chk_Text(V_Type) & ", " & AgL.Chk_Text(ReferenceNo) & ",
                    " & AgL.Chk_Text(Vendor) & ", " & AgL.Chk_Text(Process) & ", Null, 'Receive', " & mLotQty & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            If mBarcodeType = BarcodeType.Fixed Then
                mQry = " UPDATE Item Set BarCode = '" & mBarcodeCode & "'
                    Where Code = '" & mItemCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            If mBarcodeType = BarcodeType.Fixed Or mBarcodeType = BarcodeType.LotWise Then
                mQry = " UPDATE Stock Set BarCode = '" & mBarcodeCode & "'
                    Where DocId = '" & mDocId & "'
                    And Sr = " & mSr & ""
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub

    Public Function FGetSettings(FieldName As String, SettingType As String, Process As String, V_Type As String) As String
        Dim bNCat As String = "", bCategory As String = ""
        bNCat = V_Type

        If bNCat = Ncat.StockIssue Or bNCat = Ncat.StockReceive Then
            bCategory = "Stock"
        Else
            bCategory = "Purch"
        End If

        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode, bCategory, bNCat, V_Type, Process, "")
        FGetSettings = mValue
    End Function

    Private Sub FImportDataFromSqliteTable(bTableName As String, bJoinCondStr As String, bPrimaryField As String,
                Connection As Object, mSqlConn As Object, mSqlCmd As Object, mDbPath As String)
        Dim mTrans As String = ""
        Dim DtFields As DataTable = Nothing
        Dim DtSqliteTableData As DataTable = Nothing
        Dim DtTempItems As DataTable = Nothing
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
        DtFields = AgL.FillData(mQry, IIf(AgL.PubServerName = "", mSqlConn, AgL.GcnRead)).Tables(0)
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

        'To Update ItemName 
        If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
            mQry = "Select H_Temp.Code, H_Temp.ManualCode,H_Temp.Description,H_Temp.DisplayName
                    From Source." & bTableName & " H_Temp 
                    LEFT JOIN " & bTableName & " H On " & bJoinCondStr &
                    " Where H." & bPrimaryField & " Is Not Null "
            DtTempItems = AgL.FillData(mQry, mSqlConn).Tables(0)

            For I = 0 To DtTempItems.Rows.Count - 1
                mQry = "UPDATE Item Set ManualCode = '" + AgL.XNull(DtTempItems.Rows(I)("ManualCode")) + "', Description='" + AgL.XNull(DtTempItems.Rows(I)("Description")) + "', DisplayName ='" + AgL.XNull(DtTempItems.Rows(I)("DisplayName")) + "' Where Code = '" + AgL.XNull(DtTempItems.Rows(I)("Code")) + "' "
                AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
            Next
        End If


    End Sub

    Private Sub FImportDataFromSqliteTable_Retail(bTableName As String, bJoinCondStr As String, bPrimaryField As String,
                Connection As Object, mSqlConn As Object, mSqlCmd As Object, mDbPath As String)
        Dim mTrans As String = ""
        Dim DtFields As DataTable = Nothing
        Dim DtSqliteTableData As DataTable = Nothing
        Dim DtTempItems As DataTable = Nothing
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
        DtFields = AgL.FillData(mQry, IIf(AgL.PubServerName = "", mSqlConn, AgL.GcnRead)).Tables(0)
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
                " Where H." & bPrimaryField & " Is Null AND H_Temp.V_Type <> 'ITEM' "
        AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)

        Dim DtItemList As DataTable
        mQry = "select H.DocId,  I.Description, I.Specification, I.ItemGroup, I.ItemCategory, I.ItemType, 
                I.Unit, I.V_Type, I.HSN, I.SalesTaxPostingGroup, I.ProfitMarginPer, L.Item, L.MRP, L.Rate
                From Source.StockHead H
                left join Source.StockHeadDetail L on L.DocId = H.docid 
                left join Source.item I on I.code = L.Item 
                Left Join Item I1 on I1.GenDocId = H.DocId AND I1.OmsId = L.Item "
        'Where I1.code IS NULL  "
        DtItemList = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim mItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
        Dim mItemDescription As String = ""
        For R As Integer = 0 To DtItemList.Rows.Count - 1

            Dim mItemCode_New = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + R).ToString().PadLeft(8, "0")
            mItemDescription = AgL.XNull(DtItemList.Rows(R)("Description")) + "-" + AgL.XNull(DtItemList.Rows(R)("DocId"))

            mQry = "Select Count(*) From Item Where Description = '" & mItemDescription & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                mQry = "INSERT INTO Item (Code, ManualCode, Description, Specification, Unit, ItemGroup, ItemCategory, 
                ItemType, OmsId, V_Type, PurchaseRate, Rate, MRP, ProfitMarginPer, HSN, EntryBy, EntryDate, Div_Code, SalesTaxPostingGroup, GenDocId) 
                Select '" & mItemCode_New & "' As Code, '" & mItemCode_New & "' As ManualCode, 
                " & AgL.Chk_Text(mItemDescription) & " As Description, 
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("Specification"))) & " As Specification, 
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("Unit"))) & " As Unit, 
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("ItemGroup"))) & " As ItemGroup, 
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("ItemCategory"))) & " As ItemCategory, 
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("ItemType"))) & " As ItemType, 
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("Item"))) & " As OmsId, 
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("V_Type"))) & " As V_Type, 
                " & Val(AgL.VNull(DtItemList.Rows(R)("Rate"))) & " As PurchaseRate, 
                " & Val(AgL.VNull(DtItemList.Rows(R)("MRP"))) & " As Rate, 
                " & Val(AgL.VNull(DtItemList.Rows(R)("MRP"))) & " As MRP, 
                " & Val(AgL.VNull(DtItemList.Rows(R)("ProfitMarginPer"))) & " As ProfitMarginPer, 
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("HSN"))) & " As HSN, 
                '" & AgL.PubUserName & "' As EntryBy, 
                " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
                '" & AgL.PubDivCode & "' As Div_Code, 
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("SalesTaxPostingGroup"))) & " As SalesTaxPostingGroup,
                " & AgL.Chk_Text(AgL.XNull(DtItemList.Rows(R)("DocId"))) & " As GenDocId "
                AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
            End If
        Next


    End Sub

    Private Sub ProcImportStockIssueDataFromSqlite_JainBrothers()
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

            mQry = " Select * From Catalog "
            Dim DtCatalog_Source As DataTable = AgL.FillData(mQry, Connection).Tables(0)

            mQry = " Select * From Catalog "
            Dim DtCatalog_Destination As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

            For I = 0 To DtCatalog_Source.Rows.Count - 1
                If DtCatalog_Destination.Select("OMSId = '" & AgL.XNull(DtCatalog_Source.Rows(I)("Code")) & "'").Length = 0 Then
                    Dim CatalogTableList(0) As FrmCatalog.StructCatalog
                    Dim CatalogTable As New FrmCatalog.StructCatalog

                    CatalogTable.Code = AgL.GetMaxId("Catalog", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                    CatalogTable.Specification = AgL.XNull(DtCatalog_Source.Rows(I)("Specification"))
                    CatalogTable.Description = AgL.XNull(DtCatalog_Source.Rows(I)("Description"))
                    CatalogTable.Site_Code = AgL.XNull(DtCatalog_Source.Rows(I)("Site_Code"))
                    CatalogTable.EntryBy = AgL.XNull(DtCatalog_Source.Rows(I)("EntryBy"))
                    CatalogTable.EntryDate = AgL.XNull(DtCatalog_Source.Rows(I)("EntryDate"))
                    CatalogTable.EntryType = AgL.XNull(DtCatalog_Source.Rows(I)("EntryType"))
                    CatalogTable.EntryStatus = AgL.XNull(DtCatalog_Source.Rows(I)("EntryStatus"))
                    CatalogTable.Status = AgL.XNull(DtCatalog_Source.Rows(I)("Status"))
                    CatalogTable.Div_Code = AgL.XNull(DtCatalog_Source.Rows(I)("Div_Code"))
                    CatalogTable.UID = AgL.XNull(DtCatalog_Source.Rows(I)("UID"))
                    CatalogTable.OmsId = AgL.XNull(DtCatalog_Source.Rows(I)("Code"))
                    CatalogTable.UploadDate = AgL.XNull(DtCatalog_Source.Rows(I)("UploadDate"))

                    mQry = " Select * From CatalogDetail Where Code = '" & DtCatalog_Source.Rows(I)("Code") & "'"
                    Dim DtCatalogDetailSource_ForHeader As DataTable = AgL.FillData(mQry, Connection).Tables(0)

                    For J = 0 To DtCatalogDetailSource_ForHeader.Rows.Count - 1
                        CatalogTable.Line_Sr = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Sr"))
                        CatalogTable.Line_ItemCode = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Item"))
                        CatalogTable.Line_ItemName = ""
                        CatalogTable.Line_Qty = AgL.VNull(DtCatalogDetailSource_ForHeader.Rows(J)("Qty"))
                        CatalogTable.Line_Unit = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Unit"))
                        CatalogTable.Line_Rate = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Rate"))
                        CatalogTable.Line_DiscountPer = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("DiscountPer"))
                        CatalogTable.Line_AdditionalDiscountPer = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("AdditionalDiscountPer"))
                        CatalogTable.Line_AdditionPer = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("AdditionPer"))
                        CatalogTable.Line_ItemStateCode = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("ItemState"))
                        CatalogTable.Line_ItemStateName = ""
                        CatalogTable.Line_OMSId = AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Code")) + AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Sr"))

                        CatalogTableList(UBound(CatalogTableList)) = CatalogTable
                        ReDim Preserve CatalogTableList(UBound(CatalogTableList) + 1)
                    Next
                    FrmCatalog.InsertCatalog(CatalogTableList)
                Else
                    Dim bCatalogCode As String = DtCatalog_Destination.Select("OMSId = '" & AgL.XNull(DtCatalog_Source.Rows(I)("Code")) & "'")(0)("Code")

                    mQry = " Delete From CatalogDetail Where Code = '" & bCatalogCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = " Select * From CatalogDetail Where Code = '" & AgL.XNull(DtCatalog_Source.Rows(I)("Code")) & "'"
                    Dim DtCatalogDetailSource_ForHeader As DataTable = AgL.FillData(mQry, Connection).Tables(0)

                    For J = 0 To DtCatalogDetailSource_ForHeader.Rows.Count - 1
                        mQry = "INSERT INTO CatalogDetail (Code, Sr, Item, Qty, Rate, DiscountPer, 
                        AdditionalDiscountPer, AdditionPer, Unit, ItemState)
                        VALUES (" & AgL.Chk_Text(bCatalogCode) & ", 
                        " & AgL.VNull(DtCatalogDetailSource_ForHeader.Rows(J)("Sr")) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Item"))) & ", 
                        " & AgL.VNull(DtCatalogDetailSource_ForHeader.Rows(J)("Qty")) & ", 
                        " & AgL.VNull(DtCatalogDetailSource_ForHeader.Rows(J)("Rate")) & ", 
                        " & AgL.VNull(DtCatalogDetailSource_ForHeader.Rows(J)("DiscountPer")) & ", 
                        " & AgL.VNull(DtCatalogDetailSource_ForHeader.Rows(J)("AdditionalDiscountPer")) & ", 
                        " & AgL.VNull(DtCatalogDetailSource_ForHeader.Rows(J)("AdditionPer")) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("Unit"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtCatalogDetailSource_ForHeader.Rows(J)("ItemState"))) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Next
                End If
            Next


            If MsgBox("Do you want to import transactions", vbYesNo) = MsgBoxResult.Yes Then


                mQry = " Select H.* From PurchInvoice H "
                Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)

                mQry = " Select H.V_Type, H.ManualRefNo, I.Description As ItemDesc, 
                    C.Description As CatalogDesc, L.*
                    From PurchInvoice H 
                    LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN Item I ON L.Item = I.Code
                    LEFT JOIN Catalog C On L.Catalog = C.Code "
                Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)



                mQry = " Select * From PurchInvoice "
                Dim DtPurchInvoice_Destination As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

                For I = 0 To DtHeaderSource.Rows.Count - 1
                    If DtPurchInvoice_Destination.Select("OMSId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'").Length = 0 Then
                        Dim Tot_Gross_Amount As Double = 0
                        Dim Tot_Taxable_Amount As Double = 0
                        Dim Tot_Tax1 As Double = 0
                        Dim Tot_Tax2 As Double = 0
                        Dim Tot_Tax3 As Double = 0
                        Dim Tot_Tax4 As Double = 0
                        Dim Tot_Tax5 As Double = 0
                        Dim Tot_SubTotal1 As Double = 0


                        Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice
                        Dim PurchInvoiceTable As New FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice

                        PurchInvoiceTable.DocID = ""
                        PurchInvoiceTable.V_Type = "REC"
                        PurchInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                        PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                        PurchInvoiceTable.Div_Code = AgL.PubDivCode
                        PurchInvoiceTable.V_No = 0
                        PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                        PurchInvoiceTable.ManualRefNo = ""
                        PurchInvoiceTable.Vendor = AgL.XNull(DtHeaderSource.Rows(I)("Site_Code"))
                        PurchInvoiceTable.VendorName = ""
                        PurchInvoiceTable.AgentCode = ""
                        PurchInvoiceTable.AgentName = ""
                        PurchInvoiceTable.BillToPartyCode = ""
                        PurchInvoiceTable.BillToPartyName = ""
                        PurchInvoiceTable.VendorAddress = ""
                        PurchInvoiceTable.VendorCity = ""
                        PurchInvoiceTable.VendorMobile = ""
                        PurchInvoiceTable.VendorSalesTaxNo = ""
                        PurchInvoiceTable.SalesTaxGroupParty =
                        PurchInvoiceTable.PlaceOfSupply = ""
                        PurchInvoiceTable.StructureCode = ""
                        PurchInvoiceTable.CustomFields = ""
                        PurchInvoiceTable.VendorDocNo = AgL.XNull(DtHeaderSource.Rows(I)("VendorDocNo"))
                        PurchInvoiceTable.VendorDocDate = AgL.XNull(DtHeaderSource.Rows(I)("VendorDocDate"))
                        PurchInvoiceTable.ReferenceDocId = ""
                        PurchInvoiceTable.Tags = ""
                        PurchInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
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

                            PurchInvoiceTable.Line_CatalogCode = ""
                            PurchInvoiceTable.Line_CatalogName = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("CatalogDesc"))


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

                        FrmPurchInvoiceDirect_WithDimension.InsertPurchInvoice(PurchInvoiceTableList)
                    End If
                Next
            End If

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
    Private Sub ProcImportStockIssueDataFromSqlite_Kishor()
        Dim mStrMainQry As String = ""
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim bSelectionQry As String = ""
        Dim mSqliteDataQry As String = ""
        Dim bPrathamBrandCode = "PRATHAM"


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName

        Dim Connection_External As New SQLite.SQLiteConnection
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection_External.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Else
            Connection_External.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection_External.Open()

        mQry = " Select * From Item "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)


        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"

        Try
            Dim ItemGroupTable As New FrmItemMaster.StructItemGroup
            ItemGroupTable.Code = bPrathamBrandCode
            ItemGroupTable.Description = "PRATHAM"
            ItemGroupTable.ItemCategory = ""
            ItemGroupTable.ItemType = "TP"
            ItemGroupTable.SalesTaxPostingGroup = ""
            ItemGroupTable.Unit = "Pcs"
            ItemGroupTable.EntryBy = AgL.PubUserName
            ItemGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            ItemGroupTable.EntryType = "Add"
            ItemGroupTable.EntryStatus = AgTemplate.ClsMain.LogStatus.LogOpen
            ItemGroupTable.Div_Code = AgL.PubDivCode
            ItemGroupTable.OMSId = bPrathamBrandCode
            ItemGroupTable.Status = "Active"
            FrmItemMaster.ImportItemGroupTable(ItemGroupTable)




            FImportAllItems(ItemV_Type.ItemCategory, Connection_External)
            FImportAllItems(ItemV_Type.ItemGroup, Connection_External)
            FImportAllItems(ItemV_Type.Item, Connection_External)
            FImportAllItems(ItemV_Type.Dimension1, Connection_External)
            FImportAllItems(ItemV_Type.Dimension2, Connection_External)
            FImportAllItems(ItemV_Type.Dimension3, Connection_External)
            FImportAllItems(ItemV_Type.Dimension4, Connection_External)
            FImportAllItems(ItemV_Type.SIZE, Connection_External)
            FImportAllItems(ItemV_Type.SKU, Connection_External)

            mQry = " Select * From Barcode "
            Dim DtBarcodeSource As DataTable = AgL.FillData(mQry, Connection_External).Tables(0)

            For I = 0 To DtBarcodeSource.Rows.Count - 1
                Dim mBarcodeCode As String = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode  With (NoLock)", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                Dim bItemCode As String = FGetCodeFromOMSId(AgL.XNull(DtBarcodeSource.Rows(I)("Item")), DtItem, "Code")
                mQry = " INSERT INTO Barcode (Code, Description, Div_Code, Item, GenDocID, GenSr, 
                        Qty, SaleRate, PurchaseRate, MRP, BarcodeType, OMSId)
                        Select " & AgL.Chk_Text(mBarcodeCode) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtBarcodeSource.Rows(I)("Description"))) & ", 
                        " & AgL.Chk_Text(AgL.PubDivCode) & ", 
                        " & AgL.Chk_Text(bItemCode) & ",
                        Null As GenDocID, Null As GenSr, 
                        " & AgL.VNull(DtBarcodeSource.Rows(I)("Qty")) & " As Qty, 0 As SaleRate, 
                        " & AgL.VNull(DtBarcodeSource.Rows(I)("PurchaseRate")) & " As PurchaseRate, 
                        " & AgL.VNull(DtBarcodeSource.Rows(I)("MRP")) & " As MRP, 
                        " & AgL.Chk_Text(AgL.XNull(DtBarcodeSource.Rows(I)("BarcodeType"))) & " As BarcodeType,
                        " & AgL.Chk_Text(AgL.XNull(DtBarcodeSource.Rows(I)("Code"))) & " As OMSId
                        "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " INSERT INTO BarcodeSiteDetail (Code, Div_Code, Site_Code)
                        VALUES (" & AgL.Chk_Text(mBarcodeCode) & ", 
                        " & AgL.Chk_Text(AgL.PubDivCode) & ", 
                        " & AgL.Chk_Text(AgL.PubSiteCode) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                mQry = " UPDATE Item Set Barcode = '" & mBarcodeCode & "' 
                        Where Code = '" & bItemCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next

            mQry = " Select H.* From SaleInvoice H "
            Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection_External).Tables(0)

            mQry = " Select H.V_Type, H.ManualRefNo, I.Description As ItemDesc, L.*,
                Sids.ItemCategory, '" & bPrathamBrandCode & "' As ItemGroup, Sids.Dimension1, Sids.Dimension2, Sids.Dimension3, Sids.Dimension4, Sids.Size, I.V_Type As ItemV_Type
                From SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN SaleInvoiceDetailSku Sids On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                LEFT JOIN Item I ON L.Item = I.Code "
            Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection_External).Tables(0)

            mQry = " Select H.V_Type, H.ManualRefNo, I.Description As ItemDesc, L.*,
                Sids.ItemCategory, '" & bPrathamBrandCode & "' As ItemGroup, Sids.Dimension1, Sids.Dimension2, Sids.Dimension3, Sids.Dimension4, Sids.Size, I.V_Type As ItemV_Type
                From SaleInvoiceDimensionDetail L 
                LEFT JOIN SaleInvoice H ON H.DocID = L.DocID
                LEFT JOIN SaleInvoiceDimensionDetailSku Sids On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                LEFT JOIN Item I ON L.Item = I.Code "
            Dim DtLineDimensionDetailSource As DataTable = AgL.FillData(mQry, Connection_External).Tables(0)


            mQry = " Select * From PurchInvoice "
            Dim DtPurchInvoice_Destination As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)


            For I = 0 To DtHeaderSource.Rows.Count - 1
                If DtPurchInvoice_Destination.Select("OMSId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'").Length = 0 Then
                    Dim Tot_Gross_Amount As Double = 0
                    Dim Tot_Taxable_Amount As Double = 0
                    Dim Tot_Tax1 As Double = 0
                    Dim Tot_Tax2 As Double = 0
                    Dim Tot_Tax3 As Double = 0
                    Dim Tot_Tax4 As Double = 0
                    Dim Tot_Tax5 As Double = 0
                    Dim Tot_SubTotal1 As Double = 0
                    Dim Tot_Other_Charge As Double = 0
                    Dim Tot_Deduction As Double = 0


                    Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice
                    Dim PurchInvoiceDimensionTableList(0) As FrmPurchInvoiceDirect_WithDimension.StructPurchInvoiceDimensionDetail
                    Dim PurchInvoiceTable As New FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice

                    PurchInvoiceTable.DocID = ""
                    PurchInvoiceTable.V_Type = "PI"
                    PurchInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                    PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                    PurchInvoiceTable.Div_Code = AgL.PubDivCode
                    PurchInvoiceTable.V_No = 0
                    PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.SettingGroup = "AP"
                    PurchInvoiceTable.ManualRefNo = ""
                    PurchInvoiceTable.Vendor = "D100000001"
                    PurchInvoiceTable.VendorName = ""
                    PurchInvoiceTable.AgentCode = ""
                    PurchInvoiceTable.AgentName = ""
                    PurchInvoiceTable.BillToPartyCode = ""
                    PurchInvoiceTable.BillToPartyName = ""
                    PurchInvoiceTable.VendorAddress = ""
                    PurchInvoiceTable.VendorCity = ""
                    PurchInvoiceTable.VendorMobile = ""
                    PurchInvoiceTable.VendorSalesTaxNo = ""
                    PurchInvoiceTable.SalesTaxGroupParty = "Registered"
                    PurchInvoiceTable.PlaceOfSupply = ""
                    PurchInvoiceTable.StructureCode = ""
                    PurchInvoiceTable.CustomFields = ""
                    PurchInvoiceTable.VendorDocNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                    PurchInvoiceTable.VendorDocDate = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.ReferenceDocId = ""
                    PurchInvoiceTable.Tags = ""
                    PurchInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
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
                        PurchInvoiceTable.Line_ItemCategoryCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemCategory")), DtItem, "Code")
                        PurchInvoiceTable.Line_ItemGroupCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemGroup")), DtItem, "Code")
                        PurchInvoiceTable.Line_ItemCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Item")), DtItem, "Code")
                        PurchInvoiceTable.Line_ItemName = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemDesc"))
                        PurchInvoiceTable.Line_ItemV_Type = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemV_Type"))
                        PurchInvoiceTable.Line_Dimension1Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Dimension1")), DtItem, "Code")
                        PurchInvoiceTable.Line_Dimension2Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Dimension2")), DtItem, "Code")
                        PurchInvoiceTable.Line_Dimension3Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Dimension3")), DtItem, "Code")
                        PurchInvoiceTable.Line_Dimension4Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Dimension4")), DtItem, "Code")
                        PurchInvoiceTable.Line_SizeCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Size")), DtItem, "Code")
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
                        PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Gross_Amount"))
                        PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount"))
                        PurchInvoiceTable.Line_Tax1_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1_Per"))
                        PurchInvoiceTable.Line_Tax1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1"))
                        PurchInvoiceTable.Line_Tax2_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2_Per"))
                        PurchInvoiceTable.Line_Tax2 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2"))
                        PurchInvoiceTable.Line_Tax3_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3_Per"))
                        PurchInvoiceTable.Line_Tax3 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3"))
                        PurchInvoiceTable.Line_Tax4_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4_Per"))
                        PurchInvoiceTable.Line_Tax4 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4"))
                        PurchInvoiceTable.Line_Tax5_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5_Per"))
                        PurchInvoiceTable.Line_Tax5 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5"))
                        PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SubTotal1"))
                        PurchInvoiceTable.Line_Other_Charge = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Other_Charge"))
                        PurchInvoiceTable.Line_Deduction = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Deduction"))
                        PurchInvoiceTable.Line_Round_Off = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Round_Off"))
                        PurchInvoiceTable.Line_Net_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Net_Amount"))

                        'For Header Values
                        Tot_Gross_Amount += PurchInvoiceTable.Line_Gross_Amount
                        Tot_Taxable_Amount += PurchInvoiceTable.Line_Taxable_Amount
                        Tot_Tax1 += PurchInvoiceTable.Line_Tax1
                        Tot_Tax2 += PurchInvoiceTable.Line_Tax2
                        Tot_Tax3 += PurchInvoiceTable.Line_Tax3
                        Tot_Tax4 += PurchInvoiceTable.Line_Tax4
                        Tot_Tax5 += PurchInvoiceTable.Line_Tax5
                        Tot_SubTotal1 += PurchInvoiceTable.Line_SubTotal1

                        If DtLineDimensionDetailSource.Rows.Count > 0 Then
                            Dim DtPurchInvoiceDimensionDetail_ForHeader As New DataTable
                            For M As Integer = 0 To DtLineDimensionDetailSource.Columns.Count - 1
                                Dim DColumn As New DataColumn
                                DColumn.ColumnName = DtLineDimensionDetailSource.Columns(M).ColumnName
                                DtPurchInvoiceDimensionDetail_ForHeader.Columns.Add(DColumn)
                            Next

                            Dim DtRowPurchInvoiceDimensionDetail_ForHeader As DataRow() = DtLineDimensionDetailSource.Select("DocId = " + AgL.Chk_Text(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DocId"))) +
                                                    " And [TSr] = " + AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr")), "TSr")
                            If DtRowPurchInvoiceDimensionDetail_ForHeader.Length > 0 Then
                                For M As Integer = 0 To DtRowPurchInvoiceDimensionDetail_ForHeader.Length - 1
                                    DtPurchInvoiceDimensionDetail_ForHeader.Rows.Add()
                                    For N As Integer = 0 To DtPurchInvoiceDimensionDetail_ForHeader.Columns.Count - 1
                                        DtPurchInvoiceDimensionDetail_ForHeader.Rows(M)(N) = DtRowPurchInvoiceDimensionDetail_ForHeader(M)(N)
                                    Next
                                Next
                            End If

                            For K As Integer = 0 To DtPurchInvoiceDimensionDetail_ForHeader.Rows.Count - 1
                                Dim PurchInvoiceDimensionTable As New FrmPurchInvoiceDirect_WithDimension.StructPurchInvoiceDimensionDetail

                                PurchInvoiceDimensionTable.TSr = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("TSr"))
                                PurchInvoiceDimensionTable.Sr = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Sr"))
                                PurchInvoiceDimensionTable.Specification = AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Specification"))
                                PurchInvoiceDimensionTable.ItemCategoryCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("ItemCategory")), DtItem, "Code")
                                PurchInvoiceDimensionTable.ItemGroupCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("ItemGroup")), DtItem, "Code")
                                PurchInvoiceDimensionTable.ItemCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Item")), DtItem, "Code")
                                PurchInvoiceDimensionTable.ItemV_Type = AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("ItemV_Type"))
                                PurchInvoiceDimensionTable.Dimension1Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Dimension1")), DtItem, "Code")
                                PurchInvoiceDimensionTable.Dimension2Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Dimension2")), DtItem, "Code")
                                PurchInvoiceDimensionTable.Dimension3Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Dimension3")), DtItem, "Code")
                                PurchInvoiceDimensionTable.Dimension4Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Dimension4")), DtItem, "Code")
                                PurchInvoiceDimensionTable.SizeCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Size")), DtItem, "Code")
                                PurchInvoiceDimensionTable.Pcs = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Pcs"))
                                PurchInvoiceDimensionTable.Qty = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Qty"))
                                PurchInvoiceDimensionTable.TotalQty = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("TotalQty"))

                                PurchInvoiceDimensionTableList(UBound(PurchInvoiceDimensionTableList)) = PurchInvoiceDimensionTable
                                ReDim Preserve PurchInvoiceDimensionTableList(UBound(PurchInvoiceDimensionTableList) + 1)
                            Next
                        End If

                        PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                        ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                    Next




                    'PurchInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
                    'PurchInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
                    'PurchInvoiceTableList(0).Tax1 = Tot_Tax1
                    'PurchInvoiceTableList(0).Tax2 = Tot_Tax2
                    'PurchInvoiceTableList(0).Tax3 = Tot_Tax3
                    'PurchInvoiceTableList(0).Tax4 = Tot_Tax4
                    'PurchInvoiceTableList(0).Tax5 = Tot_Tax5
                    'PurchInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
                    'PurchInvoiceTableList(0).Other_Charge = 0
                    'PurchInvoiceTableList(0).Deduction = 0
                    'PurchInvoiceTableList(0).Round_Off = Math.Round(Math.Round(PurchInvoiceTableList(0).SubTotal1) - PurchInvoiceTableList(0).SubTotal1, 2)
                    'PurchInvoiceTableList(0).Net_Amount = Math.Round(PurchInvoiceTableList(0).SubTotal1)

                    FrmPurchInvoiceDirect_WithDimension.InsertPurchInvoice(PurchInvoiceTableList, PurchInvoiceDimensionTableList)
                End If
            Next


            AgL.ETrans.Commit()
            mTrans = "Commit"
            Connection_External.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)

            Dim Mdi As New MDIMain
            Dim StrSenderText As String = Mdi.MnuItemMasterBulk.Text
            GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
            GridReportFrm.Filter_IniGrid()
            Dim CRep As ClsItemMasterBulk = New ClsItemMasterBulk(GridReportFrm)
            CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
            CRep.Ini_Grid()
            ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
            GridReportFrm.MdiParent = Me.MdiParent
            GridReportFrm.Show()
            CRep.ProcItemMasterBulk()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            Connection_External.Close()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FImportAllItems(bItemV_Type As String, Connection_External As Object)
        mQry = "Select I.* From Item I Where IfNull(I.V_Type,'') = '" & bItemV_Type & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, Connection_External).Tables(0)

        Dim bLastItemCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("Description")) <> "" Then
                If FGetCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("Code")), DtItem, "Code") = "" Then
                    Dim ItemTable As New FrmItemMaster.StructItem
                    Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemTable.Code = bItemCode
                    ItemTable.Description = AgL.XNull(DtTemp.Rows(I)("Description"))
                    ItemTable.Specification = ItemTable.Description
                    ItemTable.ItemType = ItemTypeCode.TradingProduct
                    ItemTable.V_Type = AgL.XNull(DtTemp.Rows(I)("V_Type"))
                    ItemTable.ItemCategoryCode = FGetCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("ItemCategory")), DtItem, "Code")
                    ItemTable.ItemGroupCode = FGetCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("ItemGroup")), DtItem, "Code")
                    ItemTable.Dimension1Code = FGetCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("Dimension1")), DtItem, "Code")
                    ItemTable.Dimension2Code = FGetCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("Dimension2")), DtItem, "Code")
                    ItemTable.Dimension3Code = FGetCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("Dimension3")), DtItem, "Code")
                    ItemTable.Dimension4Code = FGetCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("Dimension4")), DtItem, "Code")
                    ItemTable.SizeCode = FGetCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("Size")), DtItem, "Code")
                    ItemTable.SalesTaxPostingGroup = AgL.XNull(DtTemp.Rows(I)("SalesTaxPostingGroup"))
                    ItemTable.Unit = AgL.XNull(DtTemp.Rows(I)("Unit"))
                    ItemTable.BarcodeType = AgL.XNull(DtTemp.Rows(I)("BarcodeType"))
                    ItemTable.BarcodePattern = AgL.XNull(DtTemp.Rows(I)("BarcodePattern"))
                    ItemTable.EntryBy = AgL.PubUserName
                    ItemTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    ItemTable.EntryType = "Add"
                    ItemTable.LockText = "Synced From Other Database."
                    ItemTable.EntryStatus = ClsMain.LogStatus.LogOpen
                    ItemTable.Div_Code = AgL.PubDivCode
                    ItemTable.Status = "Active"
                    ItemTable.OMSId = AgL.XNull(DtTemp.Rows(I)("Code"))
                    FrmItemMaster.ImportItemTable(ItemTable)
                End If
            End If
        Next
        mQry = " Select * From Item With (NoLock) "
        DtItem = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
    End Sub
    Private Function FGetCodeFromOMSId(Code As String, DtTable As DataTable, PrimaryKeyField As String) As String
        Dim DtRow As DataRow() = DtTable.Select("OMSId = '" & Code & "'")
        If DtRow.Length > 0 Then
            FGetCodeFromOMSId = DtRow(0)(PrimaryKeyField)
        Else
            FGetCodeFromOMSId = ""
        End If
    End Function
    Private Sub ProcImportSaleInvoiceDataFromSqlite_Gursheel()
        Dim mStrMainQry As String = ""
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim bSelectionQry As String = ""
        Dim mSqliteDataQry As String = ""
        Dim mVendor As String = ""

        If ClsMain.FDivisionNameForCustomization(4) = "Gur " And AgL.StrCmp(AgL.PubDBName, "GuruSheelBranch") Then
            mVendor = "GURHO"
        Else
            mVendor = "GURBRNCH"
        End If

        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName

        Dim Connection_External As New SQLite.SQLiteConnection
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection_External.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Else
            Connection_External.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection_External.Open()

        mQry = " Select * From Item "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)


        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"

        Try
            mQry = "Select Count(*) From SubGroup Where SubCode = '" & mVendor & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                mQry = "INSERT INTO Subgroup (Subcode, SubgroupType, ManualCode, Name, DispName, GroupCode, GroupNature, Nature)
                        VALUES ('" & mVendor & "', 'Supplier', '" & mVendor & "', 'Gursheel Head Office', 'Gursheel Head Office', '0016', 'L', 'Supplier') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            FImportAllItems(ItemV_Type.ItemCategory, Connection_External)
            FImportAllItems(ItemV_Type.ItemGroup, Connection_External)
            FImportAllItems(ItemV_Type.Item, Connection_External)
            FImportAllItems(ItemV_Type.Dimension1, Connection_External)
            FImportAllItems(ItemV_Type.Dimension2, Connection_External)
            FImportAllItems(ItemV_Type.Dimension3, Connection_External)
            FImportAllItems(ItemV_Type.Dimension4, Connection_External)
            FImportAllItems(ItemV_Type.SIZE, Connection_External)
            FImportAllItems(ItemV_Type.SKU, Connection_External)

            mQry = " Select * From Barcode "
            Dim DtBarcodeSource As DataTable = AgL.FillData(mQry, Connection_External).Tables(0)

            Dim mBarcodeCodeStr As String = ""
            For I = 0 To DtBarcodeSource.Rows.Count - 1
                Dim mBarcodeCode As String = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode  With (NoLock)", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                Dim bItemCode As String = FGetCodeFromOMSId(AgL.XNull(DtBarcodeSource.Rows(I)("Item")), DtItem, "Code")
                mQry = " INSERT INTO Barcode (Code, Description, Div_Code, Item, GenDocID, GenSr, 
                        Qty, SaleRate, PurchaseRate, MRP, BarcodeType, OMSId)
                        Select " & AgL.Chk_Text(mBarcodeCode) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtBarcodeSource.Rows(I)("Description"))) & ", 
                        " & AgL.Chk_Text(AgL.PubDivCode) & ", 
                        " & AgL.Chk_Text(bItemCode) & ",
                        Null As GenDocID, Null As GenSr, 
                        " & AgL.VNull(DtBarcodeSource.Rows(I)("Qty")) & " As Qty, 0 As SaleRate, 
                        " & AgL.VNull(DtBarcodeSource.Rows(I)("PurchaseRate")) & " As PurchaseRate, 
                        " & AgL.VNull(DtBarcodeSource.Rows(I)("MRP")) & " As MRP, 
                        " & AgL.Chk_Text(AgL.XNull(DtBarcodeSource.Rows(I)("BarcodeType"))) & " As BarcodeType,
                        " & AgL.Chk_Text(AgL.XNull(DtBarcodeSource.Rows(I)("Code"))) & " As OMSId
                        "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " INSERT INTO BarcodeSiteDetail (Code, Div_Code, Site_Code)
                        VALUES (" & AgL.Chk_Text(mBarcodeCode) & ", 
                        " & AgL.Chk_Text(AgL.PubDivCode) & ", 
                        " & AgL.Chk_Text(AgL.PubSiteCode) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                mQry = " UPDATE Item Set Barcode = '" & mBarcodeCode & "' 
                        Where Code = '" & bItemCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mBarcodeCodeStr += mBarcodeCode + ","
            Next

            mQry = " Select H.* From SaleInvoice H "
            Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection_External).Tables(0)

            mQry = " Select H.V_Type, H.ManualRefNo, I.Description As ItemDesc, L.*,
                Sids.ItemCategory, Sids.ItemGroup, Sids.Dimension1, Sids.Dimension2, Sids.Dimension3, Sids.Dimension4, Sids.Size, I.V_Type As ItemV_Type
                From SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN SaleInvoiceDetailSku Sids On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                LEFT JOIN Item I ON L.Item = I.Code "
            Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection_External).Tables(0)

            mQry = " Select H.V_Type, H.ManualRefNo, I.Description As ItemDesc, L.*,
                Sids.ItemCategory, Sids.ItemGroup, Sids.Dimension1, Sids.Dimension2, Sids.Dimension3, Sids.Dimension4, Sids.Size, I.V_Type As ItemV_Type
                From SaleInvoiceDimensionDetail L 
                LEFT JOIN SaleInvoice H ON H.DocID = L.DocID
                LEFT JOIN SaleInvoiceDimensionDetailSku Sids On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                LEFT JOIN Item I ON L.Item = I.Code "
            Dim DtLineDimensionDetailSource As DataTable = AgL.FillData(mQry, Connection_External).Tables(0)


            mQry = " Select * From PurchInvoice "
            Dim DtPurchInvoice_Destination As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)


            For I = 0 To DtHeaderSource.Rows.Count - 1
                If DtPurchInvoice_Destination.Select("OMSId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'").Length = 0 Then
                    Dim Tot_Gross_Amount As Double = 0
                    Dim Tot_Taxable_Amount As Double = 0
                    Dim Tot_Tax1 As Double = 0
                    Dim Tot_Tax2 As Double = 0
                    Dim Tot_Tax3 As Double = 0
                    Dim Tot_Tax4 As Double = 0
                    Dim Tot_Tax5 As Double = 0
                    Dim Tot_SubTotal1 As Double = 0
                    Dim Tot_Other_Charge As Double = 0
                    Dim Tot_Deduction As Double = 0


                    Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice
                    Dim PurchInvoiceDimensionTableList(0) As FrmPurchInvoiceDirect_WithDimension.StructPurchInvoiceDimensionDetail
                    Dim PurchInvoiceTable As New FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice

                    PurchInvoiceTable.DocID = ""
                    PurchInvoiceTable.V_Type = "PI"
                    PurchInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                    PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                    PurchInvoiceTable.Div_Code = AgL.PubDivCode
                    PurchInvoiceTable.V_No = 0
                    PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.SettingGroup = ""
                    PurchInvoiceTable.ManualRefNo = ""
                    PurchInvoiceTable.Vendor = mVendor
                    PurchInvoiceTable.VendorName = ""
                    PurchInvoiceTable.AgentCode = ""
                    PurchInvoiceTable.AgentName = ""
                    PurchInvoiceTable.BillToPartyCode = mVendor
                    PurchInvoiceTable.BillToPartyName = ""
                    PurchInvoiceTable.VendorAddress = ""
                    PurchInvoiceTable.VendorCity = ""
                    PurchInvoiceTable.VendorMobile = ""
                    PurchInvoiceTable.VendorSalesTaxNo = ""
                    PurchInvoiceTable.SalesTaxGroupParty = "Registered"
                    PurchInvoiceTable.PlaceOfSupply = ""
                    PurchInvoiceTable.StructureCode = ""
                    PurchInvoiceTable.CustomFields = ""
                    PurchInvoiceTable.VendorDocNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                    PurchInvoiceTable.VendorDocDate = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.ReferenceDocId = ""
                    PurchInvoiceTable.Tags = ""
                    PurchInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
                    PurchInvoiceTable.Status = "Active"
                    PurchInvoiceTable.EntryBy = AgL.PubUserName
                    PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    PurchInvoiceTable.ApproveBy = ""
                    PurchInvoiceTable.ApproveDate = ""
                    PurchInvoiceTable.MoveToLog = ""
                    PurchInvoiceTable.MoveToLogDate = ""
                    PurchInvoiceTable.UploadDate = ""
                    PurchInvoiceTable.OmsId = AgL.XNull(DtHeaderSource.Rows(I)("DocId"))
                    PurchInvoiceTable.LockText = ""

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
                        PurchInvoiceTable.Line_ItemCategoryCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemCategory")), DtItem, "Code")
                        PurchInvoiceTable.Line_ItemGroupCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemGroup")), DtItem, "Code")
                        PurchInvoiceTable.Line_ItemCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Item")), DtItem, "Code")
                        PurchInvoiceTable.Line_ItemName = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemDesc"))
                        PurchInvoiceTable.Line_ItemV_Type = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemV_Type"))
                        PurchInvoiceTable.Line_Dimension1Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Dimension1")), DtItem, "Code")
                        PurchInvoiceTable.Line_Dimension2Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Dimension2")), DtItem, "Code")
                        PurchInvoiceTable.Line_Dimension3Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Dimension3")), DtItem, "Code")
                        PurchInvoiceTable.Line_Dimension4Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Dimension4")), DtItem, "Code")
                        PurchInvoiceTable.Line_SizeCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Size")), DtItem, "Code")
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
                        PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Gross_Amount"))
                        PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount"))
                        PurchInvoiceTable.Line_Tax1_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1_Per"))
                        PurchInvoiceTable.Line_Tax1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1"))
                        PurchInvoiceTable.Line_Tax2_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2_Per"))
                        PurchInvoiceTable.Line_Tax2 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2"))
                        PurchInvoiceTable.Line_Tax3_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3_Per"))
                        PurchInvoiceTable.Line_Tax3 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3"))
                        PurchInvoiceTable.Line_Tax4_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4_Per"))
                        PurchInvoiceTable.Line_Tax4 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4"))
                        PurchInvoiceTable.Line_Tax5_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5_Per"))
                        PurchInvoiceTable.Line_Tax5 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5"))
                        PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SubTotal1"))
                        PurchInvoiceTable.Line_Other_Charge = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Other_Charge"))
                        PurchInvoiceTable.Line_Deduction = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Deduction"))
                        PurchInvoiceTable.Line_Round_Off = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Round_Off"))
                        PurchInvoiceTable.Line_Net_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Net_Amount"))

                        'For Header Values
                        Tot_Gross_Amount += PurchInvoiceTable.Line_Gross_Amount
                        Tot_Taxable_Amount += PurchInvoiceTable.Line_Taxable_Amount
                        Tot_Tax1 += PurchInvoiceTable.Line_Tax1
                        Tot_Tax2 += PurchInvoiceTable.Line_Tax2
                        Tot_Tax3 += PurchInvoiceTable.Line_Tax3
                        Tot_Tax4 += PurchInvoiceTable.Line_Tax4
                        Tot_Tax5 += PurchInvoiceTable.Line_Tax5
                        Tot_SubTotal1 += PurchInvoiceTable.Line_SubTotal1

                        If DtLineDimensionDetailSource.Rows.Count > 0 Then
                            Dim DtPurchInvoiceDimensionDetail_ForHeader As New DataTable
                            For M As Integer = 0 To DtLineDimensionDetailSource.Columns.Count - 1
                                Dim DColumn As New DataColumn
                                DColumn.ColumnName = DtLineDimensionDetailSource.Columns(M).ColumnName
                                DtPurchInvoiceDimensionDetail_ForHeader.Columns.Add(DColumn)
                            Next

                            Dim DtRowPurchInvoiceDimensionDetail_ForHeader As DataRow() = DtLineDimensionDetailSource.Select("DocId = " + AgL.Chk_Text(AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DocId"))) +
                                                    " And [TSr] = " + AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr")), "TSr")
                            If DtRowPurchInvoiceDimensionDetail_ForHeader.Length > 0 Then
                                For M As Integer = 0 To DtRowPurchInvoiceDimensionDetail_ForHeader.Length - 1
                                    DtPurchInvoiceDimensionDetail_ForHeader.Rows.Add()
                                    For N As Integer = 0 To DtPurchInvoiceDimensionDetail_ForHeader.Columns.Count - 1
                                        DtPurchInvoiceDimensionDetail_ForHeader.Rows(M)(N) = DtRowPurchInvoiceDimensionDetail_ForHeader(M)(N)
                                    Next
                                Next
                            End If

                            For K As Integer = 0 To DtPurchInvoiceDimensionDetail_ForHeader.Rows.Count - 1
                                Dim PurchInvoiceDimensionTable As New FrmPurchInvoiceDirect_WithDimension.StructPurchInvoiceDimensionDetail

                                PurchInvoiceDimensionTable.TSr = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("TSr"))
                                PurchInvoiceDimensionTable.Sr = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Sr"))
                                PurchInvoiceDimensionTable.Specification = AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Specification"))
                                PurchInvoiceDimensionTable.ItemCategoryCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("ItemCategory")), DtItem, "Code")
                                PurchInvoiceDimensionTable.ItemGroupCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("ItemGroup")), DtItem, "Code")
                                PurchInvoiceDimensionTable.ItemCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Item")), DtItem, "Code")
                                PurchInvoiceDimensionTable.ItemV_Type = AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("ItemV_Type"))
                                PurchInvoiceDimensionTable.Dimension1Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Dimension1")), DtItem, "Code")
                                PurchInvoiceDimensionTable.Dimension2Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Dimension2")), DtItem, "Code")
                                PurchInvoiceDimensionTable.Dimension3Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Dimension3")), DtItem, "Code")
                                PurchInvoiceDimensionTable.Dimension4Code = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Dimension4")), DtItem, "Code")
                                PurchInvoiceDimensionTable.SizeCode = FGetCodeFromOMSId(AgL.XNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Size")), DtItem, "Code")
                                PurchInvoiceDimensionTable.Pcs = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Pcs"))
                                PurchInvoiceDimensionTable.Qty = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("Qty"))
                                PurchInvoiceDimensionTable.TotalQty = AgL.VNull(DtPurchInvoiceDimensionDetail_ForHeader.Rows(K)("TotalQty"))

                                PurchInvoiceDimensionTableList(UBound(PurchInvoiceDimensionTableList)) = PurchInvoiceDimensionTable
                                ReDim Preserve PurchInvoiceDimensionTableList(UBound(PurchInvoiceDimensionTableList) + 1)
                            Next
                        End If

                        PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                        ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                    Next




                    'PurchInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
                    'PurchInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
                    'PurchInvoiceTableList(0).Tax1 = Tot_Tax1
                    'PurchInvoiceTableList(0).Tax2 = Tot_Tax2
                    'PurchInvoiceTableList(0).Tax3 = Tot_Tax3
                    'PurchInvoiceTableList(0).Tax4 = Tot_Tax4
                    'PurchInvoiceTableList(0).Tax5 = Tot_Tax5
                    'PurchInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
                    'PurchInvoiceTableList(0).Other_Charge = 0
                    'PurchInvoiceTableList(0).Deduction = 0
                    'PurchInvoiceTableList(0).Round_Off = Math.Round(Math.Round(PurchInvoiceTableList(0).SubTotal1) - PurchInvoiceTableList(0).SubTotal1, 2)
                    'PurchInvoiceTableList(0).Net_Amount = Math.Round(PurchInvoiceTableList(0).SubTotal1)

                    Dim bDocId As String = FrmPurchInvoiceDirect_WithDimension.InsertPurchInvoice(PurchInvoiceTableList, PurchInvoiceDimensionTableList)
                    mQry = " UPDATE Barcode Set GenDocID = '" & bDocId & "' 
                            Where Code In ('" & mBarcodeCodeStr.ToString.Replace(",", "','") & "')"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next


            AgL.ETrans.Commit()
            mTrans = "Commit"
            Connection_External.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)

            Dim Mdi As New MDIMain
            Dim StrSenderText As String = Mdi.MnuItemMasterBulk.Text
            GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
            GridReportFrm.Filter_IniGrid()
            Dim CRep As ClsItemMasterBulk = New ClsItemMasterBulk(GridReportFrm)
            CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
            CRep.Ini_Grid()
            ClsMain.FAdjustBackgroudMaximizedWindow(Me.MdiParent)
            GridReportFrm.MdiParent = Me.MdiParent
            GridReportFrm.Show()
            CRep.ProcItemMasterBulk()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            Connection_External.Close()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ProcImportSaleInvoiceDataFromSqlite_Sadhvi()
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
                    From SaleInvoice H "
            Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)

            mQry = " Select H.V_Type, H.ManualRefNo, I.Description As ItemDesc,
                L.*
                From SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
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
                    PurchInvoiceTable.Div_Code = AgL.XNull(DtHeaderSource.Rows(I)("Div_Code"))
                    PurchInvoiceTable.V_No = 0
                    PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.ManualRefNo = ""
                    PurchInvoiceTable.Vendor = ""
                    If PurchInvoiceTable.Div_Code = "E" Then
                        PurchInvoiceTable.VendorName = "SADHVI EMBROIDERY"
                    Else
                        PurchInvoiceTable.VendorName = "SADHVI ENTERPRISES"
                    End If
                    PurchInvoiceTable.AgentCode = ""
                    PurchInvoiceTable.AgentName = ""
                    PurchInvoiceTable.BillToPartyCode = ""
                    PurchInvoiceTable.BillToPartyName = PurchInvoiceTable.VendorName
                    PurchInvoiceTable.VendorAddress = ""
                    PurchInvoiceTable.VendorCity = ""
                    PurchInvoiceTable.VendorMobile = ""
                    PurchInvoiceTable.VendorSalesTaxNo = ""
                    PurchInvoiceTable.SalesTaxGroupParty = "Registered"
                    PurchInvoiceTable.PlaceOfSupply = AgLibrary.ClsMain.agConstants.PlaceOfSupplay.OutsideState
                    PurchInvoiceTable.StructureCode = ""
                    PurchInvoiceTable.CustomFields = ""
                    PurchInvoiceTable.VendorDocNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                    PurchInvoiceTable.VendorDocDate = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.ReferenceDocId = ""
                    PurchInvoiceTable.Tags = ""
                    PurchInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
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

                        If AgL.VNull(AgL.Dman_Execute(" Select Rate From RateListDetail 
                                Where Code = '" & PurchInvoiceTable.Line_ItemCode & "'
                                And RateType Is Null ", AgL.GCn).ExecuteScalar()) <> PurchInvoiceTable.Line_Rate Then
                            mQry = " Update RateListDetail Set Rate = " & PurchInvoiceTable.Line_Rate & " 
                                    Where Code = '" & PurchInvoiceTable.Line_ItemCode & "' 
                                    And RateType Is Null "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " Update Item Set Rate = " & PurchInvoiceTable.Line_Rate & " 
                                    Where Code = '" & PurchInvoiceTable.Line_ItemCode & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If



                        PurchInvoiceTable.Line_DiscountPer = 0
                        PurchInvoiceTable.Line_DiscountAmount = 0
                        PurchInvoiceTable.Line_AdditionalDiscountPer = 0
                        PurchInvoiceTable.Line_AdditionalDiscountAmount = 0
                        PurchInvoiceTable.Line_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                        PurchInvoiceTable.Line_Remark = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Remark"))
                        PurchInvoiceTable.Line_BaleNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("BaleNo"))
                        PurchInvoiceTable.Line_LotNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("LotNo"))
                        PurchInvoiceTable.Line_ReferenceDocId = ""
                        PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Gross_Amount"))
                        PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount"))
                        PurchInvoiceTable.Line_Tax1_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1_Per"))
                        PurchInvoiceTable.Line_Tax1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax1"))
                        PurchInvoiceTable.Line_Tax2_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2_Per"))
                        PurchInvoiceTable.Line_Tax2 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax2"))
                        PurchInvoiceTable.Line_Tax3_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3_Per"))
                        PurchInvoiceTable.Line_Tax3 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax3"))
                        PurchInvoiceTable.Line_Tax4_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4_Per"))
                        PurchInvoiceTable.Line_Tax4 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax4"))
                        PurchInvoiceTable.Line_Tax5_Per = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5_Per"))
                        PurchInvoiceTable.Line_Tax5 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Tax5"))
                        PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SubTotal1"))
                        PurchInvoiceTable.Line_Other_Charge = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Other_Charge"))
                        PurchInvoiceTable.Line_Deduction = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Deduction"))
                        PurchInvoiceTable.Line_Round_Off = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Round_Off"))
                        PurchInvoiceTable.Line_Net_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Net_Amount"))


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
End Class