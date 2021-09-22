Public Class FrmUpdateLinkAccount
    Private Sub FrmUpdateLinkAccount_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim mQry As String
        mQry = "Select Code,Name From viewHelpSubgroup where SubgroupType In ('Customer','Supplier')"
        TxtParty.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        mQry = "Select Code,Name From viewHelpSubgroup where SubgroupType In ('Master Customer','Master Supplier')"
        txtLinkedParty.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim mQry As String
        Dim mTrans As Boolean
        Try

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            mQry = "Update Ledger Set LinkedSubcode =" & AgL.Chk_Text(txtLinkedParty.Tag) & " Where Subcode ='" & TxtParty.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Update LedgerHeadDetail Set LinkedSubcode =" & AgL.Chk_Text(txtLinkedParty.Tag) & " Where Subcode ='" & TxtParty.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Update LedgerHead Set LinkedSubcode =" & AgL.Chk_Text(txtLinkedParty.Tag) & " Where Subcode ='" & TxtParty.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Update PurchInvoice Set BillToParty =" & AgL.Chk_Text(txtLinkedParty.Tag) & " Where Vendor ='" & TxtParty.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Update SaleInvoice Set BillToParty =" & AgL.Chk_Text(txtLinkedParty.Tag) & " Where SaleToParty ='" & TxtParty.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Update wLedgerHeaddetail Set LinkedParty =" & AgL.Chk_Text(txtLinkedParty.Tag) & " Where Party ='" & TxtParty.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Update wpurchInvoiceDetail Set MasterSupplier =" & AgL.Chk_Text(txtLinkedParty.Tag) & " Where Supplier ='" & TxtParty.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Update wsaleInvoiceDetail Set MasterParty =" & AgL.Chk_Text(txtLinkedParty.Tag) & " Where Party ='" & TxtParty.Tag & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = False

            MsgBox("Updated Successfully")
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
End Class