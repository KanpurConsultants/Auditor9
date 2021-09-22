Imports AgLibrary.ClsMain.agConstants
Imports AgTemplate.ClsMain

Public Class ClsSyncFinancialDataFromActual
    Dim mQry As String = ""
    Dim Connection_Pakka As New SQLite.SQLiteConnection
    Public mDbPath As String = ""
    Public Sub FProcSave()
        Dim mTrans As String = ""
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            FPostVoucherEntry_New(AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"
            MsgBox("Entry Saved Successfullt...", MsgBoxStyle.Information)
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FPostVoucherEntry_New(Conn As Object, Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtHeader As DataTable = Nothing
        Dim DtLine As DataTable = Nothing
        Dim DtDestination As DataTable = Nothing
        Dim bMultiplier As Integer = 1
        Dim I As Integer
        Dim J As Integer
        Dim StrErrLog As String = ""

        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        Connection_Pakka.Open()

        mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, 
                H.ReferenceNo, H.Subcode, H.DrCr, H.UptoDate, H.Remarks, H.Status, Sg.Name AS HeaderSubCodeName, 
                H.PartyName, H.PartyAddress, H.PartyPincode, 
                H.PartyCity, H.PartyMobile, H.PartySalesTaxNo, H.ShipToAddress, H.SalesTaxGroupParty, H.PlaceOfSupply, H.Structure, 
                H.CustomFields, H.PartyDocNo, H.PartyDocDate, H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, H.MoveToLog, 
                H.MoveToLogDate, H.UploadDate, H.PartyAadharNo, H.PartyPanNo, H.InUseBy, H.InUseToken, H.ManualRefNo, H.PaymentMode,
                Hc.Gross_Amount, Hc.Taxable_Amount, Hc.Tax1_Per, Hc.Tax1, Hc.Tax2_Per, Hc.Tax2, Hc.Tax3_Per, Hc.Tax3, 
                Hc.Tax4_Per, Hc.Tax4, Hc.Tax5_Per, Hc.Tax5, Hc.SubTotal1, Hc.Deduction_Per, Hc.Deduction, Hc.Other_Charge_Per, 
                Hc.Other_Charge, Hc.Round_Off, Hc.Net_Amount, Hc.SpecialDiscount_Per, Hc.SpecialDiscount,
                L.Sr AS Line_Sr, L.Subcode AS Line_Subcode, Sg1.Name AS Line_SubCodeName, 
                Sg2.Name AS Line_LinkedSubCodeName, 
                L.SpecificationDocID AS Line_SpecificationDocID, 
                L.SpecificationDocIDSr AS Line_SpecificationDocIDSr, 
                L.Specification AS Line_Specification, L.SalesTaxGroupItem AS Line_SalesTaxGroupItem, L.Qty AS Line_Qty, L.Unit AS Line_Unit, 
                L.Rate AS Line_Rate, L.Amount AS Line_Amount, L.ChqRefNo AS Line_ChqRefNo, L.ChqRefDate AS Line_ChqRefDate, 
                L.EffectiveDate AS Line_EffectiveDate, 
                L.Remarks AS Line_Remarks, L.ReferenceDocID AS Line_ReferenceDocID, L.ReferenceDocIDSr AS Line_ReferenceDocIDSr, 
                L.Barcode AS Line_Barcode, L.LinkedSubcode AS Line_LinkedSubcode, L.HSN AS Line_HSN,
                Lc.Gross_Amount As Line_Gross_Amount, Lc.Taxable_Amount As Line_Taxable_Amount, Lc.Tax1_Per As Line_Tax1_Per, 
                Lc.Tax1 As Line_Tax1, Lc.Tax2_Per As Line_Tax2_Per, 
                Lc.Tax2 As Line_Tax2, Lc.Tax3_Per As Line_Tax3_Per, Lc.Tax3 As Line_Tax3, 
                Lc.Tax4_Per As Line_Tax4_Per, Lc.Tax4 As Line_Tax4, Lc.Tax5_Per As Line_Tax5_Per, 
                Lc.Tax5 As Line_Tax5, Lc.SubTotal1 As Line_SubTotal1, 
                Lc.Deduction_Per As Line_Deduction_Per, Lc.Deduction As Line_Deduction, 
                Lc.Other_Charge_Per As Line_Other_Charge_Per, Lc.Other_Charge As Line_Other_Charge, 
                Lc.Round_Off As Line_Round_Off, Lc.Net_Amount As Line_Net_Amount, 
                Lc.SpecialDiscount_Per As Line_SpecialDiscount_Per, Lc.SpecialDiscount As Line_SpecialDiscount
                FROM LedgerHead H 
                LEFT JOIN LedgerHeadCharges Hc On H.DocId = Hc.DocId
                LEFT JOIN Subgroup Sg ON H.Subcode = Sg.Subcode  
                LEFT JOIN LedgerHeadDetail L ON H.DocId = L.DocId
                LEFT JOIN LedgerHeadDetailCharges Lc On L.DocId = Lc.DocId And L.Sr = Lc.Sr
                LEFT JOIN Subgroup Sg1 ON L.Subcode = Sg1.Subcode
                LEFT JOIN SubGroup Sg2 On L.LinkedSubCode = Sg2.SubCode
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                WHERE Vt.NCat In ('" & Ncat.Payment & "','" & Ncat.Receipt & "') "
        DtHeader = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        mQry = " Select * From LedgerHead "
        DtDestination = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim bDocIdStr As String = ""
        For I = 0 To DtHeader.Rows.Count - 1
            If DtDestination.Select(" OMSId = '" & DtHeader.Rows(I)("DocId") & "' ").Length = 0 Then
                If bDocIdStr <> "" Then bDocIdStr += ","
                bDocIdStr += AgL.XNull(DtHeader.Rows(I)("DocId"))
            End If
        Next

        If bDocIdStr = "" Then Exit Sub

        FSyncParties(bDocIdStr, Conn, Cmd)

        For I = 0 To DtHeader.Rows.Count - 1
            If DtDestination.Select(" OMSId = '" & DtHeader.Rows(I)("DocId") & "' ").Length = 0 Then
                Dim VoucherEntryTableList(0) As FrmVoucherEntry.StructLedgerHead
                Dim VoucherEntryTable As New FrmVoucherEntry.StructLedgerHead

                VoucherEntryTable.DocID = ""
                VoucherEntryTable.V_Type = AgL.XNull(DtHeader.Rows(I)("V_Type"))
                VoucherEntryTable.V_Prefix = AgL.XNull(DtHeader.Rows(I)("V_Prefix"))
                VoucherEntryTable.Site_Code = AgL.XNull(DtHeader.Rows(I)("Site_Code"))
                VoucherEntryTable.Div_Code = AgL.XNull(DtHeader.Rows(I)("Div_Code"))
                VoucherEntryTable.V_No = 0
                VoucherEntryTable.V_Date = AgL.XNull(DtHeader.Rows(I)("V_Date"))
                VoucherEntryTable.ManualRefNo = AgL.XNull(DtHeader.Rows(I)("ManualRefNo"))
                VoucherEntryTable.Subcode = ""
                VoucherEntryTable.SubcodeName = AgL.XNull(DtHeader.Rows(I)("HeaderSubCodeName"))
                VoucherEntryTable.DrCr = AgL.XNull(DtHeader.Rows(I)("DrCr"))
                VoucherEntryTable.SalesTaxGroupParty = AgL.XNull(DtHeader.Rows(I)("SalesTaxGroupParty"))
                VoucherEntryTable.PlaceOfSupply = AgL.XNull(DtHeader.Rows(I)("PlaceOfSupply"))
                VoucherEntryTable.StructureCode = AgL.XNull(DtHeader.Rows(I)("Structure"))
                VoucherEntryTable.CustomFields = AgL.XNull(DtHeader.Rows(I)("CustomFields"))
                VoucherEntryTable.Remarks = AgL.XNull(DtHeader.Rows(I)("Remarks"))
                VoucherEntryTable.Status = AgL.XNull(DtHeader.Rows(I)("Status"))
                VoucherEntryTable.EntryBy = AgL.XNull(DtHeader.Rows(I)("EntryBy"))
                VoucherEntryTable.EntryDate = AgL.XNull(DtHeader.Rows(I)("EntryDate"))
                VoucherEntryTable.ApproveBy = AgL.XNull(DtHeader.Rows(I)("ApproveBy"))
                VoucherEntryTable.ApproveDate = AgL.XNull(DtHeader.Rows(I)("ApproveDate"))
                VoucherEntryTable.MoveToLog = AgL.XNull(DtHeader.Rows(I)("MoveToLog"))
                VoucherEntryTable.MoveToLogDate = AgL.XNull(DtHeader.Rows(I)("MoveToLogDate"))
                VoucherEntryTable.UploadDate = AgL.XNull(DtHeader.Rows(I)("UploadDate"))
                VoucherEntryTable.Gross_Amount = AgL.VNull(DtHeader.Rows(I)("Gross_Amount"))
                VoucherEntryTable.Taxable_Amount = AgL.VNull(DtHeader.Rows(I)("Taxable_Amount"))
                VoucherEntryTable.Tax1 = AgL.VNull(DtHeader.Rows(I)("Tax1"))
                VoucherEntryTable.Tax2 = AgL.VNull(DtHeader.Rows(I)("Tax2"))
                VoucherEntryTable.Tax3 = AgL.VNull(DtHeader.Rows(I)("Tax3"))
                VoucherEntryTable.Tax4 = AgL.VNull(DtHeader.Rows(I)("Tax4"))
                VoucherEntryTable.Tax5 = AgL.VNull(DtHeader.Rows(I)("Tax5"))
                VoucherEntryTable.SubTotal1 = AgL.VNull(DtHeader.Rows(I)("SubTotal1"))
                VoucherEntryTable.Deduction_Per = AgL.VNull(DtHeader.Rows(I)("Deduction_Per"))
                VoucherEntryTable.Deduction = AgL.VNull(DtHeader.Rows(I)("Deduction"))
                VoucherEntryTable.Other_Charge_Per = AgL.VNull(DtHeader.Rows(I)("Other_Charge_Per"))
                VoucherEntryTable.Other_Charge = AgL.VNull(DtHeader.Rows(I)("Other_Charge"))
                VoucherEntryTable.Round_Off = AgL.VNull(DtHeader.Rows(I)("Round_Off"))
                VoucherEntryTable.Net_Amount = AgL.VNull(DtHeader.Rows(I)("Net_Amount"))
                VoucherEntryTable.LockText = "Synced From Pakka"
                VoucherEntryTable.OMSId = AgL.XNull(DtHeader.Rows(I)("DocId"))



                VoucherEntryTable.Line_Sr = AgL.XNull(DtHeader.Rows(I)("Line_Sr"))
                VoucherEntryTable.Line_SubCode = ""
                VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtHeader.Rows(I)("Line_SubCodeName"))
                VoucherEntryTable.Line_LinkedSubCode = ""
                VoucherEntryTable.Line_LinkedSubCodeName = AgL.XNull(DtHeader.Rows(I)("Line_LinkedSubCodeName"))
                VoucherEntryTable.Line_Specification = AgL.XNull(DtHeader.Rows(I)("Line_Specification"))
                VoucherEntryTable.Line_SalesTaxGroupItem = AgL.XNull(DtHeader.Rows(I)("Line_SalesTaxGroupItem"))
                VoucherEntryTable.Line_Qty = AgL.VNull(DtHeader.Rows(I)("Line_Qty"))
                VoucherEntryTable.Line_Unit = AgL.XNull(DtHeader.Rows(I)("Line_Unit"))
                VoucherEntryTable.Line_Rate = AgL.VNull(DtHeader.Rows(I)("Line_Rate"))
                VoucherEntryTable.Line_Amount = AgL.VNull(DtHeader.Rows(I)("Line_Amount"))
                VoucherEntryTable.Line_ChqRefNo = AgL.XNull(DtHeader.Rows(I)("Line_ChqRefNo"))
                VoucherEntryTable.Line_ChqRefDate = AgL.XNull(DtHeader.Rows(I)("Line_ChqRefDate"))
                VoucherEntryTable.Line_Remarks = AgL.XNull(DtHeader.Rows(I)("Line_Remarks"))
                VoucherEntryTable.Line_Gross_Amount = AgL.VNull(DtHeader.Rows(I)("Line_Gross_Amount"))
                VoucherEntryTable.Line_Taxable_Amount = AgL.VNull(DtHeader.Rows(I)("Line_Taxable_Amount"))
                VoucherEntryTable.Line_Tax1_Per = AgL.VNull(DtHeader.Rows(I)("Line_Tax1_Per"))
                VoucherEntryTable.Line_Tax1 = AgL.VNull(DtHeader.Rows(I)("Line_Tax1"))
                VoucherEntryTable.Line_Tax2_Per = AgL.VNull(DtHeader.Rows(I)("Line_Tax2_Per"))
                VoucherEntryTable.Line_Tax2 = AgL.VNull(DtHeader.Rows(I)("Line_Tax2"))
                VoucherEntryTable.Line_Tax3_Per = AgL.VNull(DtHeader.Rows(I)("Line_Tax3_Per"))
                VoucherEntryTable.Line_Tax3 = AgL.VNull(DtHeader.Rows(I)("Line_Tax3"))
                VoucherEntryTable.Line_Tax4_Per = AgL.VNull(DtHeader.Rows(I)("Line_Tax4_Per"))
                VoucherEntryTable.Line_Tax4 = AgL.VNull(DtHeader.Rows(I)("Line_Tax4"))
                VoucherEntryTable.Line_Tax5_Per = AgL.VNull(DtHeader.Rows(I)("Line_Tax5_Per"))
                VoucherEntryTable.Line_Tax5 = AgL.VNull(DtHeader.Rows(I)("Line_Tax5"))
                VoucherEntryTable.Line_SubTotal1 = AgL.VNull(DtHeader.Rows(I)("Line_SubTotal1"))

                VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)

                Dim bDocId As String = FrmVoucherEntry.InsertLedgerHead(VoucherEntryTableList)
            End If
        Next
    End Sub
    Private Sub FSyncParties(DocIdStr As String, Conn As Object, Cmd As Object)
        Dim mPartyQry As String = " Select VReg.SalesTaxNo, VReg.PanNo, VReg.AadharNo,  
                C.CityName, S.Description As StateName, Ag.GroupName, Sg.*
                From Ledger H 
                LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                LEFT JOIN AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN City C ON Sg.CityCode = C.CityCode 
                LEFT JOIN State S ON C.State = S.Code
                LEFT JOIN (
	                SELECT Sgr.Subcode, 
	                Max(CASE WHEN Sgr.RegistrationType =  'Sales Tax No' THEN Sgr.RegistrationNo ELSE NULL END) AS SalesTaxNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'PAN No' THEN Sgr.RegistrationNo ELSE NULL END) AS PanNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'AADHAR NO' THEN Sgr.RegistrationNo ELSE NULL END) AS AadharNo
	                FROM SubgroupRegistration Sgr 
	                GROUP BY Sgr.Subcode         
                ) AS VReg ON H.SubCode = VReg.SubCode
                Where H.DocId In ('" & DocIdStr.Replace(",", "','") & "')"
        Dim DtPartySource As DataTable = AgL.FillData(mPartyQry, Connection_Pakka).Tables(0)
        Dim DtLinkedPartySource As DataTable = AgL.FillData(mPartyQry.Replace("H.Vendor", "H.LinkedSubcode"), Connection_Pakka).Tables(0)

        FSyncSubGroup(DtPartySource, Conn, Cmd)
    End Sub
    Public Sub FSyncSubGroup(DtPartySource As DataTable, Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        Dim bLastAcGroupCode As Integer = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(GroupCode AS INTEGER)),0) FROM AcGroup WHERE ABS(GroupCode)>0", AgL.GcnRead).ExecuteScalar)
        Dim DtAccountGroup = DtPartySource.DefaultView.ToTable(True, "GroupName")
        For I = 0 To DtAccountGroup.Rows.Count - 1
            Dim AcGroupTable As New FrmPerson.StructAcGroup
            Dim bAcGroupCode As String = (bLastAcGroupCode + (I + 1)).ToString.PadLeft(4).Replace(" ", "0")

            AcGroupTable.GroupCode = bAcGroupCode
            AcGroupTable.SNo = ""
            AcGroupTable.GroupName = AgL.XNull(DtAccountGroup.Rows(I)("GroupName"))
            AcGroupTable.ContraGroupName = AgL.XNull(DtAccountGroup.Rows(I)("GroupName"))
            AcGroupTable.GroupUnder = ""
            AcGroupTable.GroupNature = ""
            AcGroupTable.Nature = ""
            AcGroupTable.SysGroup = ""
            AcGroupTable.U_Name = AgL.PubUserName
            AcGroupTable.U_EntDt = AgL.GetDateTime(AgL.GcnRead)
            AcGroupTable.U_AE = "A"

            FrmPerson.ImportAcGroupTable(AcGroupTable)
        Next

        Dim bLastSubCode As String = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtPartySource.Rows.Count - 1
            Dim SubGroupTable As New FrmPerson.StructSubGroupTable
            Dim bSubCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastSubCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(8, "0")

            SubGroupTable.SubCode = bSubCode
            SubGroupTable.Site_Code = AgL.PubSiteCode
            SubGroupTable.Name = AgL.XNull(DtPartySource.Rows(I)("Name"))
            SubGroupTable.DispName = AgL.XNull(DtPartySource.Rows(I)("DispName"))
            SubGroupTable.ManualCode = AgL.XNull(DtPartySource.Rows(I)("ManualCode"))
            SubGroupTable.AccountGroup = AgL.XNull(DtPartySource.Rows(I)("GroupName"))
            SubGroupTable.StateName = AgL.XNull(DtPartySource.Rows(I)("StateName"))
            SubGroupTable.AgentName = ""
            SubGroupTable.TransporterName = ""
            SubGroupTable.AreaName = ""
            SubGroupTable.CityName = AgL.XNull(DtPartySource.Rows(I)("CityName"))
            SubGroupTable.GroupCode = AgL.XNull(DtPartySource.Rows(I)("GroupCode"))
            SubGroupTable.GroupNature = AgL.XNull(DtPartySource.Rows(I)("GroupNature"))
            SubGroupTable.Nature = AgL.XNull(DtPartySource.Rows(I)("Nature"))
            SubGroupTable.Address = AgL.XNull(DtPartySource.Rows(I)("Address"))
            SubGroupTable.CityCode = AgL.XNull(DtPartySource.Rows(I)("CityCode"))
            SubGroupTable.PIN = AgL.XNull(DtPartySource.Rows(I)("PIN"))
            SubGroupTable.Phone = AgL.XNull(DtPartySource.Rows(I)("Phone"))
            SubGroupTable.ContactPerson = AgL.XNull(DtPartySource.Rows(I)("ContactPerson"))
            SubGroupTable.SubgroupType = AgL.XNull(DtPartySource.Rows(I)("SubgroupType"))
            SubGroupTable.Mobile = AgL.XNull(DtPartySource.Rows(I)("Mobile"))
            SubGroupTable.CreditDays = AgL.XNull(DtPartySource.Rows(I)("CreditDays"))
            SubGroupTable.CreditLimit = AgL.XNull(DtPartySource.Rows(I)("CreditLimit"))
            SubGroupTable.EMail = AgL.XNull(DtPartySource.Rows(I)("EMail"))
            SubGroupTable.ParentCode = AgL.XNull(DtPartySource.Rows(I)("Parent"))
            SubGroupTable.SalesTaxPostingGroup = AgL.XNull(DtPartySource.Rows(I)("SalesTaxPostingGroup"))
            SubGroupTable.EntryBy = AgL.PubUserName
            SubGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            SubGroupTable.EntryType = "Add"
            SubGroupTable.EntryStatus = LogStatus.LogOpen
            SubGroupTable.Div_Code = AgL.PubDivCode
            SubGroupTable.Status = "Active"
            SubGroupTable.SalesTaxNo = AgL.XNull(DtPartySource.Rows(I)("SalesTaxNo"))
            SubGroupTable.PANNo = AgL.XNull(DtPartySource.Rows(I)("PANNo"))
            SubGroupTable.AadharNo = AgL.XNull(DtPartySource.Rows(I)("AadharNo"))
            SubGroupTable.Cnt = I
            FrmPerson.ImportSubgroupTable(SubGroupTable)
        Next
    End Sub
    Private Function FGetUpdateClause(DtPakka As DataTable, RowIndexPakka As Integer,
                                      DtKachha As DataTable, RowIndexKaccha As Integer,
                                      FieldName As String, Optional DataType As String = "")
        If AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName)) <> AgL.XNull(DtKachha.Rows(RowIndexKaccha)(FieldName)) Then
            If DataType = "Date" Then
                FGetUpdateClause = FieldName + " = " & AgL.Chk_Date(AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName))) & ""
            ElseIf DataType = "Number" Then
                FGetUpdateClause = FieldName + " = " & AgL.VNull(DtPakka.Rows(RowIndexPakka)(FieldName)) & ""
            Else
                FGetUpdateClause = FieldName + " = " & AgL.Chk_Text(AgL.XNull(DtPakka.Rows(RowIndexPakka)(FieldName))) & ""
            End If
        Else
            FGetUpdateClause = ""
        End If
    End Function
    Private Sub FSyncUpdatedSaleInvoice()
        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        Connection_Pakka.Open()

        mQry = " Select * From SaleInvoice Where UploadDate Is Null "
        Dim DtPakka As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Connection_Pakka.Close()

        Dim bSourceDocIdStr As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            If bSourceDocIdStr <> "" Then bSourceDocIdStr += ","
            bSourceDocIdStr += AgL.Chk_Text(AgL.XNull(DtPakka.Rows(0)("DocId")))
        Next

        mQry = " Select * From SaleInvoice Where OMSId In (" & bSourceDocIdStr & ") "
        Dim DtKachha As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            For J As Integer = 0 To DtKachha.Rows.Count - 1
                If AgL.XNull(DtPakka.Rows(I)("DocId")) = AgL.XNull(DtKachha.Rows(J)("OMSId")) Then
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "V_Date", "Date") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ManualRefNo") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyName") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyAddress") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyPinCode") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyMobile") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartySalesTaxNo") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ShipToAddress") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SalesTaxGroupParty") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PlaceOfSupply") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyDocNo") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyDocDate") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Remarks") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "TermsAndConditions") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Gross_Amount", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Taxable_Amount", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax1_Per", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax1", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax2_Per", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax2", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax3_Per", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax3", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax4_Per", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax4", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax5_Per", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Tax5", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SubTotal1", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Deduction_Per", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Deduction", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Other_Charge_Per", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Other_Charge", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Round_Off", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Net_Amount", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PaidAmt", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "CreditLimit", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "CreditDays", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Status") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryBy") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryDate") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyAadharNo") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SaleToPartyPanNo") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "DeliveryDate") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ReferenceNo") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SpecialDiscount_Per", "Number") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SpecialDiscount", "Number") + ","


                End If

                mQry = " UPDATE SaleInvoice Set " + bUpdateClauseQry + " Where DocId = '" & AgL.XNull(DtKachha.Rows(J)("DocId")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next
        Next
    End Sub
    Private Sub FSyncUpdateSubGroup()
        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        Connection_Pakka.Open()

        mQry = " Select * From SubGroup Where UploadDate Is Null "
        Dim DtPakka As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Connection_Pakka.Close()

        Dim bSourceDocIdStr As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            If bSourceDocIdStr <> "" Then bSourceDocIdStr += ","
            bSourceDocIdStr += AgL.Chk_Text(AgL.XNull(DtPakka.Rows(0)("SubCode")))
        Next

        mQry = " Select * From SubGroup Where OMSId In (" & bSourceDocIdStr & ") "
        Dim DtKachha As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim bUpdateClauseQry As String = ""
        For I As Integer = 0 To DtPakka.Rows.Count - 1
            For J As Integer = 0 To DtKachha.Rows.Count - 1
                If AgL.XNull(DtPakka.Rows(I)("SubCode")) = AgL.XNull(DtKachha.Rows(J)("OMSId")) Then
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SubgroupType") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ManualCode") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "NamePrefix") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Name") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "DispName") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "GroupCode") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "GroupNature") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Nature") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Address") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PIN") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Phone") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Mobile") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Email") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ContactPerson") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "CreditLimit") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "CreditDays") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "SalesTaxPostingGroup") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Div_Code") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Site_Code") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "PostingGroupSalesTaxItem") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "HSN") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Status") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryBy") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryDate") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "EntryType") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "Remarks") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ShowAccountInOtherDivisions") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "WeekOffDays") + ","
                    bUpdateClauseQry += FGetUpdateClause(DtPakka, I, DtKachha, J, "ShowAccountInOtherSites") + ","
                End If

                mQry = " UPDATE SubGroup  Set " + bUpdateClauseQry + " Where SubCode = '" & AgL.XNull(DtKachha.Rows(J)("SubCode")) & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next
        Next
    End Sub
End Class
