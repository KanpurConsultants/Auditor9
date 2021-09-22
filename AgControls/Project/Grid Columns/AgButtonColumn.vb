Public Class AgButtonColumn
    Inherits DataGridViewButtonColumn

    Dim mReadOnly As Boolean = False

    Public Property AgReadOnly() As Boolean
        Get
            AgReadOnly = mReadOnly
        End Get
        Set(ByVal value As Boolean)
            mReadOnly = value
        End Set
    End Property

End Class
