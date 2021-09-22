Public Class FrmSiteSelection
    Private Const GSNo As Byte = 0
    Private Const GSiteCode As Byte = 1
    Private Const GSiteName As Byte = 2
    Private Const GHO_YN As Byte = 3
    Private Const GManualCode As Byte = 4
    Private Const GAdd1 As Byte = 5
    Private Const GAdd2 As Byte = 6
    Private Const GAdd3 As Byte = 7
    Private Const GCityName As Byte = 8
    Private Const GPinNo As Byte = 9
    Private Const GPhone As Byte = 10
    Private Const GMobile As Byte = 11

    Private WithEvents FGMain As New CustomDataGridView

    Private Sub FrmCompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Agl.GridDesign(FGMain)
        IniGrid()
        MoveRec()
    End Sub

    Private Sub IniGrid()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        Agl.AddTextColumn(FGMain, "SNo", 50, 5, "S.No.", True, True, False)
        AgL.AddTextColumn(FGMain, "SiteCode", 0, 5, "Site Code", False, True, False)
        AgL.AddTextColumn(FGMain, "SiteName", 465, 5, "Site Name", True, True, False)
        AgL.AddTextColumn(FGMain, "HO_YN", 50, 3, "H.O.", True, True, False)
        AgL.AddTextColumn(FGMain, "ColManualCode", 50, 50, "Short Name", False, True, False)
        AgL.AddTextColumn(FGMain, "ColAdd1", 50, 50, "Address1", False, True, False)
        AgL.AddTextColumn(FGMain, "ColAdd2", 50, 50, "Address2", False, True, False)
        AgL.AddTextColumn(FGMain, "ColAdd3", 50, 50, "Address3", False, True, False)
        AgL.AddTextColumn(FGMain, "ColCityName", 100, 50, "Site City", True, True, False)
        AgL.AddTextColumn(FGMain, "ColPinNo", 50, 50, "Pin No.", False, True, False)
        AgL.AddTextColumn(FGMain, "ColPhone", 50, 50, "Phone No.", False, True, False)
        AgL.AddTextColumn(FGMain, "ColMobile", 50, 50, "Mobile No.", False, True, False)

        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        Agl.FSetSNo(FGMain, GSNo)
        FGMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        FGMain.BackgroundColor = Color.White
        FGMain.TabIndex = 0
        FGMain.BorderStyle = BorderStyle.None
        FGMain.GridColor = Color.White
    End Sub

    Public Sub MoveRec()
        Dim ADTemp As OleDb.OleDbDataAdapter
        Dim DTTemp As New DataTable, DTTempSiteList As New DataTable
        Dim I As Integer
        Dim mQry As String = "", mCondStr As String = "", StrUserSiteList As String = ""

        FGMain.Rows.Clear()

        mCondStr = " Where 1=1 "

        If AgL.IsFieldExist("Active", "SiteMast", AgL.GcnMain) Then
            mCondStr = mCondStr & " And IsNull(S.Active,0) <> 0 "
        End If

        If Not (AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName)) Then
            mQry = "Select Sitelist From UserSite Where User_Name='" & AgL.PubUserName & "' And CompCode='" & AgL.PubCompCode & "'"
            StrUserSiteList = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnMain).ExecuteScalar)
            mCondStr = mCondStr & " And S.Code In (" & Replace(StrUserSiteList, "|", "'") & ")"
        End If

        mQry = "Select S.Code, S.Name, Case IsNull(S.Ho_Yn,'N') When 'N' Then 'No' Else 'Yes' End As Ho_Yn, " & _
                " S.ManualCode, S.Add1, S.Add2, S.Add3, C.CityName, S.Phone, S.Mobile, S.PinNo " & _
                " From SiteMast S " & _
                " LEFT JOIN City C ON S.City_Code = C.CityCode " & mCondStr

        DTTemp = AgL.FillData(mQry, AgL.GcnMain).TABLES(0)
        FGMain.Rows.Add(DTTemp.Rows.Count)
        For I = 0 To DTTemp.Rows.Count - 1
            FGMain(GSNo, I).Value = Trim(I + 1)
            FGMain(GSiteCode, I).Value = DTTemp.Rows(I).Item("Code")
            FGMain(GSiteName, I).Value = DTTemp.Rows(I).Item("Name")
            FGMain(GHO_YN, I).Value = DTTemp.Rows(I).Item("Ho_Yn")
            FGMain(GManualCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("ManualCode"))
            FGMain(GAdd1, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Add1"))
            FGMain(GAdd2, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Add2"))
            FGMain(GAdd3, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Add3"))
            FGMain(GCityName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("CityName"))
            FGMain(GPinNo, I).Value = AgL.XNull(DTTemp.Rows(I).Item("PinNo"))
            FGMain(GPhone, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Phone"))
            FGMain(GMobile, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Mobile"))

        Next
        ADTemp = Nothing
        DTTemp = Nothing
    End Sub

    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Select Case sender.Name
            Case BtnOk.Name
                FSelectSite()
            Case BtnCancel.Name
                Me.Dispose()
                AgL.PubSiteCode = "Cancel"
                FrmLogin.Show()
        End Select
    End Sub

    Private Sub FSelectSite()
        Dim mQry As String = "", mTable_Schema As String = ""

        If FGMain(GSiteCode, FGMain.CurrentRow.Index).Value <> "" Then
            AgIniVar.ProcIniSiteDetail(FGMain(GSiteCode, FGMain.CurrentRow.Index).Value, AgIniVar)
            AgL.PubCompName = FGMain.Item(GSiteName, FGMain.CurrentRow.Index).Value
            AgL.PubCompAdd1 = FGMain.Item(GAdd1, FGMain.CurrentRow.Index).Value
            AgL.PubCompAdd2 = FGMain.Item(GAdd2, FGMain.CurrentRow.Index).Value

            'AgL.PubSiteCode = FGMain(GSiteCode, FGMain.CurrentRow.Index).Value
            'AgL.PubSiteName = FGMain(GSiteName, FGMain.CurrentRow.Index).Value
            'AgL.PubSiteManualCode = FGMain(GManualCode, FGMain.CurrentRow.Index).Value
            'AgL.PubSiteAdd1 = FGMain(GAdd1, FGMain.CurrentRow.Index).Value
            'AgL.PubSiteAdd2 = FGMain(GAdd2, FGMain.CurrentRow.Index).Value
            'AgL.PubSiteAdd3 = FGMain(GAdd3, FGMain.CurrentRow.Index).Value
            'AgL.PubSiteCity = FGMain(GCityName, FGMain.CurrentRow.Index).Value
            'AgL.PubSitePinNo = FGMain(GPinNo, FGMain.CurrentRow.Index).Value
            'AgL.PubSitePhone = FGMain(GPhone, FGMain.CurrentRow.Index).Value
            'AgL.PubSiteMobile = FGMain(GMobile, FGMain.CurrentRow.Index).Value

            'AgL.PubSiteCodeDisplay = "('" & FGMain(GSiteCode, FGMain.CurrentRow.Index).Value & "')"
            'AgL.PubLogSiteName = AgL.PubSiteName

            'If FGMain(GHO_YN, FGMain.CurrentRow.Index).Value Is Nothing Then FGMain(GHO_YN, FGMain.CurrentRow.Index).Value = ""

            'AgL.PubIsHo = AgL.StrCmp(FGMain(GHO_YN, FGMain.CurrentRow.Index).Value.ToString, "Yes")

            'AgIniVar.ProcSwapSiteCompanyDetail()

            Me.Dispose()
        End If
    End Sub



    Private Sub FrmCompany_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim LGBBaseBackGround As System.Drawing.Drawing2D.LinearGradientBrush
        Dim RctVar As Rectangle
        Dim CtlVar As Control
        Dim StrVar As String

        'For Form Left
        RctVar = New Rectangle(0, Me.LblBottom.Height + 32, Me.LblLeft.Width, Me.LblLeft.Height)
        LGBBaseBackGround = New System.Drawing.Drawing2D.LinearGradientBrush(RctVar, Color.Gray, _
                                Color.WhiteSmoke, System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        e.Graphics.FillRectangle(LGBBaseBackGround, RctVar)

        'For Form Right
        RctVar = New Rectangle(Me.Width - Me.LblLeft.Width, Me.LblBottom.Height + 32, Me.LblRight.Width, Me.LblRight.Height)
        LGBBaseBackGround = New System.Drawing.Drawing2D.LinearGradientBrush(RctVar, Color.WhiteSmoke, _
                                Color.Gray, System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        e.Graphics.FillRectangle(LGBBaseBackGround, RctVar)

        For Each CtlVar In Me.Controls
            StrVar = CtlVar.GetType.ToString
            If StrVar = "System.Windows.Forms.Label" Then
                CtlVar.BackColor = System.Drawing.Color.Transparent
            End If
        Next
    End Sub

    Private Sub FGMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FGMain.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then
            FSelectSite()
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class