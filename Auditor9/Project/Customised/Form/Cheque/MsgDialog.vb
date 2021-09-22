Public Class MsgDialog

    Dim ktb As String = Nothing
    Dim mtxt As String = Nothing
    Dim op As String = Nothing
    Dim mtype As String = Nothing
    Dim i As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Timer1.Dispose()
        Me.Dispose()
    End Sub

    Private Sub Yes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub No_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Close()
    End Sub

    Public Sub ShowMsgDlg(ByVal KeyTxtBox As Control, ByVal MsgTxt As String, ByVal OprPerf As String, ByVal MsgType As String)

        ktb = KeyTxtBox.Text.ToString.Trim

        'If MsgTxt.Length > 25 Then
        '    For i = 26 To MsgTxt.Length - 1
        '        If MsgTxt(i) = " " Then
        '            MsgTxt = MsgTxt.Substring(0, i) & vbCrLf & MsgTxt.Substring(i, MsgTxt.Length - i)
        '            Exit For
        '        End If
        '    Next i
        'End If

        mtxt = MsgTxt
        op = OprPerf
        mtype = MsgType
        Me.ShowDialog()
    End Sub

    Public Function ShowMsgDlg(ByVal MsgTxt As String, ByVal OprPerf As String, ByVal MsgType As String) As MsgBoxResult

        'If MsgTxt.Length > 25 Then
        '    For i = 26 To MsgTxt.Length - 1
        '        If MsgTxt(i) = " " Then
        '            MsgTxt = MsgTxt.Substring(0, i) & vbCrLf & MsgTxt.Substring(i, MsgTxt.Length - i)
        '            Exit For
        '        End If
        '    Next i
        'End If

        mtxt = MsgTxt
        op = OprPerf
        mtype = MsgType
        Try
            Return Me.ShowDialog()
        Catch ex As Exception

        End Try

    End Function

    Private Sub MsgDialog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        mtxt = ""
        op = ""
        mtype = ""
        ktb = ""
    End Sub

    Private Sub MsgDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'frmPOSMaster.obj_formpref.setcolors(Me)
        SetFormStyle(Me)
        Me.TableLayoutPanel1.Visible = False
        Me.TableLayoutPanel3.Visible = False
        With Me
            .lblMsg.Text = mtxt + ktb
            .lblHeading.Text = op
            Select Case mtype
                Case "Q"            ' yes or no
                    .pnlImage.BackgroundImage = My.Resources.QUEST
                    .TableLayoutPanel3.Visible = Not .TableLayoutPanel1.Visible
                    Console.Beep()
                    If Not .btnYes.Focused = True Then btnYes.Focus()

                Case "I"            ' INFO
                    .pnlImage.BackgroundImage = My.Resources.information
                    .TableLayoutPanel1.Visible = Not .TableLayoutPanel3.Visible
                    Timer1.Enabled = True
                    Timer1.Interval = 3000
                    If Not .btnOk.Focused = True Then btnOk.Focus()

                Case "E"            'ERROR
                    .pnlImage.BackgroundImage = My.Resources.Attention
                    .TableLayoutPanel1.Visible = Not .TableLayoutPanel3.Visible
                    Console.Beep()
                    If Not .btnOk.Focused = True Then btnOk.Focus()

                Case "W"            'WARNING
                    .pnlImage.BackgroundImage = My.Resources.Attention
                    .TableLayoutPanel1.Visible = Not .TableLayoutPanel3.Visible
                    Console.Beep()
                    If Not .btnOk.Focused = True Then btnOk.Focus()
            End Select
        End With

    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        OK_Button_Click(sender, e)
    End Sub

    Private Sub pnlMain_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlMain.Paint

    End Sub

    Private Sub lblHeading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHeading.Click

    End Sub
End Class
