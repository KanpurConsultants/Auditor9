Public Class FrmStockAdjustmentFIFO
    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        ClsMain.FifoAdjustSale()
    End Sub
End Class