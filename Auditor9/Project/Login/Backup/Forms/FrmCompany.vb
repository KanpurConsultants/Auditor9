Public Class FrmCompany
    Private Const GSNo As Byte = 0
    Private Const GCompanyCode As Byte = 1
    Private Const GCompanyName As Byte = 2
    Private Const GYear As Byte = 3
    Private WithEvents FGMain As New CustomDataGridView

    Private Sub FrmCompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AgL.GridDesign(FGMain)
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
        AgL.AddTextColumn(FGMain, "SNo", 50, 5, "S.No.", True, True, False)
        AgL.AddTextColumn(FGMain, "CompCode", 0, 5, "Company Code", False, True, False)
        Agl.AddTextColumn(FGMain, "CompName", 515, 5, "Company", True, True, False)
        Agl.AddTextColumn(FGMain, "Year", 100, 5, "Year", True, True, False)
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(FGMain, GSNo)
        FGMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        FGMain.BackgroundColor = Color.White
        FGMain.TabIndex = 0
        FGMain.BorderStyle = BorderStyle.None
        FGMain.GridColor = Color.White
    End Sub

    Public Sub MoveRec()
        Dim ADTemp As OleDb.OleDbDataAdapter
        Dim DTTemp As New DataTable
        Dim I As Integer
        Dim CondStr As String = " Where 1 = 1 "

        FGMain.Rows.Clear()
        CondStr += " And IsNull(DeletedYN,'N') <> 'Y' "
        'If AgL.PubDivisionApplicable Then
        '    CondStr += " And IsNull(Div_Code,'')='" & AgL.PubDivCode & "'"
        'End If
        DTTemp = AgL.FillData("Select Comp_Code,Comp_Name,CYear, City From Company " & CondStr & " Order By Start_Dt Desc", AgL.GcnMain).TABLES(0)

        If DTTemp.Rows.Count = 0 Then
            MsgBox("Company Detail Not Exists!..." & vbCrLf & "Contact to System Administrator!...")
        Else
            FGMain.Rows.Add(DTTemp.Rows.Count)
            For I = 0 To DTTemp.Rows.Count - 1
                FGMain(GSNo, I).Value = Trim(I + 1)
                FGMain(GCompanyCode, I).Value = DTTemp.Rows(I).Item("Comp_Code")
                FGMain(GCompanyName, I).Value = DTTemp.Rows(I).Item("Comp_Name")
                FGMain(GYear, I).Value = DTTemp.Rows(I).Item("CYear")
            Next
        End If
        ADTemp = Nothing
        DTTemp = Nothing
    End Sub

    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click

        Select Case sender.Name
            Case BtnOk.Name
                FSelectCompany()
            Case BtnCancel.Name
                FrmDivisionSelection.Dispose()
                FrmLogin.Dispose()
                Me.Dispose()
        End Select
    End Sub


    Private Sub FSelectCompany()
        Dim mQry As String, mU_EntDt As String, bCompanyName$ = ""
        Dim DtTemp As DataTable = Nothing

        If FGMain(GCompanyCode, FGMain.CurrentRow.Index).Value <> "" Then

            bCompanyName = FGMain(GCompanyName, FGMain.CurrentRow.Index).Value

            'If AgL.StrCmp(bCompanyName.Substring(0, 7), "DATAMAN") Then
            '    Carpet_ProjLib.ClsMain.IsClient_Dataman = True
            '    AgL.PubKillerDate = ""

            'ElseIf AgL.StrCmp(bCompanyName.Substring(0, 5), "Surya") Then
            '    Carpet_ProjLib.ClsMain.IsClient_SuryaCarpet = True
            '    AgL.PubKillerDate = "15/May/2011"
            'End If

            Call ProcActiveModule()

            AgIniVar.FOpenConnection(FGMain(GCompanyCode, FGMain.CurrentRow.Index).Value)

            If Trim(AgL.PubSiteCode) = "" Then
                mQry = "SELECT Code As Site_Code " & _
                       " FROM SiteMast S " & _
                       " WHERE IsNull(S.Active,0) <> 0"
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count = 1 Then
                    AgIniVar.ProcIniSiteDetail(DtTemp.Rows(0).Item("Site_Code"), AgIniVar)
                Else
                    Dim FrmObj As New FrmSiteSelection()
                    FrmObj.ShowDialog()
                End If
            End If
            If Trim(AgL.PubSiteCode) = "Cancel" Then AgL.PubSiteCode = "" : Exit Sub
            If Trim(AgL.PubSiteCode) = "" Then MsgBox("Site Code Can't Be Blank!") : Exit Sub
            AgL.PubSiteList = FGetAllSiteList()

            mU_EntDt = AgL.GetDateTime(AgL.GcnMain)

            mQry = "Select IsNull(Count(Table_Name),0) As Cnt From INFORMATION_SCHEMA.Tables Where Table_Name='Login_Log' And Table_Type='BASE TABLE'"
            AgL.ECmd = AgL.Dman_Execute(mQry, AgL.GcnMain)
            If AgL.ECmd.ExecuteScalar > 0 Then
                mQry = "Insert Into Login_Log (User_Name, MachineName, Div_Code, Site_Code , Comp_Code, U_EntDt) Values(" & _
                        " " & AgL.Chk_Text(AgL.PubUserName) & "," & AgL.Chk_Text(AgL.PubMachineName) & "," & AgL.Chk_Text(AgL.PubDivCode) & "," & _
                        " " & AgL.Chk_Text(AgL.PubSiteCode) & "," & AgL.Chk_Text(AgL.PubCompCode) & ",'" & mU_EntDt & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            Call FPreventPiracy()

            Dim MD As New MDIMain
            FAddMenu(MD, AgLibrary.ClsConstant.Module_Common_Master)
            MD.StrCurrentModule = AgLibrary.ClsConstant.Module_Common_Master
            MD.Show()
            Me.Dispose()

            DtTemp.Dispose()
        End If
    End Sub

    Public Function FGetAllSiteList()
        Dim DTTemp As DataTable
        Dim I As Integer
        Dim StrSiteList As String

        StrSiteList = ""

        DTTemp = AgL.FillData("Select Code From SiteMast ", AgL.GCn).tables(0)
        For I = 0 To DTTemp.Rows.Count - 1
            If StrSiteList <> "" Then StrSiteList += ","
            StrSiteList += "'" & AgL.XNull(DTTemp.Rows(I).Item("Code")) & "'"
        Next

        FGetAllSiteList = StrSiteList
    End Function

    Private Sub FPreventPiracy()
        'Dim StrKey As String
        'Try
        '    DMKey.set_FirmName(AgL.PubRegOfficeName)
        '    DMKey.set_CityName(AgL.PubRegOfficeCity)
        '    DMKey.set_ModuleCode("CPT")


        '    If Not DMKey.Validate(AgL.PubCompSerialNo) Then
        '        MsgBox("This Copy Is Not Registered. Please Enter Serial Key.")
        '        StrKey = InputBox("Enter Serial No. : ", "Dataman Registration.")
        '        If Trim(StrKey) <> "" Then
        '            AgL.Dman_ExecuteNonQry("Update Company Set SerialKeyNo='" & StrKey & "' Where Comp_Code='" & AgL.PubCompCode & "' ", AgL.GcnMain)
        '            MsgBox("Please Re-Run The Software.")
        '            End
        '        End If
        '    End If

        '    If Not DMKey.Validate(AgL.PubCompSerialNo) Then
        '        '===============================================================
        '        '================== For Demo Purpose ===========================
        '        '====================== Begin ==================================
        '        '===============================================================

        '        Dim DTTemp As DataTable
        '        DTTemp = AgL.FillData("Select Count(*) As Cnt From Login_Log", AgL.GCn).Tables(0)
        '        If AgL.VNull(DTTemp.Rows(0).Item("Cnt")) > 2000 Then
        '            MsgBox("Demonstration Limit Exceeds." & vbCrLf & "Please Contact Dataman Computers System.", MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        '            DTTemp.Dispose()
        '            DTTemp = Nothing
        '            End
        '        End If
        '        DTTemp.Dispose()
        '        DTTemp = Nothing

        '        '===============================================================
        '        '================== For Demo Purpose ===========================
        '        '====================== End ====================================
        '        '===============================================================

        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub


    Private Sub ProcActiveModule()
        AgLibrary.ClsConstant.IsOldFaVoucherEntryActive = False
        AgLibrary.ClsConstant.IsNewFaVoucherEntryActive = True

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
            FSelectCompany()
        End If
    End Sub

 
End Class