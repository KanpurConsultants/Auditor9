Imports System.Threading

Public Class BackgroundWorker(Of TArgument, TProgress, TResult)

    Public Const MinProgress As Int32 = 0
    Public Const MaxProgress As Int32 = 100

    Public Event DoWork As EventHandler(Of DoWorkEventArgs(Of TArgument, TResult))
    Public Event ProgressChanged As EventHandler(Of ProgressChangedEventArgs(Of TProgress))
    Public Event RunWorkerCompleted As EventHandler(Of RunWorkerCompletedEventArgs(Of TResult))

    Private asyncOperation As AsyncOperation = Nothing
    Private ReadOnly threadStart As BasicDelegate
    Private ReadOnly operationCompleted As SendOrPostCallback
    Private ReadOnly progressReporterDelegate As SendOrPostCallback

    Private _CancellationPending As Boolean
    Private _IsBusy As Boolean
    Private _WorkerReportsProgress As Boolean
    Private _WorkerSupportsCancellation As Boolean

    Public Sub New()
        threadStart = AddressOf WorkerThreadStart
        operationCompleted = AddressOf AsyncOperationCompleted
        progressReporterDelegate = AddressOf ProgressReporter
        WorkerReportsProgress = True
        WorkerSupportsCancellation = True
    End Sub

    Public Property CancellationPending() As Boolean
        Get
            Return _CancellationPending
        End Get
        Private Set(ByVal value As Boolean)
            _CancellationPending = value
        End Set
    End Property

    Public Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Private Set(ByVal value As Boolean)
            _IsBusy = value
        End Set
    End Property

    Public Property WorkerReportsProgress() As Boolean
        Get
            Return _WorkerReportsProgress
        End Get
        Set(ByVal value As Boolean)
            _WorkerReportsProgress = value
        End Set
    End Property

    Public Property WorkerSupportsCancellation() As Boolean
        Get
            Return _WorkerSupportsCancellation
        End Get
        Set(ByVal value As Boolean)
            _WorkerSupportsCancellation = value
        End Set
    End Property

    Private Sub AsyncOperationCompleted(ByVal state As Object)
        IsBusy = False
        CancellationPending = False
        OnRunWorkerCompleted(CType(state, RunWorkerCompletedEventArgs(Of TResult)))
    End Sub

    Public Function CancelAsync() As Boolean
        If Not WorkerSupportsCancellation Then
            Return False
        End If
        CancellationPending = True
        Return True
    End Function

    Protected Overridable Sub OnDoWork(ByVal e As DoWorkEventArgs(Of TArgument, TResult))
        RaiseEvent DoWork(Me, e)
    End Sub

    Protected Overridable Sub OnProgressChanged(ByVal e As ProgressChangedEventArgs(Of TProgress))
        RaiseEvent ProgressChanged(Me, e)
    End Sub

    Protected Overridable Sub OnRunWorkerCompleted(ByVal e As RunWorkerCompletedEventArgs(Of TResult))
        RaiseEvent RunWorkerCompleted(Me, e)
    End Sub

    Private Sub ProgressReporter(ByVal state As Object)
        OnProgressChanged(CType(state, ProgressChangedEventArgs(Of TProgress)))
    End Sub

    Public Function ReportProgress(ByVal percentProgress As Int32) As Boolean
        Return ReportProgress(percentProgress, CType(Nothing, TProgress))
    End Function

    Public Function ReportProgress(ByVal percentProgress As Int32, ByVal userState As TProgress) As Boolean
        If Not WorkerReportsProgress Then
            Return False
        End If
        If percentProgress < MinProgress Then
            percentProgress = MinProgress
        ElseIf percentProgress > MaxProgress Then
            percentProgress = MaxProgress
        End If
        Dim args As ProgressChangedEventArgs(Of TProgress) = _
            New ProgressChangedEventArgs(Of TProgress)(percentProgress, userState)
        If Not asyncOperation Is Nothing Then
            asyncOperation.Post(progressReporterDelegate, args)
        Else
            progressReporterDelegate(args)
        End If
        Return True
    End Function

    Public Function RunWorkerAsync() As Boolean
        Return RunWorkerAsync(CType(Nothing, TArgument))
    End Function

    Public Function RunWorkerAsync(ByVal argument As TArgument) As Boolean
        If IsBusy Then
            Return False
        End If
        IsBusy = True
        CancellationPending = False
        asyncOperation = AsyncOperationManager.CreateOperation(argument)
        threadStart.BeginInvoke(Nothing, Nothing)
        Return True
    End Function

    Private Sub WorkerThreadStart()
        Dim workerResult As TResult = CType(Nothing, TResult)
        Dim err As Exception = Nothing
        Dim cancelled As Boolean = False
        Try
            Dim doWorkArgs As DoWorkEventArgs(Of TArgument, TResult) = _
                New DoWorkEventArgs(Of TArgument, TResult)(CType(asyncOperation.UserSuppliedState, TArgument))
            OnDoWork(doWorkArgs)
            If doWorkArgs.Cancel Then
                cancelled = True
            Else
                workerResult = doWorkArgs.Result
            End If
        Catch exception As Exception
            err = exception
        End Try
        Dim e As RunWorkerCompletedEventArgs(Of TResult) = New RunWorkerCompletedEventArgs(Of TResult)(workerResult, err, cancelled)
        asyncOperation.PostOperationCompleted(operationCompleted, e)
    End Sub

