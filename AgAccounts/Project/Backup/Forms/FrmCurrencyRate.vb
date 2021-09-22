Public Class FrmCurrencyRate

    Private Const GSNo As Byte = 0
    Private Const GCurrencyCode As Byte = 1
    Private Const GCurrency As Byte = 2
    Private Const GDescription As Byte = 3
    Private Const GSmallCurrency As Byte = 4
    Private Const GRate As Byte = 5

    Public WithEvents FGMain As New AgControls.AgDataGrid
    Private LIEvent As ClsEvents
    Private Sub FrmCurrencyRate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 498, 680, 0, 0)
            Agl.GridDesign(FGMain)
            IniGrid()
            MoveRec()
            TxtModified.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
    End Sub
    Private Sub IniGrid()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        PnlMain.Visible = False
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        FGMain.AllowUserToAddRows = False
        AgCl.AddAgTextColumn(FGMain, "SNo", 42, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "CurrencyCode", 0, 5, "Currency Code", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Currency", 70, 5, "Currency", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Description", 200, 5, "Description", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "SmallCurrency", 80, 5, "Small Currency", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Rate", 100, 15, "Rate (LC)", True, False, True)

        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        Agl.FSetSNo(FGMain, GSNo)
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Private Sub MoveRec()
        Dim DTTemp As New DataTable
        Dim I As Integer

        FGMain.Rows.Clear()
        Agl.ADMain = New SqlClient.SqlDataAdapter("Select SmallCurrency,Name,Code,Rate,ModifiedByRate,Discription From Currency Order By Name", Agl.Gcn)
        Agl.ADMain.Fill(DTTemp)
        If DTTemp.Rows.Count > 0 Then
            FGMain.Rows.Add(DTTemp.Rows.Count)
        End If
        For I = 0 To DTTemp.Rows.Count - 1
            TxtModified.Text = Agl.Xnull(DTTemp.Rows(I).Item("ModifiedByRate"))
            FGMain(GSNo, I).Value = Trim(I + 1)
            FGMain(GCurrencyCode, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Code"))
            FGMain(GCurrency, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Name"))
            FGMain(GDescription, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Discription"))
            FGMain(GSmallCurrency, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("SmallCurrency"))
            FGMain(GRate, I).Value = Agl.VNull(DTTemp.Rows(I).Item("Rate"))
        Next
        DTTemp.Dispose()
    End Sub

    Private Sub FGMain_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles FGMain.EditingControlShowing
        If TypeOf e.Control Is AgControls.AgTextBox Then
            RemoveHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
            AddHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
        End If
    End Sub

    Private Sub FGrdNumPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case FGMain.CurrentCell.ColumnIndex
            Case GRate
                CMain.NumPress(sender, e, 10, 2, False)
        End Select
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles BtnSave.Click, BtnClose.Click

        Select Case sender.name
            Case BtnSave.Name
                FSave()
            Case BtnClose.Name
                Me.Close()
        End Select
    End Sub
    Private Sub FSave()
        Dim I As Integer
        Dim GcnCmd As New SqlClient.SqlCommand

        GcnCmd.Connection = Agl.Gcn
        For I = 0 To FGMain.Rows.Count - 1
            GcnCmd.CommandText = "Update Currency Set "
            GcnCmd.CommandText = GcnCmd.CommandText + "Rate=" & Val(FGMain(GRate, I).Value) & ", "
            GcnCmd.CommandText = GcnCmd.CommandText + "Transfered='N', "
            GcnCmd.CommandText = GcnCmd.CommandText + "ModifiedByRate='" & Agl.PubUserName & "' "
            GcnCmd.CommandText = GcnCmd.CommandText + "Where Code='" & FGMain(GCurrencyCode, I).Value & "' "
            GcnCmd.ExecuteNonQuery()
        Next
        MsgBox(ClsMain.MsgSave)
    End Sub
    Private Sub FrmCurrencyRate_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub
End Class