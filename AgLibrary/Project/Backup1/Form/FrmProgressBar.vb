Public Class FrmProgressBar
    Dim C1 As Integer
    Dim P1 As Integer


    Public Sub FMoveBar()
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If c1 = PBar1.PositionMax Then
            P1 = -1
        Else
            If C1 = PBar1.PositionMin Then
                P1 = 1
            End If
        End If
        C1 += P1
        PBar1.Position = C1
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

End Class