Public Class FrmReportMaster
    Dim ObjRFG As AgLibrary.RepFormGlobal
    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim DsRep As DataSet
        DsRep = AgL.FillData("Exec " & TxtProcedure.Text & "", AgL.GCn)
        ObjRFG = New AgLibrary.RepFormGlobal(AgL)
        ObjRFG.PrintReport(DsRep, TxtReportName.Text, TxtReportName.Text, AgL.PubReportPath)
        ObjRFG.Dispose()
    End Sub
End Class