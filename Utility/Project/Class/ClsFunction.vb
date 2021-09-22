Public Class ClsFunction
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal
    Dim CRepProc As ClsReportProcedures

    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, Optional ByVal IsEntryPoint As Boolean = True, _
                            Optional ByVal StrUserPermission As String = "", Optional ByVal DTUP As DataTable = Nothing)
        Dim FrmObj As Form
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim MDI As New MDIMain

        'For User Permission Open
        If StrUserPermission.Trim = "" Then
            StrUserPermission = AgIniVar.FunGetUserPermission(AgLibrary.ClsConstant.Module_Utility, StrSender, StrSenderText, DTUP)
        End If
        ''For User Permission End


        If IsEntryPoint Then
            Select Case StrSender
                '================  FORMS OPENING FOR STRUCTURE MANAGEMENT  =================
                Case MDI.MnuStructureHeadMaster.Name
                    FrmObj = New AgStructure.FrmCharges(StrUserPermission, DTUP)
                Case MDI.MnuStructureMaster.Name
                    FrmObj = New AgStructure.FrmStructure(StrUserPermission, DTUP)
                Case MDI.MnuTaxRateMaster.Name
                    FrmObj = New AgStructure.FrmTaxPostingGroupEntry
                Case MDI.MnuNCatMapping.Name
                    FrmObj = New AgStructure.FrmNCat(StrUserPermission, DTUP)

                    '================  FORMS OPENING FOR CUSTOM FIELD MANAGEMENT  =================
                Case MDI.MnuCustomFieldHeadMaster.Name
                    FrmObj = New AgCustomFields.FrmHead(StrUserPermission, DTUP)
                Case MDI.MnuCustomFieldMaster.Name
                    FrmObj = New AgCustomFields.FrmCustomFields(StrUserPermission, DTUP)




                    '================  FORMS OPENING FOR USER PERMISSIONS  =================
                Case MDI.MnuUserMaster.Name
                    FrmObj = New FrmUser(StrUserPermission, DTUP, AgL)
                Case MDI.MnuUserPermission.Name
                    FrmObj = New FrmUserPermission(StrUserPermission, DTUP, AgL)
                Case MDI.MnuUserControlPermission.Name
                    FrmObj = New FrmUserControlPermission(StrUserPermission, DTUP, AgL)
                Case MDI.MnuUserVoucherTypeRestriction.Name
                    FrmObj = New FrmVoucherTypeRestriction(StrUserPermission, DTUP)


                    '================  FORMS OPENING FOR DATABASE UTILITIES  =================
                Case MDI.MnuBackup.Name
                    FrmObj = New AgLibrary.FrmBackupDatase(AgL)



                    '================  FORMS OPENING FOR VOUCHER TYPE SETTINGS  =================
                Case MDI.MnuVoucherTypePrintSetting.Name
                    FrmObj = New FrmVoucherTypeSettingsPrint(StrUserPermission, DTUP)

                Case MDI.MnuVoucherTypeSetting.Name
                    FrmObj = New FrmVoucherTypeSettings(StrUserPermission, DTUP)

                Case MDI.MnuVoucherTypeMaster.Name
                    FrmObj = New FrmVoucherTypeMaster(StrUserPermission, DTUP)


                    '================  FORMS OPENING FOR COMPANY HIERARCHY  =================
                Case MDI.MnuCompanyMaster.Name
                    FrmObj = New FrmCompanyMaster(StrUserPermission, DTUP)
                Case MDI.MnuSiteBranchMaster.Name
                    FrmObj = New FrmSiteMaster(StrUserPermission, DTUP)
                Case MDI.MnuDivisionMaster.Name
                    FrmObj = New FrmDivision(StrUserPermission, DTUP)




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

