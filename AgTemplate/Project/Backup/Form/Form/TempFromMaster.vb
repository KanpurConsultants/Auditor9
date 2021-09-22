Public Class TempFormMaster
    Inherits AgTemplate.TempMaster
    Dim mQry$

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.TxtCategory = New AgControls.AgTextBox
        Me.LblCategory = New System.Windows.Forms.Label
        Me.LblNamrReq = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblName = New System.Windows.Forms.Label
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(863, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 237)
        Me.GroupBox1.Size = New System.Drawing.Size(905, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(6, 241)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(142, 241)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(556, 241)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 23)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(133, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(400, 241)
        Me.GBoxApprove.Size = New System.Drawing.Size(147, 44)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Size = New System.Drawing.Size(89, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(118, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(702, 241)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(271, 241)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'TxtCategory
        '
        Me.TxtCategory.AgMandatory = False
        Me.TxtCategory.AgMasterHelp = True
        Me.TxtCategory.AgNumberLeftPlaces = 0
        Me.TxtCategory.AgNumberNegetiveAllow = False
        Me.TxtCategory.AgNumberRightPlaces = 0
        Me.TxtCategory.AgPickFromLastValue = False
        Me.TxtCategory.AgRowFilter = ""
        Me.TxtCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCategory.AgSelectedValue = Nothing
        Me.TxtCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCategory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCategory.Location = New System.Drawing.Point(402, 120)
        Me.TxtCategory.MaxLength = 20
        Me.TxtCategory.Name = "TxtCategory"
        Me.TxtCategory.Size = New System.Drawing.Size(191, 18)
        Me.TxtCategory.TabIndex = 672
        '
        'LblCategory
        '
        Me.LblCategory.AutoSize = True
        Me.LblCategory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCategory.Location = New System.Drawing.Point(269, 123)
        Me.LblCategory.Name = "LblCategory"
        Me.LblCategory.Size = New System.Drawing.Size(46, 16)
        Me.LblCategory.TabIndex = 681
        Me.LblCategory.Text = "Category"
        '
        'LblNamrReq
        '
        Me.LblNamrReq.AutoSize = True
        Me.LblNamrReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblNamrReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblNamrReq.Location = New System.Drawing.Point(386, 108)
        Me.LblNamrReq.Name = "LblNamrReq"
        Me.LblNamrReq.Size = New System.Drawing.Size(10, 7)
        Me.LblNamrReq.TabIndex = 666
        Me.LblNamrReq.Text = "Ä"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgMandatory = True
        Me.TxtDescription.AgMasterHelp = True
        Me.TxtDescription.AgNumberLeftPlaces = 0
        Me.TxtDescription.AgNumberNegetiveAllow = False
        Me.TxtDescription.AgNumberRightPlaces = 0
        Me.TxtDescription.AgPickFromLastValue = False
        Me.TxtDescription.AgRowFilter = ""
        Me.TxtDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDescription.AgSelectedValue = Nothing
        Me.TxtDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(402, 100)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(191, 18)
        Me.TxtDescription.TabIndex = 665
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(269, 103)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(42, 16)
        Me.LblName.TabIndex = 661
        Me.LblName.Text = "Name"
        '
        'FrmDocument
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(863, 285)
        Me.Controls.Add(Me.TxtCategory)
        Me.Controls.Add(Me.LblCategory)
        Me.Controls.Add(Me.LblNamrReq)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblName)
        Me.Name = "FrmDocument"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblName, 0)
        Me.Controls.SetChildIndex(Me.TxtDescription, 0)
        Me.Controls.SetChildIndex(Me.LblNamrReq, 0)
        Me.Controls.SetChildIndex(Me.LblCategory, 0)
        Me.Controls.SetChildIndex(Me.TxtCategory, 0)
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblName As System.Windows.Forms.Label
    Friend WithEvents TxtDescription As AgControls.AgTextBox
    Friend WithEvents LblNamrReq As System.Windows.Forms.Label
    Friend WithEvents LblCategory As System.Windows.Forms.Label
    Friend WithEvents TxtCategory As AgControls.AgTextBox


#End Region

    Private Sub FrmDocument_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtDescription, LblName.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Form_Master Where Description='" & TxtDescription.Text & "' And " & ClsMain.RetDivFilterStr & ""
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Document Name Already Exists")
        Else
            mQry = "Select count(*) From Form_Master Where Description='" & TxtDescription.Text & "' And Code<>'" & mInternalCode & "' And " & ClsMain.RetDivFilterStr & ""
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Document Name Already Exists")
        End If
    End Sub

    Private Sub FrmDocument_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "H.Div_Code") & " "
        'AgL.PubFindQry = "SELECT UID, Description [Document Description], Category " & _
        '                " FROM Form_Master_Log " & mConStr & _
        '                " And EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  "

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.Description AS Form, H.Category, H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], " & _
                    " H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date],  " & _
                    " H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status,D.Div_Name AS Division  " & _
                    " FROM  Form_Master_Log H " & _
                    " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & mConStr & _
                    " And H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  "

        AgL.PubFindQryOrdBy = "[Form]"
    End Sub

    Private Sub FrmDocument_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "H.Div_Code") & " "
        'AgL.PubFindQry = "SELECT Code, Description [Document Description], Category " & _
        '                " FROM Form_Master " & mConStr & _
        '                " And IsNull(IsDeleted,0)=0 "

        AgL.PubFindQry = " SELECT H.Code AS SearchCode, H.Description AS Form, H.Category, H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], " & _
                            " H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date],  " & _
                            " H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status,D.Div_Name AS Division  " & _
                            " FROM  Form_Master H " & _
                            " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & mConStr & _
                            " And IsNull(H.IsDeleted,0)=0 "

        AgL.PubFindQryOrdBy = "[Form]"
    End Sub

    Private Sub FrmDocument_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Form_Master"
        LogTableName = "Form_Master_LOG"
    End Sub

    Private Sub FrmDocument_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "Update Form_Master_Log " & _
                "   SET  " & _
                "	Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " & _
                "	Category = " & AgL.Chk_Text(TxtCategory.Text) & " " & _
                "   Where UID = '" & SearchCode & "' "

        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name , Div_Code " & _
            " From Form_Master  " & _
            " Order By Description"
        TxtDescription.AgHelpDataSet(1) = AgL.FillData(mQry, AgL.GCn)


        mQry = " Select 'Road Permit' As Code, 'Road Permit' As Category " & _
                " Union All " & _
                " Select 'From C' As Code, 'From C' As Category " & _
                " Union All " & _
                " Select 'From H' As Code, 'From H' As Category "

        TxtCategory.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub FrmDocument_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " "
        mQry = "Select Code As SearchCode " & _
            " From Form_Master " & mConStr & _
            " And IsNull(IsDeleted,0)=0 Order By Description "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmDocument_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " "
        mQry = "Select UID As SearchCode " & _
               " From Form_Master_log " & mConStr & _
               " And EntryStatus='" & LogStatus.LogOpen & "' Order By Description"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select * " & _
                " From Form_Master Where Code='" & SearchCode & "'"
        Else
            mQry = "Select * " & _
                " From Form_Master_Log Where UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtCategory.Text = AgL.XNull(.Rows(0)("Category"))
            End If
        End With
        Topctrl1.tPrn = False
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDescription.Enter, TxtCategory.Enter
        Try
            Select Case sender.name
                Case TxtDescription.Name
                    sender.AgRowFilter = ClsMain.RetDivFilterStr
            End Select
        Catch ex As Exception
        End Try
    End Sub
End Class
