Module MdlFunction
    Public Function XNull(ByVal temp As Object) As Object
        XNull = CStr(IIf(IsDBNull(temp), "", temp))
    End Function
    Public Function VNull(ByRef temp As Object) As Object
        VNull = Val(IIf(IsDBNull(temp), 0, temp))
    End Function
End Module
