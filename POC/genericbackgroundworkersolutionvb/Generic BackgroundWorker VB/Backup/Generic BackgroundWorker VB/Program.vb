Imports System
Imports System.Windows.Forms

Class Program
    Private Sub New()

    End Sub

    <System.STAThread()> _
    Public Shared Sub Main()
        System.Windows.Forms.Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        System.Windows.Forms.Application.Run(New FormMain)
    End Sub

End Class
