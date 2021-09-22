Public Class FileData

    Private _Filename As String
    Private _Timestamp As DateTime

    Public Sub New(ByVal filename As String, ByVal timestamp As DateTime)
        _Filename = filename
        _Timestamp = timestamp
    End Sub

    Public Property Filename() As String
        Get
            Return _Filename
        End Get
        Private Set(ByVal value As String)
            _Filename = value
        End Set
    End Property

    Public Property Timestamp() As DateTime
        Get
            Return _Timestamp
        End Get
        Private Set(ByVal value As DateTime)
            _Timestamp = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return String.Format("File: {0} Timestamp: {1}", Filename, Timestamp.Ticks)
    End Function

End Class
