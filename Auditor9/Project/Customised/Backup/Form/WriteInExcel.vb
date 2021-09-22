Public Class WriteInExcel

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        If sfdSaveFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtFileName.Text = sfdSaveFile.FileName
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWrite.Click
        If Not String.IsNullOrEmpty(txtFileName.Text) Then

            Dim ErrorMessage As String = String.Empty
            Dim OExcelHandler As New ExcelHandler()
            btnClose.Enabled = False
            Try
                Dim ds As DataSet = GetGridData()
                If ds IsNot Nothing Then
                    OExcelHandler.ExportToExcel(txtFileName.Text.Trim(), ds, "Write In Excel", ErrorMessage)
                End If
            Catch ex As Exception

            Finally
                btnClose.Enabled = True
                If Not String.IsNullOrEmpty(ErrorMessage) Then
                    MessageBox.Show(ErrorMessage)
                Else
                    MessageBox.Show("Operation Successful!")
                End If
            End Try

        End If
    End Sub

    Private Function GetGridData() As DataSet
        Dim ds As New DataSet
        ds.Tables.Add("Table0")
        ds.Tables(0).Columns.Add("Column1")
        ds.Tables(0).Columns.Add("Column2")
        ds.Tables(0).Columns.Add("Column3")
        ds.Tables(0).Columns.Add("Column4")
        ds.Tables(0).Columns.Add("Column5")

        For i As Integer = 0 To dgvExcelData.Rows.Count - 1
            Dim dtr As DataRow = ds.Tables(0).NewRow()
            dtr("Column1") = dgvExcelData.Item("Column1", i).Value
            dtr("Column2") = dgvExcelData.Item("Column2", i).Value
            dtr("Column3") = dgvExcelData.Item("Column3", i).Value
            dtr("Column4") = dgvExcelData.Item("Column4", i).Value
            dtr("Column5") = dgvExcelData.Item("Column5", i).Value
            ds.Tables(0).Rows.Add(dtr)
        Next
        Return ds
    End Function

    Private Sub WriteInExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

End Class