Public Class ClsMDIFunctions
    Dim MainMnuCounter As Integer
    Dim SubMnuCounter As Integer
    Dim LeafMnuCounter As Integer
    Dim MnuMainStreamCode As String
    Dim MnuGroupLevel As Integer
    Dim Agl As ClsMain

    Public Sub New(ByVal AglVar As ClsMain)
        Agl = AglVar
    End Sub


    'Public Sub FGenerate_UP_Control(ByVal ObjFor As Object, ByVal StrModule As String, _
    'ByVal GCnCmd As SqlClient.SqlCommand)
    '    Dim FrmObj As Form
    '    Dim ObjCtrl As Object
    '    Dim ADMain As OleDb.OleDbDataAdapter = Nothing
    '    Dim DTMain As New DataTable
    '    Dim StrName As String = "", StrText As String = "", mQry As String = ""
    '    Dim I As Integer

    '    mQry = "Select MnuName,MnuText From User_Permission Where UserName='SA' And MnuModule='" & StrModule & "'"
    '    DTMain = AgL.FillData(mQry, AgL.ECompConn).tables(0)
    '    For I = 0 To DTMain.Rows.Count - 1
    '        StrName = AgL.XNull(DTMain.Rows(I).Item("MnuName"))
    '        StrText = AgL.XNull(DTMain.Rows(I).Item("MnuText"))
    '        FrmObj = ObjFor.CFOpen.FOpen(StrName, StrText)
    '        If Not IsNothing(FrmObj) Then
    '            For Each ObjCtrl In FrmObj.Controls
    '                If ObjCtrl.GetType.ToString = "System.Windows.Forms.GroupBox" Then
    '                    If Trim(UCase(ObjCtrl.Tag)) = "UP" Then
    '                        GCnCmd.CommandText = "Insert Into User_Control_Permission(UserName,MnuModule,MnuName,MnuText,GroupText,GroupName) Values" & _
    '                        "('SA','" & StrModule & "','" & StrName & "','" & StrText & "','" & ObjCtrl.Text & "','" & ObjCtrl.Name & "')"
    '                        GCnCmd.ExecuteNonQuery()
    '                    End If
    '                End If
    '            Next
    '        End If
    '        FrmObj = Nothing
    '    Next
    '    DTMain.Dispose()
    'End Sub

    Public Sub FGenerate_UP_Control(ByVal ObjFor As Object, ByVal StrModule As String, _
    ByVal GCnCmd As SqlClient.SqlCommand)
        Dim FrmObj As Form
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim DTMain As New DataTable
        Dim StrName As String = "", StrText As String = "", mQry As String = "", ControlPermissionGroupsText As String = ""
        Dim I As Integer

        mQry = "Select MnuName,MnuText,ControlPermissionGroups From User_Permission Where UserName='SA' And IsNull(Active,'Y') = 'Y' And MnuModule='" & StrModule & "'"
        DTMain = AgL.FillData(mQry, AgL.ECompConn).tables(0)
        For I = 0 To DTMain.Rows.Count - 1
            StrName = AgL.XNull(DTMain.Rows(I).Item("MnuName"))
            StrText = Agl.XNull(DTMain.Rows(I).Item("MnuText"))
            ControlPermissionGroupsText = Agl.XNull(DTMain.Rows(I).Item("ControlPermissionGroups"))
            FrmObj = ObjFor.CFOpen.FOpen(StrName, StrText)
            If Not IsNothing(FrmObj) Then
                FGenerate_UP_ContainerControl(FrmObj, StrModule, StrName, StrText, ControlPermissionGroupsText, GCnCmd)

                'For Each ObjCtrl In FrmObj.Controls
                '    If ObjCtrl.GetType.ToString = "System.Windows.Forms.GroupBox" Then
                '        If Trim(UCase(ObjCtrl.Tag)) = "UP" Then
                '            GCnCmd.CommandText = "Insert Into User_Control_Permission(UserName,MnuModule,MnuName,MnuText,GroupText,GroupName) Values" & _
                '            "('SA','" & StrModule & "','" & StrName & "','" & StrText & "','" & ObjCtrl.Text & "','" & ObjCtrl.Name & "')"
                '            GCnCmd.ExecuteNonQuery()
                '        End If
                '    End If
                'Next
            End If
            FrmObj = Nothing
        Next
        DTMain.Dispose()
    End Sub

    Private Sub FGenerate_UP_ContainerControl(ByRef ObjContainer As Object, ByVal StrModule As String, _
                                                ByVal StrMnuName As String, ByVal StrMnuText As String, ByVal StrControlPermissionGroups As String, _
                                                ByVal GCnCmd As SqlClient.SqlCommand)
        Dim ObjCtrl As Object, ObjTabPage As Object, ObjGBox As Object
        Dim mQry As String = ""

        For Each ObjCtrl In ObjContainer.Controls
            If ObjCtrl.GetType.ToString = "System.Windows.Forms.GroupBox" Then
                If UCase(StrControlPermissionGroups).Contains(Trim(UCase(ObjCtrl.Text))) And ObjCtrl.Text <> "" Then
                    GCnCmd.CommandText = "Insert Into User_Control_Permission(UserName,MnuModule,MnuName,MnuText,GroupText,GroupName) Values" & _
                                         "('SA','" & StrModule & "','" & StrMnuName & "','" & StrMnuText & "','" & ObjCtrl.Text & "','" & ObjCtrl.Name & "')"
                    GCnCmd.ExecuteNonQuery()

                ElseIf Trim(UCase(ObjCtrl.Tag)) = "UP" And StrControlPermissionGroups.ToString = "" Then 'User Permission for a perticular menu
                    GCnCmd.CommandText = "Insert Into User_Control_Permission(UserName,MnuModule,MnuName,MnuText,GroupText,GroupName) Values" & _
                                         "('SA','" & StrModule & "','" & StrMnuName & "','" & StrMnuText & "','" & ObjCtrl.Text & "','" & ObjCtrl.Name & "')"
                    GCnCmd.ExecuteNonQuery()
                End If
            Else
                If TypeOf ObjCtrl Is TabControl Then
                    For Each ObjTabPage In ObjCtrl.Controls
                        FGenerate_UP_ContainerControl(ObjTabPage, StrModule, StrMnuName, StrMnuText, StrControlPermissionGroups, GCnCmd)
                    Next
                End If
            End If
        Next
    End Sub

    Public Sub FGenerate_Apply_GlobalParameter(ByVal ObjFor As Object, ByVal StrModule As String, _
    ByVal GCnCmd As SqlClient.SqlCommand)
        Dim FrmObj As Form
        Dim ObjCtrl As Object
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim DTMain As New DataTable
        Dim StrName As String = "", StrText As String = "", mQry As String = ""
        Dim I As Integer

        mQry = "Select MnuName,MnuText From User_Permission Where UserName='SA' And IsNull(Active,'Y') = 'Y' And MnuModule='" & StrModule & "'"
        DTMain = Agl.FillData(mQry, Agl.ECompConn).tables(0)
        For I = 0 To DTMain.Rows.Count - 1
            StrName = Agl.XNull(DTMain.Rows(I).Item("MnuName"))
            StrText = Agl.XNull(DTMain.Rows(I).Item("MnuText"))
            FrmObj = ObjFor.CFOpen.FOpen(StrName, StrText)
            If Not IsNothing(FrmObj) Then
                For Each ObjCtrl In FrmObj.Controls
                    If ObjCtrl.GetType.ToString = "System.Windows.Forms.GroupBox" Then
                        If Trim(UCase(ObjCtrl.Tag)) = "UP" Then
                            GCnCmd.CommandText = "Insert Into User_Control_Permission(UserName,MnuModule,MnuName,MnuText,GroupText,GroupName) Values" & _
                            "('SA','" & StrModule & "','" & StrName & "','" & StrText & "','" & ObjCtrl.Text & "','" & ObjCtrl.Name & "')"
                            GCnCmd.ExecuteNonQuery()
                        End If
                    End If
                Next
            End If
            FrmObj = Nothing
        Next
        'ADMain.Dispose()
        DTMain.Dispose()
    End Sub


    Public Sub FGenerate_UP(ByVal ObjFor As Object, ByVal StrParent As String, _
    ByVal IntSno As Integer, ByVal StrMnuPath As String, ByVal GCnCmd As SqlClient.SqlCommand)
        Dim Mnu As Object
        For Each Mnu In ObjFor.Controls
            If Mnu.GetType.ToString = "System.Windows.Forms.MenuStrip" Then
                FRotateAllMenuItems(Mnu, Mnu.Name, StrParent, StrParent, IntSno, GCnCmd)
            End If
        Next
    End Sub

    Public Sub FInsertUP(ByVal StrParent As String, ByVal StrMnuText As String, ByVal StrMnuName As String, _
    ByVal StrMnuModule As String, ByVal IntSNo As Integer, ByVal IntLevel As String, ByVal ReportFor As String, ByVal ControlPermissionGroups As String)
        Dim GCnCmd As New SqlClient.SqlCommand
        Static Dim I As Integer

        I = I + 1
        GCnCmd.Connection = Agl.ECompConn
        GCnCmd.CommandText = "Insert Into User_Permission(UserName,Parent,MnuText,MnuName,Permission,SNo,MnuModule,MnuLevel,ReportFor, ControlPermissionGroups,MainStreamCode, GroupLevel, Active) Values " & _
                                     "('SA','" & StrParent & "','" & Replace(StrMnuText, "&", "") & "','" & StrMnuName & "','AEDP'," & I & ",'" & StrMnuModule & "'," & IntLevel & "," & Agl.Chk_Text(ReportFor) & ", " & Agl.Chk_Text(ControlPermissionGroups) & ", " & Agl.Chk_Text(MnuMainStreamCode) & ", " & MnuGroupLevel & ", 'Y')"
        GCnCmd.ExecuteNonQuery()
    End Sub

    Public Function FRotateAllMenuItems(ByRef MnuStrp As MenuStrip, ByVal StrMnuMain As String, ByVal StrModule As String, ByVal StrParent As String, _
    ByVal IntSno As Integer, ByVal GCnCmd As SqlClient.SqlCommand) As Integer
        Dim TSI_Main As ToolStripItem
        Dim TSMI_Main As ToolStripMenuItem
        Dim IntRtn As Integer
        Dim ReportFor As String


        For Each TSI_Main In MnuStrp.Items
            'If TSI_Main.Visible = True Then
            If TSI_Main.GetCurrentParent.Name = StrMnuMain Then
                IntSno = 0
                LeafMnuCounter = 0
                SubMnuCounter = 0
                MainMnuCounter += 1
                MnuMainStreamCode = Format(MainMnuCounter, "000").ToString
            End If

            If TSI_Main.Tag Is Nothing Or IsDBNull(TSI_Main.Tag) Then
                ReportFor = ""
            Else
                ReportFor = TSI_Main.Tag
            End If

            FInsertUP("", TSI_Main.Text, TSI_Main.Name, StrParent, IntSno, IntSno, ReportFor, TSI_Main.AccessibleDescription)


            If TSI_Main.GetType.ToString = "System.Windows.Forms.ToolStripMenuItem" Then
                TSMI_Main = TSI_Main
                IntRtn = FRotateAllMenuItems(TSMI_Main.DropDownItems, StrMnuMain, StrModule, TSMI_Main.Name, IntSno + 1, GCnCmd)
                If IntRtn <> 0 Then
                    IntSno = IntRtn
                End If
            End If
            'End If
        Next
        Return IntSno
    End Function

    Public Function FRotateAllMenuItems(ByRef Menus As ToolStripItemCollection, ByVal StrMnuMain As String, ByVal StrModule As String, ByVal StrParent As String, _
    ByVal IntSno As Integer, ByVal GCnCmd As SqlClient.SqlCommand) As Integer
        Dim TSI_Main As ToolStripItem
        Dim TSMI_Main As ToolStripMenuItem
        Dim ReportFor As String

        For Each TSI_Main In Menus
            'If TSI_Main.Visible = True Then
            If Trim(TSI_Main.Text) <> "" Then
                If TSI_Main.Tag Is Nothing Or IsDBNull(TSI_Main.Tag) Then
                    ReportFor = ""
                Else
                    ReportFor = TSI_Main.Tag
                End If

                MnuMainStreamCode = Agl.FillData("Select MainStreamCode From User_Permission Where UserName = 'SA' And MnuModule = '" & StrModule & "' And MnuName='" & StrParent & "' ", Agl.ECompConn).Tables(0).Rows(0)(0)
                MnuMainStreamCode = MnuMainStreamCode + Format(IntSno, "000").ToString

                FInsertUP(StrParent, TSI_Main.Text, TSI_Main.Name, StrModule, IntSno, IntSno, ReportFor, TSI_Main.AccessibleDescription)
                If TSI_Main.GetType.ToString = "System.Windows.Forms.ToolStripMenuItem" Then
                    TSMI_Main = TSI_Main
                    IntSno = FRotateAllMenuItems(TSMI_Main.DropDownItems, StrMnuMain, StrModule, TSMI_Main.Name, IntSno + 1, GCnCmd)
                End If
            End If
            'End If
        Next
        Return IntSno
    End Function


    Public Sub FUpdateUserGroupLevels(ByVal mConn As SqlClient.SqlConnection, ByVal mCmd As SqlClient.SqlCommand)
        Agl.Dman_ExecuteNonQry("UPDATE User_Permission SET GroupLevel = X.GroupLevel " & _
                               "FROM (SELECT UserName, MainStreamCode, (SELECT Count(*) FROM User_Permission UP WHERE LEFT(UP.MainStreamCode, Len(P.mainStreamCode))=P.MainStreamCode AND UP.UserName =P.UserName) AS GroupLevel FROM User_Permission P WHERE P.UserName='SA') AS X " & _
                               "WHERE User_Permission.MainStreamCode =X.MainStreamCode AND User_Permission.UserName =X.UserName ", mConn, mCmd)
    End Sub

    Public Sub FManageEntryPointPermission(ByVal mConn As SqlClient.SqlConnection, ByVal mCmd As SqlClient.SqlCommand)
        Dim bQry$ = ""

        If Agl.IsTableExist("EntryPointPermission", mConn) Then
            bQry = "INSERT INTO EntryPointPermission " & _
                    " 	( " & _
                    " 	MnuModule, " & _
                    "   MnuName " & _
                    " 	) " & _
                    " SELECT " & _
                    " 	U.MnuModule, " & _
                    " 	U.MnuName " & _
                    " FROM User_Permission U " & _
                    " LEFT JOIN EntryPointPermission E ON E.MnuModule = U.MnuModule AND E.MnuName = U.MnuName " & _
                    " WHERE U.UserName = 'SA' AND E.MnuModule IS NULL "
            Agl.Dman_ExecuteNonQry(bQry, mConn, mCmd)

            bQry = "Delete From EntryPointPermission " & _
                    " Where MnuModule + MnuName Not In " & _
                    " (SELECT " & _
                    " 	U.MnuModule + U.MnuName " & _
                    " FROM User_Permission U " & _
                    " WHERE U.UserName = 'SA') "
            Agl.Dman_ExecuteNonQry(bQry, mConn, mCmd)
        End If

    End Sub



End Class
