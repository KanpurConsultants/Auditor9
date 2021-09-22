Imports System.IO
Imports Customised.ClsMain

Public Class FrmImportOpening
    Private Sub BtnImport_Click(sender As Object, e As EventArgs) Handles BtnImport.Click

    End Sub
    Public Sub FImportFromExcel(bImportFor As ImportFor)
        Dim mQry As String = ""
        Dim bHeadSubCodeName As String = ""
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtLedger As DataTable
        Dim DtLedger_DataFields As DataTable
        Dim DtPurchInvoice As DataTable = Nothing
        Dim DtPurchInvoice_DataFields As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Contra Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Narration") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Dr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Cr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        DtLedger_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        DtPurchInvoice_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As Object
        If bImportFor = ImportFor.Dos Then
            ObjFrmImport = New FrmImportPurchaseFromExcel
            ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
            ObjFrmImport.Dgl2.DataSource = DtPurchInvoice_DataFields
        Else
            ObjFrmImport = New FrmImportFromExcel
            ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
        End If

        ObjFrmImport.Text = "Voucher Entry Import"
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        If bImportFor = ImportFor.Dos Then
            DtLedger = ObjFrmImport.P_DsExcelData_PurchInvoice.Tables(0)
            DtPurchInvoice = ObjFrmImport.P_DsExcelData_PurchInvoiceDetail.Tables(0)
        Else
            DtLedger = ObjFrmImport.P_DsExcelData.Tables(0)
        End If

        mFlag_Import = True

        Dim DtLedger_Original As DataTable = DtLedger
        If bImportFor = ImportFor.Dos Then
            ''''''''''''''For Filtering Data To Import In This Entry'''''''''''''''''''''''''''''''''''
            Dim DtLedger_Filtered As New DataTable
            DtLedger_Filtered = DtLedger.Clone
            Dim DtLedgerRows_Filtered As DataRow() = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('ZD','ZC','ZH','PR','MP','ZR','JV','OB') 
                        And Trim([" & GetFieldAliasName(bImportFor, "Narration") & "]) <> 'DISCOUNT' ")
            For I = 0 To DtLedgerRows_Filtered.Length - 1
                DtLedger_Filtered.ImportRow(DtLedgerRows_Filtered(I))
            Next
            DtLedger = DtLedger_Filtered
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            DtLedger.Columns.Add("File_V_Type")
            For I = 0 To DtLedger.Rows.Count - 1
                DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "File_V_Type")) = DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim
                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZR" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "PMT"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZD" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "ZC" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CNC"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "PR" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "VR"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "MP" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "EV"
                End If

                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "ZH" Then
                    If AgL.VNull(DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                        DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "DNS"
                    Else
                        DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "CNS"
                    End If
                End If

                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")).ToString().Trim() = "CASH A/C." Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")) = "CASH A/C"
                End If

                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name")).ToString().Trim() = "CASH A/C." Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name")) = "CASH A/C"
                End If
            Next
        End If





        Dim DtV_Date = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Date"))
        For I = 0 To DtV_Date.Rows.Count - 1
            If AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) <> "" Then
                If CDate(AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))).Year < "2010" Then
                    If ErrorLog.Contains("These Dates are not valid") = False Then
                        ErrorLog += vbCrLf & "These Dates are not valid" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtV_Type = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"))
        For I = 0 To DtV_Type.Rows.Count - 1
            If AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From Voucher_TYpe where V_Type = '" & AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Voucher Types Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Voucher Types Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtLedgerAccount = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Ledger Account Name"))
        For I = 0 To DtLedgerAccount.Rows.Count - 1
            If AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where LTRIM(RTRIM(Name)) = " & AgL.Chk_Text(AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim()) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Ledger Accounts Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Ledger Accounts Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtContraLedgerAccount = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))
        For I = 0 To DtContraLedgerAccount.Rows.Count - 1
            If AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where LTRIM(RTRIM(Name)) = " & AgL.Chk_Text(AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString().Trim) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Ledger Accounts Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Ledger Accounts Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtLedger_DataFields.Rows.Count - 1
            If AgL.XNull(DtLedger_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtLedger.Columns.Contains(AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If
            End If
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"



            Dim DtLedgerHeader = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"),
                                                                  GetFieldAliasName(bImportFor, "V_No"),
                                                                  GetFieldAliasName(bImportFor, "V_Date"))

            For I = 0 To DtLedgerHeader.Rows.Count - 1
                bHeadSubCodeName = ""
                Dim VoucherEntryTableList(0) As FrmVoucherEntry.StructLedgerHead
                Dim VoucherEntryTable As New FrmVoucherEntry.StructLedgerHead


                VoucherEntryTable.DocID = ""
                VoucherEntryTable.V_Type = AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")))
                VoucherEntryTable.V_Prefix = ""
                VoucherEntryTable.V_Date = AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))
                VoucherEntryTable.V_No = AgL.VNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                VoucherEntryTable.Div_Code = AgL.PubDivCode
                VoucherEntryTable.Site_Code = AgL.PubSiteCode
                VoucherEntryTable.ManualRefNo = AgL.VNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                VoucherEntryTable.Subcode = ""
                VoucherEntryTable.SubcodeName = ""


                If VoucherEntryTable.V_Type = "DNS" Or VoucherEntryTable.V_Type = "DNC" Then
                    VoucherEntryTable.DrCr = "Dr"
                ElseIf VoucherEntryTable.V_Type = "CNS" Or VoucherEntryTable.V_Type = "CNC" Then
                    VoucherEntryTable.DrCr = "Cr"
                End If

                If VoucherEntryTable.V_Type = "JV" Or VoucherEntryTable.V_Type = "OB" Then
                    mFlag_Import = False
                Else
                    mFlag_Import = True
                End If



                VoucherEntryTable.UptoDate = ""
                VoucherEntryTable.Remarks = ""
                VoucherEntryTable.Status = "Active"
                VoucherEntryTable.SalesTaxGroupParty = ""
                VoucherEntryTable.PlaceOfSupply = ""
                VoucherEntryTable.PartySalesTaxNo = ""
                VoucherEntryTable.StructureCode = ""
                VoucherEntryTable.CustomFields = ""
                VoucherEntryTable.PartyDocNo = ""
                VoucherEntryTable.PartyDocDate = ""
                VoucherEntryTable.EntryBy = AgL.PubUserName
                VoucherEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                VoucherEntryTable.ApproveBy = ""
                VoucherEntryTable.ApproveDate = ""
                VoucherEntryTable.MoveToLog = ""
                VoucherEntryTable.MoveToLogDate = ""
                VoucherEntryTable.UploadDate = ""

                VoucherEntryTable.Gross_Amount = 0
                VoucherEntryTable.Taxable_Amount = 0
                VoucherEntryTable.Tax1_Per = 0
                VoucherEntryTable.Tax1 = 0
                VoucherEntryTable.Tax2_Per = 0
                VoucherEntryTable.Tax2 = 0
                VoucherEntryTable.Tax3_Per = 0
                VoucherEntryTable.Tax3 = 0
                VoucherEntryTable.Tax4_Per = 0
                VoucherEntryTable.Tax4 = 0
                VoucherEntryTable.Tax5_Per = 0
                VoucherEntryTable.Tax5 = 0
                VoucherEntryTable.SubTotal1 = 0
                VoucherEntryTable.Deduction_Per = 0
                VoucherEntryTable.Deduction = 0
                VoucherEntryTable.Other_Charge_Per = 0
                VoucherEntryTable.Other_Charge = 0
                VoucherEntryTable.Round_Off = 0
                VoucherEntryTable.Net_Amount = 0

                Dim DtLedger_ForHeader As New DataTable
                For M = 0 To DtLedger.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtLedger.Columns(M).ColumnName
                    DtLedger_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowLedger_ForHeader As DataRow() = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)("V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "V_No") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))) + " And [" & GetFieldAliasName(bImportFor, "V_Date") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))))
                If DtRowLedger_ForHeader.Length > 0 Then
                    For M = 0 To DtRowLedger_ForHeader.Length - 1
                        DtLedger_ForHeader.Rows.Add()
                        For N = 0 To DtLedger_ForHeader.Columns.Count - 1
                            DtLedger_ForHeader.Rows(M)(N) = DtRowLedger_ForHeader(M)(N)
                        Next
                    Next
                End If

                For J = 0 To DtLedger_ForHeader.Rows.Count - 1
                    If Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("CGST") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("SGST") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("IGST") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("BANK") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("DEDUCTION") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString.Trim.Contains("DEDUCTION") And
                            Not AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim.Contains("ROUND") Then

                        VoucherEntryTable.Line_Sr = J + 1
                        VoucherEntryTable.Line_SubCode = ""
                        VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                        VoucherEntryTable.Line_SpecificationDocID = ""
                        VoucherEntryTable.Line_SpecificationDocIDSr = ""
                        VoucherEntryTable.Line_Specification = ""
                        VoucherEntryTable.Line_SalesTaxGroupItem = ""
                        VoucherEntryTable.Line_Qty = 0
                        VoucherEntryTable.Line_Unit = ""
                        VoucherEntryTable.Line_Rate = 0

                        If VoucherEntryTable.V_Type = "JV" Or VoucherEntryTable.V_Type = "OB" Then
                            VoucherEntryTable.Line_Amount = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr")))
                            VoucherEntryTable.Line_Amount_Cr = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr")))
                        Else
                            If AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                VoucherEntryTable.Line_Amount = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr")))
                            ElseIf AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                VoucherEntryTable.Line_Amount = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr")))
                            End If
                        End If


                        VoucherEntryTable.Line_ChqRefNo = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq No"))).ToString.Trim
                        VoucherEntryTable.Line_ChqRefDate = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq Date"))).ToString.Trim
                        VoucherEntryTable.Line_Remarks = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Narration")))
                        VoucherEntryTable.Line_Gross_Amount = 0
                        VoucherEntryTable.Line_Taxable_Amount = 0
                        VoucherEntryTable.Line_Tax1_Per = 0
                        VoucherEntryTable.Line_Tax1 = 0
                        VoucherEntryTable.Line_Tax2_Per = 0
                        VoucherEntryTable.Line_Tax2 = 0
                        VoucherEntryTable.Line_Tax3_Per = 0
                        VoucherEntryTable.Line_Tax3 = 0
                        VoucherEntryTable.Line_Tax4_Per = 0
                        VoucherEntryTable.Line_Tax4 = 0
                        VoucherEntryTable.Line_Tax5_Per = 0
                        VoucherEntryTable.Line_Tax5 = 0
                        VoucherEntryTable.Line_SubTotal1 = 0
                        VoucherEntryTable.Line_Deduction_Per = 0
                        VoucherEntryTable.Line_Deduction = 0

                        If bImportFor = ImportFor.Dos Then
                            Dim DtRowDiscount As DataRow() = Nothing
                            DtRowDiscount = DtLedger_Original.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)("File_V_Type"))) + " And [V_no] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))) +
                                                            " And Trim([" & GetFieldAliasName(bImportFor, "Narration") & "]) = 'DISCOUNT'")
                            If DtRowDiscount IsNot Nothing Then
                                If DtRowDiscount.Length > 0 Then
                                    If AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                        VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Cr")))
                                    ElseIf AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                        VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Dr")))
                                    End If
                                Else
                                    DtRowDiscount = DtLedger_Original.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)("File_V_Type"))) + " And [V_no] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))) +
                                                            " And Trim([" & GetFieldAliasName(bImportFor, "Contra Ledger Account Name") & "]) = 'PURCHASE DEDUCTION'")
                                    If DtRowDiscount.Length > 0 Then
                                        If AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                            VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Cr")))
                                        ElseIf AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                            VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowDiscount(0)(GetFieldAliasName(bImportFor, "Amt Dr")))
                                        End If
                                    End If
                                End If
                            End If


                            Dim DtRowIGST As DataRow() = Nothing
                            DtRowIGST = DtLedger_Original.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)("File_V_Type"))) + " And [V_no] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))) +
                                                            " And Trim([" & GetFieldAliasName(bImportFor, "Narration") & "]) = 'DISCOUNT'")
                            If DtRowIGST IsNot Nothing Then
                                If DtRowIGST.Length > 0 Then
                                    If AgL.VNull(DtRowIGST(0)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                        VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowIGST(0)(GetFieldAliasName(bImportFor, "Amt Cr")))
                                    ElseIf AgL.VNull(DtRowIGST(0)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                        VoucherEntryTable.Line_Deduction = AgL.VNull(DtRowIGST(0)(GetFieldAliasName(bImportFor, "Amt Dr")))
                                    End If
                                End If
                            End If
                        End If





                        VoucherEntryTable.Line_Other_Charge_Per = 0
                        VoucherEntryTable.Line_Other_Charge = 0
                        VoucherEntryTable.Line_Round_Off = 0
                        VoucherEntryTable.Line_Net_Amount = 0

                        If bHeadSubCodeName = "" Then
                            If VoucherEntryTable.V_Type = "DNS" Or VoucherEntryTable.V_Type = "DNC" Or VoucherEntryTable.V_Type = "VR" Then
                                If AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                    bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                                End If
                            ElseIf VoucherEntryTable.V_Type = "CNS" Or VoucherEntryTable.V_Type = "CNC" Or VoucherEntryTable.V_Type = "EV" Then
                                If AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr"))) > 0 Then
                                    bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                                End If
                            ElseIf VoucherEntryTable.V_Type = "PMT" Then
                                If AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr"))) > 0 Then
                                    bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString.Trim
                                End If
                            End If
                        End If



                        If DtPurchInvoice IsNot Nothing Then
                            Dim DtRowPurchInvoice_ForHeader As DataRow() = Nothing
                            DtRowPurchInvoice_ForHeader = DtPurchInvoice.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)("File_V_Type"))) + " And [V_no] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))))
                            If DtRowPurchInvoice_ForHeader IsNot Nothing Then
                                If DtRowPurchInvoice_ForHeader.Length > 0 Then VoucherEntryTable.Remarks = DtRowPurchInvoice_ForHeader(0)("fv_no")
                            End If
                        End If

                        VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                        ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
                    End If
                Next


                For J = 0 To VoucherEntryTableList.Length - 1
                    If bHeadSubCodeName <> "" Then
                        VoucherEntryTableList(J).SubcodeName = bHeadSubCodeName
                    End If
                Next
                FrmVoucherEntry.InsertLedgerHead(VoucherEntryTableList)
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

            mFlag_Import = False
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
            mFlag_Import = False
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub
    Private Function GetFieldAliasName(bImportFor As ImportFor, bFieldName As String)
        Dim bAliasName As String = bFieldName
        If bImportFor = ImportFor.Dos Then
            Select Case bFieldName
                Case "V_TYPE"
                    bAliasName = "V_TYPE"
                Case "V_NO"
                    bAliasName = "V_NO"
                Case "V_Date"
                    bAliasName = "V_DATE"
                Case "Ledger Account Name"
                    bAliasName = "ledgername"
                Case "Contra Ledger Account Name"
                    bAliasName = "contraname"
                Case "Narration"
                    bAliasName = "narration"
                Case "Chq No"
                    bAliasName = "chq_no"
                Case "Chq Date"
                    bAliasName = "chq_date"
                Case "Amt Dr"
                    bAliasName = "amt_dr"
                Case "Amt Cr"
                    bAliasName = "amt_cr"




                Case "Party Name"
                    bAliasName = "vendor"
                Case "Line Ledger Account Name"
                    bAliasName = "item_name"
                Case "Entry No"
                    bAliasName = "V_No"
                Case "SubTotal1"
                    bAliasName = "SUBTOTAL1"
                Case "Deduction_Per"
                    bAliasName = "DED_PER"
                Case "Deduction"
                    bAliasName = "DEDUCTION"
                Case "Other_Charge_Per"
                    bAliasName = "OT_CH_PER"
                Case "Other_Charge"
                    bAliasName = "OT_CHARGE"
                Case "Round_Off"
                    bAliasName = "ROUND_OFF"
                Case "Net_Amount"
                    bAliasName = "NET_AMOUNT"
                Case "Gross_Amount"
                    bAliasName = "GROSS_AMT"
                Case "Taxable_Amount"
                    bAliasName = "TAXABLEAMT"
                Case "Tax1_Per"
                    bAliasName = "TAX1_PER"
                Case "Tax1"
                    bAliasName = "TAX1"
                Case "Tax2_Per"
                    bAliasName = "TAX2_PER"
                Case "Tax2"
                    bAliasName = "TAX2"
                Case "Tax3_Per"
                    bAliasName = "TAX3_PER"
                Case "Tax3"
                    bAliasName = "TAX3"
                Case "Tax4_Per"
                    bAliasName = "TAX4_PER"
                Case "Tax4"
                    bAliasName = "TAX4"
                Case "Tax5_Per"
                    bAliasName = "TAX5_PER"
                Case "Tax5"
                    bAliasName = "TAX5"
            End Select

            Return bAliasName
        Else
            Return bFieldName
        End If
    End Function
End Class