Public NotInheritable Class RunWorkerCompletedEventArgs(Of T)
    Inherits System.EventArgs

    Private _Cancelled As Boolean
    Private _Err As Exception
    Private _Result As T

    Public Sub New(ByVal result As T, ByVal err As Exception, ByVal cancelled As Boolean)
        _Cancelled = cancelled
        _Err = err
        _Result = result
    End Sub

    Public Shared Widening Operator CType(ByVal e As RunWorkerCompletedEventArgs(Of T)) As AsyncCompletedEventArgs
        Return New AsyncCompletedEventArgs(e.Err, e.Cancelled, e.Result)
    End Operator

    Public Property Cancelled() As Boolean
        Get
            Return _Cancelled
        End Get
        Private Set(ByVal value As Boolean)
            _Cancelled = value
        End Set
    End Property

    Public Property Err() As Exception
        Get
            Return _Err
        End Get
        Private Set(ByVal value As Exception)
            _Err = value
        End Set
    End Property

    Public Property Result() As T
        Get
            Return _Result
        End Get
        Private Set(ByVal value As T)
            _Result = value
        End Set
    End Property

End Class
