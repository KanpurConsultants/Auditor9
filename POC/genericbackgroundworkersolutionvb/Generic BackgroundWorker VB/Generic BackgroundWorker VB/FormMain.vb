Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.Custom.Generic
Imports System.Threading
Imports System.Windows.Forms

Public Class FormMain

    Private fileWorker As BackgroundWorker(Of String(), String, List(Of FileData))
    Private files As String()

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        InitializeFileArray()
        fileWorker = New BackgroundWorker(Of String(), String, List(Of FileData))
        AddHandler fileWorker.DoWork, AddressOf fileWorker_DoWorkHandler
        AddHandler fileWorker.ProgressChanged, AddressOf fileWorker_ProgressChangedHandler
        AddHandler fileWorker.RunWorkerCompleted, AddressOf fileWorker_RunWorkerCompletedHandler
    End Sub

    Public Sub fileWorker_DoWorkHandler(ByVal sender As Object, ByVal e As DoWorkEventArgs(Of String(), List(Of FileData)))
        Dim progress As Int32 = 0
        e.Result = New List(Of FileData)(e.Argument.Length)
        For Each file As String In e.Argument
            If fileWorker.CancellationPending Then
                e.Cancel = True
                Return
            End If
            fileWorker.ReportProgress(progress, file)
            Thread.Sleep(50)
            e.Result.Add(New FileData(file, DateTime.Now))
            progress += 2
        Next
        fileWorker.ReportProgress(progress, String.Empty)
    End Sub

    Public Sub fileWorker_ProgressChangedHandler _
        (ByVal sender As Object, ByVal e As ProgressChangedEventArgs(Of String))
        labelProgress.Text = e.UserState
        progressBar.Value = e.ProgressPercentage
    End Sub

    Public Sub fileWorker_RunWorkerCompletedHandler(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs(Of List(Of FileData)))
        If e.Cancelled Then
            labelProgress.Text = "Cancelled"
            progressBar.Value = 0
        Else
            labelProgress.Text = "Done!"
        End If
        listBox.DataSource = e.Result
        listBox.Enabled = True
        buttonStart.Enabled = True
        buttonCancel.Enabled = False
        progressBar.Enabled = False
        AcceptButton = buttonStart
    End Sub

    Private Sub InitializeFileArray()
        files = New String() { _
            "00", "01", "02", "03", "04", "05", "06", "07", _
            "08", "09", "0A", "0B", "0C", "0D", "0E", "0F", _
            "10", "11", "12", "13", "14", "15", "16", "17", _
            "18", "19", "1A", "1B", "1C", "1D", "1E", "1F", _
            "20", "21", "22", "23", "24", "25", "26", "27", _
            "28", "29", "2A", "2B", "2C", "2D", "2E", "2F", _
            "30", "31"}
    End Sub

    Private Sub buttonStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonStart.Click
        buttonCancel.Enabled = True
        AcceptButton = buttonCancel
        buttonStart.Enabled = False
        listBox.DataSource = Nothing
        listBox.Enabled = False
        fileWorker.RunWorkerAsync(files)
    End Sub

    Private Sub buttonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonCancel.Click
        fileWorker.CancelAsync()
    End Sub

End Class
