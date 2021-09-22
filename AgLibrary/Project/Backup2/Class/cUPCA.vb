Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing

Public Class cUPCA
    Private newBitmap As Bitmap
    Private g As Graphics
    Private barCodeHeight As Integer = 80
    Private placeMarker As Integer = 0
    Private imageWidth As Integer = 0
    Private imageScale As Single = 1
    Private UPCABegin As String = "0000000000000101"
    Private UPCALeft As String() = {"0001101", "0011001", "0010011", "0111101", "0100011", "0110001", _
     "0101111", "0111011", "0110111", "0001011"}
    Private UPCAMiddle As String = "01010"
    Private UPCARight As String() = {"1110010", "1100110", "1101100", "1000010", "1011100", "1001110", _
     "1010000", "1000100", "1001000", "1110100"}
    Private UPCAEnd As String = "1010000000000000"

    Public Function CreateBarCode(ByVal txt As String, ByVal scale As Integer) As Image
        Dim img As Image = Nothing
        imageWidth = 120
        imageScale = scale
        imageWidth = System.Convert.ToInt32(imageWidth * imageScale)
        Dim imageHeightHolder As Integer = System.Convert.ToInt32(barCodeHeight * imageScale)
        Dim incomingString As String = txt.ToUpper()
        If incomingString.Length = 0 Then
            Return img
        End If
        Dim numberOfChars As Integer = incomingString.Length
        newBitmap = New Bitmap((imageWidth), imageHeightHolder, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        g = Graphics.FromImage(newBitmap)
        g.ScaleTransform(imageScale, imageScale)
        Dim newRec As New Rectangle(0, 0, (imageWidth), imageHeightHolder)
        g.FillRectangle(New SolidBrush(Color.White), newRec)
        placeMarker = 0
        txt = txt.Substring(0, 11) & GetCheckSum(txt).ToString()
        Dim wholeSet As Integer = 0
        For wholeSet = 1 To System.Convert.ToInt32(incomingString.Length)
            Dim currentASCII As Integer = AscW(Convert.ToChar((incomingString.Substring(wholeSet - 1, 1)))) - 48
            Dim currentLetter As String = ""
            If wholeSet = 1 Then
                DrawSet(UPCABegin, placeMarker, 0)
                DrawSet(UPCALeft(currentASCII), placeMarker, 0)
            ElseIf wholeSet <= 5 Then
                DrawSet(UPCALeft(currentASCII), placeMarker, 6)
            ElseIf wholeSet = 6 Then
                DrawSet(UPCALeft(currentASCII), placeMarker, 6)
                DrawSet(UPCAMiddle, placeMarker, 0)
            ElseIf wholeSet <= 11 Then
                DrawSet(UPCARight(currentASCII), placeMarker, 6)
            ElseIf wholeSet = 12 Then
                DrawSet(UPCARight(currentASCII), placeMarker, 0)
                DrawSet(UPCAEnd, placeMarker, 0)
            End If
        Next

        Dim font As New System.Drawing.Font("Courier New, Bold", 9)
        Try
            g.DrawString(txt.Substring(0, 1), font, Brushes.Black, New System.Drawing.PointF(0, 67))
            g.DrawString(txt.Substring(1, 5), font, Brushes.Black, New System.Drawing.PointF(22, 67))
            g.DrawString(txt.Substring(6, 5), font, Brushes.Black, New System.Drawing.PointF(60, 67))
            g.DrawString(txt.Substring(11, 1), font, Brushes.Black, New System.Drawing.PointF(108, 67))
        Finally
            font.Dispose()
        End Try
        img = Image.FromHbitmap(newBitmap.GetHbitmap())
        Return img
    End Function

    Public Function GetCheckSum(ByVal barCode As String) As Integer
        Dim leftSideOfBarCode As String = barCode.Substring(0, 11)
        Dim total As Integer = 0
        Dim currentDigit As Integer = 0
        Dim i As Integer = 0
        For i = 0 To leftSideOfBarCode.Length - 1
            currentDigit = Convert.ToInt32(leftSideOfBarCode.Substring(i, 1))
            If ((i - 1) Mod 2 = 0) Then
                total += currentDigit * 1
            Else
                total += currentDigit * 3
            End If
        Next
        Dim iCheckSum As Integer = (10 - (total Mod 10)) Mod 10
        Return iCheckSum
    End Function

    Private Sub DrawSet(ByVal upcCode As String, ByVal drawLocation As Integer, ByVal barHeight As Integer)
        Dim currentLetterArray As Integer() = New Integer(upcCode.Length - 1) {}
        placeMarker += upcCode.Length
        barHeight = barCodeHeight - barHeight
        Dim i As Integer = 0
        For i = 0 To upcCode.Length - 1
            currentLetterArray(i) = Convert.ToInt16(upcCode.Substring(i, 1))
        Next
        For i = 0 To upcCode.Length - 1
            If currentLetterArray(i) = 0 Then
                g.DrawLine(Pens.White, i + (drawLocation), 0, i + (drawLocation), barHeight - 6)
            ElseIf currentLetterArray(i) = 1 Then
                g.DrawLine(Pens.Black, i + (drawLocation), 0, i + (drawLocation), barHeight - 6)
            End If
        Next
    End Sub

End Class