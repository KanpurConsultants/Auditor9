Public Class DoWorkEventArgs(Of TArgument, TResult)
    Inherits System.ComponentModel.CancelEventArgs

    Private _Argument As TArgument
    Private _Result As TResult

    Public Sub New(ByVal argument As TArgument)
        _Argument = argument
    End Sub

    Public Property Argument() As TArgument
        Get
            Return _Argument
        End Get
        Private Set(ByVal value As TArgument)
            _Argument = value
        End Set
    End Property

    Public Property Result() As TResult
        Get
            Return _Result
        End Get
        Set(ByVal value As TResult)
            _Result = value
        End Set
    End Property

End Class

Public Class DoWorkEventArgs(Of T)
    Inherits System.ComponentModel.CancelEventArgs

    Private _Argument As T
    Private _Result As T

    Public Sub New(ByVal argument As T)
        _Argument = argument
    End Sub

    Public Property Argument() As T
        Get
            Return _Argument
        End Get
        Private Set(ByVal value As T)
            _Argument = value
        End Set
    End Property

    Public Property Result() As T
        Get
            Return _Result
        End Get
        Set(ByVal value As T)
            _Result = value
        End Set
    End Property
End Class