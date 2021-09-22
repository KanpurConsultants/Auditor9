
Public Class AgComboColumn
    Inherits DataGridViewComboBoxColumn

    Dim mAllowDuplicate As Boolean = True
    Dim mReadOnly As Boolean = False

    Public Property AgReadOnly() As Boolean
        Get
            AgReadOnly = mReadOnly
        End Get
        Set(ByVal value As Boolean)
            mReadOnly = value
        End Set
    End Property

    Public Property AgAllowDuplicate() As Boolean
        Get
            AgAllowDuplicate = mAllowDuplicate
        End Get
        Set(ByVal value As Boolean)
            mAllowDuplicate = value
        End Set
    End Property

End Class
