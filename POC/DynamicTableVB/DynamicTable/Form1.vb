Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports Microsoft.Reporting.WinForms

Friend Class Form1

    Private m_dataSet As DataSet
    Private m_rdl As MemoryStream

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        OpenDataFile("test.xml", False)
    End Sub

    Private Sub ShowReport()
        Me.ReportViewer1.Reset()
        Me.ReportViewer1.LocalReport.LoadReportDefinition(m_rdl)
        Me.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("MyData", m_dataSet.Tables(0)))
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Function GenerateRdl(ByVal allFields As List(Of String), ByVal selectedFields As List(Of String)) As MemoryStream
        Dim ms As New MemoryStream()
        Dim gen As New DynamicTable.RdlGenerator()
        gen.AllFields = allFields
        gen.SelectedFields = selectedFields
        gen.WriteXml(ms)
        ms.Position = 0
        Return ms
    End Function

    Private Function GetAvailableFields() As List(Of String)
        Dim dataTable As DataTable = m_dataSet.Tables(0)
        Dim availableFields As New List(Of String)
        Dim i As Integer
        For i = 0 To dataTable.Columns.Count - 1
            availableFields.Add(dataTable.Columns(i).ColumnName)
        Next i
        Return availableFields
    End Function

    Private Sub OpenDataFile(ByVal filename As String, ByVal showOptionsDialog As Boolean)
        Try
            m_dataSet = New DataSet()
            m_dataSet.ReadXml(filename)

            Dim allFields As List(Of String) = GetAvailableFields()
            Dim dlg As New ReportOptionsDialog(allFields)
            If showOptionsDialog Then
                If dlg.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Return
                End If
            End If

            Dim selectedFields As List(Of String) = dlg.GetSelectedFields()

            If Not (m_rdl Is Nothing) Then
                m_rdl.Dispose()
            End If
            m_rdl = GenerateRdl(allFields, selectedFields)

            ShowReport()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OpenToolStripMenuItem_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            OpenDataFile(OpenFileDialog1.FileName, True)
        End If
    End Sub
End Class
