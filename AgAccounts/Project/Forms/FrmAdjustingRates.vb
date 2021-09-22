Public Class FrmAdjustingRates
    Private Const GTick As Byte = 0
    Private Const GItemType As Byte = 1
    Private Const GItemTypeName As Byte = 2
    Private Const GAffectableRecords As Byte = 3

    Public WithEvents FGMain As New AgControls.AgDataGrid
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private LIEvent As ClsEvents
    Private Sub FrmAdjustingRates_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 528, 864, 0, 0)
            AgL.GridDesign(FGMain)
            IniGrid()
            FFillItemType()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub IniGrid()
        FGMain.Columns.Clear()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        PnlMain.Visible = False
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        FGMain.AllowUserToAddRows = False
        AgCl.AddAgTextColumn(FGMain, "Tick", 42, 5, "Tick", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "ItemType", 0, 0, "ItemType", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Affect Item Type", 255, 0, "Affect Item Type", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Affectable Records", 130, 0, "Affectable Records (Aprx.)", True, True, False)

        FGMain.Columns(GTick).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)
        FGMain.Columns(GTick).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        FGMain.Columns(GTick).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        FGMain.ColumnHeadersHeight = 40
        FGMain.Anchor = PnlMain.Anchor
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Private Sub FFillItemType()
        Dim DTTemp As DataTable
        Dim I As Int16
        Dim StrSQL As String

        StrSQL = "Select IM.ItemType as Code,IM.ItemType As Name,Count(*) As ARD "
        StrSQL += "From Item IM  "
        StrSQL += "Left Join Stock ST On ST.Item=IM.Code "
        StrSQL += "Group By IM.ItemType "
        StrSQL += "Order By IM.ItemType "

        FGMain.Rows.Clear()
        DTTemp = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        For I = 0 To DTTemp.Rows.Count - 1
            FGMain.Rows.Add()
            FGMain(GTick, I).Value = "þ"
            FGMain(GItemType, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Code"))
            FGMain(GItemTypeName, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Name"))
            FGMain(GAffectableRecords, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("ARD"))
            If "FM" = Trim(UCase(Agl.Xnull(DTTemp.Rows(I).Item("Code")))) Then
                FGMain(GTick, I).Value = "o"
            End If
        Next
        DTTemp.Dispose()
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        If e.KeyCode = Keys.Delete Then sender.Text = "" : sender.Tag = ""
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Private Sub FrmAdjustingRates_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
        LblAccountBG.BackColor = Color.LemonChiffon
        LblAccountM.BackColor = Color.LemonChiffon
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click, BtnFIFO_AVG.Click
        Select Case sender.Name
            Case BtnClose.Name
                Me.Close()
            Case BtnFIFO_AVG.Name
                If MsgBox("Are You Sure? You Want To Update Value According To Average And FIFO Value. " & vbCrLf & "Once Rates Updated Cannot Be Undone.") = MsgBoxResult.No Then Exit Sub
                Refresh()
                CMain.FUpdateFIFO_Average(FGetItemType())
                MsgBox("Process Completed Successfully.")
        End Select
    End Sub
    Private Function FGetItemType() As String
        Dim StrItemType As String = ""
        Dim I As Integer

        For I = 0 To FGMain.Rows.Count - 1
            If "þ" = FGMain(GTick, I).Value Then
                If StrItemType <> "" Then StrItemType += ","
                StrItemType += "'" & Trim(FGMain(GItemType, I).Value) & "'"
            End If
        Next
        FGetItemType = StrItemType
    End Function
    Private Sub FCheckSend(ByVal IntRow As Integer, ByVal IntCol As Integer)
        If IntRow < 0 Or IntCol <> GTick Then Exit Sub
        If FGMain(GTick, IntRow).Value = "o" Then
            FGMain(GTick, IntRow).Value = "þ"
        Else
            FGMain(GTick, IntRow).Value = "o"
        End If
    End Sub
    Private Sub FGMain_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellClick
        If e.RowIndex >= 0 Then
            FCheckSend(e.RowIndex, e.ColumnIndex)
        End If
    End Sub
    Private Sub FGMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FGMain.KeyPress
        If e.KeyChar = Chr(Keys.Space) Then
            FCheckSend(FGMain.CurrentCell.RowIndex, FGMain.CurrentCell.ColumnIndex)
        End If
    End Sub
End Class