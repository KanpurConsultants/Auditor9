'This Program is not copyrighted in any way. This code is freely distributable.
'Some of the code for this Application has been provided from various sources.
'Created By Andrew Courtice

Imports System.Net.Sockets
Imports System.Threading
Imports System.IO
Imports System
Imports System.Net.Dns
Imports System.Drawing.Bitmap
Imports CustomControls.IconComboBox
Imports UNOLibs.Net.ClientClass
Imports RapChatLib.RapChatLib

Public Class Form1

#Region "Declarations"

    Dim Listener As New TcpListener(65535)
    Dim Client As New TcpClient
    Dim Message As String = ""
    Dim Listener1 As New TcpListener(65534)
    Dim Client1 As New TcpClient
    Dim Message1 As String = ""
    Dim IPAdd As String
    Dim clnt As New UNOLibs.Net.ClientClass
    Dim clnt2 As New UNOLibs.Net.ClientClass
    Dim WithEvents server As UNOLibs.Net.ServerClass
    Dim WithEvents server2 As UNOLibs.Net.ServerClass

#End Region

#Region "Form"

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Location = My.Settings.WindowPosition
        Me.Size = My.Settings.WindowSize
        txtSound.Text = My.Settings.Audio
        txtName.Text = My.Settings.Username
        PicClient.ImageLocation = My.Settings.DisplayPic
        picMypic.ImageLocation = My.Settings.DisplayPic
        frmSettings.chkSound.CheckState = My.Settings.ChkSound


        txtSaveLoc.Text = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\"
        server = New UNOLibs.Net.ServerClass(65533, True, txtSaveLoc.Text)
        server2 = New UNOLibs.Net.ServerClass(65532, True, "C:\")
        Dim ListThread As New Thread(New ThreadStart(AddressOf Listening)) 'Creates the thread
        ListThread.Start() 'Starts the thread

        Dim shostname As String
        shostname = System.Net.Dns.GetHostName
        Console.WriteLine("Your Machine Name = " & shostname)
        'Call Get IPAddress
        Console.WriteLine("Your IP = " & GetIPAddress())
        StatusStrip1.Text = ("My Computer: " & shostname & " - " & GetIPAddress())
        NotifyIcon1.Visible = True
        Dim me1 As New IconComboItem
        Dim ico As New Icon(My.Application.Info.DirectoryPath & "\Users.ico")
        me1.DisplayText = GetIPAddress()
        me1.ItemImage = ico
        cmbAddress.Items.Add(me1)
        My.Application.SaveMySettingsOnExit = True
        cmbAddress.SelectedIndex = 0
    End Sub


    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        NotifyIcon1.Visible = False
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim offlinestring As String = " Has gone Offline. You can No longer Message This User."
        If RichTextBox1.Text.Contains(offlinestring) Then
            End
        Else
            txtmessage.Text = txtName.Text & " Has gone Offline. You can No longer Message This User."
            btnSend.PerformClick()

            My.Settings.WindowPosition = Me.Location
            My.Settings.WindowSize = Me.Size
            My.Settings.Username = txtName.Text
            My.Settings.DisplayPic = PicClient.ImageLocation
            My.Settings.Audio = txtSound.Text
            My.Settings.Save()

        End If
    End Sub

#End Region

#Region "Functions"

    Public Shared Function GetIPAddress() As String
        Dim oAddr As System.Net.IPAddress
        Dim sAddr As String
        With System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName())
            oAddr = New System.Net.IPAddress(.AddressList(0).Address)
            sAddr = oAddr.ToString
        End With
        GetIPAddress = sAddr
    End Function

    Private Sub Listening()
        Listener.Start()
        Listener1.Start()
    End Sub


    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Listener.Pending = True Then
            Message = ""
            Client = Listener.AcceptTcpClient()

            Dim Reader As New StreamReader(Client.GetStream())
            While Reader.Peek > -1
                Message = Message + Convert.ToChar(Reader.Read()).ToString
            End While
            RichTextBox1.ForeColor = Color.Black
            RichTextBox1.Text += Message + vbCrLf

            If frmSettings.chkSound.CheckState = CheckState.Unchecked Then
                NotifyIcon1.BalloonTipTitle = ("New Message")
                NotifyIcon1.BalloonTipText = Message
                NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
                frmMessage.Show()
                frmMessage.lblMessage.Text = Message
                lblStat.Text = ("Last Message Received At " & My.Computer.Clock.LocalTime)
            Else
                My.Computer.Audio.Play(txtSound.Text)
                NotifyIcon1.BalloonTipTitle = ("New Message")
                NotifyIcon1.BalloonTipText = Message
                NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
                frmMessage.Show()
                frmMessage.lblMessage.Text = Message
                lblStat.Text = ("Last Message Received At " & My.Computer.Clock.LocalTime)
            End If
           

        End If

        If Listener1.Pending = True Then
            Message1 = ""
            Client1 = Listener1.AcceptTcpClient()

            Dim Reader1 As New StreamReader(Client1.GetStream())
            While Reader1.Peek > -1
                Message1 = Message1 + Convert.ToChar(Reader1.Read()).ToString
            End While
            PicClient.Image = StringToBitmap(Message1)
        End If
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        If txtName.Text = "" Or cmbAddress.Text = "" Then
            MessageBox.Show("All Fields must be Filled", "Error Sending Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            btnImage.PerformClick()
            Try
                Client = New TcpClient(cmbAddress.Text, 65535)



                Dim Writer As New StreamWriter(Client.GetStream())
                Writer.Write(txtName.Text & " Says:  " & txtmessage.Text)
                Writer.Flush()
                RichTextBox1.Text += (txtName.Text & " Says:  " & txtmessage.Text) + vbCrLf
                txtmessage.Text = ""
            Catch ex As Exception
                Console.WriteLine(ex)
                Dim Errorresult As String = ex.Message
                MessageBox.Show(Errorresult & vbCrLf & vbCrLf & "Please Review Client Address", "Error Sending Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
    End Sub

    Private Sub txtmessage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmessage.TextChanged
        Dim imagepath As String = My.Application.Info.DirectoryPath & "\Message.png"
        Dim img As Image = Image.FromFile(imagepath)
        lblStat.Image = img
        lblStat.Text = "Writing Message..."

        If txtmessage.Text <> "" Then
            btnClear.Enabled = True
            btnSend.Enabled = True
        Else
            btnClear.Enabled = False
            btnSend.Enabled = False
        End If
    End Sub


    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        If cmbAddress.Text.Length < 4 Then
            MessageBox.Show("Please Enter a Valid Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            btnConnect.Text = "Connecting"
            Dim pingresult As String = My.Computer.Network.Ping(cmbAddress.Text)
            If pingresult = "True" Then
                btnConnect.Text = "Connected"
            Else
                btnConnect.Text = "Disconnected"
            End If
        End If
       
    End Sub

    Private Sub cmbAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAddress.TextChanged
        btnConnect.Text = "Connect..."
    End Sub


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtmessage.Text = ""
        lblStat.Text = "Last Message Received At: "
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImBrowse.Click
        ofdImage.ShowDialog()
        If ofdImage.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            picMypic.ImageLocation = ofdImage.FileName
            PicClient.ImageLocation = ofdImage.FileName
            Dim imgstr As String
            Dim abyt As Byte()
            Dim ms As New IO.MemoryStream
            Dim bmp As New Bitmap(picMypic.ImageLocation)

            bmp.Save(ms, Imaging.ImageFormat.Bmp)

            abyt = ms.ToArray()
            imgstr = System.Convert.ToBase64String(abyt)
            TextBox1.Text = imgstr
            PicClient.Image = picMypic.Image
        Else
            PicClient.ImageLocation = My.Application.Info.DirectoryPath & "\Data\Users.png"
            picMypic.ImageLocation = My.Application.Info.DirectoryPath & "\Data\Users.png"
        End If
    End Sub

    Public Function StringToBitmap(ByVal sImageData As String) As Bitmap
        Try
            Dim ms As New MemoryStream(Convert.FromBase64String(sImageData))
            Dim bmp As Bitmap = Bitmap.FromStream(ms)
            Return bmp
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Sub ImageButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageButton2.Click
        btnConnect.PerformClick()
    End Sub

    Private Sub ImagebtnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageButton1.Click
        cmbAddress.Text = ""
        txtName.Text = ""
        txtmessage.Text = ""
        RichTextBox1.Text = ""
    End Sub


    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        
    End Sub

    Private Sub btnImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImage.Click
    
        clnt2.SendMessage(cmbAddress.Text, 65532, TextBox1.Text)
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        Me.Text = ("Rapid Chat - " & txtName.Text)
    End Sub

    Private Sub HideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideToolStripMenuItem.Click
        Me.Hide()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.Click
        frmSettings.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        frmMyData.Show()
    End Sub

    Private Sub NetworkStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NetworkStatusToolStripMenuItem.Click
        btnBrowse.PerformClicked()
    End Sub

    Private Sub txtmessage_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmessage.Leave
        lblStat.Text = "Last Message Received At:"
    End Sub

    Private Sub PicClient_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicClient.DoubleClick
        frmNewAddress.Show()
    End Sub

    Private Sub FontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontToolStripMenuItem.Click
        fntFont.ShowDialog()
        If DialogResult.OK Then
            RichTextBox1.Font = fntFont.Font
        End If
    End Sub

    Private Sub FileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileToolStripMenuItem.Click
        SendFileToolStripMenuItem.PerformClick()
    End Sub

    Private Sub SendFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendFileToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog()

        If DialogResult.OK Then
            RichTextBox1.Text += (txtName.Text & " Is Sending You a File...") + vbCrLf
            clnt.SendFiles(cmbAddress.Text, 65533, OpenFileDialog1.FileNames)
        Else
            Exit Sub
        End If
    End Sub

    Private Sub BrowseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseToolStripMenuItem.Click
        FolderBrowserDialog1.ShowDialog()
        If DialogResult.OK Then
            txtSaveLoc.Text = FolderBrowserDialog1.SelectedPath & "\"
            server.IncomingPath = txtSaveLoc.Text
        Else
            txtSaveLoc.Text = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        End If
    End Sub

    Private Sub OnDiagnosticMessage(ByVal Args As String) Handles server.DiagnosticMessage
        RichTextBox1.Text += (txtName.Text & " - " & Args) + vbCrLf
        My.Computer.Audio.Play(My.Application.Info.DirectoryPath & "\Sounds\Notify.wav")
    End Sub

    Private Sub mnuCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCut.Click
        RichTextBox1.Cut()
    End Sub

    Private Sub mnuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCopy.Click
        RichTextBox1.Copy()
    End Sub

    Private Sub mnuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectAll.Click
        RichTextBox1.SelectAll()
    End Sub

    Private Sub FontToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontToolStripMenuItem1.Click
        FontToolStripMenuItem.PerformClick()
    End Sub

    Private Sub OnIncomingMessage(ByVal Args As UNOLibs.Net.ServerClass.InMessEvArgs) Handles server2.IncomingMessage
        PicClient.Image = StringToBitmap(Args.message)
    End Sub

    Private Sub FontColourToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontColourToolStripMenuItem.Click
        clrFont.ShowDialog()
        If DialogResult.OK Then
            RichTextBox1.ForeColor = clrFont.Color
        Else
            RichTextBox1.ForeColor = Color.Black
        End If
    End Sub



    Private Sub ExitToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem2.Click
        End
    End Sub

    Private Sub ShowToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolStripMenuItem1.Click
        Me.Show()
    End Sub

    Private Sub SettingsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem1.Click
        frmSettings.Show()
    End Sub

    Private Sub SettingsToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem2.Click
        frmSettings.Show()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem1.Click
        End
    End Sub

    Private Sub RichTextBox1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox1.MouseMove
        If RichTextBox1.SelectionLength > 0 Then
            mnuCut.Enabled = True
            mnuCopy.Enabled = True
            cmsmCut.Enabled = True
            cmsmCopy.Enabled = True
        Else
            mnuCut.Enabled = False
            mnuCopy.Enabled = False
            cmsmCut.Enabled = False
            cmsmCopy.Enabled = False
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        RichTextBox1.SelectAll()
    End Sub

#End Region

#Region "Status"

    Private Sub AwayToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AwayToolStripMenuItem2.Click
        txtmessage.Text = AwayToolStripMenuItem2.Tag
        btnSend.PerformClick()
        pnlStatus.Visible = True
        lblStatus.Text = "Your Status is currently set to Away"
    End Sub

    Private Sub lnkOnline_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkOnline.LinkClicked
        txtmessage.Text = "I am currently Online"
        btnSend.PerformClick()
        pnlStatus.Visible = False
    End Sub

    Private Sub mnuBusy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBusy.Click
        txtmessage.Text = mnuBusy.Tag
        btnSend.PerformClick()
        pnlStatus.Visible = True
        lblStatus.Text = "Your Status is currently set to Busy"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        pnlStatus.Visible = False
    End Sub

    Private Sub mnuAppearOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAppearOff.Click
        mnuAppearOff.Tag = txtName.Text & " Has gone Offline. You can No longer Message This User."
        txtmessage.Text = mnuAppearOff.Tag
        btnSend.PerformClick()
        pnlStatus.Visible = True
        lblStatus.Text = "Your Status is currently set to Appear Offline"
    End Sub

    Private Sub OnlineToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlineToolStripMenuItem2.Click
        txtmessage.Text = "I am currently Online"
        btnSend.PerformClick()
        pnlStatus.Visible = False
    End Sub

#End Region

   
    Private Sub AboutRapidChatToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutRapidChatToolStripMenuItem.Click
        Process.Start(My.Application.Info.DirectoryPath & "\About Rapid Chat\About Rapid Chat.exe")
    End Sub
End Class
