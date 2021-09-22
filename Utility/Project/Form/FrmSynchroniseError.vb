Public Class FrmSynchroniseError

    Private Sub FrmSynchroniseError_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DtTemp As DataTable
        Dim mQry$


        mQry = "Select L.*, E.Message From Synchronise_Error E Left Join Log_TableRecords L On E.RowId = L.RowId"
        If MsgBox("Want to See Offline Not Synchronised Data", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DtTemp = AgL.FillData(mQry, AgL.GcnSite).Tables(0)
        Else
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        End If
        Dgl1.DataSource = DtTemp
        AgCL.AgSetDataGridAutoWidths(Dgl1, 100, False)
    End Sub
End Class