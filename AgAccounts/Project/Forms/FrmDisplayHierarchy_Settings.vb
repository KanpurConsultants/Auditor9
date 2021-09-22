Imports System.Data.SQLite
Public Class FrmDisplayHierarchy_Settings
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Dim FrmDHMain As FrmDisplayHierarchy
    Sub New(ByVal FrmDHVar As FrmDisplayHierarchy)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FrmDHMain = FrmDHVar
        With DHSMain
            TxtFromDate.Text = .StrFromDate
            TxtToDate.Text = .StrToDate
            TxtSite.Tag = .StrSiteCode
            TxtSite.Text = .StrSiteName
            TxtDivision.Tag = .StrDivisionCode
            TxtDivision.Text = .StrDivisionName
            TxtZeroBalance.Tag = .StrZeroBalace
            TxtZeroBalance.Text = IIf(UCase(.StrZeroBalace) = "N", "No", "Yes")
            TxtShowContra.Tag = .StrShowContra
            TxtShowContra.Text = IIf(UCase(.StrShowContra) = "Y", "Yes", "No")
            TxtMonthWise.Tag = .StrMonthWise
            TxtMonthWise.Text = IIf(UCase(.StrMonthWise) = "Y", "Yes", "No")
            TxtClosingStock.Text = .DblClosingStock
        End With
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub
    Private Sub TxtBoxKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtFromDate.KeyDown, TxtToDate.KeyDown, TxtClosingStock.KeyDown
        KEAMainKeyCode = e
    End Sub

    Private Sub TxtBoxKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFromDate.KeyPress, TxtToDate.KeyPress, TxtClosingStock.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If e.KeyChar = Chr(Keys.Enter) Then SendKeys.Send("{Tab}") : Exit Sub
        If KEAMainKeyCode.Control Or KEAMainKeyCode.Shift Or KEAMainKeyCode.Alt Then Exit Sub
        'LIPublic.CheckQuote(e)

        Select Case sender.Name
            Case TxtClosingStock.Name
                CMain.NumPress(sender, e, 10, 2, False)
        End Select
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        If AgL.RequiredField(TxtFromDate, "From Date") Then Exit Sub
        If AgL.RequiredField(TxtToDate, "To Date") Then Exit Sub
        If AgL.RequiredField(TxtSite, "Site Name") Then Exit Sub
        If AgL.RequiredField(TxtZeroBalance, "Show Zero Value A/c") Then Exit Sub
        If AgL.RequiredField(TxtShowContra, "Show Contra A/c") Then Exit Sub

        With DHSMain
            .StrFromDate = AgL.RetDate(TxtFromDate.Text)
            .StrToDate = AgL.RetDate(TxtToDate.Text)
            .StrSiteCode = TxtSite.Tag
            .StrSiteName = TxtSite.Text
            .StrDivisionCode = TxtDivision.Tag
            .StrDivisionName = TxtDivision.Text
            .StrZeroBalace = IIf(UCase(TxtZeroBalance.Tag) = "N", "N", "Y")
            .StrShowContra = IIf(UCase(TxtShowContra.Tag) = "Y", "Y", "N")
            .StrMonthWise = IIf(UCase(TxtMonthWise.Tag) = "Y", "Y", "N")
            .DblClosingStock = Val(TxtClosingStock.Text)
            .StrCostCenter = TxtCostCenter.Tag
        End With
        Me.Close()
    End Sub

    Private Sub FrmDisplayHierarchy_Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim mQry$

        TxtSite.AgHelpDataSet = AgL.FillData("Select SM.Code,SM.Name From SiteMast SM Order By SM.Name", AgL.GCn)
        mQry = "Select 'Y' As Code, 'Yes' as Name " &
             " Union All " &
             " Select 'N' As Code, 'No' as Name "
        TxtZeroBalance.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        TxtShowContra.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        TxtMonthWise.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmDisplayHierarchy_Settings_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub TxtFromDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles TxtFromDate.Validated, TxtToDate.Validated
        Select Case sender.name
            Case TxtFromDate.Name, TxtToDate.Name
                sender.Text = AgL.RetDate(sender.Text)
        End Select

    End Sub

    Private Sub FHPGD_Site()
        Dim mQry$
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        Dim strCond As String = ""




        mQry = " SELECT 'o' As Tick, H.Code, H.Name " &
                " FROM SiteMast H " &
                " Order By H.Name "


        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Site Name", 150, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            TxtSite.Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
            TxtSite.Text = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If

        FRH_Multiple = Nothing
    End Sub

    Private Sub FHP_Site(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("Select SM.Code,SM.Name From SiteMast SM Order By SM.Name", AgL.GCn).Tables(0)
        'TxtSite.AgHelpDataSet = AgL.FillData("Select SM.Code,SM.Name From SiteMast SM Order By SM.Name", AgL.GCn)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 325, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Site", 250, DataGridViewContentAlignment.MiddleLeft)

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



    Private Sub FHPGD_Division()
        Dim mQry$
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        Dim strCond As String = ""


        mQry = " SELECT 'o' As Tick, H.Div_Code as Code, Sg.DispName as Name 
                FROM Division H 
                Left Join Subgroup Sg On H.Subcode = Sg.Subcode
                Order By Sg.DispName "


        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Division Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            TxtDivision.Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
            TxtDivision.Text = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If

        FRH_Multiple = Nothing
    End Sub

    Private Sub FHP_Division(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("Select SM.Div_Code as Code,SM.DispName as Name From Division SM Left Join Subgroup Sg On SM.Subcode = Sg.Subcode Order By Sg.DispName", AgL.GCn).Tables(0)
        'TxtDivision.AgHelpDataSet = AgL.FillData("Select SM.Code,SM.Name From DivisionMast SM Order By SM.Name", AgL.GCn)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 325, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Division", 250, DataGridViewContentAlignment.MiddleLeft)

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



    'Private Sub FHPGD_AcGroup()
    '    Dim mQry$
    '    Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
    '    Dim StrRtn As String = ""

    '    Dim strCond As String = ""

    '    mQry = " SELECT 'o' As Tick, H.GroupCode as Code, H.GroupName as Name " &
    '            " FROM AcGroup H " &
    '            " Order By H.GroupName "


    '    If LblAcGroup.Tag IsNot Nothing Then
    '        FRH_Multiple = LblAcGroup.Tag
    '    Else
    '        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 500, , , False)
    '    End If

    '    FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
    '    FRH_Multiple.FFormatColumn(1, , 0, , False)
    '    FRH_Multiple.FFormatColumn(2, "A/c Group Name", 300, DataGridViewContentAlignment.MiddleLeft)

    '    FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
    '    FRH_Multiple.ShowDialog()

    '    If FRH_Multiple.BytBtnValue = 0 Then
    '        TxtAcGroup.Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
    '        TxtAcGroup.Text = FRH_Multiple.FFetchData(2, "", "", ",", True)
    '        LblAcGroup.Tag = FRH_Multiple
    '    End If

    '    FRH_Multiple = Nothing
    'End Sub

    Private Sub FHPGD_CostCenter()
        Dim mQry$
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        Dim strCond As String = ""

        'mQry = " SELECT 'o' As Tick, H.Code, H.Name  " &
        '        " FROM CostCenterMast H " &
        '        " Where IfNull(H.Div_Code,'" & AgL.PubDivCode & "') = '" & AgL.PubDivCode & "' " &
        '        " Order By H.Name "

        mQry = " SELECT 'o' As Tick, H.Code, H.Name  " &
                " FROM CostCenterMast H " &
                " Order By H.Name "


        If LblCostCenter.Tag IsNot Nothing Then
            FRH_Multiple = LblCostCenter.Tag
        Else
            FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 500, , , False)
        End If

        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Cost Center Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            TxtCostCenter.Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
            TxtCostCenter.Text = FRH_Multiple.FFetchData(2, "", "", ",", True)
            TxtCostCenter.Tag = FRH_Multiple
        End If

        FRH_Multiple = Nothing
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
    Private Sub TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
        Handles TxtSite.KeyDown, TxtZeroBalance.KeyDown, TxtShowContra.KeyDown, TxtCostCenter.KeyDown, TxtDivision.KeyDown, TxtMonthWise.KeyDown

        'If e.KeyCode = Keys.Enter And (TypeOf sender Is ComboBox) Then SendKeys.Send("{Tab}") : Exit Sub
        If e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Down Or
            e.KeyCode = Keys.Up Or e.KeyCode = Keys.Tab Or e.KeyCode = Keys.Enter Or
            e.KeyCode = Keys.ShiftKey Or e.KeyCode = 18 Then Exit Sub
        KEAMainKeyCode = e

        Select Case sender.Name
            Case TxtSite.Name
                FHPGD_Site()
            Case TxtDivision.Name
                FHPGD_Division()
            'Case TxtAcGroup.Name
            '    FHPGD_AcGroup()
            Case TxtCostCenter.Name
                FHPGD_CostCenter()

            Case TxtSite.Name, TxtZeroBalance.Name, TxtShowContra.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select

    End Sub
    Private Sub TextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles TxtSite.KeyPress, TxtZeroBalance.KeyPress, TxtShowContra.KeyPress, TxtDivision.KeyPress, TxtCostCenter.KeyPress, TxtMonthWise.KeyPress

        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If e.KeyChar = Chr(Keys.Enter) Then SendKeys.Send("{Tab}") : Exit Sub
        If KEAMainKeyCode.Control Or KEAMainKeyCode.Shift Or KEAMainKeyCode.Alt Then Exit Sub

        'LIPublic.CheckQuote(e)
        Try
            Select Case sender.Name
                Case TxtSite.Name
                    'FHP_Site(e, sender)
                Case TxtZeroBalance.Name, TxtShowContra.Name
                    'FHP_YN(e, sender)
            End Select

        Catch Ex As Exception
            MsgBox("System Exception : " & vbCrLf & Ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub FrmDisplayHierarchy_Settings_QueryContinueDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.QueryContinueDragEventArgs) Handles Me.QueryContinueDrag

    End Sub

    Private Sub TxtSite_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSite.TextChanged

    End Sub
End Class