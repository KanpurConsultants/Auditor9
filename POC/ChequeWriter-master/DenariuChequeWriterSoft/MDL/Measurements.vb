Module Measurements

    Public oneInch2MM As Double = 25.4
    Public oneInch2Tups As Double = 1440
    Public oneInch2Pixel As Double = 100


    Enum Units
        Tups
        Inch
        MilliMeter
        Pixel
    End Enum

    Public Function UnitConversion(ByVal value As String, ByVal FromUOM As Units, ByVal ToUOM As Units) As String
        Dim num As Double = Val(value)
        Dim RValue As Double = num

        If FromUOM = Units.Inch Then
            If ToUOM = Units.MilliMeter Then RValue = num * oneInch2MM
            If ToUOM = Units.Tups Then RValue = num * oneInch2Tups
            If ToUOM = Units.Pixel Then RValue = num * oneInch2Pixel
        ElseIf FromUOM = Units.MilliMeter Then
            If ToUOM = Units.Inch Then RValue = num / oneInch2MM
            If ToUOM = Units.Tups Then RValue = (num / oneInch2MM) * oneInch2Tups
            If ToUOM = Units.Pixel Then RValue = (num / oneInch2MM) * oneInch2Pixel
        ElseIf FromUOM = Units.Tups Then
            If ToUOM = Units.Inch Then RValue = num / oneInch2Tups
            If ToUOM = Units.MilliMeter Then RValue = (num / oneInch2Tups) * oneInch2MM
            If ToUOM = Units.Pixel Then RValue = (num / oneInch2Tups) * oneInch2Pixel
        End If

        Return RValue.ToString()
    End Function

End Module
