Imports System.Windows.Forms
Public Class AgComboBox
    Inherits ComboBox


    Dim mCmboMaster As Boolean = False
    Dim mMandatory As Boolean = False

    Public Property AgCmboMaster() As Boolean
        Get
            AgCmboMaster = mCmboMaster
        End Get
        Set(ByVal value As Boolean)
            mCmboMaster = value
        End Set
    End Property

    Public Property AgMandatory() As Boolean
        Get
            AgMandatory = mMandatory
        End Get
        Set(ByVal value As Boolean)
            mMandatory = value
        End Set
    End Property

    Private Sub AgCombo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Dim sTypedText As String
        Dim iFoundIndex As Integer

        Select Case e.KeyChar
            Case Chr(Keys.Back), Chr(Keys.Delete)
            Case Chr(Keys.Enter), Chr(Keys.Return), Chr(Keys.Tab)

            Case Else
                If mCmboMaster = False Then
                    sTypedText = Me.Text + e.KeyChar
                    iFoundIndex = Me.FindString(sTypedText)
                    If iFoundIndex < 0 Then
                        e.KeyChar = ""
                    End If
                End If
        End Select
    End Sub

    Private Sub AgCombo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        If Me.Text <> "" And mCmboMaster = False Then
            Me.SelectedIndex = Me.FindString(Me.Text)
        End If
    End Sub

    Private Sub AgComboBox_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If mMandatory = True Then
            If Me.Text.Trim = "" Then
                MsgBox("Required Field" & vbCrLf & "Can't Be Blank!")
                e.Cancel = True
            End If
        End If
    End Sub
End Class

