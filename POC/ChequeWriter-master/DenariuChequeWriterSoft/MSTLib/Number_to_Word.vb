
Public Module Number_to_Word

    Dim Words As String
    Dim getw As String
    Dim num As Integer
    Function numtoword(ByVal num As String) As String
        If Val(num) <> 0 Then
            numtoword = ConvertNumerToWords(num) & " ONLY"

        Else
            numtoword = "Zero"
        End If
    End Function

    Private Function GetDivideby(ByVal len1 As Byte) As Long
        Select Case len1
            Case 1, 2 : GetDivideby = 1
            Case 3, 3 : GetDivideby = 100 : getw = " Hundred"
            Case 4, 5 : GetDivideby = 1000 : getw = " Thousand"
            Case 6, 7 : GetDivideby = 100000 : getw = " Lac"
            Case 8, 9 : GetDivideby = 10000000 : getw = " Crore"
        End Select
    End Function

    Private Function ConvertNumerToWords(ByVal NumberToConvert As String) As String
        On Error Resume Next
        Dim divideby As Long, Remainder As Long, Quotient As Long, dividend As Long
        Dim Quotient1 As String, Remainder1 As String, textcontent() As String

        textcontent = Split(NumberToConvert, ".")
        Remainder = Val(textcontent(0))
        Remainder1 = Remainder
        Do While Val(Remainder1) > 0
            dividend = Remainder
            divideby = GetDivideby(Len(Remainder1))

            Quotient = dividend \ divideby
            Quotient1 = Quotient

            wordings(Quotient1)

            Words = Words & getw
            getw = ""
            Remainder = dividend Mod divideby
            Remainder1 = Remainder
        Loop

        If UBound(textcontent) = 1 Then
            If Len(textcontent(1)) = 1 Then
                Words = Words & " And Sen " & Getsinglenumber(Val(textcontent(1)))
            ElseIf Len(textcontent(1)) = 2 Then
                Words = Words & " And Sen" & Getty(Left(textcontent(1), 1))
                Words = Words & Getsinglenumber(Right(textcontent(1), 1))
            End If
        End If

        ConvertNumerToWords = Words
        Words = ""
    End Function

    Private Function wordings(ByVal common As String) As String
        If Val(common) < 20 Then
            Words = Words & Getsinglenumber(Val(common))
        Else
            Words = Words & Getty(Left(common, 1))
            Words = Words & Getsinglenumber(Right(common, 1))
        End If
        Return Words
    End Function

    Private Function Getsinglenumber(ByVal number As Long) As String
        Dim Str As String = ""
        Select Case number
            Case 1 : Str = " One"
            Case 2 : Str = " Two"
            Case 3 : Str = " Three"
            Case 4 : Str = " Four"
            Case 5 : Str = " Five"
            Case 6 : Str = " Six"
            Case 7 : Str = " Seven"
            Case 8 : Str = " Eight"
            Case 9 : Str = " Nine"
            Case 10 : Str = " Ten"
            Case 11 : Str = " Eleven"
            Case 12 : Str = " Twelve"
            Case 13 : Str = " Thirteen"
            Case 14 : Str = " Fourteen"
            Case 15 : Str = " Fifteen"
            Case 16 : Str = " Sixteen"
            Case 17 : Str = " Seventeen"
            Case 18 : Str = " Eighteen"
            Case 19 : Str = " Nineteen"
        End Select
        Return Str
    End Function

    Private Function Getty(ByVal number As Long) As String
        Dim Str As String = ""
        Select Case number
            Case 2 : Str = " Twenty"
            Case 3 : Str = " Thirty"
            Case 4 : Str = " Forty"
            Case 5 : Str = " Fifty"
            Case 6 : Str = " Sixty"
            Case 7 : Str = " Seventy"
            Case 8 : Str = " Eighty"
            Case 9 : Str = " Ninty"
        End Select
        Return Str
    End Function


    Function ConvertCurrencyToEnglish(ByVal MyNumber)
        If MyNumber = "" Then Return ""
        If Not IsNumeric(MyNumber) Then Return ""
        Dim Temp
        Dim Dollars, Cents
        Dim DecimalPlace, Count

        Dim Place(9) As String
        Place(2) = " Thousand "
        Place(3) = " Million "
        Place(4) = " Billion "
        Place(5) = " Trillion "

        ' Convert MyNumber to a string, trimming extra spaces.
        MyNumber = Trim(Str(MyNumber))

        ' Find decimal place.
        DecimalPlace = InStr(MyNumber, ".")

        ' If we find decimal place...
        If DecimalPlace > 0 Then
            ' Convert cents
            Temp = Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)
            Cents = ConvertTens(Temp)

            ' Strip off cents from remainder to convert.
            MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
        End If

        Count = 1
        Do While MyNumber <> ""
            ' Convert last 3 digits of MyNumber to English dollars.
            Temp = ConvertHundreds(Right(MyNumber, 3))
            If Temp <> "" Then Dollars = Temp & Place(Count) & Dollars
            If Len(MyNumber) > 3 Then
                ' Remove last 3 converted digits from MyNumber.
                MyNumber = Left(MyNumber, Len(MyNumber) - 3)
            Else
                MyNumber = ""
            End If
            Count = Count + 1
        Loop

        ' Clean up dollars.
        Select Case Dollars
            Case ""
                Dollars = ""
            Case "One"
                Dollars = "One "
            Case Else
                Dollars = Dollars & " "
        End Select

        ' Clean up cents.
        Select Case Cents
            Case ""
                Cents = ""
            Case "Zero"
                Cents = ""
            Case "One"
                Cents = " And One Sen"
            Case Else
                Cents = " And " & " Sen " & Cents
        End Select

        ConvertCurrencyToEnglish = Dollars & Cents & " ONLY"
    End Function

    Private Function ConvertHundreds(ByVal MyNumber)
        Dim Result As String

        ' Exit if there is nothing to convert.
        If Val(MyNumber) = 0 Then Exit Function

        ' Append leading zeros to number.
        MyNumber = Right("000" & MyNumber, 3)

        ' Do we have a hundreds place digit to convert?
        If Left(MyNumber, 1) <> "0" Then
            Result = ConvertDigit(Left(MyNumber, 1)) & " Hundred "
        End If

        ' Do we have a tens place digit to convert?
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & ConvertTens(Mid(MyNumber, 2))
        Else
            ' If not, then convert the ones place digit.
            Result = Result & ConvertDigit(Mid(MyNumber, 3))
        End If

        ConvertHundreds = Trim(Result)
    End Function

    Private Function ConvertTens(ByVal MyTens)
        Dim Result As String

        ' Is value between 10 and 19?
        If Val(Left(MyTens, 1)) = 1 Then
            Select Case Val(MyTens)
                Case 10 : Result = "Ten"
                Case 11 : Result = "Eleven"
                Case 12 : Result = "Twelve"
                Case 13 : Result = "Thirteen"
                Case 14 : Result = "Fourteen"
                Case 15 : Result = "Fifteen"
                Case 16 : Result = "Sixteen"
                Case 17 : Result = "Seventeen"
                Case 18 : Result = "Eighteen"
                Case 19 : Result = "Nineteen"
                Case Else
            End Select
        Else
            ' .. otherwise it's between 20 and 99.
            Select Case Val(Left(MyTens, 1))
                Case 2 : Result = "Twenty "
                Case 3 : Result = "Thirty "
                Case 4 : Result = "Forty "
                Case 5 : Result = "Fifty "
                Case 6 : Result = "Sixty "
                Case 7 : Result = "Seventy "
                Case 8 : Result = "Eighty "
                Case 9 : Result = "Ninety "
                Case Else
            End Select

            ' Convert ones place digit.
            Result = Result & ConvertDigit(Right(MyTens, 1))
        End If

        ConvertTens = Result
    End Function

    Private Function ConvertDigit(ByVal MyDigit)
        Select Case Val(MyDigit)
            Case 1 : ConvertDigit = "One"
            Case 2 : ConvertDigit = "Two"
            Case 3 : ConvertDigit = "Three"
            Case 4 : ConvertDigit = "Four"
            Case 5 : ConvertDigit = "Five"
            Case 6 : ConvertDigit = "Six"
            Case 7 : ConvertDigit = "Seven"
            Case 8 : ConvertDigit = "Eight"
            Case 9 : ConvertDigit = "Nine"
            Case Else : ConvertDigit = ""
        End Select
    End Function

End Module
