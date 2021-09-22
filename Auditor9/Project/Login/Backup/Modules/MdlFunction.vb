Imports System.Data.SqlClient

Module MdlFunction
    Public DtMenu As DataTable
    Public ArrButtonModuleName(1) As String


    Public Function FOpenIni(ByVal StrIniPath As String, ByVal StrUserName As String, ByVal StrPassword As String) As Boolean
        Dim StrGetPassword As String = ""
        Dim OLECmd As New OleDb.OleDbCommand
        Dim BlnRtn As Boolean
        Dim ECmd As SqlClient.SqlCommand

        BlnRtn = False
        Try
            AgL.PubServerName = AgL.INIRead(StrIniPath, "Server", "Offline", "")

            If AgL.PubServerName.Trim <> "" Then
                If Not IsConnectionAvailable() Then
                    If MsgBox("Internet is Not Available. Do you want to Run Software Offline?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        AgL.PubServerName = AgL.INIRead(StrIniPath, "Server", "Offline", "")
                    Else
                        FOpenIni = False
                        Exit Function
                    End If
                Else
                    AgL.PubServerName = AgL.INIRead(StrIniPath, "Server", "Name", "")
                    AgL.PubSqlServerSite = AgL.XNull(AgL.INIRead(StrIniPath, "Server", "Offline", ""))
                    If AgL.PubSqlServerSite <> "" Then
                        AgL.PubOfflineApplicable = True
                    End If

                End If
            Else
                AgL.PubServerName = AgL.INIRead(StrIniPath, "Server", "Name", "")
            End If

            AgL.PubDBUserSQL = "SA"
            AgL.PubDBPasswordSQL = ""
            AgL.PubReportPath = AgL.INIRead(StrIniPath, "Reports", "Path", "")
            AgL.PubCompanyDBName = AgL.INIRead(StrIniPath, "CompanyInfo", "Path", "")
            AgL.PubChkPasswordSQL = AgL.INIRead(StrIniPath, "Security", "PasswordSQL", "")
            AgL.PubChkPasswordAccess = AgL.INIRead(StrIniPath, "Security", "PasswordAccess", "")
            AgL.PubSiteCodeActual = AgL.INIRead(StrIniPath, "Site", "Site", "")
            AgL.PubDataBackUpPath = AgL.INIRead(StrIniPath, "Backup", "BackupPath", "")
            AgL.PubReportPath = Trade.My.Application.Info.DirectoryPath & "\Reports"
            AgL.PubReportFaPath = Trade.My.Application.Info.DirectoryPath & "\ReportsFA"

            AgL.PubMoveRecApplicable = True


            AgIniVar = New AgLibrary.ClsIniVariables(AgL)

            BlnRtn = AgIniVar.FOpenIni(StrUserName, StrPassword)

            OLECmd = Nothing
        Catch Ex As Exception
            BlnRtn = False
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        Finally
            ECmd = Nothing
            AgPL = New AgLibrary.ClsPrinting(AgL)

            FOpenIni = BlnRtn
        End Try

    End Function


#Region "Menu Design"
    Public Sub FAddMenu_Windows(ByVal MD As MDIMain, ByVal MenuStrip1 As MenuStrip)
        Dim TSMI_Item As ToolStripMenuItem
        Dim TSSP_Item As ToolStripSeparator

        MenuStrip1.Items.Add("Windows")
        MenuStrip1.Items(MenuStrip1.Items.Count - 1).Name = "CWDS_Windows"
        MenuStrip1.Items(MenuStrip1.Items.Count - 1).Tag = "CWDS"     'CWSD Stands For Common Windows 
        FAddHandler(MenuStrip1.Items(MenuStrip1.Items.Count - 1), MD)
        MenuStrip1.MdiWindowListItem = MenuStrip1.Items(MenuStrip1.Items.Count - 1)

        TSMI_Item = New ToolStripMenuItem("Cascade")
        TSMI_Item.Name = "CWDS_Cascade"
        TSMI_Item.Tag = "CWDS"
        FAddHandler(TSMI_Item, MD)
        DirectCast(MenuStrip1.Items(MenuStrip1.Items.Count - 1), ToolStripMenuItem).DropDownItems.Add(TSMI_Item)

        TSMI_Item = New ToolStripMenuItem("Tile Horizontal")
        TSMI_Item.Name = "CWDS_TileHorizontal"
        TSMI_Item.Tag = "CWDS"
        FAddHandler(TSMI_Item, MD)
        DirectCast(MenuStrip1.Items(MenuStrip1.Items.Count - 1), ToolStripMenuItem).DropDownItems.Add(TSMI_Item)

        TSMI_Item = New ToolStripMenuItem("Tile Vertical")
        TSMI_Item.Name = "CWDS_TileVertical"
        TSMI_Item.Tag = "CWDS"
        FAddHandler(TSMI_Item, MD)
        DirectCast(MenuStrip1.Items(MenuStrip1.Items.Count - 1), ToolStripMenuItem).DropDownItems.Add(TSMI_Item)

        TSSP_Item = New ToolStripSeparator()
        TSSP_Item.Name = "CWDS_SP1"
        TSSP_Item.Tag = "CWDS"
        DirectCast(MenuStrip1.Items(MenuStrip1.Items.Count - 1), ToolStripMenuItem).DropDownItems.Add(TSSP_Item)

        TSMI_Item = New ToolStripMenuItem("Close All")
        TSMI_Item.Name = "CWDS_CloseAll"
        TSMI_Item.Tag = "CWDS"
        FAddHandler(TSMI_Item, MD)
        DirectCast(MenuStrip1.Items(MenuStrip1.Items.Count - 1), ToolStripMenuItem).DropDownItems.Add(TSMI_Item)

        TSMI_Item = New ToolStripMenuItem("Exit")
        TSMI_Item.Name = "CWDS_Exit"
        TSMI_Item.Tag = "CWDS"
        FAddHandler(TSMI_Item, MD)
        DirectCast(MenuStrip1.Items(MenuStrip1.Items.Count - 1), ToolStripMenuItem).DropDownItems.Add(TSMI_Item)
    End Sub

    Public Sub FAddMenu(ByVal MD As MDIMain, Optional ByVal StrModule As String = "")
        Dim MenuStrip1 As New MenuStrip
        Dim ADTemp As OleDb.OleDbDataAdapter = Nothing
        Dim DTTemp As New DataTable
        Dim DTTemp1 As New DataTable
        Dim mParentMenu As String
        Dim I As Integer, Cnt As Integer
        Dim TSMIParent As ToolStripMenuItem

        Dim mQry$
        mQry = "SELECT P.MnuModule, P.MnuName, P.MnuText, P.MainStreamCode, P.ReportFor     FROM User_Permission P WHERE P.UserName='" & IIf(AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName), "SA", AgL.PubUserName) & "' And IsNull(P.Active,'Y')='Y' And IsNull(P.Permission,'****') <> '****' ORDER BY P.MainStreamCode "
        DtMenu = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRemoveMenu(MD)


        DTTemp = AgL.FillData("Select P.UserName+P.MnuName+P.MnuModule as SearchKey, 	P.UserName,P.MnuModule,P.MnuName,P.MnuText,P.SNo,P.MnuLevel,P.Parent,P.Permission,P.ReportFor,P.Active,P.RowId,P.UpLoadDate,User_Module.MainStreamCode,User_Module.GroupLevel From User_Permission P Left Join (Select MnuName+MnuModule As SearchKey,MainStreamCode, GroupLevel From User_Permission Where UserName='SA') As User_Module On P.MnuName+P.MnuModule=User_Module.SearchKey Where P.UserName='" & IIf(AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName), "SA", AgL.PubUserName) & "' And IsNull(P.Active,'Y')='Y' And IsNull(P.Permission,'****') <> '****' Order By User_Module.MainStreamCode", AgL.ECompConn).Tables(0)

        Try
            For I = 0 To DTTemp.Rows.Count - 1
                StrModule = AgL.XNull(DTTemp.Rows(I)("MnuModule"))
                If StrModule = "Utility" And AgL.XNull(DTTemp.Rows(I).Item("MnuName")) = "MnuStructure" Then
                    Exit For
                End If
                Select Case Len(AgL.XNull(DTTemp.Rows(I)("MainStreamCode")))
                    Case 3
                        mParentMenu = AgL.XNull(DTTemp.Rows(I).Item("MnuName"))
                        MenuStrip1.Items.Add(AgL.XNull(DTTemp.Rows(I).Item("MnuText")))
                        MenuStrip1.Items(Cnt).Name = AgL.XNull(DTTemp.Rows(I).Item("MnuName"))
                        MenuStrip1.Items(Cnt).Tag = AgL.XNull(DTTemp.Rows(I).Item("ReportFor"))
                        MenuStrip1.Items(Cnt).ToolTipText = StrModule
                        FAddHandler(MenuStrip1.Items(Cnt), MD)
                        MD.AgSideBar1.Buttons.Add(AgL.XNull(DTTemp.Rows(I).Item("MnuText")))
                        MD.AgSideBar1.Buttons(Cnt).Tag = AgL.XNull(DTTemp.Rows(I).Item("MainStreamCode"))
                        ArrButtonModuleName(Cnt) = AgL.XNull(DTTemp.Rows(I).Item("MnuModule"))
                        ReDim Preserve ArrButtonModuleName(UBound(ArrButtonModuleName) + 1)
                        TSMIParent = DirectCast(MenuStrip1.Items(Cnt), ToolStripMenuItem)
                        FAddChildMenu(TSMIParent, AgL.XNull(DTTemp.Rows(I).Item("MnuName")), DTTemp, StrModule, MD)
                        Cnt += 1
                End Select
            Next
            FAddMenu_Windows(MD, MenuStrip1)
            MD.Controls.Add(MenuStrip1)
            If MD.AgSideBar1.Buttons.Count > 0 Then
                MD.AgSideBar1.Height = MD.AgSideBar1.Buttons(0).Height * MD.AgSideBar1.Buttons.Count + 100
                If DTTemp.Rows.Count > 0 Then
                    MD.TreeView1.Tag = AgL.XNull(DTTemp.Rows(0)("MnuModule"))
                    MD.Fill_PermissionTree(DTTemp.Rows(0)("MnuModule"), MD.AgSideBar1.Buttons(0).Tag)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub





    Private Sub FAddChildMenu(ByVal TSMI As ToolStripMenuItem, ByVal ParentName As String, ByVal DtMenu As DataTable, ByVal StrModule As String, ByVal MD As MDIMain)
        Dim PkCol(1) As DataColumn
        Dim PkCol1(1) As DataColumn
        Dim DtSubMenu As DataTable
        Dim DtSubMenu1 As DataTable
        Dim TSMI_Item As ToolStripMenuItem = Nothing
        Dim StrTemp As String
        Dim I As Integer

        Try

            DtSubMenu = DtMenu.Copy
            DtSubMenu1 = DtMenu.Copy

            PkCol(0) = DtSubMenu.Columns(0)
            DtSubMenu.PrimaryKey = PkCol

            PkCol1(0) = DtSubMenu1.Columns(0)
            DtSubMenu1.PrimaryKey = PkCol1

            DtSubMenu.DefaultView.RowFilter = Nothing
            DtSubMenu.DefaultView.RowFilter = "[Parent] = '" + ParentName + "' And [MnuModule] = '" & StrModule & "' "

            For I = 0 To DtSubMenu.DefaultView.Count - 1
                StrTemp = AgL.XNull(DtSubMenu.DefaultView.Item(I)("MnuText"))
                TSMI_Item = New ToolStripMenuItem(StrTemp)
                StrTemp = AgL.XNull(DtSubMenu.DefaultView.Item(I)("MnuName"))
                TSMI_Item.Name = StrTemp
                TSMI_Item.Tag = AgL.XNull(DtSubMenu.DefaultView.Item(I)("ReportFor"))
                TSMI_Item.ToolTipText = StrModule
                FAddHandler(TSMI_Item, MD)


                DtSubMenu1.DefaultView.RowFilter = Nothing
                DtSubMenu1.DefaultView.RowFilter = "[Parent] = '" + TSMI_Item.Name + "' And [MnuModule] = '" & StrModule & "' "

                If DtSubMenu1.DefaultView.Count > 0 Then
                    FAddChildMenu(TSMI_Item, AgL.XNull(DtSubMenu.DefaultView.Item(I)("MnuName")), DtMenu, StrModule, MD)
                End If
                TSMI.DropDownItems.Add(TSMI_Item)
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'TSMI_Item = Nothing
        End Try
    End Sub


    Private Sub FRemoveHandler(ByVal MnuStrp As MenuStrip, ByVal MD As MDIMain)
        RemoveHandler MnuStrp.Click, AddressOf MD.FMenuItem_Click
    End Sub

    Private Sub FRemoveHandler(ByVal TSMI_Var As ToolStripMenuItem, ByVal MD As MDIMain)
        RemoveHandler TSMI_Var.Click, AddressOf MD.FMenuItem_Click
    End Sub

    Private Sub FAddHandler(ByVal MnuStrp As MenuStrip, ByVal MD As MDIMain)
        FRemoveHandler(MnuStrp, MD)
        AddHandler MnuStrp.Click, AddressOf MD.FMenuItem_Click
    End Sub

    Private Sub FAddHandler(ByVal TSMI_Var As ToolStripMenuItem, ByVal MD As MDIMain)
        FRemoveHandler(TSMI_Var, MD)
        AddHandler TSMI_Var.Click, AddressOf MD.FMenuItem_Click
    End Sub

    Private Sub FAddTSMI(ByVal MnuStrp As MenuStrip, ByVal Index As Integer, ByVal StrUser As String, _
    ByVal StrParent As String, ByVal StrModule As String, ByVal MD As MDIMain)
        Dim ADTemp As OleDb.OleDbDataAdapter = Nothing
        Dim DTTemp As New DataTable
        Dim DTTemp1 As New DataTable
        Dim I As Integer
        Dim TSMI_Item As ToolStripMenuItem
        Dim StrTemp As String

        StrTemp = "Select * From User_Permission Where UserName='" & StrUser & "' And Parent='" & StrParent & "' And MnuModule='" & StrModule & "' And IsNull(Active,'Y')='Y'  And IsNull(Permission,'****') <> '****'  Order By SNo"
        DTTemp = AgL.FillData(StrTemp, AgL.ECompConn).Tables(0)
        For I = 0 To DTTemp.Rows.Count - 1
            Try
                StrTemp = AgL.XNull(DTTemp.Rows(I).Item("MnuText"))
                TSMI_Item = New ToolStripMenuItem(StrTemp)
                StrTemp = AgL.XNull(DTTemp.Rows(I).Item("MnuName"))
                TSMI_Item.Name = StrTemp
                TSMI_Item.Tag = AgL.XNull(DTTemp.Rows(I).Item("ReportFor"))
                TSMI_Item.ToolTipText = StrModule
                FAddHandler(TSMI_Item, MD)

                DTTemp1.Clear()
                DTTemp1 = AgL.FillData("Select * From User_Permission Where UserName='" & StrUser & "' And MnuName='" & AgL.XNull(DTTemp.Rows(I).Item("MnuName")) & "' And MnuModule='" & StrModule & "' And IsNull(Active,'Y')='Y' And IsNull(Permission,'****') <> '****'  Order By SNo", AgL.ECompConn).Tables(0)
                'If DTTemp1.Rows.Count > 0 Then
                'FAddTSMI_DropDown(TSMI_Item, StrUser, AgL.XNull(DTTemp.Rows(I).Item("MnuName")), StrModule, MD)
                'End If

                'DirectCast(MnuStrp.Items(Index), ToolStripMenuItem).DropDownItems.Add(TSMI_Item)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                TSMI_Item = Nothing
            End Try
        Next
    End Sub

    Private Sub FAddTSMI_DropDown(ByVal TSMI_Var As ToolStripMenuItem, ByVal StrUser As String, _
        ByVal StrParent As String, ByVal StrModule As String, ByVal MD As MDIMain)
        Dim TSMI_Item As ToolStripMenuItem
        Dim ADTemp As OleDb.OleDbDataAdapter = Nothing
        Dim DTTemp As New DataTable
        Dim I As Integer
        Dim StrTemp As String
        Dim DTTemp1 As New DataTable

        DTTemp = AgL.FillData("Select * From User_Permission Where UserName='" & StrUser & "' And Parent='" & StrParent & "' And MnuModule='" & StrModule & "' And IsNull(Active,'Y')='Y' And IsNull(Permission,'****') <> '****'  Order By SNo", AgL.ECompConn).Tables(0)
        For I = 0 To DTTemp.Rows.Count - 1
            Try
                StrTemp = AgL.XNull(DTTemp.Rows(I).Item("MnuText"))
                TSMI_Item = New ToolStripMenuItem(StrTemp)
                StrTemp = AgL.XNull(DTTemp.Rows(I).Item("MnuName"))
                TSMI_Item.Name = StrTemp
                TSMI_Item.Tag = AgL.XNull(DTTemp.Rows(I).Item("ReportFor"))
                TSMI_Item.ToolTipText = StrModule
                FAddHandler(TSMI_Item, MD)

                DTTemp1.Clear()
                DTTemp1 = AgL.FillData("Select * From User_Permission Where UserName='" & StrUser & "' And MnuName='" & AgL.XNull(DTTemp.Rows(I).Item("MnuName")) & "' And MnuModule='" & StrModule & "' And IsNull(Active,'Y')='Y' Order By SNo", AgL.ECompConn).Tables(0)
                If DTTemp1.Rows.Count > 0 Then
                    FAddTSMI_DropDown(TSMI_Item, StrUser, AgL.XNull(DTTemp.Rows(I).Item("MnuName")), StrModule, MD)
                End If

                TSMI_Var.DropDownItems.Add(TSMI_Item)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                TSMI_Item = Nothing
            End Try
        Next
    End Sub

    '======================= Remove MDI Menu ======================
    Public Sub FRemoveMenu(ByVal ObjFor As MDIMain)
        Dim Mnu As Object

        For Each Mnu In ObjFor.Controls
            If Mnu.GetType.ToString = "System.Windows.Forms.MenuStrip" Then
                'FRemoveMenu(Mnu, ObjFor)
                ObjFor.Controls.Remove(Mnu)
                'FRemoveHandler(Mnu, ObjFor)
            End If
        Next
    End Sub

    Public Sub FRemoveMenu(ByRef MnuStrp As MenuStrip, ByVal ObjFor As Object)
        Dim TSI_Main As ToolStripItem
        Dim TSMI_Main As ToolStripMenuItem

        For Each TSI_Main In MnuStrp.Items
            If TSI_Main.GetType.ToString = "System.Windows.Forms.ToolStripMenuItem" Then
                TSMI_Main = TSI_Main
                FRemoveMenu(TSMI_Main.DropDownItems, ObjFor)
            End If
        Next

    End Sub

    Public Sub FRemoveMenu(ByRef Menus As ToolStripItemCollection, ByVal ObjFor As MDIMain)
        Dim TSI_Main As ToolStripItem
        Dim TSMI_Main As ToolStripMenuItem

        For Each TSI_Main In Menus
            If TSI_Main.GetType.ToString = "System.Windows.Forms.ToolStripMenuItem" Then
                TSMI_Main = TSI_Main
                FRemoveMenu(TSMI_Main.DropDownItems, ObjFor)
            End If
            Menus.Remove(TSI_Main)
        Next
    End Sub

#End Region


    Public Function IsConnectionAvailable() As Boolean
        Try
            If My.Computer.Network.Ping("www.google.com", 1000) Then
                IsConnectionAvailable = True
            End If
        Catch
        End Try

    End Function


End Module