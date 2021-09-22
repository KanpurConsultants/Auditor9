Module MdlFunction
    Public Sub EnableDisableControls(ByRef mForm As Form, ByVal EntryMode As String)
        Dim mObj As Object
        Dim Enb As Boolean = False

        If EntryMode <> "Browse" Then
            Enb = True
        End If
        For Each mObj In mForm.Controls
            With mObj
                If TypeOf mObj Is TextBox Then
                    .Enabled = Enb
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is ComboBox Then
                    .Enabled = Enb
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is GroupBox Then
                    .Enabled = Enb
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is DateTimePicker Then
                    .Enabled = Enb
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is TabPage Then
                    EnableDisableControls(mObj, Enb)
                End If
            End With
        Next
    End Sub

    Public Sub EnableDisableControls(ByRef mTabPage As TabPage, ByVal Enb As Boolean)
        Dim mObj As Object
        For Each mObj In mTabPage.Controls
            With mObj
                If TypeOf mObj Is TextBox Then
                    .Enabled = Enb
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is ComboBox Then
                    .Enabled = Enb
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is GroupBox Then
                    .Enabled = Enb
                    .BackColor = Color.White
                ElseIf TypeOf mObj Is DateTimePicker Then
                    .Enabled = Enb
                    .BackColor = Color.White
                End If
            End With
        Next
    End Sub

End Module
