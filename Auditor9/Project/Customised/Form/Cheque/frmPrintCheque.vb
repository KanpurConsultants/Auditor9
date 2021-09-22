Imports System.IO
Imports System.Data.SqlClient

Imports System.Drawing.Printing

Public Class frmPrintCheque
    Dim LastBankPrinted As String = ""
    Dim applocation, cursorlocation As New Point(0, 0)
    Private Sub DoWork()
        applocation = Me.Location
        cursorlocation = Cursor.Position
    End Sub

    Private Sub frmPrintCheque_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtChqAmount.Text = txtChqAmount.Text
        Me.Icon = My.Resources.plain

    End Sub

    Private Sub frmMainNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetFormStyle(Me)

        dgvChequeIssue.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgvChequeIssue.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2

        db.PrintData.SetControls(txtVoucherNo, txtBayerName, txtAmount, dtpDate, txtBankName, txtCompanyName, dtpIssueDate, txtChequeNo, cmbChequeStatus, ckbdate, ckbPayeename, ckbamount, txtVoucherNoNew, txtParticulars)
        db.PrintData.NewRecord()
        NewForm()
        txtCompanyName.Text = db.CompanyDetails.IdByCode(CompName)
        pnDate.Parent = imgChequePreview
        lbChqAmountInWord1.Parent = imgChequePreview
        lbChqAmountInWord2.Parent = imgChequePreview
        LoadRotate()

        Dim tooltip As New ToolTip()
        tooltip.ShowAlways = True
        tooltip.SetToolTip(Me.ckbIsRotate90Degree, "It's applicable only for Printing Option.")
        tooltip.SetToolTip(Me.GroupBox1, "It's applicable only for Printing Option.")
        lbChqAmountInWord1.Text = ""
        List_Datas(txtBankName, db.Bank.Codes())
        List_Datas(txtPayeeName, db.Payee.Column("PayeeName", String.Format("CompanyID='{0}'", db.CompanyDetails.IdByCode(CompName))))

    End Sub

    Private Sub frmMain_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        pnlContent.Left = (Me.Width - pnlContent.Width) / 2
        'pnlContent.Top = (Me.Height - pnlContent.Height) / 2
    End Sub

    Private Sub frm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlForm.Resize
        Dim l, t As Integer
        l = (pnlForm.Width - Panel1.Width) / 2
        If l < 0 Then l = 0
        t = (pnlForm.Height - Panel1.Height) / 2
        If t < 0 Then t = 0
        Panel1.Left = l

    End Sub

    Private Sub CTRL_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtChequeNo.Enter, cmbChequeStatus.Enter

        List_Datas(txtPayeeName, db.Payee.Column("PayeeName", String.Format("CompanyID='{0}'", db.CompanyDetails.IdByCode(CompName))))

        sender.Backcolor = FColor
        sender.Forecolor = BColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbEnterHeight
    End Sub

    Private Sub CTRL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChequeNo.Leave, cmbChequeStatus.Leave
        sender.Backcolor = BColor
        sender.Forecolor = FColor
        If sender.Name.ToLower.StartsWith("cmb") Then sender.Height = cmbLeaveHeight
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
            If MsgDialog.ShowMsgDlg("Do you want to Exit?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then Me.Close()
        End If

    End Sub

    Sub PrintCheque()

        PrintDialog1.AllowSomePages = True
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings

        If PrintDialog1.ShowDialog = DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings

            Dim dv As New DataView
            dv = db.Bank.Grid("LeftMargin,TopMargin,ChqWidth,ChqHeight", String.Format("BankName='{0}'", txtBankName.Text))
            LastBankPrinted = txtBankName.Text
            Dim PageSetup As New PageSettings
            With PageSetup
                .Margins.Left = Val(UnitConversion(dv.Item(0)("LeftMargin").ToString(), Units.MilliMeter, Units.Pixel))
                .Margins.Top = Val(UnitConversion(dv.Item(0)("TopMargin").ToString(), Units.MilliMeter, Units.Pixel))
                .Margins.Right = 0
                .Margins.Bottom = 0
            End With
            Dim xDPI, yDPI As Integer

            PrintDocument1.DefaultPageSettings = PageSetup
            PrintDocument1.DefaultPageSettings.PaperSize = New PaperSize("ChequeSize", Val(UnitConversion(dv.Item(0)("ChqWidth").ToString(), Units.MilliMeter, Units.Pixel)), Val(UnitConversion(dv.Item(0)("ChqHeight").ToString(), Units.MilliMeter, Units.Pixel)))
            'PrintDocument1.DefaultPageSettings.PaperSize = New PaperSize("ChequeSize", Val(UnitConversion("210", Units.MilliMeter, Units.Pixel)), Val(UnitConversion("297", Units.MilliMeter, Units.Pixel)))
            PrintDocument1.DefaultPageSettings.Landscape = False
            PrintPreviewDialog1.Document = PrintDocument1
            'PrintPreviewDialog1.ShowDialog()
            PrintDocument1.Print()
        End If

    End Sub

    Sub Printvoucher(ByVal VoucherNo As String)
        PrintDialog1.AllowSomePages = True
        If PrintDialog1.ShowDialog = DialogResult.OK Then

        End If
        Dim ds As New DataSet
        Dim DTPrint As New DataTable()
        Dim DTCompany As New DataTable()

        'DTPrint = db.PrintData.Grid("*", String.Format("VoucherNo='{0}' and CompanyName = '{1}'", VoucherNo, db.CompanyDetails.IdByCode(CompName))).ToTable("PrintData")
        Dim dv As New DataView()
        'dv = db.ExecuteQuery(String.Format("SELECT 'OFFICE COPY' AS SNO, VoucherNo, PayeeName, Amount, ChequeDate, BankName, CompanyName, IssueDate, ChequeNo, ChequeStatus, DisplayDate, DisplayPayeename, Displayamount, VoucherNewNo, Particulars FROM            PrintData where VoucherNo='{0}' and CompanyName = '{1}' UNION SELECT        'CUSTOMER COPY' AS SNO, VoucherNo, PayeeName, Amount, ChequeDate, BankName, CompanyName, IssueDate, ChequeNo, ChequeStatus, DisplayDate, DisplayPayeename, Displayamount, VoucherNewNo, Particulars FROM            PrintData AS PrintData_1 where VoucherNo='{0}' and CompanyName = '{1}'", VoucherNo, db.CompanyDetails.IdByCode(CompName)))
        dv = db.ExecuteQuery(String.Format("SELECT 'OFFICE COPY' AS SNO, VoucherNo, PayeeName, Amount, ChequeDate, BankName, CompanyName, IssueDate, ChequeNo, ChequeStatus, DisplayDate, DisplayPayeename, Displayamount, VoucherNewNo, Particulars FROM            PrintData where VoucherNo='{0}' and CompanyName = '{1}' ", VoucherNo, db.CompanyDetails.IdByCode(CompName)))
        DTPrint = dv.ToTable("PrintData")

        DTCompany = db.CompanyDetails.Grid("*", String.Format("CompanyName='{0}'", CompName)).ToTable("CompanyDetails")
        ds.Clear()

        ds.Tables.Add(DTPrint)
        ds.Tables.Add(DTCompany)

     

        ReportPrint("cryVoucher.rpt", ds, Nothing, PrintDialog1.PrinterSettings.PrinterName)
    End Sub

    Private Sub brnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brnPrint.Click
        txtBayerName.Text = txtChqName.Text
        txtAmount.Text = txtChqAmount.Text
        txtInputWords.Text = lbChqAmountInWord1.Text

        If txtChqAmount.Text.Trim() = "" Then
            MsgBox("Enter Amount")
            txtChqAmount.Focus()
        ElseIf txtChequeNo.Text.Trim() = "" Or txtChequeNo.Text.Length < 6 Then
            MsgBox("Please Enter The Valid Cheque Number")
            txtChequeNo.Focus()
        ElseIf db.PrintData.Data("VoucherNo", String.Format("ChequeNo='{0}' and BankName='{1}' and CompanyName='{2}'", txtChequeNo.Text, txtBankName.Text, db.CompanyDetails.IdByCode(CompName))) <> "" Then
            MsgBox("Cheque Number is already used")
            txtChequeNo.Focus()
        Else

            If DateDiff(DateInterval.Month, Now, dtpDate.Value) > 6 Then
                If MsgBox(String.Format("Seems To Be The Date After Six Month!!!{0}Are You Wish To Proceed?", vbCrLf), MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    MonthCalendar1.Focus()
                    Exit Sub
                End If
            End If

            PrintCheque()
            If MsgDialog.ShowMsgDlg("Do you Save this Record?", Me.Text, "Q") = Windows.Forms.DialogResult.Yes Then
                db.PrintData.SaveRecord()
            End If
            NewForm()
        End If
        lbChqAmountInWord1.Text = ""
    End Sub

    Sub NewForm()
        db.PrintData.NewRecord()
        txtCompanyName.Text = db.CompanyDetails.IdByCode(CompName)
        txtBankName.Text = LastBankPrinted
        txtPayeeName.Text = ""

        Dim DV As New DataView
        DV = db.PrintData.Grid("VoucherNo,BankName,Particulars,PayeeName,ChequeDate,IssueDate,ChequeStatus,Amount", String.Format("CompanyName='{0}'", db.CompanyDetails.IdByCode(CompName)), "VoucherNo desc")
        dgvChequeIssue.Rows.Clear()
        For i As Integer = 0 To DV.Count - 1
            dgvChequeIssue.Rows.Add()
            dgvChequeIssue.Item("VoucherNo", i).Value = DV.Item(i)("VoucherNo").ToString()
            dgvChequeIssue.Item("Particulars", i).Value = DV.Item(i)("Particulars").ToString()
            dgvChequeIssue.Item("BankName", i).Value = DV.Item(i)("BankName").ToString()
            dgvChequeIssue.Item("PayeeName", i).Value = DV.Item(i)("PayeeName").ToString()
            dgvChequeIssue.Item("ChequeDate", i).Value = DV.Item(i)("ChequeDate")
            dgvChequeIssue.Item("IssueDate", i).Value = DV.Item(i)("IssueDate")
            dgvChequeIssue.Item("ChequeStatus", i).Value = DV.Item(i)("ChequeStatus")
            dgvChequeIssue.Item("Amount", i).Value = DV.Item(i)("Amount")
            dgvChequeIssue.Item("btnVoucher", i).Value = "Print"
        Next
        dgvChequeIssue.Columns("IssueDate").DefaultCellStyle.Format = "dd/MM/yyyy"
        dgvChequeIssue.Columns("ChequeDate").DefaultCellStyle.Format = "dd/MM/yyyy"
        ckbdate.Checked = True
        ckbPayeename.Checked = True
        ckbamount.Checked = True


        Dim VNO As String = db.PrintData.Data("isNull(Max(VoucherNo),0) + 1", String.Format("CompanyName='{0}'", db.CompanyDetails.IdByCode(CompName)))
        'If VNO = "1" Then
        '    VNO = db.CompanyDetails.Data("VoucherNo", String.Format("CompanyId='{0}'", db.CompanyDetails.IdByCode(CompName)))
        'End If
        txtVoucherNoNew.Text = VNO

        
    End Sub

    Private Sub BankName_Selected(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBankName.Leave
        txtChqAmount.Text = ""
        lbChqAmountInWord1.Text = ""
        lbChqAmountInWord2.Text = ""
        txtChqName.Text = ""

        txtChqD1.Text = ""
        txtChqD2.Text = ""
        txtChqM1.Text = ""
        txtChqM2.Text = ""
        txtChqY1.Text = ""
        txtChqY2.Text = ""

        txtInputWords.Text = ""
        pnlImage.BackgroundImage = Nothing

        Dim conn As New SqlConnection
        If conn.State = ConnectionState.Closed Then
            conn.ConnectionString = GetConStr()
        End If
        'Connect()
        Try
            conn.Open()
            Dim sqlquery As String = "SELECT * FROM Bank Where BankName = @BankName"
            Dim data As SqlDataReader
            Dim adapter As New SqlDataAdapter
            Dim parameter As New SqlParameter
            Dim command As SqlCommand = New SqlCommand(sqlquery, conn)
            With command.Parameters
                .Add(New SqlParameter("@BankName", txtBankName.Text))
            End With
            command.Connection = conn
            adapter.SelectCommand = command
            data = command.ExecuteReader()
            While data.Read
                If data.HasRows = True Then
                    Dim img_buffer() As Byte
                    img_buffer = CType(data("BankImg"), Byte())
                    Dim img_stream As New MemoryStream(img_buffer, True)
                    img_stream.Write(img_buffer, 0, img_buffer.Length)
                    imgChequePreview.Image = New Bitmap(img_stream)
                    imgChequePreview.Width = UnitConversion((imgChequePreview.Image.Width / imgChequePreview.Image.HorizontalResolution), Units.Inch, Units.Pixel)
                    imgChequePreview.Height = UnitConversion((imgChequePreview.Image.Height / imgChequePreview.Image.VerticalResolution), Units.Inch, Units.Pixel)
                    imgChequePreview.SizeMode = PictureBoxSizeMode.StretchImage

                    img_stream.Close()

                End If
            End While

            Dim dv As New DataView
            dv = db.Bank.Grid("RPTNameLeft,RPTNameTop,RPTAmountLeft,RPTAmountTop,RPTDateLeft,RPTDateTop,RPTWordsLeft,RPTWordsTop", String.Format("BankName='{0}'", txtBankName.Text))

            txtChqName.Left = Math.Round(Val(UnitConversion(dv.Item(0)("RPTNameLeft").ToString(), Units.MilliMeter, Units.Pixel)))
            txtChqName.Top = Math.Round(Val(UnitConversion(dv.Item(0)("RPTNameTop").ToString(), Units.MilliMeter, Units.Pixel)))
            txtChqAmount.Left = Math.Round(Val(UnitConversion(dv.Item(0)("RPTAmountLeft").ToString(), Units.MilliMeter, Units.Pixel)))
            txtChqAmount.Top = Math.Round(Val(UnitConversion(dv.Item(0)("RPTAmountTop").ToString(), Units.MilliMeter, Units.Pixel)))
            pnDate.Left = Math.Round(Val(UnitConversion(dv.Item(0)("RPTDateLeft").ToString(), Units.MilliMeter, Units.Pixel)))
            pnDate.Top = Math.Round(Val(UnitConversion(dv.Item(0)("RPTDateTop").ToString(), Units.MilliMeter, Units.Pixel)))

            lbChqAmountInWord1.Left = Math.Round(Val(UnitConversion(dv.Item(0)("RPTWordsLeft").ToString(), Units.MilliMeter, Units.Pixel)))
            lbChqAmountInWord1.Top = Math.Round(Val(UnitConversion(dv.Item(0)("RPTWordsTop").ToString(), Units.MilliMeter, Units.Pixel)))

            lbChqAmountInWord2.Left = Math.Round(Val(UnitConversion(dv.Item(0)("RPTWordsLeft").ToString(), Units.MilliMeter, Units.Pixel))) - 30
            lbChqAmountInWord2.Top = Math.Round(Val(UnitConversion(dv.Item(0)("RPTWordsTop").ToString(), Units.MilliMeter, Units.Pixel))) + 30

            txtChqName.Visible = True
            txtChqAmount.Visible = True
            lbChqAmountInWord1.Visible = True

            txtChqD1.Visible = True
            txtChqD2.Visible = True
            txtChqM1.Visible = True
            txtChqM2.Visible = True
            txtChqY1.Visible = True
            txtChqY2.Visible = True

        Catch ex As Exception

        End Try
        SetChequeNo()
    End Sub

    Sub SetChequeNo()
        Dim no As String = db.PrintData.Data("isnull(max(ChequeNo),0)", String.Format("BankName='{0}' and CompanyName='{1}'", txtBankName.Text, db.CompanyDetails.IdByCode(CompName)))
        Dim CNo As Double = Val(no) + 1
        txtChequeNo.Text = String.Format("{0:000000}", CNo)
    End Sub
    Private Sub txtChqAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChqAmount.KeyDown
        Select Case e.KeyCode
            Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9, _
                    Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                    Keys.Left, Keys.Right
                If e.Shift = True Then
                    e.SuppressKeyPress = True
                    Exit Sub
                End If
                e.SuppressKeyPress = False
            Case Else
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub txtInpAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtChqAmount.TextChanged
        lbChqAmountInWord1.Text = Number_to_Word.ConvertCurrencyToEnglish(txtChqAmount.Text)
        lbChqAmountInWord1.Text = "** " + lbChqAmountInWord1.Text.ToUpper() + " **"
        Dim NoOfChar As Integer = 45
        Dim Str1, Str2 As String
        lbChqAmountInWord2.Text = ""
        If lbChqAmountInWord1.Text.Length > NoOfChar Then
            Str1 = lbChqAmountInWord1.Text.Substring(0, Mid(lbChqAmountInWord1.Text, 1, NoOfChar).LastIndexOf(" "))
            Str2 = lbChqAmountInWord1.Text.Substring(Mid(lbChqAmountInWord1.Text, 1, NoOfChar).LastIndexOf(" "))
            lbChqAmountInWord1.Text = Str1.ToUpper()
            lbChqAmountInWord2.Text = Str2.ToUpper()
        End If
    End Sub

    Private Sub MonthCalendar1_DateSelected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        dtpDate.Value = MonthCalendar1.SelectionRange.Start
        txtChqD1.Text = dtpDate.Value.Day \ 10
        txtChqD2.Text = Val(dtpDate.Value.Day) Mod 10
        txtChqM1.Text = dtpDate.Value.Month \ 10
        txtChqM2.Text = Val(dtpDate.Value.Month) Mod 10
        txtChqY1.Text = (dtpDate.Value.Year Mod 100) \ 10
        txtChqY2.Text = Val(dtpDate.Value.Year) Mod 10

    End Sub

    Private Sub PayeeName_SelectedInd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPayeeName.TextChanged

        Try
            txtChqName.Text = txtPayeeName.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvChequeIssue_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvChequeIssue.CellClick
        Try
            If dgvChequeIssue.Columns(e.ColumnIndex).Name = "btnVoucher" And e.RowIndex >= 0 Then
                Dim VoucherNo As String = dgvChequeIssue.Item("VoucherNo", e.RowIndex).Value
                Dim PayFor As String = dgvChequeIssue.Item("Particulars", e.RowIndex).Value
                If PayFor.Trim() = "" Then
                    MsgBox("Please Enter the Pay For Details...")

                    dgvChequeIssue.Item("Particulars", e.RowIndex).Selected = True
                    Exit Sub
                End If

                Dim ChequeStatus As String = dgvChequeIssue.Item("ChequeStatus", e.RowIndex).Value
                Dim Particulars As String = dgvChequeIssue.Item("Particulars", e.RowIndex).Value
                Dim Qry As String = String.Format("update printdata set ChequeStatus = '{0}',Particulars='{1}' where VoucherNo='{2}'", ChequeStatus, Particulars.Replace("'", "''"), VoucherNo)
                db.ExecuteQuery(Qry)

                Printvoucher(VoucherNo)
            End If
            'If dgvChequeIssue.Columns(e.ColumnIndex).Name = "ChequeStatus" And e.RowIndex >= 0 Then
            '    Dim VoucherNo As String = dgvChequeIssue.Item("VoucherNo", e.RowIndex).Value
            '    Dim ChequeStatus As String = dgvChequeIssue.Item("ChequeStatus", e.RowIndex).Value
            '    Dim Qry As String = String.Format("update printdata set ChequeStatus = '{0}' where VoucherNo='{1}'", ChequeStatus, VoucherNo)
            '    db.ExecuteQuery(Qry)
            'End If
        Catch ex As Exception

        End Try
       
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        SendKeys.Send("{ESCAPE}")
    End Sub

    Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint

    End Sub

    Private Sub PrintDocument1_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.EndPrint

    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        Dim PrintPage As System.Drawing.Graphics = e.Graphics
        Dim fnt As New Font("Arial", 12, FontStyle.Regular)
        Dim PrintBrush As System.Drawing.Brush
        PrintBrush = Brushes.DarkRed

        Dim dv As New DataView
        dv = db.Bank.Grid("ChqWidth,ChqHeight,RPTNameLeft,RPTNameTop,RPTAmountLeft,RPTAmountTop,RPTDateLeft,RPTDateTop,RPTWordsLeft,RPTWordsTop", String.Format("BankName='{0}'", txtBankName.Text))
        Dim dd1Left, dd1Top, dd2Left, dd2Top, mm1Left, mm1Top, mm2Left, mm2Top, yy1Left, yy1Top, yy2Left, yy2Top As Integer
        Dim ddSpace As Integer = 30

        

        dd1Left = Val(UnitConversion(dv.Item(0)("RPTDateLeft").ToString(), Units.MilliMeter, Units.Pixel))
        dd2Left = dd1Left + (ddSpace * 1)
        mm1Left = dd1Left + (ddSpace * 2)
        mm2Left = dd1Left + (ddSpace * 3)
        yy1Left = dd1Left + (ddSpace * 4)
        yy2Left = dd1Left + (ddSpace * 5)

        dd1Top = Val(UnitConversion(dv.Item(0)("RPTDateTop").ToString(), Units.MilliMeter, Units.Pixel))
        dd2Top = dd1Top
        mm1Top = dd1Top
        mm2Top = dd1Top
        yy1Top = dd1Top
        yy2Top = dd1Top


        Dim nameLeft, nameTop, AmountLeft, AmountTop, AmountInWordLeft, AmountInWordTop As Integer

        nameLeft = Val(UnitConversion(dv.Item(0)("RPTNameLeft").ToString(), Units.MilliMeter, Units.Pixel))
        nameTop = Val(UnitConversion(dv.Item(0)("RPTNameTop").ToString(), Units.MilliMeter, Units.Pixel))

        AmountLeft = Val(UnitConversion(dv.Item(0)("RPTAmountLeft").ToString(), Units.MilliMeter, Units.Pixel))
        AmountTop = Val(UnitConversion(dv.Item(0)("RPTAmountTop").ToString(), Units.MilliMeter, Units.Pixel))

        AmountInWordLeft = Val(UnitConversion(dv.Item(0)("RPTWordsLeft").ToString(), Units.MilliMeter, Units.Pixel))
        AmountInWordTop = Val(UnitConversion(dv.Item(0)("RPTWordsTop").ToString(), Units.MilliMeter, Units.Pixel))


        With PrintPage
            If Not ckbIsRotate90Degree.Checked Then
                .RotateTransform(Val(TextBox1.Text))
                .TranslateTransform(Val(TextBox2.Text), Val(TextBox3.Text))
            End If
            '.DrawRectangle(Pens.Red, 0, 0, CInt(Val(UnitConversion(dv.Item(0)("ChqWidth").ToString(), Units.MilliMeter, Units.Pixel))), CInt(Val(UnitConversion(dv.Item(0)("ChqHeight").ToString(), Units.MilliMeter, Units.Pixel))))
            If ckbdate.Checked = True Then
                .DrawString(txtChqD1.Text, txtChqD1.Font, New System.Drawing.SolidBrush(txtChqD1.ForeColor), dd1Left, dd1Top)
                .DrawString(txtChqD2.Text, txtChqD2.Font, New System.Drawing.SolidBrush(txtChqD2.ForeColor), dd2Left, dd2Top)
                .DrawString(txtChqM1.Text, txtChqM1.Font, New System.Drawing.SolidBrush(txtChqM1.ForeColor), mm1Left, mm1Top)
                .DrawString(txtChqM2.Text, txtChqM2.Font, New System.Drawing.SolidBrush(txtChqM2.ForeColor), mm2Left, mm2Top)
                .DrawString(txtChqY1.Text, txtChqY1.Font, New System.Drawing.SolidBrush(txtChqY1.ForeColor), yy1Left, yy1Top)
                .DrawString(txtChqY2.Text, txtChqY2.Font, New System.Drawing.SolidBrush(txtChqY2.ForeColor), yy2Left, yy2Top)
            End If

            If ckbamount.Checked = True Then
                .DrawString(lbChqAmountInWord1.Text, lbChqAmountInWord1.Font, New System.Drawing.SolidBrush(lbChqAmountInWord1.ForeColor), AmountInWordLeft, AmountInWordTop)
                .DrawString(lbChqAmountInWord2.Text, lbChqAmountInWord2.Font, New System.Drawing.SolidBrush(lbChqAmountInWord2.ForeColor), AmountInWordLeft, AmountInWordTop + 30)

                .DrawString("** " + txtChqAmount.Text + " **", txtChqAmount.Font, New System.Drawing.SolidBrush(txtChqAmount.ForeColor), AmountLeft, AmountTop)
            End If

            If ckbPayeename.Checked = True Then
                .DrawString(txtChqName.Text, txtChqName.Font, New System.Drawing.SolidBrush(txtChqName.ForeColor), nameLeft, nameTop)
            End If

        End With
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtChqD1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChqD1.KeyDown
        Select Case e.KeyCode
            Case Keys.D0 To Keys.D3, Keys.NumPad0 To Keys.NumPad3, _
                    Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                    Keys.Left, Keys.Right
                If Keys.D0 < Keys.D3 Or Keys.NumPad0 < Keys.NumPad3 Then
                    txtChqD2.Focus()
                Else
                    txtChqD1.Clear()
                End If
                If e.Shift = True Then
                    e.SuppressKeyPress = True
                    Exit Sub
                End If
                e.SuppressKeyPress = False
            Case Else
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub txtChqD1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqD1.KeyPress
        If Asc(e.KeyChar) = 8 Then
            txtChqD1.Clear()
            txtChqD1.Focus()
        End If
    End Sub

    Private Sub txtChqD2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChqD2.KeyDown
        If Val(txtChqD1.Text) = 3 Or txtChqD1.Text = "" Then
            Select Case e.KeyCode
                Case Keys.D0 To Keys.D1, Keys.NumPad0 To Keys.NumPad1, _
                        Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                        Keys.Left, Keys.Right

                    If Keys.D0 < Keys.D1 Or Keys.NumPad0 < Keys.NumPad1 Then
                        txtChqM1.Focus()
                    Else
                        MsgBox("Invalid Date")
                        txtChqD2.Focus()
                        txtChqD2.Clear()
                    End If
                    If e.Shift = True Then
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    e.SuppressKeyPress = False
                Case Else
                    e.SuppressKeyPress = True
            End Select
        ElseIf Val(txtChqD1.Text) = 0 Then

            Select Case e.KeyCode
                Case Keys.D1 To Keys.D9, Keys.NumPad1 To Keys.NumPad9, _
                        Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                        Keys.Left, Keys.Right

                    If Keys.D1 < Keys.D9 Or Keys.NumPad1 < Keys.NumPad9 Then
                        txtChqM1.Focus()
                    Else
                        MsgBox("Invalid Month")
                        txtChqD2.Focus()
                        txtChqD2.Clear()
                    End If
                    If e.Shift = True Then
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    e.SuppressKeyPress = False
                Case Else
                    e.SuppressKeyPress = True
            End Select

        Else
            Select Case e.KeyCode
                Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9, _
                        Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                        Keys.Left, Keys.Right

                    If Keys.D0 < Keys.D9 Or Keys.NumPad0 < Keys.NumPad9 Then
                        txtChqM1.Focus()
                    Else
                        txtChqD2.Clear()
                    End If
                    If e.Shift = True Then
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    e.SuppressKeyPress = False
                Case Else
                    e.SuppressKeyPress = True
            End Select
        End If
    End Sub

    Private Sub txtChqD2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqD2.KeyPress
        If Asc(e.KeyChar) = 8 Then
            txtChqD2.Clear()
            txtChqD1.Focus()
        End If
    End Sub

    Private Sub txtChqM1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChqM1.KeyDown
        Select Case e.KeyCode
            Case Keys.D0 To Keys.D1, Keys.NumPad0 To Keys.NumPad1, _
                    Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                    Keys.Left, Keys.Right
                If Keys.D0 < Keys.D1 Or Keys.NumPad0 < Keys.NumPad1 Then
                    txtChqM2.Focus()
                Else
                    txtChqM1.Clear()
                End If
                If e.Shift = True Then
                    e.SuppressKeyPress = True
                    Exit Sub
                End If
                e.SuppressKeyPress = False
            Case Else
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub txtChqM1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqM1.KeyPress
        If Asc(e.KeyChar) = 8 Then
            txtChqM1.Clear()
            txtChqD2.Focus()
        End If
    End Sub

    Private Sub txtChqM2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChqM2.KeyDown

        If Val(txtChqM1.Text) = 1 Or txtChqM1.Text = "" Then
            Select Case e.KeyCode
                Case Keys.D0 To Keys.D2, Keys.NumPad0 To Keys.NumPad2, _
                        Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                        Keys.Left, Keys.Right

                    If Keys.D0 < Keys.D2 Or Keys.NumPad0 < Keys.NumPad2 Then
                        txtChqY1.Focus()
                    Else
                        MsgBox("Invalid Month")
                        txtChqM2.Focus()
                        txtChqM2.Clear()
                    End If
                    If e.Shift = True Then
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    e.SuppressKeyPress = False
                Case Else
                    e.SuppressKeyPress = True
            End Select
        ElseIf Val(txtChqM1.Text) = 0 Then

            Select Case e.KeyCode
                Case Keys.D1 To Keys.D9, Keys.NumPad1 To Keys.NumPad9, _
                        Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                        Keys.Left, Keys.Right

                    If Keys.D1 < Keys.D9 Or Keys.NumPad1 < Keys.NumPad9 Then
                        txtChqY1.Focus()
                    Else
                        MsgBox("Invalid Month")
                        txtChqM2.Focus()
                        txtChqM2.Clear()
                    End If
                    If e.Shift = True Then
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    e.SuppressKeyPress = False
                Case Else
                    e.SuppressKeyPress = True
            End Select

        Else
            Select Case e.KeyCode
                Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9, _
                        Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                        Keys.Left, Keys.Right

                    If Keys.D0 < Keys.D9 Or Keys.NumPad0 < Keys.NumPad9 Then
                        txtChqY1.Focus()
                    Else
                        txtChqM2.Clear()
                    End If
                    If e.Shift = True Then
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    e.SuppressKeyPress = False
                Case Else
                    e.SuppressKeyPress = True
            End Select
        End If
    End Sub

    Private Sub txtChqM2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqM2.KeyPress
        If Asc(e.KeyChar) = 8 Then
            txtChqM2.Clear()
            txtChqM1.Focus()
        End If
    End Sub

    Private Sub txtChqY1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChqY1.KeyDown
        Select Case e.KeyCode
            Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9, _
                    Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                    Keys.Left, Keys.Right
                If Keys.D0 < Keys.D9 Or Keys.NumPad0 < Keys.NumPad9 Then
                    txtChqY2.Focus()
                Else
                    txtChqY1.Clear()
                End If
                If e.Shift = True Then
                    e.SuppressKeyPress = True
                    Exit Sub
                End If
                e.SuppressKeyPress = False
            Case Else
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub txtChqY1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqY1.KeyPress
        If Asc(e.KeyChar) = 8 Then
            txtChqY1.Clear()
            txtChqM2.Focus()
        End If
    End Sub

    Private Sub txtChqY2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChqY2.KeyDown
        If Val(txtChqY1.Text) = 0 Or txtChqY1.Text = "" Then

            Select Case e.KeyCode
                Case Keys.D1 To Keys.D9, Keys.NumPad1 To Keys.NumPad9, _
                        Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                        Keys.Left, Keys.Right

                    If Keys.D1 < Keys.D9 Or Keys.NumPad1 < Keys.NumPad9 Then
                        txtChqAmount.Focus()
                    Else
                        MsgBox("Invalid Month")
                        txtChqY2.Focus()
                        txtChqY2.Clear()
                    End If
                    If e.Shift = True Then
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    e.SuppressKeyPress = False
                Case Else
                    e.SuppressKeyPress = True
            End Select

        Else
            Select Case e.KeyCode
                Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9, _
                        Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                        Keys.Left, Keys.Right
                    If txtChqY1.Text = 9 Then
                        Select Case e.KeyCode
                            Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9, _
                            Keys.OemPeriod, Keys.Decimal, Keys.Back, Keys.Delete, _
                        Keys.Left, Keys.Right
                                If e.Shift = True Then
                                    e.SuppressKeyPress = True
                                    Exit Sub
                                End If
                                e.SuppressKeyPress = False
                            Case Else
                                e.SuppressKeyPress = True
                        End Select
                    End If
                    If Keys.D0 < Keys.D9 Or Keys.NumPad0 < Keys.NumPad9 Then
                        txtChqAmount.Focus()
                    Else
                        txtChqY2.Clear()
                    End If
                    If e.Shift = True Then
                        e.SuppressKeyPress = True
                        Exit Sub
                    End If
                    e.SuppressKeyPress = False
                Case Else
                    e.SuppressKeyPress = True
            End Select
        End If
    End Sub

    Private Sub txtChqY2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqY2.KeyPress
        If Asc(e.KeyChar) = 8 Then
            txtChqY2.Clear()
            txtChqY1.Focus()
        End If
    End Sub

    Private Sub IsRotate()
        Dim conn As New SqlConnection
        If conn.State = ConnectionState.Closed Then
            conn.ConnectionString = GetConStr()
        End If

        Try
            conn.Open()
            Dim sqlquery As String = "Update CompanyDetails SET IsRotate90Degree = '1' WHERE CompanyName = '" + txtCompanyName.Text + "'"
            Dim data As SqlDataReader
            Dim adapter As New SqlDataAdapter
            Dim command As SqlCommand = New SqlCommand(sqlquery, conn)

            command.Connection = conn
            adapter.SelectCommand = command
            data = command.ExecuteReader()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadRotate()
        Dim notRotate As Boolean = False
        Dim conn As New SqlConnection
        If conn.State = ConnectionState.Closed Then
            conn.ConnectionString = GetConStr()
        End If

        Try
            conn.Open()
            Dim sqlquery As String = "select IsRotate90Degree from CompanyDetails WHERE CompanyName = '" + txtCompanyName.Text + "'"
            Dim data As SqlDataReader
            Dim adapter As New SqlDataAdapter
            Dim command As SqlCommand = New SqlCommand(sqlquery, conn)

            command.Connection = conn
            adapter.SelectCommand = command
            data = command.ExecuteReader()
            data.Read()

            If data.HasRows Then
                If data.GetValue(0) = False Then
                    ckbIsRotate90Degree.CheckState = CheckState.Unchecked
                Else
                    ckbIsRotate90Degree.CheckState = CheckState.Checked
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub IsNotRotate()
        Dim conn As New SqlConnection
        If conn.State = ConnectionState.Closed Then
            conn.ConnectionString = GetConStr()
        End If

        Try
            conn.Open()
            Dim sqlquery As String = "Update CompanyDetails SET IsRotate90Degree = '0' WHERE CompanyName = '" + txtCompanyName.Text + "'"
            Dim data As SqlDataReader
            Dim adapter As New SqlDataAdapter
            Dim command As SqlCommand = New SqlCommand(sqlquery, conn)

            command.Connection = conn
            adapter.SelectCommand = command
            data = command.ExecuteReader()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ckbIsRotate90Degree_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbIsRotate90Degree.CheckStateChanged
        If ckbIsRotate90Degree.CheckState = CheckState.Checked Then
            IsNotRotate()
        Else
            IsRotate()
        End If
    End Sub

    Private Sub frmPrintCheque_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        txtChqAmount.Text = txtChqAmount.Text
    End Sub

    Private Sub frmPrintCheque_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        txtChqAmount.Text = txtChqAmount.Text
    End Sub

    Private Sub cmbPayeeName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChqName.KeyDown, txtPayeeName.KeyDown

        SetChequeFontSize()

        If e.KeyCode = Keys.Enter Then
            txtChqAmount.Focus()
        End If
    End Sub

    Sub SetChequeFontSize()
        If txtChqName.TextLength < 30 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 33 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 36 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 39 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 41 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 43 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 45 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 7.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 48 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 52 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 55 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 58 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 5.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf txtChqName.TextLength < 61 Then
            txtChqName.Font = New System.Drawing.Font("Times New Roman", 5.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        End If
    End Sub

    Private Sub MonthCalendar1_DateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        MonthCalendar1.MinDate = Today.AddMonths(-6)
        MonthCalendar1.MaxDate = Today.AddYears(5)

        Dim YR As String = Today.Date

        ' MsgBox(YR.Substring(5, 2))
        'Dim NYR As Integer = Today.AddYears(1)
    End Sub

    Private Sub txtChequeNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChequeNo.KeyPress
        e.KeyChar = NumericCheckwithsingledot(sender, e.KeyChar, False)
    End Sub


    Private Sub ckbdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbdate.CheckedChanged

        If ckbdate.Checked = True Then
            txtChqD1.ForeColor = Color.Black
            txtChqD2.ForeColor = Color.Black
            txtChqM1.ForeColor = Color.Black
            txtChqM2.ForeColor = Color.Black
            txtChqY1.ForeColor = Color.Black
            txtChqY2.ForeColor = Color.Black
        Else
            txtChqD1.ForeColor = Color.Red
            txtChqD2.ForeColor = Color.Red
            txtChqM1.ForeColor = Color.Red
            txtChqM2.ForeColor = Color.Red
            txtChqY1.ForeColor = Color.Red
            txtChqY2.ForeColor = Color.Red

        End If
    End Sub

    Private Sub ckbPayeename_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbPayeename.CheckedChanged

        If ckbPayeename.Checked = True Then
            txtChqName.ForeColor = Color.Black
        Else
            txtChqName.ForeColor = Color.Red
        End If
    End Sub

    Private Sub ckbamount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbamount.CheckedChanged



        If ckbamount.Checked = True Then
            txtChqAmount.ForeColor = Color.Black
            lbChqAmountInWord1.ForeColor = Color.Black
            lbChqAmountInWord2.ForeColor = Color.Black
        Else
            txtChqAmount.ForeColor = Color.Red
            lbChqAmountInWord1.ForeColor = Color.Red
            lbChqAmountInWord2.ForeColor = Color.Red
        End If
        txtChqAmount.Text = String.Format("{0}", txtChqAmount.Text)
    End Sub

     
    Private Sub frmPrintCheque_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed

    End Sub

End Class
