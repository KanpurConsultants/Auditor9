Imports System.ComponentModel
Imports System.Data.SqlClient
Public Class FrmPurchPartyDetail
    Dim mQry As String = ""

    Dim DtMaster As DataTable = Nothing

    Public mOkButtonPressed As Boolean = False

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
            If e.KeyCode = Keys.Escape Then Me.Close()
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            BtnOk.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
            BtnCancel.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BlankText()
    End Sub

    Public Sub DispText(ByVal Enable As Boolean)
        TxtVendorMobile.Enabled = Enable
        TxtVendorName.Enabled = Enable
        TxtVendorAdd1.Enabled = Enable
        TxtVendorCity.Enabled = Enable
        TxtState.Enabled = Enable

        TxtVendorMobile.BackColor = Color.White
        TxtVendorName.BackColor = Color.White
        TxtVendorAdd1.BackColor = Color.White
        TxtVendorCity.BackColor = Color.White
        TxtState.BackColor = Color.White
    End Sub

    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0
        Try
            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Try
            Select Case sender.Name
                Case BtnOk.Name
                    If Not Data_Validation() Then Exit Sub
                    mOkButtonPressed = True
                    Me.Close()

                Case BtnCancel.Name
                    mOkButtonPressed = False
                    Me.Close()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtSaleToPartyCity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtVendorCity.Enter
        Try
            Select Case sender.Name
                Case TxtVendorCity.Name
                    mQry = " SELECT C.CityCode AS Code, C.CityName, S.Description as StateName, C.State as StateCode 
                             FROM City C  With (NoLock)
                            Left Join State S On S.Code = C.State
                           "
                    TxtVendorCity.AgHelpDataSet(1) = AgL.FillData(mQry, AgL.GCn)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtSaleToPartyMobile_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtVendorMobile.Validating
        Dim DtTemp As DataTable = Nothing
        Try
            Select Case sender.Name
                Case TxtVendorMobile.Name
                    If TxtVendorMobile.Text <> "" And TxtVendorName.Text = "" Then
                        If AgL.PubServerName = "" Then
                            mQry = " Select H.SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyCity, C.CityName As SaleToPartyCityName, C.State as StateCode, S.Description as StateName " &
                                " From SaleInvoice H  With (NoLock) " &
                                " LEFT JOIN City C  With (NoLock) On H.SaleToPartyCity = C.CityCode " &
                                " Left Join State S  With (NoLock) On C.State = S.Code" &
                                " Where H.SaleToPartyMobile = '" & TxtVendorMobile.Text & "' Order by V_Date Desc Limit 1"
                        Else
                            mQry = " Select Top 1 H.SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyCity, C.CityName As SaleToPartyCityName, C.State as StateCode, S.Description as StateName " &
                                " From SaleInvoice H  With (NoLock) " &
                                " LEFT JOIN City C  With (NoLock) On H.SaleToPartyCity = C.CityCode " &
                                " Left Join State S  With (NoLock) On C.State = S.Code" &
                                " Where H.SaleToPartyMobile = '" & TxtVendorMobile.Text & "' Order by V_Date Desc"
                        End If
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            With DtTemp
                                If .Rows.Count > 0 Then
                                    TxtVendorName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                                    TxtVendorAdd1.Text = AgL.XNull(.Rows(0)("SaleToPartyAddress"))
                                    TxtVendorCity.Tag = AgL.XNull(.Rows(0)("SaleToPartyCity"))
                                    TxtVendorCity.Text = AgL.XNull(.Rows(0)("SaleToPartyCityName"))
                                    TxtState.Tag = AgL.XNull(.Rows(0)("StateCode"))
                                    TxtState.Text = AgL.XNull(.Rows(0)("StateName"))
                                End If
                            End With
                        End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtVendorCity_Validating(sender As Object, e As CancelEventArgs) Handles TxtVendorCity.Validating
        Dim DrTemp As DataRow() = Nothing

        DrTemp = TxtVendorCity.AgHelpDataSet.Tables(0).Select("Code = '" & TxtVendorCity.Tag & "'")
        If DrTemp.Length > 0 Then
            TxtState.Tag = AgL.XNull(DrTemp(0)("StateCode"))
            TxtState.Text = AgL.XNull(DrTemp(0)("StateName"))
        End If
    End Sub
End Class