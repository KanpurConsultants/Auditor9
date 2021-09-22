Public Class ProgressChangedEventArgs(Of T)
    Inherits System.EventArgs

    Private _ProgressPercentage As Int32
    Private _UserState As T

    Public Sub New(ByVal progressPercentage As Int32, ByVal userState As T)
        _ProgressPercentage = progressPercentage
        _UserState = userState
    End Sub

    Public Property ProgressPercentage() As Int32
        Get
            Return _ProgressPercentage
        End Get
        Private Set(ByVal value As Int32)
            _ProgressPercentage = value
        End Set
    End Property

    Public Property UserState() As T
        Get
            Return _UserState
        End Get
        Private Set(ByVal value As T)
            _UserState = value
        End Set
    End Property
End Class
