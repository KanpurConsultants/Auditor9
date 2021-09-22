Public Class DBTBOBJ
    'Master Obj
    Public UserAccount As JISTable = JISDb.Table("UserAccount")
    Public CompanyDetails As JISTable = JISDb.Table("CompanyDetails")
    Public Bank As JISTable = JISDb.Table("Bank")
    Public Payee As JISTable = JISDb.Table("Payee")
    Public PrintData As JISTable = JISDb.Table("PrintData")
    Public EnvelopeSize As JISTable = JISDb.Table("EnvelopeSize")
    Public EnvelopeName As JISTable = JISDb.Table("EnvelopeName")
    Sub New()

    End Sub
    Public Function ExecuteQuery(ByVal Qry As String) As Object
        Return JISDb.ExecuteQuery(Qry)
    End Function
End Class
