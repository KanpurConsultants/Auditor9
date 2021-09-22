Imports System.Drawing.Printing
Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.Reporting.WinForms

Public Class FrmRepPrint

    Dim dsReport As New DataSet()
    Dim mReportStr As String = ""
    Dim I As Integer = 0
    Dim mReportFontSize As Integer = 8
    Dim mTotalColumnWidth As Double = 0
    Dim A4PortraitSizeWidth As Integer = 850
    Dim A4LandscapeSizeWidth As Integer = 1100
    Dim mReportTitle As String = "", mReportSubTitle As String = ""
    Dim AgL As AgLibrary.ClsMain

    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar

    End Sub

    Public Property ReportTitle() As String
        Get
            ReportTitle = mReportTitle
        End Get
        Set(ByVal value As String)
            mReportTitle = value
        End Set
    End Property
    Public Property ReportSubTitle() As String
        Get
            ReportSubTitle = mReportSubTitle
        End Get
        Set(ByVal value As String)
            mReportSubTitle = value
        End Set
    End Property
    Private Sub FSetPageSetting()
        Me.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        reportViewer1.ZoomMode = ZoomMode.Percent
        reportViewer1.ZoomPercent = 100


        Dim ps As New PageSettings()
        ps.Margins = New Margins(40, 10, 20, 20)
        If mTotalColumnWidth <= A4PortraitSizeWidth Then
            ps.PaperSize = New PaperSize("A4", 850, 1100)
            ps.PaperSize.RawKind = PaperKind.A4
        Else
            ps.Landscape = True
            ps.PaperSize = New PaperSize("A4", 850, 1100)
            ps.PaperSize.RawKind = PaperKind.A4
        End If
        reportViewer1.SetPageSettings(ps)
        reportViewer1.RefreshReport()
    End Sub
    Private Sub FrmReportPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        reportViewer1.RefreshReport()
        Me.WindowState = FormWindowState.Maximized
        FSetPageSetting()
    End Sub
    Private Function FGetColumnWidthMultiplier()
        If mTotalColumnWidth <= A4PortraitSizeWidth Then
            Return 0.016666666666666666 * 0.5
        ElseIf mTotalColumnWidth <= A4LandscapeSizeWidth Then
            Return 0.016666666666666666 * 0.5
        Else
            Return 0.016666666666666666 * 0.45
        End If
    End Function
    Private Function GetDataSetFromDataGridView(ByVal dgv As AgControls.AgDataGrid) As DataSet
        Dim ds As New DataSet
        Try
            ' Add Table
            ds.Tables.Add("Table1")

            ' Add Columns
            Dim col As DataColumn
            For Each dgvCol As DataGridViewColumn In dgv.Columns
                If dgvCol.Visible = True Then
                    col = New DataColumn(dgvCol.Name.Replace(" ", ""))
                    ds.Tables("Table1").Columns.Add(col)
                    col.DataType = dgvCol.ValueType
                End If
            Next

            'Add Rows from the datagridview
            Dim row As DataRow
            For i As Integer = 0 To dgv.Rows.Count - 1
                row = ds.Tables("Table1").Rows.Add
                For Each column As DataGridViewColumn In dgv.Columns
                    If column.Visible = True Then
                        row.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Value
                    End If
                Next
            Next

            Return ds
        Catch ex As Exception
            MsgBox("CRITICAL ERROR : Exception caught while converting dataGridView to DataSet (dgvtods).. " & Chr(10) & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Sub ProcessPrint()
        FSetPageSetting()
        reportViewer1.Visible = True
        Dim id As Integer = 0
        reportViewer1.ProcessingMode = ProcessingMode.Local
        'dsReport = GetDataSetFromDataGridView(mDGL1)

        'CreateRDLFile("GridReport", GetReportStr())
        reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "GridReport.rdlc"

        If (dsReport.Tables(0).Rows.Count > 0) Then
            Dim rds As New ReportDataSource("DsMain", dsReport.Tables(0))
            reportViewer1.LocalReport.DataSources.Clear()
            reportViewer1.LocalReport.DataSources.Add(rds)

            reportViewer1.LocalReport.SetParameters(New ReportParameter("ReportTitle", mReportTitle))



            reportViewer1.LocalReport.Refresh()
            reportViewer1.RefreshReport()


        End If
    End Sub

    Public Sub CreateRDLFile(FileName As String, FileCode As String)
        Dim FileFullPath As String = ""
        FileFullPath = AgL.PubReportPath + FileName + ".rdlc"
        If File.Exists(FileFullPath) Then
            File.Delete(FileFullPath)
        End If

        Dim sw As StreamWriter = File.CreateText(FileFullPath)
        sw.AutoFlush = True
        sw.Write(FileCode)
        sw.Close()
    End Sub





    Private Sub FrmReportPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class