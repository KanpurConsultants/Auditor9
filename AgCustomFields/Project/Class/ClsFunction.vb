Public Class ClsFunction
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal

    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, Optional ByVal IsEntryPoint As Boolean = True)
        Dim FrmObj As Form
        Dim StrUserPermission As String
        Dim DTUP As New DataTable
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim MDI As New MDIMain
        Dim mQry As String
        Dim bSkipUserControlPermissionFlag As Boolean = False
        'For User Permission Open

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "SA") Then
            StrUserPermission = "AEDP"
        Else
            StrUserPermission = "****"
        End If

        mQry = "Select Permission From User_Permission Where UserName='" & AgL.PubUserName & "' And MnuModule='Utility' And MnuName='" & StrSender & "'"
        DTUP = AgL.FillData(mQry, AgL.GcnMain).tables(0)
        If DTUP.Rows.Count > 0 Then
            StrUserPermission = AgL.XNull(DTUP.Rows(0).Item("Permission"))
        End If
        DTUP.Clear()
        DTUP = Nothing

        If AgL.PubOfflineApplicable And AgL.PubSiteCode <> AgL.PubSiteCodeActual Then
            StrUserPermission = Replace(Replace(Replace(StrUserPermission, "A", "*"), "E", "*"), "D", "*")
            If bSkipUserControlPermissionFlag = False Then bSkipUserControlPermissionFlag = True
        End If

        mQry = "SELECT " & IIf(AgL.PubOfflineApplicable, "Ep.IsOnLineEntry", "Ep.IsOffLineEntry") & "  " & _
                " FROM EntryPointPermission Ep " & _
                " WHERE  MnuModule='Utility' And MnuName='" & StrSender & "'"

        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = False Then
            StrUserPermission = Replace(Replace(Replace(StrUserPermission, "A", "*"), "E", "*"), "D", "*")
            If bSkipUserControlPermissionFlag = False Then bSkipUserControlPermissionFlag = True
        End If

        If bSkipUserControlPermissionFlag Then
            DTUP = Nothing
        Else
            mQry = "Select GroupText As UP From User_Control_Permission Where UserName='" & AgL.PubUserName & "' And MnuModule='Utility' And MnuName='" & StrSender & "'"
            DTUP = AgL.FillData(mQry, AgL.ECompConn).Tables(0)
        End If
        'For User Permission End 

        If AgL.PubIsHo Then
            StrUserPermission = "****"
            DTUP = Nothing
        End If

        If IsEntryPoint Then
            Select Case StrSender

                Case MDI.MnuStructureMaster.Name
                    FrmObj = New FrmCustomFields(StrUserPermission, DTUP)

                Case MDI.MnuChargesMasterToolStripMenuItem.Name
                    FrmObj = New FrmHead(StrUserPermission, DTUP)

                Case Else
                    FrmObj = Nothing
            End Select
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

