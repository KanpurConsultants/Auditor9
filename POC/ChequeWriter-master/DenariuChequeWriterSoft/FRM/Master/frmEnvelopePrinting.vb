Public Class frmEnvelopePrinting

    Private Sub frmEnvelopePrinting_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmEnvelopePrinting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ds As New DataSet
        Dim DTPrint As New DataTable()

        Dim arr As New ArrayList
        arr = db.EnvelopeSize.Row("FromLeft,FromTop", String.Format("EnvelopeSizeId='{0}'", txtESId.Text))
        Dim FromLeft, FromTop As Integer
        FromLeft = UnitConversion(arr(0), Units.MilliMeter, Units.Tups)
        FromTop = UnitConversion(arr(1), Units.MilliMeter, Units.Tups)
        ' DTPrint = db.ExecuteQuery(String.Format("Select {0} as PayeeName, {1} as PayDate, {2} as Amount", frmMaster.cmbPayee.Text, frmMaster.dtpDate.Value, frmMaster.txtAmount.Text)).ToTable("PrintData")
        DTPrint = db.EnvelopeName.Grid("*", String.Format("EnvelopeId='{0}'", txtEnvelopeId.Text)).ToTable("EnvelopeName")

        ds.Clear()
        ds.Tables.Add(DTPrint)
        ReportView(CrystalReportViewer1, "cryEnvelope.rpt", ds, Nothing, FromLeft, FromTop)
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

End Class