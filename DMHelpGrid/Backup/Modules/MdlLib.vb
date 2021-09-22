Module MdlLib
    Public Const StrMsgTitle As String = "Information Window ...."
    Public Sub IniGrid(ByVal FGObj As System.Windows.Forms.DataGridView)
        FGObj.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(95, 95, 95)  'Color.FromArgb(0, 64, 64)
        FGObj.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(95, 95, 95) 'Color.FromArgb(0, 64, 64)
        FGObj.RowsDefaultCellStyle.SelectionBackColor = Color.Gainsboro 'Color.FromArgb(224, 224, 224)
        FGObj.RowsDefaultCellStyle.SelectionForeColor = Color.Black
        FGObj.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 192)
        FGObj.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 192)
        FGObj.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Regular)
        FGObj.DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Regular)
        FGObj.BorderStyle = BorderStyle.FixedSingle
        FGObj.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        FGObj.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        FGObj.AllowUserToResizeRows = False
        FGObj.AllowUserToDeleteRows = True
        FGObj.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
        FGObj.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
        FGObj.BorderStyle = DataGridViewHeaderBorderStyle.Raised
    End Sub
    Public Function XNull(ByVal temp As Object) As Object
        XNull = CStr(IIf(IsDBNull(temp), "", temp))
    End Function
End Module
