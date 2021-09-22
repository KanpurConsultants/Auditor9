Imports System.Windows.Forms
Imports System.Drawing

Public Module Frmevents
    Dim ctrl As Object

    Function NumericCheck(ByVal KeyChar As Char) As Char
        ' # Accept only numeric values , ".",Enter ,Tab and Back Space key
        If (AscW(KeyChar) < 48 Or AscW(KeyChar) > 57) And AscW(KeyChar) <> 46 And AscW(KeyChar) <> 13 And AscW(KeyChar) <> 9 And AscW(KeyChar) <> 8 And AscW(KeyChar) <> 45 And (AscW(KeyChar) <> 27) Then
            KeyChar = ChrW(0)
            'MsgDialog.ShowMsgDlg("Accept only numeric values" + " and " + ".", "INVALID ENTRY", "I")
        End If
        NumericCheck = KeyChar
    End Function

    Function NumericCheckwithsingledot(ByVal sender As Object, ByVal KeyChar As Char, ByVal hyphen As Boolean) As Char
        ' # Accept only numeric values , ".",Enter ,Tab and Back Space key
        If hyphen = False Then
            If AscW(KeyChar) = 45 Then
                KeyChar = ChrW(0)
            End If
        Else
            If (sender.Text.ToString.Length > 0 Or sender.Text.contains("-")) And AscW(KeyChar) = 45 Then
                KeyChar = ChrW(0)
            End If
        End If

        If sender.Text.Contains(".") And AscW(KeyChar) = 46 Then
            KeyChar = ChrW(0)
        End If

        If (AscW(KeyChar) < 48 Or AscW(KeyChar) > 57) And AscW(KeyChar) <> 46 And AscW(KeyChar) <> 13 And AscW(KeyChar) <> 9 And AscW(KeyChar) <> 8 And AscW(KeyChar) <> 45 And (AscW(KeyChar) <> 27) Then
            KeyChar = ChrW(0)
            'MsgDialog.ShowMsgDlg("Accept only numeric values" + " and " + ".", "INVALID ENTRY", "I")
        End If
        NumericCheckwithsingledot = KeyChar
    End Function

    Function NumericCheckWithNoPrecision(ByVal sender As Object, ByVal KeyChar As Char, ByVal hyphen As Boolean) As Char
        If hyphen = False Then
            If AscW(KeyChar) = 45 Then
                KeyChar = ChrW(0)
            End If
        Else
            If sender.Text.contains("-") And AscW(KeyChar) = 45 Then
                KeyChar = ChrW(0)
            End If
        End If

        ' # Accept only numeric values ,Enter ,Tab and Back Space key
        If (AscW(KeyChar) < 48 Or AscW(KeyChar) > 57) And AscW(KeyChar) <> 13 And AscW(KeyChar) <> 9 And AscW(KeyChar) <> 8 And AscW(KeyChar) <> 45 And (AscW(KeyChar) <> 27) Then
            KeyChar = ChrW(0)
        End If

        NumericCheckWithNoPrecision = KeyChar
    End Function

    Function NumericCheckWithNoPrecision(ByVal KeyChar As Char) As Char
        ' # Accept only numeric values ,Enter ,Tab and Back Space key
        If (AscW(KeyChar) < 48 Or AscW(KeyChar) > 57) And AscW(KeyChar) <> 13 And AscW(KeyChar) <> 9 And AscW(KeyChar) <> 8 And (AscW(KeyChar) <> 27) Then
            KeyChar = ChrW(0)
        End If

        NumericCheckWithNoPrecision = KeyChar
    End Function

    Function varcharTB(ByVal KeyChar As Char) As Char
        If (AscW(KeyChar) > 32 And AscW(KeyChar) < 44) Then
            KeyChar = ChrW(0)
        End If
        varcharTB = KeyChar
    End Function

    Public Sub Clear(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            Call Clear(ctrl)
            If TypeOf ctrl Is TextBox And ctrl.Name <> "TxtSrchBox" Then
                CType(ctrl, TextBox).Text = ""
            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).Text = ""
            End If
        Next ctrl
    End Sub

    Public Sub enable(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            Call enable(ctrl)
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Enabled = True
            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).Enabled = True
            ElseIf TypeOf ctrl Is DateTimePicker Then
                CType(ctrl, DateTimePicker).Enabled = True
            End If
        Next ctrl

    End Sub

    Public Sub disable(ByVal root As Control)

        For Each ctrl As Control In root.Controls

            Call disable(ctrl)

            If TypeOf ctrl Is TextBox Then

                CType(ctrl, TextBox).Enabled = False
                CType(ctrl, TextBox).BackColor = Color.White

            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).Enabled = False
                CType(ctrl, ComboBox).BackColor = Color.White
            ElseIf TypeOf ctrl Is DateTimePicker Then
                CType(ctrl, DateTimePicker).Enabled = False
                CType(ctrl, DateTimePicker).BackColor = Color.White

            End If

        Next ctrl
    End Sub

    Public Sub SearchBoxPrp(ByVal SrchBoxName As Control, ByVal Gotfocus As Boolean, ByVal Txt As String)

        If Gotfocus = True Then
            SrchBoxName.ForeColor = Color.Black
            SrchBoxName.Text = Trim(Txt)
        Else
            SrchBoxName.ForeColor = Color.DarkGray
            SrchBoxName.Text = Trim(Txt)
        End If

    End Sub

    Function MakeAlignCenter(ByVal length As Integer, ByVal str As String) As String
        Dim StrLen As Integer, SpaceAndData As Integer
        Dim line As String
        line = ""
        StrLen = Len(str)
        SpaceAndData = (length \ 2 - StrLen \ 2) + StrLen
        line = line & Space(length \ 2 - StrLen \ 2) & str & Space(length + 1 - SpaceAndData)
        MakeAlignCenter = line
    End Function

    Function dotedLine(ByVal length As Integer) As String
        Dim i As Integer, str As String
        str = ""
        For i = 1 To length
            str = str + "-"
        Next
        dotedLine = str
    End Function

    Public Sub RdOnly(ByVal root As Control, ByVal setbool As Boolean)
        For Each ctrl As Control In root.Controls
            Call RdOnly(ctrl, setbool)
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).ReadOnly = setbool
                CType(ctrl, TextBox).BackColor = Color.White
            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).Enabled = Not (setbool)
                CType(ctrl, ComboBox).BackColor = Color.White
            ElseIf TypeOf ctrl Is DateTimePicker Then
                CType(ctrl, DateTimePicker).Enabled = Not (setbool)
                CType(ctrl, DateTimePicker).BackColor = Color.White
            End If
        Next ctrl
    End Sub


End Module
