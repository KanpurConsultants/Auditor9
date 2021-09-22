Public Class FrmDisplayHierarchy_Settings_Stock
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Dim FrmDHMain As FrmDisplayHierarchy_Stock
    Dim mQry$ = ""

    Sub New(ByVal FrmDHVar As FrmDisplayHierarchy_Stock)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FrmDHMain = FrmDHVar
        With DHSSMain
            TxtFromDate.Text = .StrFromDate
            TxtToDate.Text = .StrToDate
            TxtZeroBalance.Tag = .StrZeroBalace
            TxtZeroBalance.Text = IIf(UCase(.StrZeroBalace) = "N", "No", "Yes")
            TxtMethod.Text = IIf(UCase(.StrReportType) = "A", "Average Wise", "FIFO Wise")
            TxtMethod.Tag = .StrReportType

            TxtItemCategory.Text = .StrItemCategory
            TxtItemCategory.Tag = .StrItemCategoryCode

            TxtItemGroup.Text = .StrItemGroup
            TxtItemGroup.Tag = .StrItemGroupCode

            TxtItemName.Text = .StrItemName
            TxtItemName.Tag = .StrItemNameCode

            TxtSite.Text = .StrSiteName
            TxtSite.Tag = .StrSiteCode

            TxtGodown.Text = .StrGodownName
            TxtGodown.Tag = .StrGodownCode

        End With
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub FrmDisplayHierarchy_Settings_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Agl.WinSetting(Me,  272, 686, 150, 150)
    End Sub

    Private Sub TxtBoxKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtFromDate.KeyDown, TxtToDate.KeyDown
        KEAMainKeyCode = e
    End Sub

    Private Sub TxtBoxKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFromDate.KeyPress, TxtToDate.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If e.KeyChar = Chr(Keys.Enter) Then SendKeys.Send("{Tab}") : Exit Sub
        If KEAMainKeyCode.Control Or KEAMainKeyCode.Shift Or KEAMainKeyCode.Alt Then Exit Sub
        'LIPublic.CheckQuote(e)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        If AgL.RequiredField(TxtFromDate, "From Date") Then Exit Sub
        If AgL.RequiredField(TxtToDate, "To Date") Then Exit Sub
        If AgL.RequiredField(TxtZeroBalance, "Show A/c Zero Value") Then Exit Sub
        If AgL.RequiredField(TxtMethod, "Method") Then Exit Sub
        If AgL.RequiredField(TxtItemCategory, "Item Category") Then Exit Sub
        If AgL.RequiredField(TxtItemGroup, "Item Group") Then Exit Sub
        If AgL.RequiredField(TxtItemName, "Item Name") Then Exit Sub
        If AgL.RequiredField(TxtGodown, "Godown") Then Exit Sub
        If AgL.RequiredField(TxtSite, "Site Name") Then Exit Sub

        With DHSSMain
            .StrFromDate = AgL.RetDate(TxtFromDate.Text)
            .StrToDate = AgL.RetDate(TxtToDate.Text)
            .StrZeroBalace = IIf(UCase(TxtZeroBalance.Tag) = "N", "N", "Y")
            .StrReportType = IIf(UCase(TxtMethod.Tag) = "A", "A", "F")

            .StrItemCategory = TxtItemCategory.Text
            .StrItemCategoryCode = TxtItemCategory.Tag

            .StrItemGroup = TxtItemGroup.Text
            .StrItemGroupCode = TxtItemGroup.Tag

            .StrItemName = TxtItemName.Text
            .StrItemNameCode = TxtItemName.Tag

            .StrGodownName = TxtGodown.Text
            .StrGodownCode = TxtGodown.Tag

            .StrSiteCode = TxtSite.Tag
            .StrSiteName = TxtSite.Text

        End With
        Me.Close()
    End Sub

    Private Sub FrmStockDisplayHierarchy_Settings_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub TxtFromDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles TxtFromDate.Validated, TxtToDate.Validated
        Select Case sender.name
            Case TxtFromDate.Name, TxtToDate.Name
                sender.Text = AgL.RetDate(sender.Text)
        End Select

    End Sub

    Private Sub FHP_ItemCategory(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = CMain.FGetDatTable("Select 'o' As Tick,IC.Code,IC.Description From ItemCategory IC Order By IC.Description", AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 300, 370, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)
        FRH.FFormatColumn(0, "Tick", 42, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",", False)
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub FHP_Godown(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = CMain.FGetDatTable("Select 'o' As Tick,GM.Code,GM.Description From Godown GM Order By GM.Description", AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 300, 370, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)
        FRH.FFormatColumn(0, "Tick", 42, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",", False)
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub FHP_Site(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = CMain.FGetDatTable("Select 'o' As Tick,SI.Code,SI.Name From SiteMast SI Order By SI.Name", AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 300, 370, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)
        FRH.FFormatColumn(0, "Tick", 42, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",", False)
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub FHP_ItemGroup(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = CMain.FGetDatTable("Select 'o' As Tick,IG.Code,IG.Description From ItemGroup IG Order By IG.Description", AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 300, 370, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)
        FRH.FFormatColumn(0, "Tick", 42, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",", False)
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_ItemName(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = CMain.FGetDatTable("Select 'o' As Tick,IM.Code,IM.Description as Name,IM.ManualCode From Item IM Order By IM.Description", AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 300, 470, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)
        FRH.FFormatColumn(0, "Tick", 42, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",", False)
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_YN(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)

        StrSQL = "Declare @TempTable As Table(Code NVarChar(1),Name NVarChar(3)) "
        StrSQL += "Insert Into @TempTable Values ('Y','Yes') "
        StrSQL += "Insert Into @TempTable Values ('N','No') "
        StrSQL += "Select Code,Name From @TempTable Order By Name "

        DTMain = CMain.FGetDatTable(StrSQL, AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 180, (Top + Txt.Top) + 45, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Method(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)

        StrSQL = "Declare @TempTable As Table(Code NVarChar(1),Name NVarChar(20)) "
        StrSQL += "Insert Into @TempTable Values ('A','Average Wise') "
        StrSQL += "Insert Into @TempTable Values ('F','FIFO Wise') "
        StrSQL += "Select Code,Name From @TempTable Order By Name "

        DTMain = CMain.FGetDatTable(StrSQL, AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 180, (Top + Txt.Top) + 45, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
        Handles TxtZeroBalance.KeyDown, TxtMethod.KeyDown, TxtItemCategory.KeyDown, TxtItemGroup.KeyDown, _
        TxtItemName.KeyDown, TxtGodown.KeyDown, TxtSite.KeyDown

        If e.KeyCode = Keys.Enter And (TypeOf sender Is ComboBox) Then SendKeys.Send("{Tab}") : Exit Sub
        KEAMainKeyCode = e

        Select Case sender.Name
            Case TxtZeroBalance.Name, TxtMethod.Name, TxtItemCategory.Name, TxtItemGroup.Name, _
                 TxtItemName.Name, TxtSite.Name, TxtGodown.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub
    Private Sub TextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles TxtZeroBalance.KeyPress, TxtMethod.KeyPress, TxtItemCategory.KeyPress, TxtItemGroup.KeyPress, _
    TxtItemName.KeyPress, TxtSite.KeyPress, TxtGodown.KeyPress

        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If e.KeyChar = Chr(Keys.Enter) Then SendKeys.Send("{Tab}") : Exit Sub
        If KEAMainKeyCode.Control Or KEAMainKeyCode.Shift Or KEAMainKeyCode.Alt Then Exit Sub

        'LIPublic.CheckQuote(e)
        Try
            Select Case sender.Name
                Case TxtZeroBalance.Name
                    FHP_YN(e, sender)
                Case TxtMethod.Name
                    FHP_Method(e, sender)
                Case TxtItemCategory.Name
                    FHP_ItemCategory(e, sender)
                Case TxtItemGroup.Name
                    FHP_ItemGroup(e, sender)
                Case TxtItemName.Name
                    FHP_ItemName(e, sender)
                Case TxtSite.Name
                    FHP_Site(e, sender)
                Case TxtGodown.Name
                    FHP_Godown(e, sender)
            End Select

        Catch Ex As Exception
            MsgBox("System Exception : " & vbCrLf & Ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub Ini_List()
        Try
            mQry = " Select 'Y' As Code, 'Yes' As Name " & _
                    " UNION ALL " & _
                    " Select 'N' As Code, 'No' As Name "
            TxtZeroBalance.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select 'A' As Code, 'Average Wise' As Name " & _
                    " UNION ALL " & _
                    " Select 'F' As Code, 'FIFO Wise' As Name "
            TxtMethod.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select 'Y' As Code, 'Yse' As Name " & _
                    " UNION ALL " & _
                    " Select 'N' As Code, 'No' As Name "
            TxtItemCategory.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select 'Y' As Code, 'Yse' As Name " & _
                    " UNION ALL " & _
                    " Select 'N' As Code, 'No' As Name "
            TxtItemGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select 'Y' As Code, 'Yse' As Name " & _
                    " UNION ALL " & _
                    " Select 'N' As Code, 'No' As Name "
            TxtItemName.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select 'Y' As Code, 'Yse' As Name " & _
                    " UNION ALL " & _
                    " Select 'N' As Code, 'No' As Name "
            TxtSite.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select 'Y' As Code, 'Yse' As Name " & _
                    " UNION ALL " & _
                    " Select 'N' As Code, 'No' As Name "
            TxtGodown.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class