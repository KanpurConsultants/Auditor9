Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class ClsShyamaShyam
    Private mQry As String = ""
    Public Sub FSeedData_ShyamaShyam()
        Dim ClsObj As New Customised.ClsMain(AgL)
        Try
            If ClsMain.IsScopeOfWorkContains("+Cloth Aadhat Module") Or ClsMain.IsScopeOfWorkContains("+Double Entry Module") Or
                ClsMain.IsScopeOfWorkContains(IndustryType.SubIndustryType.AadhatModule) Then
                If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or
                        ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                    FCreateTable_SaleInvoiceGeneratedEntries()
                    FCreateTable_WPurchInvoiceDetail()
                    FCreateTable_WSaleInvoiceDetail()
                    FCreateTable_WLedgerHeadDetail()
                    FSeedRequiredData()

                    FAlterTable_SaleInvoice()
                    FAlterTable_PurchInvoice()
                    FAlterTable_LedgerHead()
                    FAlterTable_StockHead()

                    FConfigure_SaleReturn(ClsObj)
                    FConfigure_MasterSupplier(ClsObj)
                    FConfigure_OpeningEntry(ClsObj)
                    FConfigure_JournalEntry(ClsObj)
                    FConfigure_PaymentEntry(ClsObj)
                    FConfigure_ReceiptEntry(ClsObj)
                    'FConfigure_PaymentSettlementEntry(ClsObj)
                    'FConfigure_CustomerSettlementEntry(ClsObj)

                    FConfigure_ItemGroup(ClsObj)
                    FConfigure_Person(ClsObj)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FSeedData_TextileIndustry")
        End Try
    End Sub
    Private Sub FCreateTable_SaleInvoiceGeneratedEntries()
        Try
            If Not AgL.IsTableExist("SaleInvoiceGeneratedEntries", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SaleInvoiceGeneratedEntries] (Code nVarchar(10) COLLATE NOCASE); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "Type", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "DocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "V_Type", "nVarchar(5)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "SaleOrderNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "SaleOrderDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "Site_Code", "nVarchar(1)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "Div_Code", "nVarchar(1)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "ApproveBy", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "ApproveDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "TransactionType", "nVarchar(20)", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_SubgroupType]")
        End Try
    End Sub

    Private Sub FSeedRequiredData()
        If AgL.FillData("Select * from SubGroup Where SubCode='RateDiff'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, CityCode, PIN, Phone, Mobile, EMail, Status, SalesTaxPostingGroup, Parent, SubgroupType, Address)
                    VALUES('RateDiff', '1', 'D', NULL, 'Rate Diff A/c', 'Rate Diff A/c', '0023', '', 'Rate Diff', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select * from SubGroup Where SubCode='DiscDiff'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, CityCode, PIN, Phone, Mobile, EMail, Status, SalesTaxPostingGroup, Parent, SubgroupType, Address)
                    VALUES('DiscDiff', '1', 'D', NULL, 'Discount Diff A/c', 'Discount Diff A/c', '0023', '', 'Discount Diff', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then


            If AgL.FillData("Select * from Voucher_Type Where V_Type='WSI'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Structure, ReportName, PrintingDescription, HeaderAccountDrCr)
                VALUES('SI', 'SALE', 'WSI', 'W Sale Invoice', 'WSI', 'Y', 0, 1, 'sa', '2012-10-11', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'MnuSalesEntry', 'Sales Entry', NULL, NULL, NULL, NULL, NULL, NULL, 'GstSaleW', NULL, 'W Sale Invoice', 'N/A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            mQry = "  INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WSI' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WSI') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'SI'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            If AgL.FillData("Select * from Voucher_Type Where V_Type='WPI'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Structure, ReportName, PrintingDescription, HeaderAccountDrCr)
                VALUES('PI', 'PURCH', 'WPI', 'W Purchase Invoice', 'WPI', 'Y', 0, 1, 'sa', '2012-10-11', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'MnuSalesEntry', 'Sales Entry', NULL, NULL, NULL, NULL, NULL, NULL, 'GstPurW', NULL, 'W Purchase Invoice', 'N/A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from Voucher_Type Where V_Type='WDNS'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Nature, Structure, ReportName, PrintingDescription, HeaderAccountDrCr, Status, ManualRefType, VoucherTypeTags, DivisionList, SiteList, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Div_Code, LockText)
                        VALUES ('WDNS', 'PURCH', 'WDNS', 'W DEBIT NOTE (SUPPLIERS)', 'WDNS', 'Y', NULL, NULL, 'SA', NULL, 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'MnuDebitNote', 'Voucher Entry', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'GstPur', NULL, NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from Voucher_Type Where V_Type='WCNS'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Nature, Structure, ReportName, PrintingDescription, HeaderAccountDrCr, Status, ManualRefType, VoucherTypeTags, DivisionList, SiteList, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Div_Code, LockText)
                        VALUES ('WCNS', 'PURCH', 'WCNS', 'W CREDIT NOTE (SUPPLIERS)', 'WCNS', 'Y', NULL, NULL, 'SA', NULL, 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'MnuCreditNote', 'Credit Note Entry', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'GstPur', NULL, NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            mQry = " INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WPI' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WPI') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'PI'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            If AgL.FillData("Select * from Voucher_Type Where V_Type='WPMT'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Structure, ReportName, PrintingDescription, HeaderAccountDrCr)
                VALUES('PMT', 'PMT', 'WPMT', 'W Payment', 'WPMT', 'Y', 0, 1, 'sa', '2012-10-11', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'MnuSalesEntry', 'Sales Entry', NULL, NULL, NULL, NULL, NULL, NULL, 'GstSale', NULL, 'W Sale Invoice', 'N/A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            mQry = "  INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WPMT' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WPMT') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'PMT'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            If AgL.FillData("Select * from Voucher_Type Where V_Type='WRCT'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Structure, ReportName, PrintingDescription, HeaderAccountDrCr)
                VALUES('RCT', 'RCT', 'WRCT', 'W Receipt', 'WRCT', 'Y', 0, 1, 'sa', '2012-10-11', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'MnuSalesEntry', 'Sales Entry', NULL, NULL, NULL, NULL, NULL, NULL, 'GstSale', NULL, 'W Sale Invoice', 'N/A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            mQry = "  INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WRCT' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WRCT') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'RCT'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)



            If AgL.FillData("Select * from Voucher_Type Where V_Type='WPS'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Structure, ReportName, PrintingDescription, HeaderAccountDrCr)
                VALUES('PS', 'PS', 'WPS', 'W PAYMENT SETTLEMENT', 'WPS', 'Y', 0, 1, 'sa', '2012-10-11', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'MnuPaymentSettlementEntry', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'W PAYMENT SETTLEMENT', 'DR') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            mQry = "  INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WPS' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WPS') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'PS'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)



            If AgL.FillData("Select * from Voucher_Type Where V_Type='WPS'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Structure, ReportName, PrintingDescription, HeaderAccountDrCr)
                VALUES('RS', 'RS', 'WRS', 'W Receipt Settlement', 'WRS', 'Y', 0, 1, 'sa', '2012-10-11', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'FrmCustomerAcSettlementAadhat', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'W Receipt Settlement', 'CR') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            mQry = "  INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WRS' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WRS') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'RS'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)





            If AgL.FillData("Select * from Voucher_Type Where V_Type='WSR'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Structure, ReportName, PrintingDescription, HeaderAccountDrCr)
                VALUES('SR', 'SALE', 'WSR', 'W Sale Return', 'WSR', 'Y', 0, 1, 'sa', '2012-10-11', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'MnuSalesEntry', 'Sales Entry', NULL, NULL, NULL, NULL, NULL, NULL, 'GstSale', NULL, 'W Sale Invoice', 'N/A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            mQry = "  INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WSR' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WSR') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'SR'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            If AgL.FillData("Select * from Voucher_Type Where V_Type='WPR'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type, Structure, ReportName, PrintingDescription, HeaderAccountDrCr)
                VALUES('PR', 'PURCH', 'WPR', 'W Purchase Return', 'WPR', 'Y', 0, 1, 'sa', '2012-10-11', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'Customised', NULL, NULL, 1, NULL, 'MnuSalesEntry', 'Sales Entry', NULL, NULL, NULL, NULL, NULL, NULL, 'GstPur', NULL, 'W Purchase Invoice', 'N/A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            mQry = " INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WPR' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WPR') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'PR'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)



            mQry = " INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WDNS' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WDNS') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'DNS'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            mQry = " INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code)
                    SELECT 'WCNS' As V_Type, H.Date_From, H.Prefix, 0 As Start_Srl_No, H.Date_To, H.Comp_Code, H.Site_Code, H.Div_Code
                    FROM Voucher_Prefix H 
                    LEFT JOIN (
                        SELECT V.Prefix, Site_Code, Div_Code
                        FROM Voucher_Prefix V
                        WHERE V.V_Type = 'WCNS') AS V1 ON H.Prefix = V1.Prefix And H.Site_Code = V1.Site_Code And H.Div_Code = V1.Div_Code
                    WHERE H.V_Type = 'CNS'
                    AND V1.Prefix IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub FCreateTable_WPurchInvoiceDetail()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("WPurchInvoiceDetail", AgL.GcnMain) Then
                mQry = " CREATE TABLE [WPurchInvoiceDetail] (Code nVarchar(10) COLLATE NOCASE); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "Sr", "Int", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "PakkaSaleInvoiceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "AddedManuallySr", "Int", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "IsThirdPartyBilling", "Int", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "SyncedPurchInvoiceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "Supplier", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "InvoiceNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "InvoiceDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "ItemGroup", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "InvoiceDiscountPer", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "InvoiceAdditionalDiscountPer", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "Tax", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "DiscountPer", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "AdditionalDiscountPer", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "AdditionPer", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "Amount", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "AmountWithoutDiscountAndTax", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "MasterSupplier", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "WInvoiceNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "WInvoiceDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "WQty", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "WFreight", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "WPacking", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "WAmount", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "WPurchInvoiceAmount", "float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "Commission", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "AdditionalCommission", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "WPurchInvoiceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WPurchInvoiceDetail", "GeneratedDocId", "nVarchar(21)", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_WPurchInvoiceDetail]")
        End Try
    End Sub

    Private Sub FCreateTable_WSaleInvoiceDetail()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("WSaleInvoiceDetail", AgL.GcnMain) Then
                mQry = " CREATE TABLE [WSaleInvoiceDetail] (Code nVarchar(10) COLLATE NOCASE); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "Sr", "Int", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "PakkaSaleInvoiceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "AddedManuallySr", "Int", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "IsThirdPartyBilling", "Int", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "SyncedSaleInvoiceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "Party", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "InvoiceNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "InvoiceDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "ItemGroup", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "DiscountPer", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "AdditionalDiscountPer", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "ExtraDiscountPer", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "AdditionPer", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "Amount", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "AmountWithoutTax", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "Tax", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "Discount", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "MasterParty", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "ShipToParty", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "WSaleOrderDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "WInvoiceNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "WInvoiceDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "WQty", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "WFreight", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "WPacking", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "WDiscount", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "WSaleInvoiceAmount", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "WSaleInvoiceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WSaleInvoiceDetail", "GeneratedDocId", "nVarchar(21)", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_WSaleInvoiceDetail]")
        End Try
    End Sub


    Private Sub FCreateTable_WLedgerHeadDetail()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("WLedgerHeadDetail", AgL.GcnMain) Then
                mQry = " CREATE TABLE [WLedgerHeadDetail] (Code nVarchar(10) COLLATE NOCASE); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite(AgL.GcnMain, "WLedgerHeadDetail", "Sr", "Int", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WLedgerHeadDetail", "DrCr", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WLedgerHeadDetail", "V_Date", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WLedgerHeadDetail", "Party", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WLedgerHeadDetail", "LinkedParty", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WLedgerHeadDetail", "ReasonAc", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WLedgerHeadDetail", "Amount", "Float", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WLedgerHeadDetail", "SyncedPurchInvoiceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "WLedgerHeadDetail", "Remark", "nVarchar(255)", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_WLedgerHeadDetail]")
        End Try
    End Sub
    Private Sub FConfigure_SaleReturn(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceReturn_Aadhat", Ncat.SaleReturn, "Dgl2", FrmSaleInvoiceReturn_Aadhat.hcPartyDocNo, 1, 0, 0, "Against Sale Inv. No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceReturn_Aadhat", Ncat.SaleReturn, "Dgl2", FrmSaleInvoiceReturn_Aadhat.hcPartyDocDate, 1, 0, 0, "Against Sale Inv. Date")

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceReturn_Aadhat", Ncat.SaleReturn, "DGL2", FrmSaleInvoiceReturn_Aadhat.hcSupplier, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceReturn_Aadhat", Ncat.SaleReturn, "DGL2", FrmSaleInvoiceReturn_Aadhat.hcMasterSupplier, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceReturn_Aadhat", Ncat.SaleReturn, "DGL2", FrmSaleInvoiceReturn_Aadhat.hcReturnNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceReturn_Aadhat", Ncat.SaleReturn, "DGL2", FrmSaleInvoiceReturn_Aadhat.hcReturnDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceReturn_Aadhat", Ncat.SaleReturn, "DGL2", FrmSaleInvoiceReturn_Aadhat.hcPurchaseInvoiceNo, 1)


        If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAmsDocNo, 1,,, "Ams Ret.No.")
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAmsDocDate, 1,,, "Ams Ret.Date")
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAmsDocNetAmount, 1,,, "Ams Ret.Amt")


            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5ItemGroup, False)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5ParentSupplier, False)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5Supplier, True)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5PlaceOfSupply, False)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5PurchInvoiceNo, True)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5PurchInvoiceDate, True)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5GrossAmount, True)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5TotalTax, True)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5OtherCharge, False)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5OtherCharge1, False)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5Deduction, False)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5NetAmount, True)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5CommissionAmount, True)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AdditionalCommissionAmount, False)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AmsDocNo, True)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AmsDocDate, True)
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AmsDocAmount, True)




            mQry = "UPDATE Setting SET Value = 'WPR'
                WHERE SettingType = 'General' AND Div_Code IS NULL AND Site_Code IS NULL 
                AND Category IS NULL AND NCat = 'SR' 
                AND FieldName = 'Generated Entry V_Type' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            mQry = "UPDATE Voucher_Type SET Status  = 'InActive' WHERE V_Type = 'SR'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            mQry = "UPDATE Voucher_Type SET Structure = 'GstSaleW' WHERE V_Type = 'WSR';"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            mQry = "UPDATE Voucher_Type SET Structure = 'GstPurW' WHERE V_Type = 'WPR';"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub FAlterTable_SaleInvoice()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoice", "IsAlreadyUploaded", "Bit", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoice", "AmsDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoice", "AmsDocTaxAmount", "Float", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & " [FAlterTable_SaleInvoice]")
        End Try
    End Sub
    Private Sub FAlterTable_PurchInvoice()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "PurchInvoice", "IsAlreadyUploaded", "Bit", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "PurchInvoice", "AmsDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "PurchInvoice", "AmsDocTaxAmount", "Float", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & " [FAlterTable_PurchInvoice]")
        End Try
    End Sub
    Private Sub FAlterTable_LedgerHead()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "LedgerHead", "IsAlreadyUploaded", "Bit", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & " [FAlterTable_LedgerHead]")
        End Try
    End Sub
    Private Sub FAlterTable_StockHead()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "StockHead", "IsAlreadyUploaded", "Bit", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & " [FAlterTable_StockHead]")
        End Try
    End Sub
    Private Sub FConfigure_MasterSupplier(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", FrmPerson.hcFairDiscountPer, 1, 0)
    End Sub
    Private Sub FConfigure_OpeningEntry(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmJournalEntry", "", Ncat.OpeningBalance, "", "", "", "Dgl1", FrmJournalEntry.Col1LinkedSubcode, 1, 0, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmJournalEntry", Ncat.OpeningBalance, "Dgl1", FrmJournalEntry.Col1AmsReferenceNo, True,, "Ams Bill No")
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmJournalEntry", Ncat.OpeningBalance, "Dgl1", FrmJournalEntry.Col1AmsReferenceDate, True,, "Ams Bill Date")
    End Sub
    Private Sub FConfigure_JournalEntry(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmJournalEntry", "", Ncat.JournalVoucher, "", "", "", "Dgl1", FrmJournalEntry.Col1LinkedSubcode, 1, 0, 0, "")
    End Sub
    Private Sub FConfigure_PaymentEntry(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmVoucherEntry", "", Ncat.Payment, "", "", "", "Dgl1", FrmVoucherEntry.Col1LinkedSubcode, 1, 0, 0, "")
    End Sub
    Private Sub FConfigure_ReceiptEntry(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmVoucherEntry", "", Ncat.Receipt, "", "", "", "Dgl1", FrmVoucherEntry.Col1LinkedSubcode, 1, 0, 0, "")
    End Sub
    Private Sub FConfigure_ItemGroup(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmItemGroup", ItemTypeCode.TradingProduct, "DglRateType", ClsMain.ConfigurableFields.FrmItemGroupLineRateType.ExtraDiscountPer, True)
    End Sub
    Private Sub FConfigure_Person(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ClsMain.ConfigurableFields.FrmPersonHeaderDgl1.ExtraDiscount, 1, 0)
    End Sub
    Private Sub FConfigure_PaymentSettlementEntry(ClsObj As ClsMain)
        mQry = "UPDATE Voucher_Type SET Status  = 'InActive' WHERE V_Type = 'PS'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_CustomerSettlementEntry(ClsObj As ClsMain)
        mQry = "UPDATE Voucher_Type SET Status  = 'InActive' WHERE V_Type = 'RS'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_EInvoice(ClsObj As ClsMain)
        'KANPUR
        'SSFK@1960
        'Nikhil@123
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoiceUserName, "API_SSFK@1960", AgDataType.Text, "50",,,,, "1")
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoicePassword, "P@ssw0rd!", AgDataType.Text, "50",,,,, "1")

        ''AHMEDABAD
        ''SSFA@1960
        ''Nikhil@123
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoiceUserName, "API_SSFA@1960", AgDataType.Text, "50",,,,, "2")
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoicePassword, "P@ssw0rd!", AgDataType.Text, "50",,,,, "2")

        ''LUDIYANA
        ''SSFL@1960
        ''Nikhil@123
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoiceUserName, "API_SSFL@1960", AgDataType.Text, "50",,,,, "3")
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoicePassword, "P@ssw0rd!", AgDataType.Text, "50",,,,, "3")

        ''DELHI
        ''Shyama_dlh
        ''Nikhil@123
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoiceUserName, "API_Shyama_dlh", AgDataType.Text, "50",,,,, "4")
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoicePassword, "P@ssw0rd!", AgDataType.Text, "50",,,,, "4")

        ''JABALPUR
        ''SSFJ@1960
        ''Nikhil@123
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoiceUserName, "API_SSFJ@1960", AgDataType.Text, "50",,,,, "5")
        ClsObj.FSeedSingleIfNotExist_Setting("E Invoice", "", "", SettingFields.DivisionSiteEInvoicePassword, "P@ssw0rd!", AgDataType.Text, "50",,,,, "5")
    End Sub
End Class
