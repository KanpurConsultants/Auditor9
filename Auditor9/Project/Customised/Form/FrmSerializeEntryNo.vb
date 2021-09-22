Imports AgLibrary.ClsMain.agConstants

Public Class FrmSerializeEntryNo
    Private Sub FrmSerializeEntryNo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim mQry As String
        mQry = "Select Code,Name From viewHelpSubgroup where SubgroupType In ('Customer','Supplier')"
        TxtParty.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        mQry = "Select Code,Name From viewHelpSubgroup where SubgroupType In ('Master Customer','Master Supplier')"
        txtLinkedParty.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim mQry As String
        Dim mTrans As Boolean
        Dim dtEntryNo As DataTable
        Dim I As Integer
        Try

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            mQry = "Select H.DocId, H.V_No, H.ManualRefNo from SaleInvoice H With (NoLock) Where V_Type = '" & Ncat.SaleInvoice & "' And Div_Code ='" & AgL.PubDivCode & "' and Site_code = '" & AgL.PubSiteCode & "' and V_Date >= " & AgL.Chk_Date(AgL.PubStartDate) & " Order By H.V_Date, H.V_No "
            dtEntryNo = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            For I = 0 To dtEntryNo.Rows.Count - 1
                mQry = "Update SaleInvoice Set ManualRefNo = '" & I + 1 & "' where DocID = '" & dtEntryNo.Rows(I)("DocID") & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "Update Ledger Set RecID = '" & I + 1 & "' where DocID = '" & dtEntryNo.Rows(I)("DocID") & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next

            AgL.ETrans.Commit()
            mTrans = False

            MsgBox("Updated Successfully")
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
End Class