Public Class FrmDataSynchronisation
    Dim mQry$

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 300, 300, 0, 0)
            IniGrid()
            FIniMaster()
            OptOfflineData.Checked = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IniGrid()
        '<Exicutable Code>
    End Sub

    Sub FIniMaster()
        '<Exicutable Code>
    End Sub

    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        Dim mTrans As Boolean
        Dim mSearchCode$ = ""

        If Not (AgL.StrCmp(AgL.PubUserName, "SA") Or _
                AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName)) Then Err.Raise(1, , "Permission Denied!...")

        If Not (OptOnlineData.Checked Or OptOfflineData.Checked) Then Err.Raise(1, , "Please Select Any Option For Synchronisation!...")

        If MsgBox("Sure to Synchronise?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            If OptOnlineData.Checked Then
                mSearchCode = "Online Data"
                AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString)                

            ElseIf OptOfflineData.Checked Then
                mSearchCode = "Offline Data"
                AgL.SynchroniseSiteOffineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
            End If


            AgL.ETrans.Commit()
            mTrans = False

            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgLibrary.ClsConstant.SynchroniseMode, AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, , mSearchCode + Space(1) + "Synchronisation", , , , , AgL.PubSiteCode, AgL.PubDivCode)

        Catch Ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(Ex.Message)
        End Try
    End Sub

End Class