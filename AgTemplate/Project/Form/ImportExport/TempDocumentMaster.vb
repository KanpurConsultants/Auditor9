Imports System.Data.SQLite
Public Class TempDocumentMaster
    Inherits AgTemplate.TempMaster
    Dim mQry$

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.TxtNature = New AgControls.AgTextBox
        Me.LblNature = New System.Windows.Forms.Label
        Me.LblNamrReq = New System.Windows.Forms.Label
        Me.TxtName = New AgControls.AgTextBox
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
        'TxtNature
        '
        Me.TxtNature.AgMandatory = False
        Me.TxtNature.AgMasterHelp = True
        Me.TxtNature.AgNumberLeftPlaces = 0
        Me.TxtNature.AgNumberNegetiveAllow = False
        Me.TxtNature.AgNumberRightPlaces = 0
        Me.TxtNature.AgPickFromLastValue = False
        Me.TxtNature.AgRowFilter = ""
        Me.TxtNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNature.AgSelectedValue = Nothing
        Me.TxtNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNature.Location = New System.Drawing.Point(402, 120)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(191, 18)
        Me.TxtNature.TabIndex = 672
        '
        'LblNature
        '
        Me.LblNature.AutoSize = True
        Me.LblNature.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNature.Location = New System.Drawing.Point(269, 123)
        Me.LblNature.Name = "LblNature"
        Me.LblNature.Size = New System.Drawing.Size(46, 16)
        Me.LblNature.TabIndex = 681
        Me.LblNature.Text = "Nature"
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
        'TxtName
        '
        Me.TxtName.AgMandatory = True
        Me.TxtName.AgMasterHelp = True
        Me.TxtName.AgNumberLeftPlaces = 0
        Me.TxtName.AgNumberNegetiveAllow = False
        Me.TxtName.AgNumberRightPlaces = 0
        Me.TxtName.AgPickFromLastValue = False
        Me.TxtName.AgRowFilter = ""
        Me.TxtName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtName.AgSelectedValue = Nothing
        Me.TxtName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.Location = New System.Drawing.Point(402, 100)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(191, 18)
        Me.TxtName.TabIndex = 665
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
        Me.Controls.Add(Me.TxtNature)
        Me.Controls.Add(Me.LblNature)
        Me.Controls.Add(Me.LblNamrReq)
        Me.Controls.Add(Me.TxtName)
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
        Me.Controls.SetChildIndex(Me.TxtName, 0)
        Me.Controls.SetChildIndex(Me.LblNamrReq, 0)
        Me.Controls.SetChildIndex(Me.LblNature, 0)
        Me.Controls.SetChildIndex(Me.TxtNature, 0)
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
    Friend WithEvents TxtName As AgControls.AgTextBox
    Friend WithEvents LblNamrReq As System.Windows.Forms.Label
    Friend WithEvents LblNature As System.Windows.Forms.Label
    Friend WithEvents TxtNature As AgControls.AgTextBox


#End Region

    Private Sub FrmDocument_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtName, LblName.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From IE_Doc Where Description='" & TxtName.Text & "' And " & ClsMain.RetDivFilterStr & ""
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Document Name Already Exists")
        Else
            mQry = "Select count(*) From IE_Doc Where Description='" & TxtName.Text & "' And Code<>'" & mInternalCode & "' And " & ClsMain.RetDivFilterStr & ""
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Document Name Already Exists")
        End If
    End Sub

    Private Sub FrmDocument_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " "
        AgL.PubFindQry = "SELECT UID, Description [Document Description], Nature " &
                        " FROM IE_Doc_Log " & mConStr &
                        " And EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  "
        AgL.PubFindQryOrdBy = "[Document Description]"
    End Sub

    Private Sub FrmDocument_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " "
        AgL.PubFindQry = "SELECT Code, Description [Document Description], Nature " &
                        " FROM IE_Doc " & mConStr &
                        " And IfNull(IsDeleted,0)=0 "
        AgL.PubFindQryOrdBy = "[Document Description]"
    End Sub

    Private Sub FrmDocument_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "IE_Doc"
        LogTableName = "IE_Doc_LOG"
    End Sub

    Private Sub FrmDocument_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "Update IE_Doc_Log " &
                "   SET  " &
                "	Description = " & AgL.Chk_Text(TxtName.Text) & ", " &
                "	Nature = " & AgL.Chk_Text(TxtNature.Text) & " " &
                "   Where UID = '" & SearchCode & "' "

        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name , Div_Code " &
            " From IE_Doc  " &
            " Order By Description"
        TxtName.AgHelpDataSet(1) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Distinct Nature  as Code, Nature as Name, Div_Code " &
                "  From IE_Doc  " &
                "  WHERE Nature Is Not Null" &
                "  Order By Nature"
        TxtNature.AgHelpDataSet(1) = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub FrmDocument_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " "
        mQry = "Select Code As SearchCode " &
            " From IE_Doc " & mConStr &
            " And IfNull(IsDeleted,0)=0 Order By Description "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmDocument_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " "
        mQry = "Select UID As SearchCode " &
               " From IE_Doc_log " & mConStr &
               " And EntryStatus='" & LogStatus.LogOpen & "' Order By Description"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select * " &
                " From IE_Doc Where Code='" & SearchCode & "'"
        Else
            mQry = "Select * " &
                " From IE_Doc_Log Where UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtName.Text = AgL.XNull(.Rows(0)("Description"))
                TxtNature.Text = AgL.XNull(.Rows(0)("Nature"))
            End If
        End With
        Topctrl1.tPrn = False
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtName.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtName.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtName.Enter, TxtNature.Enter
        Try
            Select Case sender.name
                Case TxtName.Name, TxtNature.Name
                    sender.AgRowFilter = ClsMain.RetDivFilterStr
            End Select
        Catch ex As Exception
        End Try
    End Sub
End Class
