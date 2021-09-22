Public Class FrmTest
    Private DSMain As New DataSet
    Private GCn As New SqlClient.SqlConnection
    Private ADMain As SqlClient.SqlDataAdapter
    Private FRH_M As FrmHelpGrid_Multi
    Private FRH_S As FrmHelpGrid

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        FHP_Single(e)
    End Sub
    Private Sub FrmTest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GCn.ConnectionString = "Persist Security Info=False;User ID='sa';pwd='BhelServer';Initial Catalog=Bhel08 ;Data Source=."
        GCn.Open()

        Top = 0
        Left = 0

    End Sub
    Private Sub FHP_Single(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim StrSendText As String, DTMain As New DataTable
        Try
            If TextBox1.Text = "" Then
                StrSendText = e.KeyChar
            Else
                StrSendText = TextBox1.Text
            End If
            ADMain = New SqlClient.SqlDataAdapter("Select SG.SubCode,SG.Name,SG.Add1,SG.Add2,SG.Nature,C.CityName From Subgroup SG Left Join City C On C.CityCode=SG.CityCode Order by SG.Name", GCn)
            'If Not DTMain.Rows.Count > 0 Then
            'ADMain = New SqlClient.SqlDataAdapter("Select DocId,V_Type,RTrim(V_No) As V_No,RTrim(V_Date) As V_Date From Ledger Order by V_Type,V_No", GCn)
            ADMain.Fill(DTMain)
            'End If
            FRH_S = New FrmHelpGrid(New DataView(DTMain), StrSendText, 400, 880, (Top + TextBox1.Top) + 42, Left + TextBox1.Left + 2, False)

            FRH_S.FFormatColumn(0, , 0, , False)
            FRH_S.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
            FRH_S.FFormatColumn(2, "Address", 200, DataGridViewContentAlignment.MiddleLeft)
            FRH_S.FFormatColumn(3, "Address 2", 200, DataGridViewContentAlignment.MiddleLeft)
            FRH_S.FFormatColumn(4, "Nature", 100, DataGridViewContentAlignment.MiddleLeft)
            FRH_S.FFormatColumn(5, "City", 100, DataGridViewContentAlignment.MiddleLeft)
            FRH_S.ShowDialog(Me)

            If FRH_S.BytBtnValue = 0 Then
                If Not FRH_S.DRReturn.Equals(Nothing) Then
                    TextBox1.Text = FRH_S.DRReturn.Item(1)
                    TextBox1.Tag = FRH_S.DRReturn.Item(0)
                End If
            End If
            FRH_M = Nothing
            'ADMain = Nothing
            'DTMain = Nothing
            e.KeyChar = ""
        Catch Ex As Exception
            MsgBox("System Exception : " & vbCrLf & Ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
        End Try
    End Sub
    Private Sub FHP_Multi(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim StrSendText As String, DTMain As New DataTable
        Try
            If TextBox1.Text = "" Then
                StrSendText = e.KeyChar
            Else
                StrSendText = TextBox1.Text
            End If
            ADMain = New SqlClient.SqlDataAdapter("Select 'o' As Tick,SG.SubCode,SG.Name,SG.Add1,SG.Add2,SG.Nature,C.CityName From Subgroup SG Left Join City C On C.CityCode=SG.CityCode Order by SG.Name", GCn)
            'If Not DTMain.Rows.Count > 0 Then
            'ADMain = New SqlClient.SqlDataAdapter("Select DocId,V_Type,RTrim(V_No) As V_No,RTrim(V_Date) As V_Date From Ledger Order by V_Type,V_No", GCn)
            ADMain.Fill(DTMain)
            'End If
            FRH_M = New FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 400, 920, (Top + TextBox1.Top) + 42, Left + TextBox1.Left + 2, False)

            FRH_M.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter)
            FRH_M.FFormatColumn(1, , 0, , False)
            FRH_M.FFormatColumn(2, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
            FRH_M.FFormatColumn(3, "Address", 200, DataGridViewContentAlignment.MiddleLeft)
            FRH_M.FFormatColumn(4, "Address 2", 200, DataGridViewContentAlignment.MiddleLeft)
            FRH_M.FFormatColumn(5, "Nature", 100, DataGridViewContentAlignment.MiddleLeft)
            FRH_M.FFormatColumn(6, "City", 100, DataGridViewContentAlignment.MiddleLeft)
            FRH_M.ShowDialog(Me)

            If FRH_M.BytBtnValue = 0 Then
                MsgBox(FRH_M.FFetchData(2, "'", "'", ","))
            End If
            FRH_M = Nothing
            'ADMain = Nothing
            'DTMain = Nothing
            e.KeyChar = ""
        Catch Ex As Exception
            MsgBox("System Exception : " & vbCrLf & Ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
        End Try
    End Sub
End Class