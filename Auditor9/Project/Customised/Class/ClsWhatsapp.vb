Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Public Class WhatsAppSender
    Public Property Task As Object

    Public Sub SendPdfViaWhatsApp(phoneNumber As String, pdfPath As String, Optional message As String = "")
        ' Step 1: Validate inputs
        If Not File.Exists(pdfPath) Then
            MessageBox.Show("PDF file not found!")
            Return
        End If

        ' Step 2: Format phone number (remove all non-digits)
        Dim cleanNumber As String = New String(phoneNumber.Where(Function(c) Char.IsDigit(c)).ToArray())

        ' Step 3: Create temporary copy in accessible location
        Dim tempFolder As String = Path.Combine(Path.GetTempPath(), "WhatsAppSend")
        Directory.CreateDirectory(tempFolder)
        Dim tempFilePath As String = Path.Combine(tempFolder, Path.GetFileName(pdfPath))
        File.Copy(pdfPath, tempFilePath, True)

        Try
            ' Step 4: Generate WhatsApp deep link
            Dim whatsappUrl As String = $"https://wa.me/{cleanNumber}?text={Uri.EscapeDataString(message)}"

            ' Step 5: Open WhatsApp with the file attached
            Process.Start(New ProcessStartInfo() With {
                .FileName = whatsappUrl,
                .UseShellExecute = True
            })

            ' Step 6: Wait for WhatsApp to open
            Threading.Thread.Sleep(2000)

            ' Step 7: Simulate ALT+TAB to bring window to focus (optional)
            SendKeys.SendWait("%{TAB}")

            ' Step 8: Auto-attach the file (requires UI automation)
            Threading.Thread.Sleep(1000)
            SendKeys.SendWait("^a")  ' Select existing text
            SendKeys.SendWait("{DEL}") ' Clear text
            SendKeys.SendWait("^t")  ' Ctrl+T to attach (works in WhatsApp Web)
            Threading.Thread.Sleep(500)
            SendKeys.SendWait(tempFilePath)
            SendKeys.SendWait("{ENTER}")

            ' Note: User still needs to manually press send button
            MessageBox.Show("Please click SEND in WhatsApp to complete the process")

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        Finally
            ' Clean up after 5 minutes
            Task.Delay(300000).ContinueWith(Sub(t) Directory.Delete(tempFolder, True))
        End Try
    End Sub

    Public Sub SendPdfViaWhatsAppWeb(phoneNumber As String, pdfPath As String)
        ' Open WhatsApp Web with the phone number
        Process.Start($"https://web.whatsapp.com/send?phone={phoneNumber}")

        ' Instruct user to manually attach the file
        MessageBox.Show("Please manually attach this file: " & pdfPath)
    End Sub

    Public Sub SendPdfWithAttachment(phoneNumber As String, pdfPath As String, Optional message As String = "")
        ' Clean phone number (remove all non-digits)
        Dim cleanNumber As String = New String(phoneNumber.Where(Function(c) Char.IsDigit(c)).ToArray())

        ' Verify file exists
        If Not File.Exists(pdfPath) Then
            MessageBox.Show("PDF file not found!")
            Return
        End If

        ' Create a temporary copy in a safe location
        Dim tempFolder As String = Path.Combine(Path.GetTempPath(), "WhatsAppSend")
        Directory.CreateDirectory(tempFolder)
        Dim tempFilePath As String = Path.Combine(tempFolder, Path.GetFileName(pdfPath))
        File.Copy(pdfPath, tempFilePath, True)

        Try
            ' Open WhatsApp with the phone number
            Process.Start($"whatsapp://send?phone={cleanNumber}")

            ' Wait for WhatsApp to open (adjust delay as needed)
            Threading.Thread.Sleep(3000)

            ' Send keys to attach file
            SendKeys.SendWait("^t") ' Ctrl+T (attach file shortcut)
            Threading.Thread.Sleep(1000)
            SendKeys.SendWait(tempFilePath) ' Path to the file
            SendKeys.SendWait("{ENTER}") ' Confirm file selection
            Threading.Thread.Sleep(1000)

            ' Type the message (if any)
            If Not String.IsNullOrEmpty(message) Then
                SendKeys.SendWait(message)
            End If

            ' Note: User must still manually click "Send"
            MessageBox.Show("Please click SEND in WhatsApp")

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub
End Class

