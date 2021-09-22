Public Class FrmVoucherEntry_ReferenceFilter
    Private LIEvent As ClsEvents
    Dim FrmVEMain As FrmVoucherEntry_LedgerAdjNew
    Dim IntRowMain As Integer
    Dim BankCode As String
    Dim BlnModeVal As Boolean

    Sub New(ByVal FrmVEVar As FrmVoucherEntry_LedgerAdjNew, ByVal IntRowVar As Integer)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FrmVEMain = FrmVEVar
        IntRowMain = IntRowVar
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        With FrmVEMain
            .FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_FromDate, IntRowMain).Value = TxtFromDate.Text
            .FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_ToDate, IntRowMain).Value = TxtToDate.Text
            .FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_BillNo, IntRowMain).Value = TxtBillNo.Text
            .FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_Party, IntRowMain).Tag = TxtParty.Tag
            .FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_Party, IntRowMain).Value = TxtParty.Text
        End With
        Me.Close()
    End Sub

    Private Sub FrmVoucherEntry_Chq_Det_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LIEvent = New ClsEvents(Me)

        If FrmVEMain.FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_FromDate, IntRowMain).Value <> "" Then
            TxtFromDate.Text = FrmVEMain.FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_FromDate, IntRowMain).Value
        Else
            TxtFromDate.Text = AgL.PubStartDate
        End If

        If FrmVEMain.FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_ToDate, IntRowMain).Value <> "" Then
            TxtToDate.Text = FrmVEMain.FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_ToDate, IntRowMain).Value
        Else
            TxtToDate.Text = AgL.PubEndDate
        End If

        TxtBillNo.Text = FrmVEMain.FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_BillNo, IntRowMain).Value
        TxtParty.Tag = FrmVEMain.FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_Party, IntRowMain).Tag
        TxtParty.Text = FrmVEMain.FGMain(FrmVoucherEntry_LedgerAdjNew.GFilter_Party, IntRowMain).Value
    End Sub

    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
    End Sub

    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
    End Sub

    Private Sub FrmVoucherEntry_Chq_Det_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub

    Private Sub TxtParty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtParty.KeyDown
        Dim mQry$ = ""
        Try
            Select Case sender.Name
                Case TxtParty.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtParty.AgHelpDataSet Is Nothing Then
                            mQry = " Select Sg.SubCode, Sg.Name From SubGroup Sg Where Sg.Nature In ('Customer','Supplier') "
                            TxtParty.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class