End Class

Public Class BackgroundWorker(Of T)

    Public Const MinProgress As Int32 = 0
    Public Const MaxProgress As Int32 = 100

    Public Event DoWork As EventHandler(Of DoWorkEventArgs(Of T))
    Public Event ProgressChanged As EventHandler(Of ProgressChangedEventArgs(Of T))
    Public Event RunWorkerCompleted As EventHandler(Of RunWorkerCompletedEventArgs(Of T))

    Private asyncOperation As AsyncOperation = Nothing
    Private ReadOnly threadStart As BasicDelegate
    Private ReadOnly operationCompleted As SendOrPostCallback
    Private ReadOnly progressReporterDelegate As SendOrPostCallback

    Private _CancellationPending As Boolean
    Private _IsBusy As Boolean
    Private _WorkerReportsProgress As Boolean
    Private _WorkerSupportsCancellation As Boolean

    Public Sub New()
        threadStart = AddressOf WorkerThreadStart
        operationCompleted = AddressOf AsyncOperationCompleted
        progressReporterDelegate = AddressOf ProgressReporter
        WorkerReportsProgress = True
        WorkerSupportsCancellation = True
    End Sub

    Public Property CancellationPending() As Boolean
        Get
            Return _CancellationPending
        End Get
        Private Set(ByVal value As Boolean)
            _CancellationPending = value
        End Set
    End Property

    Public Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Private Set(ByVal value As Boolean)
            _IsBusy = value
        End Set
    End Property

    Public Property WorkerReportsProgress() As Boolean
        Get
            Return _WorkerReportsProgress
        End Get
        Set(ByVal value As Boolean)
            _WorkerReportsProgress = value
        End Set
    End Property

    Public Property WorkerSupportsCancellation() As Boolean
        Get
            Return _WorkerSupportsCancellation
        End Get
        Set(ByVal value As Boolean)
            _WorkerSupportsCancellation = value
        End Set
    End Property

    Private Sub AsyncOperationCompleted(ByVal state As Object)
        IsBusy = False
        CancellationPending = False
        OnRunWorkerCompleted(CType(state, RunWorkerCompletedEventArgs(Of T)))
    End Sub

    Public Function CancelAsync() As Boolean
        If Not WorkerSupportsCancellation Then
            Return False
        End If
        CancellationPending = True
        Return True
    End Function

    Protected Overridable Sub OnDoWork(ByVal e As DoWorkEventArgs(Of T))
        RaiseEvent DoWork(Me, e)
    End Sub

    Protected Overridable Sub OnProgressChanged(ByVal e As ProgressChangedEventArgs(Of T))
        RaiseEvent ProgressChanged(Me, e)
    End Sub

    Protected Overridable Sub OnRunWorkerCompleted(ByVal e As RunWorkerCompletedEventArgs(Of T))
        RaiseEvent RunWorkerCompleted(Me, e)
    End Sub

    Private Sub ProgressReporter(ByVal state As Object)
        OnProgressChanged(CType(state, ProgressChangedEventArgs(Of T)))
    End Sub

    Public Function ReportProgress(ByVal percentProgress As Int32) As Boolean
        Return ReportProgress(percentProgress, CType(Nothing, T))
    End Function

    Public Function ReportProgress(ByVal percentProgress As Int32, ByVal userState As T) As Boolean
        If Not WorkerReportsProgress Then
            Return False
        End If
        If percentProgress < MinProgress Then
            percentProgress = MinProgress
        ElseIf percentProgress > MaxProgress Then
            percentProgress = MaxProgress
        End If
        Dim args As ProgressChangedEventArgs(Of T) = _
            New ProgressChangedEventArgs(Of T)(percentProgress, userState)
        If Not asyncOperation Is Nothing Then
            asyncOperation.Post(progressReporterDelegate, args)
        Else
            progressReporterDelegate(args)
        End If
        Return True
    End Function

    Public Function RunWorkerAsync() As Boolean
        Return RunWorkerAsync(CType(Nothing, T))
    End Function

    Public Function RunWorkerAsync(ByVal argument As T) As Boolean
        If IsBusy Then
            Return False
        End If
        IsBusy = True
        CancellationPending = False
        asyncOperation = AsyncOperationManager.CreateOperation(argument)
        threadStart.BeginInvoke(Nothing, Nothing)
        Return True
    End Function

    Private Sub WorkerThreadStart()
        Dim workerResult As T = CType(Nothing, T)
        Dim err As Exception = Nothing
        Dim cancelled As Boolean = False
        Try
            Dim doWorkArgs As DoWorkEventArgs(Of T) = New DoWorkEventArgs(Of T)(CType(asyncOperation.UserSuppliedState, T))
            OnDoWork(doWorkArgs)
            If doWorkArgs.Cancel Then
                cancelled = True
            Else
                workerResult = doWorkArgs.Result
            End If
        Catch exception As Exception
            err = exception
        End Try
        Dim e As RunWorkerCompletedEventArgs(Of T) = New RunWorkerCompletedEventArgs(Of T)(workerResult, err, cancelled)
        asyncOperation.PostOperationCompleted(operationCompleted, e)
    End Sub

End Class
