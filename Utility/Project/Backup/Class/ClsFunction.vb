Public Class ClsFunction
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal
    Dim CRepProc As ClsReportProcedures

    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, Optional ByVal IsEntryPoint As Boolean = True, _
                            Optional ByVal StrUserPermission As String = "", Optional ByVal DTUP As DataTable = Nothing)
        Dim FrmObj As Form
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim MDI As New MDIMain
        'Dim StrUserPermission As String = ""
        'Dim DTUP As DataTable = Nothing
        'Dim mQry$ = ""

        ''For User Permission Open
        'If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
        '    StrUserPermission = "AEDP"
        'Else
        '    StrUserPermission = "****"
        'End If

        'mQry = "Select Permission From User_Permission Where UserName='" & AgL.PubUserName & "' And MnuModule='Utility' And MnuName='" & StrSender & "'"
        'DTUP = AgL.FillData(mQry, AgL.ECompConn).tables(0)
        'If DTUP.Rows.Count > 0 Then
        '    StrUserPermission = AgL.XNull(DTUP.Rows(0).Item("Permission"))
        'End If
        'DTUP.Clear()
        'DTUP = Nothing

        'If AgL.PubOfflineApplicable And AgL.PubSiteCode <> AgL.PubSiteCodeActual Then
        '    StrUserPermission = Replace(Replace(Replace(StrUserPermission, "A", "*"), "E", "*"), "D", "*")
        'End If

        'If AgL.IsTableExist("EntryPointPermission", AgL.GCn) Then
        '    mQry = "SELECT " & IIf(AgL.PubOfflineApplicable, "Ep.IsOnLineEntry", "Ep.IsOffLineEntry") & "  " & _
        '            " FROM EntryPointPermission Ep " & _
        '            " WHERE  MnuModule='Utility' And MnuName='" & StrSender & "'"

        '    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = False Then
        '        StrUserPermission = Replace(Replace(Replace(StrUserPermission, "A", "*"), "E", "*"), "D", "*")
        '    End If
        'End If

        'mQry = "Select GroupText As UP From User_Control_Permission Where UserName='" & IIf(AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName), "SA", AgL.PubUserName) & "' And MnuModule='Utility' And MnuName='" & StrSender & "'"
        'DTUP = AgL.FillData(mQry, AgL.ECompConn).Tables(0)
        ''For User Permission End 

        'If AgL.PubIsHo Then
        '    StrUserPermission = "****"
        '    DTUP = Nothing
        'End If

        'For User Permission Open
        If StrUserPermission.Trim = "" Then
            StrUserPermission = AgIniVar.FunGetUserPermission(AgLibrary.ClsConstant.Module_Utility, StrSender, StrSenderText, DTUP)
        End If
        ''For User Permission End


        If IsEntryPoint Then
            Select Case StrSender
                Case MDI.MnuUserMaster.Name
                    FrmObj = New FrmUser(StrUserPermission, DTUP, AgL)

                Case MDI.MnuUserPermission.Name
                    FrmObj = New FrmUserPermission(StrUserPermission, DTUP, AgL)

                Case MDI.MnuUserControlPermission.Name
                    FrmObj = New FrmUserControlPermission(StrUserPermission, DTUP, AgL)

                Case MDI.MnuUserTarget.Name
                    FrmObj = New FrmUserTarget(StrUserPermission, DTUP)

                Case MDI.MnuTableKeys.Name
                    FrmObj = New FrmTableSearchFields

                Case MDI.MnuReportMaster.Name
                    FrmObj = New FrmReportMaster

                Case MDI.MnuDataSynchronisation.Name
                    FrmObj = New FrmDataSynchronisation

                Case MDI.MnuBackupDatabase.Name
                    FrmObj = New AgLibrary.FrmBackupDatase(AgL)

                Case MDI.MnuDataNotSynchronised.Name
                    FrmObj = New FrmSynchroniseError

                Case MDI.MnuUserLoginPasswardChange.Name
                    FrmObj = New FrmUserPasswardChange("*E**", DTUP, AgL)

                Case Else
                    FrmObj = Nothing
            End Select
        Else
            ObjRepFormGlobal = New AgLibrary.RepFormGlobal(AgL)
            CRepProc = New ClsReportProcedures(ObjRepFormGlobal)
            CRepProc.GRepFormName = Replace(Replace(StrSenderText, "&", ""), " ", "")
            CRepProc.Ini_Grid()
            FrmObj = ObjRepFormGlobal
        End If
        If FrmObj IsNot Nothing Then
            FrmObj.Text = StrSenderText
        End If
        Return FrmObj
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    
End Class

