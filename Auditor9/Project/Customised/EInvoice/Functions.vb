Module Functions
    Function URLEncode(EncodeStr As String) As String
        Dim i As Integer
        Dim erg As String

        erg = EncodeStr

        ' *** First replace '%' chr
        erg = Replace(erg, "%", Chr(1))

        ' *** then '+' chr
        erg = Replace(erg, "+", Chr(2))

        For i = 0 To 255
            Select Case i
            ' *** Allowed 'regular' characters
                Case 37, 43, 48 To 57, 65 To 90, 97 To 122

                Case 1  ' *** Replace original %
                    erg = Replace(erg, Chr(i), "%25")

                Case 2  ' *** Replace original +
                    erg = Replace(erg, Chr(i), "%2B")

                Case 32
                    erg = Replace(erg, Chr(i), "+")

                Case 3 To 15
                    erg = Replace(erg, Chr(i), "%0" & Hex(i))

                Case Else
                    erg = Replace(erg, Chr(i), "%" & Hex(i))

            End Select
        Next

        URLEncode = erg

    End Function

End Module
