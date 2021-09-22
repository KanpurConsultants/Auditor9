Imports System.IO
Imports System.Data.SqlClient

Public Class frmBankInfo
    Dim tb As JISTable = db.Bank
    Dim fName As String
    Dim cnn As SqlConnection
    Dim connectionString As String
    Dim oneMM2Pixel As Double = 3.779528
    Dim onePixel2Point As Double = 0.75

    Dim IsDrag As Boolean = False
    Dim DateX, DateY As Integer

    Public w, h As String
    Private Function ImageToStream(ByVal fileName As String) As Byte()
        Dim stream As New MemoryStream()
tryagain:
        Try
            Dim image As New Bitmap(fileName)
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
        Catch ex As Exception
            GoTo tryagain
        End Try
        Return stream.ToArray()
    End Function
    Function FormValidate() As Boolean
        Dim RValue As Boolean = True
        If txtBankName.Text.Trim() = "" Then
            MsgBox(My.Resources.Message.Bank_Name_Required)
            txtBankName.Focus()
            RValue = False
        ElseIf tb.State = "New" And tb.ExistCode(txtBankName.Text) Then
            MsgBox("Bank Name Already Exist", MsgBoxStyle.Information)
            RValue = False
        ElseIf imgChequePreview.Tag Is Nothing Then
            MsgBox(My.Resources.Message.Bank_Cheqe_Leaf_Required)
            RValue = False
        End If

        If Val(txtWidth.Text) < 0 Then
            MsgBox(My.Resources.Message.Bank_Width_Validate)
            txtWidth.Focus()
            RValue = False
        End If

        If Val(txtHeight.Text) < 0 Then
            MsgBox(My.Resources.Message.Bank_Height_Validate)
            txtHeight.Focus()
            RValue = False
        End If
        Return RValue
    End Function

    Private Sub CTRL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmtLeft.Enter, txtAmtTop.Enter, txtBankName.Enter, txtCode.Enter, txtDateLeft.Enter, txtDateTop.Enter, txtFileName.Enter, txtFilePath.Enter, txtFromLeft.Enter, txtFromTop.Enter, txtHeight.Enter, txtId.Enter, txtInpAmountLeft.Enter, txtInpAmountTop.Enter, txtInpNameLeft.Enter, txtInpNameTop.Enter, txtInpWordsLeft.Enter, txtInpWordsTop.Enter, txtLeft.Enter, txtNameLeft.Enter, txtNameTop.Enter, txtPnlDateLeft.Enter, txtPnlDateTop.Enter, txtSearchbox.Enter, txtTop.Enter, txtWidth.Enter
        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmtTop.Leave, txtBankName.Leave, txtCode.Leave, txtDateTop.Leave, txtFileName.Leave, txtFilePath.Leave, txtFromLeft.Leave, txtFromTop.Leave, txtHeight.Leave, txtId.Leave, txtInpAmountLeft.Leave, txtInpAmountTop.Leave, txtInpNameLeft.Leave, txtInpNameTop.Leave, txtInpWordsLeft.Leave, txtInpWordsTop.Leave, txtNameTop.Leave, txtPnlDateLeft.Leave, txtPnlDateTop.Leave, txtSearchbox.Leave, txtTop.Leave, txtWidth.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight

    End Sub

    Private Sub frmBankInfo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.Icon = My.Resources.Plain
    End Sub

    Private Sub frmBanks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim ctl As Control = Me.ActiveControl
            If TypeOf ctl Is TextBox Then
                Dim txt As TextBox = DirectCast(ctl, TextBox)
                If txt.Multiline = False Then e.KeyChar = ChrW(0)
            Else
                e.KeyChar = ChrW(0)
            End If
        End If

    End Sub

    Private Sub frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tb.SetControls(txtId, txtBankName, txtLeft, txtTop, imgChequePreview, txtPnlDateLeft, txtPnlDateTop, txtInpNameLeft, txtInpNameTop, txtInpAmountLeft, txtInpAmountTop, txtInpWordsLeft, txtInpWordsTop, txtNameLeft, txtNameTop, txtAmtLeft, txtAmtTop, txtDateLeft, txtDateTop, txtInWordLeft, txtInWordTop, txtWidth, txtHeight)
        SetFormStyle(Me)
        NewForm()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        'FormShowView(frmBankInfoReport)
    End Sub

    Private Sub frm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyValue = Keys.Return Then
            Dim ctl As Control = Me.ActiveControl
            If ctl.Text = "+" Then

            ElseIf ctl.Text = "@" Then

            Else
                If TypeOf ctl Is TextBox Then
                    Dim txt As TextBox = DirectCast(ctl, TextBox)
                    If txt.Multiline = True Then
                        If txt.Text.EndsWith(vbCrLf) Then
                            Me.SelectNextControl(ctl, True, True, True, True)
                        End If
                    Else
                        Me.SelectNextControl(ctl, True, True, True, True)
                    End If
                Else
                    Me.SelectNextControl(ctl, True, True, True, True)
                End If




            End If

        ElseIf e.KeyValue = Keys.Escape Then
            If MsgDialog.ShowMsgDlg(My.Resources.Message.Bank_Exit, Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
        End If

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        NewForm()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        SendKeys.Send("{ESCAPE}")
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveForm()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteForm()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewForm()
    End Sub

    Sub defaultChequeValue()
        txtWidth.Text = "176"
        txtHeight.Text = "89"
        txtPnlDateLeft.Text = "480"
        txtPnlDateTop.Text = "46"
        txtInpNameLeft.Text = "72"
        txtInpNameTop.Text = "97"
        txtInpAmountLeft.Text = "480"
        txtInpAmountTop.Text = "127"
        txtInpWordsLeft.Text = "87"
        txtInpWordsTop.Text = "121"

        txtNameLeft.Text = 20
        txtNameTop.Text = 31



        txtDateLeft.Text = 127
        txtDateTop.Text = 19

        txtAmtLeft.Text = 127
        txtAmtTop.Text = 40

        txtInWordLeft.Text = 25
        txtInWordTop.Text = 41

        txtLeft.Text = 0
        txtTop.Text = 0.75
    End Sub

    Sub NewForm()
        tb.NewRecord()
        txtBankName.Focus()

        defaultChequeValue()

        imgChequePreview.Tag = Nothing

        ErrorProvider1.Clear()
    End Sub

    Sub ViewForm()
        If txtSearchbox.Text.Trim() = "" Then
            txtSearchbox.Focus()
        Else
            Dim str As String = ""
            str = tb.ViewRecordByCode(txtSearchbox.Text)
            imgChequePreview.Width = ((Val(txtWidth.Text) / oneInch2MM) * 96) * (96 / 72)
            imgChequePreview.Height = ((Val(txtHeight.Text) / oneInch2MM) * 96) * (96 / 72)
            imgChequePreview.SizeMode = PictureBoxSizeMode.StretchImage
            If str <> "" Then MsgDialog.ShowMsgDlg(str, Me.Text, "I") Else txtSearchbox.Text = ""
            ErrorProvider1.Clear()
        End If

    End Sub

    Sub SaveForm()
        If FormValidate() Then
            connectionString = GetConStr()
            cnn = New SqlConnection(connectionString)

            cnn.Open()
            Dim cmd As SqlCommand
            If tb.State = "New" Then
                cmd = New SqlCommand("insert into Bank (BankId, BankName, LeftMargin, TopMargin, BankImg, PnlDateLeft, PnlDateTop, InpNameLeft, InpNameTop, InpAmountLeft, InpAmountTop, InpWordsLeft, InpWordsTop, RPTNameLeft, RPTNameTop, RPTAmountLeft, RPTAmountTop, RPTDateLeft, RPTDateTop, RPTWordsLeft, RPTWordsTop,ChqWidth,ChqHeight) values (@BankId, @BankName, @LeftMargin, @TopMargin, @BankImg, @PnlDateLeft, @PnlDateTop, @InpNameLeft, @InpNameTop, @InpAmountLeft, @InpAmountTop, @InpWordsLeft, @InpWordsTop,@RPTNameLeft,@RPTNameTop,@RPTAmountLeft,@RPTAmountTop,@RPTDateLeft,@RPTDateTop,@RPTWordsLeft,@RPTWordsTop,@ChqWidth,@ChqHeight)", cnn)

            Else
                cmd = New SqlCommand("update Bank set BankName=@BankName,LeftMargin=@LeftMargin,TopMargin=@TopMargin,BankImg=@BankImg,PnlDateLeft=@PnlDateLeft,PnlDateTop=@PnlDateTop,InpNameLeft=@InpNameLeft,InpNameTop=@InpNameTop,InpAmountLeft=@InpAmountLeft,InpAmountTop=@InpAmountTop, InpWordsLeft=@InpWordsLeft, InpWordsTop=@InpWordsTop,RPTNameLeft=@RPTNameLeft,RPTNameTop=@RPTNameTop,RPTAmountLeft=@RPTAmountLeft,RPTAmountTop=@RPTAmountTop,RPTDateLeft=@RPTDateLeft,RPTDateTop=@RPTDateTop,RPTWordsLeft=@RPTWordsLeft,RPTWordsTop=@RPTWordsTop,ChqWidth=@ChqWidth,ChqHeight=@ChqHeight where BankId=@BankId", cnn)
            End If

            cmd.Parameters.AddWithValue("@BankId", txtId.Text)
            cmd.Parameters.AddWithValue("@BankName", txtBankName.Text)
            If txtWidth.Text = "" Then
                cmd.Parameters.AddWithValue("@LeftMargin", "0")
            Else
                cmd.Parameters.AddWithValue("@LeftMargin", txtLeft.Text)
            End If
            If txtHeight.Text = "" Then
                cmd.Parameters.AddWithValue("@TopMargin", 0)
            Else
                cmd.Parameters.AddWithValue("@TopMargin", txtTop.Text)
            End If

            If imgChequePreview.Tag Is Nothing Then
                txtFilePath.Text = Application.StartupPath & "\Default.png"
                txtFileName.Text = System.IO.Path.GetFileNameWithoutExtension("Default.png")
                Me.imgChequePreview.ImageLocation = txtFilePath.Text
                imgChequePreview.Tag = ImageToStream(txtFilePath.Text)
                cmd.Parameters.AddWithValue("@BankImg", imgChequePreview.Tag)
            Else
                cmd.Parameters.AddWithValue("@BankImg", imgChequePreview.Tag)
            End If


            If txtPnlDateLeft.Text = "" Then
                cmd.Parameters.AddWithValue("@PnlDateLeft", 0)
            Else
                cmd.Parameters.AddWithValue("@PnlDateLeft", txtPnlDateLeft.Text)
            End If

            If txtPnlDateTop.Text = "" Then
                cmd.Parameters.AddWithValue("@PnlDateTop", "0")
            Else
                cmd.Parameters.AddWithValue("@PnlDateTop", txtPnlDateTop.Text)
            End If

            If txtInpNameLeft.Text = "" Then
                cmd.Parameters.AddWithValue("@InpNameLeft", "0")
            Else
                cmd.Parameters.AddWithValue("@InpNameLeft", txtInpNameLeft.Text)
            End If

            If txtInpNameTop.Text = "" Then
                cmd.Parameters.AddWithValue("@InpNameTop", "0")
            Else
                cmd.Parameters.AddWithValue("@InpNameTop", txtInpNameTop.Text)
            End If

            If txtInpAmountLeft.Text = "" Then
                cmd.Parameters.AddWithValue("@InpAmountLeft", "0")
            Else
                cmd.Parameters.AddWithValue("@InpAmountLeft", txtInpAmountLeft.Text)
            End If

            If txtInpAmountTop.Text = "" Then
                cmd.Parameters.AddWithValue("@InpAmountTop", "0")
            Else
                cmd.Parameters.AddWithValue("@InpAmountTop", txtInpAmountTop.Text)
            End If
            If txtInpAmountTop.Text = "" Then
                cmd.Parameters.AddWithValue("@InpWordsLeft", "0")
            Else
                cmd.Parameters.AddWithValue("@InpWordsLeft", txtInpWordsLeft.Text)
            End If


            If txtInpWordsTop.Text = "" Then
                cmd.Parameters.AddWithValue("@InpWordsTop", "0")
            Else
                cmd.Parameters.AddWithValue("@InpWordsTop", txtInpWordsTop.Text)
            End If

            cmd.Parameters.AddWithValue("@RPTNameLeft", txtNameLeft.Text)
            cmd.Parameters.AddWithValue("@RPTNameTop", txtNameTop.Text)
            cmd.Parameters.AddWithValue("@RPTAmountLeft", txtAmtLeft.Text)
            cmd.Parameters.AddWithValue("@RPTAmountTop", txtAmtTop.Text)
            cmd.Parameters.AddWithValue("@RPTDateLeft", txtDateLeft.Text)
            cmd.Parameters.AddWithValue("@RPTDateTop", txtDateTop.Text)
            cmd.Parameters.AddWithValue("@RPTWordsLeft", txtInWordLeft.Text)
            cmd.Parameters.AddWithValue("@RPTWordsTop", txtInWordTop.Text)
            cmd.Parameters.AddWithValue("@ChqWidth", txtWidth.Text)
            cmd.Parameters.AddWithValue("@ChqHeight", txtHeight.Text)

            cmd.ExecuteNonQuery()

            MsgBox(My.Resources.Message.Bank_Save)
            cnn.Close()

            db.Bank.NewRecord()
            NewForm()
        End If

    End Sub

    Sub DeleteForm()
        If tb.State = "New" Then Exit Sub
        If MsgDialog.ShowMsgDlg(My.Resources.Message.Bank_Delete_Record, Me.Text, "Q") <> Windows.Forms.DialogResult.Yes Then Exit Sub
        Dim str As String
        str = tb.DeleteRecord()
        MsgDialog.ShowMsgDlg(str, My.Resources.Message.Bank_Delete, "I")
        NewForm()
    End Sub

    Private Sub btnSearch1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch1.Click
        'FormShowView(frmBankInfoSearch)
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim od As New OpenFileDialog
        If od.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFilePath.Text = od.FileName
            txtFileName.Text = System.IO.Path.GetFileNameWithoutExtension(od.FileName)
            imgChequePreview.Image = New Bitmap(od.FileName)



            imgChequePreview.SizeMode = PictureBoxSizeMode.StretchImage
            'Me.PictureBox1.ImageLocation = txtFilePath.Text
            imgChequePreview.Tag = ImageToStream(txtFilePath.Text)
            txtWidth.Text = Math.Round(Val(imgChequePreview.Image.Width / imgChequePreview.Image.HorizontalResolution) * oneInch2MM)
            txtHeight.Text = Math.Round(Val(imgChequePreview.Image.Height / imgChequePreview.Image.VerticalResolution) * oneInch2MM)
            imgChequePreview.Width = ((Val(txtWidth.Text) / oneInch2MM) * 96) * (96 / 72)
            imgChequePreview.Height = ((Val(txtHeight.Text) / oneInch2MM) * 96) * (96 / 72)
        End If
    End Sub

    Private Sub frmMain_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlForm.Resize
        Dim l, t As Integer
        l = (pnlForm.Width - Panel2.Width) / 2
        If l < 0 Then l = 0
        t = (pnlForm.Height - Panel2.Height) / 2
        If t < 0 Then t = 0
        Panel2.Left = l
    End Sub


    Private Sub CTRL_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblDragDate.MouseDown, lblDragName.MouseDown, lblDragAmountWord.MouseDown, lblDragAmount.MouseDown
        IsDrag = True
        DateX = e.X
        DateY = e.Y
        sender.BorderStyle = BorderStyle.FixedSingle
        panelHorizantal.Visible = True
        panelVertical.Visible = True
    End Sub

    Private Sub CTRL_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblDragDate.MouseMove, lblDragName.MouseMove, lblDragAmountWord.MouseMove, lblDragAmount.MouseMove
        If IsDrag Then
            Dim L, T As Integer
            L = sender.Left + e.X - DateX
            T = sender.Top + e.Y - DateY
            If imgChequePreview.Left < L Then sender.Left = L
            If imgChequePreview.Top < T Then sender.Top = T
            panelHorizantal.Left = L
            panelVertical.Top = T - imgChequePreview.Top
            sender.BringToFront()
            If sender.Name = lblDragDate.Name Then
                txtDateLeft.Text = Math.Round((sender.Left - imgChequePreview.Left) / (96 / 72) / oneMM2Pixel)
                txtDateTop.Text = Math.Round((sender.Top - imgChequePreview.Top) / (96 / 72) / oneMM2Pixel)
            ElseIf sender.Name = lblDragName.Name Then
                txtNameLeft.Text = Math.Round((sender.Left - imgChequePreview.Left) / (96 / 72) / oneMM2Pixel)
                txtNameTop.Text = Math.Round((sender.Top - imgChequePreview.Top) / (96 / 72) / oneMM2Pixel)
            ElseIf sender.Name = lblDragAmount.Name Then
                txtAmtLeft.Text = Math.Round((sender.Left - imgChequePreview.Left) / (96 / 72) / oneMM2Pixel)
                txtAmtTop.Text = Math.Round((sender.Top - imgChequePreview.Top) / (96 / 72) / oneMM2Pixel)
            ElseIf sender.Name = lblDragAmountWord.Name Then
                txtInWordLeft.Text = Math.Round((sender.Left - imgChequePreview.Left) / (96 / 72) / oneMM2Pixel)
                txtInWordTop.Text = Math.Round((sender.Top - imgChequePreview.Top) / (96 / 72) / oneMM2Pixel)
            End If
        End If

    End Sub

    Private Sub CTRL_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblDragDate.MouseUp, lblDragName.MouseUp, lblDragAmountWord.MouseUp, lblDragAmount.MouseUp
        IsDrag = False
        sender.BorderStyle = BorderStyle.None
        panelHorizantal.Visible = False
        panelVertical.Visible = False
    End Sub

    Private Sub txtNameLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameLeft.TextChanged
        lblDragName.Left = imgChequePreview.Left + Math.Round(Val(txtNameLeft.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtNameTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameTop.TextChanged
        lblDragName.Top = imgChequePreview.Top + Math.Round(Val(txtNameTop.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtDateLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDateLeft.TextChanged
        lblDragDate.Left = imgChequePreview.Left + Math.Round(Val(txtDateLeft.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtDateTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDateTop.TextChanged
        lblDragDate.Top = imgChequePreview.Top + Math.Round(Val(txtDateTop.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtAmtLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtLeft.TextChanged
        lblDragAmount.Left = imgChequePreview.Left + Math.Round(Val(txtAmtLeft.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtAmtTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtTop.TextChanged
        lblDragAmount.Top = imgChequePreview.Top + Math.Round(Val(txtAmtTop.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtInWordLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInWordLeft.TextChanged
        lblDragAmountWord.Left = imgChequePreview.Left + Math.Round(Val(txtInWordLeft.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub txtInWordTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInWordTop.TextChanged
        lblDragAmountWord.Top = imgChequePreview.Top + Math.Round(Val(txtInWordTop.Text) * (96 / 72) * oneMM2Pixel)
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        defaultChequeValue()
    End Sub

    Private Sub txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTop.KeyPress, txtNameTop.KeyPress, txtNameLeft.KeyPress, txtLeft.KeyPress, txtInWordTop.KeyPress, txtInWordLeft.KeyPress, txtDateTop.KeyPress, txtDateLeft.KeyPress, txtAmtTop.KeyPress, txtAmtLeft.KeyPress
        If (Asc(e.KeyChar) = 124 Or Asc(e.KeyChar) = 36 Or Asc(e.KeyChar) = 94 Or Char.IsPunctuation(e.KeyChar) = True Or Char.IsLetter(e.KeyChar) = True) And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLeft.Leave
        w = Val(txtWidth.Text)
        If w < Val(txtLeft.Text) Then
            MsgBox(" 0 to " + w + " only allowed")
            sender.focus()
        End If
    End Sub

    Private Sub txtNameLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNameLeft.Leave
        w = Val(txtWidth.Text)
        If w < Val(txtNameLeft.Text) Then
            MsgBox(" 0 to " + w + " only allowed")
            sender.focus()
        End If
    End Sub

    Private Sub txtDateLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDateLeft.Leave
        w = Val(txtWidth.Text)
        If w < Val(txtDateLeft.Text) Then
            MsgBox(" 0 to " + w + " only allowed")
            sender.focus()
        End If
    End Sub

    Private Sub txtAmtLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtLeft.Leave
        w = Val(txtWidth.Text)
        If w < Val(txtAmtLeft.Text) Then
            MsgBox(" 0 to " + w + " only allowed")
            sender.focus()
        End If
    End Sub

    Private Sub txtInWordLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInWordLeft.Leave
        w = Val(txtWidth.Text)
        If w < Val(txtInWordLeft.Text) Then
            MsgBox(" 0 to " + w + " only allowed")
            sender.focus()
        End If
    End Sub

    Private Sub txtTop_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTop.KeyUp
        h = Val(txtHeight.Text)
        If h < Val(txtTop.Text) Then
            MsgBox(" 0 to " + h + " only allowed")
            sender.focus()
        End If
    End Sub

    Private Sub txtNameTop_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNameTop.KeyUp
        h = Val(txtHeight.Text)
        If h < Val(txtNameTop.Text) Then
            MsgBox(" 0 to " + h + " only allowed")
            sender.focus()
        End If
    End Sub

    Private Sub txtDateTop_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDateTop.KeyUp
        h = Val(txtHeight.Text)
        If h < Val(txtDateTop.Text) Then
            MsgBox(" 0 to " + h + " only allowed")
            sender.focus()
        End If
    End Sub

    Private Sub txtAmtTop_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmtTop.KeyUp
        h = Val(txtHeight.Text)
        If h < Val(txtAmtTop.Text) Then
            MsgBox(" 0 to " + h + " only allowed")
            sender.focus()
        End If
    End Sub

    Private Sub txtInWordTop_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInWordTop.KeyUp
        h = Val(txtHeight.Text)
        If h < Val(txtInWordTop.Text) Then
            MsgBox(" 0 to " + h + " only allowed")
            sender.focus()
        End If
    End Sub

End Class