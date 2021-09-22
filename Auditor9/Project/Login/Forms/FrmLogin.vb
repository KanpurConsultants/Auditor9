Imports System.IO
Public Class FrmLogin

    Dim PlaceHolder_UserName$ = "User Name"
    Dim PlaceHolder_Password$ = "Password"
    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Dim DtTemp As DataTable = Nothing
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(TxtPassword.Text, PlaceHolder_Password) And TxtPassword.PasswordChar = vbNullChar Then TxtPassword.Text = ""
                'FCreateDatabase()
                'FCreateTables(StrPath + "\" + IniName)
                If FOpenIni(StrPath + "\" + IniName, TxtUserName.Text, TxtPassword.Text) Then
                    If AgL.PubDivisionApplicable Then
                        DtTemp = AgL.FillData("SELECT D.* FROM Division D", AgL.GcnMain).Tables(0)
                        If DtTemp.Rows.Count = 1 Then
                            AgL.PubDivCode = AgL.XNull(DtTemp.Rows(0)("Div_Code"))
                            AgL.PubDivName = AgL.XNull(DtTemp.Rows(0)("Div_Name"))
                            'AgL.PubDivisionDBName =  AgL.XNull(DtTemp.Rows(0)("DataPath"))
                            FrmCompany.Show()
                        Else
                            FrmDivisionSelection.Show()
                        End If
                        DtTemp = Nothing
                    Else
                        FrmCompany.Show()
                    End If

                    Me.Hide()
                Else
                    TxtPassword.Text = PlaceHolder_Password
                    TxtPassword.Focus()
                End If
            Case BtnCancel.Name
                Me.Dispose()
                End
        End Select

    End Sub

    Private Sub FrmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL = New AgLibrary.ClsMain()
        AgL.AglObj = AgL
        AgL.PubIsLogInProjectActive = True
        AgL.PubDivisionApplicable = True
        FConnectDb()


        TxtUserName.Text = PlaceHolder_UserName
        TxtPassword.PasswordChar = ""
        TxtPassword.Text = PlaceHolder_Password
        TxtUserName.ForeColor = Color.LightGray
        TxtPassword.ForeColor = Color.LightGray


        If AgL.StrCmp(AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "MarketedBy", ""), "Equal2") Then
            LogoPictureBox.Image = My.Resources.Equal2Logo
            LblHeaderText.Text = "Login To Equal2"
            LblWebsite.Text = "www.equal2.in"
            LblFooterText.Text = "@Equal2 Solutions, All  rights reserved."
            LblHeaderText.Left = (LblHeaderText.Parent.Width \ 2) - (LblHeaderText.Width \ 2)
            LblWebsite.Left = (LblWebsite.Parent.Width \ 2) - (LblWebsite.Width \ 2)
            LblFooterText.Left = (LblFooterText.Parent.Width \ 2) - (LblFooterText.Width \ 2)
        ElseIf AgL.StrCmp(AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "MarketedBy", ""), "Auditor9") Then
            LogoPictureBox.Image = My.Resources.Auditor9Logo
            LblHeaderText.Text = "Login To Auditor9"
            LblWebsite.Text = "www.auditor9.com"
            LblFooterText.Text = "@Auditor9 Erp Solutions, All  rights reserved."
            LblHeaderText.Left = (LblHeaderText.Parent.Width \ 2) - (LblHeaderText.Width \ 2)
            LblWebsite.Left = (LblWebsite.Parent.Width \ 2) - (LblWebsite.Width \ 2)
            LblFooterText.Left = (LblFooterText.Parent.Width \ 2) - (LblFooterText.Width \ 2)
        End If
    End Sub

    Private Sub FrmLogin_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
        LogoPictureBox.BackColor = Color.Transparent
    End Sub
    Private Sub TxtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles TxtPassword.KeyPress, TxtUserName.KeyPress

        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If e.KeyChar = Chr(Keys.Enter) And Not (TypeOf sender Is ComboBox) Then SendKeys.Send("{Tab}") : Exit Sub

        Try
            AgL.CheckQuote(e)
        Catch Ex As Exception
            MsgBox("System Exception : " & vbCrLf & Ex.Message, MsgBoxStyle.Exclamation, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub



    'Public Function ConnectDb(ByVal ServerName As String, ByVal DatabaseName As String, Optional ByVal DatabaseUser As String = "sa", Optional ByVal DatabasePassword As String = "") As String
    '    Agl.AglObj = Agl
    '    Agl.PubDBUserSQL = "SA"
    '    Agl.GCn = New Sqlite.SqliteConnection
    '    Agl.GcnRead = New Sqlite.SqliteConnection
    '    Agl.Gcn_ConnectionString = "Persist Security Info=False;User ID='" & DatabaseUser & "';pwd=" & DatabasePassword & ";Initial Catalog=" & DatabaseName & ";Data Source=" & ServerName
    '    Agl.GCn.ConnectionString = Agl.Gcn_ConnectionString
    '    Agl.GcnRead.ConnectionString = Agl.Gcn_ConnectionString
    '    ConnectDb = ""
    '    Try
    '        AgL.GCn.Open()
    '        Agl.GcnRead.Open()
    '        Agl.ECmd = New Sqlite.SqliteCommand
    '        Agl.ECmd = Agl.GCn.CreateCommand
    '    Catch ex As Exception
    '        ConnectDb = ex.Message
    '    End Try
    'End Function

    'Public Function FOpenUpdateIni() As Boolean
    '    Try
    '        AgL.PubDBUserSQL = "SA"
    '        AgL.PubServerName = AgL.INIRead(StrPath + "\" + IniName, "Server", "Name", "")
    '        AgL.PubCompanyDBName = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Path", "")
    '        AgL.PubChkPasswordSQL = AgL.INIRead(StrPath + "\" + IniName, "Security", "Password", "")
    '        FOpenUpdateIni = True
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Function

    Private Sub FConnectDb()
        Dim objFrm As FrmCompanyInput
        AgL.PubServerName = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Server", "")
        AgL.PubCompanyDBPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Path", "")
        AgL.PubCompanyDBName = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "DbName", "")

        If AgL.PubServerName = "" Then
            Dim curFile As String = AgL.PubCompanyDBPath & AgL.PubCompanyDBName
            If Not File.Exists(curFile) Then
                objFrm = New FrmCompanyInput()
                objFrm.ShowDialog()
            End If
        Else
            Dim mConn As New SqlClient.SqlConnection
            mConn.ConnectionString = "Persist Security Info=False;User ID='SA';pwd=;Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            Try
                mConn.Open()
            Catch ex As Exception
                objFrm = New FrmCompanyInput()
                objFrm.ShowDialog()
            End Try
        End If

    End Sub

    Private Sub TextBox1_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUserName.GotFocus, TxtPassword.GotFocus
        Select Case sender.name
            Case TxtUserName.Name
                If TxtUserName.Text = PlaceHolder_UserName Then
                    TxtUserName.Text = ""
                    TxtUserName.ForeColor = Nothing
                End If

            Case TxtPassword.Name
                If TxtPassword.Text = PlaceHolder_Password Then
                    TxtPassword.Text = ""
                    TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9632)
                    TxtPassword.ForeColor = Nothing
                End If
        End Select
    End Sub
    Private Sub TextBox1_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUserName.LostFocus, TxtPassword.LostFocus
        Select Case sender.name
            Case TxtUserName.Name
                If TxtUserName.Text = "" Then
                    TxtUserName.Text = PlaceHolder_UserName
                    TxtUserName.ForeColor = Color.LightGray
                End If

            Case TxtPassword.Name
                If TxtPassword.Text = "" Then
                    TxtPassword.Text = PlaceHolder_Password
                    TxtPassword.PasswordChar = ""
                    TxtPassword.ForeColor = Color.LightGray
                End If
        End Select
    End Sub
End Class
