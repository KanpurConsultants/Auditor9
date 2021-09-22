Public Class ClsStructure

    Enum SectionType
        ReportHeader
        Footer
        Detail
        Header
    End Enum

    Structure StrucColumnFormating
        Dim StrHideColumn As String
        Dim IntWidth As Integer
        Dim StrWrapColumn As String
    End Structure
End Class
