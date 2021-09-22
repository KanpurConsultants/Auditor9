Public Class FrmSplitLedgerOpening

    Dim mQry As String = ""
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim M As Integer = 0
        Dim N As Integer = 0
        Dim mTrans As String = ""
        Dim DtLedgerHeadDetail As DataTable
        Dim DtLedgerHead As DataTable
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = "SELECT Sg.Name As LedgerAccountName, L.* 
                    FROM LedgerHeadDetail L With (NoLock) 
                    LEFT JOIN SubGroup Sg On L.SubCode = Sg.SubCode
                    WHERE L.DocID = '" & TxtEntryNo.Tag & "'"
            DtLedgerHeadDetail = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            mQry = "SELECT L.Subcode
                    FROM LedgerHeadDetail L With (NoLock) 
                    WHERE L.DocID = '" & TxtEntryNo.Tag & "'
                    GROUP BY L.Subcode"
            DtLedgerHead = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            For I = 0 To DtLedgerHead.Rows.Count - 1


                Dim VoucherEntryTableList(0) As FrmVoucherEntry.StructLedgerHead
                Dim VoucherEntryTable As New FrmVoucherEntry.StructLedgerHead

                VoucherEntryTable.DocID = ""
                VoucherEntryTable.V_Type = "OB"
                VoucherEntryTable.V_Prefix = ""
                VoucherEntryTable.V_Date = "31/Mar/2019"
                VoucherEntryTable.V_No = 1
                VoucherEntryTable.Div_Code = AgL.PubDivCode
                VoucherEntryTable.Site_Code = AgL.PubSiteCode
                VoucherEntryTable.ManualRefNo = 1
                VoucherEntryTable.Subcode = ""
                VoucherEntryTable.SubcodeName = ""


                VoucherEntryTable.UptoDate = ""
                VoucherEntryTable.Remarks = "Opening Imported On Software Start."
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

                Dim DtLedgerHeadDetail_ForHeader As New DataTable
                For M = 0 To DtLedgerHeadDetail.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtLedgerHeadDetail.Columns(M).ColumnName
                    DtLedgerHeadDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowLedger_ForHeader As DataRow() = DtLedgerHeadDetail.Select("[SubCode] = " + AgL.Chk_Text(DtLedgerHead.Rows(I)("SubCode")))
                If DtRowLedger_ForHeader.Length > 0 Then
                    For M = 0 To DtRowLedger_ForHeader.Length - 1
                        DtLedgerHeadDetail_ForHeader.Rows.Add()
                        For N = 0 To DtLedgerHeadDetail_ForHeader.Columns.Count - 1
                            DtLedgerHeadDetail_ForHeader.Rows(M)(N) = DtRowLedger_ForHeader(M)(N)
                        Next
                    Next
                End If



                For J = 0 To DtLedgerHeadDetail_ForHeader.Rows.Count - 1
                    VoucherEntryTable.Line_Sr = J + 1
                    VoucherEntryTable.Line_SubCode = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)("SubCode"))
                    VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)("LedgerAccountName"))
                    VoucherEntryTable.Line_SpecificationDocID = ""
                    VoucherEntryTable.Line_SpecificationDocIDSr = ""
                    VoucherEntryTable.Line_Specification = ""
                    VoucherEntryTable.Line_SalesTaxGroupItem = ""
                    VoucherEntryTable.Line_Qty = 0
                    VoucherEntryTable.Line_Unit = ""
                    VoucherEntryTable.Line_Rate = 0
                    VoucherEntryTable.Line_Amount = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)("Amount"))
                    VoucherEntryTable.Line_Amount_Cr = AgL.VNull(DtLedgerHeadDetail_ForHeader.Rows(J)("AmountCr"))
                    VoucherEntryTable.Line_ChqRefNo = ""
                    VoucherEntryTable.Line_ChqRefDate = ""
                    VoucherEntryTable.Line_ReferenceNo = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)("ReferenceNo"))
                    VoucherEntryTable.Line_ReferenceDate = AgL.XNull(DtLedgerHeadDetail_ForHeader.Rows(J)("ReferenceDate"))
                    VoucherEntryTable.Line_Remarks = ""
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
                    VoucherEntryTable.Line_Other_Charge_Per = 0
                    VoucherEntryTable.Line_Other_Charge = 0
                    VoucherEntryTable.Line_Round_Off = 0
                    VoucherEntryTable.Line_Net_Amount = 0

                    VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                    ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
                Next
                FrmVoucherEntry.InsertLedgerHead(VoucherEntryTableList)
            Next

            mQry = "Delete From Ledger Where DocId = '" & TxtEntryNo.Tag & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerHeadDetailCharges Where DocId = '" & TxtEntryNo.Tag & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "Delete From LedgerHeadDetail Where DocId = '" & TxtEntryNo.Tag & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "DELETE FROM LedgerHeadCharges Where DocId = '" & TxtEntryNo.Tag & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = "Delete From LedgerHead Where DocId = '" & TxtEntryNo.Tag & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            AgL.ETrans.Commit()
            mTrans = "Commit"

            MsgBox("Process Completed Successfully...!", MsgBoxStyle.Information)
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtEntryNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtEntryNo.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "SELECT H.DocID, H.ManualRefNo 
                                    FROM LedgerHead H
                                    WHERE V_Type = 'OB'
                                    And H.Div_Code = '" & AgL.PubDivCode & "'
                                    And H.Site_Code = '" & AgL.PubSiteCode & "'"
                            sender.agHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class