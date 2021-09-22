Public Class ClsEvents    
    Public KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private FrmFor As Object
    Sub New(ByVal FrmObjVar As Form)
        FrmFor = FrmObjVar
        FSetTxtEvents()
    End Sub
    Public Sub FSetTxtEvents()
        Dim CtlVar As Control
        Dim StrVar As String

        For Each CtlVar In FrmFor.Controls
            StrVar = CtlVar.GetType.ToString
            FSetTxtEvents_Part1(StrVar, CtlVar)
        Next
    End Sub
    Private Sub FSetTxtEvents_Part1(ByVal StrVar As String, ByVal CtlVar As Control)
        Select Case StrVar
            Case "System.Windows.Forms.TabControl", "System.Windows.Forms.TabPage", "System.Windows.Forms.GroupBox"
                For Each CtlVar In CtlVar.Controls
                    StrVar = CtlVar.GetType.ToString
                    FSetTxtEvents_Part1(StrVar, CtlVar)
                Next
            Case Else
                FSetTxtEvents_Part2(StrVar, CtlVar)
        End Select
    End Sub
    Private Sub FSetTxtEvents_Part2(ByVal StrVar As String, ByVal CtlVar As Control)
        Dim TxtTemp As TextBox
        If StrVar = "AgControls.AgTextBox" Then
            TxtTemp = CtlVar
            RemoveHandler DirectCast(CtlVar, AgControls.AgTextBox).KeyPress, AddressOf Txt_KeyPress
            RemoveHandler DirectCast(CtlVar, AgControls.AgTextBox).KeyDown, AddressOf Txt_KeyDown
            RemoveHandler DirectCast(CtlVar, AgControls.AgTextBox).GotFocus, AddressOf Txt_GotFocus
            If Not TxtTemp.Multiline Then TxtTemp.ContextMenu = New ContextMenu()
            AddHandler DirectCast(CtlVar, AgControls.AgTextBox).KeyPress, AddressOf Txt_KeyPress
            AddHandler DirectCast(CtlVar, AgControls.AgTextBox).KeyDown, AddressOf Txt_KeyDown
            If TxtTemp.Multiline Then AddHandler DirectCast(CtlVar, AgControls.AgTextBox).GotFocus, AddressOf Txt_GotFocus
        End If

        If StrVar = "System.Windows.Forms.ComboBox" Then
            RemoveHandler DirectCast(CtlVar, System.Windows.Forms.ComboBox).KeyPress, AddressOf Txt_KeyPress
            RemoveHandler DirectCast(CtlVar, System.Windows.Forms.ComboBox).KeyDown, AddressOf Txt_KeyDown
            AddHandler DirectCast(CtlVar, System.Windows.Forms.ComboBox).KeyPress, AddressOf Txt_KeyPress
            AddHandler DirectCast(CtlVar, System.Windows.Forms.ComboBox).KeyDown, AddressOf Txt_KeyDown
        End If
    End Sub
    Private Sub Txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            sender.Select(0, 0)
            FrmFor.FTxtGotFocus(sender, e)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            KEAMainKeyCode = e
            If e.KeyCode = Keys.Enter And (TypeOf sender Is ComboBox) Then SendKeys.Send("{Tab}") : Exit Sub
            If Not (TypeOf sender Is ComboBox) Then
                If Not sender.Multiline Then
                    If KEAMainKeyCode.Control Then KEAMainKeyCode.SuppressKeyPress = True
                End If
            End If
            FrmFor.FTxtKeyDown(sender, e)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If Asc(e.KeyChar) = 39 Then e.KeyChar = ""
            If Not (TypeOf sender Is ComboBox) Then If sender.Multiline Then Exit Sub
            If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
            If e.KeyChar = Chr(Keys.Enter) And Not (TypeOf sender Is ComboBox) Then SendKeys.Send("{Tab}") : Exit Sub
            If KEAMainKeyCode.Control Or KEAMainKeyCode.Alt Then Exit Sub

            FrmFor.FTxtKeyPress(sender, e)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
