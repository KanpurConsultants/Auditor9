Public Class TempQCGroup
    Inherits AgTemplate.TempMaster
    Public mQry$

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Parameter As String = "Parameter"
    Protected Const Col1StdValue As String = "Standard Value"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.LblLQCGroupReq = New System.Windows.Forms.Label
        Me.TxtLQCGroup = New AgControls.AgTextBox
        Me.LblLQCGroup = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
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
        Me.Topctrl1.Size = New System.Drawing.Size(862, 41)
        Me.Topctrl1.TabIndex = 2
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 398)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(6, 402)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(142, 402)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(556, 402)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 23)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(133, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(400, 402)
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
        Me.GroupBox2.Location = New System.Drawing.Point(702, 402)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(271, 402)
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
        'LblLQCGroupReq
        '
        Me.LblLQCGroupReq.AutoSize = True
        Me.LblLQCGroupReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblLQCGroupReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblLQCGroupReq.Location = New System.Drawing.Point(292, 76)
        Me.LblLQCGroupReq.Name = "LblLQCGroupReq"
        Me.LblLQCGroupReq.Size = New System.Drawing.Size(10, 7)
        Me.LblLQCGroupReq.TabIndex = 666
        Me.LblLQCGroupReq.Text = "Ä"
        '
        'TxtLQCGroup
        '
        Me.TxtLQCGroup.AgMandatory = True
        Me.TxtLQCGroup.AgMasterHelp = True
        Me.TxtLQCGroup.AgNumberLeftPlaces = 0
        Me.TxtLQCGroup.AgNumberNegetiveAllow = False
        Me.TxtLQCGroup.AgNumberRightPlaces = 0
        Me.TxtLQCGroup.AgPickFromLastValue = False
        Me.TxtLQCGroup.AgRowFilter = ""
        Me.TxtLQCGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLQCGroup.AgSelectedValue = Nothing
        Me.TxtLQCGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLQCGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtLQCGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLQCGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLQCGroup.Location = New System.Drawing.Point(308, 70)
        Me.TxtLQCGroup.MaxLength = 50
        Me.TxtLQCGroup.Name = "TxtLQCGroup"
        Me.TxtLQCGroup.Size = New System.Drawing.Size(358, 18)
        Me.TxtLQCGroup.TabIndex = 0
        '
        'LblLQCGroup
        '
        Me.LblLQCGroup.AutoSize = True
        Me.LblLQCGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLQCGroup.Location = New System.Drawing.Point(197, 71)
        Me.LblLQCGroup.Name = "LblLQCGroup"
        Me.LblLQCGroup.Size = New System.Drawing.Size(66, 16)
        Me.LblLQCGroup.TabIndex = 661
        Me.LblLQCGroup.Text = "QC Group"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(57, 108)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(749, 282)
        Me.Pnl1.TabIndex = 1
        '
        'TempQCGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 446)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.LblLQCGroupReq)
        Me.Controls.Add(Me.TxtLQCGroup)
        Me.Controls.Add(Me.LblLQCGroup)
        Me.Name = "TempQCGroup"
        Me.Text = "Temp QC Group"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblLQCGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtLQCGroup, 0)
        Me.Controls.SetChildIndex(Me.LblLQCGroupReq, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
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

    Friend WithEvents LblLQCGroup As System.Windows.Forms.Label
    Friend WithEvents TxtLQCGroup As AgControls.AgTextBox
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents LblLQCGroupReq As System.Windows.Forms.Label


#End Region

    Private Sub FrmShade_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation

        If TxtLQCGroup.Text.Trim = "" Then MsgBox("QC Group Is Required!") : passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Parameter).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, Dgl1.Columns(Col1Parameter).Index) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From QcGroup Where Description='" & TxtLQCGroup.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then MsgBox("QC Group Already Exists") : passed = False : Exit Sub

            mQry = "Select count(*) From QcGroup_log Where Description='" & TxtLQCGroup.Text & "' And EntryStatus='" & ClsMain.LogStatus.LogOpen & "' and IsNull(MoveToLog,'')=''  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then MsgBox("QC Group Already Exists in Log File") : passed = False : Exit Sub
        Else
            mQry = "Select count(*) From QcGroup Where Description='" & TxtLQCGroup.Text & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then MsgBox("QC Group Already Exists") : passed = False : Exit Sub

            mQry = "Select count(*) From QcGroup_log Where Description='" & TxtLQCGroup.Text & "' And UID <>'" & mSearchCode & "' And EntryStatus='" & ClsMain.LogStatus.LogOpen & "' and IsNull(MoveToLog,'')=''  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then MsgBox("QC Group Already Exists in Log File") : passed = False : Exit Sub
        End If
    End Sub

    Private Sub FrmQCGroup_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "S.Div_Code") & " "
        AgL.PubFindQry = "SELECT S.UID, S.Description [QC Group] " & _
                        " FROM QcGroup_Log  S " & _
                        " LEFT JOIN SiteMast C On S.Site_Code = C.Code " & mConStr & _
                        " And S.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  "
        AgL.PubFindQryOrdBy = "[QC Group]"
    End Sub

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1  " & AgL.RetDivisionCondition(AgL, "S.Div_Code") & " "
        AgL.PubFindQry = "SELECT S.Code, S.Description [QC Group] " & _
                        " FROM QcGroup  S " & _
                        " LEFT JOIN SiteMast C On S.Site_Code = C.Code  " & mConStr & _
                        " WHERE  IsNull(S.IsDeleted,0)=0 "
        AgL.PubFindQryOrdBy = "[Godown Name]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "QcGroup"
        LogTableName = "QcGroup_LOG"
        MainLineTableCsv = "QcGroupDetail"
        LogLineTableCsv = "QcGroupDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmShade_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        mQry = "Update QcGroup_Log " & _
                "   SET  " & _
                "	Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & ", " & _
                "	Description = " & AgL.Chk_Text(TxtLQCGroup.Text) & " " & _
                "   Where UID = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete From QcGroupDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Parameter, I).Value <> "" Then
                    mSr += 1
                    
                    mQry = " INSERT INTO QcGroupDetail_Log (Code, Sr, Parameter, StdValue, UID) " & _
                            " VALUES (" & AgL.Chk_Text(mInternalCode) & "," & mSr & "," & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Parameter, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1StdValue, I).Value) & ", " & AgL.Chk_Text(mSearchCode) & " ) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                End If
            Next
        End With

    End Sub

    Private Sub TempQCGroup_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub FrmQuality1_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        'TxtSite_Code.Enabled = False
        'If AgL.StrCmp(Topctrl1.Mode, "Add") Then
        '    TxtSite_Code.AgSelectedValue = AgL.PubSiteCode
        'End If
    End Sub

    Public Overridable Sub Topctrl_tbAdd() Handles Topctrl1.tbAdd
        ' TxtSite_Code.AgSelectedValue = AgL.PubSiteCode
        TxtLQCGroup.Focus()
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As QCGroup, Div_Code ,IsNull( IsDeleted,0) as IsDeleted " & _
            " From QcGroup  " & _
            " Order By Description"
        TxtLQCGroup.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select Code As SearchCode " & _
            " From QcGroup " & _
            " WHERE IsNull(IsDeleted,0)=0 Order By Description "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmShade_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        mQry = "Select UID As SearchCode " & _
               " From QcGroup_log " & _
               " WHERE EntryStatus='" & LogStatus.LogOpen & "' Order By Description"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub TempQCGroup_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Parameter, 300, 50, Col1Parameter, True, False, False)
            .AddAgTextColumn(Dgl1, Col1StdValue, 300, 50, Col1StdValue, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Anchor = AnchorStyles.None
        Pnl1.Anchor = Dgl1.Anchor
        Dgl1.ColumnHeadersHeight = 35

    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select * " & _
                " From QcGroup Where Code ='" & SearchCode & "'"
        Else
            mQry = "Select * " & _
                " From QcGroup_Log Where UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtLQCGroup.Text = AgL.XNull(.Rows(0)("Description"))
            End If
        End With

        '-------------------------------------------------------------
        'Line Records are showing in Grid
        '-------------------------------------------------------------
        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select * From QcGroupDetail where Code = '" & SearchCode & "' Order By Sr"
        Else
            mQry = "Select * from QcGroupDetail_Log where UID = '" & SearchCode & "' Order By Sr"
        End If

        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Parameter, I).Value = AgL.XNull(.Rows(I)("Parameter"))
                    Dgl1.Item(Col1StdValue, I).Value = AgL.XNull(.Rows(I)("StdValue"))
                Next I
            End If
        End With

    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtLQCGroup.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtLQCGroup.Focus()
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name

            'Case Col1Book
            '    Dgl1.AgRowFilter(Dgl1.Columns(Col1Book).Index) = " IsDeleted = 0 And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Status='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
            '                            " AND ItemType IN( " & AgL.Chk_Text(ClsMain.ItemType.Book) & " ," & AgL.Chk_Text(ClsMain.ItemType.Generals) & " ) "

        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                'Case Col1BookId
                '    Validating_BookId(Dgl1.AgSelectedValue(Col1BookId, mRowIndex), mRowIndex)


            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
