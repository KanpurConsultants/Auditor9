Public Class frmKeyRegistration

    Private Sub frmKeyRegistration_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Me.DialogResult <> Windows.Forms.DialogResult.OK Then
            End
        End If
    End Sub

    Private Sub frmKeyRegistration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim K1 As String = getk1value()
        lblText1.Text = K1.Substring(0, 5)
        lblText2.Text = K1.Substring(5, 5)
        lblText3.Text = K1.Substring(10, 5)

    End Sub

    Private Sub txtK2Box1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtK2Box3.KeyPress, txtK2Box2.KeyPress, txtK2Box1.KeyPress
        e.KeyChar = NumericCheckwithsingledot(sender, e.KeyChar, False)
    End Sub

    Private Sub frmKeyRegistration_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Me.Icon = My.Resources.Plain
    End Sub

    Private Sub btnRegistration_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistration.Click
        Registration()
    End Sub
    Sub Registration()
        If Encrypt(lblText1.Text + lblText2.Text + lblText3.Text, sk) = txtK2Box1.Text + txtK2Box2.Text + txtK2Box3.Text Then
            SetK2Value(txtK2Box1.Text + txtK2Box2.Text + txtK2Box3.Text)
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            MsgBox("Please enter the valid key.")
        End If
    End Sub
    Private Sub btnCancel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End
    End Sub



    Private Sub txtK2Box2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtK2Box2.KeyDown
        If e.KeyCode = Keys.Back Then
            If txtK2Box2.Text.Trim() = "" Then
                txtK2Box1.Focus()
            End If        
        End If
       
    End Sub

    Private Sub txtK2Box3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtK2Box3.KeyDown
        If e.KeyCode = Keys.Back Then
            If txtK2Box3.Text.Trim() = "" Then
                txtK2Box2.Focus()
            End If
        ElseIf e.KeyCode = Keys.Enter Then
            Registration()
        End If
        
    End Sub

    Private Sub txtK2Box1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtK2Box1.KeyUp
        
        Dim len As Integer
        len = Val(txtK2Box1.Text.Length)
        If len > 4 Then
            txtK2Box2.Focus()
        End If
    End Sub

    Private Sub txtK2Box2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtK2Box2.KeyUp
 
        Dim len As Integer
        len = Val(txtK2Box2.Text.Length)
        If len > 4 Then
            txtK2Box3.Focus()
        End If
    End Sub
End Class