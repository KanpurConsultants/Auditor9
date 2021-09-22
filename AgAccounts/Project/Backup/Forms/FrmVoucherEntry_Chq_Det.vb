Public Class FrmVoucherEntry_Chq_Det
    Private LIEvent As ClsEvents
    Dim FrmVEMain As FrmVoucherEntry
    Dim IntRowMain As Integer
    Dim BankCode As String
    Dim BlnModeVal As Boolean
    Sub New(ByVal FrmVEVar As FrmVoucherEntry, ByVal IntRowVar As Integer, ByVal BlnMode As Boolean, ByVal SubCode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FrmVEMain = FrmVEVar
        IntRowMain = IntRowVar
        With FrmVEMain
            TxtChequeNo.Text = .FGMain(FrmVoucherEntry.GChqNo, IntRowMain).Value
            TxtChequeDate.Text = .FGMain(FrmVoucherEntry.GChqDate, IntRowMain).Value
            BankCode = SubCode
            BlnModeVal = BlnMode
        End With
        BtnOK.Enabled = BlnMode
        CMain.SetEnableDisable(Me, BtnOK.Enabled)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        With FrmVEMain
            .FGMain(FrmVoucherEntry.GChqNo, IntRowMain).Value = Trim(TxtChequeNo.Text)
            .FGMain(FrmVoucherEntry.GChqDate, IntRowMain).Value = AgL.RetDate(TxtChequeDate.Text)
        End With
        Me.Close()
    End Sub

    Private Sub FrmVoucherEntry_Chq_Det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LIEvent = New ClsEvents(Me)
    End Sub

    Private Sub FrmVoucherEntry_Chq_Det_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub
    Private Sub TxtFromDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles TxtChequeDate.Validated
        Select Case sender.name
            Case TxtChequeDate.Name
                sender.Text = Agl.RetDate(sender.Text)
        End Select
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtChequeNo.Name
                If e.KeyCode = Keys.Insert Then
                    FHP_ChequeNo(sender)
                End If

        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
    End Sub
    Private Sub FHP_ChequeNo(ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSQL As String

        StrSQL = "Select ChequeNo As Code,ChequeNo "
        StrSQL += "From ChequeMast Where BCode='" & BankCode & "' And ChequeNo Not In "
        StrSQL += "(Select Chq_No From Ledger_Temp Where ContraSub='" & BankCode & "' And IsNull(Chq_No,'')<>'') "
        StrSQL += "Order By ChequeNo "
        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), "", 300, 200, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 120, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
            End If
        End If
        FRH = Nothing
    End Sub
End Class