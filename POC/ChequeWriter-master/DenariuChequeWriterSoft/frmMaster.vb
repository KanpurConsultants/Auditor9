Imports System.IO
Imports System.IO.Packaging
Imports DenariuChequeWriterSoft.My.Resources

Public Class frmMaster

    Private Sub frmMaster_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.OK Then
            e.Cancel = True
            If MsgDialog.ShowMsgDlg(Message.Company_Exit, Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then End
        End If
    End Sub

    Private Sub frm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.Return Then

        ElseIf e.KeyValue = Keys.Escape And MdiChildren.Length = 0 Then
            If MsgDialog.ShowMsgDlg(Message.Main_Esc_Msg, Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then End
        End If

    End Sub

    Private Sub frmMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If Type <> "Admin" Then
        '    MenuStrip1.Visible = False
        'Else
        '    MenuStrip1.Visible = True
        'End If

        'Me.Text = CompName

        'Dim ComCode As String = db.CompanyDetails.CodeByUserCode(CompName)
        If File.Exists(Application.StartupPath + "\Images\wallpaper.jpg") Then
            Dim buffer As Byte() = File.ReadAllBytes(Application.StartupPath + "\Images\wallpaper.jpg")
            Dim stream As MemoryStream = New MemoryStream(buffer)

            Dim bm As Bitmap = CType(Bitmap.FromStream(stream), Bitmap)

            Me.BackgroundImage = bm.Clone
            stream.Dispose()
            bm.Dispose()
            Me.BackgroundImageLayout = ImageLayout.Stretch
        Else
            Me.BackgroundImage = Nothing
        End If
        FormShow(frmPrintCheque)

        'Dim f As New frmPrintCheque
        'f.MdiParent = Me
        'f.ControlBox = False
        'f.WindowState = FormWindowState.Maximized
        'f.Show()

        'Timer1.Interval = 500
        'Timer1.Start()
        'lbWecomeMsg.Text = "Welcome " & CompName
    End Sub

    Private Sub MasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MasterToolStripMenuItem.Click
        FormShow(frmCompanyDetails)
        ' If frmCompanyDetails.WindowState = FormWindowState.Minimized Or frmCompanyDetails.WindowState = FormWindowState.Normal Then frmCompanyDetails.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub PAYEEREGISTRATIONToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYEEREGISTRATIONToolStripMenuItem.Click
        FormShow(frmPayeeInfo)
        ' If frmPayeeInfo.WindowState = FormWindowState.Minimized Or frmPayeeInfo.WindowState = FormWindowState.Normal Then frmPayeeInfo.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem.Click
        FormShow(frmChequeIssueReport)
        'If frmChequeIssueReport.WindowState = FormWindowState.Minimized Or frmChequeIssueReport.WindowState = FormWindowState.Normal Then frmChequeIssueReport.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub PRINTENVELOPEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRINTENVELOPEToolStripMenuItem.Click
        ' If frmPrintEnvelopePrint.WindowState = FormWindowState.Minimized Or frmPrintEnvelopePrint.WindowState = FormWindowState.Normal Then frmPrintEnvelopePrint.WindowState = FormWindowState.Maximized
        FormShow(frmPrintEnvelopePrint)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'lbWecomeMsg.Visible = Not lbWecomeMsg.Visible
    End Sub

    Private Sub PRINTCHEQUEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRINTCHEQUEToolStripMenuItem.Click
        ' If frmPrintCheque.WindowState = FormWindowState.Minimized Or frmPrintCheque.WindowState = FormWindowState.Normal Then frmPrintCheque.WindowState = FormWindowState.Maximized
        FormShow(frmPrintCheque)
    End Sub

    Private Sub ENVELOPESIZEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub EXITToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXITToolStripMenuItem.Click
        If MsgDialog.ShowMsgDlg("Do you want to Exit?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then End
    End Sub

    Private Sub NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem.Click
        'If frmBankInfo.WindowState = FormWindowState.Minimized Or frmBankInfo.WindowState = FormWindowState.Normal Then frmBankInfo.WindowState = FormWindowState.Maximized
        FormShow(frmBankInfo)
    End Sub

    Private Sub ENVELOPEADDRESSToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ENVELOPEADDRESSToolStripMenuItem1.Click
        ' If frmPrintEnvelopeNew.WindowState = FormWindowState.Minimized Or frmPrintEnvelopeNew.WindowState = FormWindowState.Maximized Then frmPrintEnvelopeNew.WindowState = FormWindowState.Normal
        FormShow(frmPrintEnvelopeNew)
    End Sub

    Private Sub ENVELOPESIZEToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ENVELOPESIZEToolStripMenuItem1.Click
        ' If frmEnvelopeSizeNew.WindowState = FormWindowState.Minimized Or frmEnvelopeSizeNew.WindowState = FormWindowState.Maximized Then frmEnvelopeSizeNew.WindowState = FormWindowState.Normal
        FormShow(frmEnvelopeSizeNew)
    End Sub
    Sub ZipFile(ByVal ZipPath As String, ByVal FileNames As ArrayList)
        If FileNames.Count = 0 Then Exit Sub

        Dim Zip As Package = ZipPackage.Open(ZipPath, FileMode.OpenOrCreate, FileAccess.ReadWrite)

        For i As Integer = 0 To FileNames.Count - 1

            Dim uriFileName As String = FileNames(i).ToString().Replace(" ", "_")
            Dim zipUri As String = String.Concat("/", IO.Path.GetFileName(uriFileName))

            Dim partUri As New Uri(zipUri, UriKind.Relative)
            Dim contentType As String = Net.Mime.MediaTypeNames.Application.Zip

            Dim pkgPart As PackagePart = Zip.CreatePart(partUri, contentType, CompressionOption.Normal)
            Dim bites As Byte() = File.ReadAllBytes(FileNames(i).ToString())
            pkgPart.GetStream().Write(bites, 0, bites.Length)

        Next

        Zip.Close()
    End Sub
    Private Sub UnZipFile(ByVal UnZipPath As String, ByVal ZipFilePath As String)

        Dim zip As Package = ZipPackage.Open(ZipFilePath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
        For Each pkgPart As PackagePart In zip.GetParts()
            Dim fileName As String = pkgPart.Uri.OriginalString.Substring(1)

            If IO.Path.GetExtension(fileName) = ".psmdcp" OrElse fileName.IndexOf("_rels") > -1 Then Continue For

            Dim stream As Stream = pkgPart.GetStream()
            'Read all of the bytes from the file to add to the zip file
            Dim bites(CInt(stream.Length - 1)) As Byte
            stream.Read(bites, 0, bites.Length)
            'fileName = fileName.Replace("_", " ")  'replace underscore with space
            File.WriteAllBytes(String.Concat(UnZipPath, "\", fileName), bites)

        Next

        zip.Close()

    End Sub
    Private Sub BackupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupToolStripMenuItem.Click
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            Dim DBPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + GetBackupPath()
            Dim sourcepath1 As String = DBPath + GetDBFileName()
            Dim sourcepath2 As String = DBPath + GetDBLogFileName()

            frmPrintCheque.Close()

            Dim arr As New ArrayList
            arr.Add(sourcepath1)
            arr.Add(sourcepath2)

            JISDb.Disconnect()
            JISDb = Nothing

            System.Threading.Thread.Sleep(20000)

            ZipFile(SaveFileDialog1.FileName, arr)
            JISDb = New JISDatabase(ConStr, ConType)

            Me.Close()
            frmLogin.Show()
            frmLogin.txtUserName.Text = ""
            frmLogin.txtPassword.Text = ""
            frmLogin.BringToFront()

        End If
    End Sub

    Private Sub RestoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreToolStripMenuItem.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            Dim DBPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + GetBackupPath()
            JISDb.Disconnect()
            System.Threading.Thread.Sleep(10000)

            UnZipFile(DBPath, OpenFileDialog1.FileName)

            JISDb = New JISDatabase(ConStr, ConType)
            Me.Close()
            frmLogin.Show()
            frmLogin.BringToFront()

        End If
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        If MsgDialog.ShowMsgDlg("Do you want to LogOut?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then
            frmLogin.Show()
            frmLogin.BringToFront()
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
       
    End Sub

    Private Sub ENVELOPEADDRESSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ENVELOPEADDRESSToolStripMenuItem.Click
        FormShow(frmEnvelopeSizeNew)
    End Sub

    Private Sub ENVELOPESIZEToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ENVELOPESIZEToolStripMenuItem.Click
        FormShow(frmPrintEnvelopeNew)
    End Sub

    Private Sub PRINTPAYMENTVOUCHERToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PRINTPAYMENTVOUCHERToolStripMenuItem.Click
        FormShow(frmPaymentVoucher)
    End Sub
End Class